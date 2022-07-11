using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SQLCommandString
{
    public partial class Form_MainPage : Form
    {
        string excelPath = "";

        public Form_MainPage()
        {
            InitializeComponent();
        }

        //static void openfile(string mysheet) {
        //    var excelApp = new Excel.Application();
        //    excelApp.Visible = true;

        //    Excel.Workbooks books = excelApp.Workbooks;
        //    Excel.Workbook sheet = books.Open(mysheet);
        //}



        private void SetSQLCommandStringText(string commandString)
        {

            Clipboard.SetData(DataFormats.Text, commandString);

            SQL_CommandString.Text = commandString;
        }

        private void SetDegreeOfCompletionText(int current)
        {
            DegreeOfCompletion.Text = "完成率：" + current + "％";
            progressBar1.Value = current;
            System.Windows.Forms.Application.DoEvents();
        }

        private void ResetDegreeOfCompletionText()
        {
            DegreeOfCompletion.Text = "完成率：" + 0 + "％";
            progressBar1.Value = 0;
        }

        private void button_Cooy_Click(object sender, EventArgs e)
        {
            SQL_CommandString.SelectAll();
            SQL_CommandString.Copy();
        }

        private void button_CreateTable_Click(object sender, EventArgs e)
        {
            ResetDegreeOfCompletionText();
            DataTable TableValue = UtilityTools.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            string startEndLocation = UtilityTools.GetStartEndLocation(TableValue);
            string primaryKeyLocation = UtilityTools.GetPrimaryKeyLocation(TableValue);
            string commandString = UtilityTools.GetCreateString(TableValue, startEndLocation, primaryKeyLocation);

            SetSQLCommandStringText(commandString);
        }

        private void button_Alter_Click(object sender, EventArgs e)
        {
            ResetDegreeOfCompletionText();
            DataTable TableValue = UtilityTools.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            string commandString = UtilityTools.GetAlterString(TableValue);

            SetSQLCommandStringText(commandString);
        }

        private void button_Dectionary_Click(object sender, EventArgs e)
        {
            ResetDegreeOfCompletionText();
            DataTable TableValue = UtilityTools.ImportExcel(excelPath);

            if (TableValue.Rows.Count <= 0)
                return;

            string startEndLocation = UtilityTools.GetStartEndLocation(TableValue);
            string primaryKeyLocation = UtilityTools.GetPrimaryKeyLocation(TableValue);
            string commandString = UtilityTools.GetDectionaryString(TableValue, startEndLocation, primaryKeyLocation);

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

