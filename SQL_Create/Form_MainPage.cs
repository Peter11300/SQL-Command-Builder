using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SqlCommandBuilder
{
    public partial class Form_MainPage : Form
    {
        private string excelPath = "";

        public Form_MainPage()
        {
            InitializeComponent();
        }

        private void button_Cooy_Click(object sender, EventArgs e)
        {
            SQLCommandString.SelectAll();
            SQLCommandString.Copy();
        }

        private void button_CreateTable_Click(object sender, EventArgs e)
        {
            DataTable TableValue = ExcelManager.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            List<string> ls = RefactorContentManager.GetSqlCreateTableName(TableValue);

            if (ls == null)
                return;

            DataSet organizedData = RefactorContentManager.GetCreateDataSet(TableValue, ls);

            string commandString = CreateManager.GetCreateString(organizedData);

            SetSQLCommandStringText(commandString);
        }

        private void button_Alter_Click(object sender, EventArgs e)
        {
            DataTable TableValue = ExcelManager.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            DataTable dt = RefactorContentManager.GetSqlAlterTable(TableValue);

            if (dt == null)
                return;

            string commandString = AlterManager.GetAlterString(dt);

            SetSQLCommandStringText(commandString);
        }

        private void button_Dectionary_Click(object sender, EventArgs e)
        {
            DataTable TableValue = ExcelManager.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            List<string> startEndLocation = ContentManager.GetStartEndLocation(TableValue);
            string commandString = ContentManager.GetDectionaryString(TableValue, startEndLocation);

            SetSQLCommandStringText(commandString);
        }

        private void Form_MainPage_Load(object sender, EventArgs e)
        {
            excelPath = ExcelManager.CreateExcelFile();
        }

        private void Form_MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.Delete(excelPath);
        }

        private void SetSQLCommandStringText(string commandString)
        {

            Clipboard.SetData(DataFormats.Text, commandString);

            SQLCommandString.Text = commandString;
        }

        private void button_command_Click(object sender, EventArgs e)
        {
            DataTable TableValue = ExcelManager.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            string commandString = "";

            List<string> ls = RefactorContentManager.GetSqlCreateTableName(TableValue);

            if (ls != null)
            {
                DataSet organizedData = RefactorContentManager.GetCreateDataSet(TableValue, ls);
                commandString = CreateManager.GetCreateString(organizedData);
            }

            commandString += "\r\n";

            DataTable dt = RefactorContentManager.GetSqlAlterTable(TableValue);

            if (dt != null)
                commandString += AlterManager.GetAlterString(dt);

            SetSQLCommandStringText(commandString);
        }
    }
}
