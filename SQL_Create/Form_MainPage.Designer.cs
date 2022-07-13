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
            this.SQLCommandString = new System.Windows.Forms.TextBox();
            this.button_Alter = new System.Windows.Forms.Button();
            this.button_Cooy = new System.Windows.Forms.Button();
            this.button_Dectionary = new System.Windows.Forms.Button();
            this.button_command = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Create
            // 
            this.button_Create.BackColor = System.Drawing.SystemColors.Control;
            this.button_Create.FlatAppearance.BorderSize = 0;
            this.button_Create.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Create.Location = new System.Drawing.Point(12, 426);
            this.button_Create.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(112, 54);
            this.button_Create.TabIndex = 3;
            this.button_Create.Text = "新增資料表";
            this.button_Create.UseVisualStyleBackColor = false;
            this.button_Create.Click += new System.EventHandler(this.button_CreateTable_Click);
            // 
            // SQLCommandString
            // 
            this.SQLCommandString.HideSelection = false;
            this.SQLCommandString.Location = new System.Drawing.Point(12, 13);
            this.SQLCommandString.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SQLCommandString.Multiline = true;
            this.SQLCommandString.Name = "SQLCommandString";
            this.SQLCommandString.ReadOnly = true;
            this.SQLCommandString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SQLCommandString.Size = new System.Drawing.Size(584, 405);
            this.SQLCommandString.TabIndex = 4;
            // 
            // button_Alter
            // 
            this.button_Alter.BackColor = System.Drawing.SystemColors.Control;
            this.button_Alter.FlatAppearance.BorderSize = 0;
            this.button_Alter.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Alter.Location = new System.Drawing.Point(130, 426);
            this.button_Alter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Alter.Name = "button_Alter";
            this.button_Alter.Size = new System.Drawing.Size(112, 54);
            this.button_Alter.TabIndex = 5;
            this.button_Alter.Text = "增修欄位";
            this.button_Alter.UseVisualStyleBackColor = false;
            this.button_Alter.Click += new System.EventHandler(this.button_Alter_Click);
            // 
            // button_Cooy
            // 
            this.button_Cooy.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button_Cooy.FlatAppearance.BorderSize = 0;
            this.button_Cooy.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Cooy.Location = new System.Drawing.Point(484, 426);
            this.button_Cooy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Cooy.Name = "button_Cooy";
            this.button_Cooy.Size = new System.Drawing.Size(112, 54);
            this.button_Cooy.TabIndex = 7;
            this.button_Cooy.Text = "複製\r\n所有內容";
            this.button_Cooy.UseVisualStyleBackColor = false;
            this.button_Cooy.Click += new System.EventHandler(this.button_Cooy_Click);
            // 
            // button_Dectionary
            // 
            this.button_Dectionary.BackColor = System.Drawing.SystemColors.Control;
            this.button_Dectionary.FlatAppearance.BorderSize = 0;
            this.button_Dectionary.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Dectionary.Location = new System.Drawing.Point(366, 426);
            this.button_Dectionary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Dectionary.Name = "button_Dectionary";
            this.button_Dectionary.Size = new System.Drawing.Size(112, 55);
            this.button_Dectionary.TabIndex = 8;
            this.button_Dectionary.Text = "新增Data Dectionary";
            this.button_Dectionary.UseVisualStyleBackColor = false;
            this.button_Dectionary.Click += new System.EventHandler(this.button_Dectionary_Click);
            // 
            // button_command
            // 
            this.button_command.BackColor = System.Drawing.SystemColors.Control;
            this.button_command.FlatAppearance.BorderSize = 0;
            this.button_command.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_command.Location = new System.Drawing.Point(248, 426);
            this.button_command.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_command.Name = "button_command";
            this.button_command.Size = new System.Drawing.Size(112, 54);
            this.button_command.TabIndex = 9;
            this.button_command.Text = "產生所有\r\n規格書字串";
            this.button_command.UseVisualStyleBackColor = false;
            this.button_command.Click += new System.EventHandler(this.button_command_Click);
            // 
            // Form_MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(613, 493);
            this.Controls.Add(this.button_command);
            this.Controls.Add(this.button_Dectionary);
            this.Controls.Add(this.button_Cooy);
            this.Controls.Add(this.button_Alter);
            this.Controls.Add(this.SQLCommandString);
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
        private System.Windows.Forms.TextBox SQLCommandString;
        private System.Windows.Forms.Button button_Alter;
        private System.Windows.Forms.Button button_Cooy;
        private System.Windows.Forms.Button button_Dectionary;
        private System.Windows.Forms.Button button_command;
    }
}

