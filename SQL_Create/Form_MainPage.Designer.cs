namespace SQLCommandString {
    partial class Form_MainPage {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            this.button_Create = new System.Windows.Forms.Button();
            this.SQL_CommandString = new System.Windows.Forms.TextBox();
            this.button_Alter = new System.Windows.Forms.Button();
            this.DegreeOfCompletion = new System.Windows.Forms.Label();
            this.button_Cooy = new System.Windows.Forms.Button();
            this.button_Dectionary = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // button_Create
            // 
            this.button_Create.BackColor = System.Drawing.Color.PapayaWhip;
            this.button_Create.FlatAppearance.BorderSize = 0;
            this.button_Create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Create.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Create.Location = new System.Drawing.Point(9, 13);
            this.button_Create.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(112, 43);
            this.button_Create.TabIndex = 3;
            this.button_Create.Text = "新增資料表";
            this.button_Create.UseVisualStyleBackColor = false;
            this.button_Create.Click += new System.EventHandler(this.button_CreateTable_Click);
            // 
            // SQL_CommandString
            // 
            this.SQL_CommandString.HideSelection = false;
            this.SQL_CommandString.Location = new System.Drawing.Point(127, 13);
            this.SQL_CommandString.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SQL_CommandString.Multiline = true;
            this.SQL_CommandString.Name = "SQL_CommandString";
            this.SQL_CommandString.ReadOnly = true;
            this.SQL_CommandString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SQL_CommandString.Size = new System.Drawing.Size(459, 405);
            this.SQL_CommandString.TabIndex = 4;
            // 
            // button_Alter
            // 
            this.button_Alter.BackColor = System.Drawing.Color.PapayaWhip;
            this.button_Alter.FlatAppearance.BorderSize = 0;
            this.button_Alter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Alter.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Alter.Location = new System.Drawing.Point(9, 64);
            this.button_Alter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Alter.Name = "button_Alter";
            this.button_Alter.Size = new System.Drawing.Size(112, 43);
            this.button_Alter.TabIndex = 5;
            this.button_Alter.Text = "增修欄位";
            this.button_Alter.UseVisualStyleBackColor = false;
            this.button_Alter.Click += new System.EventHandler(this.button_Alter_Click);
            // 
            // DegreeOfCompletion
            // 
            this.DegreeOfCompletion.AutoSize = true;
            this.DegreeOfCompletion.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.DegreeOfCompletion.Location = new System.Drawing.Point(140, 438);
            this.DegreeOfCompletion.Name = "DegreeOfCompletion";
            this.DegreeOfCompletion.Size = new System.Drawing.Size(129, 27);
            this.DegreeOfCompletion.TabIndex = 6;
            this.DegreeOfCompletion.Text = "完成率：0％";
            // 
            // button_Cooy
            // 
            this.button_Cooy.BackColor = System.Drawing.Color.SkyBlue;
            this.button_Cooy.FlatAppearance.BorderSize = 0;
            this.button_Cooy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cooy.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Cooy.Location = new System.Drawing.Point(450, 426);
            this.button_Cooy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Cooy.Name = "button_Cooy";
            this.button_Cooy.Size = new System.Drawing.Size(112, 43);
            this.button_Cooy.TabIndex = 7;
            this.button_Cooy.Text = "複製內容";
            this.button_Cooy.UseVisualStyleBackColor = false;
            this.button_Cooy.Click += new System.EventHandler(this.button_Cooy_Click);
            // 
            // button_Dectionary
            // 
            this.button_Dectionary.BackColor = System.Drawing.Color.PapayaWhip;
            this.button_Dectionary.FlatAppearance.BorderSize = 0;
            this.button_Dectionary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Dectionary.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Dectionary.Location = new System.Drawing.Point(9, 115);
            this.button_Dectionary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Dectionary.Name = "button_Dectionary";
            this.button_Dectionary.Size = new System.Drawing.Size(112, 55);
            this.button_Dectionary.TabIndex = 8;
            this.button_Dectionary.Text = "新增Data Dectionary";
            this.button_Dectionary.UseVisualStyleBackColor = false;
            this.button_Dectionary.Click += new System.EventHandler(this.button_Dectionary_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(127, 434);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(295, 31);
            this.progressBar1.TabIndex = 9;
            // 
            // Form_MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(598, 481);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button_Dectionary);
            this.Controls.Add(this.button_Cooy);
            this.Controls.Add(this.DegreeOfCompletion);
            this.Controls.Add(this.button_Alter);
            this.Controls.Add(this.SQL_CommandString);
            this.Controls.Add(this.button_Create);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_MainPage";
            this.Text = "SQL字串產生";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_MainPage_FormClosed);
            this.Load += new System.EventHandler(this.Form_MainPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_Create;
        private System.Windows.Forms.TextBox SQL_CommandString;
        private System.Windows.Forms.Button button_Alter;
        private System.Windows.Forms.Label DegreeOfCompletion;
        private System.Windows.Forms.Button button_Cooy;
        private System.Windows.Forms.Button button_Dectionary;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

