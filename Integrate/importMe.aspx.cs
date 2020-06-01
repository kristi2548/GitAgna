using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class importMe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtTableName.Text = "GPS_PROTEA_INFO";
            txtField1Filter.Text = "TARGA";
            txtField2Filter.Text = "STATUS";
            //ListItem lst = new ListItem("TOTAL", "0");
            //ddlGpsStatus.Items.Insert(ddlGpsStatus.Items.Count - 1, lst);
            //lst = new ListItem("BRENDA ORARIT", "1");
            //ddlGpsStatus.Items.Insert(ddlGpsStatus.Items.Count - 1, lst);
            //lst = new ListItem("JASHTE ORARIT", "2");
            //ddlGpsStatus.Items.Insert(ddlGpsStatus.Items.Count - 1, lst);
            if (!Page.IsPostBack)
            {
                ddlGpsStatus.Items.Add(new ListItem("TOTAL"));
                ddlGpsStatus.Items.Add(new ListItem("BRENDA ORARIT"));
                ddlGpsStatus.Items.Add(new ListItem("JASHTE ORARIT"));
                ddlGpsStatus.SelectedIndex = -1;
            }

            string nrRowsImported = ""; ;
            if (IsPostBack && Upload.HasFile)
            {
                if (Path.GetExtension(Upload.FileName).Equals(".xlsx"))//|| Path.GetExtension(Upload.FileName).Equals(".csv")
                {
                    var excel = new ExcelPackage(Upload.FileContent);
                    
                    Stream fs = Upload.FileContent;
                    ExcelPackage package = new ExcelPackage(fs);
                    DataTable dt = new DataTable();
                    dt = package.ToDataTable();
                    List<DataRow> listOfRows = new List<DataRow>();
                    listOfRows = dt.AsEnumerable().ToList();

                    if (dt.Columns.Contains("Start Date"))
                    {
                        dt.Columns["Start Date"].ColumnName = "DATE";
                    }
                    if (dt.Columns.Contains("Vehicle"))
                    {
                        dt.Columns["Vehicle"].ColumnName = "TARGA";
                    }
                    if (dt.Columns.Contains("Distance"))
                    {
                        dt.Columns["Distance"].ColumnName = "Total Distance (GPS)";
                    }

                    if (ddlGpsStatus.SelectedIndex != -1 && ! dt.Columns.Contains("STATUS"))
                    {
                        System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                        newColumn.DefaultValue = ddlGpsStatus.SelectedValue.ToString();
                        dt.Columns.Add(newColumn);
                    }

                    var table = txtTableName.Text;// "artikujPerberes";
                    nrRowsImported = dt.Rows.Count.ToString();
                    string connection = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;//@"Data Source=DESKTOP-M5TQV9A;Initial Catalog=ALLTEST;Integrated Security=True";
                    using (var conn = new SqlConnection(connection))
                    {
                        var bulkCopy = new SqlBulkCopy(conn);
                        bulkCopy.DestinationTableName = table;
                        conn.Open();
                        var schema = conn.GetSchema("Columns", new[] { null, null, table, null });
                        foreach (DataColumn sourceColumn in dt.Columns)
                        {
                            foreach (DataRow row in schema.Rows)
                            {
                                if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase))
                                {
                                    bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                                    break;
                                }
                            }
                        }
                        bulkCopy.WriteToServer(dt);
                    }
                    callSqlText(txtKerkoGride.Text, "UPDATE GPS_PROTEA_INFO SET TARGA_SPLIT = ltrim(substring(targa,0,case when (DATALENGTH(targa)-DATALENGTH(REPLACE(targa,'-','')))/DATALENGTH('-') in (2,3) then 10 else " +
                   " (case when charindex('-', targa) = 0 then charindex(' ', targa) else charindex('-', targa) end) " +
                   " end ))  WHERE TARGA_SPLIT IS NULL OR TARGA_SPLIT = '' ", "");
                    error.Text = "Importi i " + nrRowsImported + "  rreshtave perfundoi me sukses : " + System.DateTime.Now.ToString();
                }
                else
                {
                    MessageBox("Formati i file duhet te jete xlsx");
                }
            }
           
            //callSqlText(txtKerkoGride.Text, "UPDATE GPS_PROTEA_INFO SET TARGA_SPLIT = ltrim(TARGA_SPLIT)  WHERE TARGA_SPLIT IS NULL OR TARGA_SPLIT = '' ", "");
            bindGride("");
        }
        catch (Exception ex)
        {
            Log.LogData("importMe error", ex.Message);
            error.Text = "Import pa sukses " + ex.Message + "):";
        }
    }
    public void bindGride(string txtKerko)
    {
        //dgImportedData.DataSource = ktheDataTable(txtKerkoGride.Text, txtTableName.Text, txtField1Filter.Text, txtField2Filter.Text);
        string where = "";
        if (txtField1Filter.Text == "" && txtField2Filter.Text == "")
        {
            where = "";
        }
        else if (txtField1Filter.Text == "" && txtField2Filter.Text != "")
        {
            where = " where  " +
            " (" +
            " upper(" + txtField2Filter.Text + ") like ('%" + txtKerkoGride.Text + "%')" +
            " )";
        }
        else if (txtField1Filter.Text != "" && txtField2Filter.Text == "")
        {
            where = " where  " +
           " (" +
           " upper(" + txtField1Filter.Text + ") like ('%" + txtKerkoGride.Text + "%')" +
           " )";
        }
        else
        {
            where =  " where  " +
            " (" +
            " upper(" + txtField1Filter.Text + ") like ('%" + txtKerkoGride.Text + "%') or " +
            " upper(" + txtField2Filter.Text + ") like ('%" + txtKerkoGride.Text + "%')" +
            " )";
        }

        dgImportedData.AllowPaging = true;
        dgImportedData.PageSize = 100;

        dgImportedData.DataSource = ktheDataTable_BySqlTextType(txtKerkoGride.Text, "select * from " + txtTableName.Text + where +
         " order by Id desc" , txtField1Filter.Text);
        dgImportedData.DataBind();
        //gdPersoni.Ref
        dgImportedData.Font.Size = FontUnit.Small;
        dgImportedData.Font.Name = "Century Gothic";// new Font("Century Gothic", 9);
        //dgImportedData.Columns["DateTs"].SortMode = DataGridViewColumnSortMode.Automatic;
        //gdPersoni.Font = new Font("Century Gothic", 9);
    }
    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }
    protected void GridViewStudents_Sorting(object sender, GridViewSortEventArgs e)
    {
        //dgImportedData.Sort (e.SortExpression , e.SortDirection);

        dgImportedData.DataSource = ktheDataTable(txtKerkoGride.Text, txtTableName.Text, txtField1Filter.Text, txtField2Filter.Text);
        dgImportedData.DataBind();
        //gdPersoni.Ref
        dgImportedData.Font.Size = FontUnit.Small;
        dgImportedData.Font.Name = "Century Gothic";// new Font("Century Gothic", 9);
    }
    public System.Data.DataTable ktheDataTable(string txtKerko, string tableName, string field1ToFilter, string field2ToFilter)
    {
        try
        {
            txtKerko = txtKerko.ToUpper();
            string configvalue1 =
            System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string insertSql = "";
            if (txtField1Filter.Text == "" && txtField2Filter.Text == "")
            {
                insertSql = "select * from " + tableName;
            }
            else if (txtField1Filter.Text == "" && txtField2Filter.Text != "")
            {
                insertSql = "select * from " + tableName + " where  " +
             " (" +
                " upper(" + field2ToFilter + ") like ('%" + txtKerkoGride.Text + "%')" +
                " )";
            }
            else if (txtField1Filter.Text != "" && txtField2Filter.Text == "")
            {
                insertSql = "select * from " + tableName + " where  " +
          " (" +
             " upper(" + field1ToFilter + ") like ('%" + txtKerkoGride.Text + "%') " +
             " )";
            }
            else
            {
                insertSql = "select * from " + tableName + " where  " +
             " (" +
                " upper(" + field1ToFilter + ") like ('%" + txtKerkoGride.Text + "%') or " +
                " upper(" + field2ToFilter + ") like ('%" + txtKerkoGride.Text + "%')" +
                " )";
            }

            var tb = new DataTable();
            SqlConnection sqlConn = new SqlConnection(configvalue1);
            sqlConn.Open();
            SqlCommand sqlComm = new SqlCommand(insertSql, sqlConn);
            sqlComm.CommandType = System.Data.CommandType.Text;
            //sqlComm.ExecuteReader();

            using (SqlDataReader dr = sqlComm.ExecuteReader())
            {
                tb.Load(dr);
            }
            //sqlConn.Close();
            return tb;
        }
        catch (Exception ex)
        {
            lblError.Text = "ktheDataTable error " + ex.Message;
            return null;
        }
    }
    public System.Data.DataTable ktheDataTable_BySqlTextType(string txtKerko, string sqlText, string sqlType)
    {
        try
        {
            txtKerko = txtKerko.ToUpper();
            string configvalue1 =
            System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var tb = new DataTable();
            SqlConnection sqlConn = new SqlConnection(configvalue1);
            sqlConn.Open();
            SqlCommand sqlComm = new SqlCommand(sqlText, sqlConn);
            sqlComm.CommandType = System.Data.CommandType.Text;
            //sqlComm.ExecuteReader();

            using (SqlDataReader dr = sqlComm.ExecuteReader())
            {
                tb.Load(dr);
            }
            //sqlConn.Close();
            return tb;
        }
        catch (Exception ex)
        {
            lblError.Text = "ktheDataTable error " + ex.Message;
            return null;
        }
    }
    public string callSqlText(string txtKerko, string sqlText, string sqlType)
    {
        try
        {
            txtKerko = txtKerko.ToUpper();
            string configvalue1 =
            System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var tb = new DataTable();
            SqlConnection sqlConn = new SqlConnection(configvalue1);
            sqlConn.Open();
            SqlCommand sqlComm = new SqlCommand(sqlText, sqlConn);
            sqlComm.CommandType = System.Data.CommandType.Text;
            //sqlComm.ExecuteReader();
            sqlComm.ExecuteNonQuery();

            //using (SqlDataReader dr = sqlComm.ExecuteNonQuery())
            //{
            //    tb.Load(dr);
            //}
            //sqlConn.Close();
            return "OK";
        }
        catch (Exception ex)
        {
            MessageBox("callSqlText error " + ex.Message);
            lblError.Text = "callSqlText error " + ex.Message;
            return null;
        }
    }

    protected void txtKerkoGride_TextChanged(object sender, EventArgs e)
    {
        //bindGride("");
    }

    protected void btnFiltro_Click(object sender, EventArgs e)
    {
        if (txtField1Filter.Text == "" && txtField2Filter.Text == "")
        {
            Response.Write("<script>alert('Plotesoni me siper fushat sipas te cilave doni te filtroni ');</script>");
        }
        else
        {
            bindGride("");
        }
    }
    public void MessageBox(string txt)
    {
        Response.Write("<script>alert('" + txt + " ');</script>");
    }
}