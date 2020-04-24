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
using System.Drawing.Printing;
using System.Drawing.Drawing2D;

namespace ProgZyraAvokat
{
    public partial class Dashboard_Form : Form
    {
        public string selectCommandGlobal;
        public string raportiPerzgjedhur;
        public string dateFillimi;
        public string dateFundi;
        PrintDocument myDocument;
        int printIndex;
        PrintPreviewDialog PrintPreviewDialog1;
        private Font printFont;
        //private StreamReader streamToPrint;

        public Dashboard_Form()
        {
            InitializeComponent();
            Global.localConn = ConfigurationManager.ConnectionStrings["appConn"].ToString();
            ApplicationLookAndFeel.UseTheme(this, 10);
           
            dtpDateNga.Format = DateTimePickerFormat.Custom;
            dtpDateNga.CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
            DateTime dataSot = System.DateTime.Now,dateFillimMuaj;
            int dita = Convert.ToInt16(System.DateTime.Now.Day.ToString()) * -1 ;
            dateFillimMuaj = Convert.ToDateTime(System.DateTime.Now.AddDays(dita + 1));// DateTime.Today.AddDays(-1);
            dtpDateNga.Value = Convert.ToDateTime(dateFillimMuaj.Year.ToString() + "-" + dateFillimMuaj.Month.ToString() + "-" + dateFillimMuaj.Day.ToString() + " 00:00:00");
            dateFillimi  = dateFillimMuaj.Year.ToString() + "-" + dateFillimMuaj.Month.ToString() + "-" + dateFillimMuaj.Day.ToString() + " 00:00:00";

            dtpDateDeri.Format = DateTimePickerFormat.Custom;
            dtpDateDeri.CustomFormat = "yyyy-MM-dd hh:mm:ss tt"; //"dd/MM/yyyy hh:mm:ss";
            dtpDateDeri.Value = Convert.ToDateTime(dataSot.Year.ToString() + "-" + dataSot.Month.ToString() + "-" + dataSot.Day.ToString() + " 23:59:00");
            dateFundi  = dataSot.Year.ToString() + "-" + dataSot.Month.ToString() + "-" + dataSot.Day.ToString() + " 23:59:00";

            raportiPerzgjedhur = "CESHTJETEGJITHA";
            ExportDynamicMe("", "GRIDE");

            printerData();
        }
        public void printerData()
        {
            myDocument = new PrintDocument();

            PrintPreviewDialog1 = new PrintPreviewDialog();
            PrintPreviewDialog1.Document = myDocument;
            myDocument.DocumentName = "Test2015";
            myDocument.DefaultPageSettings.Landscape = true;
            //PrintPreviewDialog1.
            myDocument.BeginPrint += printDocument1_BeginPrint;
            myDocument.PrintPage += myDocument_PrintPage;
            
            //PrintPreviewDialog1.ShowDialog();
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            int y = 0;

            while (printIndex < dgCeshtje.Rows.Count &&
                   y + dgCeshtje.Rows[printIndex].Height < e.MarginBounds.Height)
            {

                foreach (DataGridViewRow row in dgCeshtje.Rows)
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                            e.Graphics.DrawString(cell.Value.ToString(), Font, Brushes.Black,
                                        new Point(cell.ColumnIndex * 123, cell.RowIndex * 12));
                    }

                y += dgCeshtje.Rows[printIndex].Height;
                ++printIndex;
            }

