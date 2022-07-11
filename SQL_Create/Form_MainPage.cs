using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SQLCommandString
{
    public partial class Form_MainPage : Form
    {
        private string excelPath = "";

        public Form_MainPage()
        {
            InitializeComponent();
        }

        private void SetSQLCommandStringText(string commandString)
        {

            Clipboard.SetData(DataFormats.Text, commandString);

            SQLCommandString.Text = commandString;
        }

        

        private void ResetDegreeOfCompletionText()
        {
            DegreeOfCompletion.Text = "完成率：" + 0 + "％";
            progressBar1.Value = 0;
        }

        private void button_Cooy_Click(object sender, EventArgs e)
        {
            SQLCommandString.SelectAll();
            SQLCommandString.Copy();
        }

        private void button_CreateTable_Click(object sender, EventArgs e)
        {
            //ResetDegreeOfCompletionText();
            DataTable TableValue = UtilityTools.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            List<string> startEndLocation = UtilityTools.GetStartEndLocation(TableValue);
            List<string> primaryKeyLocation = UtilityTools.GetPrimaryKeyLocation(TableValue);
            string commandString = UtilityTools.GetCreateString(TableValue, startEndLocation, primaryKeyLocation);

            SetSQLCommandStringText(commandString);
        }

        private void button_Alter_Click(object sender, EventArgs e)
        {
            //ResetDegreeOfCompletionText();
            DataTable TableValue = UtilityTools.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            string commandString = UtilityTools.GetAlterString(TableValue);

            SetSQLCommandStringText(commandString);
        }

        private void button_Dectionary_Click(object sender, EventArgs e)
        {
            //ResetDegreeOfCompletionText();
            DataTable TableValue = UtilityTools.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            List<string> startEndLocation = UtilityTools.GetStartEndLocation(TableValue);            
            string commandString = UtilityTools.GetDectionaryString(TableValue, startEndLocation);

            SetSQLCommandStringText(commandString);
        }

        private void Form_MainPage_Load(object sender, EventArgs e)
        {
            excelPath = UtilityTools.CreateExcelFile();
        }

        private void Form_MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.Delete(excelPath);
        }

    }
}

