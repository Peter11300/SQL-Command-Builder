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
using System.Diagnostics;

namespace SQLCommandString {
    public partial class Form_MainPage : Form {
        public Form_MainPage() {
            InitializeComponent();
        }

        //static void openfile(string mysheet) {
        //    var excelApp = new Excel.Application();
        //    excelApp.Visible = true;

        //    Excel.Workbooks books = excelApp.Workbooks;
        //    Excel.Workbook sheet = books.Open(mysheet);
        //}

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
                Process p = Process.Start(FilePath);
                p.WaitForInputIdle();
                p.WaitForExit();

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

        private string GetStartEndLocation(DataTable excelTable) {
            string location = "";
            bool firstCheck = true;
            for (int i = 0; i < excelTable.Rows.Count; i++) {
                if (excelTable.Rows[i]["修改註記V"].ToString() == "AT") {
                    if (firstCheck) {
                        location += "s" + i;
                        firstCheck = false;
                    } else {
                        if (i + 1 < excelTable.Rows.Count) {
                            if (!(excelTable.Rows[i]["規格書"].ToString().Equals(excelTable.Rows[i + 1]["規格書"].ToString()))) {
                                location += ",e" + i + ",s" + (i + 1);
                            }
                        } else {
                            location += ",e" + i;
                        }
                    }
                }
            }
            return location;
        }

        private string GetPrimaryKeyLocation(DataTable excelTable) {
            string location = "";
            for (int i = 0; i < excelTable.Rows.Count; i++) {
                if (excelTable.Rows[i]["KEY"].ToString() == "P" && excelTable.Rows[i]["修改註記V"].ToString() == "AT") {
                    location += "p" + i + ",";
                }
            }
            return location;
        }

        private string GetColumnString(DataTable excelTable, int excelTableIndex) {
            string columnString = "";
            if (excelTable.Rows[excelTableIndex]["備註"].ToString().ToUpper() == "IDENTIFY") {
                columnString = excelTable.Rows[excelTableIndex]["資料行名稱"].ToString() + " " + excelTable.Rows[excelTableIndex]["資料類型"].ToString() + " " + "IDENTITY(1, 1)" + " " + excelTable.Rows[excelTableIndex]["允許Null"].ToString() + ", \r\n";
            } else {
                columnString = excelTable.Rows[excelTableIndex]["資料行名稱"].ToString() + " " + excelTable.Rows[excelTableIndex]["資料類型"].ToString() + " " + excelTable.Rows[excelTableIndex]["允許Null"].ToString() + ", \r\n";
            }
            return columnString;
        }

        private string GetCreateString(DataTable excelTable, string _startEndLocation, string _primaryKeyLocation) {
            string[] startEndLocation = _startEndLocation.Split(',');
            string[] primaryKeyLocation = _primaryKeyLocation.Split(',');
            int startEndLocationIndex = 0;
            int primaryKeyLocationIndex = 0;
            StringBuilder commandString = new StringBuilder("");
            StringBuilder temp_commandString = new StringBuilder("");
            string primaryString = "\r\nPRIMARY KEY(";
            bool primaryCheck = true;
            Action<int> action = SetDegreeOfCompletionText;

            for (int i = 0; i < excelTable.Rows.Count; i++) {
                //setDegreeOfCompletionText(i, ExcelTable.Rows.Count);
                action.Invoke(((i + 1) * 100 / (excelTable.Rows.Count)));
                if (excelTable.Rows[i]["修改註記V"].ToString() == "AT") {
                    if ("p" + i == primaryKeyLocation[primaryKeyLocationIndex]) {
                        if (primaryCheck == false) {
                            primaryString = primaryString + "," + excelTable.Rows[i]["資料行名稱"].ToString();
                            primaryKeyLocationIndex++;
                        } else {
                            primaryString = primaryString + excelTable.Rows[i]["資料行名稱"].ToString();
                            primaryCheck = false;
                            primaryKeyLocationIndex++;
                        }
                    }

                    if ("e" + i == startEndLocation[startEndLocationIndex]) {

                        temp_commandString.Append(GetColumnString(excelTable, i));

                        temp_commandString.Append(primaryString + ") \r\n");
                        temp_commandString.Append("); \r\n\r\n");

                        commandString.Append(temp_commandString);
                        primaryString = "\r\nPRIMARY KEY(";
                        temp_commandString.Clear();
                        startEndLocationIndex++;
                        primaryCheck = true;
                    } else {
                        if ("s" + i == startEndLocation[startEndLocationIndex]) {
                            string createTableString = "CREATE TABLE ";
                            createTableString = createTableString + excelTable.Rows[i]["規格書"].ToString() + "\r\n( \r\n";
                            temp_commandString.Append(createTableString);
                            startEndLocationIndex++;
                        }

                        temp_commandString.Append(GetColumnString(excelTable, i));
                    }
                }

            }

            return commandString.ToString();
        }

