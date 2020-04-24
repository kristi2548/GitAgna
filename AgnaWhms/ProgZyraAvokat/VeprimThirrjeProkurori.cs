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
    public partial class VeprimThirrjeProkurori : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        private BindingSource bindingSource2 = new BindingSource();
        public VeprimThirrjeProkurori()
        {
            //AKTE PROCEDURIALE
            InitializeComponent();
            Global.fillCombo(ref cmbLlojVeprimi, Global.localConn, "SELECT [TVID],[LLOJ_VEPRIMI_TRUPI],[AKTIV],[CREATED_AT] " +
                " FROM [dbo].[LLOJ_TRUP_VEPRIMI]  where AKTIV = 1 AND TVID > 2", "LLOJ_VEPRIMI_TRUPI", "TVID");
            ApplicationLookAndFeel.UseTheme(this, 14);
            cmbLlojVeprimi.SelectedIndex = 1;
            //cmbLlojVeprimi.Enabled = false;
            Global.idTrupVeprimiLlojId = cmbLlojVeprimi.SelectedValue.ToString();//filtrojme shkrese ose akt procedurial

            callGridUpdate();
            callGridTrupVeprime();

            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            chkAktiv.Checked = true;
            txtProkuror.Focus();
        }
        public void boshatisForme()
        {
            txtIPandehur.Text = "";
            txtProkuror.Text = "";
            chkAktiv.Checked = true;
            txtNen.Text = "";
            cmbLlojVeprimi.Focus();
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

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgVeprim.Columns["DATE_RREGJISTRIMI"].Visible = false;
                dgVeprim.Columns["PRIND_KID"].Visible = false;
                dgVeprim.Columns["AKTIV"].Visible = false;
                dgVeprim.Columns["KOMENTE"].Visible = false;
                dgVeprim.Columns["KID"].Visible = false;
                dgVeprim.Columns["LLVID"].Visible = false;
                dgVeprim.Columns["FABUL"].Visible = false;


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
                "SELECT b.[ID],b.[LLTVID],b.[DATA],b.[PROKURORI],b.[IPANDEHURI],b.[NENI],b.[DATE_RREGJISTRIMI],b.[AKTIV],b.[KOMENTE],b.[PRIND_ID] " +
                " FROM veprim a inner join THIRRJE_PROKURORI b on a.kid = b.prind_id " +
                 " where b.aktiv = 1 " + 
                 " and a.kid = " + Global.idVeprimiProcedurePenale.ToString() ,
                 //" and LLTVID = " + Global.idTrupVeprimiLlojId,
                 "", "Text");

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgTrupVeprimi.Columns["ID"].Visible = false;
                dgTrupVeprimi.Columns["LLTVID"].Visible = false;
                dgTrupVeprimi.Columns["AKTIV"].Visible = false;

                dgTrupVeprimi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgTrupVeprimi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgTrupVeprimi.RowTemplate.Height = 50;
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
                Global.LLTVID = Convert.ToInt32(cmbLlojVeprimi.SelectedValue.ToString());
                Global.NR_PROTOKOLLI = txtProkuror.Text;
                Global.DATE = dtpDate.Text;
                Global.PROKURORI = txtProkuror.Text;
                Global.IPANDEHURI = txtIPandehur.Text;
                Global.NENI = txtNen.Text;
                Global.DATE_RREGJISTRIMI = System.DateTime.Now.ToString();
                if (chkAktiv.Checked) { Global.AKTIV_TRUPI = 1; } else { Global.AKTIV_TRUPI = 0; }
                Global.DATE_CREATED = System.DateTime.Now.ToString();
                Global.KOMENTE_TRUPI = "";
                string dateFillimNderto = dtpDate.Value.Year.ToString() + "-" + dtpDate.Value.Month.ToString() + "-" + dtpDate.Value.Day.ToString() +
                    dtpDate.Value.Hour.ToString() + ":" + dtpDate.Value.Minute.ToString() + ":" + dtpDate.Value.Second.ToString();
                //" 00:00:00";
                Global.hapVeprimi = "PROKURORI";
                if (Global.shto_ThirrjeProkurori(dateFillimNderto))
                {
                    Global.idTrupVeprimi = 0;
                }
                else
                {
                    MessageBox.Show("Thirrja ne prokurori  NUK u rregjistrua me sukses");
                }
                boshatisForme();
                callGridTrupVeprime();
                dgTrupVeprimi.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thirrja ne prokurori  NUK u rregjistrua me sukses");
                Log.LogData("rregjistrimKallezim_Click", ex.Message);
            }
        }

        private void btnAnulloShkresa_Click(object sender, EventArgs e)
        {
            if (Global.veprimThirrjeProkurori == null)
            {
                Global.veprimThirrjeProkurori.Hide();
                Global.veprimProcedimPenal = new VeprimProcedimPenal();
                Global.veprimProcedimPenal.Show();
            }
            else
            {
                Global.veprimThirrjeProkurori.Hide();
                Global.veprimProcedimPenal.Show();
            }
        }

        private void dgTrupVeprimi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgTrupVeprimi.Rows.Count > 0 && dgTrupVeprimi.SelectedCells.Count > 0)
                {
                    //ceshtje_ERe();
                    int selectedrowindex = dgTrupVeprimi.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgTrupVeprimi.Rows[selectedrowindex];
                    Global.idTrupVeprimi = Convert.ToInt32(selectedRow.Cells["ID"].Value.ToString());
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
                "SELECT b.[ID],b.[LLTVID],b.[DATA],b.[PROKURORI],b.[IPANDEHURI],b.[NENI],b.[DATE_RREGJISTRIMI],b.[AKTIV],b.[KOMENTE],b.[PRIND_ID] " +
                " FROM veprim a inner join THIRRJE_PROKURORI b on a.kid = b.prind_id " +
                 " where a.kid = " + Global.idVeprimiProcedurePenale.ToString() ,
                 "", "Text", null, "Text");

                if (dtTblKallezim != null && dtTblKallezim.Rows.Count > 0)
                {
                    cmbLlojVeprimi.SelectedValue = dtTblKallezim.Rows[0][1].ToString();
                    dtpDate.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][2].ToString());// DateTime.Today.AddDays(-1);
                    txtProkuror.Text = dtTblKallezim.Rows[0][3].ToString();
                    txtIPandehur.Text = dtTblKallezim.Rows[0][4].ToString();
                    txtNen.Text = dtTblKallezim.Rows[0][5].ToString();
                    if (dtTblKallezim.Rows[0][7].ToString() == "1") { chkAktiv.Checked = true; } else { chkAktiv.Checked = false; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err mbushTrupVeprimi " + ex.Message);
            }
        }

        private void btnHiqProdukt_Click(object sender, EventArgs e)
        {
            if (Global.veprimProcedimPenal == null)
            {
                Global.veprimKallezim.Hide();
                Global.veprimProcedimPenal = new VeprimProcedimPenal();
                Global.veprimProcedimPenal.Show();
            }
            else
            {
                Global.veprimKallezim.Hide();
                Global.veprimProcedimPenal.Show();
            }
        }

        private void btnMbyllCeshtje_Click(object sender, EventArgs e)
        {
            try
            {
                Global.STATUS_CESHTJE = "Mbyllur";
                Global.DATE_STATUS_CESHTJE = dtpDate.Text;

                Global.idTrupVeprimi = 0;
                if (Global.idVeprimiProcedurePenale == 0)
                {
                    MessageBox.Show("Rregjistroni fillimisht Njoftim Akuze /Perfundim Hetimesh ,pastaj mund te ndryshoni statusin e ceshtjes !", "Njoftim ploteso");
                }
                else
                {
                    Global.ndryshoStatusCeshtje();
                    MessageBox.Show("Ceshtja u mbyll me sukses ");
                    //if (Global.veprimThirrjeProkurori == null)
                    //{
                    //    Global.veprimThirrjeProkurori = new VeprimThirrjeProkurori();
                    //}
                    //Global.veprimProcedimPenal.Hide();
                    //if (Global.listeCeshtje != null)
                    //{
                    //    Global.listeCeshtje.Hide();
                    //}
                    //Global.veprimThirrjeProkurori.Show();
                    //Global.veprimThirrjeProkurori.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mbyll Ceshtje " + ex.Message);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Global.kallezimePageNr = 1;
            if (Global.listeCeshtje == null)
            {
                Global.listeCeshtje = new ListeKallezime();
            }
            Global.veprimThirrjeProkurori.Hide();
            Global.listeCeshtje.callGridUpdate("");
            Global.listeCeshtje.Show();
        }
    }
}
