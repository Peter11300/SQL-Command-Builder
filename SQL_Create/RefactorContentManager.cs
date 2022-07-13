using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SQLCommandString
{
    class RefactorContentManager
    {
        public static List<string> GetSqlCreateTableName(DataTable excelData)
        {
            DataTable dataTableCopy = excelData.Select($"修改註記V = 'AT'").CopyToDataTable();

            DataTable distDt = dataTableCopy.DefaultView.ToTable(true, "規格書");

            List<string> sqlTableNames = distDt.AsEnumerable().Select(x => x["規格書"].ToString()).Where(y => y != "" && y != null).ToList();

            return sqlTableNames;
        }

        public static DataSet GetCreateDataSet(DataTable excelData, List<string> sqlTableNames)
        {
            DataSet organizedData = new DataSet();

            foreach (string sqlTableName in sqlTableNames)
            {
                DataTable selectedData = excelData.Select($"規格書 = '{sqlTableName}' AND 修改註記V = 'AT'").CopyToDataTable();
                selectedData.TableName = sqlTableName;
                organizedData.Tables.Add(selectedData);
            }

            return organizedData;
        }

        public static DataTable GetSqlAlterTable(DataTable excelData)
        {
            DataTable dataTableCopy = excelData.Select($"修改註記V = 'A' OR 修改註記V = 'D' OR 修改註記V = 'C' OR 修改註記V = 'M'").CopyToDataTable();

            return dataTableCopy;
        }        
    }
}