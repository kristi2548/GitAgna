using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using ProgZyraAvokat;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace AgnaWhms
{
    public partial class ProdukteInfo : Form
    {
        SqlCommand sCommand;
        SqlDataAdapter sAdapter;
        SqlCommandBuilder sBuilder;
        SqlConnection connection;
        DataSet sDs;
        DataTable sTable;
        ComboBox selCombo = new ComboBox(); 
        string myMode = "";
        String currEntityId = "";
        public ProdukteInfo()
        {
            InitializeComponent();
            ApplicationLookAndFeel.UseTheme(this, 10);
        }
        public void LoadGrid()
        {
            Global.localConn = ConfigurationManager.ConnectionStrings["appConn"].ToString();
            string query = "SELECT * FROM wProducts";
            connection = new SqlConnection(Global.localConn);
            connection.Open();
            sCommand = new SqlCommand(query, connection);
            sAdapter = new SqlDataAdapter(sCommand);
            sBuilder = new SqlCommandBuilder(sAdapter);
            sDs = new DataSet();
            sAdapter.Fill(sDs, "Table");
            sTable = sDs.Tables["Table"];

            connection.Close();
            dataGridView1.DataSource = sTable;
            Global.addComboToGridWithRef(ref dataGridView1, "Magazina", 1,
               "SELECT [WarehouseID],[WarehouseCode] + '-' + [WarehouseName] as Magazina FROM [warehouses]", "Magazina", "WarehouseID",160);

            Global.addStyleToGrid(ref dataGridView1, "#4655A5", "#FFFFFF", "#FFFFFF", "#4655A5", "#FFFFFF");

            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;

            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           
        }
        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (selCombo != null)
                dataGridView1[e.ColumnIndex, e.RowIndex].Tag = selCombo.SelectedIndex;
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string msg = String.Format("Editing Cell at ({0}, {1})",
                e.ColumnIndex, e.RowIndex);
            this.Text = msg;
            currEntityId = this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DateTime newDate;

                switch (this.dataGridView1.Columns[e.ColumnIndex].Name)
                {
                    case "ColumnText":
                        string newText = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        break;
                    case "ColumnCombo":
                        string newPriority = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        break;
                    case "ColumnDate":
                        DateTime.TryParse(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out newDate);
                        break;
                }
            }
        }
        private void dataGridView1_EditingControlShowing(object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView?.CurrentCell?.ColumnIndex != 1) return;
            var comboBox = e.Control as DataGridViewComboBoxEditingControl;

            if (comboBox == null) return;
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            if (!true.Equals(comboBox.Tag))
            {
                comboBox.Tag = true;
                comboBox.Validating += (obj, args) =>
                {
                    var column = (DataGridViewComboBoxColumn)dataGridView.CurrentCell.OwningColumn;
                    var txt = comboBox.Text;
                    if (dataGridView1.CurrentCell.ColumnIndex == 1)
                    {
                        Global.fillComboGrid(ref column, Global.localConn,
                          "SELECT [WarehouseID],[WarehouseCode] + '-' + [WarehouseName] as Magazina FROM [warehouses] " +
                           " WHERE WarehouseID = '" + currEntityId + "'", "Magazina", "WarehouseID");
                    }
                    
                    dataGridView.CurrentCell.Value = txt;
                    dataGridView.NotifyCurrentCellDirty(true);
                };

                selCombo = comboBox as ComboBox;
            }
        }
        private void ProdukteInfo_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;
            myMode = "add";
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (myMode == "add")
            {
                sAdapter.Update(sTable);
                MessageBox.Show("Prices Are Successfully Added.", "Saved.", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else if (myMode == "edit")
            {
                string query = "UPDATE wProducts SET " +
                    "ProductPrice = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'," +
                    "ProductSTOCK = '" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "', " +
                    "WHERE ProdNavid = '" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                connection = new SqlConnection(Global.localConn);
                connection.Open();
                sCommand = new SqlCommand(query, connection);
                sAdapter = new SqlDataAdapter(sCommand);
                sBuilder = new SqlCommandBuilder(sAdapter);
                sDs = new DataSet();
                sAdapter.Fill(sDs, "Table");
                sTable = sDs.Tables["Table"];
                connection.Close();
                dataGridView1.DataSource = sTable;
                dataGridView1.ReadOnly = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string cmbWhsVal = "";
                if (e.ColumnIndex == dataGridView1.Columns["add"].Index)
                {
                    cmbWhsVal = selCombo.SelectedIndex.ToString();
                    
                }
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    currEntityId = (selectedRow.Cells["WarehouseID"].Value.ToString());
                    //Global.veprimKallezim.fillVeprimById(Global.idVeprimi.ToString());
                    //Global.callSqlCommand(Global.localConn, "update veprim set aktiv = 0 where kid = '" + Global.idVeprimi + "'", "Text", "Execute", null);
                }
                //else if (e.ColumnIndex == dataGridView1.Columns["edit"].Index)
                //{
                //    //if (dataGridView1.SelectedCells.Count > 0)
                //    //{

                //    //    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                //    //    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                //    //    Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["ProdNavid"].Value.ToString());
                //    //}
                //}
                //else if (e.ColumnIndex == dataGridView1.Columns["Lexo"].Index)
                //{
                //    //if (dataGridView1.SelectedCells.Count > 0)
                //    //{
                //    //    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                //    //    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                //    //    Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["ProdNavid"].Value.ToString());
                //    //}
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err dataGridView1_CellClick " + ex.Message);
            }
        }

        private void btnDil_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
