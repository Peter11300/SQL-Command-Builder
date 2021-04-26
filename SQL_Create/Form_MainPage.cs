using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

namespace SQLCommandString {
    public partial class Form_MainPage : Form {
        public Form_MainPage() {
            InitializeComponent();
        }

        static void openfile(string mysheet) {
            var excelApp = new Excel.Application();
            excelApp.Visible = true;

            Excel.Workbooks books = excelApp.Workbooks;
            Excel.Workbook sheet = books.Open(mysheet);
        }

        public DataTable ImportExcel() {
            string windowFilter = "Excel files|*.xlsx";
            string windowTitle = "匯入Excel資料";

            OpenFileDialog openFileDialogFunction = new OpenFileDialog();
            openFileDialogFunction.Filter = windowFilter; //開窗搜尋副檔名
            openFileDialogFunction.Title = windowTitle; //開窗標題

            DataTable dataTable = new DataTable();

           

            if (openFileDialogFunction.ShowDialog() == DialogResult.OK) {
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
                openfile(FilePath); 


                using (OleDbConnection Connect = new OleDbConnection(connectString)) {
                    try {
                        Connect.Open();
                    } catch (Exception ex) {
                        MessageBox.Show("請關閉目前開啟的Excel檔案");
                        return dataTable;
                    }
                    //=============================================================
                    DataTable dataTable_sheetname = Connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    foreach (DataRow row in dataTable_sheetname.Rows) {
                        // Write the sheet name to the screen

                        //就是在這取得Sheet Name
                        //=============================================================

                        string queryString = "SELECT * FROM [" + row["TABLE_NAME"].ToString() + "]";
                        try {
                            using (OleDbDataAdapter dr = new OleDbDataAdapter(queryString, Connect)) {
                                dr.Fill(dataTable);
                            }
                        } catch (Exception ex) {
                            MessageBox.Show("異常訊息:" + ex.Message, "異常訊息");
                        }
                    }


                }
            }

            return dataTable;
        }

        private String getStartEndLocation(DataTable ExcelTable) {
            String location = "s0";
            for (int i = 0; i < ExcelTable.Rows.Count; i++) {
                if (i + 1 < ExcelTable.Rows.Count) {
                    if (!(ExcelTable.Rows[i]["規格書"].ToString().Equals(ExcelTable.Rows[i + 1]["規格書"].ToString()))) {
                        location += ",e" + i + ",s" + (i + 1);
                    }
                } else {
                    location += ",e" + i;
                }

            }
            return location;
        }

        private String getPrimaryKeyLocation(DataTable ExcelTable) {
            String location = "";
            for (int i = 0; i < ExcelTable.Rows.Count; i++) {
                if (ExcelTable.Rows[i]["KEY"].ToString() == "P") {
                    location += "p" + i + ",";
                }
            }
            return location;
        }

        private String getcolumnString(DataTable ExcelTable, int ExcelTableIndex) {
            String columnString = "";
            columnString = ExcelTable.Rows[ExcelTableIndex]["資料行名稱"].ToString() + " " + ExcelTable.Rows[ExcelTableIndex]["資料類型"].ToString() + " " + ExcelTable.Rows[ExcelTableIndex]["允許Null"].ToString() + ", \r\n";

            return columnString;
        }

        private String getCreateString(DataTable ExcelTable, String _startEndLocation, String _primaryKeyLocation) {
            String[] startEndLocation = _startEndLocation.Split(',');
            String[] primaryKeyLocation = _primaryKeyLocation.Split(',');
            int startEndLocationIndex = 0;
            int primaryKeyLocationIndex = 0;
            String commandString = "";
            String temp_commandString = "";
            String primaryString = "\r\nPRIMARY KEY(";
            Boolean primaryCheck = true;

            for (int i = 0; i < ExcelTable.Rows.Count; i++) {
                setDegreeOfCompletionText(i, ExcelTable.Rows.Count);
                if (ExcelTable.Rows[i]["修改註記V"].ToString() == "AT") {
                    if ("p" + i == primaryKeyLocation[primaryKeyLocationIndex]) {
                        if (primaryCheck == false) {
                            primaryString = primaryString + "," + ExcelTable.Rows[i]["資料行名稱"].ToString();
                            primaryKeyLocationIndex++;
                        } else {
                            primaryString = primaryString + ExcelTable.Rows[i]["資料行名稱"].ToString();
                            primaryCheck = false;
                            primaryKeyLocationIndex++;
                        }
                    }

                    if ("e" + i == startEndLocation[startEndLocationIndex]) {

                        temp_commandString += getcolumnString(ExcelTable, i);

                        temp_commandString = temp_commandString + primaryString + ") \r\n";
                        temp_commandString += "); \r\n\r\n";

                        commandString += temp_commandString;
                        primaryString = "\r\nPRIMARY KEY(";
                        temp_commandString = "";
                        startEndLocationIndex++;
                        primaryCheck = true;
                    } else {
                        if ("s" + i == startEndLocation[startEndLocationIndex]) {
                            String createTableString = "CREATE TABLE ";
                            createTableString = createTableString + ExcelTable.Rows[i]["規格書"].ToString() + "\r\n( \r\n";
                            temp_commandString += createTableString;
                            startEndLocationIndex++;
                        }

                        temp_commandString += getcolumnString(ExcelTable, i);
                    }
                }

            }

            return commandString;
        }

