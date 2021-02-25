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
using Autodesk.Revit.DB;
#endregion

namespace SchedulesTable
{
    public static class Support
    {
        public static List<ViewSheet> GetScheduleSheets(Document doc, ViewSchedule vs)
        {
            int scheduleId = vs.Id.IntegerValue;
            List<ScheduleSheetInstance> ssis = new FilteredElementCollector(doc)
                .OfClass(typeof(ScheduleSheetInstance))
                .Cast<ScheduleSheetInstance>()
                .Where(i => i.ScheduleId.IntegerValue == scheduleId)
                .ToList();

            if (ssis.Count == 0) return null;

            List<ViewSheet> sheets = new List<ViewSheet>();

            foreach(ScheduleSheetInstance ssi in ssis)
            {
                ElementId sheetId = ssi.OwnerViewId;
                ViewSheet sheet = doc.GetElement(sheetId) as ViewSheet;
                sheets.Add(sheet);
            }

            
            
            return sheets;
        }

        public static List<ViewSheet> GetAllSheetsFromDocument(Document doc)
        {
            List<ViewSheet> sheets = new FilteredElementCollector(doc)
                .OfClass(typeof(ViewSheet))
                .WhereElementIsNotElementType()
                .Cast<ViewSheet>()
                .ToList();
            return sheets;
        }


        public static List<Document> GetAllLinkedDocs(Document doc)
        {
            List<Document> docs = new List<Document>();

            List<RevitLinkInstance> links = new FilteredElementCollector(doc)
                .OfClass(typeof(RevitLinkInstance))
                .Cast<RevitLinkInstance>()
                .ToList();
            

            foreach(RevitLinkInstance rli in links)
            {
                Document linkDoc = rli.GetLinkDocument();
                if (linkDoc == null) continue;
                if (docs.Contains(linkDoc)) continue;

                docs.Add(linkDoc);
            }
            return docs;
        }
    
    }
}
