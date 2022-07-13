using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCommandBuilder
{
    static class AlterManager
    {
        public static string GetAlterString(DataTable excelTable)
        {
            StringBuilder commandString = new StringBuilder("");
            StringBuilder addString = new StringBuilder("");
            StringBuilder dropString = new StringBuilder("");
            StringBuilder modifyString = new StringBuilder("");
            StringBuilder changeString = new StringBuilder("");

            for (int i = 0 ; i < excelTable.Rows.Count ; i++)
            {
                switch (excelTable.Rows[i]["修改註記V"].ToString())
                {
                    case "A":
                        addString.Append(GetAlterAddString(excelTable.Rows[i]));
                        break;
                    case "D":
                        dropString.Append(GetAlterDropString(excelTable.Rows[i]));
                        break;
                    case "C":
                        changeString.Append(GetAlterChangeString(excelTable.Rows[i]));
                        break;
                    case "M":
                        modifyString.Append(GetAlterModifyString(excelTable.Rows[i]));
                        break;
                }
            }

            commandString.Append(addString.ToString());
            commandString.Append(dropString.ToString());
            commandString.Append(modifyString.ToString());

            if (!string.IsNullOrEmpty(changeString.ToString()))
            {
                commandString.Append("\r\n---------- 以下請逐行執行 ----------\r\n");
                commandString.Append(changeString);
            }

            return commandString.ToString();
        }

        private static string GetAlterAddString(DataRow dataRow)
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

        private static string GetAlterDropString(DataRow dataRow)
        {
            string alterDropString = $"ALTER TABLE {dataRow["規格書"].ToString()} DROP COLUMN {dataRow["資料行名稱"].ToString()} ;\r\n";

            return alterDropString;
        }

        private static string GetAlterChangeString(DataRow dataRow)
        {
            string alterChangeString = $"\r\n sp_rename '{dataRow["規格書"].ToString() }.{dataRow["原欄位名稱"].ToString()}', '{dataRow["資料行名稱"].ToString()}', 'COLUMN' ;\r\n";
            StringBuilder commandString = new StringBuilder();

            return alterChangeString;
        }

        private static string GetAlterModifyString(DataRow dataRow)
        {
            string alterModifyString = $"ALTER TABLE {dataRow["規格書"].ToString()} ALTER COLUMN {dataRow["資料行名稱"].ToString()} {dataRow["資料類型"].ToString()} ;\r\n";

            return alterModifyString;
        }
    }
}