        private String getAlterString(DataTable ExcelTable) {
            String commandString = "";
            String commandAddString = "";
            String commandDropString = "";
            String commandModifyString = "";
            String storedprocedureString = "";

            for (int i = 0; i < ExcelTable.Rows.Count; i++) {
                setDegreeOfCompletionText(i, ExcelTable.Rows.Count);
                switch (ExcelTable.Rows[i]["修改註記V"].ToString()) {
                    case "A":
                        commandAddString = commandAddString + getAlterAddString(ExcelTable.Rows[i]);
                        break;
                    case "D":
                        commandDropString = commandDropString + getAlterDropString(ExcelTable.Rows[i]);
                        break;
                    case "C":
                        storedprocedureString = storedprocedureString + getAlterChangeString(ExcelTable.Rows[i]);
                        break;
                    case "M":
                        commandModifyString = commandModifyString + getAlterModifyString(ExcelTable.Rows[i]);
                        break;

                }
            }

            commandString = commandString + commandAddString + commandDropString + commandModifyString;

            if (storedprocedureString != "") {
                commandString = commandString + "\r\n==========================\r\n" + "======== 以下請逐行執行 ========\r\n" + "==========================\r\n";
                commandString = commandString + storedprocedureString;
            }

            return commandString;
        }

        private String getAlterAddString(DataRow dataRow) {
            String commandString = "";

            commandString = commandString + "ALTER TABLE " + dataRow["規格書"].ToString() + " ADD " + dataRow["資料行名稱"].ToString() + " " + dataRow[4].ToString() + " ;\r\n";

            return commandString;
        }

        private String getAlterDropString(DataRow dataRow) {
            String commandString = "";

            commandString = commandString + "ALTER TABLE " + dataRow["規格書"].ToString() + " DROP COLUMN " + dataRow["資料行名稱"].ToString() + " ;\r\n";

            return commandString;
        }

        private String getAlterChangeString(DataRow dataRow) {
            String commandString = "";

            commandString = commandString + "\r\n sp_rename '" + dataRow["規格書"].ToString() + "." + dataRow["原欄位名稱"].ToString() + "', '" + dataRow["資料行名稱"].ToString() + "', '" + "COLUMN' ;\r\n";

            return commandString;
        }

        private String getAlterModifyString(DataRow dataRow) {
            String commandString = "";

            commandString = commandString + "ALTER TABLE " + dataRow["規格書"].ToString() + " ALTER COLUMN " + dataRow["資料行名稱"].ToString() + " " + dataRow["資料類型"].ToString() + " ;\r\n";

            return commandString;
        }

        private void setSQL_CommandStringText(String CommandString) {

            Clipboard.SetData(DataFormats.Text, CommandString);

            SQL_CommandString.Text = CommandString;
        }

        private void button_CreateTable_Click(object sender, EventArgs e) {
            resetDegreeOfCompletionText();
            DataTable TableValue = ImportExcel();
            String startEndLocation = getStartEndLocation(TableValue);
            String primaryKeyLocation = getPrimaryKeyLocation(TableValue);
            String commandString = getCreateString(TableValue, startEndLocation, primaryKeyLocation);

            setSQL_CommandStringText(commandString);
        }

        private void button_Alter_Click(object sender, EventArgs e) {
            resetDegreeOfCompletionText();
            DataTable TableValue = ImportExcel();
            String commandString = getAlterString(TableValue);

            setSQL_CommandStringText(commandString);
        }

        private void setDegreeOfCompletionText(int current, int total) {
            if ((total - 1) != 0) {
                DegreeOfCompletion.Text = "完成率：" + (current * 100 / (total - 1)) + "％";
            }
        }

        private void resetDegreeOfCompletionText() {
            DegreeOfCompletion.Text = "完成率：" + 0 + "％";
        }

        private void button_Cooy_Click(object sender, EventArgs e) {

            SQL_CommandString.SelectAll();
            SQL_CommandString.Copy();
        }
    }
}

