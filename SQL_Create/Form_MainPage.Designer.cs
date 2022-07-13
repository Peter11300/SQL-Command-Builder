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
            this.button_Alter = new System.Windows.Forms.Button();
            this.button_Cooy = new System.Windows.Forms.Button();
            this.button_Dectionary = new System.Windows.Forms.Button();
            this.button_command = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.SQLCommandString = new System.Windows.Forms.TextBox();
            this.panelMenu.SuspendLayout();
            this.panelDesktop.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Create
            // 
            this.button_Create.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(189)))), ((int)(((byte)(33)))));
            this.button_Create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Create.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Create.Location = new System.Drawing.Point(12, 13);
            this.button_Create.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Create.Name = "button_Create";
            this.button_Create.Size = new System.Drawing.Size(175, 54);
            this.button_Create.TabIndex = 3;
            this.button_Create.Text = "Create Table";
            this.button_Create.UseVisualStyleBackColor = false;
            this.button_Create.Click += new System.EventHandler(this.button_CreateTable_Click);
            // 
            // button_Alter
            // 
            this.button_Alter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(189)))), ((int)(((byte)(33)))));
            this.button_Alter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Alter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Alter.Location = new System.Drawing.Point(12, 83);
            this.button_Alter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Alter.Name = "button_Alter";
            this.button_Alter.Size = new System.Drawing.Size(175, 54);
            this.button_Alter.TabIndex = 5;
            this.button_Alter.Text = "Alter Column";
            this.button_Alter.UseVisualStyleBackColor = false;
            this.button_Alter.Click += new System.EventHandler(this.button_Alter_Click);
            // 
            // button_Cooy
            // 
            this.button_Cooy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(145)))), ((int)(((byte)(137)))));
            this.button_Cooy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Cooy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Cooy.Location = new System.Drawing.Point(12, 426);
            this.button_Cooy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Cooy.Name = "button_Cooy";
            this.button_Cooy.Size = new System.Drawing.Size(175, 54);
            this.button_Cooy.TabIndex = 7;
            this.button_Cooy.Text = "Copy Content";
            this.button_Cooy.UseVisualStyleBackColor = false;
            this.button_Cooy.Click += new System.EventHandler(this.button_Cooy_Click);
            // 
            // button_Dectionary
            // 
            this.button_Dectionary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(189)))), ((int)(((byte)(33)))));
            this.button_Dectionary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Dectionary.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Dectionary.Location = new System.Drawing.Point(12, 223);
            this.button_Dectionary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Dectionary.Name = "button_Dectionary";
            this.button_Dectionary.Size = new System.Drawing.Size(175, 55);
            this.button_Dectionary.TabIndex = 8;
            this.button_Dectionary.Text = "Add Data Dectionary";
            this.button_Dectionary.UseVisualStyleBackColor = false;
            this.button_Dectionary.Visible = false;
            this.button_Dectionary.Click += new System.EventHandler(this.button_Dectionary_Click);
            // 
            // button_command
            // 
            this.button_command.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(189)))), ((int)(((byte)(33)))));
            this.button_command.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_command.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_command.Location = new System.Drawing.Point(12, 153);
            this.button_command.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_command.Name = "button_command";
            this.button_command.Size = new System.Drawing.Size(175, 54);
            this.button_command.TabIndex = 9;
            this.button_command.Text = "Create and Alter";
            this.button_command.UseVisualStyleBackColor = false;
            this.button_command.Click += new System.EventHandler(this.button_command_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(139)))), ((int)(((byte)(104)))));
            this.panelMenu.Controls.Add(this.button_Create);
            this.panelMenu.Controls.Add(this.button_Cooy);
            this.panelMenu.Controls.Add(this.button_Dectionary);
            this.panelMenu.Controls.Add(this.button_command);
            this.panelMenu.Controls.Add(this.button_Alter);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(200, 493);
            this.panelMenu.TabIndex = 10;
            // 
            // panelDesktop
            // 
            this.panelDesktop.Controls.Add(this.SQLCommandString);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(200, 0);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(753, 493);
            this.panelDesktop.TabIndex = 11;
            // 
            // SQLCommandString
            // 
            this.SQLCommandString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(188)))), ((int)(((byte)(158)))));
            this.SQLCommandString.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SQLCommandString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SQLCommandString.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SQLCommandString.HideSelection = false;
            this.SQLCommandString.Location = new System.Drawing.Point(0, 0);
            this.SQLCommandString.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SQLCommandString.Multiline = true;
            this.SQLCommandString.Name = "SQLCommandString";
            this.SQLCommandString.ReadOnly = true;
            this.SQLCommandString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SQLCommandString.Size = new System.Drawing.Size(753, 493);
            this.SQLCommandString.TabIndex = 5;
            // 
            // Form_MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(953, 493);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelMenu);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form_MainPage";
            this.Text = "SQL Command Builder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_MainPage_FormClosed);
            this.Load += new System.EventHandler(this.Form_MainPage_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelDesktop.ResumeLayout(false);
            this.panelDesktop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_Create;
        private System.Windows.Forms.Button button_Alter;
        private System.Windows.Forms.Button button_Cooy;
        private System.Windows.Forms.Button button_Dectionary;
        private System.Windows.Forms.Button button_command;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.TextBox SQLCommandString;
    }
}

