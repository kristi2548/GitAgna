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
using System.Configuration; // this namespace is add I am adding connection name in

namespace AgnaWhms
{
    public partial class FormeStoku : Form
    {
        public FormeStoku()
        {
            InitializeComponent();
            initStok();
        }
        public void initStok()
        {
            Global.localConn = ConfigurationManager.ConnectionStrings["appConn"].ToString();
            Global.localConnB2B = ConfigurationManager.ConnectionStrings["appConnExternal"].ToString();
            fillCombo();
            callGridsUpdates();
            ProgZyraAvokat.ApplicationLookAndFeel.UseTheme(this, 12);
        }
        public void callGridsUpdates()
        {
            callGridB2B("");
            callGridB2C("");
            callGridStockECommerce("");
            callGridStockTirana("");
        }
        public bool fillCombo()
        {
            try
            {
                Global.fillCombo(ref cmbProdukti, Global.localConn,
               "SELECT [ProductNav],[ProductNav] + '-' + [ProductName] AS Produkti FROM [wProducts]", "Produkti", "ProductNav");
                cmbProdukti.SelectedIndex = -1;
                Global.fillCombo(ref cmbDepartamenti, Global.localConn,
               "SELECT [DepartmentID],[DepartmentCode] + '-' + [DepartmentName] as Departamenti FROM [wDepartments]", "Departamenti", "DepartmentID");
                cmbDepartamenti.SelectedIndex = -1;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err fillCombo " + ex.Message);
                return false;
            }
        }
        public void callGridB2B(string productNav)
        {
            try
            {
                DataTable result = new DataTable();
                string sqlQuery = "";
                if (cmbProdukti.SelectedIndex > -1 && cmbProdukti.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    productNav = cmbProdukti.SelectedValue.ToString();
                    sqlQuery = " SELECT  " +
                " B_Head.BID, B_Head.VID, C_Movs.MID, bdtrg as Data, A_CatItems.GCD + B_Items.CD AS ProductNav, A_Departamente.DID, " +
                " A_Departamente.DNM AS Departamenti, B_Items.IID, " +
                " B_Items.NM AS ProductName,  " +
                " round(Sum(CASE WHEN NJESIA = 'COPE' THEN " +
                " (CASE WHEN c_movs.iid = 38742 OR c_movs.iid = 38743 OR c_movs.iid = 38744 THEN " +
                " (C_Movs.MNR / ArtikujNjesi.KOEFICENTI) * 2 ELSE C_Movs.MNR / ArtikujNjesi.KOEFICENTI END) ELSE 0 END), 0) AS QtyCope ," +
                "  C_Movs.MPR AS Price, C_Movs.MNR * C_Movs.MPR AS Value, " +
                " B_Head.VLF, " +
                " sum(CASE WHEN KOEFICENTI = 1 THEN " +
                " (CASE WHEN c_movs.iid = 38742 OR c_movs.iid = 38743 OR c_movs.iid = 38744 THEN " +
                " (C_Movs.MNR / ArtikujNjesi.KOEFICENTI) * 2 ELSE C_Movs.MNR / ArtikujNjesi.KOEFICENTI END) ELSE 0 END) AS Qty_SalesUnit " +
                " FROM B_Head INNER JOIN " +
                " C_Movs ON B_Head.BID = C_Movs.BID INNER JOIN " +
                " B_Items ON C_Movs.IID = B_Items.IIID INNER JOIN " +
                " A_Departamente ON B_Items.DID = A_Departamente.DID INNER JOIN " +
                " A_CatItems ON B_Items.GID = A_CatItems.GGID LEFT OUTER JOIN " +
                " ArtikujNjesi ON C_Movs.IID = ArtikujNjesi.IID inner join " +
                " A_Units on B_Items.NID = A_Units.NID " +
                " WHERE (B_Head.VID = 15) AND (B_Head.VLF = 1) and " +
                " (KOEFICENTI = 1 or NJESIA = 'COPE') " +
                " and gcd + cd = '" + productNav + "' " +
                " group by B_Head.BID, B_Head.VID, C_Movs.MID, bdtrg, A_CatItems.GCD + B_Items.CD, A_Departamente.DID, " +
                " A_Departamente.DNM, B_Items.IID, " +
                " B_Items.NM, C_Movs.MNR, C_Movs.MPR, C_Movs.MNR * C_Movs.MPR, " +
                " B_Head.VLF " +
                " order by bdtrg desc";

                    result = Global.fillGridWithRef(ref dgPorosiB2b, Global.localConnB2B, sqlQuery, "", "Text");

                    if (result != null)
                    {
                        Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                        Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                        dgPorosiB2b.Columns["BID"].Visible = false;
                        dgPorosiB2b.Columns["VID"].Visible = false;
                        dgPorosiB2b.Columns["MID"].Visible = false;
                        dgPorosiB2b.Columns["DID"].Visible = false;
                        dgPorosiB2b.Columns["IID"].Visible = false;
                        dgPorosiB2b.Columns["VLF"].Visible = false;
                        dgPorosiB2b.Columns["Departamenti"].Visible = false;

                        dgPorosiB2b.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgPorosiB2b.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dgPorosiB2b.RowTemplate.Height = 30;
                        dgPorosiB2b.ForeColor = lblForeColor12;
                        dgPorosiB2b.BackgroundColor = formBackColorAll;
                        dgPorosiB2b.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                        dgPorosiB2b.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        dgPorosiB2b.RowsDefaultCellStyle.BackColor = formBackColorAll;
                        dgPorosiB2b.Font = new Font("Century Gothic", 10);
                        dgPorosiB2b.ReadOnly = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridB2B " + ex.Message);
            }
        }
        public void callGridB2C(string productNav)
        {
            try
            {
                DataTable result = new DataTable();
                if (cmbProdukti.SelectedIndex > -1 && cmbProdukti.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    productNav = cmbProdukti.SelectedValue.ToString();
                    result = Global.fillGridWithRef(ref dgPorosiB2c, Global.localConn,
                " SELECT OrderDTS,[OrderID] " +
                "   ,[OrdECID],[ConsumerID],[ConsumerMobNr],[ConsumerName] " +
                "   ,[ConsumerAddress],[OrderNr],[OrderSubmit],[OrderNetTotal],[OrderShipping] " +
                "   ,[OrderExtra],[OrderPaidAm],[VAT_Nr],[PaymentInfo],[OrderNotes] " +
                "   ,[ConsumerEM],[ConsumerTel],[OrderDetID],[ProductNAV],[ProductID],ProductName " +
                "   ,[OrderQty] as QtyCope,[OrderPrice] as Price,[UnitsPack],OrderQty / UnitsPack as Qty_SalesUnit, [OrdDetNotes],[DepartmentID],[AreaID],[AreaName],[AreaCode] " +
                "    FROM [online_orders_full] " +
                "     where ProductNAV  = '" + productNav + "' order by OrderDTS desc ",
                "", "Text");
                    if (result != null )
                    {
                        Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                        Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                        dgPorosiB2c.Columns["OrderID"].Visible = false;
                        dgPorosiB2c.Columns["OrdECID"].Visible = false;
                        dgPorosiB2c.Columns["OrderNr"].Visible = false;
                        dgPorosiB2c.Columns["ProductID"].Visible = false;
                        dgPorosiB2c.Columns["ConsumerID"].Visible = false;
                        dgPorosiB2c.Columns["ConsumerMobNr"].Visible = false;
                        dgPorosiB2c.Columns["AreaID"].Visible = false;
                        dgPorosiB2c.Columns["AreaName"].Visible = false;

                        dgPorosiB2c.Columns["OrderSubmit"].Visible = false;
                        dgPorosiB2c.Columns["OrderNetTotal"].Visible = false;
                        dgPorosiB2c.Columns["OrderShipping"].Visible = false;
                        dgPorosiB2c.Columns["OrderExtra"].Visible = false;
                        dgPorosiB2c.Columns["OrderPaidAm"].Visible = false;

                        dgPorosiB2c.Columns["VAT_Nr"].Visible = false;
                        dgPorosiB2c.Columns["PaymentInfo"].Visible = false;
                        dgPorosiB2c.Columns["OrderNotes"].Visible = false;
                        dgPorosiB2c.Columns["OrderNotes"].Visible = false;
                        dgPorosiB2c.Columns["DepartmentID"].Visible = false;
                        dgPorosiB2c.Columns["ConsumerEM"].Visible = false;

                        dgPorosiB2c.Columns["ConsumerTel"].Visible = false;
                        dgPorosiB2c.Columns["OrderDetID"].Visible = false;
                        dgPorosiB2c.Columns["ConsumerName"].Visible = false;
                        dgPorosiB2c.Columns["ConsumerAddress"].Visible = false;
                        dgPorosiB2c.Columns["ConsumerEM"].Visible = false;

                        dgPorosiB2c.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgPorosiB2c.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dgPorosiB2c.RowTemplate.Height = 30;
                        dgPorosiB2c.ForeColor = lblForeColor12;
                        dgPorosiB2c.BackgroundColor = formBackColorAll;
                        dgPorosiB2c.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                        dgPorosiB2c.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        dgPorosiB2c.RowsDefaultCellStyle.BackColor = formBackColorAll;
                        dgPorosiB2c.Font = new Font("Century Gothic", 10);
                        dgPorosiB2c.ReadOnly = true;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridB2C " + ex.Message);
            }
        }
        public void callGridStockECommerce(string productNav)
        {
            try
            {
                DataTable result = new DataTable();
                if (cmbProdukti.SelectedIndex > -1 && cmbProdukti.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    productNav = cmbProdukti.SelectedValue.ToString();
                    result = Global.fillGridWithRef(ref dgStokuECommerce, Global.localConn,
                " SELECT distinct [MovHeadID],[MovDetID] ,MovHeadTime,[ProductID],[LotID],[MovStatusID],[ProductNav],[ProductName],[QtyX] as QtyCope,[ProductPrice], " +
                " QtyX / UnitsPackX as Qty_SalesUnit, [RoleID],[WarehouseName],[WarehouseCode],[MovStatusName],[WarehouseID],[UserID],[MovCatID],[MovCatCode],[MovCatName]," +
                " [UnitsPackX],[MovDetNotes],[Aktiv],MovHeadNr, [OrderID],[ConsumerID]  " +
                " FROM [ORDER_WHMS] where ProductNav  = '" + productNav + "' order by MovHeadTime desc ",
                "", "Text");
                    if (result != null)
                    {
                        Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                        Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                        dgStokuECommerce.Columns["MovHeadID"].Visible = false;
                        dgStokuECommerce.Columns["MovDetID"].Visible = false;
                        dgStokuECommerce.Columns["ProductID"].Visible = false;
                        dgStokuECommerce.Columns["LotID"].Visible = false;
                        dgStokuECommerce.Columns["MovStatusID"].Visible = false;

                        dgStokuECommerce.Columns["RoleID"].Visible = false;
                        dgStokuECommerce.Columns["WarehouseName"].Visible = false;
                        dgStokuECommerce.Columns["WarehouseCode"].Visible = false;
                        dgStokuECommerce.Columns["MovStatusName"].Visible = false;
                        dgStokuECommerce.Columns["WarehouseID"].Visible = false;

                        dgStokuECommerce.Columns["UserID"].Visible = false;
                        dgStokuECommerce.Columns["MovCatID"].Visible = false;
                        dgStokuECommerce.Columns["MovCatCode"].Visible = false;
                        dgStokuECommerce.Columns["MovCatName"].Visible = false;
                        dgStokuECommerce.Columns["MovDetNotes"].Visible = false;

                        dgStokuECommerce.Columns["MovHeadNr"].Visible = false;
                        dgStokuECommerce.Columns["OrderID"].Visible = false;
                        dgStokuECommerce.Columns["ConsumerID"].Visible = false;
                        //dgStokuECommerce.Columns["ConsumerAddress"].Visible = false;
                        //dgStokuECommerce.Columns["ConsumerEM"].Visible = false;

                        dgStokuECommerce.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgStokuECommerce.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dgStokuECommerce.RowTemplate.Height = 30;
                        dgStokuECommerce.ForeColor = lblForeColor12;
                        dgStokuECommerce.BackgroundColor = formBackColorAll;
                        dgStokuECommerce.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                        dgStokuECommerce.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        dgStokuECommerce.RowsDefaultCellStyle.BackColor = formBackColorAll;
                        dgStokuECommerce.Font = new Font("Century Gothic", 10);
                        dgStokuECommerce.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridStockECommerce " + ex.Message);
            }
        }
        public void callGridStockTirana(string productNav)
        {
            try
            {
                DataTable result = new DataTable();
                if (cmbProdukti.SelectedIndex > -1 && cmbProdukti.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    productNav = cmbProdukti.SelectedValue.ToString();
                    result  = Global.fillGridWithRef(ref dgStokuTirana, Global.localConnStockTr,
                    " SELECT        dbo.B_Items.IID, getdate() as Date,GCD + CD AS ProductNav, dbo.B_Items.NM AS ProductName,  " +
                    " round(Sum(CASE WHEN NJESIA = 'COPE' THEN " +
                    " (CASE WHEN B_Items.IID = 38742 OR B_Items.IID = 38743 OR B_Items.IID = 38744 THEN " +
                    " (STCK / ArtikujNjesi.KOEFICENTI) * 2 ELSE STCK / ArtikujNjesi.KOEFICENTI END) ELSE 0 END), 0) AS StockQtyCope, " +
                    " dbo.B_Items.Pr1 AS CmimPakice, dbo.A_Departamente.DNM AS Departamenti, " +
                    " ROUND(sum(CASE WHEN KOEFICENTI = 1 THEN " +
                    " (CASE WHEN B_Items.IID = 38742 OR B_Items.IID = 38743 OR B_Items.IID = 38744 THEN " +
                    " (STCK / ArtikujNjesi.KOEFICENTI) * 2 ELSE STCK / ArtikujNjesi.KOEFICENTI END) ELSE 0 END), 0) AS StockQty_SalesUnit " +
                    " FROM            dbo.B_Items INNER JOIN " +
                    "             dbo.A_CatItems ON dbo.B_Items.GID = dbo.A_CatItems.GGID INNER JOIN " +
                    "             dbo.A_Departamente ON dbo.B_Items.DID = dbo.A_Departamente.DID " +
                    "             LEFT OUTER JOIN " +
                    "             ArtikujNjesi ON B_Items.IID = ArtikujNjesi.IID inner join " +
                    "             A_Units on B_Items.NID = A_Units.NID " +
                    "             where GCD + CD = '" + productNav + "' " +
                    " GROUP BY dbo.B_Items.IID, GCD + CD, dbo.B_Items.NM, " +
                    " dbo.B_Items.STCK, dbo.B_Items.Pr1, dbo.A_Departamente.DNM ",
                    "", "Text");
                    if (result != null )
                    {
                        Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");
                        Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                        dgStokuTirana.Columns["IID"].Visible = false;
                        dgStokuTirana.Columns["Departamenti"].Visible = false;
                        //dgStokuTirana.Columns["ProductID"].Visible = false;

                        dgStokuTirana.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgStokuTirana.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dgStokuTirana.RowTemplate.Height = 30;
                        dgStokuTirana.ForeColor = lblForeColor12;
                        dgStokuTirana.BackgroundColor = formBackColorAll;
                        dgStokuTirana.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                        dgStokuTirana.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        dgStokuTirana.RowsDefaultCellStyle.BackColor = formBackColorAll;
                        dgStokuTirana.Font = new Font("Century Gothic", 10);
                        dgStokuTirana.ReadOnly = true;
                    }
                    
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridStockTirana " + ex.Message);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (Global.listeFatura == null)
            {
                Global.listeFatura = new ListeFatura();
            }
            Global.listeFatura.callGridUpdate("");
            if (Global.formeStoku != null)
            {
                Global.formeStoku.Hide();
            }
            
            Global.listeFatura.Show();
        }

        private void cmbProdukti_SelectedIndexChanged(object sender, EventArgs e)
        {
            callGridsUpdates();
        }
    }
}