        private string GetAlterString(DataTable excelTable) {
            StringBuilder commandString = new StringBuilder("");
            StringBuilder commandAddString = new StringBuilder("");
            StringBuilder commandDropString = new StringBuilder("");
            StringBuilder commandModifyString = new StringBuilder("");
            StringBuilder storedprocedureString = new StringBuilder("");
            Action<int> action = SetDegreeOfCompletionText;

            for (int i = 0; i < excelTable.Rows.Count; i++) {
                //setDegreeOfCompletionText(i, ExcelTable.Rows.Count);
                action.Invoke(((i + 1) * 100 / (excelTable.Rows.Count)));
                switch (excelTable.Rows[i]["修改註記V"].ToString()) {
                    case "A":
                        commandAddString.Append(GetAlterAddString(excelTable.Rows[i]));
                        break;
                    case "D":
                        commandDropString.Append(GetAlterDropString(excelTable.Rows[i]));
                        break;
                    case "C":
                        storedprocedureString.Append(storedprocedureString + GetAlterChangeString(excelTable.Rows[i]));
                        break;
                    case "M":
                        commandModifyString.Append(GetAlterModifyString(excelTable.Rows[i]));
                        break;
                }
            }

            commandString.Append(commandAddString.ToString() + commandDropString.ToString() + commandModifyString.ToString());

            if (string.IsNullOrEmpty(storedprocedureString.ToString())) {
                commandString.Append("\r\n==========================\r\n" + "======== 以下請逐行執行 ========\r\n" + "==========================\r\n");
                commandString.Append(storedprocedureString);
            }

            return commandString.ToString();
        }

        private string GetAlterAddString(DataRow dataRow) {
            string commandString = "";

            if (dataRow["備註"].ToString().ToUpper() == "IDENTIFY") {
                commandString = commandString + "ALTER TABLE " + dataRow["規格書"].ToString() + " ADD " + dataRow["資料行名稱"].ToString() + " " + dataRow["資料類型"].ToString() + " " + "IDENTITY(1, 1)" + " ;\r\n";
            } else {
                commandString = commandString + "ALTER TABLE " + dataRow["規格書"].ToString() + " ADD " + dataRow["資料行名稱"].ToString() + " " + dataRow["資料類型"].ToString() + " ;\r\n";
            }

            return commandString;
        }

        private string GetAlterDropString(DataRow dataRow) {
            string commandString = "";

            commandString = commandString + "ALTER TABLE " + dataRow["規格書"].ToString() + " DROP COLUMN " + dataRow["資料行名稱"].ToString() + " ;\r\n";

            return commandString;
        }

        private string GetAlterChangeString(DataRow dataRow) {
            string commandString = "";

            commandString = commandString + "\r\n sp_rename '" + dataRow["規格書"].ToString() + "." + dataRow["原欄位名稱"].ToString() + "', '" + dataRow["資料行名稱"].ToString() + "', '" + "COLUMN' ;\r\n";

            return commandString;
        }

        private string GetAlterModifyString(DataRow dataRow) {
            string commandString = "";

            commandString = commandString + "ALTER TABLE " + dataRow["規格書"].ToString() + " ALTER COLUMN " + dataRow["資料行名稱"].ToString() + " " + dataRow["資料類型"].ToString() + " ;\r\n";

            return commandString;
        }

