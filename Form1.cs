using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KMUHMIS_EF.Repository;
using System.Data.SqlClient;
using OfficeOpenXml;
using KMUHMIS_EF.DefualtDTO;
using KMUHMIS_EF.Model;
using System.Data.OracleClient;
using System.Dynamic;
using System.Reflection;
using Microsoft.Samples.EntityDataReader;

namespace SQLTool
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class Globals
        {
            public static string HOST = "60.......";
            public static bool Cr_Ta_mode = false;
            public static string ID_HOST = "192.....";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridView_Load();
            dataGridView2.Hide();
            //toggleSwitch1.Checked = true;
        }
        public Dictionary<string, Exception> ExcelToSQL(string fullPath) // bool isHeader
        {

            #region Reference Url
            // https://github.com/JanKallman/EPPlus
            // http://ccsig.blogspot.com/2017/10/c-epplus-excel-xlsx.html
            // http://smile0103.blogspot.com/2014/02/excel_330.html
            // https://stackoverflow.com/questions/16828222/checking-the-background-color-of-excel-file-using-epplus-library
            #endregion
            bool isHeader = false;
            List<List<string>> ExcelData = new List<List<string>>();
            #region Read Excel
            //Dictionary<string, string> ExcelData = new Dictionary<string, string>();
            using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (ExcelPackage excelPkg = new ExcelPackage(fs))
                {
                    ExcelWorksheet sheet = excelPkg.Workbook.Worksheets[1];//取得Sheet1

                    int startRowIndex = sheet.Dimension.Start.Row;//起始列
                    int endRowIndex = sheet.Dimension.End.Row;//結束列

                    int startColumn = sheet.Dimension.Start.Column;//開始欄
                    int endColumn = 10;//sheet.Dimension.End.Column;//結束欄

                    if (isHeader)//有包含標題
                    {
                        startRowIndex += 1;
                    }
                    for (int currentRow = startRowIndex; currentRow <= endRowIndex; currentRow++)
                    {
                        //抓出當前的資料範圍
                        OfficeOpenXml.ExcelRange range = sheet.Cells[currentRow, startColumn, currentRow, endColumn];
                        //全部儲存格是完全空白時則跳過
                        if (range.Any(c => !string.IsNullOrEmpty(c.Text)) == false)
                        {
                            continue;//略過此列
                        }
                        else
                        {
                            List<string> tmp_row = new List<string>();
                            string FirstCellColor = range.FirstOrDefault().Style.Fill.BackgroundColor.Rgb;
                            tmp_row.Add(FirstCellColor);
                            for (int currentColumn = startColumn; currentColumn <= endColumn; currentColumn++)
                            {
                                tmp_row.Add(sheet.Cells[currentRow, currentColumn].Text.ToUpper());
                            }
                            ExcelData.Add(tmp_row);
                            if (sheet.Cells[currentRow, 1].Text == "資料表名稱")
                            {
                                currentRow += 2;
                            }
                        }
                    }
                }
            }
            #endregion
            #region ToMsql
            #region 功能 - 對應色碼(FF + R,G,B)
            // Title - FFFFCC99
            // Create - FF00B050
            // Edit - FF00B0F0
            // Delete - FFFF0000
            // EndLine - FFFFFF00
            #endregion
            #region ExcelData 索引定義
            //異動色碼	0
            //異動色塊  1
            //項目名稱	2
            //欄位名稱	3
            //欄位型態	4
            //PK	    5
            //FK	    6
            //NULL	    7
            //唯一	    8
            //必填	    9
            //備註說明  10
            #endregion
            ConnectDB<dynamic> MssqlDB = new ConnectDB<dynamic>();
            string Check_Host = Globals.HOST;
            //MSSQLRepository<dynamic> MssqlDB1 = new MSSQLRepository<dynamic>();
            string Sql = "";
            string table_name = "";
            string Tcomm_Sql = "";
            string Primary_key = "";
            string PK_Sql = "";
            string Mode = "create";
            string color = "";
            string edit_data = "";
            bool table_status = false;
            int Table_Num = 0;
            int table_no = 0;
            int TmpNum = 0;
            int index = 0;
            List<dynamic> OLD_Table = new List<dynamic>();
            Dictionary<string, Exception> Error = new Dictionary<string, Exception>();
            for (int i = 0; i < ExcelData.Count; i++)
            {
                if (ExcelData[i][1] == "資料表名稱")
                {
                    Sql = "";
                    table_name = "";
                    Tcomm_Sql = "";
                    Primary_key = "";
                    PK_Sql = "";
                    table_name = ExcelData[i][2];
                    Table_Num += 1;
                    index += 1;
                    try { table_status = int.Parse(MssqlDB.GetAlls<string>("SELECT COUNT(*) FROM  " + table_name, Check_Host).FirstOrDefault()) >= 0;}
                    catch { table_status = false; }
                    if (table_status == false)
                    {
                        Mode = "create";
                        Sql = "CREATE TABLE " + ExcelData[i][2] + "(";
                        if (ExcelData[i][3] != "") Tcomm_Sql = string.Format("exec sp_addextendedproperty 'MS_Description', '{0}', 'user', 'dbo', 'table', '{1}'", ExcelData[i][3], table_name);
                    }
                    else
                    {
                        Mode = "update";
                        OLD_Table = MssqlDB.GetAlls<dynamic>(string.Format(" SELECT COLUMN_NAME, " +
                                                                          " (UPPER(DATA_TYPE) + CASE WHEN CHARACTER_MAXIMUM_LENGTH IS NULL THEN '' ELSE '(' + CONVERT(varchar, CHARACTER_MAXIMUM_LENGTH) + ')' END) DATA_TYPE, " +
                                                                          " (CASE WHEN IS_NULLABLE = 'NO' THEN '' ELSE 'V' END) IS_NULLABLE " + // ,COLUMN_DEFAULT
                                                                          " FROM abc.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}'", table_name), Check_Host).ToList();
                        // 刪除原有PK 
                        PK_Sql += int.Parse(MssqlDB.GetAlls<string>("SELECT COUNT(*) FROM abc.INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE WHERE TABLE_NAME = '" + table_name + "'", Check_Host).FirstOrDefault()) >= 1 ?
                                  string.Format(" ALTER TABLE {0} DROP {0}_PK ;\n", table_name) : "";
                    }
                    continue;
                }
                Primary_key += ExcelData[i][5] != "" ? ExcelData[i][3] + "," : "";
                if (ExcelData[i][0] == "FFFFFF00" && ExcelData[i][1] != "資料表名稱")
                {
                    // Create Table
                    Sql = Sql.Substring(0, 1) == "C" ? Sql.Substring(0, Sql.Length - 1) + ")":Sql;
                    // Create Primary key
                    if (Primary_key != "") PK_Sql += string.Format("ALTER TABLE [dbo].[{0}] ADD CONSTRAINT [{0}_PK] PRIMARY KEY (", table_name) + Primary_key.Substring(0, Primary_key.Length - 1) + ")";
                    // Column comment
                    try
                    {
                        richTextBox1.Text += "--" + Mode.ToUpper() + " Table-"+table_name + "\n";
                        if (Globals.Cr_Ta_mode)
                        {
                            richTextBox1.Text += Sql + "\n";
                            richTextBox1.Text += "--新增Primary_key:\n";
                            richTextBox1.Text += PK_Sql + "\n";
                            richTextBox1.Text += "--新增Table註解:\n";
                            richTextBox1.Text += Tcomm_Sql + "\n";
                        }
                        richTextBox1.Text += string.Concat("----------------------", Table_Num.ToString(), "-" ,table_name ," 讀取成功!!", "---------------------\n");
                        //MessageBox.Show(Table_Num.ToString()+ "-" + table_name + "讀取成功!!");
                        using (SqlConnection conn = new SqlConnection($"Data Source={Globals.HOST};Initial Catalog=abc;Persist Security Info=True;User ID=abc;Password=abc;Connection Timeout=0"))
                        {
                            conn.Open();
                            SqlTransaction tran = conn.BeginTransaction();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandTimeout = 60;
                            cmd.CommandText = Sql + "\n" + PK_Sql + "\n" + Tcomm_Sql;
                            cmd.Transaction = tran;
                            int result = cmd.ExecuteNonQuery();
                            if (result == 0)
                            {
                                tran.Rollback();    //交易取消
                            }
                            else
                            {
                                tran.Commit();      //執行交易
                            }
                            conn.Close();
                        }
                    }
                    catch (Exception e) { MessageBox.Show(e.ToString() + "檔案格式內格式有誤!!"); }
                    index += 1;
                }

                else
                {
                    if (Mode == "create")
                    {
                        // Auto Increment
                        string Auto_Increment = ExcelData[i][3] == "NO123" ? " IDENTITY(1,1) ":"";
                        Sql += ExcelData[i][3] + " " + ExcelData[i][4] + " " + Auto_Increment + (ExcelData[i][7] == "" ? "NOT" : "") + " NULL,";
                        index += 1;
                    }
                    else
                    {
                        #region 功能 - 對應色碼(FF + R,G,B)
                        // Title - FFFFCC99
                        // Create - FF00B050
                        // Edit - FF00B0F0
                        // Delete - FFFF0000
                        // EndLine - FFFFFF00
                        #endregion
                        color = ExcelData[i][0];
                        edit_data = "";
                        TmpNum = Table_Num > 1 && Table_Num != table_no ? index : (Table_Num > 1 ? TmpNum : 1);
                        table_no = Table_Num;
                        index += 1;
                        switch (color)
                        {
                            // nochange
                            case null:
                                continue;
                            case "":
                                continue;
                            // new create
                            case "FF00B050":
                                Sql += string.Format(" ALTER TABLE {0} ADD {1} ;\n", table_name, ExcelData[i][3] + " " + ExcelData[i][4] + " " + (ExcelData[i][7] == "" ? "NOT" : "") + " NULL");
                                continue;
                            // edit
                            case "FF00B0F0":
                                // compare the column information
                                // Column name
                                edit_data = OLD_Table[i - TmpNum].COLUMN_NAME != ExcelData[i][3] ? ExcelData[i][3] : "";
                                Sql += edit_data != "" ? string.Format(" exec sp_rename  '{0}.{1}', {2};\n", table_name, OLD_Table[i - TmpNum].COLUMN_NAME, edit_data) : "";
                                // Data type
                                edit_data = OLD_Table[i - TmpNum].DATA_TYPE != ExcelData[i][4] ? ExcelData[i][4] : "";
                                Sql += edit_data != "" ? string.Format(" ALTER TABLE {0} ALTER COLUMN  {1} {2};\n", table_name, ExcelData[i][3], edit_data) : "";
                                // Nullable
                                edit_data = OLD_Table[i - TmpNum].IS_NULLABLE != ExcelData[i][7] ? (ExcelData[i][7] == "V" ? "NULL" : "NOT NULL") : "";
                                Sql += edit_data != "" ? string.Format(" ALTER TABLE {0} ALTER COLUMN  {1} {2} {3};\n", table_name, ExcelData[i][3], ExcelData[i][4], edit_data) : "";
                                continue;
                            // delete
                            case "FFFF0000":
                                PK_Sql += string.Format(" ALTER TABLE {0} DROP COLUMN {1} ;\n", table_name, ExcelData[i][3]);
                                continue;
                            // exception
                            default:
                                var Ex = new Exception("Excel row:" + i + "column:" + 1 + "、色碼錯誤");
                                Error.Add(table_name, Ex);
                                return Error;
                        }
                    }
                }
            }


            #endregion

            return Error;
        }



        #region TAB-[Create_Table]
        /// <summary>
        /// Cr_Ta_Btn1_Click - Connection Test;
        /// Cr_Ta_ComboBox1_TextBoxTextChanged - Choose DB - ComboBox;
        /// </summary>

        private void ExcelToDB_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                richTextBox1.Text = "檔案路徑：" + filename + "\n";
                ExcelToSQL(@filename);
                //MessageBox.Show(filename);
            }
        }               

        private void Cr_Ta_ComboBox1_TextBoxTextChanged(object sender, EventArgs e)
        {
            var Check_host = this.Cr_Ta_ComboBox1.TextBoxText.ToString();
            switch (Check_host)
            {
                case "外部MSSQL":
                    Globals.HOST = this.Cr_Ta_DB1.Value;
                    break;
                case "內部MSSQL":
                    Globals.HOST = this.Cr_Ta_DB2.Value;
                    break;
                default:
                    Globals.HOST = this.Cr_Ta_DB1.Value;
                    break;
            }            
        }

        private void Cr_Ta_Btn1_Click(object sender, EventArgs e)
        {
            string Check_Host = Globals.HOST;
            string Host_IP = "";
            ConnectDB<dynamic> MssqlDB = new ConnectDB<dynamic>();
            //MSSQLRepository<dynamic> MssqlDB = new MSSQLRepository<dynamic>();
            try
            {
                Host_IP = MssqlDB.GetAlls<dynamic>("select local_net_address IP_address from sys.dm_exec_connections where session_id = @@SPID", Check_Host).FirstOrDefault().IP_address;                
            }
            catch
            {
                Host_IP = "";
            }            
            switch (Host_IP)
            {
                case "192":
                    MessageBox.Show("內部-MsSql連線成功!!");
                    break;
                case "1923":
                    MessageBox.Show("Test-MsSql連線成功!!");
                    break;
                case "":
                    if (Check_Host == "60...")
                    {
                        try
                        {
                            var tmp = MssqlDB.GetAlls<string>("SELECT COUNT(*) FROM ACC_ITEM", Globals.HOST);
                            MessageBox.Show("外部-MsSql連線成功!!");
                        }
                        catch { MessageBox.Show("外部-MsSql連線失敗!!"); }                        
                    }
                    else
                    {
                        MessageBox.Show("連線失敗，請確認VPN!!");
                    }
                    break;               
            }
        }

        private void ribbonButton2_DoubleClick(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
        }             

        
        private void DataGridView_Load()
        {
            this.dataGridView1.Rows.Add(15);
            this.dataGridView2.Rows.Add(15);

            #region 綁定 DataSource

            //DataTable table = new DataTable();
            //// add columns to datatable
            //table.Columns.Add("Table Name", typeof(string));
            //table.Columns.Add("Where", typeof(string));

            //// add rows to datatable

            //for (int i=0;i < 5; i++)table.Rows.Add("", "");

            //this.dataGridView1.DataSource = table;

            #endregion
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // KeyCode of [Ctrl+v] http://www.physics.udel.edu/~watson/scen103/ascii.html
            char value = e.KeyChar;
            int key = (int)value;
            if (key != 22)
                return;
            PasteClipboard(dataGridView1);
        }

        private void PasteClipboard(DataGridView dataGridView)
        {
            #region 複製資料貼上Datagridview

            string clipboard = Clipboard.GetText();
            string[] lines = clipboard.Split('\n');

            //取得目前的行列數
            int currentRow = dataGridView.CurrentCell.RowIndex;
            int currentColumn = dataGridView.CurrentCell.ColumnIndex;
            int rowCount = dataGridView.Rows.Count;

            DataGridViewCell CurrentCell;
            //增加列數
            if (lines.Length > rowCount - currentRow)
            {
                int cut = lines.Length - (rowCount - currentRow);
                dataGridView.Rows.Add(cut);
            }
            //將值寫入到DataGridView
            foreach (string line in lines)
            {
                string[] cells = line.Split('\t');
                for (int i = 0; i < cells.Length; i++)
                {
                    //處理儲存格
                    CurrentCell = dataGridView[currentColumn + i, currentRow];
                    string value = cells[i];
                    CurrentCell.Value = value;
                }
                currentRow++;
            }

            #endregion
        }

        private void TableToDB_Click(object sender, EventArgs e)
        {
            // check TableToDB Mode switch
            if (toggleSwitch1.Checked)
            {
                MessageBox.Show("功能尚未支援!!");
                #region Get Data from dataGridView1
                //List<string> data = new List<string>();

                //for (int i = 0; i < dataGridView1.RowCount; i++)
                //{
                //    data.Add((dataGridView1.Rows[i].Cells[0].Value ?? "").ToString().ToUpper());
                //}

                #endregion

            }
            else
            {
                MessageBox.Show("請先將 TableToDB Mode 開啟!!");
            }
        }

        private void toggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            #region TableToDB Mode switch (改變視窗大小)
            //bool Chack = this.richTextBox1.Location.X == 5 && this.richTextBox1.Location.Y == 143;
            //if (Chack == true)
            //{
            //    this.richTextBox1.ClientSize = new Size(470, 370);
            //    this.richTextBox1.Location = new Point(245, 143);
            //}
            //else
            //{
            //    this.richTextBox1.ClientSize = new Size(710, 370);
            //    this.richTextBox1.Location = new Point(5, 143);
            //}
            if (toggleSwitch1.Checked)
            {
                this.richTextBox1.ClientSize = new Size(508, 370);
                this.richTextBox1.Location = new Point(207, 143);
            }
            else
            {
                this.richTextBox1.ClientSize = new Size(710, 370);
                this.richTextBox1.Location = new Point(5, 143);
            }
            #endregion
        }

        private void toggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            #region Developer Mode：Show SQL syntax

            string tmp = this.label1.Text;
            this.label1.Text = tmp == "General" ? "Developer" : "General";
            this.label1.BackColor = tmp == "General" ? Color.Yellow : Color.Lime;
            Globals.Cr_Ta_mode = tmp == "General" ? true : false;

            #endregion
        }

        #endregion

        private void ribbonTab1_ActiveChanged(object sender, EventArgs e)
        {
            #region Determine Create_Table - (show or hide & resize)

            if (ribbonTab1.Selected)
            {                
                toggleSwitch1.Show();
                toggleSwitch2.Show();
                dataGridView1.Show();
                dataGridView2.Hide();
                toggleSwitch1_CheckedChanged(this.toggleSwitch1, (EventArgs)e);
                label1.Show();
                //toggleSwitch2_CheckedChanged(this.toggleSwitch2, (EventArgs)e);
                //richTextBox1.ClientSize = new Size(508, 370);
                //richTextBox1.Location = new Point(207, 143); 
            }
            else
            {
                toggleSwitch1.Hide();
                toggleSwitch2.Hide();
                dataGridView1.Hide();
                dataGridView2.Show();
                richTextBox1.ClientSize = new Size(470, 370); 
                richTextBox1.Location = new Point(245, 143);
                label1.Hide();
            }

            #endregion
        }

        #region TAB-[Insert_Data]

        private void ID_Transfer_Click(object sender, EventArgs e)
        {
            List<TableToDB> data = new List<TableToDB>();

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                List<TableToDB> tmp = new List<TableToDB>();
                tmp.Add(new TableToDB() {
                    Table_Name = (dataGridView2.Rows[i].Cells[0].Value ?? "").ToString().ToUpper(),
                    Where = (dataGridView2.Rows[i].Cells[1].Value ?? "").ToString().ToUpper()
                });
                if (tmp.FirstOrDefault().Where.Contains("WHERE") && tmp.FirstOrDefault().Where.Length > 5)
                {
                    tmp.FirstOrDefault().Where = " " + tmp.FirstOrDefault().Where;
                }
                //else if (tmp.FirstOrDefault().Where.Length <= 5 && tmp.FirstOrDefault().Where.Length >= 1)
                //{
                //    MessageBox.Show("輸入無效字串!!");
                //    break;
                //}
                //else if (tmp.FirstOrDefault().Where.Contains("WHERE") == false)
                //{
                //    MessageBox.Show("輸入無效字串!!");
                //    break;
                //}
                else
                {
                    if (tmp.FirstOrDefault().Table_Name != "") data.AddRange(tmp.ToList());
                }
            }
            richTextBox1.Text = "";
            OracleTOMSSQLDATA(data);
            //List<string> tt = new List<string>() { "CHART"};//" CHART ", " ACO_GSDEP2 " };
            //List<string> ss = new List<string>() { ""};//"", " WHERE  YY>=105 " };
            //Dictionary<string, Exception> Error = new Dictionary<string, Exception>();

            //if (data.Count() >= 1)
            //{
            //    Error = OracleTOMSSQLDATA(data);
            //}
            //if (Error != null)
            //{
            //    foreach (var item in Error)
            //    {
            //        richTextBox1.Text += item.Key;
            //        richTextBox1.Text += item.Value;
            //    }
            //}

        }

        private void OracleTOMSSQLDATA(List<TableToDB> Data)
        {


            Dictionary<string, Exception> Error1 = new Dictionary<string, Exception>();
            // 批次取值數量
            int Catch_Max = 10000;
            int T_NUM = 0;

            foreach (var item in Data)
            {
                try
                {
                    var sqlcode = "";
                    string cloumnname = "";
                    T_NUM += 1;
                    richTextBox1.Text += string.Concat(T_NUM.ToString(), " - ", item.Table_Name + "\n");
                    MSSQLRepository<UpDefualtModelDTO> db = new MSSQLRepository<UpDefualtModelDTO>();
                    Oracle9iRepository<ACC_TITLE> services = new Oracle9iRepository<ACC_TITLE>();
                    var num_sql = (int)services.GetAlls<dynamic>("SELECT count(*) COUNT FROM " + item.Table_Name).FirstOrDefault().COUNT;
                    var list = services.GetAlls<dynamic>(
                        " SELECT " +
                        " B.TABLE_NAME AS TABLE_NAME , B.COLUMN_NAME AS COLUMN_NAME , B.COMMENTS AS COMMENTS , A.DATA_TYPE AS DATA_TYPE ,A.DATA_LENGTH AS DATA_LENGTH , A.DATA_PRECISION AS DATA_PRECISION,A.DATA_SCALE AS DATA_SCALE , A.NULLABLE AS NULLABLE " +
                        " FROM USER_TAB_COLUMNS A ,USER_COL_COMMENTS B WHERE A.TABLE_NAME = '" + item.Table_Name + "' AND A.TABLE_NAME = B.TABLE_NAME AND A.COLUMN_NAME = B.COLUMN_NAME ORDER BY COLUMN_ID ").ToList();
                    foreach (var items in list)
                    {
                        cloumnname = cloumnname + items.COLUMN_NAME + ",";
                    }
                    cloumnname = cloumnname.Substring(0, cloumnname.Length - 1);                    

                    for (int i = 0; i < (int)((num_sql / Catch_Max) + 1); i++)
                    {

                        sqlcode = String.Format("SELECT {0} FROM (SELECT ROWNUM NO, A.* FROM {1}  A WHERE ROWNUM <= {2} {4}) WHERE NO > {3} {4}", cloumnname, item.Table_Name, Catch_Max * (i + 1), Catch_Max * i, item.Where.Replace("WHERE", "AND"));
                        //sqlcode += " and " + where;
                        using (OracleConnection cn = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST=192...)(PORT=123)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=abc)));User ID=abc;Password=abc;Enlist=false;Unicode=True"))
                        {
                            cn.Open();
                            using (OracleCommand command = new OracleCommand(sqlcode, cn))
                            {
                                //4.搭配SqlCommand物件使用SqlDataReader
                                using (OracleDataReader dr = command.ExecuteReader())
                                {
                                    using (SqlConnection conn = new SqlConnection($"Data Source={Globals.ID_HOST};Initial Catalog=kmuhmis;Persist Security Info=True;User ID=sqlmis;Password=!qaz2wsx;Connection Timeout=0"))
                                    {
                                        conn.Open();
                                        SqlTransaction trans = conn.BeginTransaction();
                                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls, trans))
                                        {
                                            bulkCopy.DestinationTableName = "dbo." + item.Table_Name;
                                            bulkCopy.BulkCopyTimeout = 36000;
                                            foreach (var itemss in list)
                                            {
                                                bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(itemss.COLUMN_NAME, itemss.COLUMN_NAME));
                                            }
                                            bulkCopy.WriteToServer(dr);
                                        }
                                        trans.Commit();
                                        richTextBox1.Text += string.Concat("目前進度到" + (Catch_Max * (i + 1) >= num_sql ? num_sql : Catch_Max * (i + 1)).ToString() + "筆\n");
                                    }
                                }
                            }
                        }
                    }
                    //richTextBox1.Text += "--- success";
                }
                catch (Exception e)
                {
                    //Error1.Add(item.Table_Name, e);
                    richTextBox1.Text += e;
                }
            }

            richTextBox1.Text += "----------------------";
            //return Error1;
        }

        private void OracleTOMSSQLDATA(List<TableToDB> Data, bool big5 = false)
        {


            Dictionary<string, Exception> Error1 = new Dictionary<string, Exception>();
            // 批次取值數量
            int Catch_Max = 5;
            int T_NUM = 0;

            foreach (var item in Data)
            {
                try
                {
                    var sqlcode = "";
                    string cloumnname = "";
                    T_NUM += 1;
                    List<int> Big5_ColNum = new List<int>();
                    richTextBox1.Text += string.Concat(T_NUM.ToString(), " - ", item.Table_Name + "\n");
                    MSSQLRepository<UpDefualtModelDTO> db = new MSSQLRepository<UpDefualtModelDTO>();
                    Oracle9iRepository<ACC_TITLE> services = new Oracle9iRepository<ACC_TITLE>();
                    var num_sql = (int)services.GetAlls<dynamic>("SELECT count(*) COUNT FROM " + item.Table_Name).FirstOrDefault().COUNT;
                    var list = services.GetAlls<dynamic>(
                        " SELECT " +
                        " B.TABLE_NAME AS TABLE_NAME , B.COLUMN_NAME AS COLUMN_NAME , B.COMMENTS AS COMMENTS , A.DATA_TYPE AS DATA_TYPE ,A.DATA_LENGTH AS DATA_LENGTH , A.DATA_PRECISION AS DATA_PRECISION,A.DATA_SCALE AS DATA_SCALE , A.NULLABLE AS NULLABLE " +
                        " FROM USER_TAB_COLUMNS A ,USER_COL_COMMENTS B WHERE A.TABLE_NAME = '" + item.Table_Name + "' AND A.TABLE_NAME = B.TABLE_NAME AND A.COLUMN_NAME = B.COLUMN_NAME ORDER BY COLUMN_ID ").ToList();                    
                    
                    if (big5)
                    {
                        var TmpD = services.GetAlls<dynamic>($" SELECT * FROM {item.Table_Name} WHERE ROWNUM < 6").ToList();
                        foreach (var t in TmpD)
                        {
                            int Q = 0;
                            foreach (var tt in t)
                            {
                                if (CheckEncode(tt.Value == null ?"":tt.Value.ToString()) == true)
                                {
                                    Big5_ColNum.Add(Q);
                                }
                                Q += 1;
                            }
                        }
                        Big5_ColNum = Big5_ColNum.Distinct().ToList();
                        // RAWTOHEX
                        for (int ii =0; ii < list.Count(); ii++)
                        {                            
                            cloumnname += (Big5_ColNum.Any(x => x == ii) == true ? string.Concat("RAWTOHEX(", list[ii].COLUMN_NAME, ") ", list[ii].COLUMN_NAME) : list[ii].COLUMN_NAME) + ",";
                        }
                        cloumnname = cloumnname.Substring(0, cloumnname.Length - 1);
                    }

                    for (int i = 0; i < (int)((num_sql / Catch_Max) + 1); i++)
                    {

                        sqlcode = String.Format("SELECT {0} FROM (SELECT ROWNUM NO, A.* FROM {1}  A WHERE ROWNUM <= {2} {4}) WHERE NO > {3} {4}", cloumnname, item.Table_Name, Catch_Max * (i + 1), Catch_Max * i, item.Where.Replace("WHERE", "AND"));

                        var AE_list1 = services.GetAlls<dynamic>(sqlcode).ToList();
                        List<dynamic> AE_list = new List<dynamic>();

                        // Decode BIG5 
                        foreach (var items in AE_list1)
                        {
                            int index = 0;
                            String EnHex = "";
                            List<dynamic> tmp = new List<dynamic>();
                            foreach (var ind in items)
                            {
                                if (Big5_ColNum.Any(x => x == index) == true)
                                {
                                    string hex = ind.Value;
                                    byte[] buff = new byte[hex.Length / 2];
                                    for (int ie = 0; ie < hex.Length / 2; ie++)
                                    {
                                        buff[ie] = byte.Parse(hex.Substring(ie * 2, 2),
                                        System.Globalization.NumberStyles.AllowHexSpecifier);
                                    }
                                    // 處理KeyValuePair唯獨
                                    KeyValuePair<string, object> lKVP = new KeyValuePair<string, object>(ind.Key, Encoding.GetEncoding("big5").GetString(buff));
                                    tmp.Add(lKVP);
                                    //EnHex = Encoding.GetEncoding("big5").GetString(buff);
                                }
                                else
                                {
                                    tmp.Add(ind);
                                }
                                index += 1;
                            }
                            AE_list.Add(tmp);
                        }
                        // Decode BIG5 
                        //foreach (var items in AE_list)
                        //{
                        //    string hex = items.PT_NAME;
                        //    byte[] buff = new byte[hex.Length / 2];
                        //    for (int index0 = 0; index0 < hex.Length / 2; index0++)
                        //    {
                        //        buff[index0] = byte.Parse(hex.Substring(index0 * 2, 2),
                        //            System.Globalization.NumberStyles.AllowHexSpecifier);
                        //    }
                        //    items.PT_NAME = Encoding.GetEncoding("big5").GetString(buff);
                        //}
                        var frlp = ToDataTable(AE_list1);



                        //using (SqlConnection conn = new SqlConnection("Data Source=60...;I
                        ////using (SqlConnection conn = new SqlConnection("Data Source=192..
                        //{
                        //    conn.Open();
                        //    SqlTransaction trans = conn.BeginTransaction();
                        //    using (SqlBulkCopy bulkCopy =
                        //        new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls, trans))
                        //    {
                        //        bulkCopy.DestinationTableName = "dbo." + item.Table_Name;
                        //        bulkCopy.BulkCopyTimeout = 3600;
                        //        foreach (var itemss in list)
                        //        {
                        //            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(itemss.COLUMN_NAME, itemss.COLUMN_NAME));
                        //        }
                        //        bulkCopy.WriteToServer(ToDataTable(AE_list));
                        //    }
                        //    trans.Commit();
                        //    richTextBox1.Text += string.Concat("目前進度到" + (Catch_Max * (i + 1) >= num_sql ? num_sql : Catch_Max * (i + 1)).ToString() + "筆\n");
                        //}
                    }
                    //richTextBox1.Text += "--- success";
                }
                catch (Exception e)
                {
                    //Error1.Add(item.Table_Name, e);
                    richTextBox1.Text += e;
                }
            }

            richTextBox1.Text += "----------------------";
            //return Error1;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        private void ID_ComboBox2_TextBoxTextChanged(object sender, EventArgs e)
        {
            var Check_host = this.ID_ComboBox2.TextBoxText.ToString();
            switch (Check_host)
            {
                case "內部MSSQL":
                    Globals.ID_HOST = this.ID_To_DB1.Value;
                    break;
                case "外部MSSQL":
                    Globals.ID_HOST = this.ID_To_DB2.Value;
                    break;
                default:
                    Globals.ID_HOST = this.ID_To_DB1.Value;
                    break;
            }
        }

        private void ID_Big5_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("尚未支援!!");

            //const string message = "請確認輸入TABLE是否包含BIG5字元??";
            //const string caption = "注意!!";
            //string result = MessageBox.Show(message, caption,
            //                             MessageBoxButtons.YesNo,
            //                             MessageBoxIcon.Warning).ToString();
            //if (result == "Yes")
            //{
            //    List<TableToDB> data = new List<TableToDB>();

            //    for (int i = 0; i < dataGridView2.RowCount; i++)
            //    {
            //        List<TableToDB> tmp = new List<TableToDB>();
            //        tmp.Add(new TableToDB()
            //        {
            //            Table_Name = (dataGridView2.Rows[i].Cells[0].Value ?? "").ToString().ToUpper(),
            //            Where = (dataGridView2.Rows[i].Cells[1].Value ?? "").ToString().ToUpper()
            //        });
            //        if (tmp.FirstOrDefault().Where.Contains("WHERE") && tmp.FirstOrDefault().Where.Length > 5)
            //        {
            //            tmp.FirstOrDefault().Where = " " + tmp.FirstOrDefault().Where;
            //        }
            //        //else if (tmp.FirstOrDefault().Where.Length <= 5 && tmp.FirstOrDefault().Where.Length >= 1)
            //        //{
            //        //    MessageBox.Show("輸入無效字串!!");
            //        //    break;
            //        //}
            //        //else if (tmp.FirstOrDefault().Where.Contains("WHERE") == false)
            //        //{
            //        //    MessageBox.Show("輸入無效字串!!");
            //        //    break;
            //        //}
            //        else
            //        {
            //            if (tmp.FirstOrDefault().Table_Name != "") data.AddRange(tmp.ToList());
            //        }
            //    }

            //    OracleTOMSSQLDATA(data, true);

            //}
        }

        #endregion

        // 判断字符串中是否有中文
        static public bool CheckEncode(string srcString)
        {
            return System.Text.Encoding.UTF8.GetBytes(srcString).Length > srcString.Length;
        }


    }
}
