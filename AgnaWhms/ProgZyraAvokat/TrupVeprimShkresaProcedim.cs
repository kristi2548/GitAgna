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

using System.Drawing.Drawing2D;

namespace ProgZyraAvokat
{
    public partial class TrupVeprimShkresaProcedim : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        private BindingSource bindingSource2 = new BindingSource();
        public TrupVeprimShkresaProcedim()
        {
            //SHKRESA
            InitializeComponent();
            Global.fillCombo(ref cmbLlojVeprimi, Global.localConn, "SELECT [TVID],[LLOJ_VEPRIMI_TRUPI],[AKTIV],[CREATED_AT] " +
                " FROM [dbo].[LLOJ_TRUP_VEPRIMI] where AKTIV = 1", "LLOJ_VEPRIMI_TRUPI", "TVID");
            ApplicationLookAndFeel.UseTheme(this, 12);
            cmbLlojVeprimi.SelectedIndex = 0;
            Global.idTrupVeprimiLlojId = cmbLlojVeprimi.SelectedValue.ToString();//filtrojme shkrese ose akt procedurial
            cmbLlojVeprimi.Enabled = false;
            callGridUpdate();
            callGridTrupVeprime();
            dtpDate.Format = DateTimePickerFormat.Custom;
            chkAktiv.Checked = true;
            dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            txtNr.Focus();
        }
        public void boshatisForme()
        {
            txtFabul.Text = "";
            txtDerguesi.Text = "";
            txtLenda.Text = "";
            txtNr.Text = "";
            chkAktiv.Checked = true;
            txtMarresi.Text = "";
            txtNr.Focus();

        }
        private void lblLlojVeprimi_Click(object sender, EventArgs e)
        {

        }

        private void cmbLlojVeprimi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void callGridUpdate()
        {
            try
            {
                fillGrid(Global.localConn,
                "SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID]," +
                " a.[AKTIV],[KOMENTE],[KID],[LLVID] " +
                " FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID where a.kid = " + Global.idVeprimiProcedurePenale.ToString(),
                "", "Text");

                dgVeprim.Columns["DATE_RREGJISTRIMI"].Visible = false;
                dgVeprim.Columns["PRIND_KID"].Visible = false;
                dgVeprim.Columns["AKTIV"].Visible = false;
                dgVeprim.Columns["KOMENTE"].Visible = false;
                dgVeprim.Columns["KID"].Visible = false;
                dgVeprim.Columns["LLVID"].Visible = false;
                dgVeprim.Columns["FABUL"].Visible = false;

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgVeprim.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgVeprim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgVeprim.RowTemplate.Height = 30;
                dgVeprim.ForeColor = lblForeColor12;
                dgVeprim.BackgroundColor = formBackColorAll;
                dgVeprim.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                dgVeprim.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgVeprim.RowsDefaultCellStyle.BackColor = formBackColorAll;
                dgVeprim.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        public void callGridTrupVeprime()
        {
            try
            {
                fillGridTrupi(Global.localConn,
                "SELECT  TRUP_VEPRIMI.NR_PROTOKOLLI, TRUP_VEPRIMI.DERGUESI, TRUP_VEPRIMI.MARRESI, " +
                 " TRUP_VEPRIMI.LLOJI_AKT_PROCEDURIAL, NENI,LLOJ_TRUP_VEPRIMI.LLOJ_VEPRIMI_TRUPI,VEPRIM.KID,TRUP_VEPRIMI.TID" +
                 " FROM VEPRIM INNER JOIN" +
                 " TRUP_VEPRIMI ON VEPRIM.KID = TRUP_VEPRIMI.KID INNER JOIN" +
                 " LLOJ_TRUP_VEPRIMI ON TRUP_VEPRIMI.LLTVID = LLOJ_TRUP_VEPRIMI.TVID where VEPRIM.kid = " + Global.idVeprimiProcedurePenale.ToString() +
                 "  AND TRUP_VEPRIMI.AKTIV = 1 and LLTVID = " + Global.idTrupVeprimiLlojId,
                "", "Text");

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgTrupVeprimi.Columns["TID"].Visible = false;
                dgTrupVeprimi.Columns["KID"].Visible = false;
                dgTrupVeprimi.Columns["LLOJI_AKT_PROCEDURIAL"].Visible = false;

                dgTrupVeprimi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgTrupVeprimi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgTrupVeprimi.RowTemplate.Height = 30;
                dgTrupVeprimi.ForeColor = lblForeColor12;
                dgTrupVeprimi.BackgroundColor = formBackColorAll;
                dgTrupVeprimi.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                dgTrupVeprimi.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgTrupVeprimi.RowsDefaultCellStyle.BackColor = formBackColorAll;
                dgTrupVeprimi.ReadOnly = true;
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
                dgVeprim.DataSource = bindingSource1;
                bindingSource1.DataSource = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                dgVeprim.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
            }
        }
        public void fillGridTrupi(string myConnectionString, string selectCommand,
           string furnitor, string kod)
        {
            try
            {
                dgTrupVeprimi.DataSource = bindingSource2;
                bindingSource2.DataSource = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                dgTrupVeprimi.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
            }
        }

        private void btnShtoShkresa_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNr.Text == "")
                {
                    MessageBox.Show("Nuk mund te rregjistrohet shkrese pa nr protokolli ", "Nr Protokolli ploteso");
                    txtNr.Focus();
                }
                else
                {
                    Global.LLTVID = Convert.ToInt32(cmbLlojVeprimi.SelectedValue.ToString());
                    Global.NR_PROTOKOLLI = txtNr.Text;
                    Global.DATE = dtpDate.Text;
                    Global.LENDA = "";
                    Global.DATE_RREGJISTRIMI = System.DateTime.Now.ToString();
                    Global.LENDA = txtLenda.Text;
                    Global.DERGUESI = txtDerguesi.Text;
                    Global.MARRESI = txtMarresi.Text;
                    Global.FABUL_TRUPI = txtFabul.Text;
                    Global.LLOJI_AKT_PROCEDURIAL = "";
                    if (chkAktiv.Checked) { Global.AKTIV_TRUPI = 1; } else { Global.AKTIV_TRUPI = 0; }
                    Global.DATE_CREATED = System.DateTime.Now.ToString();
                    Global.KOMENTE_TRUPI = "";
                    string dateFillimNderto = dtpDate.Value.Year.ToString() + "-" + dtpDate.Value.Month.ToString() + "-" + dtpDate.Value.Day.ToString() +
                        dtpDate.Value.Hour.ToString() + ":" + dtpDate.Value.Minute.ToString() + ":" + dtpDate.Value.Second.ToString();
                    //" 00:00:00";
                    //if (Global.shtoTrupVeprimi(dateFillimNderto))
                    if (Global.shtoTrupVeprimi_ProcedimPenal(dateFillimNderto))
                    {
                        //MessageBox.Show("Shkresa u rregjistrua me sukses");
                        Global.idTrupVeprimi = 0;
                    }
                    else
                    {
                        MessageBox.Show("Shkresa NUK u rregjistrua me sukses");
                    }
                    boshatisForme();
                    callGridTrupVeprime();
                    Global.idTrupVeprimi = 0;
                    dgTrupVeprimi.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    txtNr.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Faza 1-1 " + ex.Message);
                MessageBox.Show("Shkresa NUK u rregjistrua me sukses");
                Log.LogData("rregjistrimKallezim_Click", ex.Message);
            }
        }

