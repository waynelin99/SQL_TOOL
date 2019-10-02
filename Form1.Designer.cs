namespace SQLTool
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.RibbonPanel ribbonPanel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ExcelToDB = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator1 = new System.Windows.Forms.RibbonSeparator();
            this.TableToDB = new System.Windows.Forms.RibbonButton();
            this.ribbonSeparator2 = new System.Windows.Forms.RibbonSeparator();
            this.Cr_Ta_ComboBox1 = new System.Windows.Forms.RibbonComboBox();
            this.Cr_Ta_DB1 = new System.Windows.Forms.RibbonButton();
            this.Cr_Ta_DB2 = new System.Windows.Forms.RibbonButton();
            this.Cr_Ta_Btn1 = new System.Windows.Forms.RibbonButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton2 = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel();
            this.ribbonLabel1 = new System.Windows.Forms.RibbonLabel();
            this.ribbonLabel2 = new System.Windows.Forms.RibbonLabel();
            this.ribbonTab2 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.ID_Transfer = new System.Windows.Forms.RibbonButton();
            this.ID_ComboBox1 = new System.Windows.Forms.RibbonComboBox();
            this.ID_DB1 = new System.Windows.Forms.RibbonButton();
            this.ribbonButton3 = new System.Windows.Forms.RibbonButton();
            this.ID_ComboBox2 = new System.Windows.Forms.RibbonComboBox();
            this.ID_To_DB1 = new System.Windows.Forms.RibbonButton();
            this.ID_To_DB2 = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel5 = new System.Windows.Forms.RibbonPanel();
            this.ID_Big5_btn = new System.Windows.Forms.RibbonButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CT_TName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toggleSwitch1 = new JCS.ToggleSwitch();
            this.toggleSwitch2 = new JCS.ToggleSwitch();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ID_TName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_Where = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonPanel1
            // 
            ribbonPanel1.Items.Add(this.ExcelToDB);
            ribbonPanel1.Items.Add(this.ribbonSeparator1);
            ribbonPanel1.Items.Add(this.TableToDB);
            ribbonPanel1.Items.Add(this.ribbonSeparator2);
            ribbonPanel1.Items.Add(this.Cr_Ta_ComboBox1);
            ribbonPanel1.Items.Add(this.Cr_Ta_Btn1);
            ribbonPanel1.Name = "ribbonPanel1";
            ribbonPanel1.Text = "";
            // 
            // ExcelToDB
            // 
            this.ExcelToDB.Image = global::SQLTool.Properties.Resources.exceltodb_50;
            this.ExcelToDB.LargeImage = global::SQLTool.Properties.Resources.exceltodb_50;
            this.ExcelToDB.Name = "ExcelToDB";
            this.ExcelToDB.SmallImage = ((System.Drawing.Image)(resources.GetObject("ExcelToDB.SmallImage")));
            this.ExcelToDB.Text = "ExcelToDB";
            this.ExcelToDB.ToolTip = "請注意連線DB 。預設為外部MSSQL           ";
            this.ExcelToDB.ToolTipTitle = "匯入Exel建表  ";
            this.ExcelToDB.Click += new System.EventHandler(this.ExcelToDB_Click);
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // TableToDB
            // 
            this.TableToDB.Image = global::SQLTool.Properties.Resources.table_48;
            this.TableToDB.LargeImage = global::SQLTool.Properties.Resources.table_48;
            this.TableToDB.Name = "TableToDB";
            this.TableToDB.SmallImage = ((System.Drawing.Image)(resources.GetObject("TableToDB.SmallImage")));
            this.TableToDB.Text = "TableToDB";
            this.TableToDB.Click += new System.EventHandler(this.TableToDB_Click);
            // 
            // ribbonSeparator2
            // 
            this.ribbonSeparator2.Name = "ribbonSeparator2";
            // 
            // Cr_Ta_ComboBox1
            // 
            this.Cr_Ta_ComboBox1.DrawIconsBar = false;
            this.Cr_Ta_ComboBox1.DropDownItems.Add(this.Cr_Ta_DB1);
            this.Cr_Ta_ComboBox1.DropDownItems.Add(this.Cr_Ta_DB2);
            this.Cr_Ta_ComboBox1.Name = "Cr_Ta_ComboBox1";
            this.Cr_Ta_ComboBox1.Text = "Choose DB";
            this.Cr_Ta_ComboBox1.TextBoxText = "預設";
            this.Cr_Ta_ComboBox1.TextBoxTextChanged += new System.EventHandler(this.Cr_Ta_ComboBox1_TextBoxTextChanged);
            // 
            // Cr_Ta_DB1
            // 
            this.Cr_Ta_DB1.Image = ((System.Drawing.Image)(resources.GetObject("Cr_Ta_DB1.Image")));
            this.Cr_Ta_DB1.LargeImage = ((System.Drawing.Image)(resources.GetObject("Cr_Ta_DB1.LargeImage")));
            this.Cr_Ta_DB1.Name = "Cr_Ta_DB1";
            this.Cr_Ta_DB1.SmallImage = global::SQLTool.Properties.Resources.DB1;
            this.Cr_Ta_DB1.Text = "外部MSSQL";
            this.Cr_Ta_DB1.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Center;
            this.Cr_Ta_DB1.Value = "";
            // 
            // Cr_Ta_DB2
            // 
            this.Cr_Ta_DB2.Image = ((System.Drawing.Image)(resources.GetObject("Cr_Ta_DB2.Image")));
            this.Cr_Ta_DB2.LargeImage = ((System.Drawing.Image)(resources.GetObject("Cr_Ta_DB2.LargeImage")));
            this.Cr_Ta_DB2.Name = "Cr_Ta_DB2";
            this.Cr_Ta_DB2.SmallImage = global::SQLTool.Properties.Resources.DB1;
            this.Cr_Ta_DB2.Text = "內部MSSQL";
            this.Cr_Ta_DB2.Value = "";
            // 
            // Cr_Ta_Btn1
            // 
            this.Cr_Ta_Btn1.Image = ((System.Drawing.Image)(resources.GetObject("Cr_Ta_Btn1.Image")));
            this.Cr_Ta_Btn1.LargeImage = ((System.Drawing.Image)(resources.GetObject("Cr_Ta_Btn1.LargeImage")));
            this.Cr_Ta_Btn1.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Medium;
            this.Cr_Ta_Btn1.Name = "Cr_Ta_Btn1";
            this.Cr_Ta_Btn1.SmallImage = ((System.Drawing.Image)(resources.GetObject("Cr_Ta_Btn1.SmallImage")));
            this.Cr_Ta_Btn1.Text = "Connection Test";
            this.Cr_Ta_Btn1.TextAlignment = System.Windows.Forms.RibbonItem.RibbonItemTextAlignment.Right;
            this.Cr_Ta_Btn1.Click += new System.EventHandler(this.Cr_Ta_Btn1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(5, 143);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(710, 370);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon1.Size = new System.Drawing.Size(720, 138);
            this.ribbon1.TabIndex = 4;
            this.ribbon1.Tabs.Add(this.ribbonTab1);
            this.ribbon1.Tabs.Add(this.ribbonTab2);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(6, 26, 20, 0);
            this.ribbon1.TabSpacing = 3;
            this.ribbon1.Text = "ribbon1";
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Panels.Add(ribbonPanel1);
            this.ribbonTab1.Panels.Add(this.ribbonPanel3);
            this.ribbonTab1.Panels.Add(this.ribbonPanel4);
            this.ribbonTab1.Text = "Create Table";
            this.ribbonTab1.ToolTip = "";
            this.ribbonTab1.ActiveChanged += new System.EventHandler(this.ribbonTab1_ActiveChanged);
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.Items.Add(this.ribbonButton2);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "";
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.Image = global::SQLTool.Properties.Resources.Empty_rash;
            this.ribbonButton2.LargeImage = global::SQLTool.Properties.Resources.Empty_rash;
            this.ribbonButton2.Name = "ribbonButton2";
            this.ribbonButton2.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton2.SmallImage")));
            this.ribbonButton2.Text = "ClearWindow";
            this.ribbonButton2.ToolTip = "DoubleClick";
            this.ribbonButton2.ToolTipTitle = "Clear Window";
            this.ribbonButton2.DoubleClick += new System.EventHandler(this.ribbonButton2_DoubleClick);
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.Items.Add(this.ribbonLabel1);
            this.ribbonPanel4.Items.Add(this.ribbonLabel2);
            this.ribbonPanel4.Name = "ribbonPanel4";
            this.ribbonPanel4.Text = "Mode Setting";
            // 
            // ribbonLabel1
            // 
            this.ribbonLabel1.Name = "ribbonLabel1";
            this.ribbonLabel1.Text = "                     TableToDB";
            // 
            // ribbonLabel2
            // 
            this.ribbonLabel2.Name = "ribbonLabel2";
            this.ribbonLabel2.Text = "                     Developer ";
            this.ribbonLabel2.ToolTip = "General Mode：Don\'t show  SQL syntax    Developer Mode：Show SQL syntax";
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Panels.Add(this.ribbonPanel2);
            this.ribbonTab2.Panels.Add(this.ribbonPanel5);
            this.ribbonTab2.Text = "Insert Data";
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add(this.ID_Transfer);
            this.ribbonPanel2.Items.Add(this.ID_ComboBox1);
            this.ribbonPanel2.Items.Add(this.ribbonButton3);
            this.ribbonPanel2.Items.Add(this.ID_ComboBox2);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "";
            // 
            // ID_Transfer
            // 
            this.ID_Transfer.Image = global::SQLTool.Properties.Resources.data_transfer_48;
            this.ID_Transfer.LargeImage = global::SQLTool.Properties.Resources.data_transfer_48;
            this.ID_Transfer.Name = "ID_Transfer";
            this.ID_Transfer.SmallImage = ((System.Drawing.Image)(resources.GetObject("ID_Transfer.SmallImage")));
            this.ID_Transfer.Text = "Transfer";
            this.ID_Transfer.Click += new System.EventHandler(this.ID_Transfer_Click);
            // 
            // ID_ComboBox1
            // 
            this.ID_ComboBox1.DropDownItems.Add(this.ID_DB1);
            this.ID_ComboBox1.Name = "ID_ComboBox1";
            this.ID_ComboBox1.Text = "From - DB";
            this.ID_ComboBox1.TextBoxText = "正式機ORACLE";
            // 
            // ID_DB1
            // 
            this.ID_DB1.Image = ((System.Drawing.Image)(resources.GetObject("ID_DB1.Image")));
            this.ID_DB1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ID_DB1.LargeImage")));
            this.ID_DB1.Name = "ID_DB1";
            this.ID_DB1.SmallImage = global::SQLTool.Properties.Resources.DB1;
            this.ID_DB1.Text = "正式機ORACLE";
            this.ID_DB1.Value = "";
            // 
            // ribbonButton3
            // 
            this.ribbonButton3.DrawDropDownIconsBar = false;
            this.ribbonButton3.Image = global::SQLTool.Properties.Resources.outgoing_data_48;
            this.ribbonButton3.LargeImage = global::SQLTool.Properties.Resources.outgoing_data_48;
            this.ribbonButton3.Name = "ribbonButton3";
            this.ribbonButton3.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribbonButton3.SmallImage")));
            this.ribbonButton3.Text = "";
            // 
            // ID_ComboBox2
            // 
            this.ID_ComboBox2.DropDownItems.Add(this.ID_To_DB1);
            this.ID_ComboBox2.DropDownItems.Add(this.ID_To_DB2);
            this.ID_ComboBox2.Name = "ID_ComboBox2";
            this.ID_ComboBox2.Text = "To - DB";
            this.ID_ComboBox2.TextBoxText = "內部MSSQL";
            this.ID_ComboBox2.TextBoxTextChanged += new System.EventHandler(this.ID_ComboBox2_TextBoxTextChanged);
            // 
            // ID_To_DB1
            // 
            this.ID_To_DB1.Image = ((System.Drawing.Image)(resources.GetObject("ID_To_DB1.Image")));
            this.ID_To_DB1.LargeImage = ((System.Drawing.Image)(resources.GetObject("ID_To_DB1.LargeImage")));
            this.ID_To_DB1.Name = "ID_To_DB1";
            this.ID_To_DB1.SmallImage = global::SQLTool.Properties.Resources.DB1;
            this.ID_To_DB1.Text = "內部MSSQL";
            this.ID_To_DB1.Value = "192.168.20.74";
            // 
            // ID_To_DB2
            // 
            this.ID_To_DB2.Image = ((System.Drawing.Image)(resources.GetObject("ID_To_DB2.Image")));
            this.ID_To_DB2.LargeImage = ((System.Drawing.Image)(resources.GetObject("ID_To_DB2.LargeImage")));
            this.ID_To_DB2.Name = "ID_To_DB2";
            this.ID_To_DB2.SmallImage = global::SQLTool.Properties.Resources.DB1;
            this.ID_To_DB2.Text = "外部MSSQL";
            this.ID_To_DB2.Value = "";
            // 
            // ribbonPanel5
            // 
            this.ribbonPanel5.Items.Add(this.ID_Big5_btn);
            this.ribbonPanel5.Name = "ribbonPanel5";
            this.ribbonPanel5.Text = "";
            // 
            // ID_Big5_btn
            // 
            this.ID_Big5_btn.Image = global::SQLTool.Properties.Resources.big5_48;
            this.ID_Big5_btn.LargeImage = global::SQLTool.Properties.Resources.big5_48;
            this.ID_Big5_btn.Name = "ID_Big5_btn";
            this.ID_Big5_btn.SmallImage = ((System.Drawing.Image)(resources.GetObject("ID_Big5_btn.SmallImage")));
            this.ID_Big5_btn.Text = "BIG5";
            this.ID_Big5_btn.Click += new System.EventHandler(this.ID_Big5_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Lime;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(644, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "General";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CT_TName});
            this.dataGridView1.Location = new System.Drawing.Point(5, 143);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(196, 370);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // CT_TName
            // 
            this.CT_TName.HeaderText = "Table Name";
            this.CT_TName.Name = "CT_TName";
            this.CT_TName.Width = 150;
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(233)))), ((int)(((byte)(255)))));
            this.toggleSwitch1.Location = new System.Drawing.Point(436, 67);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toggleSwitch1.OnFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toggleSwitch1.OnText = "ON";
            this.toggleSwitch1.Size = new System.Drawing.Size(57, 16);
            this.toggleSwitch1.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Modern;
            this.toggleSwitch1.TabIndex = 6;
            this.toggleSwitch1.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.toggleSwitch1_CheckedChanged);
            // 
            // toggleSwitch2
            // 
            this.toggleSwitch2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(233)))), ((int)(((byte)(255)))));
            this.toggleSwitch2.Location = new System.Drawing.Point(436, 88);
            this.toggleSwitch2.Name = "toggleSwitch2";
            this.toggleSwitch2.OffFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toggleSwitch2.OnFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toggleSwitch2.OnText = "ON";
            this.toggleSwitch2.Size = new System.Drawing.Size(57, 16);
            this.toggleSwitch2.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Modern;
            this.toggleSwitch2.TabIndex = 7;
            this.toggleSwitch2.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.toggleSwitch2_CheckedChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_TName,
            this.ID_Where});
            this.dataGridView2.Location = new System.Drawing.Point(5, 143);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(234, 370);
            this.dataGridView2.TabIndex = 8;
            // 
            // ID_TName
            // 
            this.ID_TName.HeaderText = "Table Name";
            this.ID_TName.Name = "ID_TName";
            this.ID_TName.Width = 90;
            // 
            // ID_Where
            // 
            this.ID_Where.HeaderText = "Where";
            this.ID_Where.Name = "ID_Where";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 516);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.toggleSwitch2);
            this.Controls.Add(this.toggleSwitch1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ribbon1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "SQLTool";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonTab ribbonTab2;
        private System.Windows.Forms.RibbonButton ExcelToDB;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator1;
        private System.Windows.Forms.RibbonComboBox Cr_Ta_ComboBox1;
        private System.Windows.Forms.RibbonButton Cr_Ta_DB1;
        private System.Windows.Forms.RibbonButton Cr_Ta_DB2;
        private System.Windows.Forms.RibbonButton Cr_Ta_Btn1;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton ribbonButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RibbonButton TableToDB;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator2;
        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.RibbonLabel ribbonLabel1;
        private System.Windows.Forms.RibbonLabel ribbonLabel2;
        private JCS.ToggleSwitch toggleSwitch1;
        private JCS.ToggleSwitch toggleSwitch2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CT_TName;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_TName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_Where;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButton ID_Transfer;
        private System.Windows.Forms.RibbonButton ribbonButton3;
        private System.Windows.Forms.RibbonComboBox ID_ComboBox1;
        private System.Windows.Forms.RibbonButton ID_DB1;
        private System.Windows.Forms.RibbonComboBox ID_ComboBox2;
        private System.Windows.Forms.RibbonButton ID_To_DB1;
        private System.Windows.Forms.RibbonButton ID_To_DB2;
        private System.Windows.Forms.RibbonPanel ribbonPanel5;
        private System.Windows.Forms.RibbonButton ID_Big5_btn;
    }
}

