using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SQLCommandString
{
    class RefactorContentManager
    {
        public static List<string> GetSqlTableName(DataTable dataTable, string expresion)
        {
            DataTable dataTableCopy = dataTable.Select($"修改註記V = '{expresion}'").CopyToDataTable();

            DataTable distDt = dataTableCopy.DefaultView.ToTable(true, "規格書");

            List<string> sqlTableNames = distDt.AsEnumerable().Select(x => x["規格書"].ToString()).Where(y => y != "" && y != null).ToList();

            return sqlTableNames;
        }

        public static DataSet GetDataSet(DataTable dataTable, List<string> sqlTableNames, string expresion)
        {
            DataSet dataSet = new DataSet();

            foreach (string sqlTableName in sqlTableNames)
            {
                DataTable selectedDT = dataTable.Select($"規格書 = '{sqlTableName}' AND 修改註記V = '{expresion}'").CopyToDataTable();
                selectedDT.TableName = sqlTableName;
                dataSet.Tables.Add(selectedDT);
            }

            return dataSet;
        }

        public static string GetCreateString(DataSet ds)
        {
            StringBuilder commandString = new StringBuilder("");

            foreach (DataTable excelTable in ds.Tables)
            {
                StringBuilder temp_commandString = new StringBuilder("");
                List<string> primaryKeyStringList = new List<string>();
                string createTableString = $"CREATE TABLE {excelTable.TableName} \r\n( \r\n";
                temp_commandString.Append(createTableString);

                for (int i = 0 ; i < excelTable.Rows.Count ; i++)
                {
                    if (excelTable.Rows[i]["KEY"].ToString() == "P")
                        primaryKeyStringList.Add(excelTable.Rows[i]["資料行名稱"].ToString());

                    if (i == excelTable.Rows.Count - 1)
                    {
                        temp_commandString.Append(GetColumnString(excelTable, i));

                        temp_commandString.Append($"\r\nPRIMARY KEY( {string.Join(", ", primaryKeyStringList.ToArray())} ) \r\n");
                        temp_commandString.Append("); \r\n\r\n");

                        commandString.Append(temp_commandString);
                    }
                    else
                        temp_commandString.Append(GetColumnString(excelTable, i));

                }

            }

            return commandString.ToString();
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
    }
}