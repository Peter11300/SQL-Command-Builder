using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SQLCommandString
{
    public static class ContentManager
    {
        public static List<string> GetStartEndLocation(DataTable excelTable)
        {
            List<string> location = new List<string>();
            string preTableName = "";
            bool firstCheck = true;
            for (int i = 0 ; i < excelTable.Rows.Count ; i++)
            {
                if (excelTable.Rows[i]["修改註記V"].ToString() == "AT")
                {
                    if (firstCheck)
                    {
                        location.Add($"s{i}");
                        firstCheck = false;
                        preTableName = excelTable.Rows[i]["規格書"].ToString();
                    }
                    else
                    {
                        if (i + 1 < excelTable.Rows.Count)
                        {
                            if (!(excelTable.Rows[i]["規格書"].ToString().Equals(preTableName)))
                            {
                                location.Add($"e{(i - 1)}");
                                location.Add($"s{i}");
                                preTableName = excelTable.Rows[i]["規格書"].ToString();
                            }
                        }
                        else
                        {
                            location.Add($"e{i}");
                        }
                    }
                }
            }
            return location;
        }

        public static List<string> GetPrimaryKeyLocation(DataTable excelTable)
        {
            List<string> location = new List<string>();

            for (int i = 0 ; i < excelTable.Rows.Count ; i++)
            {
                if (excelTable.Rows[i]["KEY"].ToString() == "P" &&
                    excelTable.Rows[i]["修改註記V"].ToString() == "AT")
                {
                    location.Add($"p{i}");
                }
            }

            return location;
        }

        public static string GetColumnString(DataTable excelTable, int excelTableIndex)
        {
            StringBuilder columnString = new StringBuilder();

            columnString.Append(
                excelTable.Rows[excelTableIndex]["資料行名稱"].ToString() + " " +
                excelTable.Rows[excelTableIndex]["資料類型"].ToString() + " "
                );

            if (excelTable.Rows[excelTableIndex]["備註"].ToString().ToUpper() == "IDENTIFY")
            {
                columnString.Append("IDENTITY(1, 1) ");
            }

            columnString.Append(excelTable.Rows[excelTableIndex]["允許Null"].ToString() + " ");

            if (excelTable.Rows[excelTableIndex]["Constraint"].ToString().Contains("預設"))
            {
                string constraintStr = excelTable.Rows[excelTableIndex]["Constraint"].ToString();
                int startIndex = constraintStr.Contains("=") ? constraintStr.IndexOf("=") + 1 : 2;
                string def = constraintStr.Substring(startIndex, constraintStr.Length - startIndex);
                columnString.Append($"DEFAULT {def} ");
            }

            columnString.Append(", \r\n");

            return columnString.ToString();
        }

        public static string GetCreateString(DataTable excelTable, List<string> startEndLocationList, List<string> primaryKeyLocationList)
        {
            int startEndLocationIndex = 0;
            int primaryKeyLocationIndex = 0;
            StringBuilder commandString = new StringBuilder("");
            StringBuilder temp_commandString = new StringBuilder("");
            string primaryString = "\r\nPRIMARY KEY(";
            bool primaryCheck = true;

            for (int i = 0 ; i < excelTable.Rows.Count ; i++)
            {
                if (excelTable.Rows[i]["修改註記V"].ToString() == "AT")
                {
                    if (primaryKeyLocationIndex < primaryKeyLocationList.Count && $"p{i}" == primaryKeyLocationList[primaryKeyLocationIndex])
                    {
                        if (primaryCheck == false)
                        {
                            primaryString =
                                primaryString + "," +
                                excelTable.Rows[i]["資料行名稱"].ToString();

                        }
                        else
                        {
                            primaryString =
                                primaryString +
                                excelTable.Rows[i]["資料行名稱"].ToString();

                            primaryCheck = false;
                        }

                        primaryKeyLocationIndex++;
                    }

                    if ($"e{i}" == startEndLocationList[startEndLocationIndex])
                    {

                        temp_commandString.Append(GetColumnString(excelTable, i));

                        temp_commandString.Append(primaryString + ") \r\n");
                        temp_commandString.Append("); \r\n\r\n");

                        commandString.Append(temp_commandString);
                        primaryString = "\r\nPRIMARY KEY(";
                        temp_commandString.Clear();
                        startEndLocationIndex++;
                        primaryCheck = true;
                    }
                    else
                    {
                        if ($"s{i}" == startEndLocationList[startEndLocationIndex])
                        {
                            string createTableString = "CREATE TABLE ";

                            createTableString =
                                createTableString +
                                excelTable.Rows[i]["規格書"].ToString() +
                                "\r\n( \r\n";

                            temp_commandString.Append(createTableString);
                            startEndLocationIndex++;
                        }

                        temp_commandString.Append(GetColumnString(excelTable, i));
                    }
                }

            }

            return commandString.ToString();
        }

        public static string GetAlterString(DataTable excelTable)
        {
            StringBuilder commandString = new StringBuilder("");
            StringBuilder commandAddString = new StringBuilder("");
            StringBuilder commandDropString = new StringBuilder("");
            StringBuilder commandModifyString = new StringBuilder("");
            StringBuilder storedprocedureString = new StringBuilder("");

            for (int i = 0 ; i < excelTable.Rows.Count ; i++)
            {
                switch (excelTable.Rows[i]["修改註記V"].ToString())
                {
                    case "A":
                        commandAddString.Append(GetAlterAddString(excelTable.Rows[i]));
                        break;
                    case "D":
                        commandDropString.Append(GetAlterDropString(excelTable.Rows[i]));
                        break;
                    case "C":
                        storedprocedureString.Append(GetAlterChangeString(excelTable.Rows[i]));
                        break;
                    case "M":
                        commandModifyString.Append(GetAlterModifyString(excelTable.Rows[i]));
                        break;
                }
            }

            commandString.Append(commandAddString.ToString());
            commandString.Append(commandDropString.ToString());
            commandString.Append(commandModifyString.ToString());

            if (!string.IsNullOrEmpty(storedprocedureString.ToString()))
            {
                commandString.Append("\r\n---------- 以下請逐行執行 ----------\r\n");
                commandString.Append(storedprocedureString);
            }

            return commandString.ToString();
        }

        public static string GetAlterAddString(DataRow dataRow)
        {
            StringBuilder commandString = new StringBuilder();

            commandString.Append(
                "ALTER TABLE " +
                dataRow["規格書"].ToString() +
                " ADD " +
                dataRow["資料行名稱"].ToString() + " " +
                dataRow["資料類型"].ToString() + " "
                );

            if (dataRow["備註"].ToString().ToUpper() == "IDENTIFY")
            {
                commandString.Append("IDENTITY(1, 1) ");
            }

            commandString.Append(dataRow["允許Null"].ToString() + " ");

            if (dataRow["Constraint"].ToString().Contains("預設"))
            {
                string constraintStr = dataRow["Constraint"].ToString();
                int startIndex = constraintStr.Contains("=") ? constraintStr.IndexOf("=") + 1 : 2;
                string def = constraintStr.Substring(startIndex, constraintStr.Length - startIndex);
                commandString.Append($"DEFAULT {def} ");
            }

            commandString.Append("; \r\n");

            return commandString.ToString();
        }

        public static string GetAlterDropString(DataRow dataRow)
        {
            string alterDropString = $"ALTER TABLE {dataRow["規格書"].ToString()} DROP COLUMN {dataRow["資料行名稱"].ToString()} ;\r\n";            

            return alterDropString;
        }

        public static string GetAlterChangeString(DataRow dataRow)
        {
            string alterChangeString = $"\r\n sp_rename '{dataRow["規格書"].ToString() }.{dataRow["原欄位名稱"].ToString()}', '{dataRow["資料行名稱"].ToString()}', 'COLUMN' ;\r\n";
            StringBuilder commandString = new StringBuilder();

            return alterChangeString;
        }

        public static string GetAlterModifyString(DataRow dataRow)
        {
            string alterModifyString = $"ALTER TABLE {dataRow["規格書"].ToString()} ALTER COLUMN {dataRow["資料行名稱"].ToString()} {dataRow["資料類型"].ToString()} ;\r\n";            

            return alterModifyString;
        }

        public static string GetFIELD_TYPE(string inputString)
        {
            return (inputString.Contains("(")) ? inputString.Substring(0, inputString.IndexOf("(")) : inputString;
        }

        public static string GetFIELD_LENGTH(string inputString)
        {
            string variableType = (inputString.Contains("(")) ? inputString.Substring(0, inputString.IndexOf("(")) : inputString;

            switch (variableType)
            {
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
                    return "NULL";


            }
        }

        public static string GetIS_KEY(string inputString)
        {
            return (inputString == "P") ? "Y" : "N";
        }

        public static string GetIS_NULL(string inputString)
        {
            return (inputString == "NULL") ? "N" : "Y";
        }

        public static string GetInsertString(DataTable excelTable, int excelTableIndex, int tableIndex, string startEnd)
        {
            StringBuilder insertString = new StringBuilder("");

            if (startEnd == "Start")
            {
                insertString.Append(
                    "('" +
                    (excelTable.Rows[excelTableIndex]["規格書"].ToString() + "', '" +
                    "*" + "', " +
                    "0" + ", " +
                    "NULL" + ", '" +
                    "N" + "', " +
                    "NULL" + ", '" +
                    excelTable.Rows[excelTableIndex]["規格書名稱"].ToString() + "', " +
                    "NULL, NULL , NULL, NULL, NULL, NULL, NULL, " +
                    "NULL" + ", " +
                    "NULL, NULL , NULL, NULL, NULL, NULL, NULL, NULL, NULL),\r\n"
                ));
            }
            else if (startEnd == "End")
            {
                insertString.Append(
                    "('" +
                    (excelTable.Rows[excelTableIndex]["規格書"].ToString() + "', '" +
                    excelTable.Rows[excelTableIndex]["資料行名稱"].ToString() + "', " +
                    tableIndex.ToString() + ", '" +
                    GetFIELD_TYPE(excelTable.Rows[excelTableIndex]["資料類型"].ToString()) + "', '" +
                    GetIS_KEY(excelTable.Rows[excelTableIndex]["KEY"].ToString()) + "', " +
                    GetFIELD_LENGTH(excelTable.Rows[excelTableIndex]["資料類型"].ToString()) + ", '" +
                    excelTable.Rows[excelTableIndex]["資料行中文名稱"].ToString() + "', " +
                    "NULL, NULL , NULL, NULL, NULL, NULL, NULL, '" +
                    GetIS_NULL(excelTable.Rows[excelTableIndex]["允許Null"].ToString()) + "', " +
                    "NULL, NULL , NULL, NULL, NULL, NULL, NULL, NULL, NULL)\r\n"
                ));
            }
            else
            {
                insertString.Append(
                    "('" +
                    (excelTable.Rows[excelTableIndex]["規格書"].ToString() + "', '" +
                    excelTable.Rows[excelTableIndex]["資料行名稱"].ToString() + "', " +
                    tableIndex.ToString() + ", '" +
                    GetFIELD_TYPE(excelTable.Rows[excelTableIndex]["資料類型"].ToString()) + "', '" +
                    GetIS_KEY(excelTable.Rows[excelTableIndex]["KEY"].ToString()) + "', " +
                    GetFIELD_LENGTH(excelTable.Rows[excelTableIndex]["資料類型"].ToString()) + ", '" +
                    excelTable.Rows[excelTableIndex]["資料行中文名稱"].ToString() + "', " +
                    "NULL, NULL , NULL, NULL, NULL, NULL, NULL, '" +
                    GetIS_NULL(excelTable.Rows[excelTableIndex]["允許Null"].ToString()) + "', " +
                    "NULL, NULL , NULL, NULL, NULL, NULL, NULL, NULL, NULL),\r\n"
                ));
            }


            return insertString.ToString();
        }

        public static string GetDectionaryString(DataTable excelTable, List<string> startEndLocationList)
        {
            StringBuilder InsetString = new StringBuilder("INSERT INTO COLDEF(TABLE_NAME,FIELD_NAME,SEQ,FIELD_TYPE,IS_KEY,FIELD_LENGTH,CAPTION,EDITMASK,NEEDBOX,CANREPORT,EXT_MENUID,FIELD_SCALE,DD_NAME,DEFAULT_VALUE,CHECK_NULL,QUERYMODE,CAPTION1,CAPTION2,CAPTION3,CAPTION4,CAPTION5,CAPTION6,CAPTION7,CAPTION8) VALUES \r\n");

            int startEndLocationIndex = 0;
            int index = 1;

            for (int i = 0 ; i < excelTable.Rows.Count ; i++)
            {
                if (excelTable.Rows[i]["修改註記V"].ToString() == "AT")
                {
                    if ($"e{i}" == startEndLocationList[startEndLocationList.Count - 1])
                    {
                        InsetString.Append(GetInsertString(excelTable, i, index, "End"));
                    }
                    else if ($"e{i}" == startEndLocationList[startEndLocationIndex])
                    {
                        InsetString.Append(GetInsertString(excelTable, i, index, ""));
                        startEndLocationIndex++;
                        index = 1;
                    }
                    else
                    {
                        if ($"s{i}" == startEndLocationList[startEndLocationIndex])
                        {
                            InsetString.Append(GetInsertString(excelTable, i, index, "Start"));
                            startEndLocationIndex++;
                        }
                        InsetString.Append(GetInsertString(excelTable, i, index, ""));
                        index++;
                    }
                }
            }

            return InsetString.ToString();
        }

    }
}