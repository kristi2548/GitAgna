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
    public partial class TrupVeprimiAkte : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        private BindingSource bindingSource2 = new BindingSource();
        public TrupVeprimiAkte()
        {
            //AKTE PROCEDURIALE
            InitializeComponent();
            //Global.fillCombo(ref cmbLlojVeprimi, Global.localConn, "SELECT [TVID],[LLOJ_VEPRIMI_TRUPI],[AKTIV],[CREATED_AT] " +
            //    " FROM [dbo].[LLOJ_TRUP_VEPRIMI]  where AKTIV = 1", "LLOJ_VEPRIMI_TRUPI", "TVID");
            //ApplicationLookAndFeel.UseTheme(this, 12);
            //cmbLlojVeprimi.SelectedIndex = 1;
            //Global.idTrupVeprimiLlojId = cmbLlojVeprimi.SelectedValue.ToString();//filtrojme shkrese ose akt procedurial
            //cmbLlojVeprimi.Enabled = false;
            //callGridUpdate();
            //callGridTrupVeprime();
            //dtpDate.Format = DateTimePickerFormat.Custom;
            //dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            //chkAktiv.Checked = true;
            //txtLloji.Focus();
            fillControls();
        }
        public void fillControls()
        {
            Global.fillCombo(ref cmbLlojVeprimi, Global.localConn, "SELECT [TVID],[LLOJ_VEPRIMI_TRUPI],[AKTIV],[CREATED_AT] " +
               " FROM [dbo].[LLOJ_TRUP_VEPRIMI]  where AKTIV = 1", "LLOJ_VEPRIMI_TRUPI", "TVID");
            ApplicationLookAndFeel.UseTheme(this, 12);
            cmbLlojVeprimi.SelectedIndex = 1;
            Global.idTrupVeprimiLlojId = cmbLlojVeprimi.SelectedValue.ToString();//filtrojme shkrese ose akt procedurial
            cmbLlojVeprimi.Enabled = false;
            callGridUpdate();
            callGridTrupVeprime();
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            chkAktiv.Checked = true;
            txtLloji.Focus();
        }
        public void boshatisForme()
        {
            txtFabul.Text = "";
            txtLloji.Text = "";
            chkAktiv.Checked = true;
            txtLloji.Focus();
        }
        public void callGridUpdate()
        {
            try
            {
                fillGrid(Global.localConn,
                "SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID]," +
                " a.[AKTIV],[KOMENTE],[KID],[LLVID] " +
                " FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID where a.kid = " + Global.idVeprimi.ToString(),
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
                dgVeprim.DefaultCellStyle.Font = new Font("Century Gothic", 9);
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
                "SELECT  dbo.TRUP_VEPRIMI.NR_PROTOKOLLI, dbo.TRUP_VEPRIMI.DERGUESI, dbo.TRUP_VEPRIMI.MARRESI, " +
                 " dbo.TRUP_VEPRIMI.LLOJI_AKT_PROCEDURIAL, dbo.LLOJ_TRUP_VEPRIMI.LLOJ_VEPRIMI_TRUPI,dbo.VEPRIM.KID,TRUP_VEPRIMI.TID" +
                 " FROM dbo.VEPRIM INNER JOIN" +
                 " dbo.TRUP_VEPRIMI ON dbo.VEPRIM.KID = dbo.TRUP_VEPRIMI.KID INNER JOIN" +
                 " dbo.LLOJ_TRUP_VEPRIMI ON dbo.TRUP_VEPRIMI.LLTVID = dbo.LLOJ_TRUP_VEPRIMI.TVID where VEPRIM.kid = " + Global.idVeprimi.ToString() +
                 "  AND TRUP_VEPRIMI.AKTIV = 1 and LLTVID = " + Global.idTrupVeprimiLlojId,
                "", "Text");

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgTrupVeprimi.Columns["TID"].Visible = false;
                dgTrupVeprimi.Columns["KID"].Visible = false;
                dgTrupVeprimi.Columns["DERGUESI"].Visible = false;
                dgTrupVeprimi.Columns["MARRESI"].Visible = false;

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
                if (txtLloji.Text != "")
                {
                    Global.LLTVID = Convert.ToInt32(cmbLlojVeprimi.SelectedValue.ToString());
                    Global.NR_PROTOKOLLI = txtLloji.Text;
                    Global.DATE = dtpDate.Text;
                    Global.LENDA = "";
                    Global.DATE_RREGJISTRIMI = System.DateTime.Now.ToString();
                    Global.LENDA = "";
                    Global.DERGUESI = "";
                    Global.MARRESI = "";
                    Global.FABUL_TRUPI = txtFabul.Text;
                    Global.LLOJI_AKT_PROCEDURIAL = txtLloji.Text;
                    if (chkAktiv.Checked) { Global.AKTIV_TRUPI = 1; } else { Global.AKTIV_TRUPI = 0; }
                    Global.DATE_CREATED = System.DateTime.Now.ToString();
                    Global.KOMENTE_TRUPI = "";
                    string dateFillimNderto = dtpDate.Value.Year.ToString() + "-" + dtpDate.Value.Month.ToString() + "-" + dtpDate.Value.Day.ToString() +
                        dtpDate.Value.Hour.ToString() + ":" + dtpDate.Value.Minute.ToString() + ":" + dtpDate.Value.Second.ToString();
                    //" 00:00:00";
                    if (Global.shtoTrupVeprimi(dateFillimNderto))
                    {
                        //MessageBox.Show("Akti Procedurial u rregjistrua me sukses");
                    }
                    else
                    {
                        MessageBox.Show("Akti Procedurial  NUK u rregjistrua me sukses");
                    }
                    boshatisForme();
                    callGridTrupVeprime();
                    dgTrupVeprimi.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    Global.idTrupVeprimi = 0;
                }
                else
                {
                    MessageBox.Show("Plotesoni llojin,nuk mund te jete bosh !", "Plotesoni llojin");
                    txtLloji.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Akti Procedurial  NUK u rregjistrua me sukses");
                Log.LogData("rregjistrimKallezim_Click", ex.Message);
            }
        }

        private void btnAnulloShkresa_Click(object sender, EventArgs e)
        {
            if (Global.veprimKallezim == null)
            {
                Global.veprimKallezim = new VeprimKallezim();
            }
            Global.trupVeprimiAkte.Hide();
            Global.veprimKallezim.Show();
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
                 " TRUP_VEPRIMI.MARRESI,TRUP_VEPRIMI.LLOJI_AKT_PROCEDURIAL, LLOJ_TRUP_VEPRIMI.LLOJ_VEPRIMI_TRUPI,VEPRIM.KID,TRUP_VEPRIMI.LLTVID,TRUP_VEPRIMI.aktiv " +
                 " FROM VEPRIM INNER JOIN" +
                 " TRUP_VEPRIMI ON VEPRIM.KID = TRUP_VEPRIMI.KID INNER JOIN" +
                 " LLOJ_TRUP_VEPRIMI ON TRUP_VEPRIMI.LLTVID = LLOJ_TRUP_VEPRIMI.TVID where VEPRIM.kid = " + Global.idVeprimi.ToString() +
                 " and tid = " + idTrupVeprimi,
                 "", "Text", null, "Text");

                if (dtTblKallezim != null)
                {
                    txtFabul.Text = dtTblKallezim.Rows[0][2].ToString();
                    txtLloji.Text = dtTblKallezim.Rows[0][6].ToString();
                    
                    cmbLlojVeprimi.SelectedValue = dtTblKallezim.Rows[0][9].ToString();
                    if (dtTblKallezim.Rows[0][10].ToString() == "1") { chkAktiv.Checked = true; } else { chkAktiv.Checked = false; }
                    dtpDate.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][0].ToString());// DateTime.Today.AddDays(-1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err mbushTrupVeprimi " + ex.Message);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Global.kallezimePageNr = 1;
            if (Global.listeCeshtje == null)
            {
                Global.listeCeshtje = new ListeKallezime();
            }
            Global.trupVeprimiAkte.Hide();
            Global.listeCeshtje.callGridUpdate("");
            Global.listeCeshtje.Show();
        }
    }
}
