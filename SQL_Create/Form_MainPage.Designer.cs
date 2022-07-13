namespace SqlCommandBuilder {
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
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonAlter = new System.Windows.Forms.Button();
            this.buttonCooy = new System.Windows.Forms.Button();
            this.buttonDectionary = new System.Windows.Forms.Button();
            this.buttonCommand = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.SQLCommandString = new System.Windows.Forms.TextBox();
            this.panelMenu.SuspendLayout();
            this.panelDesktop.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            this.buttonCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(223)))), ((int)(((byte)(187)))));
            this.buttonCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonCreate.Location = new System.Drawing.Point(12, 13);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(175, 54);
            this.buttonCreate.TabIndex = 3;
            this.buttonCreate.Text = "Create Table";
            this.buttonCreate.UseVisualStyleBackColor = false;
            this.buttonCreate.Click += new System.EventHandler(this.button_CreateTable_Click);
            // 
            // buttonAlter
            // 
            this.buttonAlter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(223)))), ((int)(((byte)(187)))));
            this.buttonAlter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAlter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonAlter.Location = new System.Drawing.Point(12, 83);
            this.buttonAlter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonAlter.Name = "buttonAlter";
            this.buttonAlter.Size = new System.Drawing.Size(175, 54);
            this.buttonAlter.TabIndex = 5;
            this.buttonAlter.Text = "Alter Column";
            this.buttonAlter.UseVisualStyleBackColor = false;
            this.buttonAlter.Click += new System.EventHandler(this.button_Alter_Click);
            // 
            // buttonCooy
            // 
            this.buttonCooy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(175)))), ((int)(((byte)(155)))));
            this.buttonCooy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCooy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonCooy.Location = new System.Drawing.Point(12, 426);
            this.buttonCooy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCooy.Name = "buttonCooy";
            this.buttonCooy.Size = new System.Drawing.Size(175, 54);
            this.buttonCooy.TabIndex = 7;
            this.buttonCooy.Text = "Copy Content";
            this.buttonCooy.UseVisualStyleBackColor = false;
            this.buttonCooy.Click += new System.EventHandler(this.button_Cooy_Click);
            // 
            // buttonDectionary
            // 
            this.buttonDectionary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(223)))), ((int)(((byte)(187)))));
            this.buttonDectionary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDectionary.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonDectionary.Location = new System.Drawing.Point(12, 223);
            this.buttonDectionary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonDectionary.Name = "buttonDectionary";
            this.buttonDectionary.Size = new System.Drawing.Size(175, 55);
            this.buttonDectionary.TabIndex = 8;
            this.buttonDectionary.Text = "Add Data Dectionary";
            this.buttonDectionary.UseVisualStyleBackColor = false;
            this.buttonDectionary.Visible = false;
            this.buttonDectionary.Click += new System.EventHandler(this.button_Dectionary_Click);
            // 
            // buttonCommand
            // 
            this.buttonCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(223)))), ((int)(((byte)(187)))));
            this.buttonCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCommand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonCommand.Location = new System.Drawing.Point(12, 153);
            this.buttonCommand.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCommand.Name = "buttonCommand";
            this.buttonCommand.Size = new System.Drawing.Size(175, 54);
            this.buttonCommand.TabIndex = 9;
            this.buttonCommand.Text = "Create and Alter";
            this.buttonCommand.UseVisualStyleBackColor = false;
            this.buttonCommand.Click += new System.EventHandler(this.button_command_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(238)))), ((int)(((byte)(213)))));
            this.panelMenu.Controls.Add(this.buttonCreate);
            this.panelMenu.Controls.Add(this.buttonCooy);
            this.panelMenu.Controls.Add(this.buttonDectionary);
            this.panelMenu.Controls.Add(this.buttonCommand);
            this.panelMenu.Controls.Add(this.buttonAlter);
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
            this.SQLCommandString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(227)))), ((int)(((byte)(176)))));
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
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonAlter;
        private System.Windows.Forms.Button buttonCooy;
        private System.Windows.Forms.Button buttonDectionary;
        private System.Windows.Forms.Button buttonCommand;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.TextBox SQLCommandString;
    }
}

