using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SQLCommandString
{
    public static class ExcelManager
    {
        public static DataTable ImportExcel(string path)
        {
            DataTable dataTable = new DataTable();

            //定義OleDb======================================================
            //1.檔案位置
            string FilePath = path;

            //2.提供者名稱  Microsoft.Jet.OLEDB.4.0適用於2003以前版本，Microsoft.ACE.OLEDB.12.0 適用於2007以後的版本處理 xlsx 檔案
            string ProviderName = "Microsoft.ACE.OLEDB.12.0;";

            //3.Excel版本，Excel 8.0 針對Excel2000及以上版本，Excel5.0 針對Excel97。
            string ExtendedString = "'Excel 8.0;";

            //4.第一行是否為標題(;結尾區隔)
            string HDR = "No;";

            //5.IMEX=1 通知驅動程序始終將「互混」數據列作為文本讀取(;結尾區隔,'文字結尾)
            string IMEX = "0';";

            //=============================================================
            //連線字串
            string connectString =
                    "Data Source=" + FilePath + ";" +
                    "Provider=" + ProviderName +
                    "Extended Properties=" + ExtendedString +
                    "HDR=" + HDR +
                    "IMEX=" + IMEX;
            //=============================================================

            //開啟Excel檔案
            Process p = Process.Start(FilePath);
            p.WaitForInputIdle();
            p.WaitForExit();

            using (OleDbConnection Connect = new OleDbConnection(connectString))
            {
                try
                {
                    Connect.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("請關閉目前開啟的Excel檔案");
                    return dataTable;
                }
                //=============================================================
                DataTable dataTable_sheetname = Connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (DataRow row in dataTable_sheetname.Rows)
                {
                    // Write the sheet name to the screen

                    //就是在這取得Sheet Name
                    //=============================================================

                    string queryString = "SELECT * FROM [" + row["TABLE_NAME"].ToString() + "]";
                    try
                    {
                        using (OleDbDataAdapter dr = new OleDbDataAdapter(queryString, Connect))
                        {
                            dr.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("異常訊息:" + ex.Message, "異常訊息");
                    }
                }


            }

            return dataTable;
        }

        public static DataTable OpenExcelFile()
        {
            string windowFilter = "Excel files|*.xlsx";
            string windowTitle = "匯入Excel資料";

            OpenFileDialog openFileDialogFunction = new OpenFileDialog();
            openFileDialogFunction.Filter = windowFilter; //開窗搜尋副檔名
            openFileDialogFunction.Title = windowTitle; //開窗標題

            DataTable dataTable = new DataTable();



            if (openFileDialogFunction.ShowDialog() == DialogResult.OK)
            {
                //定義OleDb======================================================
                //1.檔案位置
                string FilePath = openFileDialogFunction.FileName;

                //2.提供者名稱  Microsoft.Jet.OLEDB.4.0適用於2003以前版本，Microsoft.ACE.OLEDB.12.0 適用於2007以後的版本處理 xlsx 檔案
                string ProviderName = "Microsoft.ACE.OLEDB.12.0;";

                //3.Excel版本，Excel 8.0 針對Excel2000及以上版本，Excel5.0 針對Excel97。
                string ExtendedString = "'Excel 8.0;";

                //4.第一行是否為標題(;結尾區隔)
                string HDR = "No;";

                //5.IMEX=1 通知驅動程序始終將「互混」數據列作為文本讀取(;結尾區隔,'文字結尾)
                string IMEX = "0';";

                //=============================================================
                //連線字串
                string connectString =
                        "Data Source=" + FilePath + ";" +
                        "Provider=" + ProviderName +
                        "Extended Properties=" + ExtendedString +
                        "HDR=" + HDR +
                        "IMEX=" + IMEX;
                //=============================================================

                //開啟Excel檔案
                Process p = Process.Start(FilePath);
                p.WaitForInputIdle();
                p.WaitForExit();

                using (OleDbConnection Connect = new OleDbConnection(connectString))
                {
                    try
                    {
                        Connect.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("請關閉目前開啟的Excel檔案");
                        return dataTable;
                    }
                    //=============================================================
                    DataTable dataTable_sheetname = Connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    foreach (DataRow row in dataTable_sheetname.Rows)
                    {
                        // Write the sheet name to the screen

                        //就是在這取得Sheet Name
                        //=============================================================

                        string queryString = "SELECT * FROM [" + row["TABLE_NAME"].ToString() + "]";
                        try
                        {
                            using (OleDbDataAdapter dr = new OleDbDataAdapter(queryString, Connect))
                            {
                                dr.Fill(dataTable);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("異常訊息:" + ex.Message, "異常訊息");
                        }
                    }


                }
            }
            return dataTable;
        }

        public static string CreateExcelFile()
        {
            var rand = new Random();
            string fileName = "SQLCommandString" + rand.Next(1, 100000) + ".xlsx";
            FileInfo fi = new FileInfo(fileName);
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();
            if (xlapp == null)
            {
                MessageBox.Show("請安裝office!!");
            }
            xlapp.Visible = false;//不顯示excel程式
            Excel.Workbook wb = xlapp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[1];
            ws.Name = "Data";
            ws.Cells[1, 1] = "規格書";
            ws.Cells[1, 2] = "KEY";
            ws.Cells[1, 3] = "資料行名稱";
            ws.Cells[1, 4] = "資料行中文名稱";
            ws.Cells[1, 5] = "資料類型";
            ws.Cells[1, 6] = "允許Null";
            ws.Cells[1, 7] = "Constraint";
            ws.Cells[1, 8] = "備註";
            ws.Cells[1, 9] = "修改(新增)日";
            ws.Cells[1, 10] = "修改(新增)者";
            ws.Cells[1, 11] = "規格書名稱";
            ws.Cells[1, 12] = "修改註記V";
            ws.Cells[1, 13] = "原欄位名稱";

            if (ws == null)
            {
                MessageBox.Show("建立sheet失敗");
            }

            string fullPath = @fi.DirectoryName + "\\" + fileName;

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            wb.SaveAs(fullPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wb.Close(false, Type.Missing, Type.Missing);
            xlapp.Workbooks.Close();
            xlapp.Quit();
            //刪除 Windows工作管理員中的Excel.exe process，  
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlapp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);

            return fullPath;
        }
    }
}