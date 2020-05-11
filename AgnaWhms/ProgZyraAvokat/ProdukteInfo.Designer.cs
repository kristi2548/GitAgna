namespace AgnaWhms
{
    partial class ProdukteInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.productIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Add = new System.Windows.Forms.DataGridViewButtonColumn();
            this.prodNavIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.departmentIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouseIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodSubCatIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNavDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitsPackDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packsNrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productWebNameALDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productWebNameENDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productSTOCKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productBarcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNotWebDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productTSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockTRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseUnitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blockedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brandDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.topOfferDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newProdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kW1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kW2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kW3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kW4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kW5DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNotesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wProductsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aB2CDataSet = new AgnaWhms.AB2CDataSet();
            this.wProductsTableAdapter = new AgnaWhms.AB2CDataSetTableAdapters.wProductsTableAdapter();
            this.btnDil = new System.Windows.Forms.Button();
            this.btnListeCeshtje = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wProductsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aB2CDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.productIDDataGridViewTextBoxColumn,
            this.Add,
            this.prodNavIDDataGridViewTextBoxColumn,
            this.departmentIDDataGridViewTextBoxColumn,
            this.warehouseIDDataGridViewTextBoxColumn,
            this.prodSubCatIDDataGridViewTextBoxColumn,
            this.productNavDataGridViewTextBoxColumn,
            this.unitsPackDataGridViewTextBoxColumn,
            this.packsNrDataGridViewTextBoxColumn,
            this.productNameDataGridViewTextBoxColumn,
            this.productWebNameALDataGridViewTextBoxColumn,
            this.productWebNameENDataGridViewTextBoxColumn,
            this.productPriceDataGridViewTextBoxColumn,
            this.productSTOCKDataGridViewTextBoxColumn,
            this.productBarcodeDataGridViewTextBoxColumn,
            this.productNotWebDataGridViewTextBoxColumn,
            this.productTSDataGridViewTextBoxColumn,
            this.transferimeDataGridViewTextBoxColumn,
            this.stockTRDataGridViewTextBoxColumn,
            this.pointsDataGridViewTextBoxColumn,
            this.baseUnitDataGridViewTextBoxColumn,
            this.blockedDataGridViewTextBoxColumn,
            this.brandDataGridViewTextBoxColumn,
            this.topOfferDataGridViewTextBoxColumn,
            this.newProdDataGridViewTextBoxColumn,
            this.kW1DataGridViewTextBoxColumn,
            this.kW2DataGridViewTextBoxColumn,
            this.kW3DataGridViewTextBoxColumn,
            this.kW4DataGridViewTextBoxColumn,
            this.kW5DataGridViewTextBoxColumn,
            this.productNotesDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.wProductsBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(829, 378);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // productIDDataGridViewTextBoxColumn
            // 
            this.productIDDataGridViewTextBoxColumn.DataPropertyName = "ProductID";
            this.productIDDataGridViewTextBoxColumn.HeaderText = "ProductID";
            this.productIDDataGridViewTextBoxColumn.Name = "productIDDataGridViewTextBoxColumn";
            this.productIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Add
            // 
            this.Add.HeaderText = "Add";
            this.Add.Name = "Add";
            // 
            // prodNavIDDataGridViewTextBoxColumn
            // 
            this.prodNavIDDataGridViewTextBoxColumn.DataPropertyName = "ProdNavID";
            this.prodNavIDDataGridViewTextBoxColumn.HeaderText = "ProdNavID";
            this.prodNavIDDataGridViewTextBoxColumn.Name = "prodNavIDDataGridViewTextBoxColumn";
            // 
            // departmentIDDataGridViewTextBoxColumn
            // 
            this.departmentIDDataGridViewTextBoxColumn.DataPropertyName = "DepartmentID";
            this.departmentIDDataGridViewTextBoxColumn.HeaderText = "DepartmentID";
            this.departmentIDDataGridViewTextBoxColumn.Name = "departmentIDDataGridViewTextBoxColumn";
            // 
            // warehouseIDDataGridViewTextBoxColumn
            // 
            this.warehouseIDDataGridViewTextBoxColumn.DataPropertyName = "WarehouseID";
            this.warehouseIDDataGridViewTextBoxColumn.HeaderText = "WarehouseID";
            this.warehouseIDDataGridViewTextBoxColumn.Name = "warehouseIDDataGridViewTextBoxColumn";
            // 
            // prodSubCatIDDataGridViewTextBoxColumn
            // 
            this.prodSubCatIDDataGridViewTextBoxColumn.DataPropertyName = "ProdSubCatID";
            this.prodSubCatIDDataGridViewTextBoxColumn.HeaderText = "ProdSubCatID";
            this.prodSubCatIDDataGridViewTextBoxColumn.Name = "prodSubCatIDDataGridViewTextBoxColumn";
            // 
            // productNavDataGridViewTextBoxColumn
            // 
            this.productNavDataGridViewTextBoxColumn.DataPropertyName = "ProductNav";
            this.productNavDataGridViewTextBoxColumn.HeaderText = "ProductNav";
            this.productNavDataGridViewTextBoxColumn.Name = "productNavDataGridViewTextBoxColumn";
            // 
            // unitsPackDataGridViewTextBoxColumn
            // 
            this.unitsPackDataGridViewTextBoxColumn.DataPropertyName = "UnitsPack";
            this.unitsPackDataGridViewTextBoxColumn.HeaderText = "UnitsPack";
            this.unitsPackDataGridViewTextBoxColumn.Name = "unitsPackDataGridViewTextBoxColumn";
            // 
            // packsNrDataGridViewTextBoxColumn
            // 
            this.packsNrDataGridViewTextBoxColumn.DataPropertyName = "PacksNr";
            this.packsNrDataGridViewTextBoxColumn.HeaderText = "PacksNr";
            this.packsNrDataGridViewTextBoxColumn.Name = "packsNrDataGridViewTextBoxColumn";
            // 
            // productNameDataGridViewTextBoxColumn
            // 
            this.productNameDataGridViewTextBoxColumn.DataPropertyName = "ProductName";
            this.productNameDataGridViewTextBoxColumn.HeaderText = "ProductName";
            this.productNameDataGridViewTextBoxColumn.Name = "productNameDataGridViewTextBoxColumn";
            // 
            // productWebNameALDataGridViewTextBoxColumn
            // 
            this.productWebNameALDataGridViewTextBoxColumn.DataPropertyName = "ProductWebNameAL";
            this.productWebNameALDataGridViewTextBoxColumn.HeaderText = "ProductWebNameAL";
            this.productWebNameALDataGridViewTextBoxColumn.Name = "productWebNameALDataGridViewTextBoxColumn";
            // 
            // productWebNameENDataGridViewTextBoxColumn
            // 
            this.productWebNameENDataGridViewTextBoxColumn.DataPropertyName = "ProductWebNameEN";
            this.productWebNameENDataGridViewTextBoxColumn.HeaderText = "ProductWebNameEN";
            this.productWebNameENDataGridViewTextBoxColumn.Name = "productWebNameENDataGridViewTextBoxColumn";
            // 
            // productPriceDataGridViewTextBoxColumn
            // 
            this.productPriceDataGridViewTextBoxColumn.DataPropertyName = "ProductPrice";
            this.productPriceDataGridViewTextBoxColumn.HeaderText = "ProductPrice";
            this.productPriceDataGridViewTextBoxColumn.Name = "productPriceDataGridViewTextBoxColumn";
            // 
            // productSTOCKDataGridViewTextBoxColumn
            // 
            this.productSTOCKDataGridViewTextBoxColumn.DataPropertyName = "ProductSTOCK";
            this.productSTOCKDataGridViewTextBoxColumn.HeaderText = "ProductSTOCK";
            this.productSTOCKDataGridViewTextBoxColumn.Name = "productSTOCKDataGridViewTextBoxColumn";
            // 
            // productBarcodeDataGridViewTextBoxColumn
            // 
            this.productBarcodeDataGridViewTextBoxColumn.DataPropertyName = "ProductBarcode";
            this.productBarcodeDataGridViewTextBoxColumn.HeaderText = "ProductBarcode";
            this.productBarcodeDataGridViewTextBoxColumn.Name = "productBarcodeDataGridViewTextBoxColumn";
            // 
            // productNotWebDataGridViewTextBoxColumn
            // 
            this.productNotWebDataGridViewTextBoxColumn.DataPropertyName = "ProductNotWeb";
            this.productNotWebDataGridViewTextBoxColumn.HeaderText = "ProductNotWeb";
            this.productNotWebDataGridViewTextBoxColumn.Name = "productNotWebDataGridViewTextBoxColumn";
            // 
            // productTSDataGridViewTextBoxColumn
            // 
            this.productTSDataGridViewTextBoxColumn.DataPropertyName = "ProductTS";
            this.productTSDataGridViewTextBoxColumn.HeaderText = "ProductTS";
            this.productTSDataGridViewTextBoxColumn.Name = "productTSDataGridViewTextBoxColumn";
            // 
            // transferimeDataGridViewTextBoxColumn
            // 
            this.transferimeDataGridViewTextBoxColumn.DataPropertyName = "Transferime";
            this.transferimeDataGridViewTextBoxColumn.HeaderText = "Transferime";
            this.transferimeDataGridViewTextBoxColumn.Name = "transferimeDataGridViewTextBoxColumn";
            // 
            // stockTRDataGridViewTextBoxColumn
            // 
            this.stockTRDataGridViewTextBoxColumn.DataPropertyName = "StockTR";
            this.stockTRDataGridViewTextBoxColumn.HeaderText = "StockTR";
            this.stockTRDataGridViewTextBoxColumn.Name = "stockTRDataGridViewTextBoxColumn";
            // 
            // pointsDataGridViewTextBoxColumn
            // 
            this.pointsDataGridViewTextBoxColumn.DataPropertyName = "Points";
            this.pointsDataGridViewTextBoxColumn.HeaderText = "Points";
            this.pointsDataGridViewTextBoxColumn.Name = "pointsDataGridViewTextBoxColumn";
            // 
            // baseUnitDataGridViewTextBoxColumn
            // 
            this.baseUnitDataGridViewTextBoxColumn.DataPropertyName = "BaseUnit";
            this.baseUnitDataGridViewTextBoxColumn.HeaderText = "BaseUnit";
            this.baseUnitDataGridViewTextBoxColumn.Name = "baseUnitDataGridViewTextBoxColumn";
            // 
            // blockedDataGridViewTextBoxColumn
            // 
            this.blockedDataGridViewTextBoxColumn.DataPropertyName = "Blocked";
            this.blockedDataGridViewTextBoxColumn.HeaderText = "Blocked";
            this.blockedDataGridViewTextBoxColumn.Name = "blockedDataGridViewTextBoxColumn";
            // 
            // brandDataGridViewTextBoxColumn
            // 
            this.brandDataGridViewTextBoxColumn.DataPropertyName = "Brand";
            this.brandDataGridViewTextBoxColumn.HeaderText = "Brand";
            this.brandDataGridViewTextBoxColumn.Name = "brandDataGridViewTextBoxColumn";
            // 
            // topOfferDataGridViewTextBoxColumn
            // 
            this.topOfferDataGridViewTextBoxColumn.DataPropertyName = "TopOffer";
            this.topOfferDataGridViewTextBoxColumn.HeaderText = "TopOffer";
            this.topOfferDataGridViewTextBoxColumn.Name = "topOfferDataGridViewTextBoxColumn";
            // 
            // newProdDataGridViewTextBoxColumn
            // 
            this.newProdDataGridViewTextBoxColumn.DataPropertyName = "NewProd";
            this.newProdDataGridViewTextBoxColumn.HeaderText = "NewProd";
            this.newProdDataGridViewTextBoxColumn.Name = "newProdDataGridViewTextBoxColumn";
            // 
            // kW1DataGridViewTextBoxColumn
            // 
            this.kW1DataGridViewTextBoxColumn.DataPropertyName = "KW1";
            this.kW1DataGridViewTextBoxColumn.HeaderText = "KW1";
            this.kW1DataGridViewTextBoxColumn.Name = "kW1DataGridViewTextBoxColumn";
            // 
            // kW2DataGridViewTextBoxColumn
            // 
            this.kW2DataGridViewTextBoxColumn.DataPropertyName = "KW2";
            this.kW2DataGridViewTextBoxColumn.HeaderText = "KW2";
            this.kW2DataGridViewTextBoxColumn.Name = "kW2DataGridViewTextBoxColumn";
            // 
            // kW3DataGridViewTextBoxColumn
            // 
            this.kW3DataGridViewTextBoxColumn.DataPropertyName = "KW3";
            this.kW3DataGridViewTextBoxColumn.HeaderText = "KW3";
            this.kW3DataGridViewTextBoxColumn.Name = "kW3DataGridViewTextBoxColumn";
            // 
            // kW4DataGridViewTextBoxColumn
            // 
            this.kW4DataGridViewTextBoxColumn.DataPropertyName = "KW4";
            this.kW4DataGridViewTextBoxColumn.HeaderText = "KW4";
            this.kW4DataGridViewTextBoxColumn.Name = "kW4DataGridViewTextBoxColumn";
            // 
            // kW5DataGridViewTextBoxColumn
            // 
            this.kW5DataGridViewTextBoxColumn.DataPropertyName = "KW5";
            this.kW5DataGridViewTextBoxColumn.HeaderText = "KW5";
            this.kW5DataGridViewTextBoxColumn.Name = "kW5DataGridViewTextBoxColumn";
            // 
            // productNotesDataGridViewTextBoxColumn
            // 
            this.productNotesDataGridViewTextBoxColumn.DataPropertyName = "ProductNotes";
            this.productNotesDataGridViewTextBoxColumn.HeaderText = "ProductNotes";
            this.productNotesDataGridViewTextBoxColumn.Name = "productNotesDataGridViewTextBoxColumn";
            // 
            // wProductsBindingSource
            // 
            this.wProductsBindingSource.DataMember = "wProducts";
            this.wProductsBindingSource.DataSource = this.aB2CDataSet;
            // 
            // aB2CDataSet
            // 
            this.aB2CDataSet.DataSetName = "AB2CDataSet";
            this.aB2CDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // wProductsTableAdapter
            // 
            this.wProductsTableAdapter.ClearBeforeFill = true;
            // 
            // btnDil
            // 
            this.btnDil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(77)))), ((int)(((byte)(81)))));
            this.btnDil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDil.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDil.Font = new System.Drawing.Font("Century Gothic", 7.8F);
            this.btnDil.ForeColor = System.Drawing.Color.White;
            this.btnDil.Location = new System.Drawing.Point(108, 10);
            this.btnDil.Margin = new System.Windows.Forms.Padding(1);
            this.btnDil.Name = "btnDil";
            this.btnDil.Size = new System.Drawing.Size(82, 24);
            this.btnDil.TabIndex = 36;
            this.btnDil.TabStop = false;
            this.btnDil.Text = "Mbyll";
            this.btnDil.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDil.UseVisualStyleBackColor = false;
            this.btnDil.Click += new System.EventHandler(this.btnDil_Click);
            // 
            // btnListeCeshtje
            // 
            this.btnListeCeshtje.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnListeCeshtje.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnListeCeshtje.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnListeCeshtje.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListeCeshtje.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListeCeshtje.ForeColor = System.Drawing.Color.White;
            this.btnListeCeshtje.Location = new System.Drawing.Point(12, 10);
            this.btnListeCeshtje.Margin = new System.Windows.Forms.Padding(1);
            this.btnListeCeshtje.Name = "btnListeCeshtje";
            this.btnListeCeshtje.Size = new System.Drawing.Size(82, 24);
            this.btnListeCeshtje.TabIndex = 35;
            this.btnListeCeshtje.Text = "Ceshtje";
            this.btnListeCeshtje.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnListeCeshtje.UseVisualStyleBackColor = false;
            // 
            // ProdukteInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 570);
            this.Controls.Add(this.btnDil);
            this.Controls.Add(this.btnListeCeshtje);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ProdukteInfo";
            this.Text = "ProdukteInfo";
            this.Load += new System.EventHandler(this.ProdukteInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wProductsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aB2CDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private AB2CDataSet aB2CDataSet;
        private System.Windows.Forms.BindingSource wProductsBindingSource;
        private AB2CDataSetTableAdapters.wProductsTableAdapter wProductsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn productIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn Add;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodNavIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn departmentIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouseIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodSubCatIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNavDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitsPackDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn packsNrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productWebNameALDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productWebNameENDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productSTOCKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productBarcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNotWebDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productTSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockTRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pointsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseUnitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blockedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brandDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn topOfferDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn newProdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kW1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kW2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kW3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kW4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kW5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNotesDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnDil;
        private System.Windows.Forms.Button btnListeCeshtje;
    }
}