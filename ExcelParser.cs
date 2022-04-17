using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Net;

namespace ParsingApp
{

    public struct Data
    {
        public string Indicator { get; set; }
        public string IndicatorWithUBI { get; set; }
        public string NameOfUBI { get; set; }
        public string Description { get; set; }
        public string ThreatSource { get; set; }
        public string ImpactObject { get; set; }
        public string ConfederationViolation { get; set; }
        public string IntegrityViolation { get;  set; }
        public string AccessViolation { get; set; }
        public void SetConfederationViolation(string value)
        {
            ConfederationViolation = ToString(value);
        }
        public void SetIntegrityViolation(string value)
        {
            IntegrityViolation = ToString(value);
        }
        public void SetAccessViolation(string value)
        {
            AccessViolation = ToString(value);
        }
        public string InclusionDate { get; set; }
        public string ChangeDate { get; set; }
        private string ToString(string value)
        {
            return value == "1" ? "Да" : "Нет";
        }
    }

    public class ExcelParser
    {
        private const string FileName = "thrlist.xlsx";
        private const string SiteURL = @"https://bdu.fstec.ru/files/documents/thrlist.xlsx";

        public enum LoadStatus
        {
            OK,
            error
        }
        public struct LoadInfo
        {
            public LoadStatus status;
            public string error;
        }

        public ExcelParser()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public Data[] FromExcel()
        {
            try
            {
                var fi = new FileInfo(FileName);
                using (var package = new ExcelPackage(fi))
                {
                    using (ExcelWorksheet workSheet = package.Workbook.Worksheets[0])
                    {
                        //инициализация массива с указанием кол-ва сторк
                        
                        //счётчик для массива 
                        int countData = 0;
                        //выгрузка всех значение
                        var list1 = workSheet.Cells[workSheet.Dimension.Start.Row + 2, 1, workSheet.Dimension.End.Row, 10].ToList();
                        //другой счётчик для создания объекта, инициализации и заполнения
                        Data[] datas = new Data[list1.Count/10];
                        for (int i = 0; i < list1.Count; i+=10)
                        {
                            datas[countData] = new Data();
                            datas[countData].Indicator = list1[i].Text;
                            datas[countData].IndicatorWithUBI = "УБИ." + list1[i].Text;
                            datas[countData].NameOfUBI = list1[i + 1].Text;
                            datas[countData].Description = list1[i + 2].Text;
                            datas[countData].ThreatSource = list1[i + 3].Text;
                            datas[countData].ImpactObject = list1[i + 4].Text;
                            datas[countData].SetConfederationViolation(list1[i + 5].Text);
                            datas[countData].SetIntegrityViolation(list1[i + 6].Text);
                            datas[countData].SetAccessViolation(list1[i + 7].Text);
                            datas[countData].InclusionDate = list1[i + 8].Text;
                            datas[countData].ChangeDate = list1[i + 9].Text; ;
                            countData++;
                        }
                        return datas;
                        
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public bool CheckFileExist()
        {
            var fi = new FileInfo(FileName);
            return fi.Exists;
        }
        public LoadInfo LoadFromSite()
        {
            LoadInfo info = new LoadInfo();
            try
            {
                var client = new WebClient();
                client.DownloadFile(SiteURL, FileName);
                info.status = LoadStatus.OK;
            }
            catch(Exception ex)
            {
               info.error = ex.Message;
               info.status = LoadStatus.error;
            }
            return info;
        }


    }
}
