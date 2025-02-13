﻿using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;

#pragma warning disable CA1416 // Validate platform compatibility

namespace LiberacionProductoWeb.Helpers
{
    public static class BinaryExcelReader
    {
        /// <summary>
        /// Returns datatable object from the excel file with values retrieved as strings.
        /// </summary>
        /// <param name="path">Relative or absolute path to the excel file.</param>
        /// <param name="sheetName">The name of the excel sheet that you want to convert to a datatable object.</param>
        /// <param name="options">Settings you might want to change.</param>
        public static DataTable ToDataTable(string path, string sheetName, BinaryExcelReaderOptions options = null)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            options = LoadOptions(options);

            var hdr = options.HeaderExists ? "YES" : "NO";
            var connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"{path}\";Extended Properties = \"Excel 12.0;characterset=65001;HDR={hdr};IMEX=1\"";

            return ReadData(connectionString, sheetName, options);
        }

        private static BinaryExcelReaderOptions LoadOptions(BinaryExcelReaderOptions options)
        {
            if (options == null)
            {
                return new BinaryExcelReaderOptions();
            }

            return options;
        }

        private static DataTable ReadData(string connectionString, string sheetName, BinaryExcelReaderOptions options)
        {
            var result = new DataTable();

            using OleDbConnection connection = new(connectionString); connection.Open();

            string query = $"select * from [{sheetName}$]";

            using OleDbCommand command = new(query, connection);

            using OleDbDataReader reader = command.ExecuteReader();

            LoadHeader(result, reader, options);

            LoadRows(result, reader, options);

            return result;
        }

        private static void LoadHeader(DataTable result, OleDbDataReader reader, BinaryExcelReaderOptions options)
        {
            var columns = reader.GetSchemaTable().Rows;

            for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
            {
                AddColumn(result, columns, columnIndex);

                if (options.MaxColumns != null && columnIndex + 1 == options.MaxColumns) break;
            }

            static void AddColumn(DataTable result, DataRowCollection columns, int columnIndex)
            {
                var columnName = columns[columnIndex]["ColumnName"].ToString();
                var column = new DataColumn(columnName, typeof(string));
                result.Columns.Add(column);
            }
        }

        private static void LoadRows(DataTable datatable, OleDbDataReader reader, BinaryExcelReaderOptions options)
        {
            var rowIndex = 1; // 0. row was already consumed by the driver into header row.
            while (reader.Read())
            {
                // If excel does not contains header, we just insert the rows.
                if (options.HeaderExists == false)
                {
                    DataRow row = CreateRow(datatable, reader);
                    AddRow(datatable, row, options);
                    continue;
                }

                // If excel does contains header, we take header row index option into account.
                if (rowIndex < options.HeaderRowIndex)
                {
                    rowIndex++;
                    continue;
                }
                if (options.HeaderRowIndex != 0 && rowIndex == options.HeaderRowIndex)
                {
                    UpdateColumnNames(datatable, reader);
                }
                else
                {
                    DataRow row = CreateRow(datatable, reader);
                    AddRow(datatable, row, options);
                }
                rowIndex++;
            }

            static void UpdateColumnNames(DataTable datatable, OleDbDataReader reader)
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    if (reader == null || string.IsNullOrEmpty(reader[i].ToString())) continue;
                    datatable.Columns[i].ColumnName = reader[i].ToString();
                }
            }

            static DataRow CreateRow(DataTable datatable, OleDbDataReader reader)
            {
                var row = datatable.NewRow();
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    row[i] = reader[i].ToString();
                }

                return row;
            }

            static void AddRow(DataTable datatable, DataRow row, BinaryExcelReaderOptions options)
            {
                if (options.RemoveEmptyRows)
                {
                    var rowContainsData =
                        row.ItemArray.Any(value => value != DBNull.Value && !string.IsNullOrEmpty((string)value));

                    if (rowContainsData)
                    {
                        datatable.Rows.Add(row);
                    }
                }
                else
                {
                    datatable.Rows.Add(row);
                }
            }
        }
    }
}

#pragma warning restore CA1416 // Validate platform compatibility
