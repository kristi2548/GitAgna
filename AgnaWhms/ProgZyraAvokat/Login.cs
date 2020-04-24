using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration; // this namespace is add I am adding connection name in

namespace ProgZyraAvokat
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Global.localConn = ConfigurationManager.ConnectionStrings["appConn"].ToString();
            ApplicationLookAndFeel.UseTheme(this, 22);
            Global.currentDirectory = System.IO.Directory.GetCurrentDirectory();

            //txtUserName.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Global.localConn = ConfigurationManager.ConnectionStrings["appConn"].ToString();
            SqlConnection con = new SqlConnection(Global.localConn);
            DataTable tblUser = new DataTable();
            try
            {
                string uid = txtUserName.Text;
                string pass = txtPassword.Text;
               
                if (authenticate(uid,pass))
                {
                        if (Global.levizjeMagazina == null)
                        {
                            Global.loginForm = this;
                            Global.loginForm.Hide();
                            Global.levizjeMagazina = new AgnaWhms.LevizjeMagazina();
                        }
                        Global.levizjeMagazina.Show();
                }
                else
                {
                    MessageBox.Show("Username ose password i pasakte (LOKALISHT LEXO TE DREJTA)");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnLogin_Click " + ex.Message);
                //Global.encryptFile();
                lblMsg.Visible = false;
                this.Enabled = true;
            }
        }
        public bool authenticate(string username,string password)
        {
            DataTable dtblPorosiPrind = Global.returnTableForGrid(Global.localConn,
                    " select * from [dbo].[wUsers] " +
                    " where upper([username]) = upper('" + username + "') and upper([UserPIN]) = upper('" + password + "')  " ,
                    "", "Text", null, "Text");

            if (dtblPorosiPrind != null && dtblPorosiPrind.Rows.Count > 0)
            {
                Global.orderUserId = Convert.ToInt32(dtblPorosiPrind.Rows[0][0].ToString());
                return true;
            }
            else
            {
                return false;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOptimizoPerformance_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Po optimizohen te dhenat prisni mesazhin final...","Optimizim");
                IDataReader myDataRead = null;
                myDataRead = Global.callSqlCommand(Global.localConn, "DBCC SHRINKDATABASE(N'DB_APP')", "Text", "NonExecute", null);
                MessageBox.Show("Mbaroi optimizimi i programit.Tani do dilni nga programi.","Optimizimi");

                string backup = @"DECLARE @fn nvarchar(200) " +
                @" SET @fn = N'C:\FolderFileExcel\db_ap_' + CONVERT(VARCHAR(8), GETDATE(), 112) + '.bak' " +
                @" BACKUP DATABASE[DB_APP] TO DISK = @fn WITH NOFORMAT, NOINIT, NAME = N'DB_APP-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10 ";
                myDataRead = Global.callSqlCommand(Global.localConn, backup, "Text", "NonExecute", null);
                MessageBox.Show("Mbaroi backup i programit.Tani do dilni nga programi.Pas kesaj logojuni perseri","Backup");

                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim permireso shpejtesi programi," + ex.Message);
            }
        }
    }
}
