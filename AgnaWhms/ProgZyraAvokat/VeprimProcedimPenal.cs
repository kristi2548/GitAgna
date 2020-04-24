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
    public partial class VeprimProcedimPenal : Form
    {
        public VeprimProcedimPenal()
        {
            InitializeComponent();
            Global.fillCombo(ref cmbLlojVeprimi, Global.localConn, "SELECT [ID],[LLOJ_VEPRIMI] FROM [dbo].[LLOJ_VEPRIMI] where AKTIV = 1  ORDER BY ID ASC", "LLOJ_VEPRIMI", "ID");
            cmbLlojVeprimi.SelectedIndex = 1;
            cmbLlojVeprimi.Enabled = false;
            ApplicationLookAndFeel.UseTheme(this, 12);
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";

            dtpAlert.Format = DateTimePickerFormat.Custom;
            dtpAlert.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            dtpAlert.Value = System.DateTime.Now;
        }
        #region Menus
        private void rregjistrimProcedim_Click(object sender, EventArgs e)
        {
            try
            {
                Global.LLVID = Convert.ToInt32(cmbLlojVeprimi.SelectedValue.ToString());
                Global.NR = txtNr.Text;
                Global.DATE_VEPRIMI = dtpDate.Text;
                Global.DATE_ALERTI = System.DateTime.Now.ToString();
                Global.DATE_RREGJISTRIMI = System.DateTime.Now.ToString();
                Global.KALLEZUES = txtKallezues.Text;
                Global.IKALLEZUAR = txtIKallezuari.Text;
                Global.FABUL = txtFabul.Text;
                Global.NENI = txtNeni.Text;
                Global.PRIND_KID = Global.idVeprimi;
                if (chkAktiv.Checked) { Global.AKTIV = 1; } else { Global.AKTIV = 0; }
                Global.KOMENTE = txtAlert.Text ;
                string dateFillimNderto = dtpDate.Value.Year.ToString() + "-" + dtpDate.Value.Month.ToString() + "-" + dtpDate.Value.Day.ToString() +
                    dtpDate.Value.Hour.ToString() + ":" + dtpDate.Value.Minute.ToString() + ":" + dtpDate.Value.Second.ToString();

                    if (Global.shtoKokeVeprimiProcedurePenale(dateFillimNderto))
                    {
                        MessageBox.Show("Procedimi Penal u rregjistrua me sukses");
                    }
                    else
                    {
                        MessageBox.Show("Procedimi Penal NUK u rregjistrua me sukses");
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Faza 1 " + ex.Message);
                MessageBox.Show("Procedimi Penal NUK u rregjistrua me sukses");
                Log.LogData("rregjistrimProcedim_Click", ex.Message);
            }
        }
        private void btnShkresa_Click(object sender, EventArgs e)
        {
            try
            {
                Global.idTrupVeprimi = 0;
                if (Global.idVeprimiProcedurePenale == 0 )
                {
                    MessageBox.Show("Rregjistroni fillimisht Procedimin Penal ,pastaj mund te shtoni Shkresa ose Akte Proceduriale !");
                }
                else
                {
                    if (Global.trupVeprimShkresaProcedim == null)
                    {
                        //Global.veprimProcedimPenal.Hide();
                        //if (Global.listeCeshtje != null)
                        //{
                        //    Global.listeCeshtje.Hide();
                        //}
                        Global.trupVeprimShkresaProcedim = new TrupVeprimShkresaProcedim();
                        //Global.trupVeprimShkresaProcedim.Show();
                        //Global.trupVeprimShkresaProcedim.Visible = true;
                    }
                    Global.veprimProcedimPenal.Hide();
                    if (Global.listeCeshtje != null)
                    {
                        Global.listeCeshtje.Hide();
                    }
                    Global.trupVeprimShkresaProcedim = new TrupVeprimShkresaProcedim();
                    Global.trupVeprimShkresaProcedim.Show();
                    Global.trupVeprimShkresaProcedim.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Faza 2 " + ex.Message);
                Log.LogData("rregjistrimProcedim_Click", ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Global.listeCeshtje == null)
            {
                Global.veprimProcedimPenal.Hide();
                Global.veprimKallezim = new VeprimKallezim();
                Global.veprimKallezim.fushaValidate();
                //Global.listeCeshtje.callGridUpdate("");
                Global.veprimKallezim.Show();
            }
            else
            {
                Global.veprimProcedimPenal.Hide();
                //Global.listeCeshtje.callGridUpdate("");
                Global.veprimKallezim.fushaValidate();
                Global.veprimKallezim.Show();
            }
        }
        private void btnProcedura_Click(object sender, EventArgs e)
        {
            Global.idTrupVeprimi = 0;
            if (Global.idVeprimiProcedurePenale == 0)
            {
                MessageBox.Show("Rregjistroni fillimisht Procedimin Penal ,pastaj mund te shtoni Akte Proceduriale !");
            }
            else
            {
                if (Global.trupVeprimAktProcedim == null)
                {
                    Global.trupVeprimAktProcedim = new TrupVeprimeAkteProcedim();
                }
                Global.veprimProcedimPenal.Hide();
                if (Global.listeCeshtje != null)
                {
                    Global.listeCeshtje.Hide();
                }
                Global.trupVeprimAktProcedim.fillControls();
                Global.trupVeprimAktProcedim.Show();
                Global.trupVeprimAktProcedim.Visible = true;
            }

        }
        #endregion

        public void fillVeprimById(string idVeprimi)
        {
            try
            {
                DataTable dtTblKallezim = Global.returnTableForGrid(Global.localConn,
                    "SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID]," +
                " a.[AKTIV],[KOMENTE],[KID],[LLVID],a.aktiv,date_alerti " +
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

                    Global.idVeprimiProcedurePenale = Convert.ToInt32(dtTblKallezim.Rows[0][11].ToString());

                    if (Global.isDateTime(dtTblKallezim.Rows[0][14].ToString()))
                    {
                        dtpAlert.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][14].ToString());// DateTime.Today.AddDays(-1);
                    }
                    if (dtTblKallezim.Rows[0][13].ToString() == "1") { chkAktiv.Checked = true; } else { chkAktiv.Checked = false; }
                }
                else
                {
                    boshatis();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err fillVeprimById " + ex.Message);
            }
        }
        public void fillProcedimPenal_ByKallezimId(string idVeprimi)
        {
            try
            {
                DataTable dtTblKallezim = Global.returnTableForGrid(Global.localConn,
                    "SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID]," +
                " a.[AKTIV],[KOMENTE],[KID],[LLVID],A.AKTIV " +
                " FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID where a.PRIND_KID = " + Global.idVeprimi.ToString(),
                "", "Text", null, "Text");

                if (dtTblKallezim != null && dtTblKallezim.Rows.Count > 0)
                {
                    txtNr.Text = dtTblKallezim.Rows[0][1].ToString();
                    dtpDate.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][2].ToString());// DateTime.Today.AddDays(-1);
                    txtKallezues.Text = dtTblKallezim.Rows[0][4].ToString();
                    txtIKallezuari.Text = dtTblKallezim.Rows[0][5].ToString();
                    txtFabul.Text = dtTblKallezim.Rows[0][6].ToString();
                    Global.idVeprimiProcedurePenale = Convert.ToInt32(dtTblKallezim.Rows[0][11].ToString());
                    cmbLlojVeprimi.SelectedValue = dtTblKallezim.Rows[0][12].ToString();
                    if (dtTblKallezim.Rows[0][13].ToString() == "1") { chkAktiv.Checked = true; } else { chkAktiv.Checked = false; }
                }
                else
                {
                    boshatis();
                    Global.idVeprimiProcedurePenale = 0;
                }
                txtNr.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err fillVeprimById " + ex.Message);
            }
        }
        public void boshatis()
        {
            txtFabul.Text = "";
            txtIKallezuari.Text = "";
            txtKallezues.Text = "";
            txtNr.Text = "";
            txtNeni.Text = "";
            cmbLlojVeprimi.SelectedIndex = 1;
            dtpDate.Value = DateTime.Today;// DateTime.Today.AddDays(-1);
            dtpAlert.Value = DateTime.Today;// DateTime.Today.AddDays(-1);
            chkAktiv.Checked = true;
            txtNr.Focus();
        }

        private void btnThirjeProkuori_Click(object sender, EventArgs e)
        {
            Global.idTrupVeprimi = 0;
            if (Global.idVeprimiProcedurePenale == 0)
            {
                MessageBox.Show("Rregjistroni fillimisht Procedimin Penal ,pastaj mund te shtoni Akte Proceduriale !", "Procedim Penal ploteso");
            }
            else
            {
                if (Global.veprimThirrjeProkurori == null)
                {
                    Global.veprimThirrjeProkurori = new VeprimThirrjeProkurori();
                }
                Global.veprimProcedimPenal.Hide();
                if (Global.listeCeshtje != null)
                {
                    Global.listeCeshtje.Hide();
                }
                Global.veprimThirrjeProkurori.callGridUpdate();
                Global.veprimThirrjeProkurori.callGridTrupVeprime();
                Global.veprimThirrjeProkurori.Show();
                Global.veprimThirrjeProkurori.Visible = true;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Global.kallezimePageNr = 1;
            if (Global.listeCeshtje == null)
            {
                Global.listeCeshtje = new ListeKallezime();
            }
            Global.veprimProcedimPenal.Hide();
            Global.listeCeshtje.callGridUpdate("");
            Global.listeCeshtje.Show();
        }
    }
}
