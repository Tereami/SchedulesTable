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
using System.Text;
using System.Threading.Tasks;
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
            List<string> sheetParams = new List<string>();
            sheetParams.Add("Орг.КомплектЧертежей");
            sheetParams.Add("SHT_Комплект_Марка");
            sheetParams.Add("Раздел проекта");
            sheetParams.Add("SHT_Альбом_ID");
            sheetParams.Add("Назначение вида");

            List<string> sheetNumberParams = new List<string>();
            sheetNumberParams.Add("SHT_Номер листа");
            sheetNumberParams.Add("Ш.НомерЛиста");
            sheetNumberParams.Add("Номер листа");


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

                List<ViewSheet> scheduleSheets = Support.GetScheduleSheets(doc, templateVs);
                if(scheduleSheets.Count > 1)
                {
                    message = "Ведомость размещена на нескольких листах! Невозможно определить принадлежность к комплекту. Листы: ";
                    foreach(ViewSheet vs in scheduleSheets)
                    {
                        message += vs.SheetNumber + " : " + vs.Name + "; ";
                    }
                    return Result.Failed;
                }
                firstSheet = scheduleSheets[0];
                string sheetName = firstSheet.Name;
                if(firstSheet == null)
                {
                    message = "Ведомость спецификаций не размещена на листе общих данных";
                    return Result.Failed;
                }
            }
            else if(curView is ViewSheet)
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
                if(selSse == null)
                {
                    message = "Выбранный элемент - не ведомость спецификаций.";
                    return Result.Failed;
                }
                if(!selSse.Name.Contains("спецификаций"))
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


            //проверю, какой из параметров используется для "комплекта" в данном файле
            string sheetComplectParamName = "";
            foreach (string paramCompl in sheetParams)
            {
                Parameter param = firstSheet.LookupParameter(paramCompl);
                if (param != null)
                {
                    string test = param.AsString();
                    if (!String.IsNullOrEmpty(test))
                    {
                        sheetComplectParamName = paramCompl;
                        break;
                    }
                }
            }


            List<ViewSheet> sheetsAll = Support.GetAllSheetsFromDocument(doc);
            List<ViewSheet> sheets = new List<ViewSheet>();
            //попытаюсь добавить листы из связанных файлов
            List<Document> linkDocs = Support.GetAllLinkedDocs(doc);
            foreach (Document linkDoc in linkDocs)
            {
                List<ViewSheet> linkSheets = Support.GetAllSheetsFromDocument(linkDoc);
                sheetsAll.AddRange(linkSheets);
            }

            string sheetComplect = "";
            //может оказаться, что комплектов вообще нет, в этом случае оставим все листы в проекте
            if (sheetComplectParamName == "")
            {
                TaskDialog.Show("Внимание", "Комплекты чертежей не обнаружены. Ведомость будет составлена на весь проект");
            }
            else
            {
                //если комплекты есть - дополнительно отфильтруем список
                sheetComplect = firstSheet.LookupParameter(sheetComplectParamName).AsString();

                foreach (ViewSheet sheet in sheetsAll)
                {
                    Parameter sheetComplectParam = sheet.LookupParameter(sheetComplectParamName);
                    if (sheetComplectParam == null)
                    {
                        sheetComplectParam = sheet.get_Parameter(new Guid("e1b06433-f527-403c-8986-af9a01e6be7f")); //"Орг.КомплектЧертежей" или "Орг.ОбознчТома(Комплекта)"
                        if (sheetComplectParam == null) continue;
                    }
                    string sheetComplectTest = sheetComplectParam.AsString();
                    if (sheetComplectTest == sheetComplect)
                    {
                        sheets.Add(sheet);
                    }
                }
            }


            //проверю, какой из параметров используется для "номера листа" в данном файле
            string sheetNumberParamName = "";
            foreach (string paramNum in sheetNumberParams)
            {
                Parameter param = firstSheet.LookupParameter(paramNum);
                if (param != null)
                {
                    string test = param.AsString();
                    if (!String.IsNullOrEmpty(test))
                    {
                        sheetNumberParamName = paramNum;
                        break;
                    }
                }
            }


            //получаю список спецификаций, размещенных на полученных листах
            SortedDictionary<int, List<ViewSchedule>> schedulesOnSheets =
                new SortedDictionary<int, List<ViewSchedule>>();

            foreach (ViewSheet sheet in sheets)
            {
                Document curDoc = null;
                if (sheet.Document.Title == doc.Title)
                {
                    curDoc = doc;
                }
                else
                {
                    curDoc = sheet.Document;
                }

                Parameter sheetNumberParam = sheet.LookupParameter(sheetNumberParamName);
                if (sheetNumberParam == null)
                {
                    string errmsg = "Добавьте параметр " + sheetNumberParamName + " в файл " + curDoc.Title;
                    TaskDialog.Show("Ошибка", errmsg);
                    continue;
                }
                string sheetNumberString = sheetNumberParam.AsString();

                if (sheetNumberString == null) continue;

                if (sheetNumberString.Contains("-"))
                {
                    sheetNumberString = sheetNumberString.Split('-').Last();
                }
                int sheetNumber = 0;
                try
                {
                    sheetNumber = Convert.ToInt32(Regex.Replace(sheetNumberString, @"[^\d]+", ""));
                }
                catch
                {
                    continue;
                }

                //Получаю из документа только спецификации на данном листе
                List<ScheduleSheetInstance> schsOnCurSheet = new FilteredElementCollector(curDoc)
                    .OfClass(typeof(ScheduleSheetInstance))
                    .Cast<ScheduleSheetInstance>()
                    .Where(s => s.OwnerViewId.IntegerValue == sheet.Id.IntegerValue)
                    .ToList();

                //Получаю только спецификации, имя которых заказчивается на звездочку
                List<ViewSchedule> schs = new List<ViewSchedule>();
                foreach (ScheduleSheetInstance schi in schsOnCurSheet)
                {
                    ElementId schId = schi.ScheduleId;
                    ViewSchedule sch = curDoc.GetElement(schId) as ViewSchedule;
                    string schName = sch.Name;
                    if (schName.EndsWith("*"))
                    {
                        schs.Add(sch);
                    }
                }
                if (schedulesOnSheets.ContainsKey(sheetNumber))
                {
                    schedulesOnSheets[sheetNumber].AddRange(schs);
                }
                else
                {
                    schedulesOnSheets.Add(sheetNumber, schs);
                }
            }

            //сортирую по номеру листа на всякий случай
            schedulesOnSheets.OrderBy(kk => kk.Key);

            //посчитаю количество строк в итоговой ведомости
            int rowsCount = 0;
            foreach (var kvp in schedulesOnSheets)
            {
                List<ViewSchedule> schs = kvp.Value;
                rowsCount += schs.Count;
            }

            if (rowsCount == 0)
            {
                message = "Задайте имя нужных спецификаций с использованием символа * по аналогу: КЖ0_Детали_*Спецификация конструкций*";
                return Result.Failed;
            }

            //получаю табличные данные для дальнейшей работы с ними
            TableData tData = templateVs.GetTableData();
            TableSectionData tsd = tData.GetSectionData(SectionType.Header);

            int lastRowNumber = tsd.LastRowNumber;
            if (lastRowNumber < 4)
            {
                message = "В ведомости должно быть как минимум три пустых строки.";
                return Result.Failed;
            }

            double rowHeigth = 0.027; //tsd.GetRowHeight(lastRowNumber);
            TableCellStyle cellStyle1 = tsd.GetTableCellStyle(lastRowNumber, 0);
            TableCellStyle cellStyle2 = tsd.GetTableCellStyle(lastRowNumber, 1);
            TableCellStyle cellStyle3 = tsd.GetTableCellStyle(lastRowNumber, 2);

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Ведомость спецификаций");

                //удаляю лишние строки
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
                for (int i = 0; i < rowsCount - 3; i++)
                {
                    tsd.InsertRow(tsd.LastRowNumber);

                    tsd.SetRowHeight(tsd.LastRowNumber - 1, rowHeigth);
                    tsd.SetCellStyle(tsd.LastRowNumber - 1, 0, cellStyle1);
                    tsd.SetCellStyle(tsd.LastRowNumber - 1, 1, cellStyle2);
                    tsd.SetCellStyle(tsd.LastRowNumber - 1, 2, cellStyle3);
                }

                //заполняю данные
                int curRowNumber = 2;
                foreach (var kvp in schedulesOnSheets)
                {
                    string sheetNumber = kvp.Key.ToString();
                    List<ViewSchedule> schs = kvp.Value;

                    foreach (ViewSchedule vs in schs)
                    {
                        var regex = new Regex(@"\*(?<name>.+)\*");
                        var match = regex.Match(vs.Name);
                        string scheduleName = match.Groups["name"].Value;

                        tsd.SetCellText(curRowNumber, 0, sheetNumber);
                        tsd.SetCellText(curRowNumber, 1, scheduleName);

                        //Увеличу высоту строки, если текст слишком длинный
                        if (scheduleName.Length > 85)
                        {
                            tsd.SetRowHeight(curRowNumber, rowHeigth * 1.5);
                        }
                        else
                        {
                            tsd.SetRowHeight(curRowNumber, rowHeigth);
                        }

                        curRowNumber++;
                    }
                }

                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
