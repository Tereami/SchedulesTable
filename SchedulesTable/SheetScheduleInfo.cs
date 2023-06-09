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
        public string SheetNumberString;
        public int SheetNumberInt;
        public string ScheduleName;

        public SheetScheduleInfo(ScheduleSheetInstance ssi, ViewSheet sheet, Settings sets)
        {
            Debug.WriteLine("Start creating new info, schedule " + ssi.Name + ", sheet: " + sheet.Name);
            string sheetNumberStringRaw = "";
            if (sets.useStandardSheetNumber)
            {
                sheetNumberStringRaw = sheet.get_Parameter(BuiltInParameter.SHEET_NUMBER).AsString();
            }
            else
            {
                Parameter sheetNumberParam = sheet.LookupParameter(sets.altSheetNumberParam);
                if (sheetNumberParam == null || !sheetNumberParam.HasValue)
                {
                    string msg = "Unable to get " + sets.altSheetNumberParam + " from sheet id " + sheet.Id.IntegerValue;
                    Debug.WriteLine(msg);
                    throw new Exception(msg);
                }
                else
                {
                    sheetNumberStringRaw = sheetNumberParam.AsString();
                }
            }
            if (sheetNumberStringRaw == "")
            {
                throw new Exception("Unable to get sheet number for sheet: " + sheet.Name);
            }
            SheetNumberString = Regex.Replace(sheetNumberStringRaw, @"[^\d]+", "");
            SheetNumberInt = Convert.ToInt32(SheetNumberString);

            Regex regex = new Regex(@"\*(?<name>.+)\*");
            Match match = regex.Match(ssi.Name);
            ScheduleName = match.Groups["name"].Value;
            Debug.WriteLine("Done, sheet number: " + SheetNumberString + ", name: " + ScheduleName);
        }
    }
}
