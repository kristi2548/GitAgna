using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgZyraAvokat
{
    public partial class All_Info : Form
    {
        #region declaration
        private BindingSource bindingSourceShkrese = new BindingSource();
        private BindingSource bindingSourceAkte = new BindingSource();

        private BindingSource bindingSourceShkresePp = new BindingSource();
        private BindingSource bindingSourceAktePp = new BindingSource();
        #endregion

        public All_Info()
        {
            InitializeComponent();
            Global.fillCombo(ref cmbLlojVeprimi, Global.localConn, "SELECT [ID],[LLOJ_VEPRIMI] FROM [dbo].[LLOJ_VEPRIMI] where AKTIV = 1 ORDER BY ID ASC", "LLOJ_VEPRIMI", "ID");
            cmbLlojVeprimi.SelectedIndex = 0;
            cmbLlojVeprimi.Enabled = false;
            ApplicationLookAndFeel.UseTheme(this, 10);
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";

            dtpAlert.Format = DateTimePickerFormat.Custom;
            dtpAlert.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            if (Global.idVeprimi == 0)
            {
                boshatis();
            }
        }

        #region Menus
        public void fushaValidate()
        {
            if (Global.idVeprimi == 0)
            {
                boshatis();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Global.listeCeshtje == null)
            {
                Global.veprimKallezim.Hide();
                Global.listeCeshtje = new ListeKallezime();
                Global.listeCeshtje.callGridUpdate("");
                Global.listeCeshtje.Show();
            }
            else
            {
                Global.veprimKallezim.Hide();
                Global.listeCeshtje.callGridUpdate("");
                Global.listeCeshtje.Show();
            }
        }
        #endregion
        public void boshatis()
        {
            txtFabul.Text = "";
            txtIKallezuari.Text = "";
            txtKallezues.Text = "";
            txtNr.Text = "";
            cmbLlojVeprimi.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;// DateTime.Today.AddDays(-1);
            dtpAlert.Value = DateTime.Today;// DateTime.Today.AddDays(-1);
            chkAktiv.Checked = true;
            txtAlert.Text = "";
            txtNr.Focus();
        }
        public void fillVeprimById(string idVeprimi)
        {
            try
            {
                DataTable dtTblKallezim = Global.returnTableForGrid(Global.localConn,
                    "SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID]," +
                " a.[AKTIV],[KOMENTE],[KID],[LLVID],a.aktiv,date_alerti,status " +
                " FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID where a.kid = " + Global.idVeprimi.ToString(),
                "", "Text", null, "Text");

                if (dtTblKallezim != null && dtTblKallezim.Rows.Count > 0)
                {
                    txtNr.Text = dtTblKallezim.Rows[0][1].ToString();
                    dtpDate.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][2].ToString());// DateTime.Today.AddDays(-1);
                    txtKallezues.Text = dtTblKallezim.Rows[0][4].ToString();
                    txtIKallezuari.Text = dtTblKallezim.Rows[0][5].ToString();
                    txtFabul.Text = dtTblKallezim.Rows[0][6].ToString();
                    txtAlert.Text = dtTblKallezim.Rows[0][10].ToString();
                    cmbLlojVeprimi.SelectedValue = dtTblKallezim.Rows[0][12].ToString();
                    if (dtTblKallezim.Rows[0][13].ToString() == "1") { chkAktiv.Checked = true; } else { chkAktiv.Checked = false; }
                    if (Global.CheckDate(dtTblKallezim.Rows[0][12].ToString()))
                    {
                        dtpAlert.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][14].ToString());// DateTime.Today.AddDays(-1);
                    }
                    txtStatus.Text = dtTblKallezim.Rows[0][15].ToString();
                }

               

                callGridAkte();
                callGridShkresa();
                fillProcedimPenal_ByKallezimId(Global.idTrupVeprimi.ToString());
                callGridShkresePpc();
                callGridAktePp();

                mbushProkData(Global.idVeprimiProcedurePenale.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Err fillVeprimById " + ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Global.idTrupVeprimi = 0;
                if (Global.idVeprimi == 0)
                {
                    MessageBox.Show("Rregjistroni fillimisht kallezimin ,pastaj mund te shtoni Procedim Penal !");
                }
                else
                {
                    //Global.idVeprimi = 0;
                    if (Global.veprimProcedimPenal == null)
                    {
                        Global.veprimProcedimPenal = new VeprimProcedimPenal();
                    }

                    Global.veprimKallezim.Hide();
                    if (Global.listeCeshtje != null)
                    {
                        Global.listeCeshtje.Hide();
                    }
                    Global.veprimProcedimPenal.fillProcedimPenal_ByKallezimId(Global.idVeprimi.ToString());
                    Global.veprimProcedimPenal.Show();
                    Global.veprimProcedimPenal.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Faza 1 " + ex.Message);
                Log.LogData("rregjistrimKallezim_Click", ex.Message);
            }
        }

        #region grida Veprim Kallezim
        public void callGridShkresa()
        {
            try
            {
                fillGridShkresa(Global.localConn,
                "SELECT  TRUP_VEPRIMI.NR_PROTOKOLLI, TRUP_VEPRIMI.DERGUESI, TRUP_VEPRIMI.MARRESI, " +
                 " TRUP_VEPRIMI.LLOJI_AKT_PROCEDURIAL, LLOJ_TRUP_VEPRIMI.LLOJ_VEPRIMI_TRUPI,VEPRIM.KID,TRUP_VEPRIMI.TID" +
                 " FROM VEPRIM INNER JOIN" +
                 " TRUP_VEPRIMI ON VEPRIM.KID = TRUP_VEPRIMI.KID INNER JOIN" +
                 " LLOJ_TRUP_VEPRIMI ON TRUP_VEPRIMI.LLTVID = LLOJ_TRUP_VEPRIMI.TVID where VEPRIM.kid = " + Global.idVeprimi.ToString() +
                 " AND TRUP_VEPRIMI.AKTIV = 1 and LLTVID = 1",
                "", "Text");

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgCeshtje.Columns["KID"].Visible = false;
                dgCeshtje.Columns["TID"].Visible = false;

                dgCeshtje.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgCeshtje.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgCeshtje.RowTemplate.Height = 30;
                dgCeshtje.ForeColor = lblForeColor12;
                dgCeshtje.BackgroundColor = formBackColorAll;
                dgCeshtje.Font = new Font("Century Gothic", 9);
                dgCeshtje.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                dgCeshtje.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgCeshtje.RowsDefaultCellStyle.BackColor = formBackColorAll;

                //dgCeshtje.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgCeshtje.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        public void fillGridShkresa(string myConnectionString, string selectCommand,
           string furnitor, string kod)
        {
            try
            {
                dgCeshtje.DataSource = bindingSourceShkrese;
                bindingSourceShkrese.DataSource = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                dgCeshtje.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
            }
        }

        public void callGridAkte()
        {
            try
            {
                fillGridAkte(Global.localConn,
                "SELECT  dbo.TRUP_VEPRIMI.NR_PROTOKOLLI, dbo.TRUP_VEPRIMI.DERGUESI, dbo.TRUP_VEPRIMI.MARRESI, " +
                 " dbo.TRUP_VEPRIMI.LLOJI_AKT_PROCEDURIAL, dbo.LLOJ_TRUP_VEPRIMI.LLOJ_VEPRIMI_TRUPI,dbo.VEPRIM.KID,TRUP_VEPRIMI.TID" +
                 " FROM dbo.VEPRIM INNER JOIN" +
                 " dbo.TRUP_VEPRIMI ON dbo.VEPRIM.KID = dbo.TRUP_VEPRIMI.KID INNER JOIN" +
                 " dbo.LLOJ_TRUP_VEPRIMI ON dbo.TRUP_VEPRIMI.LLTVID = dbo.LLOJ_TRUP_VEPRIMI.TVID where VEPRIM.kid = " + Global.idVeprimi.ToString() +
                 " AND TRUP_VEPRIMI.AKTIV = 1 and LLTVID =  2 ",
                "", "Text");

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgAkte.Columns["KID"].Visible = false;
                dgAkte.Columns["TID"].Visible = false;
                dgAkte.Columns["DERGUESI"].Visible = false;
                dgAkte.Columns["MARRESI"].Visible = false;

                dgAkte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgAkte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgAkte.RowTemplate.Height = 30;
                dgAkte.ForeColor = lblForeColor12;
                dgAkte.BackgroundColor = formBackColorAll;
                dgAkte.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                dgAkte.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgAkte.Font = new Font("Century Gothic", 9);
                dgAkte.RowsDefaultCellStyle.BackColor = formBackColorAll;

                //dgAkte.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgAkte.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        public void fillGridAkte(string myConnectionString, string selectCommand,
           string furnitor, string kod)
        {
            try
            {
                dgAkte.DataSource = bindingSourceAkte;
                bindingSourceAkte.DataSource = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                dgAkte.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
            }
        }
        #endregion

        #region Procedim Penal
        public void fillProcedimPenal_ByKallezimId(string idVeprimi)
        {
            try
            {
                DataTable dtTblKallezim = Global.returnTableForGrid(Global.localConn,
                    "SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID]," +
                " a.[AKTIV],[KOMENTE],[KID],[LLVID],A.AKTIV " +
                " FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID where a.PRIND_KID = " + Global.idVeprimi.ToString(),
                "", "Text", null, "Text");
                dtpDatePP.Format = DateTimePickerFormat.Custom;
                dtpAlertPP.Format = DateTimePickerFormat.Custom;

                dtpDatePP.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
                dtpAlertPP.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
                if (dtTblKallezim != null && dtTblKallezim.Rows.Count > 0)
                {
                    txtNrPP.Text = dtTblKallezim.Rows[0][1].ToString();
                    dtpDatePP.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][2].ToString());// DateTime.Today.AddDays(-1);
                    txtKallezuesPp.Text = dtTblKallezim.Rows[0][4].ToString();
                    txtIKallezuariPP.Text = dtTblKallezim.Rows[0][5].ToString();
                    txtFabulPP.Text = dtTblKallezim.Rows[0][6].ToString();
                    Global.idVeprimiProcedurePenale = Convert.ToInt32(dtTblKallezim.Rows[0][11].ToString());
                    cmbLlojVeprimiPP.SelectedValue = dtTblKallezim.Rows[0][12].ToString();
                    if (dtTblKallezim.Rows[0][13].ToString() == "1") { chkAktivPP.Checked = true; } else { chkAktivPP.Checked = false; }
                }

                Global.fillCombo(ref cmbLlojVeprimiPP, Global.localConn, "SELECT [ID],[LLOJ_VEPRIMI] FROM [dbo].[LLOJ_VEPRIMI] where AKTIV = 1  ORDER BY ID ASC", "LLOJ_VEPRIMI", "ID");
                cmbLlojVeprimiPP.SelectedIndex = 1;
                cmbLlojVeprimiPP.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err fillProcedimPenal_ByKallezimId " + ex.Message);
            }
        }

        #region shkresePp
        public void callGridShkresePpc()
        {
            try
            {
                fillGrideShkresePp(Global.localConn,
                "SELECT  TRUP_VEPRIMI.NR_PROTOKOLLI, TRUP_VEPRIMI.DERGUESI, TRUP_VEPRIMI.MARRESI, " +
                 " TRUP_VEPRIMI.LLOJI_AKT_PROCEDURIAL, LLOJ_TRUP_VEPRIMI.LLOJ_VEPRIMI_TRUPI,VEPRIM.KID,TRUP_VEPRIMI.TID" +
                 " FROM VEPRIM INNER JOIN" +
                 " TRUP_VEPRIMI ON VEPRIM.KID = TRUP_VEPRIMI.KID INNER JOIN" +
                 " LLOJ_TRUP_VEPRIMI ON TRUP_VEPRIMI.LLTVID = LLOJ_TRUP_VEPRIMI.TVID where VEPRIM.kid = " + Global.idVeprimiProcedurePenale.ToString() +
                 " AND TRUP_VEPRIMI.AKTIV = 1 and LLTVID = 1",
                "", "Text");

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgCeshtjePp.Columns["KID"].Visible = false;
                dgCeshtjePp.Columns["TID"].Visible = false;

                dgCeshtjePp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgCeshtjePp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgCeshtjePp.RowTemplate.Height = 30;
                dgCeshtjePp.Font = new Font("Century Gothic", 9);
                dgCeshtjePp.ForeColor = lblForeColor12;
                dgCeshtjePp.BackgroundColor = formBackColorAll;
                dgCeshtjePp.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                dgCeshtjePp.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgCeshtjePp.RowsDefaultCellStyle.BackColor = formBackColorAll;

                //dgCeshtjePp.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgCeshtjePp.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        public void fillGrideShkresePp(string myConnectionString, string selectCommand,
           string furnitor, string kod)
        {
            try
            {
                dgCeshtjePp.DataSource = bindingSourceShkresePp;
                bindingSourceShkresePp.DataSource = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                dgCeshtjePp.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
            }
        }
        #endregion

        #region AktePp
        public void callGridAktePp()
        {
            try
            {
                fillGridAktePp(Global.localConn,
                "SELECT  dbo.TRUP_VEPRIMI.NR_PROTOKOLLI, dbo.TRUP_VEPRIMI.DERGUESI, dbo.TRUP_VEPRIMI.MARRESI, " +
                 " dbo.TRUP_VEPRIMI.LLOJI_AKT_PROCEDURIAL, dbo.LLOJ_TRUP_VEPRIMI.LLOJ_VEPRIMI_TRUPI,dbo.VEPRIM.KID,TRUP_VEPRIMI.TID" +
                 " FROM dbo.VEPRIM INNER JOIN" +
                 " dbo.TRUP_VEPRIMI ON dbo.VEPRIM.KID = dbo.TRUP_VEPRIMI.KID INNER JOIN" +
                 " dbo.LLOJ_TRUP_VEPRIMI ON dbo.TRUP_VEPRIMI.LLTVID = dbo.LLOJ_TRUP_VEPRIMI.TVID where VEPRIM.kid = " + Global.idVeprimiProcedurePenale.ToString() +
                 " AND TRUP_VEPRIMI.AKTIV = 1 and LLTVID = 2",
                "", "Text");

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgAktePp.Columns["KID"].Visible = false;
                dgAktePp.Columns["TID"].Visible = false;
                dgAktePp.Columns["DERGUESI"].Visible = false;
                dgAktePp.Columns["MARRESI"].Visible = false;

                dgAktePp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgAktePp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgAktePp.RowTemplate.Height = 30;
                dgAktePp.Font = new Font("Century Gothic", 9);
                dgAktePp.ForeColor = lblForeColor12;
                dgAktePp.BackgroundColor = formBackColorAll;
                dgAktePp.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                dgAktePp.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgAktePp.RowsDefaultCellStyle.BackColor = formBackColorAll;
                //dgAktePp.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgAktePp.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        public void fillGridAktePp(string myConnectionString, string selectCommand,
           string furnitor, string kod)
        {
            try
            {
                dgAktePp.DataSource = bindingSourceAktePp;
                bindingSourceAktePp.DataSource = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                dgAktePp.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
            }
        }
        #endregion


        #endregion

        #region Prokurori
        public void mbushProkData(string idTrupVeprimi)
        {
            try
            {
                Global.fillCombo(ref cmbLlojVeprimiProk, Global.localConn, "SELECT [TVID],[LLOJ_VEPRIMI_TRUPI],[AKTIV],[CREATED_AT] " +
               " FROM [dbo].[LLOJ_TRUP_VEPRIMI]  where AKTIV = 1 AND TVID > 2", "LLOJ_VEPRIMI_TRUPI", "TVID");
                cmbLlojVeprimiProk.SelectedIndex = 1;

                dtpDateProk .Format = DateTimePickerFormat.Custom;
                dtpDateProk.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";

                DataTable dtTblKallezim = Global.returnTableForGrid(Global.localConn,
                "SELECT b.[ID],b.[LLTVID],b.[DATA],b.[PROKURORI],b.[IPANDEHURI],b.[NENI],b.[DATE_RREGJISTRIMI],b.[AKTIV],b.[KOMENTE],b.[PRIND_ID] " +
                " FROM veprim a inner join THIRRJE_PROKURORI b on a.kid = b.prind_id " +
                 " where a.kid = " + Global.idVeprimiProcedurePenale.ToString(),
                 "", "Text", null, "Text");

                if (dtTblKallezim != null && dtTblKallezim.Rows.Count > 0 )
                {
                    cmbLlojVeprimiProk.SelectedValue = dtTblKallezim.Rows[0][1].ToString();
                    dtpDateProk.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][2].ToString());// DateTime.Today.AddDays(-1);
                    txtProkuror.Text = dtTblKallezim.Rows[0][3].ToString();
                    txtIPandehur.Text = dtTblKallezim.Rows[0][4].ToString();
                    txtNen.Text = dtTblKallezim.Rows[0][5].ToString();
                    if (dtTblKallezim.Rows[0][7].ToString() == "1") { chkAktivProk.Checked = true; } else { chkAktivProk.Checked = false; }
                }
                else
                {
                    //cmbLlojVeprimiProk.SelectedValue = dtTblKallezim.Rows[0][1].ToString();
                    //dtpDateProk.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][2].ToString());// DateTime.Today.AddDays(-1);
                    txtProkuror.Text = "";
                    txtIPandehur.Text = "";
                    txtNen.Text = "";
                    chkAktivProk.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err mbushProdData " + ex.Message);
            }
        }
        #endregion

        #region mbyll
        private void btnMbyll_Click(object sender, EventArgs e)
        {
            if (Global.listeCeshtje == null)
            {
                Global.listeCeshtje = new ListeKallezime();
            }
            Global.allInfo.Hide();
            Global.listeCeshtje.callGridUpdate("");
            Global.listeCeshtje.Show();
        }
        #endregion

    }
}
