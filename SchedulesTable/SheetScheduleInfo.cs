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
        public int RowsCount = 1;

        public SheetScheduleInfo(ScheduleSheetInstance ssi, ViewSheet sheet, Settings sets)
        {
            Trace.WriteLine("Start creating new info, schedule " + ssi.Name + ", sheet: " + sheet.Name);
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
                    string msg = "Failed to get " + sets.altSheetNumberParam + " from sheet id " + sheet.Id.GetValue();
                    Trace.WriteLine(msg);
                    throw new Exception(msg);
                }
                else
                {
                    sheetNumberStringRaw = sheetNumberParam.AsString();
                }
            }
            if (sheetNumberStringRaw == "")
            {
                throw new Exception("Failed to get sheet number for sheet: " + sheet.Name);
            }
            SheetNumberString = Regex.Replace(sheetNumberStringRaw, @"[^\d]+", "");
            SheetNumberInt = Convert.ToInt32(SheetNumberString);

            Regex regex = new Regex(@"\*(?<name>.+)\*");
            Match match = regex.Match(ssi.Name);
            string scheduleNameRaw = match.Groups["name"].Value;

            if(scheduleNameRaw.Contains(sets.newLineSymbol))
            {
                RowsCount = scheduleNameRaw.Split(sets.newLineSymbol[0]).Length;
                scheduleNameRaw = scheduleNameRaw.Replace(sets.newLineSymbol, System.Environment.NewLine);
            }
            else if(scheduleNameRaw.Length > sets.maxCharsInOneLine)
            {
                double rowsCountRaw = (double)scheduleNameRaw.Length / (double)sets.maxCharsInOneLine;
                RowsCount =(int)Math.Ceiling(rowsCountRaw);
            }

            ScheduleName = scheduleNameRaw;

            Trace.WriteLine("Completed, sheet number: " + SheetNumberString + ", name: " + ScheduleName);
        }
    }
}