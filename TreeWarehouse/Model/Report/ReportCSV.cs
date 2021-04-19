using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreeWarehouse.Model.Report
{
    /// <summary>
    /// Object for serializing CSV Report.
    /// </summary>
    public class ReportCSV
    {
        string _path;
        string _vendorCode;
        string _name;
        uint _stockNum;

        public ReportCSV(string path, string vendorCode, string name, uint stockNum) {
            Path = path;
            VendorCode = vendorCode;
            Name = name;
            StockNum = stockNum;
        }

        public ReportCSV(string path, Product product) {
            Path = path;
            VendorCode = product.VendorCode;
            Name = product.Name;
            StockNum = product.StockNum;
        }

        [Name("путь")]
        public string Path { get => _path; set => _path = value; }
        [Name("артикул")]
        public string VendorCode { get => _vendorCode; set => _vendorCode = value; }
        [Name("название товара")]
        public string Name { get => _name; set => _name = value; }
        [Name("количество на складе")]
        public uint StockNum { get => _stockNum; set => _stockNum = value; }
    }
}
