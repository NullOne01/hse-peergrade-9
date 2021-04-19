using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TreeWarehouse.Model;
using TreeWarehouse.Model.Report;

namespace TreeWarehouse.ViewModel.Utilities
{
    public static class WarehouseCSVUtility
    {
        /// <summary>
        /// Generate and save CSV report into path. Exceptions are possible.
        /// </summary>
        /// <param name="warehouse"> Warehouse to get report from. </param>
        /// <param name="path"> Destination of CSV report. </param>
        public static void SaveReportCSV(this Warehouse warehouse, string path) {
            List<ReportCSV> reportList = warehouse.GetReportList();

            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
                csv.WriteRecords(reportList);
            }
        }
    }
}
