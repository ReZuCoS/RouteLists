using RouteLists.Model;
using Spire.Xls;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace RouteLists.ViewModel
{
    public static class ExcelRouteListCreator
    {
        public static bool IsExcelFileCreated(RouteList routeList, string path)
        {
            Workbook workbook = new Workbook()
            {
                DefaultFontName = "Times New Roman",
                DefaultFontSize = 12
            };

            int worksheetsCount = workbook.Worksheets.Count - 1;
            int routePointsCount = routeList.RoutePoints.Count;

            for (int i = worksheetsCount; i > 0; i--)
            {
                workbook.Worksheets[i].Remove();
            }

            Worksheet sheet = workbook.Worksheets[0];

            sheet.Name = routeList.ListNumber;
            sheet.PageSetup.PaperSize = PaperSizeType.PaperA4;

            sheet.PageSetup.HeaderMarginInch = 1;
            sheet.PageSetup.FooterMarginInch = 1;
            sheet.PageSetup.TopMargin = 0.75;
            sheet.PageSetup.BottomMargin = 0.75;

            sheet.PageSetup.Orientation = PageOrientationType.Landscape;
            sheet.Range[$"A1:H6"].HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range[$"A1:H{routePointsCount + 6}"].VerticalAlignment = VerticalAlignType.Center;

            for (int i = 1; i <= 5; i++)
            {
                sheet.Range[$"A{i}:H{i}"].Merge();
            }

            sheet.Range["A1"].Text = $"Маршрутный лист {routeList.ListNumber}";
            sheet.Range["A2"].Text = "Работника ООО \"СнабСтрой\"";
            sheet.Range["A3"].Text = $"На {routeList.Date:dd MMMM yyyy} г.";
            sheet.Range["A4"].Text = $"Работник: {routeList.Driver.FIO}";
            sheet.Range["A5"].Text = $"Должность: Водитель";

            sheet.Range["A6"].Text = "№\nп/п";
            sheet.Range["A6"].ColumnWidth = 3.5;

            sheet.Range["B6"].Text = "Действие";
            sheet.Range["B6"].ColumnWidth = 9.9;

            sheet.Range["C6"].Text = "Адрес";
            sheet.Range["C6"].ColumnWidth = 24.7;

            sheet.Range["D6"].Text = "Организация";
            sheet.Range["D6"].ColumnWidth = 16.7;

            sheet.Range["E6"].Text = "Менеджер";
            sheet.Range["E6"].ColumnWidth = 12.6;

            sheet.Range["F6"].Text = "Телефон";
            sheet.Range["F6"].ColumnWidth = 18.7;

            sheet.Range["G6"].Text = "Номер счёта";
            sheet.Range["G6"].ColumnWidth = 15.9;

            sheet.Range["H6"].Text = "Комментарий";
            sheet.Range["H6"].ColumnWidth = 14.4;

            sheet.Range[$"A6:H{routePointsCount + 6}"].BorderInside(LineStyleType.Thin);
            sheet.Range[$"A6:H{routePointsCount + 6}"].BorderAround(LineStyleType.Thin);
            sheet.Range[$"A6:H{routePointsCount + 6}"].Style.Font.Size = 14;

            for (int i = 1; i <= routePointsCount; i++)
            {
                RoutePoint routePoint = routeList.RoutePoints.First(rp => rp.PointNumber == i);

                sheet.Range[$"A{i + 6}"].Text = routePoint.PointNumber.ToString();
                sheet.Range[$"A{i + 6}"].HorizontalAlignment = HorizontalAlignType.Center;

                sheet.Range[$"B{i + 6}"].Text = routePoint.Action;
                sheet.Range[$"B{i + 6}"].HorizontalAlignment = HorizontalAlignType.Center;

                sheet.Range[$"C{i + 6}"].Text = routePoint.Address;
                sheet.Range[$"C{i + 6}"].IsWrapText = true;

                sheet.Range[$"D{i + 6}"].Text = routePoint.Manager.Company.Title;
                sheet.Range[$"D{i + 6}"].IsWrapText = true;

                sheet.Range[$"E{i + 6}"].Text = routePoint.Manager.Name;
                sheet.Range[$"E{i + 6}"].IsWrapText = true;

                sheet.Range[$"F{i + 6}"].Text = routePoint.Manager.Phone;

                sheet.Range[$"G{i + 6}"].Text = routePoint.InvoiceNumbers;
                sheet.Range[$"G{i + 6}"].IsWrapText = true;

                sheet.Range[$"H{i + 6}"].Text = routePoint.Comment;
                sheet.Range[$"H{i + 6}"].IsWrapText = true;
            }

            string driverFio = routeList.Driver.Patronymic == "" ?
                $"{routeList.Driver.Surname} {routeList.Driver.Name[0]}" :
                $"{routeList.Driver.Surname} {routeList.Driver.Name[0]}.{routeList.Driver.Patronymic[0]}";

            sheet.Range[$"A{routePointsCount + 7}:D{routePointsCount + 7}"].Merge();
            sheet.Range[$"A{routePointsCount + 7}"].Text = $"Маршрутный лист выдан \"___\" ________ {DateTime.Now.Year} г. в __ ч __ мин";


            sheet.Range[$"E{routePointsCount + 7}:H{routePointsCount + 7}"].Merge();
            sheet.Range[$"E{routePointsCount + 7}"].Text = $"Маршрутный лист получен \"___\" ________ {DateTime.Now.Year} г. в __ ч __ мин";


            sheet.Range[$"B{routePointsCount + 9}"].Text = "____________/";
            sheet.Range[$"B{routePointsCount + 9}"].HorizontalAlignment = HorizontalAlignType.Right;
            
            sheet.Range[$"C{routePointsCount + 9}"].Text = "Иманов К.Э, Генеральный директор";
            sheet.Range[$"C{routePointsCount + 9}"].HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range[$"B{routePointsCount + 10}"].Text = "(подпись)";
            sheet.Range[$"B{routePointsCount + 10}"].HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[$"C{routePointsCount + 10}"].Text = "(Ф.И.О, должность)";
            sheet.Range[$"C{routePointsCount + 10}"].HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[$"E{routePointsCount + 9}"].Text = "____________/";
            sheet.Range[$"E{routePointsCount + 9}"].HorizontalAlignment = HorizontalAlignType.Right;

            sheet.Range[$"F{routePointsCount + 9}"].Text = $"{driverFio}, Водитель";
            sheet.Range[$"F{routePointsCount + 9}"].HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range[$"E{routePointsCount + 10}"].Text = "(подпись)";
            sheet.Range[$"E{routePointsCount + 10}"].HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[$"F{routePointsCount + 10}"].Text = "(Ф.И.О, должность)";
            sheet.Range[$"F{routePointsCount + 10}"].HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Rows[routePointsCount + 6].RowHeight = 22.5;
            sheet.Rows[routePointsCount + 7].RowHeight = 5.9;

            sheet.Range[$"A{routePointsCount + 12}:D{routePointsCount + 12}"].Merge();
            sheet.Range[$"A{routePointsCount + 12}"].Text = $"Маршрутный лист выдан \"___\" ________ {DateTime.Now.Year} г. в __ ч __ мин";

            sheet.Range[$"E{routePointsCount + 12}:H{routePointsCount + 12}"].Merge();
            sheet.Range[$"E{routePointsCount + 12}"].Text = $"Маршрутный лист получен \"___\" ________ {DateTime.Now.Year} г. в __ ч __ мин";

            sheet.Range[$"B{routePointsCount + 14}"].Text = "____________/";
            sheet.Range[$"B{routePointsCount + 14}"].HorizontalAlignment = HorizontalAlignType.Right;

            sheet.Range[$"C{routePointsCount + 14}"].Text = $"{driverFio}, Водитель";
            sheet.Range[$"C{routePointsCount + 14}"].HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range[$"B{routePointsCount + 15}"].Text = "(подпись)";
            sheet.Range[$"B{routePointsCount + 15}"].HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range[$"C{routePointsCount + 15}"].Text = "(Ф.И.О, должность)";
            sheet.Range[$"C{routePointsCount + 15}"].HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range[$"E{routePointsCount + 14}"].Text = "____________/";
            sheet.Range[$"E{routePointsCount + 14}"].HorizontalAlignment = HorizontalAlignType.Right;

            sheet.Range[$"F{routePointsCount + 14}"].Text = "Иманов К.Э, Генеральный директор";
            sheet.Range[$"F{routePointsCount + 14}"].HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range[$"E{routePointsCount + 15}"].Text = "(подпись)";
            sheet.Range[$"E{routePointsCount + 15}"].HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range[$"F{routePointsCount + 15}"].Text = "(Ф.И.О, должность)";
            sheet.Range[$"F{routePointsCount + 15}"].HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Rows[routePointsCount + 10].RowHeight = 5.9;
            sheet.Rows[routePointsCount + 12].RowHeight = 5.9;

            try
            {
                workbook.SaveToFile(path);
                return true;
            }
            catch
            {
                MessageBox.Show("Файл используется другим приложением!\nЗакройте приложение и повторите попытку",
                    "Ошибка при сохранении файла", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        internal static void OpenFile(string fileName)
        {
            Process.Start(fileName);
        }
    }
}
