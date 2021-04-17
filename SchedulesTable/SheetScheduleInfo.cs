using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Autodesk.Revit.DB;

namespace SchedulesTable
{
    public class SheetScheduleInfo
    {
        public int SheetNumber;
        public string ScheduleName;

        public SheetScheduleInfo(ScheduleSheetInstance ssi, ViewSheet sheet, Settings sets)
        {
            string sheetNumberString = "";
            if(sets.useStandardSheetNumber)
            {
                sheetNumberString = sheet.get_Parameter(BuiltInParameter.SHEET_NUMBER).AsString();
            }
            else
            {
                Parameter complectParam = sheet.LookupParameter(sets.sheetComplectParamName);
                if (complectParam == null || !complectParam.HasValue)
                {
                    string msg = "Unable to get " + sets.altSheetNumberParam + " from sheet id " + sheet.Id.IntegerValue;
                    Debug.WriteLine(msg);
                    throw new Exception(msg);
                }
            }
            if(sheetNumberString == "")
            {
                throw new Exception("Unable to get sheet number for sheet: " + sheet.Name);
            }
            SheetNumber = Convert.ToInt32(Regex.Replace(sheetNumberString, @"[^\d]+", ""));

            Regex regex = new Regex(@"\*(?<name>.+)\*");
            Match match = regex.Match(ssi.Name);
            ScheduleName = match.Groups["name"].Value;
        }
    }
}
