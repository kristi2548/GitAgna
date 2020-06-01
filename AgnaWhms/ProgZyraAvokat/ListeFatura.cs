using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using ProgZyraAvokat;

namespace AgnaWhms
{
    public partial class ListeFatura : Form
    {

        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private BindingSource bindingSource1 = new BindingSource();
        public bool contMainMenuOpen = false;
        public ListeFatura()
        {
            try
            {
                Global.localConn = ConfigurationManager.ConnectionStrings["appConn"].ToString();

                InitializeComponent();

                //timer1.Enabled = true;
                //timer1.Start();

                //Global.timerGlobal.Enabled = true;
                //Global.timerGlobal.Start();
                //Global.timerGlobal.Interval = 10;
                //Global.timerGlobal.Tick += new System.EventHandler(Global.timer1_Tick);

                Global.currentDirectory = ConfigurationManager.AppSettings["currentDirectory"];
                Global.currentDirectory = Directory.GetCurrentDirectory();
                //Global.currentDirectory = System.IO.Directory.GetCurrentDirectory();
                Color formBackColorAll2 = ColorTranslator.FromHtml("#FFFFFF"); //ColorTranslator.FromHtml("#424242");
                Color lblForeColor2 = ColorTranslator.FromHtml("#4655A5");
                Color buttonForeColor2 = ColorTranslator.FromHtml("#4655A5");//#929BC8
                Color buttonBackColor2 = ColorTranslator.FromHtml("#F4F4F4");
                ApplicationLookAndFeel.UseThemeSido(this, 10, formBackColorAll2, lblForeColor2, buttonForeColor2, buttonBackColor2);
                int buttonFontSize = 10;
                //ApplicationLookAndFeel.UseTheme_Buttons(this, buttonFontSize);

                callGridUpdate("");
                //callGridAlert("");
                setAlertLabelStyle(buttonFontSize);

                buttonStyle(buttonFontSize);

                this.ControlBox = true;
                this.MinimizeBox = true;
                this.MaximizeBox = true;
            }
            catch (Exception EX)
            {
                MessageBox.Show("ListeCeshtje " + EX.Message);
            }
        }
        public void buttonStyle(int buttonFontSize)
        {
            btnListeCeshtje.Font = new Font("Century Gothic", buttonFontSize);
            btnMenuAll.Font = new Font("Century Gothic", buttonFontSize);
            btnCeshtjeREE.Font = new Font("Century Gothic", buttonFontSize);
            btnRaporte.Font = new Font("Century Gothic", buttonFontSize);
            btnDil.Font = new Font("Century Gothic", buttonFontSize);
            btnShtoFurnizim.Font = new Font("Century Gothic", buttonFontSize);
            btnVendosRaft.Font = new Font("Century Gothic", buttonFontSize);

            btnListeCeshtje.BackColor = ColorTranslator.FromHtml("#4655A5");
            btnListeCeshtje.ForeColor = ColorTranslator.FromHtml("#FFFFFF");

            btnBackNew.BackColor = ColorTranslator.FromHtml("#4655A5");
            btnBackNew.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            btnNextNew.BackColor = ColorTranslator.FromHtml("#4655A5");
            btnNextNew.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
        }
        public void setAlertLabelStyle(int buttonFontSize)
        {
            lblAlertKerko.Font = new Font("Century Gothic", buttonFontSize);
            lblCeshtjeKerko.Font = new Font("Century Gothic", buttonFontSize);

            Color labelBackColor = ColorTranslator.FromHtml("#F8B490"); //Color.White;;
            Color labelForeColor = ColorTranslator.FromHtml("#070B10"); //Color.White;;

            lblAlert1.BackColor = labelBackColor;
            lblAlert1.ForeColor = labelForeColor;
            lblAlert1.Font = new Font("Century Gothic", 9);
            lblAlert2.BackColor = labelBackColor;
            lblAlert2.ForeColor = labelForeColor;
            lblAlert2.Font = new Font("Century Gothic", 9);
            lblAlert3.BackColor = labelBackColor;
            lblAlert3.ForeColor = labelForeColor;
            lblAlert3.Font = new Font("Century Gothic", 9);
            lblAlert4.BackColor = labelBackColor;
            lblAlert4.ForeColor = labelForeColor;
            lblAlert4.Font = new Font("Century Gothic", 9);
            lblAlert5.BackColor = labelBackColor;
            lblAlert5.ForeColor = labelForeColor;
            lblAlert5.Font = new Font("Century Gothic", 9);
            lblAlert6.BackColor = labelBackColor;
            lblAlert6.ForeColor = labelForeColor;
            lblAlert6.Font = new Font("Century Gothic", 9);
            lblAlert7.BackColor = labelBackColor;
            lblAlert7.ForeColor = labelForeColor;
            lblAlert7.Font = new Font("Century Gothic", 9);
            lblAlert8.BackColor = labelBackColor;
            lblAlert8.ForeColor = labelForeColor;
            lblAlert8.Font = new Font("Century Gothic", 9);

            lblAlert9.BackColor = labelBackColor;
            lblAlert9.ForeColor = labelForeColor;
            lblAlert9.Font = new Font("Century Gothic", 9);
            lblAlert10.BackColor = labelBackColor;
            lblAlert10.ForeColor = labelForeColor;
            lblAlert10.Font = new Font("Century Gothic", 9);
            lblAlert11.BackColor = labelBackColor;
            lblAlert11.ForeColor = labelForeColor;
            lblAlert11.Font = new Font("Century Gothic", 9);
            lblAlert12.BackColor = labelBackColor;
            lblAlert12.ForeColor = labelForeColor;
            lblAlert12.Font = new Font("Century Gothic", 9);
        }
        public void menyKryesoreShowHideEvent()
        {
            try
            {
                if (contMainMenuOpen)
                {
                    contMainMenu.Hide();
                    contMainMenuOpen = false;
                }
                else
                {
                    contMainMenuOpen = true;
                    //Button btnSender = (Button)sender;
                    //AgnaOrders.menuButton btnSender = (AgnaOrders.menuButton)Global.klienteForm.Controls.Find("btnMenuAll", true)[0];
                    Button btnSender = this.Controls.Find("btnMenuAll", true)[0] as Button;
                    Point ptLowerLeft = new Point(0, btnSender.Height);
                    ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
                    contMainMenu.Show(ptLowerLeft);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim menyKryesoreShowHideEvent " + ex.Message);
            }
        }
        public void callGridAlert(string txtKerko)
        {
            try
            {
                DataTable myDtTable = null;
                //string selectCommand = "SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID],a.[AKTIV],[KOMENTE],[KID],[LLVID] " +
                //" FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID  " +
                //" where KALLEZUES like '%" + txtKerko + "%' or IKALLEZUAR like '%" + txtKerko + "%' or FABUL like '%" + txtKerko + "%' or NENI like '%" + txtKerko + "%'";

                string filter;
                if (txtKerko == "") { filter = "'%%'"; }
                else { filter = "'%" + txtKerko.ToUpper() + "%'"; }
                int rreshtFill, rreshtMbarim;
                int ceshtjePerFaqe = 15;
                rreshtFill = ((Global.kallezimePageNr - 1) * ceshtjePerFaqe) + 1;
                rreshtMbarim = (Global.kallezimePageNr) * ceshtjePerFaqe;
                string selectCommand = " SELECT * " +
                " FROM (" +
                " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,DATE_ALERTI,KOMENTE AS ALERTI," +
                " b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA, " +
                " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 1 and a.aktiv = 1 " +
                "  and DATE_ALERTI >= ([dbo].[ufn_GetDateOnlyUs](GETDATE())) " +
                "  and DATE_ALERTI <= ([dbo].[ufn_GetDateOnlyUs](GETDATE() + 2)) " +
                " ) AS MyDerivedTable" +
                " WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                " (UPPER(KALLEZUES) like " + filter + " or UPPER(IKALLEZUAR) like " + filter + " or UPPER(FABUL) like " + filter +
                " or UPPER(NENI) like " + filter + " or UPPER(ALERT) like " + filter + ")" +
                " order by N asc";

                myDtTable = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text"); //Global.fillGridWithRef(ref dgAlert, Global.localConn, selectCommand, "", "Text");
                //myDtTable = Global.re(ref dgAlert, Global.localConn, selectCommand, "", "Text");
                int nrRow = 1;
                if (myDtTable != null)
                {
                    foreach (DataRow row in myDtTable.Rows)
                    {
                        if (nrRow == 1) { lblAlert1.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 2) { lblAlert2.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 3) { lblAlert3.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 4) { lblAlert4.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 5) { lblAlert5.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 6) { lblAlert6.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 7) { lblAlert7.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 8) { lblAlert8.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }

                        if (nrRow == 9) { lblAlert9.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 10) { lblAlert10.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 11) { lblAlert11.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        if (nrRow == 12) { lblAlert12.Text = "Njoftim " + nrRow.ToString() + ":" + row["DATE_ALERTI"] + " \n" + row["ALERT"]; }
                        nrRow = nrRow + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        public void callGridUpdate(string txtKerko)
        {
            try
            {
                int rreshtFill, rreshtMbarim;
                Global.ceshtjePerFaqe = 15;
                string filter;
                rreshtFill = ((Global.kallezimePageNr - 1) * Global.ceshtjePerFaqe) + 1;
                rreshtMbarim = (Global.kallezimePageNr) * Global.ceshtjePerFaqe;
                if (txtKerko == "") { filter = "'%%'"; }
                else { filter = "'%" + txtKerko.ToUpper() + "%'"; }

                string selectCommand = " SELECT [MovHeadID] ,[OrderID],[ConsumerID] ,[AreaID],[MovStatusID] ,[WarehouseID] ,[UserID]  ,[MovCatID], " +
                " [MovCatCode],[MovCatName] as Levizje,[RoleID],[WarehouseName] as Magazina,[WarehouseCode],[MovStatusName] as Status,[MovHeadTime] as Date,[MovHeadNr] as Nr,[AreaCode],[AreaName] as Zona, " +
                " [ConsumerMobNr] as NrTel ,[ConsumerName] as Kons,[ConsumerAddress] as Adresa ,[OrderNr] as Nr ,[PaymentInfo] as Pagesa ,[VAT_Nr] AS Tvsh,[OrderNetTotal] as Vlera " +
                " FROM [order_for_grid_ListeFatura] " +
                " WHERE " + 
                //" MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                //" MyDerivedTable.aktiv = 1 and " + 
                " (  upper(MovHeadNr) like " + filter + " or upper(ConsumerName) like " + filter +
                " or upper(ConsumerAddress) like " + filter +
                " or upper(ConsumerMobNr) like " + filter +
                " or upper(CONVERT(varchar, [MovHeadTime],103)) like " + filter +
                " or upper(OrderNr) like " + filter + " or upper(VAT_Nr) like " + filter + ")" +
                " order by MovHeadTime desc";

                string selectCommandTotal = " SELECT * " +
                " FROM (" +
                " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA,DATE_ALERTI,KOMENTE AS ALERTI, " +
                " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 1 and a.aktiv = 1 " +
                " ) AS MyDerivedTable" +
                " WHERE " +
                //" MyDerivedTable.aktiv = 1 and " + 
                " (upper(KALLEZUES) like " + filter + " or upper(IKALLEZUAR) like " + filter +
                " or upper(FABUL) like " + filter +
                " or upper(STATUS) like " + filter +
                " or upper(CONVERT(varchar, [DATA],103)) like " + filter +
                " or upper(NENI) like " + filter + " or upper(alert) like " + filter + ")" +
                " order by N asc";

                //fillGrid( Global.localConn,selectCommand, "", "Text");
                //DataTable myDtTable2 = Global.fillCeshtjeNr(ref dgListeVeprime, Global.localConn, selectCommandTotal, "", "Text");
                DataTable myDtTable = Global.fillGridWithRef(ref dgListeVeprime, Global.localConn, selectCommand, "", "Text");

                dgListeVeprime.Columns["MovHeadID"].Visible = false;
                dgListeVeprime.Columns["OrderID"].Visible = false;
                dgListeVeprime.Columns["ConsumerID"].Visible = false;
                dgListeVeprime.Columns["AreaID"].Visible = false;

                dgListeVeprime.Columns["MovStatusID"].Visible = false;
                dgListeVeprime.Columns["WarehouseID"].Visible = false;
                dgListeVeprime.Columns["UserID"].Visible = false;
                dgListeVeprime.Columns["MovCatID"].Visible = false;
                dgListeVeprime.Columns["RoleID"].Visible = false;
                dgListeVeprime.Columns["WarehouseCode"].Visible = false;
                dgListeVeprime.Columns["AreaCode"].Visible = false;
                dgListeVeprime.Columns["WarehouseCode"].Visible = false;
                dgListeVeprime.Columns["MovCatCode"].Visible = false;
                //dgListeVeprime.Columns["PaymentInfo"].Visible = false;
                dgListeVeprime.Columns["Adresa"].Visible = false;
                dgListeVeprime.Columns["WarehouseCode"].Visible = false;


                Global.addButtonToGridWithRef(ref dgListeVeprime, "Lexo", 25);
                Global.addButtonToGridWithRef(ref dgListeVeprime, "Edit", 26);
                Global.addButtonToGridWithRef(ref dgListeVeprime, "Fshi", 27);

                dgListeVeprime.ReadOnly = true;

                //callGridAlert(txtKerko);

                txtCeshtjeKerko.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        private void dgListeVeprime_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgListeVeprime.Columns["Fshi"].Index)
                {
                    if (dgListeVeprime.SelectedCells.Count > 0)
                    {
                        int selectedrowindex = dgListeVeprime.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgListeVeprime.Rows[selectedrowindex];
                        Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["Kid"].Value.ToString());
                        //Global.veprimKallezim.fillVeprimById(Global.idVeprimi.ToString());
                        Global.callSqlCommand(Global.localConn, "update veprim set aktiv = 0 where kid = '" + Global.idVeprimi + "'", "Text", "Execute", null);
                    }
                    MessageBox.Show("Ceshtja u fshi", "Fshi Ceshtje");
                    callGridUpdate(txtCeshtjeKerko.Text);
                    callGridAlert("");
                }
                else if (e.ColumnIndex == dgListeVeprime.Columns["Edit"].Index)
                {
                    if (dgListeVeprime.SelectedCells.Count > 0)
                    {

                        int selectedrowindex = dgListeVeprime.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgListeVeprime.Rows[selectedrowindex];
                        Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["Kid"].Value.ToString());
                    }
                }
                else if (e.ColumnIndex == dgListeVeprime.Columns["Lexo"].Index)
                {
                    if (dgListeVeprime.SelectedCells.Count > 0)
                    {
                        int selectedrowindex = dgListeVeprime.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgListeVeprime.Rows[selectedrowindex];
                        Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["Kid"].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hap kallezim err " + ex.Message);
            }

        }
        public void exportToExcel(string txtKerko, string selectCommand)
        {
            try
            {
                txtKerko = txtCeshtjeKerko.Text;
                selectCommand = "SELECT " +
                " KALLEZIME.LLOJ_VEPRIMI, KALLEZIME.DATE AS DATE_KALLEZIMI,KALLEZIME.NR AS NR_KALLEZIMI," +
                " KALLEZIME.KALLEZUES, KALLEZIME.IKALLEZUAR, KALLEZIME.LLOJ_VEPRIMI_TRUPI," +
                "  KALLEZIME.KID, KALLEZIME.DATE_VEPRIMI, " +
                "  KALLEZIME.NR_PROTOKOLLI, KALLEZIME.LENDA, KALLEZIME.DERGUESI," +
                "  KALLEZIME.MARRESI, KALLEZIME.FABUL, " +
                " KALLEZIME.LLOJI_AKT_PROCEDURIAL," +
                " PROCEDIME_PENALE.* " +
                " FROM KALLEZIME LEFT OUTER JOIN" +
                "      PROCEDIME_PENALE ON KALLEZIME.KID = PROCEDIME_PENALE.PRIND_KID" +
                " where KALLEZIME.KALLEZUES like '%" + txtKerko + "%' or KALLEZIME.IKALLEZUAR like '%" + txtKerko + "%'  " +
                " or KALLEZIME.FABUL like '%" + txtKerko + "%' or  KALLEZIME.NENI like '%" + txtKerko + "%' or KALLEZIME.NR like '%" + txtKerko + "%'" +
                " ORDER BY KALLEZIME.DATE_VEPRIMI DESC";

                DataTable myDataTbl = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");

                Global.ExportToExcel(myDataTbl, Global.currentDirectory + "listeKallezime" + Global.ktheDateOre() + ".xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        public void fillGrid(string myConnectionString, string selectCommand,
           string furnitor, string kod)
        {
            try
            {
                dgListeVeprime.DataSource = bindingSource1;
                bindingSource1.DataSource = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                dgListeVeprime.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                dgListeVeprime.ClearSelection();

                dgListeVeprime.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgListeVeprime.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgListeVeprime.RowTemplate.Height = 30;
                dgListeVeprime.ForeColor = ColorTranslator.FromHtml("#4655A5");
                dgListeVeprime.BackgroundColor = ColorTranslator.FromHtml("#FFFFFF"); //Color.White;;
                dgListeVeprime.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFFFFF"); //Color.White;;
                dgListeVeprime.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgListeVeprime.RowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFFFFF"); //Color.White;;

                dgListeVeprime.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#4655A5");// Color.DodgerBlue;
                dgListeVeprime.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#FFFFFF"); //Color.White;
                dgListeVeprime.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgListeVeprime.EnableHeadersVisualStyles = false;

                dgListeVeprime.RowHeadersVisible = false;
                dgListeVeprime.ClearSelection();
                dgListeVeprime.ReadOnly = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
            }
        }
        public void ceshtjeERe()
        {
            try
            {

                //menuButton btnSender = (menuButton)Global.listeCeshtje.Controls.Find("btnShtoCeshtje", true)[0];
                //Point ptLowerLeft = new Point(0, btnSender.Height);
                //ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
                //contPorosiTirana.Show(ptLowerLeft);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim porosiEReEvent " + ex.Message);
            }
        }
        private void btnCeshtje_Click(object sender, EventArgs e)
        {
            //Form1 form1 = new ProgZyraAvokat.Form1();
            //form1.Show();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMenuAll_Click(object sender, EventArgs e)
        {
            menyKryesoreShowHideEvent();
        }

        private void txtKlientiKerko_TextChanged(object sender, EventArgs e)
        {
            callGridUpdate(txtCeshtjeKerko.Text);
        }

        private void dgListeVeprime_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hap kallezim err " + ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            exportToExcel("", "");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //String dateTani = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                //if ((System.DateTime.Now.Minute % 1) == 0 && System.DateTime.Now.Second == 35)//
                //{
                //    DataTable myDtTbl = Global.returnTableForGrid(Global.localConn,
                //    "SELECT KID, KOMENTE as ALERT, DATE_ALERTI FROM VEPRIM where komente <> '' and AKTIV = 1 and " +
                //    " dbo.ufn_GetDateOnly_Full(DATE_ALERTI) = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "'", //'01/04/2020 19:02:32', 
                //    "Text", "Execute", null, "Text");
                //    if (myDtTbl != null && myDtTbl.Rows.Count > 0)
                //    {
                //        if (Global.myNotification == null)
                //        {
                //            Global.myNotification = new notification();
                //        }
                //        if (Global.myNotification.IsDisposed)
                //        {
                //            Global.myNotification = new notification();
                //        }
                //        Global.myNotification.setText(myDtTbl.Rows[0]["DATE_ALERTI"].ToString() + ":" +  myDtTbl.Rows[0]["ALERT"].ToString());
                //        Global.myNotification.Show();
                //    }
                //}
            }
            catch (Exception EX)
            {
                MessageBox.Show("Gabim timer1_Tick " + EX.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Global.kallezimePageNr = Global.kallezimePageNr + 1;
            //callGridUpdate("");
            int NrPages = (Global.nrCeshtjeSipasFilter / Global.ceshtjePerFaqe);//Global.ceshtjePerFaqe = 15
            int mod = (Global.nrCeshtjeSipasFilter % Global.ceshtjePerFaqe);
            if (mod > 0) { NrPages = NrPages + 1; }
            if (Global.kallezimePageNr + 1 <= NrPages)
            {
                Global.kallezimePageNr = Global.kallezimePageNr + 1;
                callGridUpdate("");
            }
            else
            {
                MessageBox.Show("Nuk ka ceshtje te tjera", "Liste Ceshtje");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Global.kallezimePageNr - 1 > 0)
            {
                Global.kallezimePageNr = Global.kallezimePageNr - 1;
                callGridUpdate("");
            }
            else
            {
                MessageBox.Show("Keni arritur tek fillimi i ceshtjeve ,nuk mund te vazhdoni me tej .", "Fillim Ceshtje");
            }
        }


        private void btnDil_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            callGridAlert(txtCeshtjeKerko.Text);
        }

        private void lblAlert4_Click(object sender, EventArgs e)
        {

        }

        private void txtCeshtjeKerko_Enter(object sender, EventArgs e)
        {
            txtCeshtjeKerko.Text = "";
        }

        private void ListeKallezime_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCeshtjeKerko_TextChanged(object sender, EventArgs e)
        {
            callGridUpdate(txtCeshtjeKerko.Text);
        }

        private void btnCeshtjeREE_Click(object sender, EventArgs e)
        {
            if (Global.levizjeMagazina == null)
            {
                Global.listeFatura.Hide();
                Global.levizjeMagazina = new AgnaWhms.LevizjeMagazina();
            }
            Global.levizjeMagazina.levizjeMagazinaInit();
            Global.levizjeMagazina.Show();
        }

        private void dgListeVeprime_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgListeVeprime.Columns["Fshi"].Index)
                {
                  
                }
                else if (e.ColumnIndex == dgListeVeprime.Columns["Edit"].Index)
                {
                    if (dgListeVeprime.SelectedCells.Count > 0)
                    {

                        int selectedrowindex = dgListeVeprime.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgListeVeprime.Rows[selectedrowindex];
                        Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["MovHeadID"].Value.ToString());
                        if (Global.levizjeMagazina == null)
                        {
                            Global.listeFatura.Hide();
                            Global.levizjeMagazina = new AgnaWhms.LevizjeMagazina();
                        }
                        Global.levizjeMagazina.fillKokeVeprimById(Global.idVeprimi.ToString());
                        Global.levizjeMagazina.Show();
                    }
                }
                else if (e.ColumnIndex == dgListeVeprime.Columns["Lexo"].Index)
                {
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hap kallezim err " + ex.Message);
            }
        }

        private void btnRaporte_Click_1(object sender, EventArgs e)
        {
            if (Global.formeStoku == null)
            {
                Global.listeFatura.Hide();
                Global.formeStoku = new AgnaWhms.FormeStoku ();
            }
            Global.formeStoku.initStok();
            Global.formeStoku.Show();
        }

        private void btnShtoFurnizim_Click(object sender, EventArgs e)
        {
            if (Global.furnizim == null)
            {
                Global.listeFatura.Hide();
                Global.furnizim = new AgnaWhms.Furnizim();
            }
            Global.furnizim.levizjeMagazinaInit();
            Global.furnizim.Show();
        }

        private void btnVendosRaft_Click(object sender, EventArgs e)
        {
            if (Global.rafte == null)
            {
                Global.listeFatura.Hide();
                Global.rafte = new AgnaWhms.Rafte ();
            }
            Global.rafte.levizjeMagazinaInit();
            Global.rafte.Show();
        }
    }
}
