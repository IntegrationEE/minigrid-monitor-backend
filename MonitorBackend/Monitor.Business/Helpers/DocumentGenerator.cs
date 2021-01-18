using System;
using System.IO;
using Spire.Xls;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Monitor.Common;
using Monitor.Domain.Base;

namespace Monitor.Business
{
    public class DocumentGenerator
    {
        private const string CSV_SEPARATOR = ";";
        private const string NOT_APPLICABLE = "N/A";

        public IList<T> GetDataFromExcel<T>(IFormFile file, (string title, bool required)[] headers)
            where T : BaseYearMonthIndicatorModel
        {
            var response = new List<T>();

            using var stream = new MemoryStream();
            file.CopyTo(stream);

            var workbook = new Workbook();
            if (file.FileName.EndsWith(".csv"))
            {
                workbook.LoadFromStream(stream, CSV_SEPARATOR, 1, 1);
            }
            else
            {
                workbook.LoadFromStream(stream);
            }

            if (workbook.Worksheets.Count == 0)
            { return response; }

            var sheet = workbook.Worksheets[0];
            var columns = new List<(string Header, int Index, bool IsRequired)>();

            for (var rowIndex = 1; rowIndex <= sheet.Range.RowCount; rowIndex++)
            {
                if (rowIndex == 1)
                {
                    for (var columnIndex = 1; columnIndex <= sheet.Columns.Length; columnIndex++)
                    {
                        var header = headers[columnIndex - 1];

                        if (sheet.Range[rowIndex, columnIndex].Text != header.title)
                        { throw new CustomException("Incorrect Header or columns order."); }

                        columns.Add((header.title, columnIndex, header.required));
                    }
                }
                else
                {
                    T item = GetItem<T>(sheet, columns, rowIndex);

                    if (response.Any(z => z.Month == item.Month && z.Year == item.Year))
                    { throw new CustomException($"Found duplicated row (Index: {rowIndex}, Year: {item.Year}, Month: {item.Month})"); }

                    response.Add(item);
                }
            }

            return response;
        }

        public byte[] Generate<T>(T data, string[] headers, FileFormat format)
        {
            return Generate(new List<T>() { data }, headers, format);
        }

        public byte[] Generate<T>(IEnumerable<T> data, string[] headers, FileFormat format)
        {
            return Generate(new List<T>(data), headers, format);
        }

        public byte[] Generate<T>(List<T> data, string[] headers, FileFormat format)
        {
            Workbook workbook = new Workbook();
            workbook.Worksheets.Add("Data");

            Worksheet sheet = workbook.Worksheets[0];
            sheet.InsertRange(1, 1, data.Count + 2, headers.Length, InsertMoveOption.MoveDown, InsertOptionsType.FormatDefault);

            SetHeaders(headers, sheet);

            for (var rowIndex = 0; rowIndex < data.Count; rowIndex++)
            {
                for (var columnIndex = 1; columnIndex < headers.Length + 1; columnIndex++)
                {
                    var value = GetObjectValue(data[rowIndex], headers[columnIndex - 1].Replace(" ", string.Empty));

                    if (value == null)
                    {
                        sheet.Range[rowIndex + 2, columnIndex].Value = NOT_APPLICABLE;
                        sheet.Range[rowIndex + 2, columnIndex].Style.HorizontalAlignment = HorizontalAlignType.Center;
                    }
                    else if (IsNumber(value))
                    {
                        sheet.Range[rowIndex + 2, columnIndex].NumberValue = Convert.ToDouble(value);
                    }
                    else
                    {
                        sheet.Range[rowIndex + 2, columnIndex].DateTimeValue = Convert.ToDateTime(value);
                    }
                }
            }

            AutoFitColumns(sheet);

            using var stream = new MemoryStream();

            if (format == FileFormat.CSV)
            {
                workbook.SaveToStream(stream, CSV_SEPARATOR);
            }
            else
            {
                workbook.SaveToStream(stream, format);
            }

            return stream.ToArray();
        }

        public FileFormat GetFileFormat(Monitor.Common.Enums.FileFormat format)
            => format == Monitor.Common.Enums.FileFormat.CSV ? FileFormat.CSV : FileFormat.Version97to2003;

        private void SetHeaders(string[] headers, Worksheet sheet)
        {
            int cell = 1;

            foreach (var header in headers)
            {
                sheet.Range[1, cell].Text = header;
                sheet.Range[1, cell].Style.Font.IsBold = true;
                sheet.Range[1, cell].Style.ShrinkToFit = true;

                cell++;
            }
        }

        private object GetObjectValue<T>(T item, string name)
        {
            var field = item.GetType()
                .GetProperties()
                .First(x => x.Name.ToLower() == name.ToLower());

            return field.GetValue(item);
        }

        private T GetItem<T>(Worksheet sheet, List<(string Header, int Index, bool IsRequired)> columns, int rowIndex)
            where T : BaseYearMonthIndicatorModel
        {
            T item = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var column = columns.FirstOrDefault(z => z.Header.Replace(" ", string.Empty) == property.Name);

                if (!column.Equals(default))
                {
                    var value = sheet.Range[rowIndex, column.Index].Value;

                    if (!column.IsRequired && (string.IsNullOrWhiteSpace(value) || value.ToUpper() == NOT_APPLICABLE))
                    { continue; }

                    try
                    {
                        var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        var converedValue = value == null ? null : Convert.ChangeType(value, type);

                        property.SetValue(item, converedValue);
                    }
                    catch
                    { throw new CustomException($"Incorrect value in column: '{column.Header}' and row: '{rowIndex}'."); }
                }
            }

            return item;
        }

        private void AutoFitColumns(Worksheet sheet)
        {
            foreach (var column in sheet.Columns)
            {
                column.AutoFitColumns();
            }
        }

        private bool IsNumber(object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
    }
}
