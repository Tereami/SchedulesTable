#region License
/*Данный код опубликован под лицензией Creative Commons Attribution-ShareAlike.
Разрешено использовать, распространять, изменять и брать данный код за основу для производных в коммерческих и
некоммерческих целях, при условии указания авторства и если производные лицензируются на тех же условиях.
Код поставляется "как есть". Автор не несет ответственности за возможные последствия использования.
Зуев Александр, 2020, все права защищены.
This code is listed under the Creative Commons Attribution-ShareAlike license.
You may use, redistribute, remix, tweak, and build upon this work non-commercially and commercially,
as long as you credit the author by linking back and license your new creations under the same terms.
This code is provided 'as is'. Author disclaims any implied warranty.
Zuev Aleksandr, 2020, all rigths reserved.*/
#endregion
#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Autodesk.Revit.DB; //для работы с элементами модели Revit
using Autodesk.Revit.UI; //для работы с элементами интерфейса
using Autodesk.Revit.UI.Selection; //работы с выделенными элементами
using System.Text.RegularExpressions;
#endregion

namespace SchedulesTable
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    class CommandCreateTable : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Settings sets = null;
            try
            {
                sets = Settings.Activate();
            }
            catch (OperationCanceledException)
            {
                return Result.Cancelled;
            }

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            //получаю ведомость спецификаций
            ViewSchedule templateVs = null;
            ViewSheet firstSheet = null;
            View curView = doc.ActiveView;
            if (curView is ViewSchedule)
            {
                templateVs = curView as ViewSchedule;
                if (!templateVs.Name.Contains("спецификаций"))
                {
                    message = "Активная спецификация - не ведомость спецификаций.";
                    return Result.Failed;
                }

                List<ViewSheet> scheduleSheets = Support.GetSheetsContainsScheduleInstances(doc, templateVs);
                if (scheduleSheets.Count > 1)
                {
                    message = "Ведомость размещена на нескольких листах! Невозможно определить принадлежность к комплекту";
                    return Result.Failed;
                }
                firstSheet = scheduleSheets[0];
                if (firstSheet == null)
                {
                    message = "Ведомость спецификаций не размещена на листе общих данных";
                    return Result.Failed;
                }
            }
            else if (curView is ViewSheet)
            {
                ElementId ssiId = null;
                Selection sel = uiDoc.Selection;
                List<ElementId> selIds = sel.GetElementIds().ToList();
                if (selIds.Count == 0)
                {
                    try
                    {
                        Reference refer = sel.PickObject(ObjectType.Element,
                            new ScheduleSelectionFilter(),
                            "Выберите ведомость на листе общих данных");
                        ssiId = refer.ElementId;
                    }
                    catch
                    {
                        return Result.Cancelled;
                    }
                }
                else
                {
                    ssiId = selIds.First();
                }

                ScheduleSheetInstance selSse = doc.GetElement(ssiId) as ScheduleSheetInstance;
                if (selSse == null)
                {
                    message = "Выбранный элемент - не ведомость спецификаций.";
                    return Result.Failed;
                }
                if (!selSse.Name.Contains("спецификаций"))
                {
                    message = "Выбранная спецификация - не ведомость спецификаций.";
                    return Result.Failed;
                }
                firstSheet = curView as ViewSheet;
                templateVs = doc.GetElement(selSse.ScheduleId) as ViewSchedule;
            }
            else
            {
                message = "Перед запуском плагина откройте ведомость спецификаций или выберите её на листе общих данных.";
                return Result.Failed;
            }

            List<ViewSheet> sheetsAll = Support.GetAllSheetsFromDocument(doc);
            List<ViewSheet> sheets = new List<ViewSheet>();

            List<Document> docs = new List<Document> { doc };

            if (sets.getLinkFiles)
            {
                docs.AddRange(Support.GetAllLinkedDocs(doc));
            }

            string sheetComplect = "";
            if (sets.useComplects)
            {
                Parameter sheetComplectParam = firstSheet.LookupParameter(sets.sheetComplectParamName);
                if (sheetComplectParam == null || !sheetComplectParam.HasValue)
                {
                    message = "Не задан Комплект у листа, на котором расположена Ведомость спецификаций";
                    return Result.Failed;
                }
                sheetComplect = sheetComplectParam.AsString();

                List<ViewSheet> sheetsCurComplect = sheetsAll
                    .Where(i => i.LookupParameter(sets.sheetComplectParamName).AsString() == sheetComplect)
                    .ToList();
                sheets.AddRange(sheetsCurComplect);
            }

            List<SheetScheduleInfo> infos = new List<SheetScheduleInfo>();
            foreach (Document curDoc in docs)
            {
                List<SheetScheduleInfo> curInfos = Support.GetSchedulesInfo(curDoc, sets, sheetComplect);
                infos.AddRange(curInfos);
            }

            Debug.WriteLine("Schedules found: " + infos.Count);
            if (infos.Count == 0)
            {
                message = "Подходящие спецификаци не найдены!";
                message += "Задайте имя нужных спецификаций с использованием символа * по аналогу: КЖ0_Детали_*Спецификация конструкций*";
                return Result.Failed;
            }

            //получаю табличные данные для дальнейшей работы с ними
            TableData tData = templateVs.GetTableData();
            TableSectionData tsd = tData.GetSectionData(SectionType.Header);

            int lastRowNumber = tsd.LastRowNumber;
            if (lastRowNumber < 4)
            {
                message = "Строк должно быть не менее 3 штук.";
                return Result.Failed;
            }

            TableCellStyle cellStyle1 = tsd.GetTableCellStyle(lastRowNumber, 0);
            TableCellStyle cellStyle2 = tsd.GetTableCellStyle(lastRowNumber, 1);
            TableCellStyle cellStyle3 = tsd.GetTableCellStyle(lastRowNumber, 2);

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Ведомость спецификаций");

                //удаляю лишние промежуточные строки
                while (tsd.LastRowNumber > 4)
                {
                    tsd.RemoveRow(3);
                }

                //очищаю ячейки на всякий случай
                tsd.ClearCell(2, 0);
                tsd.ClearCell(2, 1);
                tsd.ClearCell(3, 0);
                tsd.ClearCell(3, 1);
                tsd.ClearCell(4, 0);
                tsd.ClearCell(4, 1);

                //добавляю пустые строки
                for (int i = 0; i < infos.Count - 3; i++)
                {
                    tsd.InsertRow(tsd.LastRowNumber);

                    tsd.SetRowHeight(tsd.LastRowNumber - 1, sets.rowHeight);
                    tsd.SetCellStyle(tsd.LastRowNumber - 1, 0, cellStyle1);
                    tsd.SetCellStyle(tsd.LastRowNumber - 1, 1, cellStyle2);
                    tsd.SetCellStyle(tsd.LastRowNumber - 1, 2, cellStyle3);
                }

                
                for (int i = 0; i < infos.Count; i++)
                {
                    int curRowNumber = i + 2;
                    SheetScheduleInfo info = infos[i];
                    tsd.SetCellText(curRowNumber, 0, info.SheetNumber.ToString());
                    tsd.SetCellText(curRowNumber, 1, info.ScheduleName);

                    //Увеличу высоту строки, если текст слишком длинный
                    if (info.ScheduleName.Length > sets.maxCharsInOneLine)
                    {
                        tsd.SetRowHeight(curRowNumber, sets.rowHeight * sets.rowHeightCoeff);
                    }
                    else
                    {
                        tsd.SetRowHeight(curRowNumber, sets.rowHeight);
                    }
                }

                t.Commit();
            }
            sets.Save();
            return Result.Succeeded;
        }
    }
}