        private string GetFIELD_TYPE(string inputString) {
            return (inputString.Contains("(")) ? inputString.Substring(0, inputString.IndexOf("(")) : inputString;
        }

        private string GetFIELD_LENGTH(string inputString) {
            string variableType = (inputString.Contains("(")) ? inputString.Substring(0, inputString.IndexOf("(")) : inputString;

            switch (variableType) {
                case "int":
                    return "4";
                case "bigint":
                    return "8";
                case "smallint":
                    return "2";
                case "tinyint":
                    return "1";
                case "bit":
                    return "1";
                case "datetime":
                    return "8";
                case "decimal":
                    return "17";
                case "date":
                    return "3";
                case "float":
                    return "8";
                case "image":
                    return "16";
                case "ntext":
                    return "16";
                case "text":
                    return "16";
                case "char":
                    return "1";
                case "varbinary":
                    if (inputString.Substring(inputString.IndexOf("(") + 1, inputString.Length - inputString.IndexOf("(") - 2) == "MAX")
                        return "2147483647";
                    else
                        return inputString.Substring(inputString.IndexOf("(") + 1, inputString.Length - inputString.IndexOf("(") - 2);
                case "varchar":
                    if (inputString.Substring(inputString.IndexOf("(") + 1, inputString.Length - inputString.IndexOf("(") - 2) == "MAX")
                        return "2147483647";
                    else
                        return inputString.Substring(inputString.IndexOf("(") + 1, inputString.Length - inputString.IndexOf("(") - 2);
                case "nvarchar":
                    if (inputString.Substring(inputString.IndexOf("(") + 1, inputString.Length - inputString.IndexOf("(") - 2) == "MAX")
                        return "2147483647";
                    else
                        return inputString.Substring(inputString.IndexOf("(") + 1, inputString.Length - inputString.IndexOf("(") - 2);
                default:
                    return "";


            }
        }

        private string GetIS_KEY(string inputString) {
            return (inputString == "P") ? "Y" : "N";
        }

        private string GetIS_NULL(string inputString) {
            return (inputString == "NULL") ? "N" : "Y";
        }