        private void btnAnulloShkresa_Click(object sender, EventArgs e)
        {
            if (Global.veprimProcedimPenal == null)
            {
                Global.trupVeprimShkresaProcedim.Hide();
                Global.veprimProcedimPenal = new VeprimProcedimPenal();
                Global.veprimProcedimPenal.Show();
            }
            else
            {
                Global.veprimKallezim.Hide();
                Global.veprimProcedimPenal.Show();
            }
        }

        private void dgTrupVeprimi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grideClick();
        }
        public void grideClick()
        {
            try
            {
                if (dgTrupVeprimi.Rows.Count > 0 && dgTrupVeprimi.SelectedCells.Count > 0)
                {
                    //ceshtje_ERe();
                    int selectedrowindex = dgTrupVeprimi.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgTrupVeprimi.Rows[selectedrowindex];
                    Global.idTrupVeprimi = Convert.ToInt32(selectedRow.Cells["Tid"].Value.ToString());
                    Global.veprimKallezim.fillVeprimById(Global.idVeprimi.ToString());
                    mbushTrupVeprimi(Global.idTrupVeprimi.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("dgTrupVeprimi_CellContentClick err " + ex.Message);
            }
        }
        public void mbushTrupVeprimi(string idTrupVeprimi)
        {
            try
            {
                DataTable dtTblKallezim = Global.returnTableForGrid(Global.localConn,
                 "SELECT  TRUP_VEPRIMI.DATE,TRUP_VEPRIMI.LENDA,TRUP_VEPRIMI.FABUL,TRUP_VEPRIMI.NR_PROTOKOLLI, TRUP_VEPRIMI.DERGUESI,  " +
                 " TRUP_VEPRIMI.MARRESI,TRUP_VEPRIMI.LLOJI_AKT_PROCEDURIAL, LLOJ_TRUP_VEPRIMI.LLOJ_VEPRIMI_TRUPI,VEPRIM.KID,TRUP_VEPRIMI.LLTVID" +
                 " FROM VEPRIM INNER JOIN" +
                 " TRUP_VEPRIMI ON VEPRIM.KID = TRUP_VEPRIMI.KID INNER JOIN" +
                 " LLOJ_TRUP_VEPRIMI ON TRUP_VEPRIMI.LLTVID = LLOJ_TRUP_VEPRIMI.TVID where VEPRIM.kid = " + Global.idVeprimiProcedurePenale.ToString() +
                 " and tid = " + idTrupVeprimi,
                 "", "Text", null, "Text");

                if (dtTblKallezim != null)
                {
                    txtLenda.Text = dtTblKallezim.Rows[0][1].ToString();
                    txtFabul.Text = dtTblKallezim.Rows[0][2].ToString();
                    txtNr.Text = dtTblKallezim.Rows[0][3].ToString();
                    txtDerguesi.Text = dtTblKallezim.Rows[0][4].ToString();
                    txtMarresi.Text = dtTblKallezim.Rows[0][5].ToString();

                    cmbLlojVeprimi.SelectedValue = dtTblKallezim.Rows[0][9].ToString();
                    dtpDate.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][0].ToString());// DateTime.Today.AddDays(-1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err mbushTrupVeprimi " + ex.Message);
            }
        }

        private void dgTrupVeprimi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grideClick();
        }

        private void btnHiqProdukt_Click(object sender, EventArgs e)
        {
            if (Global.veprimProcedimPenal == null)
            {
                Global.veprimProcedimPenal = new VeprimProcedimPenal();
            }
            Global.trupVeprimShkresaProcedim.Hide();
            Global.veprimProcedimPenal.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Global.kallezimePageNr = 1;
            if (Global.listeCeshtje == null)
            {
                Global.listeCeshtje = new ListeKallezime();
            }
            Global.trupVeprimShkresaProcedim.Hide();
            Global.listeCeshtje.callGridUpdate("");
            Global.listeCeshtje.Show();
        }
    }
}
