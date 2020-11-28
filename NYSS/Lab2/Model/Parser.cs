using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using ClosedXML.Excel;

namespace Lab2.Model
{
    public static class Parser
    {
        public static IEnumerable<Data> EnumerateFullData(String xlsxpath)
        {
            using (var workbook = new XLWorkbook(xlsxpath))
            {
                var worksheet = workbook.Worksheets.Worksheet(1);
                for (var row = 3; !String.IsNullOrEmpty(worksheet.Cell(row, 1).GetString()); ++row)
                {
                    var data = new Data
                    {
                        Id = worksheet.Cell(row, 1).GetValue<Int32>(),
                        Name = worksheet.Cell(row, 2).GetValue<String>(),
                        Description = worksheet.Cell(row, 3).GetValue<String>(),
                        Source = worksheet.Cell(row, 4).GetValue<String>(),
                        Target = worksheet.Cell(row, 5).GetValue<String>(),
                        ConfidentialityOffense = worksheet.Cell(row, 6).GetValue<String>() == "1" ? "да" : "нет",
                        ContinuityOffense = worksheet.Cell(row, 7).GetValue<String>() == "1" ? "да" : "нет",
                        AvailabilityOffense = worksheet.Cell(row, 8).GetValue<String>() == "1" ? "да" : "нет"
                    };
                    yield return data;
                }
            }
        }

        public static IEnumerable<ShortData> EnumerateShortFromFullData(ObservableCollection<Data> data)
        {
            foreach (var item in data)
            {
                var shortData = new ShortData
                {
                    Id = ShortDataId(item.Id.ToString()),
                    Name = item.Name
                };
                yield return shortData;
            }
        }

        private static String ShortDataId(String id)
        {
            switch (id.Length)
            {
                case 3:
                    return "УБИ." + id;
                case 2:
                    return "УБИ.0" + id;
                case 1:
                    return "УБИ.00" + id;
            }
            return String.Empty;
        }
    }
}