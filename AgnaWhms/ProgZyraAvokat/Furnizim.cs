using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgZyraAvokat;
using System.Globalization;
namespace AgnaWhms
{
    public partial class Furnizim : Form
    {
        public Furnizim()
        {
            try
            {
                InitializeComponent();

                levizjeMagazinaInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading LevizjeMagazina " + ex.Message);
            }
            
            //boshatisTrupVeprimi();
        }
        public void levizjeMagazinaInit()
        {
            try
            {
                fillCombo();

                ApplicationLookAndFeel.UseTheme(this, 12);
                dtpDate.Format = DateTimePickerFormat.Custom;
                dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";

                dtpAlert.Format = DateTimePickerFormat.Custom;
                dtpAlert.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
                if (Global.idVeprimi == 0)
                {
                    boshatis();
                    boshatisTrupVeprimi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("levizjeMagazinaInit err " + ex.Message);
            }
        }
        public bool fillCombo()
        {
            try
            {
                Global.fillCombo(ref cmbPorosiPrind, Global.localConn,
               "SELECT [MovHeadID],[OrderNr] + '-' + cast([MovHeadTime] as Varchar(20)) as OrderInfo FROM [dbo].[wMovHeads] where MovStatusID = 1 and Aktiv = 1", "OrderInfo", "MovHeadID");
                cmbPorosiPrind.SelectedIndex = -1;

                Global.fillCombo(ref cmbCatMov, Global.localConn,
               "SELECT [MovCatID],[MovCatCode] + '-' + [MovCatName] as LLojLevizje FROM [wMovCategs]", "LLojLevizje", "MovCatID");

                Global.fillCombo(ref cmbStatus, Global.localConn,
               "SELECT [MovStatusID],[MovStatusName] + '-' + MovStatusNotes as Status FROM [dbo].[wMovStatuses]", "Status", "MovStatusID");

                Global.fillCombo(ref cmbWarehouse, Global.localConn,
                "SELECT [WarehouseID],[WarehouseCode] + '-' + [WarehouseName] as Magazina FROM [warehouses]", "Magazina", "WarehouseID");

                Global.fillCombo(ref cmbArea, Global.localConn,
                "SELECT [AreaID],[AreaCode] + '-' + [AreaName] as Zona FROM [dbo].[wAreas]", "Zona", "AreaID");

                Global.fillCombo(ref cmbProdukti, Global.localConn,
               "SELECT [ProductID],[ProductNav] + '-' + [ProductName] AS Produkti FROM [wProducts]", "Produkti", "ProductID");
                cmbProdukti.SelectedIndex = -1;

                Global.fillCombo(ref cmbLot, Global.localConn,
               "SELECT [LotID],[ProductNav] + '-' + [ExpDate] as ProdInfo FROM [nProdLots]", "ProdInfo", "LotID");
                //cmbArea.SelectedIndex = 0;
                Global.fillCombo(ref cmbStatusTrupfature, Global.localConn,
              "SELECT [MovStatusID],[MovStatusName] + '-' + MovStatusNotes as Status FROM [dbo].[wMovStatuses]", "Status", "MovStatusID");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err fillCombo " + ex.Message);
                return false; 
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
        private void rregjistrimVeprim_Click(object sender, EventArgs e)
        {
            rregjistroLevizje("KOKE_VEPRIMI");
        }
        public bool rregjistroLevizje(string tipLevizje)
        {
            try
            {
                string dateFillimNderto = "";
                if (tipLevizje == "KOKE_VEPRIMI")
                {
                    Global.idVeprimi = 0;
                    if (cmbPorosiPrind.SelectedIndex > -1)
                    {
                        Global.orderPorosiId = Convert.ToInt32(cmbPorosiPrind.SelectedValue.ToString());
                        Global.orderAreaId = Convert.ToInt32(cmbArea.SelectedValue.ToString());
                    }
                    else
                    {
                        Global.orderPorosiId = 0;
                        Global.orderAreaId = 0;
                    }
                    Global.orderNr = txtNrLevizje.Text;
                    Global.orderPorosiNr = txtNrPorosie.Text;
                }
                else
                {
                    //Global.orderPorosiId = 0;
                    //Global.orderAreaId = 0;
                    Global.orderNr = "0";
                    //Global.orderPorosiNr = "0";
                }
                Global.orderMovStatusId = Convert.ToInt32(cmbStatus.SelectedValue.ToString());
                Global.orderMovCatId = Convert.ToInt32(cmbCatMov.SelectedValue.ToString());
                Global.orderWhmsId = Convert.ToInt32(cmbWarehouse.SelectedValue.ToString());
                Global.orderNotes = txtShenime.Text;
                Global.orderTime = dtpDate.Text;
                if (chkAktiv.Checked) { Global.AKTIV = 1; } else { Global.AKTIV = 0; }

                dateFillimNderto = dtpDate.Value.Year.ToString() + "-" + dtpDate.Value.Month.ToString() + "-" + dtpDate.Value.Day.ToString() + " " +
                    dtpDate.Value.Hour.ToString() + ":" + dtpDate.Value.Minute.ToString() + ":" + dtpDate.Value.Second.ToString();


                if (Global.idVeprimi == 0)
                {
                    if (tipLevizje == "KOKE_VEPRIMI")
                    {
                        if (Global.shto_KokaMagSakte(dateFillimNderto))
                        {
                            MessageBox.Show("Koka e veprimit  u rregjistrua me sukses");
                        }
                        else
                        {
                            MessageBox.Show("Koka e veprimit  NUK u rregjistrua me sukses");
                        }
                    }
                    else
                    {
                        if (Global.shto_LevizjeNgaPorosi(dateFillimNderto))
                        {
                            MessageBox.Show("Koka e veprimit  u rregjistrua me sukses");
                        }
                        else
                        {
                            MessageBox.Show("Koka e veprimit  NUK u rregjistrua me sukses");
                        }
                    }
                }
                else
                {
                    if (tipLevizje == "KOKE_VEPRIMI")
                    {
                        if (Global.shto_KokaMagSakte(dateFillimNderto))
                        {
                            MessageBox.Show("Koka e veprimit  u rregjistrua me sukses");
                        }
                        else
                        {
                            MessageBox.Show("Koka e veprimit  NUK u rregjistrua me sukses");
                        }
                    }
                    else
                    {
                        if (Global.shto_LevizjeNgaPorosi(dateFillimNderto))
                        {
                            MessageBox.Show("Koka e veprimit  u rregjistrua me sukses");
                        }
                        else
                        {
                            MessageBox.Show("Koka e veprimit  NUK u rregjistrua me sukses");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koka e veprimit  NUK u rregjistrua me sukses");
                Log.LogData("rregjistrimKallezim_Click", ex.Message);
                return false;
            }
        }
        public void callGridTrupVeprime()
        {
            try
            {
                Global.fillGridWithRef(ref dgTrupVeprimi ,Global.localConn,
                "SELECT wProducts.ProductName as Produkt,wMovStatuses.MovStatusName as Levizje,a.[MovDetID] " +
                  " ,a.[ProductNav] as KodProd " +
                  " ,a.[QtyX] as Sasi" +
                  " ,a.[ProductPrice] as Cmim " +
                  " ,a.[MovHeadID] " +
                  " ,a.[ProductID] " +
                  " ,a.[MovStatusID] " +
                  " ,a.[LotID] " +
                  " ,a.[LotNr] " +
                  " ,a.[BarcodeX] " +
                  " ,a.[PackX] " +
                  " ,a.[UnitsPackX] " +
                  " ,a.[PackNrX] " +
                  " ,a.[MovDetNotes] " +
                  " FROM [dbo].[wMovDetails]  as a inner join dbo.wProducts ON a.ProductID = wProducts.ProductID  " +
                  " inner join dbo.wMovStatuses ON a.MovStatusID = dbo.wMovStatuses.MovStatusID " +
                  " where MovHeadID = '" + Global.idVeprimi + "'",
                "", "Text");

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgTrupVeprimi.Columns["MovHeadID"].Visible = false;
                dgTrupVeprimi.Columns["MovDetID"].Visible = false;
                dgTrupVeprimi.Columns["ProductID"].Visible = false;
                dgTrupVeprimi.Columns["MovStatusID"].Visible = false;
                dgTrupVeprimi.Columns["MovDetNotes"].Visible = false;
                dgTrupVeprimi.Columns["PackX"].Visible = false;

                dgTrupVeprimi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgTrupVeprimi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgTrupVeprimi.RowTemplate.Height = 30;
                dgTrupVeprimi.ForeColor = lblForeColor12;
                dgTrupVeprimi.BackgroundColor = formBackColorAll;
                dgTrupVeprimi.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                dgTrupVeprimi.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgTrupVeprimi.RowsDefaultCellStyle.BackColor = formBackColorAll;
                dgTrupVeprimi.Font = new Font("Century Gothic", 10);
                dgTrupVeprimi.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        private void btnShkresa_Click(object sender, EventArgs e)
        {
            try
            {
                Global.idTrupVeprimi = 0;
                if (Global.idVeprimi == 0)
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

                    Global.levizjeMagazina.Hide();
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
            Global.levizjeMagazina.Hide();
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
                Global.levizjeMagazina.Hide();
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
        public void boshatis()
        {
            txtShenime.Text = "";
            txtIKallezuari.Text = "";
            txtNrPorosie.Text = "";
            txtNrLevizje.Text = "";
            //cmbPorosiPrind.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;// DateTime.Today.AddDays(-1);
            dtpAlert.Value = DateTime.Today;// DateTime.Today.AddDays(-1);
            chkAktiv.Checked = true;
            txtAlert.Text = "";
            //txtNrLevizje.Focus();
            //callGridTrupVeprime();
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
                    txtNrLevizje.Text = dtTblKallezim.Rows[0][1].ToString();
                    dtpDate.Value = Convert.ToDateTime(dtTblKallezim.Rows[0][2].ToString());// DateTime.Today.AddDays(-1);
                    txtNrPorosie.Text = dtTblKallezim.Rows[0][4].ToString();
                    txtIKallezuari.Text = dtTblKallezim.Rows[0][5].ToString();
                    txtShenime.Text = dtTblKallezim.Rows[0][6].ToString();
                    txtAlert.Text = dtTblKallezim.Rows[0][10].ToString();
                    cmbPorosiPrind.SelectedValue = dtTblKallezim.Rows[0][12].ToString();
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
        public void fill_GrideHyrjeNav(string idVeprimi)
        {
            try
            {
                DataTable dtTblKallezim = Global.fillGridWithRef(ref dgHyrjeNav, Global.localConn,
                    "SELECT [MovHeadID] " +
                      " ,[OrderID] " +
                      " ,[ConsumerID] " +
                      " ,[MovDetID] " +
                      " ,[AreaID] " +
                      " ,[ProductID] " +
                      " ,[LotID] " +
                      " ,[MovStatusID] " +
                      " ,[WarehouseID] " +
                      " ,[UserID] " +
                      " ,[MovCatID] " +
                      " ,[MovCatCode] " +
                      " ,[MovCatName] " +
                      " ,[RoleID] " +
                      " ,[WarehouseName] " +
                      " ,[WarehouseCode] " +
                      " ,[MovStatusName] " +
                      " ,[ProductNav] " +
                      " ,[ProductName] " +
                      " ,[OrderDetail_MovStatusID] " +
                      " ,[OrderDetail_MovStatusName] " +
                      " ,[MovHeadTime] " +
                      " ,[MovHeadNr] " +
                      " ,[AreaCode] " +
                      " ,[AreaName] " +
                  " FROM [dbo].[order_for_grid_full] where MovHeadID = " + Global.idVeprimi.ToString(), "", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err fill_GrideHyrjeNav " + ex.Message);
            }
        }
        public void fillKokeVeprimById(string idVeprimi)
        {
            try
            {
                DataTable dtblKokeVeprimi = Global.returnTableForGrid(Global.localConn,
                    "SELECT [MovHeadID],[OrderID],[ConsumerID],[AreaID],[MovStatusID],[WarehouseID],[UserID] " +
                    " ,[MovCatID],[MovCatCode],[MovCatName],[RoleID],[WarehouseName] " + 
                    " ,[WarehouseCode],[MovStatusName],[MovHeadTime],[MovHeadNr],[AreaCode],[AreaName] " +
                    " ,[ConsumerMobNr],[ConsumerName],[ConsumerAddress],[OrderNr],[PaymentInfo],[VAT_Nr],[OrderNetTotal],MovHeadNotes " +
                " FROM order_for_grid_ListeFatura where MovHeadID = " + Global.idVeprimi.ToString(),
                "", "Text", null, "Text");

                if (dtblKokeVeprimi != null && dtblKokeVeprimi.Rows.Count > 0)
                {
                    Global.idVeprimi = Convert.ToInt32(dtblKokeVeprimi.Rows[0][0].ToString()) ;
                    cmbCatMov.SelectedValue = dtblKokeVeprimi.Rows[0][7].ToString();
                    cmbStatus.SelectedValue = dtblKokeVeprimi.Rows[0][4].ToString();
                    cmbWarehouse.SelectedValue = dtblKokeVeprimi.Rows[0][5].ToString();
                    txtNrLevizje.Text = dtblKokeVeprimi.Rows[0][15].ToString();
                    dtpDate.Value = Convert.ToDateTime(dtblKokeVeprimi.Rows[0][14].ToString());
                    txtShenime.Text = dtblKokeVeprimi.Rows[0][25].ToString();
                    //if (dtblKokeVeprimi.Rows[0][13].ToString() == "1") { chkAktiv.Checked = true; } else { chkAktiv.Checked = false; }
                }
                else
                {
                    boshatis();
                }
                callGridTrupVeprime();
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

                    Global.levizjeMagazina.Hide();
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

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void cmbPorosiPrind_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void btnHome_Click_1(object sender, EventArgs e)
        {
            //Application.Exit();
            if (Global.listeFatura == null )
            {
                Global.listeFatura = new ListeFatura(); 
            }
            Global.listeFatura.callGridUpdate("");
            Global.levizjeMagazina.Hide();
            Global.listeFatura.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cmbProdukti_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ((int)cmbProdukti.SelectedValue > -1)
            //{
            if (string.IsNullOrEmpty(cmbProdukti.Text))
            {
                MessageBox.Show("No Item is Selected");
            }
            else
            {
                string prodId = cmbProdukti.SelectedValue.ToString();
                try
                {

                    if (prodId != "" && prodId != "0" && prodId != "System.Data.DataRowView")
                    {
                        DataTable dtblProdukt = Global.returnTableForGrid(Global.localConn,
                       " SELECT [ProductID],[ProdNavID] ,[DepartmentID] ,[WarehouseID],[ProdSubCatID],[ProductNav],[UnitsPack],[ProductBarcode],[ProductNotes] " +
                       " ,[PacksNr],[ProductName],[ProductWebNameAL],[ProductWebNameEN],[ProductPrice],[ProductSTOCK],[ProductActive],[ProductTS] " +
                       " FROM [dbo].[wProducts] where ProductID = " + prodId,
                       "", "Text", null, "Text");

                        if (dtblProdukt != null && dtblProdukt.Rows.Count > 0)
                        {
                            txtProductNav.Text = dtblProdukt.Rows[0][5].ToString();


                            txtBarkodx.Text = dtblProdukt.Rows[0][7].ToString();

                            txtUnitPack.Text = dtblProdukt.Rows[0][6].ToString();
                            if (txtUnitPack.Text == "") { txtUnitPack.Text = "0"; }
                            txtPackX.Text = dtblProdukt.Rows[0][9].ToString();
                            if (txtPackX.Text == "") { txtPackX.Text = "0"; }
                            txtPackNr.Text = dtblProdukt.Rows[0][9].ToString();
                            if (txtPackNr.Text == "") { txtPackX.Text = "0"; }
                            txtCmim.Text = Convert.ToDouble(dtblProdukt.Rows[0][13].ToString()).ToString();
                            //cmbArea.SelectedValue = Convert.ToDateTime(dtblProdukt.Rows[0][2].ToString());// DateTime.Today.AddDays(-1);
                        }
                        txtSasi.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Err cmbPorosiPrind_SelectedIndexChanged_1 " + ex.Message);
                }
            //}
                // MessageBox.Show("Item Selected is:" + cmbProdukti.Text);
            }
        }

        private void btnShtoProdukt_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Global.IsNumeric(txtSasi.Text) || Convert.ToInt32(txtSasi.Text) == 0)
                {
                    MessageBox.Show("Sasia duhet te jete vlere numerike me e madhe se 0 !");
                    txtSasi.Text = "0";
                    return;
                }
                if (!Global.IsNumeric(txtUnitPack.Text) || Convert.ToInt32(txtUnitPack.Text) == 0)
                {
                    MessageBox.Show("UnitPack duhet te jete vlere numerike !");
                    txtUnitPack.Text = "0";
                    return;
                }
              
                Global.idTrupVeprimi = 0;
                Global.orderDetailProdId = Convert.ToInt32(cmbProdukti.SelectedValue.ToString());
                Global.orderDetailsMovStatusId = Convert.ToInt32(cmbStatusTrupfature.SelectedValue.ToString());
                if (cmbLot.SelectedIndex > -1)
                {
                    Global.orderDetailLotId = Convert.ToInt32(cmbLot.SelectedValue.ToString());
                }
                else
                {
                    Global.orderDetailLotId = 0;
                }
                    
                Global.orderDetailProdNav = (txtProductNav.Text);
                Global.orderDetailLotNr = (txtLotNr.Text);
                Global.orderDetailBarcode = (txtBarkodx.Text);
                Global.orderDetailPackX = true;
                Global.orderDetailUnitPack =Convert.ToInt32(txtUnitPack.Text) ;

                Global.orderDetailPackNrX = Convert.ToInt32(txtPackX.Text);

                Global.orderDetailQuantity = Convert.ToInt32(txtSasi.Text);
                Global.orderDetailPrice = Convert.ToDouble(txtCmim.Text);
                Global.orderDetailNotes = (txtShenimeProd.Text);

                if (Global.idVeprimi == 0)
                {
                    if (Global.shto_TrupiMagSakte(""))
                    {
                        MessageBox.Show("Koka e veprimit  u rregjistrua me sukses");
                    }
                    else
                    {
                        MessageBox.Show("Koka e veprimit  NUK u rregjistrua me sukses");
                    }
                }
                else
                {
                    if (Global.shto_TrupiMagSakte(""))
                    {
                        MessageBox.Show("Trupi i veprimit  u rregjistrua me sukses");
                    }
                    else
                    {
                        MessageBox.Show("Trupi i veprimit  NUK u rregjistrua me sukses");
                    }
                }
                callGridTrupVeprime();
                boshatisTrupVeprimi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Trupi e veprimit  NUK u rregjistrua me sukses");
                Log.LogData("rregjistrimKallezim_Click", ex.Message);
            }
        }
        public bool boshatisTrupVeprimi()
        {
            //cmbProdukti.SelectedValue = -1;
            //cmbStatusTrupfature.SelectedValue = -1;
            //cmbLot.SelectedValue = -1;
            txtProductNav.Text = "";
            txtLotNr.Text = "";
            txtBarkodx.Text = "";
            Global.orderDetailPackX = true;
            txtUnitPack.Text = "0";
            txtPackX.Text = "0";
            txtSasi.Text = "0";
            txtCmim.Text = "0";
            txtShenimeProd.Text = "";

            return true;
        }

        private void cmbPorosiPrind_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string porosiId = cmbPorosiPrind.SelectedValue.ToString();

                if (porosiId != null && porosiId != "System.Data.DataRowView" && cmbPorosiPrind.SelectedIndex != -1)
                {
                    try
                    {
                        DataTable dtblPorosiPrind = Global.fillGridWithRef(ref dgHyrjeNav, Global.localConn,
                   " SELECT [MovHeadID],[OrderID],[ConsumerID],[MovDetID],[AreaID],[ProductID],[LotID],[MovStatusID],[WarehouseID],[UserID],[MovCatID],[MovCatCode],[MovCatName]," +
                   " [RoleID],[WarehouseName],[WarehouseCode],[MovStatusName],[ProductNav],[ProductName],[QtyX],[ProductPrice],[OrderDetail_MovStatusID],[OrderDetail_MovStatusName], [PackX] " +
                   " ,[UnitsPackX] " +
                   " [MovHeadTime],[MovHeadNr],[AreaCode],[AreaName]  FROM [dbo].[order_for_grid_full] where MovHeadID = " + porosiId,
                   "", "Text");

                        Global.addButtonToGridWithRef(ref dgHyrjeNav, "Zberthe ne Mag", 25);

                    dgHyrjeNav.Columns["MovHeadID"].Visible = false;
                    dgHyrjeNav.Columns["OrderID"].Visible = false;
                    dgHyrjeNav.Columns["ConsumerID"].Visible = false;
                    dgHyrjeNav.Columns["MovDetID"].Visible = false;

                    dgHyrjeNav.Columns["AreaID"].Visible = false;
                    dgHyrjeNav.Columns["ProductID"].Visible = false;
                    dgHyrjeNav.Columns["LotID"].Visible = false;
                    dgHyrjeNav.Columns["MovStatusID"].Visible = false;
                    dgHyrjeNav.Columns["WarehouseID"].Visible = false;
                    dgHyrjeNav.Columns["UserID"].Visible = false;
                    dgHyrjeNav.Columns["MovCatID"].Visible = false;
                    dgHyrjeNav.Columns["MovCatCode"].Visible = false;
                    dgHyrjeNav.Columns["RoleID"].Visible = false;
                    dgHyrjeNav.Columns["WarehouseCode"].Visible = false;
                    dgHyrjeNav.Columns["OrderDetail_MovStatusID"].Visible = false;
                    dgHyrjeNav.Columns["OrderDetail_MovStatusName"].Visible = false;
                    dgHyrjeNav.Columns["RoleID"].Visible = false;
                    dgHyrjeNav.Columns["MovHeadTime"].Visible = false;
                    dgHyrjeNav.Columns["MovHeadNr"].Visible = false;
                    dgHyrjeNav.Columns["AreaCode"].Visible = false;
                    dgHyrjeNav.Columns["AreaName"].Visible = false;
                  
                }
                catch (Exception ex)
                    {
                        MessageBox.Show("Err cmbPorosiPrind_SelectedIndexChanged_1 " + ex.Message);
                    }
                }
          
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (Global.listeFatura == null)
            {
                Global.listeFatura = new ListeFatura();
            }
            Global.listeFatura.callGridUpdate("");
            Global.furnizim.Hide();
            Global.listeFatura.Show();
        }

        private void dgHyrjeNav_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgHyrjeNav.Columns["Fshi"].Index)
                {
                    //if (dgHyrjeNav.SelectedCells.Count > 0)
                    //{
                    //    int selectedrowindex = dgHyrjeNav.SelectedCells[0].RowIndex;
                    //    DataGridViewRow selectedRow = dgHyrjeNav.Rows[selectedrowindex];
                    //    Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["Kid"].Value.ToString());
                    //    //Global.veprimKallezim.fillVeprimById(Global.idVeprimi.ToString());
                    //    Global.callSqlCommand(Global.localConn, "update veprim set aktiv = 0 where kid = '" + Global.idVeprimi + "'", "Text", "Execute", null);
                    //}
                    //MessageBox.Show("Ceshtja u fshi", "Fshi Ceshtje");
                    //callGridUpdate(txtCeshtjeKerko.Text);
                    //callGridAlert("");
                }
                else if (e.ColumnIndex == dgHyrjeNav.Columns["Zberthe ne Mag"].Index)
                {
                    if (dgHyrjeNav.SelectedCells.Count > 0)
                    {

                        int selectedrowindex = dgHyrjeNav.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgHyrjeNav.Rows[selectedrowindex];
                        Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["MovDetID"].Value.ToString());
                        if (Global.levizjeMagazina == null)
                        {
                            Global.listeFatura.Hide();
                            Global.levizjeMagazina = new AgnaWhms.LevizjeMagazina();
                        }
                        Global.levizjeMagazina.fillKokeVeprimById(Global.idVeprimi.ToString());
                        Global.levizjeMagazina.Show();
                    }
                }
                else if (e.ColumnIndex == dgHyrjeNav.Columns["Lexo"].Index)
                {
                    //if (dgHyrjeNav.SelectedCells.Count > 0)
                    //{
                    //    int selectedrowindex = dgHyrjeNav.SelectedCells[0].RowIndex;
                    //    DataGridViewRow selectedRow = dgHyrjeNav.Rows[selectedrowindex];
                    //    Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["Kid"].Value.ToString());
                    //    showAllInfo();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("dgHyrjeNav_CellContentClick err " + ex.Message);
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void txtCopPaketeMidis_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
