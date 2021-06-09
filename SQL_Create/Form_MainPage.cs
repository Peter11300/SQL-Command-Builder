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

        private String getStartEndLocation(DataTable ExcelTable) {
            String location = "";
            Boolean firstCheck = true;
            for (int i = 0; i < ExcelTable.Rows.Count; i++) {
                if (ExcelTable.Rows[i]["修改註記V"].ToString() == "AT") {
                    if (firstCheck) {
                        location += "s" + i;
                        firstCheck = false;
                    } else {
                        if (i + 1 < ExcelTable.Rows.Count) {
                            if (!(ExcelTable.Rows[i]["規格書"].ToString().Equals(ExcelTable.Rows[i + 1]["規格書"].ToString()))) {
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

        private String getPrimaryKeyLocation(DataTable ExcelTable) {
            String location = "";
            for (int i = 0; i < ExcelTable.Rows.Count; i++) {
                if (ExcelTable.Rows[i]["KEY"].ToString() == "P" && ExcelTable.Rows[i]["修改註記V"].ToString() == "AT") {
                    location += "p" + i + ",";
                }
            }
            return location;
        }

        private String getcolumnString(DataTable ExcelTable, int ExcelTableIndex) {
            String columnString = "";
            if (ExcelTable.Rows[ExcelTableIndex]["備註"].ToString().ToUpper() == "IDENTIFY") {
                columnString = ExcelTable.Rows[ExcelTableIndex]["資料行名稱"].ToString() + " " + ExcelTable.Rows[ExcelTableIndex]["資料類型"].ToString() + " " + "IDENTITY(1, 1)" + " " + ExcelTable.Rows[ExcelTableIndex]["允許Null"].ToString() + ", \r\n";
            } else {
                columnString = ExcelTable.Rows[ExcelTableIndex]["資料行名稱"].ToString() + " " + ExcelTable.Rows[ExcelTableIndex]["資料類型"].ToString() + " " + ExcelTable.Rows[ExcelTableIndex]["允許Null"].ToString() + ", \r\n";
            }
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
            Action<int> action = setDegreeOfCompletionText;

            for (int i = 0; i < ExcelTable.Rows.Count; i++) {
                //setDegreeOfCompletionText(i, ExcelTable.Rows.Count);
                action.Invoke(((i + 1) * 100 / (ExcelTable.Rows.Count)));
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
            Action<int> action = setDegreeOfCompletionText;

            for (int i = 0; i < ExcelTable.Rows.Count; i++) {
                //setDegreeOfCompletionText(i, ExcelTable.Rows.Count);
                action.Invoke(((i + 1) * 100 / (ExcelTable.Rows.Count)));
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

            if (dataRow["備註"].ToString().ToUpper() == "IDENTIFY") {
                commandString = commandString + "ALTER TABLE " + dataRow["規格書"].ToString() + " ADD " + dataRow["資料行名稱"].ToString() + " " + dataRow["資料類型"].ToString() + " " + "IDENTITY(1, 1)" + " ;\r\n";
            } else {
                commandString = commandString + "ALTER TABLE " + dataRow["規格書"].ToString() + " ADD " + dataRow["資料行名稱"].ToString() + " " + dataRow["資料類型"].ToString() + " ;\r\n";
            }

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

        private String getFIELD_TYPE(String InputString) {
            if (InputString.Contains("(")) {
                return InputString.Substring(0, InputString.IndexOf("("));
            } else {
                return InputString;
            }
        }

        private String getFIELD_LENGTH(String InputString) {
            String judgmentString = "";
            if (InputString.Contains("(")) {
                judgmentString = InputString.Substring(0, InputString.IndexOf("("));
            } else {
                judgmentString = InputString;
            }
            switch (judgmentString) {
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
                    if (InputString.Substring(InputString.IndexOf("(") + 1, InputString.Length - InputString.IndexOf("(") - 2) == "MAX")
                        return "2147483647";
                    else
                        return InputString.Substring(InputString.IndexOf("(") + 1, InputString.Length - InputString.IndexOf("(") - 2);
                case "varchar":
                    if (InputString.Substring(InputString.IndexOf("(") + 1, InputString.Length - InputString.IndexOf("(") - 2) == "MAX")
                        return "2147483647";
                    else
                        return InputString.Substring(InputString.IndexOf("(") + 1, InputString.Length - InputString.IndexOf("(") - 2);
                case "nvarchar":
                    if (InputString.Substring(InputString.IndexOf("(") + 1, InputString.Length - InputString.IndexOf("(") - 2) == "MAX")
                        return "2147483647";
                    else
                        return InputString.Substring(InputString.IndexOf("(") + 1, InputString.Length - InputString.IndexOf("(") - 2);
                default:
                    return "";


            }
        }

        private String getIS_KEY(String InputString) {
            if (InputString == "P")
                return "Y";
            else
                return "N";
        }

        private String getIS_NULL(String InputString) {
            if (InputString == "NULL")
                return "N";
            else
                return "Y";
        }

        private String getInsertString(DataTable ExcelTable, int ExcelTableIndex, int TableIndex, String StartEnd) {
            String insertString = "";
            if (StartEnd == "Start") {
                insertString = "('" + (ExcelTable.Rows[ExcelTableIndex]["規格書"].ToString() + "', '"
                + "*" + "', "
                + "0" + ", "
                + "NULL" + ", '"
                + "N" + "', "
                + "NULL" + ", '"
                + ExcelTable.Rows[ExcelTableIndex]["規格書名稱"].ToString() + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, "
                + "NULL" + ", "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, NULL, NULL),\r\n"
                );
            } else if (StartEnd == "End") {
                insertString = "('" + (ExcelTable.Rows[ExcelTableIndex]["規格書"].ToString() + "', '"
                + ExcelTable.Rows[ExcelTableIndex]["資料行名稱"].ToString() + "', "
                + TableIndex.ToString() + ", '"
                + getFIELD_TYPE(ExcelTable.Rows[ExcelTableIndex]["資料類型"].ToString()) + "', '"
                + getIS_KEY(ExcelTable.Rows[ExcelTableIndex]["KEY"].ToString()) + "', "
                + getFIELD_LENGTH(ExcelTable.Rows[ExcelTableIndex]["資料類型"].ToString()) + ", '"
                + ExcelTable.Rows[ExcelTableIndex]["資料行中文名稱"].ToString() + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, '"
                + getIS_NULL(ExcelTable.Rows[ExcelTableIndex]["允許Null"].ToString()) + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, NULL, NULL)\r\n"
                );
            } else {
                insertString = "('" + (ExcelTable.Rows[ExcelTableIndex]["規格書"].ToString() + "', '"
                + ExcelTable.Rows[ExcelTableIndex]["資料行名稱"].ToString() + "', "
                + TableIndex.ToString() + ", '"
                + getFIELD_TYPE(ExcelTable.Rows[ExcelTableIndex]["資料類型"].ToString()) + "', '"
                + getIS_KEY(ExcelTable.Rows[ExcelTableIndex]["KEY"].ToString()) + "', "
                + getFIELD_LENGTH(ExcelTable.Rows[ExcelTableIndex]["資料類型"].ToString()) + ", '"
                + ExcelTable.Rows[ExcelTableIndex]["資料行中文名稱"].ToString() + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, '"
                + getIS_NULL(ExcelTable.Rows[ExcelTableIndex]["允許Null"].ToString()) + "', "
                + "NULL, NULL , NULL, NULL, NULL, NULL, NULL, NULL, NULL),\r\n"
                );
            }


            return insertString;
        }

        private String getDectionaryString(DataTable ExcelTable, String _startEndLocation, String _primaryKeyLocation) {
            String InsetString = "INSERT INTO COLDEF(TABLE_NAME,FIELD_NAME,SEQ,FIELD_TYPE,IS_KEY,FIELD_LENGTH,CAPTION,EDITMASK,NEEDBOX,CANREPORT,EXT_MENUID,FIELD_SCALE,DD_NAME,DEFAULT_VALUE,CHECK_NULL,QUERYMODE,CAPTION1,CAPTION2,CAPTION3,CAPTION4,CAPTION5,CAPTION6,CAPTION7,CAPTION8) VALUES \r\n";
            String[] startEndLocation = _startEndLocation.Split(',');
            int startEndLocationIndex = 0;
            int index = 1;
            Action<int> action = setDegreeOfCompletionText;

            for (int i = 0; i < ExcelTable.Rows.Count; i++) {
                //setDegreeOfCompletionText(i, ExcelTable.Rows.Count);
                action.Invoke(((i + 1) * 100 / (ExcelTable.Rows.Count)));
                if ("e" + i == startEndLocation[startEndLocation.Length - 1]) {
                    InsetString += getInsertString(ExcelTable, i, index, "End");
                } else if ("e" + i == startEndLocation[startEndLocationIndex]) {
                    InsetString += getInsertString(ExcelTable, i, index, "");
                    startEndLocationIndex++;
                    index = 1;
                } else {
                    if ("s" + i == startEndLocation[startEndLocationIndex]) {
                        InsetString += getInsertString(ExcelTable, i, index, "Start");
                        startEndLocationIndex++;
                    }
                    InsetString += getInsertString(ExcelTable, i, index, "");
                    index++;
                }
            }

            return InsetString;
        }


        private void setSQL_CommandStringText(String CommandString) {

            Clipboard.SetData(DataFormats.Text, CommandString);

            SQL_CommandString.Text = CommandString;
        }

        private void setDegreeOfCompletionText(int current) {
            DegreeOfCompletion.Text = "完成率：" + current + "％";
            progressBar1.Value = current;
            System.Windows.Forms.Application.DoEvents();
        }

        private void resetDegreeOfCompletionText() {
            DegreeOfCompletion.Text = "完成率：" + 0 + "％";
            progressBar1.Value = 0;
        }

        private void button_Cooy_Click(object sender, EventArgs e) {
            SQL_CommandString.SelectAll();
            SQL_CommandString.Copy();
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

        private void button_Dectionary_Click(object sender, EventArgs e) {
            resetDegreeOfCompletionText();
            DataTable TableValue = ImportExcel();
            String startEndLocation = getStartEndLocation(TableValue);
            String primaryKeyLocation = getPrimaryKeyLocation(TableValue);
            String commandString = getDectionaryString(TableValue, startEndLocation, primaryKeyLocation);

            setSQL_CommandStringText(commandString);
        }
    }
}

