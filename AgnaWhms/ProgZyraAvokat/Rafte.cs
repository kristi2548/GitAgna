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
    public partial class Rafte : Form
    {
        public Int32 nrProduktePerHyrje = 0;
        public Rafte()
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
        public void fillComboItems(ref ComboBox myCombo)
        {
            string currItem = "";
            myCombo.Items.Clear();
            myCombo.Items.Add(string.Empty);
            for (int i = 1; i <= 999; i++)
            {
                if (i.ToString().Length == 1 ) { currItem = "00" + i.ToString(); }
                if (i.ToString().Length == 2) { currItem = "0" + i.ToString(); }
                else { currItem = i.ToString(); }
                myCombo.Items.Add(currItem);
            }
        }
        public void levizjeMagazinaInit()
        {
            try
            {
                fillCombo();

                //fillComboItems(ref cmbX);

                ApplicationLookAndFeel.UseTheme(this, 12);
                dtpDate.Format = DateTimePickerFormat.Custom;
                dtpDate.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";

                dtpLotNr.Format = DateTimePickerFormat.Custom;
                dtpLotNr.CustomFormat = "yy/MM/dd";

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

                Global.fillCombo(ref cmbX, Global.localConn,
               "SELECT [CellValueId],[CellValue] FROM [dbo].[wCellValues]", "CellValue", "CellValueId");
                cmbX.SelectedIndex = -1;

                Global.fillCombo(ref cmbY, Global.localConn,
             "SELECT [CellValueId],[CellValue] FROM [dbo].[wCellValues]", "CellValue", "CellValueId");
                cmbY.SelectedIndex = -1;

                Global.fillCombo(ref cmbZ, Global.localConn,
             "SELECT [CellValueId],[CellValue] FROM [dbo].[wCellValues]", "CellValue", "CellValueId");
                cmbZ.SelectedIndex = -1;

            //    Global.fillCombo(ref cmb, Global.localConn,
            //"SELECT [CellValueId],[CellValue] FROM [dbo].[wCellValues]", "CellValue", "CellValueId");
            //    cmbZ.SelectedIndex = -1;


                Global.fillCombo(ref cmbPorosiPrind, Global.localConn,
               "SELECT [MovHeadID],[OrderNr] + '-' + cast([MovHeadTime] as Varchar(20)) as OrderInfo FROM [dbo].[wMovHeads] where MovStatusID = 1 and Aktiv = 1", "OrderInfo", "MovHeadID");
                cmbPorosiPrind.SelectedIndex = -1;

                Global.fillCombo(ref cmbCatMov, Global.localConn,
               "SELECT [MovCatID],[MovCatCode] + '-' + [MovCatName] as LLojLevizje FROM [wMovCategs]", "LLojLevizje", "MovCatID");
                cmbCatMov.SelectedValue = 1;
                Global.fillCombo(ref cmbCellCatMov, Global.localConn,
               "SELECT [MovCatID],[MovCatCode] + '-' + [MovCatName] as LLojLevizje FROM [wMovCategs]", "LLojLevizje", "MovCatID");
                cmbCellCatMov.SelectedValue = 1;

                Global.fillCombo(ref cmbStatus, Global.localConn,
               "SELECT [MovStatusID],[MovStatusName] + '-' + MovStatusNotes as Status FROM [dbo].[wMovStatuses]", "Status", "MovStatusID");
                cmbStatus.SelectedValue = 2;
                Global.fillCombo(ref cmbCellStatus, Global.localConn,
               "SELECT [MovStatusID],[MovStatusName] + '-' + MovStatusNotes as Status FROM [dbo].[wMovStatuses]", "Status", "MovStatusID");
                cmbCellStatus.SelectedValue = 2;

                Global.fillCombo(ref cmbWarehouse, Global.localConn,
                "SELECT [WarehouseID],[WarehouseCode] + '-' + [WarehouseName] as Magazina FROM [warehouses]", "Magazina", "WarehouseID");
                cmbWarehouse.SelectedValue = 9;

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
                cmbStatusTrupfature.SelectedValue = 2;
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
                Global.orderMovStatusId = Convert.ToInt32(cmbStatus.SelectedValue.ToString());// 2;//Hyrje = Po, Vendosje ne Raft = Jo Convert.ToInt32(cmbStatus.SelectedValue.ToString());
                Global.orderMovCatId = Convert.ToInt32(cmbCatMov.SelectedValue.ToString());// 1;//Hyrje Convert.ToInt32(cmbCatMov.SelectedValue.ToString());
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
                            //MessageBox.Show("Koka e veprimit  u rregjistrua me sukses");
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
                            //MessageBox.Show("Koka e veprimit  u rregjistrua me sukses");
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
                            //MessageBox.Show("Koka e veprimit  u rregjistrua me sukses");
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
                            //MessageBox.Show("Koka e veprimit  u rregjistrua me sukses");
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
                "SELECT [MovHeadID] " +
                  " ,[MovCatID] " +
                  " ,[MovDetID] " +
                  " ,[ProductID] " +
                 "  ,[CellID] " +
                 "  ,[WarehouseID] " +
                "   ,[CellX] " +
               "    ,[CellY] " +
               "    ,[CellZ] " +
               "    ,[CellW] " +
               "    ,[CellTS] " +
               "    ,ProductName,[Qty] " +
               " ,[MovStatusIDCell] " +
               "   ,[MovCatICell] " +
              "     ,[LotNr] " +
              " FROM [dbo].[order_rafte_full] " +
                  " where MovHeadID = '" + Global.idVeprimi + "'",
                "", "Text");

                //Global.addButtonToGridWithRef(ref dgTrupVeprimi, "Fshi", 15);
                //Global.addButtonToGridWithRef(ref dgTrupVeprimi, "Edito", 16);

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgTrupVeprimi.Columns["MovHeadID"].Visible = false;
                dgTrupVeprimi.Columns["MovDetID"].Visible = false;
                dgTrupVeprimi.Columns["ProductID"].Visible = false;
                dgTrupVeprimi.Columns["MovStatusIDCell"].Visible = false;
                dgTrupVeprimi.Columns["MovCatICell"].Visible = false;
                dgTrupVeprimi.Columns["CellID"].Visible = false;
                dgTrupVeprimi.Columns["WarehouseID"].Visible = false;

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
                MessageBox.Show("callGridTrupVeprime " + ex.Message);
            }
        }
        #endregion
        public void boshatis()
        {
            txtShenime.Text = "";
            txtIKallezuari.Text = "";
            txtNrLevizje.Text = "";
            //cmbPorosiPrind.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;// DateTime.Today.AddDays(-1);
            dtpAlert.Value = DateTime.Today;// DateTime.Today.AddDays(-1);
            chkAktiv.Checked = true;
            txtNrLevizje.Text = Global.returnValForQuery("select (isnull(max([MovHeadID]),0) + 1) as NrPorosi from [wMovHeads]", Global.localConn);
            txtAlert.Text = "";
            txtPackNr.Text = "1";

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
                       " ,[OrderDetail_MovStatusName] " +
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
            
        }

        private void btnShtoProdukt_Click(object sender, EventArgs e)
        {
           
        }
        public bool shtoProdukte()
        {
            try
            {
                if (cmbProdukti.SelectedIndex == -1)
                {
                    MessageBox.Show("Nuk keni zgjedhur produkt nga lista siper !");
                    cmbProdukti.Focus();
                }
                if (!Global.IsNumeric(txtSasiPack.Text) || Convert.ToInt32(txtSasiPack.Text) == 0)
                {
                    MessageBox.Show("Sasia duhet te jete vlere numerike me e madhe se 0 !");
                    txtSasiPack.Text = "0";
                    return false;
                }
                if (!Global.IsNumeric(txtUnitPack.Text) || Convert.ToInt32(txtUnitPack.Text) == 0)
                {
                    MessageBox.Show("UnitPack duhet te jete vlere numerike !");
                    txtUnitPack.Text = "0";
                    return false;
                }

                Global.idTrupVeprimi = 0;
                Global.orderDetailProdId = Convert.ToInt32(cmbProdukti.SelectedValue.ToString());
                Global.orderDetailsMovStatusId = Convert.ToInt32(cmbStatus.SelectedValue.ToString());
                if (cmbLot.SelectedIndex > -1)
                {
                    Global.orderDetailLotId = Convert.ToInt32(cmbLot.SelectedValue.ToString());
                }
                else
                {
                    Global.orderDetailLotId = 0;
                }

                Global.orderDetailProdNav = (txtProductNav.Text);
                Global.orderDetailLotNr = (dtpLotNr.Text);
                Global.orderDetailBarcode = (txtBarkodx.Text);
                Global.orderDetailPackX = true;
                Global.orderDetailUnitPack = Convert.ToInt32(txtUnitPack.Text);

                Global.orderDetailPackNrX = Convert.ToInt32(txtPackX.Text);

                Global.orderDetailQuantity = Convert.ToInt32(txtSasiPack.Text);
                Global.orderDetailPrice = Convert.ToDouble(txtCmim.Text);
                Global.orderDetailNotes = (txtShenimeProd.Text);
                if (chkAktiv.Checked) { Global.AKTIV_TRUPI_HYRJE = 1; } else { Global.AKTIV_TRUPI_HYRJE = 0; }

                if (Global.idVeprimi == 0)
                {
                    if (Global.shto_TrupiMagSakte(""))
                    {
                        //MessageBox.Show("Produkti u shtua me sukses");
                        nrProduktePerHyrje = nrProduktePerHyrje + 1;
                    }
                    else
                    {
                        MessageBox.Show("Produkti NUK u rregjistrua me sukses");
                    }
                }
                else
                {
                    if (Global.shto_TrupiMagSakte(""))
                    {
                        nrProduktePerHyrje = nrProduktePerHyrje + 1;
                    }
                    else
                    {
                        MessageBox.Show("Produkti  NUK u rregjistrua me sukses");
                    }
                }
                callGridTrupVeprime();
                boshatisTrupVeprimi();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("shtoProdukte  NUK u rregjistrua me sukses");
                Log.LogData("shtoProdukte", ex.Message);
                return false ;
            }
        }
        public bool boshatisTrupVeprimi()
        {
            //cmbProdukti.SelectedValue = -1;
            //cmbStatusTrupfature.SelectedValue = -1;
            //cmbLot.SelectedValue = -1;
            txtProductNav.Text = "";
            //txtLotNr.Text = "";
            txtBarkodx.Text = "";
            Global.orderDetailPackX = true;
            txtUnitPack.Text = "0";
            txtPackX.Text = "0";
            txtSasiPack.Text = "0";
            txtCmim.Text = "0";
            txtShenimeProd.Text = "";
            cmbProdukti.SelectedIndex = -1;
            chkTrupHyrjeAktiv.Checked = true;
            dgTrupVeprimi.AllowUserToAddRows = false;
            dgHyrjeNav.AllowUserToAddRows = false;
            cmbCellCatMov.SelectedIndex = -1;
            cmbCellStatus.SelectedIndex = -1;
            txtCellQty.Text = "0";
            cmbX.SelectedIndex = -1;
            cmbY.SelectedIndex = -1;
            cmbZ.SelectedIndex = -1;
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
                       " [RoleID],[WarehouseName],[WarehouseCode],[MovStatusName],[ProductNav],[ProductName],[QtyX],[ProductPrice],[OrderDetail_MovStatusID], " +
                       "  [OrderDetail_MovStatusName],[UnitsPackX] as UnitsPack, [PackX],BarcodeX as Barcode," +
                       " [MovHeadTime],[MovHeadNr],[AreaCode],[AreaName]  FROM [dbo].[order_for_grid_full] where MovHeadID = " + porosiId,
                       "", "Text");
                        Global.idVeprimi = Convert.ToInt32(porosiId) ;
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
            Global.rafte.Hide();
            Global.listeFatura.Show();
        }

        private void dgHyrjeNav_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //DataTable dtblPorosiPrind = Global.fillGridWithRef(ref dgHyrjeNav, Global.localConn,
                //     " SELECT [MovHeadID],[OrderID],[ConsumerID],[MovDetID],[AreaID],[ProductID],[LotID],[MovStatusID],[WarehouseID],[UserID],[MovCatID],[MovCatCode],[MovCatName]," +
                //     " [RoleID],[WarehouseName],[WarehouseCode],[MovStatusName],[ProductNav],[ProductName],[QtyX],[ProductPrice]" +
                //     " ,[OrderDetail_MovStatusID],[OrderDetail_MovStatusName],[UnitsPackX] as UnitsPack, [PackX]  " +
                //     " [MovHeadTime],[MovHeadNr],[AreaCode],[AreaName]  FROM [dbo].[order_for_grid_full] where MovHeadID = " + porosiId,
                //     "", "Text");
                //if (e.ColumnIndex == dgHyrjeNav.Columns["Zberthe ne Mag"].Index)
                //{
                    if (dgHyrjeNav.SelectedCells.Count > 0)
                    {
                        int selectedrowindex = dgHyrjeNav.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgHyrjeNav.Rows[selectedrowindex];
                        cmbProdukti.SelectedValue = selectedRow.Cells["ProductID"].Value.ToString();
                        if (selectedRow.Cells["UnitsPack"].Value != null)
                        {
                            txtUnitPack.Text = selectedRow.Cells["UnitsPack"].Value.ToString();
                        }
                        
                        txtSasiPack.Text = selectedRow.Cells["QtyX"].Value.ToString();
                        if (Global.IsNumeric(txtUnitPack.Text) && Global.IsNumeric(txtSasiPack.Text))
                        {
                            txtSasiCope.Text = (Convert.ToInt32(txtSasiPack.Text) * Convert.ToInt32(txtUnitPack.Text)).ToString();
                        }
                        else
                        {
                            MessageBox.Show("Kontrolloni Hyrje per kete produkt");
                            txtSasiCope.Text = "0";
                        }
                        txtProductNav.Text = selectedRow.Cells["ProductNav"].Value.ToString();
                        txtBarkodx.Text = selectedRow.Cells["Barcode"].Value.ToString();
                        txtCmim.Text = selectedRow.Cells["ProductPrice"].Value.ToString();
                    }
                //}
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

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnHyrje_Click(object sender, EventArgs e)
        {

            string cellId = Global.returnValForQuery("SELECT [CellID] FROM [dbo].[wCells] where CellX = '" + cmbX.Text.ToString() +
                " ' and [CellY] = '" + cmbY.Text.ToString() + "' and [CellZ] = '" + cmbZ.Text.ToString() + "'", Global.localConn);

            if (cmbX.SelectedIndex != -1 && cmbY.SelectedIndex != -1 && cmbZ.SelectedIndex != -1)
            {
                cellId = Global.returnValForQuery("SELECT [CellID] FROM [dbo].[wCells] where CellX = '" + cmbX.Text.ToString() +
                " ' and [CellY] = '" + cmbY.Text.ToString() + "' and [CellZ] = '" + cmbZ.Text.ToString() + "'", Global.localConn);
            }
            else
            {
                if (cmbX.SelectedIndex == -1) { cmbX.Focus(); MessageBox.Show("Plotesoni X"); }
                if (cmbZ.SelectedIndex == -1) { cmbZ.Focus(); MessageBox.Show("Plotesoni Z"); }
                if (cmbY.SelectedIndex == -1) { cmbY.Focus(); MessageBox.Show("Plotesoni Y"); }
            }

            if (cellId == null || cellId == "" || cellId == "0")
            {
                Global.cellWarehouseID = Convert.ToInt32(cmbWarehouse.SelectedValue.ToString());
                Global.cellX = cmbX.Text;
                Global.cellY = cmbY.Text;
                Global.cellZ= cmbZ.Text;
                Global.cellW = "000";
                Global.cellNotes = txtShenimeProd.Text;
                Global.cellDataTime = dtpDate.Text;
                
                Global.shto_Cells("");
            }
            else
            {
                Global.cellId = Convert.ToInt32(cellId) ;
            }
            Global.movementCeId = 0;
            Global.movDetId = Global.idTrupVeprimi;
            Global.moveCellId = Global.cellId;
            Global.moveQty = Convert.ToInt32(txtCellQty.Text);
            if (cmbCellStatus.SelectedIndex > -1 )
            {
                Global.movStatusId = Convert.ToInt32(cmbCellStatus.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("Plotesoni status ");
                cmbCellStatus.Focus();
            }

            if (cmbCellCatMov.SelectedIndex > -1)
            {
                Global.movCatId = Convert.ToInt32(cmbCellCatMov.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("Plotesoni kategori levizje ");
                cmbCellCatMov.Focus();
            }
            Global.shto_MovCells("");

            callGridTrupVeprime();
            boshatisTrupVeprimi();
        }

        private void cmbProdukti_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if ((int)cmbProdukti.SelectedValue > -1)
            //{
            if (string.IsNullOrEmpty(cmbProdukti.Text))
            {
                //MessageBox.Show("No Item is Selected");
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
                        txtSasiPack.Focus();
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

        private void dgHyrjeNav_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgHyrjeNav.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgHyrjeNav.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgHyrjeNav.Rows[selectedrowindex];
                Global.idTrupVeprimi = Convert.ToInt32(selectedRow.Cells["MovDetID"].Value.ToString());
                cmbProdukti.SelectedValue = selectedRow.Cells["ProductID"].Value.ToString();
                if (selectedRow.Cells["UnitsPack"].Value != null)
                {
                    txtUnitPack.Text = selectedRow.Cells["UnitsPack"].Value.ToString();
                }

                txtSasiPack.Text = selectedRow.Cells["QtyX"].Value.ToString();
                if (Global.IsNumeric(txtUnitPack.Text) && Global.IsNumeric(txtSasiPack.Text))
                {
                    txtSasiCope.Text = (Convert.ToInt32(txtSasiPack.Text) * Convert.ToInt32(txtUnitPack.Text)).ToString();
                }
                else
                {
                    MessageBox.Show("Kontrolloni Hyrje per kete produkt");
                    txtSasiCope.Text = "0";
                }
                txtProductNav.Text = selectedRow.Cells["ProductNav"].Value.ToString();
                txtBarkodx.Text = selectedRow.Cells["Barcode"].Value.ToString();
                txtCmim.Text = selectedRow.Cells["ProductPrice"].Value.ToString();
            }
        }

        private void dgTrupVeprimi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selProdId = 0;
            if (e.ColumnIndex == dgTrupVeprimi.Columns["Fshi"].Index)
            {
                if (dgTrupVeprimi.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dgTrupVeprimi.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgTrupVeprimi.Rows[selectedrowindex];
                    selProdId = Convert.ToInt32(selectedRow.Cells["MovDetID"].Value.ToString());
                        Global.callSqlCommand(Global.localConn, "Update wMovDetails set aktiv = 0 where MovDetID = '" + selProdId + "'","Text", "Text", null) ;
                    }
                callGridTrupVeprime();
                }
            }
        }
}