        private string GetInsertString(DataTable excelTable, int excelTableIndex, int tableIndex, string startEnd) {
            StringBuilder insertString = new StringBuilder("");
            //string insertString = "";
            if (startEnd == "Start") {
                insertString.Append("('" + (excelTable.Rows[excelTableIndex]["規格書"].ToString() + "', '"
                + "*" + "', "
                + "0" + ", "
                + "NULL" + ", '"
                + "N" + "', "
                + "NULL" + ", '"
                + excelTable.Rows[excelTableIndex]["規格書名稱"].ToString() + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, "
                + "NULL" + ", "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, NULL, NULL),\r\n"
                ));
            } else if (startEnd == "End") {
                insertString.Append("('" + (excelTable.Rows[excelTableIndex]["規格書"].ToString() + "', '"
                + excelTable.Rows[excelTableIndex]["資料行名稱"].ToString() + "', "
                + tableIndex.ToString() + ", '"
                + GetFIELD_TYPE(excelTable.Rows[excelTableIndex]["資料類型"].ToString()) + "', '"
                + GetIS_KEY(excelTable.Rows[excelTableIndex]["KEY"].ToString()) + "', "
                + GetFIELD_LENGTH(excelTable.Rows[excelTableIndex]["資料類型"].ToString()) + ", '"
                + excelTable.Rows[excelTableIndex]["資料行中文名稱"].ToString() + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, '"
                + GetIS_NULL(excelTable.Rows[excelTableIndex]["允許Null"].ToString()) + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, NULL, NULL)\r\n"
                ));
            } else {
                insertString.Append("('" + (excelTable.Rows[excelTableIndex]["規格書"].ToString() + "', '"
                + excelTable.Rows[excelTableIndex]["資料行名稱"].ToString() + "', "
                + tableIndex.ToString() + ", '"
                + GetFIELD_TYPE(excelTable.Rows[excelTableIndex]["資料類型"].ToString()) + "', '"
                + GetIS_KEY(excelTable.Rows[excelTableIndex]["KEY"].ToString()) + "', "
                + GetFIELD_LENGTH(excelTable.Rows[excelTableIndex]["資料類型"].ToString()) + ", '"
                + excelTable.Rows[excelTableIndex]["資料行中文名稱"].ToString() + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, '"
                + GetIS_NULL(excelTable.Rows[excelTableIndex]["允許Null"].ToString()) + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, NULL, NULL),\r\n"
                ));
            }


            return insertString.ToString();
        }

        private string GetDectionaryString(DataTable excelTable, string _startEndLocation, string _primaryKeyLocation) {
            StringBuilder InsetString = new StringBuilder("INSERT INTO COLDEF(TABLE_NAME,FIELD_NAME,SEQ,FIELD_TYPE,IS_KEY,FIELD_LENGTH,CAPTION,EDITMASK,NEEDBOX,CANREPORT,EXT_MENUID,FIELD_SCALE,DD_NAME,DEFAULT_VALUE,CHECK_NULL,QUERYMODE,CAPTION1,CAPTION2,CAPTION3,CAPTION4,CAPTION5,CAPTION6,CAPTION7,CAPTION8) VALUES \r\n");
            string[] startEndLocation = _startEndLocation.Split(',');
            int startEndLocationIndex = 0;
            int index = 1;
            Action<int> action = SetDegreeOfCompletionText;

            for (int i = 0; i < excelTable.Rows.Count; i++) {
                //setDegreeOfCompletionText(i, ExcelTable.Rows.Count);
                action.Invoke(((i + 1) * 100 / (excelTable.Rows.Count)));
                if ("e" + i == startEndLocation[startEndLocation.Length - 1]) {
                    InsetString.Append(GetInsertString(excelTable, i, index, "End"));
                } else if ("e" + i == startEndLocation[startEndLocationIndex]) {
                    InsetString.Append(GetInsertString(excelTable, i, index, ""));
                    startEndLocationIndex++;
                    index = 1;
                } else {
                    if ("s" + i == startEndLocation[startEndLocationIndex]) {
                        InsetString.Append(GetInsertString(excelTable, i, index, "Start"));
                        startEndLocationIndex++;
                    }
                    InsetString.Append(GetInsertString(excelTable, i, index, ""));
                    index++;
                }
            }

            return InsetString.ToString();
        }

        private void SetSQLCommandStringText(string commandString) {

            Clipboard.SetData(DataFormats.Text, commandString);

            SQL_CommandString.Text = commandString;
        }

        private void SetDegreeOfCompletionText(int current) {
            DegreeOfCompletion.Text = "完成率：" + current + "％";
            progressBar1.Value = current;
            System.Windows.Forms.Application.DoEvents();
        }

        private void ResetDegreeOfCompletionText() {
            DegreeOfCompletion.Text = "完成率：" + 0 + "％";
            progressBar1.Value = 0;
        }

        private void button_Cooy_Click(object sender, EventArgs e) {
            SQL_CommandString.SelectAll();
            SQL_CommandString.Copy();
        }

        private void button_CreateTable_Click(object sender, EventArgs e) {
            ResetDegreeOfCompletionText();
            DataTable TableValue = ImportExcel();
            string startEndLocation = GetStartEndLocation(TableValue);
            string primaryKeyLocation = GetPrimaryKeyLocation(TableValue);
            string commandString = GetCreateString(TableValue, startEndLocation, primaryKeyLocation);

            SetSQLCommandStringText(commandString);
        }

        private void button_Alter_Click(object sender, EventArgs e) {
            ResetDegreeOfCompletionText();
            DataTable TableValue = ImportExcel();
            string commandString = GetAlterString(TableValue);

            SetSQLCommandStringText(commandString);
        }

        private void button_Dectionary_Click(object sender, EventArgs e) {
            ResetDegreeOfCompletionText();
            DataTable TableValue = ImportExcel();
            string startEndLocation = GetStartEndLocation(TableValue);
            string primaryKeyLocation = GetPrimaryKeyLocation(TableValue);
            string commandString = GetDectionaryString(TableValue, startEndLocation, primaryKeyLocation);

            SetSQLCommandStringText(commandString);
        }
    }
}

