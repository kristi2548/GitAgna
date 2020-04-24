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


namespace ProgZyraAvokat
{
    public partial class Form1 : Form
    {
        //private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private BindingSource bindingSource1 = new BindingSource();
        public Form1()
        {
            Global.localConn = ConfigurationManager.ConnectionStrings["appConn"].ToString();
            InitializeComponent();
            callGridUpdate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void callGridUpdate()
        {
            try
            {
                ////dgRouta.DataSource = bindingSource1;
                //Global.fillGrid(ref dgCeshtje, Global.localConn,
                //"SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID],a.[AKTIV],[KOMENTE],[KID],[LLVID] " +
                //" FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID ",
                //"", "Text");
                BindingSource bindingSource2 = new System.Windows.Forms.BindingSource();
                if (bindingSource2 == null)
                {
                    bindingSource2 = new BindingSource();
                }
                //dgTest.DataSource = bindingSource2;

                //fillGrid(ref dgCeshtje, Global.localConn,
                fillGrid(Global.localConn,
                "SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID],a.[AKTIV],[KOMENTE],[KID],[LLVID] " +
                " FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID ",
                "", "Text");

                Color lblBackColor1 = ColorTranslator.FromHtml("#22B573");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                Color lblForeColor1 = Color.White;
                lblBackColor1 = ColorTranslator.FromHtml("#22B573");///Color.FromArgb(234, 101, 148);// /234, 101, 148
                lblForeColor1 = Color.White;//#0071BC
                dgCeshtje.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgCeshtje.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgCeshtje.RowTemplate.Height = 50;
                dgCeshtje.BackgroundColor = lblBackColor1;
                dgCeshtje.AlternatingRowsDefaultCellStyle.BackColor = lblBackColor1;
                dgCeshtje.RowsDefaultCellStyle.BackColor = lblBackColor1;
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
                dgCeshtje.DataSource = bindingSource1;
                bindingSource1.DataSource = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                dgCeshtje.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
            }
        }
    }
}
