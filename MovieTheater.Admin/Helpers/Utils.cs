using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MovieTheater.Models.Catalog.Invoice;
using OfficeOpenXml;

namespace MovieTheater.Admin.Helpers
{
    public class Utils
    {
        public static Stream GetExcelRawData(DateTime fromDate, DateTime toDate ,List<InvoiceRawData> rawDatas, FileInfo path)
        {
            Stream stream = new MemoryStream();
            if (path.Exists)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using ExcelPackage p = new ExcelPackage(path);
                ExcelWorksheet sheet = p.Workbook.Worksheets[0];
                sheet.Cells[3, 3].Value = fromDate.ToString("dd-MM-yyyy");
                sheet.Cells[3, 6].Value = toDate.ToString("dd-MM-yyyy");

                for (int i = 6; i < rawDatas.Count + 6; i++)
                {
                    sheet.Cells[i, 1].Value = rawDatas[i-6].InvoiceId;
                    sheet.Cells[i, 2].Value = rawDatas[i-6].ReservationId;
                    sheet.Cells[i, 3].Value = rawDatas[i-6].Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    sheet.Cells[i, 4].Value = rawDatas[i-6].MovieId;
                    sheet.Cells[i, 5].Value = rawDatas[i-6].MovieName;
                    sheet.Cells[i, 6].Value = rawDatas[i-6].Payment;
                    sheet.Cells[i, 7].Value = rawDatas[i-6].ScreeningTime.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    sheet.Cells[i, 8].Value = rawDatas[i-6].Tickets;
                    sheet.Cells[i, 9].Value = rawDatas[i-6].TotalPrice;
                    
                }
                p.SaveAs(stream);
                stream.Position = 0;
            }
            return stream;
        }
    }
}