            e.HasMorePages = printIndex < dgCeshtje.Rows.Count;
        }
        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            printIndex = 0;
        }
        void myDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
           

            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            //linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);

            //// Print each line of the file.
            //while (count < linesPerPage &&
            //   ((line = streamToPrint.ReadLine()) != null))
            //{
            //    yPos = topMargin + (count *
            //       printFont.GetHeight(e.Graphics));
            //    e.Graphics.DrawString(line, printFont, Brushes.Black,
            //       leftMargin, yPos, new StringFormat());
            //    count++;
            //}
            int colInd = 0;
            for (var i = 0; i < dgCeshtje.ColumnCount; i++)
            {
                e.Graphics.DrawString(dgCeshtje.Columns[i].HeaderText, Font, Brushes.Black, new Point(colInd * 123, 1 * 20));
                colInd = colInd + 1;
            }
            //var name = dgCeshtje.Columns[i].HeaderText;

            foreach (DataGridViewRow row in dgCeshtje.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                        e.Graphics.DrawString(cell.Value.ToString(), Font, Brushes.Black, 
                            new Point( (cell.ColumnIndex * 123), 40 + (cell.RowIndex * 20)));
                }

            // If more lines exist, print another page.
            //if (line != null)
                e.HasMorePages = true;
            //else
            //    e.HasMorePages = false;
        }
        private void btnListeCeshtje_Click(object sender, EventArgs e)
        {
            raportiPerzgjedhur = "CESHTJETEGJITHA";
            lblCurrentReport.Text = "Ceshtje te gjitha";

            btnListeCeshtje.BZBackColor = Color.FromArgb(30, 30, 40);
            btnListeCeshtjeTeHapura.BZBackColor = Color.FromArgb(32, 37, 76);
            btnProcedimPenalSakte.BZBackColor = Color.FromArgb(32, 37, 76);
            btnProkuroriSakte.BZBackColor = Color.FromArgb(32, 37, 76);
            btnCeshtjeAnalitikeSakte.BZBackColor = Color.FromArgb(32, 37, 76);

            ExportDynamicMe("", "GRIDE");
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            ExportDynamicMe("", "EXCEL");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Global.dashboardForm.Visible = false;
            Global.kallezimePageNr = 1;
            if (Global.listeCeshtje != null)
            {
                Global.listeCeshtje.callGridUpdate("");
                Global.listeCeshtje.Show();
            }
        }

        private void btnCeshtjeAnalitike_Click(object sender, EventArgs e)
        {
            raportiPerzgjedhur = "PROCEDIMETEGJITHA";
            lblCurrentReport.Text = "Procedime te gjitha";

            btnListeCeshtje.BZBackColor = Color.FromArgb(32, 37, 76);
            btnListeCeshtjeTeHapura.BZBackColor = Color.FromArgb(32, 37, 76);
            btnProcedimPenalSakte.BZBackColor = Color.FromArgb(32, 37, 76);
            btnProkuroriSakte.BZBackColor = Color.FromArgb(30, 30, 40);
            btnCeshtjeAnalitikeSakte.BZBackColor = Color.FromArgb(32, 37, 76);

            ExportDynamicMe("", "GRIDE");
            //string txtKerko = txtCeshtjeKerko.Text;
            //raportiPerzgjedhur = "CESHTJEANALITIKE";
            //lblCurrentReport.Text = "Ceshtje analitike";

            //ExportDynamicMe("", "GRIDE");
        }
        public bool ExportDynamicMe(string status,string destination)
        {
            try
            {

                int rreshtFill, rreshtMbarim;
                string txtKerko = txtCeshtjeKerko.Text;
                int ceshtjePerFaqe = 15;
                string filter;
                rreshtFill = ((Global.kallezimePageNr - 1) * ceshtjePerFaqe) + 1;
                rreshtMbarim = (Global.kallezimePageNr) * ceshtjePerFaqe;
                if (txtKerko == "") { filter = "'%%'"; }
                else { filter = "'%" + txtKerko.ToUpper() + "%'"; }

                
                if (dtpDateNga.Text != "") { dateFillimi = dtpDateNga.Text; } //'2020-03-18 00:56:09.000'
                if (dtpDateDeri.Text != "") { dateFundi = dtpDateDeri.Text; } //'2020-03-19 23:56:09.000'
                if (raportiPerzgjedhur == "CESHTJETEGJITHA" ||  raportiPerzgjedhur == null )
                {
                    selectCommandGlobal = " SELECT * " +
                       " FROM (" +
                       " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA,DATE_ALERTI,KOMENTE AS ALERTI, " +
                       " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                       "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 1 and a.aktiv = 1 " +
                       " ) AS MyDerivedTable" +
                       //" WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                       " WHERE " +
                       //" MyDerivedTable.aktiv = 1 and " + 
                       " (upper(KALLEZUES) like " + filter + " or upper(IKALLEZUAR) like " + filter +
                       " or upper(FABUL) like " + filter +
                       " or upper(STATUS) like " + filter +
                       " or upper(CONVERT(varchar, [DATA],103)) like " + filter +
                       " or upper(NENI) like " + filter + " or upper(alert) like " + filter + ")" +
                       " order by N asc";
                }
                else if(raportiPerzgjedhur == "PROCEDIMETEGJITHA" )
                {
                    selectCommandGlobal = " SELECT * " +
                       " FROM (" +
                       " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA,DATE_ALERTI,KOMENTE AS ALERTI, " +
                       " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                       "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 2 and a.aktiv = 1 " +
                       " ) AS MyDerivedTable" +
                       " WHERE " +
                       //" WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                       //" MyDerivedTable.aktiv = 1 and " + 
                       " (upper(KALLEZUES) like " + filter + " or upper(IKALLEZUAR) like " + filter +
                       " or upper(FABUL) like " + filter +
                       " or upper(STATUS) like " + filter +
                       " or upper(CONVERT(varchar, [DATA],103)) like " + filter +
                       " or upper(NENI) like " + filter + " or upper(alert) like " + filter + ")" +
                       " order by N asc";
                }
                else if (raportiPerzgjedhur == "ALERTE")
                {
                    string selectCommandGlobal = " SELECT * " +
                   " FROM (" +
                   " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,DATE_ALERTI,KOMENTE AS ALERTI," +
                   " b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA, " +
                   " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                   "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 1 and a.aktiv = 1 " +
                   "  and DATE_ALERTI >= ([dbo].[ufn_GetDateOnlyUs](GETDATE())) " +
                   "  and DATE_ALERTI <= ([dbo].[ufn_GetDateOnlyUs](GETDATE() + 2)) " +
                   " ) AS MyDerivedTable" +
                   " WHERE " +
                   //" WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                   " (UPPER(KALLEZUES) like " + filter + " or UPPER(IKALLEZUAR) like " + filter + " or UPPER(FABUL) like " + filter +
                   " or UPPER(NENI) like " + filter + " or UPPER(ALERT) like " + filter + ")" +
                   " order by N asc";
                }
                else if (raportiPerzgjedhur == "PROKURORIGJITHA")
                {
                    selectCommandGlobal = " SELECT b.[ID],b.[LLTVID],b.[DATA],b.[PROKURORI],b.[IPANDEHURI],b.[NENI],b.[DATE_RREGJISTRIMI],b.[AKTIV]," + 
                    "b.[KOMENTE],b.[PRIND_ID] " +
                    " FROM veprim a inner join THIRRJE_PROKURORI b on a.kid = b.prind_id " +
                    " where b.aktiv = 1 and a.aktiv = 1 and " + 
                    " (upper(B.PROKURORI) like " + filter +
                    " or upper(B.IPANDEHURI) like " + filter +
                    " or upper(B.NENI) like " + filter +
                    " or upper(CONVERT(varchar, B.[DATE_RREGJISTRIMI],103)) like " + filter + ") order by b.[data] desc";
                }
                else if (raportiPerzgjedhur == "CESHTJETEHAPURA")
                {
                    selectCommandGlobal = " SELECT * " +
                      " FROM (" +
                      " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA,DATE_ALERTI,KOMENTE AS ALERTI, " +
                      " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                      "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 1 and a.aktiv = 1 " +
                      " ) AS MyDerivedTable" +
                      " WHERE " +
                      //" WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                      //" MyDerivedTable.aktiv = 1 and " + 
                      "  UPPER(STATUS) like '%HAPUR%' and " +
                      " (upper(KALLEZUES) like " + filter + " or upper(IKALLEZUAR) like " + filter +
                      " or upper(FABUL) like " + filter +
                      " or upper(CONVERT(varchar, [DATA],103)) like " + filter +
                      " or upper(NENI) like " + filter + " or upper(alert) like " + filter + ")" +
                      " order by N asc";
                }
                else if (raportiPerzgjedhur == "CESHTJEANALITIKE")
                {
                    txtKerko = txtCeshtjeKerko.Text;
                    raportiPerzgjedhur = "CESHTJEANALITIKE";
                    string filterCeshtje = "", filterCeshtjeProcedim = "", filterProkurori = "";
                    if (Global.idVeprimi == 0)
                    {
                        filterCeshtje = " ";
                    }
                    else
                    {
                        filterCeshtje = " A.KID = " + Global.idVeprimi.ToString() + " AND ";
                        filterCeshtjeProcedim = " A.PRIND_KID = " + Global.idVeprimi.ToString() + " AND ";
                        filterProkurori = " WHERE A.PRIND_ID IN (SELECT DISTINCT KID FROM PROCEDIME_PENALE WHERE PRIND_KID =  " + Global.idVeprimi.ToString() + " ) ";
                    }

                    selectCommandGlobal = "SELECT DISTINCT A.KID AS AA, CAST(A.KID AS VARCHAR(4))  + '.' + A.LLOJ_VEPRIMI AS A, A.[DATE_VEPRIMI] as B, " +
                   " 'KALLEZUES:' + A.KALLEZUES as C,'IKALLEZUAR:' + A.IKALLEZUAR as D,A.FABUL_TRUPI E ,A.NR  as F  FROM KALLEZIME A WHERE   " + filterCeshtje + "  llvid = 1 " +
                   " UNION " +
                   " SELECT DISTINCT A.TID AS AA,CAST(A.KID AS VARCHAR(4)) + '.' + A.LLOJ_VEPRIMI + '_' + A.LLOJ_VEPRIMI_TRUPI  A,A.[DATE] B , " +
                   " 'DERGUESI:' + A.DERGUESI C, 'MARRESI:' + A.MARRESI D, A.LENDA E, A.NR_PROTOKOLLI AS F FROM KALLEZIME A WHERE   " + filterCeshtje + "  lltvid = 1 " +
                   " UNION " +
                   " SELECT DISTINCT A.TID AS AA,CAST(A.KID AS VARCHAR(4)) + '.' + A.LLOJ_VEPRIMI + '_' + A.LLOJ_VEPRIMI_TRUPI  A,A.[DATE] B , " +
                   " 'DERGUESI:' + A.DERGUESI C, 'MARRESI:' + A.MARRESI D, A.LENDA E, A.NR_PROTOKOLLI AS F FROM KALLEZIME A WHERE   " + filterCeshtje + "  lltvid = 2 " +
                   " UNION " +
                   " SELECT DISTINCT A.KID AS AA,CAST(A.PRIND_KID AS VARCHAR(4)) + '.' + A.LLOJ_VEPRIMI AS A, A.[DATE_VEPRIMI] as B, " +
                   " 'KALLEZUES:' + A.KALLEZUES as C,'IKALLEZUAR:' + A.IKALLEZUAR as D,A.FABUL_TRUPI as E,A.NR  as F FROM PROCEDIME_PENALE A WHERE  " + filterCeshtjeProcedim + "  llvid = 2 " +
                   " UNION " +
                   " SELECT DISTINCT A.TID AS AA, CAST(A.PRIND_KID AS VARCHAR(4))  + '.' + A.LLOJ_VEPRIMI + '_' + A.LLOJ_VEPRIMI_TRUPI A, A.[DATE] B, " +
                   " 'DERGUESI:' + A.DERGUESI C, 'MARRESI:' + A.MARRESI D, A.LENDA E, A.NR_PROTOKOLLI AS F FROM PROCEDIME_PENALE A WHERE  " + filterCeshtjeProcedim + "  lltvid = 1 " +
                   " UNION " +
                   " SELECT DISTINCT A.TID AS AA, CAST(A.PRIND_KID AS VARCHAR(4))  + '.' + A.LLOJ_VEPRIMI + '_' + A.LLOJ_VEPRIMI_TRUPI A, A.[DATE] B, " +
                   " 'DERGUESI:' + A.DERGUESI C, 'MARRESI:' + A.MARRESI D, A.LENDA E, A.NR_PROTOKOLLI AS F FROM PROCEDIME_PENALE A WHERE  " + filterCeshtjeProcedim + "  lltvid = 2 " +
                   " UNION " +
                   " SELECT A.ID AS AA,CAST(C.PRIND_KID AS VARCHAR(4))  + '.' + B.LLOJ_VEPRIMI AS A,A.DATA AS B, " +
                   " 'PROKURORI:' + A.PROKURORI AS C,'IPANDEHURI:' + A.IPANDEHURI AS D,A.NENI E,'' AS F FROM THIRRJE_PROKURORI A INNER JOIN LLOJ_VEPRIMI B ON A.LLTVID = B.ID  " +
                   " INNER JOIN PROCEDIME_PENALE C ON A.PRIND_ID = C.KID  " +
                   //" WHERE A.PRIND_ID IN (SELECT DISTINCT KID FROM PROCEDIME_PENALE WHERE " + filterCeshtje.Replace("AND"," ") + ") " +
                   filterProkurori +
                   " UNION  " +
                   " SELECT DISTINCT A.KID AS AA, CAST(A.KID AS VARCHAR(4))  + '.' +'5.' + A.LLOJ_VEPRIMI + ' STATUS' AS A, CASE WHEN STATUS_UPDATE_DATE IS NULL THEN A.DATE_VEPRIMI ELSE A.[STATUS_UPDATE_DATE] END as B,  " +
                   @" STATUS as C,'' as D,'' E ,''  as F FROM KALLEZIME A WHERE   " + filterCeshtje + "  llvid = 1 and " +
                   " DATE_VEPRIMI >= CONVERT(DATETIME, '" + dateFillimi + "', 102) AND " +
                   " DATE_VEPRIMI <= CONVERT(DATETIME, '" + dateFundi + "', 102) AND " +
                   " UPPER(CAST(KID AS VARCHAR(4))) LIKE  '%" + txtCeshtjeKerko.Text.ToUpper() + "%' " +
                   " ORDER BY A DESC";
                }
                else
                {
                     selectCommandGlobal = " SELECT * " +
                       " FROM (" +
                       " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA,DATE_ALERTI,KOMENTE AS ALERTI, " +
                       " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                       "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 1 and a.aktiv = 1 " +
                       " ) AS MyDerivedTable" +
                       " WHERE  " +
                       //" WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                       //" MyDerivedTable.aktiv = 1 and " + 
                       " (upper(KALLEZUES) like " + filter + " or upper(IKALLEZUAR) like " + filter +
                       " or upper(FABUL) like " + filter +
                       " or upper(STATUS) like " + filter +
                       " or upper(CONVERT(varchar, [DATA],103)) like " + filter +
                       " or upper(NENI) like " + filter + " or upper(alert) like " + filter + ")" +
                       " order by N asc";
                }

                if (selectCommandGlobal != "")
                {
                    if (destination == "EXCEL")
                    {
                        Global.exportToExcel(txtKerko, selectCommandGlobal);
                    }
                    else
                    {
                        Global.fillGridWithRef(ref dgCeshtje, Global.localConn, selectCommandGlobal, "", "");
                    }
                }
                else
                {
                    Log.LogData("ExportDynamic me", selectCommandGlobal);
                }

                if (raportiPerzgjedhur == "CESHTJETEGJITHA" || raportiPerzgjedhur == null)
                {
                    dgCeshtje.Columns["KID"].Visible = false;
                    dgCeshtje.Columns["LLVID"].Visible = false;
                    dgCeshtje.Columns["PKID"].Visible = false;
                    //dgCeshtje.Columns["NR"].Visible = false;
                    dgCeshtje.Columns["ALERT"].Visible = false;
                    dgCeshtje.Columns["ALERTI"].Visible = false;
                    dgCeshtje.Columns["DATE_ALERTI"].Visible = false;
                    //dgCeshtje.Columns["AKTIV"].Visible = false;
                    //dgCeshtje.Columns["NENI"].Visible = false;
                    dgCeshtje.Columns["LLOJ_VEPRIMI"].Visible = false;
                }
                else if (raportiPerzgjedhur == "PROCEDIMETEGJITHA")
                {
                    selectCommandGlobal = " SELECT * " +
                       " FROM (" +
                       " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA,DATE_ALERTI,KOMENTE AS ALERTI, " +
                       " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                       "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 2 and a.aktiv = 1 " +
                       " ) AS MyDerivedTable" +
                       " WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                       //" MyDerivedTable.aktiv = 1 and " + 
                       " (upper(KALLEZUES) like " + filter + " or upper(IKALLEZUAR) like " + filter +
                       " or upper(FABUL) like " + filter +
                       " or upper(STATUS) like " + filter +
                       " or upper(CONVERT(varchar, [DATA],103)) like " + filter +
                       " or upper(NENI) like " + filter + " or upper(alert) like " + filter + ")" +
                       " order by N asc";

                    dgCeshtje.Columns["KID"].Visible = false;
                    dgCeshtje.Columns["LLVID"].Visible = false;
                    dgCeshtje.Columns["PKID"].Visible = false;
                    dgCeshtje.Columns["ALERT"].Visible = false;
                    dgCeshtje.Columns["ALERTI"].Visible = false;
                    dgCeshtje.Columns["DATE_ALERTI"].Visible = false;
                }
                else if (raportiPerzgjedhur == "ALERTE")
                {
                   // string selectCommandGlobal = " SELECT * " +
                   //" FROM (" +
                   //" 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,DATE_ALERTI,KOMENTE AS ALERTI," +
                   //" b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA, " +
                   //" 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                   //"               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 1 and a.aktiv = 1 " +
                   //"  and DATE_ALERTI >= ([dbo].[ufn_GetDateOnlyUs](GETDATE())) " +
                   //"  and DATE_ALERTI <= ([dbo].[ufn_GetDateOnlyUs](GETDATE() + 2)) " +
                   //" ) AS MyDerivedTable" +
                   //" WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                   //" (UPPER(KALLEZUES) like " + filter + " or UPPER(IKALLEZUAR) like " + filter + " or UPPER(FABUL) like " + filter +
                   //" or UPPER(NENI) like " + filter + " or UPPER(ALERT) like " + filter + ")" +
                   //"AND ALERT <> ''" + 
                   //" order by N asc";

                    dgCeshtje.Columns["KID"].Visible = false;
                    dgCeshtje.Columns["LLVID"].Visible = false;
                    dgCeshtje.Columns["PKID"].Visible = false;
                    //dgCeshtje.Columns["ALERT"].Visible = false;
                    dgCeshtje.Columns["ALERTI"].Visible = false;
                    //dgCeshtje.Columns["DATE_ALERTI"].Visible = false;
                }
                else if (raportiPerzgjedhur == "PROKURORIGJITHA")
                {
                    selectCommandGlobal = " SELECT b.[ID],b.[LLTVID],b.[DATA],b.[PROKURORI],b.[IPANDEHURI],b.[NENI],b.[DATE_RREGJISTRIMI],b.[AKTIV]," +
                    "b.[KOMENTE],b.[PRIND_ID] " +
                    " FROM veprim a inner join THIRRJE_PROKURORI b on a.kid = b.prind_id " +
                    " where b.aktiv = 1 and a.aktiv = 1 and " +
                    " (upper(B.PROKURORI) like " + filter +
                    " or upper(B.IPANDEHURI) like " + filter +
                    " or upper(B.NENI) like " + filter +
                    " or upper(CONVERT(varchar, B.[DATE_RREGJISTRIMI],103)) like " + filter + ")";

                    dgCeshtje.Columns["ID"].Visible = false;
                    dgCeshtje.Columns["LLTVID"].Visible = false;
                    dgCeshtje.Columns["AKTIV"].Visible = false;
                    dgCeshtje.Columns["PRIND_ID"].Visible = false;
                }
                else if (raportiPerzgjedhur == "CESHTJETEHAPURA")
                {
                    selectCommandGlobal = " SELECT * " +
                      " FROM (" +
                      " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA,DATE_ALERTI,KOMENTE AS ALERTI, " +
                      " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                      "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 1 and a.aktiv = 1 " +
                      " ) AS MyDerivedTable" +
                      " WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                      //" MyDerivedTable.aktiv = 1 and " + 
                      "  UPPER(STATUS) like '%HAPUR%' and " +
                      " (upper(KALLEZUES) like " + filter + " or upper(IKALLEZUAR) like " + filter +
                      " or upper(FABUL) like " + filter +
                      " or upper(CONVERT(varchar, [DATA],103)) like " + filter +
                      " or upper(NENI) like " + filter + " or upper(alert) like " + filter + ")" +
                      " order by N asc";

                    dgCeshtje.Columns["KID"].Visible = false;
                    dgCeshtje.Columns["LLVID"].Visible = false;
                    dgCeshtje.Columns["PKID"].Visible = false;
                    dgCeshtje.Columns["ALERT"].Visible = false;
                    dgCeshtje.Columns["ALERTI"].Visible = false;
                    dgCeshtje.Columns["DATE_ALERTI"].Visible = false;
                }
                else if (raportiPerzgjedhur == "CESHTJEANALITIKE")
                {
                    txtKerko = txtCeshtjeKerko.Text;
                    raportiPerzgjedhur = "CESHTJEANALITIKE";
                    string filterCeshtje = "", filterCeshtjeProcedim = "", filterProkurori = "";
                    if (Global.idVeprimi == 0)
                    {
                        filterCeshtje = " ";
                    }
                    else
                    {
                        filterCeshtje = " A.KID = " + Global.idVeprimi.ToString() + " AND ";
                        filterCeshtjeProcedim = " A.PRIND_KID = " + Global.idVeprimi.ToString() + " AND ";
                        filterProkurori = " WHERE A.PRIND_ID IN (SELECT DISTINCT KID FROM PROCEDIME_PENALE WHERE PRIND_KID =  " + Global.idVeprimi.ToString() + " ) ";
                    }

                    selectCommandGlobal = "SELECT DISTINCT A.KID AS AA, CAST(A.KID AS VARCHAR(4))  + '.' + A.LLOJ_VEPRIMI AS A, A.[DATE_VEPRIMI] as B, " +
                   " 'KALLEZUES:' + A.KALLEZUES as C,'IKALLEZUAR:' + A.IKALLEZUAR as D,A.FABUL_TRUPI E ,A.NR  as F  FROM KALLEZIME A WHERE   " + filterCeshtje + "  llvid = 1 " +
                   " UNION " +
                   " SELECT DISTINCT A.TID AS AA,CAST(A.KID AS VARCHAR(4)) + '.' + A.LLOJ_VEPRIMI + '_' + A.LLOJ_VEPRIMI_TRUPI  A,A.[DATE] B , " +
                   " 'DERGUESI:' + A.DERGUESI C, 'MARRESI:' + A.MARRESI D, A.LENDA E, A.NR_PROTOKOLLI AS F FROM KALLEZIME A WHERE   " + filterCeshtje + "  lltvid = 1 " +
                   " UNION " +
                   " SELECT DISTINCT A.TID AS AA,CAST(A.KID AS VARCHAR(4)) + '.' + A.LLOJ_VEPRIMI + '_' + A.LLOJ_VEPRIMI_TRUPI  A,A.[DATE] B , " +
                   " 'DERGUESI:' + A.DERGUESI C, 'MARRESI:' + A.MARRESI D, A.LENDA E, A.NR_PROTOKOLLI AS F FROM KALLEZIME A WHERE   " + filterCeshtje + "  lltvid = 2 " +
                   " UNION " +
                   " SELECT DISTINCT A.KID AS AA,CAST(A.PRIND_KID AS VARCHAR(4)) + '.' + A.LLOJ_VEPRIMI AS A, A.[DATE_VEPRIMI] as B, " +
                   " 'KALLEZUES:' + A.KALLEZUES as C,'IKALLEZUAR:' + A.IKALLEZUAR as D,A.FABUL_TRUPI as E,A.NR  as F FROM PROCEDIME_PENALE A WHERE  " + filterCeshtjeProcedim + "  llvid = 2 " +
                   " UNION " +
                   " SELECT DISTINCT A.TID AS AA, CAST(A.PRIND_KID AS VARCHAR(4))  + '.' + A.LLOJ_VEPRIMI + '_' + A.LLOJ_VEPRIMI_TRUPI A, A.[DATE] B, " +
                   " 'DERGUESI:' + A.DERGUESI C, 'MARRESI:' + A.MARRESI D, A.LENDA E, A.NR_PROTOKOLLI AS F FROM PROCEDIME_PENALE A WHERE  " + filterCeshtjeProcedim + "  lltvid = 1 " +
                   " UNION " +
                   " SELECT DISTINCT A.TID AS AA, CAST(A.PRIND_KID AS VARCHAR(4))  + '.' + A.LLOJ_VEPRIMI + '_' + A.LLOJ_VEPRIMI_TRUPI A, A.[DATE] B, " +
                   " 'DERGUESI:' + A.DERGUESI C, 'MARRESI:' + A.MARRESI D, A.LENDA E, A.NR_PROTOKOLLI AS F FROM PROCEDIME_PENALE A WHERE  " + filterCeshtjeProcedim + "  lltvid = 2 " +
                   " UNION " +
                   " SELECT A.ID AS AA,CAST(C.PRIND_KID AS VARCHAR(4))  + '.' + B.LLOJ_VEPRIMI AS A,A.DATA AS B, " +
                   " 'PROKURORI:' + A.PROKURORI AS C,'IPANDEHURI:' + A.IPANDEHURI AS D,A.NENI E,'' AS F FROM THIRRJE_PROKURORI A INNER JOIN LLOJ_VEPRIMI B ON A.LLTVID = B.ID  " +
                   " INNER JOIN PROCEDIME_PENALE C ON A.PRIND_ID = C.KID  " +
                   //" WHERE A.PRIND_ID IN (SELECT DISTINCT KID FROM PROCEDIME_PENALE WHERE " + filterCeshtje.Replace("AND"," ") + ") " +
                   filterProkurori +
                   " UNION  " +
                   " SELECT DISTINCT A.KID AS AA, CAST(A.KID AS VARCHAR(4))  + '.' +'5.' + A.LLOJ_VEPRIMI + ' STATUS' AS A, CASE WHEN STATUS_UPDATE_DATE IS NULL THEN A.DATE_VEPRIMI ELSE A.[STATUS_UPDATE_DATE] END as B,  " +
                   @" STATUS as C,'' as D,'' E ,''  as F FROM KALLEZIME A WHERE   " + filterCeshtje + "  llvid = 1 and " +
                   " DATE_VEPRIMI >= CONVERT(DATETIME, '" + dateFillimi + "', 102) AND " +
                   " DATE_VEPRIMI <= CONVERT(DATETIME, '" + dateFundi + "', 102) " +
                   " ORDER BY A";

                    dgCeshtje.Columns["AA"].Visible = false;
                    //dgCeshtje.Columns["LLVID"].Visible = false;
                    //dgCeshtje.Columns["PKID"].Visible = false;
                }
                else
                {
                    selectCommandGlobal = " SELECT * " +
                      " FROM (" +
                      " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI] AS DATA,DATE_ALERTI,KOMENTE AS ALERTI, " +
                      " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],a.[AKTIV],[KOMENTE] as ALERT,[STATUS],[KID],[LLVID],[PRIND_KID] as PKID" +
                      "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID WHERE LLVID = 1 and a.aktiv = 1 " +
                      " ) AS MyDerivedTable" +
                      " WHERE MyDerivedTable.n BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and " +
                      //" MyDerivedTable.aktiv = 1 and " + 
                      " (upper(KALLEZUES) like " + filter + " or upper(IKALLEZUAR) like " + filter +
                      " or upper(FABUL) like " + filter +
                      " or upper(STATUS) like " + filter +
                      " or upper(CONVERT(varchar, [DATA],103)) like " + filter +
                      " or upper(NENI) like " + filter + " or upper(alert) like " + filter + ")" +
                      " order by N asc";

                    dgCeshtje.Columns["KID"].Visible = false;
                    dgCeshtje.Columns["LLVID"].Visible = false;
                    dgCeshtje.Columns["PKID"].Visible = false;
                }


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err ExportDynamicMe " + ex.Message );
                return false;
            }
        }
        private void dgCeshtje_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            fillInfoKallezim();
        }
        private void fillInfoKallezim()
        {
            try
            {
                if (dgCeshtje.SelectedCells.Count > 0)
                {
                    //ceshtje_ERe();
                    int selectedrowindex = dgCeshtje.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgCeshtje.Rows[selectedrowindex];
                    Global.idVeprimi = Convert.ToInt32(selectedRow.Cells["Kid"].Value.ToString());
                    //Global.veprimKallezim.fillVeprimById(Global.idVeprimi.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hap kallezim err " + ex.Message);
            }
        }

        private void btnListeCeshtjeTeHapura_Click(object sender, EventArgs e)
        {
            raportiPerzgjedhur = "CESHTJETEHAPURA";
            lblCurrentReport.Text = "Ceshtje te hapura";
            btnListeCeshtje.BZBackColor = Color.FromArgb(32, 37, 76);
            btnListeCeshtjeTeHapura.BZBackColor = Color.FromArgb(30, 30, 40);
            btnProcedimPenalSakte.BZBackColor = Color.FromArgb(32, 37, 76);
            btnProkuroriSakte.BZBackColor = Color.FromArgb(32, 37, 76);
            btnCeshtjeAnalitikeSakte.BZBackColor = Color.FromArgb(32, 37, 76);

            ExportDynamicMe("", "GRIDE");
        }

        private void rOUTAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void listeCeshtjeRaport(string statusCeshtje)
        {
            string txtKerko = txtCeshtjeKerko.Text;
            selectCommandGlobal = "SELECT " +
                        " DISTINCT A.KID AS AA, CAST(A.KID AS VARCHAR(4)) + '.' + A.LLOJ_VEPRIMI AS A, A.[DATE_VEPRIMI] as B, " +
                        " 'KALLEZUES:' + A.KALLEZUES as C,'IKALLEZUAR:' + A.IKALLEZUAR as D,A.FABUL_TRUPI E ,A.NR  as F,A.KOMENTE AS ALERT,A.[STATUS] " +
                        " FROM KALLEZIME A LEFT OUTER JOIN" +
                        "      PROCEDIME_PENALE ON a.KID = PROCEDIME_PENALE.PRIND_KID" +
                        " where A.llvid = 1 and (A.KALLEZUES like '%" + txtKerko + "%' or A.IKALLEZUAR like '%" + txtKerko + "%'  " +
                        " or A.FABUL like '%" + txtKerko + "%' or  A.NENI like '%" + txtKerko + "%' or A.NR like '%" + txtKerko + "%' " +
                        " or A.STATUS like '%" + statusCeshtje + "%')" +
                        " ORDER BY A.DATE_VEPRIMI DESC";
            Global.exportToExcel(txtKerko, selectCommandGlobal);

            Global.fillGridWithRef(ref dgCeshtje, Global.localConn, selectCommandGlobal, "", "");
        }
        public void callGridUpdate(string txtKerko)
        {
            try
            {
                //string selectCommandGlobal = "SELECT b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI],[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID],a.[AKTIV],[KOMENTE],[KID],[LLVID] " +
                //" FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID  " +
                //" where KALLEZUES like '%" + txtKerko + "%' or IKALLEZUAR like '%" + txtKerko + "%' or FABUL like '%" + txtKerko + "%' or NENI like '%" + txtKerko + "%'";
                int rreshtFill, rreshtMbarim;
                rreshtFill = ((Global.kallezimePageNr - 1) * 10) + 1;
                rreshtMbarim = (Global.kallezimePageNr) * 10;
                selectCommandGlobal = " SELECT * " +
               " FROM (" +
               " 	SELECT ROW_NUMBER() OVER (ORDER BY a.kid desc) AS N,b.LLOJ_VEPRIMI,[NR],[DATE_VEPRIMI],[DATE_RREGJISTRIMI], " +
               " 	[KALLEZUES],[IKALLEZUAR],[FABUL],[NENI],[PRIND_KID],a.[AKTIV],[KOMENTE],[KID],[LLVID]" +
               "               FROM [dbo].[VEPRIM] a inner join LLOJ_VEPRIMI b on a.LLVID = b.ID " +
               " ) AS MyDerivedTable" +
               " WHERE MyDerivedTable.N BETWEEN " + rreshtFill.ToString() + " AND " + rreshtMbarim.ToString() + " and" +
               " (KALLEZUES like '%" + txtKerko + "%' or IKALLEZUAR like '%" + txtKerko + "%' or FABUL like '%" + txtKerko + "%' or NENI like '%" + txtKerko + "%')" +
               " order by N asc";

                Global.fillGridWithRef(ref dgCeshtje, Global.localConn, selectCommandGlobal, "", "Text");

                Color formBackColorAll = ColorTranslator.FromHtml("#424242");
                Color lblForeColor12 = ColorTranslator.FromHtml("#F2F2F2");

                dgCeshtje.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgCeshtje.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgCeshtje.RowTemplate.Height = 50;
                dgCeshtje.ForeColor = lblForeColor12;
                dgCeshtje.BackgroundColor = formBackColorAll;
                dgCeshtje.AlternatingRowsDefaultCellStyle.BackColor = formBackColorAll;
                dgCeshtje.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgCeshtje.RowsDefaultCellStyle.BackColor = formBackColorAll;
                dgCeshtje.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }

        private void dtpDateNga_ValueChanged(object sender, EventArgs e)
        {
            //dtpDateNga.Value = Convert.ToDateTime(dateFillimMuaj.Year.ToString() + "-" + dateFillimMuaj.Month.ToString() + "-" + dateFillimMuaj.Day.ToString() + " 00:00:00");
            this.Text = dtpDateNga.Value.ToString();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            ExportDynamicMe("", "GRIDE");
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            raportiPerzgjedhur = "PROKURORIGJITHA";
            lblCurrentReport.Text = "Prokurori ceshtje";

            //btnListeCeshtje.BZBackColor = Color.FromArgb(32, 37, 76);
            //btnListeCeshtjeTeHapura.BZBackColor = Color.FromArgb(32, 37, 76);
            //btnProcedimPenalSakte.BZBackColor = Color.FromArgb(32, 37, 76);
            //btnCeshtjeAnalitikeSakte.BZBackColor = Color.FromArgb(32, 37, 76);
            //btnProkuroriSakte.BZBackColor = Color.FromArgb(30, 30, 40);
            //panel2.Refresh();

            ExportDynamicMe("", "GRIDE");
        }

        private void btnProkurori_Click(object sender, EventArgs e)
        {
            string txtKerko = txtCeshtjeKerko.Text;
            raportiPerzgjedhur = "CESHTJEANALITIKE";
            lblCurrentReport.Text = "Ceshtje analitike";

            ExportDynamicMe("", "GRIDE");
        }

        private void btnAlerte_Click(object sender, EventArgs e)
        {
            string txtKerko = txtCeshtjeKerko.Text;
            raportiPerzgjedhur = "ALERTE";
            lblCurrentReport.Text = "Alerte";

            ExportDynamicMe("", "GRIDE");
        }

        private void btnPrint_Click(object sender, EventArgs e)//System.Drawing.Printing.PrintPageEventArgs e
        {
            PrintPreviewDialog1.ShowDialog();
            // Create string to draw.
            //String drawString = "Sample Text";

            //Bitmap bm = new Bitmap(this.dgCeshtje.Width, this.dgCeshtje.Height);
            //dgCeshtje.DrawToBitmap(bm, new Rectangle(0, 0, this.dgCeshtje.Width, this.dgCeshtje.Height));
            //e.Graphics.DrawImage(bm, 0, 0);

            //// Create font and brush.
            //Font drawFont = new Font("Arial", 16);
            //SolidBrush drawBrush = new SolidBrush(Color.Black);

            //// Create point for upper-left corner of drawing.
            //PointF drawPoint = new PointF(150.0F, 150.0F);

            //// Draw string to screen.
            //e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
        }
    
    }
}
