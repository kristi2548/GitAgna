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
    public partial class VeprimKallezim : Form
    {
        public VeprimKallezim()
        {
            InitializeComponent();
            Global.fillCombo(ref cmbLlojVeprimi, Global.localConn, "SELECT [ID],[LLOJ_VEPRIMI] FROM [dbo].[LLOJ_VEPRIMI] where AKTIV = 1 ORDER BY ID ASC", "LLOJ_VEPRIMI", "ID");
            cmbLlojVeprimi.SelectedIndex = 0;
            cmbLlojVeprimi.Enabled = false;
            ApplicationLookAndFeel.UseTheme(this, 12);
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
        private void rregjistrimKallezim_Click(object sender, EventArgs e)
        {
            try
            {
               
                    Global.LLVID = Convert.ToInt32(cmbLlojVeprimi.SelectedValue.ToString());
                    Global.NR = txtNr.Text;
                    Global.DATE_VEPRIMI = dtpDate.Text;
                    Global.DATE_RREGJISTRIMI = System.DateTime.Now.ToString();
                    Global.KALLEZUES = txtKallezues.Text;
                    Global.IKALLEZUAR = txtIKallezuari.Text;
                    Global.FABUL = txtFabul.Text;
                    Global.NENI = "";
                    Global.PRIND_KID = 0;
                    Global.KOMENTE = txtAlert.Text;
                    Global.DATE_ALERTI= dtpAlert.Text;
                    if (chkAktiv.Checked) { Global.AKTIV = 1; } else { Global.AKTIV = 0; }
                    string dateFillimNderto = dtpDate.Value.Year.ToString() + "-" + dtpDate.Value.Month.ToString() + "-" + dtpDate.Value.Day.ToString() +
                        dtpDate.Value.Hour.ToString() + ":" + dtpDate.Value.Minute.ToString() + ":" + dtpDate.Value.Second.ToString();

                if (Global.idVeprimi == 0)
                {
                    if (Global.shtoKokeVeprimi(dateFillimNderto))
                    {
                        MessageBox.Show("Kallezimi u rregjistrua me sukses");
                    }
                    else
                    {
                        MessageBox.Show("Kallezimi NUK u rregjistrua me sukses");
                    }
                }
                else
                {
                    if (Global.shtoKokeVeprimi(dateFillimNderto))
                    {
                        MessageBox.Show("Kallezimi u rregjistrua me sukses");
                    }
                    else
                    {
                        MessageBox.Show("Kallezimi NUK u rregjistrua me sukses");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kallezimi NUK u rregjistrua me sukses");
                Log.LogData("rregjistrimKallezim_Click", ex.Message);
            }
        }
        private void btnShkresa_Click(object sender, EventArgs e)
        {
            try
            {
                Global.idTrupVeprimi = 0;
                if (Global.idVeprimi == 0  )
                {
                    MessageBox.Show("Rregjistroni fillimisht kallezimin ,pastaj mund te shtoni Shkresa!");
                }
                else
                {
                    if (Global.trupVeprimiShkresa == null)
                    {
                        Global.trupVeprimiShkresa = new TrupVeprimiShkresa();
                    }
                    else
                    {
                        Global.trupVeprimiShkresa.Close();
                        Global.trupVeprimiShkresa = new TrupVeprimiShkresa();
                    }

                    Global.veprimKallezim.Hide();
                    if (Global.listeCeshtje != null)
                    {
                        Global.listeCeshtje.Hide();
                    }
                    Global.trupVeprimiShkresa.Show();
                    Global.trupVeprimiShkresa.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Faza 1 " + ex.Message);
                Log.LogData("rregjistrimKallezim_Click", ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Global.listeCeshtje == null)
            {
                Global.listeCeshtje = new ListeKallezime();
            }
            Global.veprimKallezim.Hide();
            Global.listeCeshtje.callGridUpdate("");
            Global.listeCeshtje.Show();
        }
        private void btnProcedura_Click(object sender, EventArgs e)
        {
            Global.idTrupVeprimi = 0;
            if (Global.idVeprimi == 0)
            {
                MessageBox.Show("Rregjistroni fillimisht kallezimin ,pastaj mund te shtoni Akte Proceduriale !");
            }
            else
            {
                if (Global.trupVeprimiAkte == null)
                {
                    Global.trupVeprimiAkte = new TrupVeprimiAkte();
                }
                Global.veprimKallezim.Hide();
                if (Global.listeCeshtje != null)
                {
                    Global.listeCeshtje.Hide();
                }
                Global.trupVeprimiAkte.fillControls();
                Global.trupVeprimiAkte.Show();
                Global.trupVeprimiAkte.Visible = true;
            }
            
        }
        #endregion
        public void boshatis ()
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
                    if (Global.isDateTime(dtTblKallezim.Rows[0][14].ToString()))
                    {
                        dtpAlert.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][14].ToString());// DateTime.Today.AddDays(-1);
                    }
                    
                    if (dtTblKallezim.Rows[0][13].ToString() == "1") { chkAktiv.Checked=true; } else { chkAktiv.Checked = false; }
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

        private void txtFabul_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpAlert_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblDateAlerti_Click(object sender, EventArgs e)
        {

        }

        private void txtAlert_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblAlert_Click(object sender, EventArgs e)
        {

        }

        private void chkAktiv_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtIKallezuari_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtKallezues_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNr_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbLlojVeprimi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblFabul_Click(object sender, EventArgs e)
        {

        }

        private void lblNr_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblKallezues_Click(object sender, EventArgs e)
        {

        }

        private void lblIKallezuar_Click(object sender, EventArgs e)
        {

        }

        private void lblLlojVeprimi_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Global.kallezimePageNr = 1;
            if (Global.listeCeshtje == null)
            {
                Global.listeCeshtje = new ListeKallezime();
            }
            Global.veprimKallezim.Hide();
            Global.listeCeshtje.callGridUpdate("");
            Global.listeCeshtje.Show();
        }
    }
}
