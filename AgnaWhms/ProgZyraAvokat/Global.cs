#region References
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.VisualBasic;
using System.Runtime;
//using CFPrinting;
using System.Web;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using System.Windows.Input;
using System.ServiceModel;
using AgnaWhms;
//using System.Windows.Forms.
//using WebEye;
//using System.Windows.con
using Microsoft.Office;
using Microsoft.Office.Interop;
using System.Globalization;
using System.Net.Sockets;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml;
using System.Media;



using System.Management;
using System.Data.SqlTypes;
using System.Reflection;

using System.Management.Instrumentation;
using Windows.Foundation;
using System.Security;
using System.Security.Principal;
#endregion

namespace ProgZyraAvokat
{
    public static class Global
    {
        public static VeprimKallezim veprimKallezim;
        public static notification myNotification;
        public static All_Info allInfo;
        public static VeprimProcedimPenal veprimProcedimPenal;
        public static VeprimThirrjeProkurori veprimThirrjeProkurori;
        public static Login loginForm;
        public static ListeKallezime listeCeshtje;
        public static LevizjeMagazina levizjeMagazina;
        public static Rafte rafte;
        public static ListeFatura listeFatura;
        public static NotifyMe notifyMe;
        public static FormeStoku formeStoku;
        public static Furnizim furnizim;
        public static TrupVeprimiShkresa trupVeprimiShkresa;
        //public static Raporte raporte;
        public static TrupVeprimiAkte trupVeprimiAkte;
        public static Dashboard_Form dashboardForm;

        public static Int32 nrCeshtjeSipasFilter;
        public static Int32 ceshtjePerFaqe;

        public static TrupVeprimeAkteProcedim trupVeprimAktProcedim;
        public static TrupVeprimShkresaProcedim trupVeprimShkresaProcedim;

        public static int screenWidth;
        public static int screenHeight;
        public static BindingSource bindingSource1 = new BindingSource();
        public static SqlDataAdapter dataAdapter = new SqlDataAdapter();
        public static string currentDirectory;

        public static string hapVeprimi;
        public static System.Windows.Forms.Timer timerGlobal = new System.Windows.Forms.Timer();
        public static int kallezimId;

        #region cellInfo
        public static int cellId;
        public static int cellWarehouseID;
        public static string cellX;
        public static string cellY;
        public static string cellZ;
        public static string cellW;
        public static string cellNotes;
        public static String cellDataTime;
        #endregion

        #region MovsCells
        public static int movementCeId;
        public static int movDetId;
        public static int moveCellId;
        public static int moveQty;
        public static int movStatusId;
        public static int movCatId;
        #endregion  

        public static int idVeprimi;
        public static int idVeprimiProcedurePenale;
        public static int idProkurori;
        public static int idTrupVeprimi;
        public static string idTrupVeprimiLlojId;

        public static int orderId;
        public static int orderStatusId;
        public static int orderMovStatusId;
        public static int orderMovCatId;
        public static int orderWhmsId;

        public static int orderUserId;
        public static int orderPorosiId;
        public static string orderPorosiNr;
        public static int orderAreaId;
        public static string orderNr;
        public static string orderTime;
        public static string orderNotes;

        public static int orderDetailProdId;
        public static int orderDetailStatusId;
        public static int orderDetailLotId;
        public static string orderDetailProdNav;
        public static string orderDetailLotNr;
        public static string orderDetailBarcode;
        public static Boolean orderDetailPackX;
        public static int orderDetailUnitPack;
        public static int orderDetailPackNrX;
        public static int orderDetailQuantity;
        public static Double orderDetailPrice;
        public static string orderDetailNotes;
        public static int orderDetailsMovStatusId;

        public static int LLVID ;
	    public static string NR ;
        public static string DATE_VEPRIMI  ;
        public static string DATE_RREGJISTRIMI ;
        public static string KALLEZUES;
        public static string IKALLEZUAR;
        public static string FABUL;
        public static string NENI;
        public static int PRIND_KID;
        public static int AKTIV;
        public static int AKTIV_TRUPI_HYRJE;
        public static string KOMENTE;
        public static string DATE_ALERTI;
        public static string STATUS_CESHTJE;
        public static string DATE_STATUS_CESHTJE;

        //trup veprimi
        public static int KID;
        public static int LLTVID;
        public static string NR_PROTOKOLLI;
        public static string DATE;
        public static string LENDA;
        public static string DERGUESI;
        public static string MARRESI;
        public static string FABUL_TRUPI;
        public static string LLOJI_AKT_PROCEDURIAL;
        public static int AKTIV_TRUPI ;
        public static string DATE_CREATED ;
        public static string KOMENTE_TRUPI ;
        public static string PROKURORI;
        public static string IPANDEHURI;

        public static int kallezimePageNr = 1;

        public static string localConn;
        public static string localConnB2B;
        public static string localConnStockTr;

        #region callSp
        public static bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //[MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
        public static void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                String dateTani = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                string alert, dateAlerti, selectComm ;
                bool shfaq = true;
                notification myNotification = null;
                if ((System.DateTime.Now.Minute % 1) == 0 && System.DateTime.Now.Second == 35)//&& System.DateTime.Now.Millisecond == 1
                {
                    selectComm = "SELECT KID, KOMENTE as ALERT, DATE_ALERTI FROM VEPRIM where komente <> '' and AKTIV = 1 and " +
                    " dbo.ufn_GetDateOnly_Full(DATE_ALERTI) = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "'";
                    DataTable myDtTbl = Global.returnTableForGrid(Global.localConn,
                   selectComm, //'01/04/2020 19:02:32', 
                    "Text", "Execute", null, "Text");
                    if (myDtTbl != null && myDtTbl.Rows.Count > 0)
                    {
                        //if (Global.myNotification == null)
                        //{
                        //    Global.myNotification = new notification();
                        //}
                        //if (Global.myNotification.IsDisposed)
                        //{
                        //    Global.myNotification = new notification();
                        //}
                        if (shfaq)
                        {
                            shfaq = false;
                            myNotification = new notification();
                            myNotification.setText(myDtTbl.Rows[0]["DATE_ALERTI"].ToString() + ":" + myDtTbl.Rows[0]["ALERT"].ToString());
                            myNotification.Show();
                            playSimpleSound();

                            Thread.Sleep(1000);
                        }
                       
                    }

                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("Gabim timer1_Tick " + EX.Message);
            }
        }
        public static void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }
        public static bool isDateTime(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool shtoKokeVeprimi(string dateVeprimi)
        {
            try
            {
                Global.hapVeprimi = "KALLEZIM";
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@KID", parameterValue = Global.idVeprimi.ToString() },
                    new ParameterObject(){ parameterName = "@LLVID", parameterValue = Global.LLVID.ToString() },
                    new ParameterObject(){ parameterName = "@NR", parameterValue = Global.NR },
                    new ParameterObject(){ parameterName = "@DATE_VEPRIMI", parameterValue =  Global.DATE_VEPRIMI },//DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    new ParameterObject(){ parameterName = "@DATE_RREGJISTRIMI", parameterValue = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")},
                    new ParameterObject(){ parameterName = "@KALLEZUES", parameterValue = Global.KALLEZUES},
                    new ParameterObject(){ parameterName = "@IKALLEZUAR", parameterValue = Global.IKALLEZUAR  },
                    new ParameterObject(){ parameterName = "@FABUL", parameterValue = Global.FABUL},
                    new ParameterObject(){ parameterName = "@NENI", parameterValue = Global.NENI  },
                    new ParameterObject(){ parameterName = "@PRIND_KID", parameterValue =  Global.PRIND_KID.ToString() },
                    new ParameterObject(){ parameterName = "@AKTIV", parameterValue = Global.AKTIV.ToString() },
                    new ParameterObject(){ parameterName = "@KOMENTE", parameterValue = Global.KOMENTE },
                    new ParameterObject(){ parameterName = "@DATE_ALERTI", parameterValue =  Global.DATE_ALERTI },//DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""},
                };

                Global.callSqlCommand(Global.localConn, "sp_ShtoKokeVeprimi", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogData("Gabim shtoKokeVeprimi " , ex.Message);
                MessageBox.Show("Gabim shtoKokeVeprimi " + ex.Message);
                return false;
            }
        }
        public static bool shto_TrupiMagSakte(string dateVeprimi)
        {
            try
            {
             //   @ORDER_ID as int,
	            //@MovHeadID as int,
	            //@ProductID as int,
	            //@MovStatusID as int,
             //   @LotID AS int,
             //   @ProductNav AS Nvarchar(10),
             //   @LotNr AS Nvarchar(100),
             //   @BarcodeX AS nvarchar(50),
             //   @PackX AS bit,
             //   @UnitsPackX AS int,
             //   @PackNrX AS int,
             //   @QtyX AS int,
             //   @ProductPrice AS numeric(2, 0),
             //   @MovDetNotes AS Nvarchar(250),
                Global.hapVeprimi = "levizjeTrupi";
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@ORDER_ID", parameterValue = Global.idTrupVeprimi.ToString() },
                    new ParameterObject(){ parameterName = "@MovHeadID", parameterValue = Global.idVeprimi.ToString() },
                    new ParameterObject(){ parameterName = "@ProductID", parameterValue = Global.orderDetailProdId.ToString() },
                    new ParameterObject(){ parameterName = "@MovStatusID", parameterValue =  Global.orderDetailsMovStatusId.ToString() },//DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    new ParameterObject(){ parameterName = "@LotID", parameterValue =  Global.orderDetailLotId.ToString()},
                    new ParameterObject(){ parameterName = "@ProductNav", parameterValue = Global.orderDetailProdNav.ToString()  },
                    new ParameterObject(){ parameterName = "@LotNr", parameterValue = Global.orderDetailLotNr.ToString()},
                    new ParameterObject(){ parameterName = "@BarcodeX", parameterValue = Global.orderDetailBarcode  },
                    new ParameterObject(){ parameterName = "@PackX", parameterValue = "1"  },
                    new ParameterObject(){ parameterName = "@UnitsPackX", parameterValue = Global.orderDetailUnitPack.ToString() },
                    new ParameterObject(){ parameterName = "@PackNrX", parameterValue = Global.orderDetailPackNrX.ToString() },
                    new ParameterObject(){ parameterName = "@QtyX", parameterValue = Global.orderDetailQuantity.ToString()  },
                    new ParameterObject(){ parameterName = "@ProductPrice", parameterValue = Global.orderDetailPrice.ToString() },
                    new ParameterObject(){ parameterName = "@MovDetNotes", parameterValue = Global.orderDetailNotes.ToString() },
                    new ParameterObject(){ parameterName = "@Aktiv", parameterValue = Global.AKTIV_TRUPI_HYRJE.ToString() },
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""},
                };

                Global.callSqlCommand(Global.localConn, "sp_Shto_wMovDetails", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogData("Gabim shtoKokeVeprimi ", ex.Message);
                MessageBox.Show("Gabim shtoKokeVeprimi " + ex.Message);
                return false;
            }
        }
        public static bool shto_KokaMagSakte(string dateFillimNderto)
        {
            try
            {
                Global.hapVeprimi = "levizje";
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@ORDER_ID", parameterValue = Global.idVeprimi.ToString() },
                    new ParameterObject(){ parameterName = "@MovCatID", parameterValue = Global.orderMovCatId.ToString() },
                    new ParameterObject(){ parameterName = "@MovStatusID", parameterValue = Global.orderMovStatusId.ToString() },
                    new ParameterObject(){ parameterName = "@UserID", parameterValue =  Global.orderUserId.ToString() },//DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    new ParameterObject(){ parameterName = "@WarehouseID", parameterValue =  Global.orderWhmsId.ToString()},
                    new ParameterObject(){ parameterName = "@OrderID", parameterValue = Global.orderPorosiId.ToString()  },
                    new ParameterObject(){ parameterName = "@AreaID", parameterValue = Global.orderAreaId.ToString()},
                    new ParameterObject(){ parameterName = "@OrderNr", parameterValue = Global.orderPorosiNr  },
                    new ParameterObject(){ parameterName = "@MovHeadNr", parameterValue = Global.orderNr.ToString()  },
                    new ParameterObject(){ parameterName = "@MovHeadTime", parameterValue = Global.orderTime.ToString() },
                    new ParameterObject(){ parameterName = "@MovHeadNotes", parameterValue = Global.orderNotes },
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""}
                };

                Global.callSqlCommand(Global.localConn, "sp_Shto_wMovHeads", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogData("Gabim shto_KokaMagSakte ", ex.Message);
                MessageBox.Show("Gabim shto_KokaMagSakte " + ex.Message);
                return false;
            }
        }
        public static bool shto_Cells(string dateFillimNderto)
        {
            try
            {
                Global.hapVeprimi = "CELLS";
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@CELL_ID", parameterValue = Global.cellId.ToString() },
                    new ParameterObject(){ parameterName = "@WarehouseID", parameterValue = Global.cellWarehouseID.ToString() },
                    new ParameterObject(){ parameterName = "@CellX", parameterValue = Global.cellX.ToString() },
                    new ParameterObject(){ parameterName = "@CellY", parameterValue =  Global.cellY.ToString() },//DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    new ParameterObject(){ parameterName = "@CellZ", parameterValue =  Global.cellZ.ToString()},
                    new ParameterObject(){ parameterName = "@CellW", parameterValue = Global.cellW.ToString()  },
                    new ParameterObject(){ parameterName = "@CellNotes", parameterValue = Global.cellNotes.ToString()},
                    new ParameterObject(){ parameterName = "@CellTS", parameterValue = Global.cellDataTime.ToString() },
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""}
                };

                Global.callSqlCommand(Global.localConn, "sp_Shto_Cell", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogData("Gabim sp_Shto_Cell ", ex.Message);
                MessageBox.Show("Gabim sp_Shto_Cell " + ex.Message);
                return false;
            }
        }
        public static bool shto_MovCells(string dateFillimNderto)
        {
            try
            {
                Global.hapVeprimi = "MOVCELLS";
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@MovCeID", parameterValue = Global.movementCeId.ToString() },
                    new ParameterObject(){ parameterName = "@MovDetID", parameterValue = Global.movDetId.ToString() },
                    new ParameterObject(){ parameterName = "@CellID", parameterValue = Global.moveCellId.ToString() },
                    new ParameterObject(){ parameterName = "@Qty", parameterValue =  Global.moveQty.ToString() },//DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    new ParameterObject(){ parameterName = "@MovStatusID", parameterValue =  Global.movStatusId.ToString()},
                    new ParameterObject(){ parameterName = "@MovCatID", parameterValue = Global.movCatId.ToString()  },
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""}
                };

                Global.callSqlCommand(Global.localConn, "sp_Shto_wMovCells", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogData("Gabim shto_MovCells ", ex.Message);
                MessageBox.Show("Gabim shto_MovCells " + ex.Message);
                return false;
            }
        }
        public static bool shto_LevizjeNgaPorosi(string dateVeprimi)
        {
            try
            {
                Global.hapVeprimi = "levizjeNgaPorosi";
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@ORDER_ID", parameterValue = Global.idVeprimi.ToString() },
                    new ParameterObject(){ parameterName = "@MovCatID", parameterValue = Global.orderMovCatId.ToString() },
                    new ParameterObject(){ parameterName = "@MovStatusID", parameterValue = Global.orderMovStatusId.ToString() },
                    new ParameterObject(){ parameterName = "@UserID", parameterValue =  Global.orderUserId.ToString() },//DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    new ParameterObject(){ parameterName = "@WarehouseID", parameterValue =  Global.orderWhmsId.ToString()},
                    new ParameterObject(){ parameterName = "@OrderID", parameterValue = Global.orderPorosiId.ToString()  },
                    new ParameterObject(){ parameterName = "@AreaID", parameterValue = Global.orderAreaId.ToString()},
                    new ParameterObject(){ parameterName = "@OrderNr", parameterValue = Global.orderPorosiNr  },
                    new ParameterObject(){ parameterName = "@MovHeadNr", parameterValue = Global.orderNr.ToString()  },
                    new ParameterObject(){ parameterName = "@MovHeadTime", parameterValue = Global.orderTime.ToString() },
                    new ParameterObject(){ parameterName = "@MovHeadNotes", parameterValue = Global.orderNotes },
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""}
                };

                Global.callSqlCommand(Global.localConn, "sp_Shto_Levizje_Nga_Porosi", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogData("Gabim shto_LevizjeNgaPorosi ", ex.Message);
                MessageBox.Show("Gabim shto_LevizjeNgaPorosi " + ex.Message);
                return false;
            }
        }
        public static bool shto_wMovHeads()
        {
            try
            {
                string insert = "INSERT INTO [dbo].[wMovHeads] " +
                       " ([MovCatID] " +
                       " ,[MovStatusID] " +
                       " ,[UserID] " +
                       " ,[WarehouseID] " +
                       " ,[OrderID] " +
                       " ,[AreaID] " +
                       " ,[OrderNr] " +
                       " ,[MovHeadNr] " +
                       " ,[MovHeadTime] " +
                       " ,[MovHeadNotes]) " +
                 " VALUES " +
                       " (' " +
                        Global.orderMovCatId.ToString() +
                       "',' " + Global.orderMovStatusId.ToString() +
                       "',' " + Global.orderUserId.ToString() +
                       "','" + Global.orderWhmsId.ToString() +
                       "','" + Global.orderPorosiId.ToString() +
                       "','" + Global.orderAreaId.ToString() +
                       "','" + Global.orderPorosiNr.ToString() +
                       "','" + Global.orderNr.ToString() +
                       "','" + Global.orderTime.ToString() +
                       "','" + Global.orderNotes + "')";

                Global.callSqlCommand(Global.localConn, insert, "Text", "Execute", null);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogData("Gabim shtoKokeVeprimi ", ex.Message);
                MessageBox.Show("Gabim shtoKokeVeprimi " + ex.Message);
                return false;
            }
        }
        public static bool shtoKokeVeprimiProcedurePenale(string dateVeprimi)
        {
            try
            {
                Global.hapVeprimi = "PROCEDIM";
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@KID", parameterValue = Global.idVeprimiProcedurePenale.ToString() },
                    new ParameterObject(){ parameterName = "@LLVID", parameterValue = Global.LLVID.ToString() },
                    new ParameterObject(){ parameterName = "@NR", parameterValue = Global.NR },
                    new ParameterObject(){ parameterName = "@DATE_VEPRIMI", parameterValue =  Global.DATE_VEPRIMI },//DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    new ParameterObject(){ parameterName = "@DATE_RREGJISTRIMI", parameterValue = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")},
                    new ParameterObject(){ parameterName = "@KALLEZUES", parameterValue = Global.KALLEZUES},
                    new ParameterObject(){ parameterName = "@IKALLEZUAR", parameterValue = Global.IKALLEZUAR  },
                    new ParameterObject(){ parameterName = "@FABUL", parameterValue = Global.FABUL},
                    new ParameterObject(){ parameterName = "@NENI", parameterValue = Global.NENI  },
                    new ParameterObject(){ parameterName = "@PRIND_KID", parameterValue =  Global.PRIND_KID.ToString() },
                    new ParameterObject(){ parameterName = "@AKTIV", parameterValue = Global.AKTIV.ToString() },
                    new ParameterObject(){ parameterName = "@KOMENTE", parameterValue = Global.KOMENTE },
                    new ParameterObject(){ parameterName = "@DATE_ALERTI", parameterValue =  Global.DATE_ALERTI },//DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") 
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""},
                };

                Global.callSqlCommand(Global.localConn, "sp_ShtoKokeVeprimi", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                Log.LogData("Gabim shtoKokeVeprimi ", ex.Message);
                MessageBox.Show("Gabim shtoKokeVeprimi " + ex.Message);
                return false;
            }
        }
        public static bool shtoTrupVeprimi(string dateVeprimi)
        {
            try
            {
                // Global.idVeprimi = Global.returnMax("VEPRIM");
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@TID", parameterValue = Global.idTrupVeprimi.ToString() },
                    new ParameterObject(){ parameterName = "@KID", parameterValue = Global.idVeprimi.ToString() },
                    new ParameterObject(){ parameterName = "@LLTVID", parameterValue = Global.LLTVID.ToString() },
                    new ParameterObject(){ parameterName = "@NR_PROTOKOLLI", parameterValue = Global.NR_PROTOKOLLI.ToString()},
                    new ParameterObject(){ parameterName = "@DATE", parameterValue = Global.DATE.ToString()},
                    new ParameterObject(){ parameterName = "@LENDA", parameterValue = Global.LENDA},
                    new ParameterObject(){ parameterName = "@DERGUESI", parameterValue = Global.DERGUESI  },
                    new ParameterObject(){ parameterName = "@MARRESI", parameterValue = Global.MARRESI},
                    new ParameterObject(){ parameterName = "@FABUL", parameterValue = Global.FABUL_TRUPI  },
                    new ParameterObject(){ parameterName = "@LLOJI_AKT_PROCEDURIAL", parameterValue =  Global.LLOJI_AKT_PROCEDURIAL.ToString() },
                    new ParameterObject(){ parameterName = "@AKTIV", parameterValue = Global.AKTIV_TRUPI.ToString() },
                    new ParameterObject(){ parameterName = "@DATE_CREATED", parameterValue = Global.DATE_CREATED },
                    new ParameterObject(){ parameterName = "@KOMENTE", parameterValue = Global.KOMENTE_TRUPI},
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""},
                };

                Global.callSqlCommand(Global.localConn, "sp_ShtoTrupVeprimi", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim sp_ShtoTrupVeprimi " + ex.Message);
                return false;
            }
        }
        public static bool shtoTrupVeprimi_ProcedimPenal(string dateVeprimi)
        {
            try
            {
                // Global.idVeprimi = Global.returnMax("VEPRIM");
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@TID", parameterValue = Global.idTrupVeprimi.ToString() },
                    new ParameterObject(){ parameterName = "@KID", parameterValue = Global.idVeprimiProcedurePenale.ToString() },
                    new ParameterObject(){ parameterName = "@LLTVID", parameterValue = Global.LLTVID.ToString() },
                    new ParameterObject(){ parameterName = "@NR_PROTOKOLLI", parameterValue = Global.NR_PROTOKOLLI.ToString()},
                    new ParameterObject(){ parameterName = "@DATE", parameterValue = Global.DATE.ToString()},
                    new ParameterObject(){ parameterName = "@LENDA", parameterValue = Global.LENDA},
                    new ParameterObject(){ parameterName = "@DERGUESI", parameterValue = Global.DERGUESI  },
                    new ParameterObject(){ parameterName = "@MARRESI", parameterValue = Global.MARRESI},
                    new ParameterObject(){ parameterName = "@FABUL", parameterValue = Global.FABUL_TRUPI  },
                    new ParameterObject(){ parameterName = "@LLOJI_AKT_PROCEDURIAL", parameterValue =  Global.LLOJI_AKT_PROCEDURIAL.ToString() },
                    new ParameterObject(){ parameterName = "@AKTIV", parameterValue = Global.AKTIV_TRUPI.ToString() },
                    new ParameterObject(){ parameterName = "@DATE_CREATED", parameterValue = Global.DATE_CREATED },
                    new ParameterObject(){ parameterName = "@KOMENTE", parameterValue = Global.KOMENTE_TRUPI},
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""},
                };

                Global.callSqlCommand(Global.localConn, "sp_ShtoTrupVeprimi", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim sp_ShtoTrupVeprimi " + ex.Message);
                return false;
            }
        }
        public static bool shto_ThirrjeProkurori(string dateVeprimi)
        {
            try
            {
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@ID", parameterValue = Global.idTrupVeprimi.ToString() },
                    new ParameterObject(){ parameterName = "@LLTVID", parameterValue = Global.LLTVID.ToString() },
                    new ParameterObject(){ parameterName = "@DATA", parameterValue = Global.DATE.ToString()},
                    new ParameterObject(){ parameterName = "@PROKURORI", parameterValue = Global.PROKURORI},
                    new ParameterObject(){ parameterName = "@IPANDEHURI", parameterValue = Global.IPANDEHURI  },
                    new ParameterObject(){ parameterName = "@NENI", parameterValue = Global.NENI},
                    new ParameterObject(){ parameterName = "@AKTIV", parameterValue = Global.AKTIV_TRUPI.ToString() },
                    new ParameterObject(){ parameterName = "@DATE_RREGJISTRIMI", parameterValue = Global.DATE_CREATED },
                    new ParameterObject(){ parameterName = "@KOMENTE", parameterValue = Global.KOMENTE_TRUPI},
                    new ParameterObject(){ parameterName = "@PRIND_ID", parameterValue = Global.idVeprimiProcedurePenale.ToString() },
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""},
                };

                Global.callSqlCommand(Global.localConn, "sp_ShtoThirrjeProkurori", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim sp_ShtoTrupVeprimi " + ex.Message);
                return false;
            }
        }
        public static bool ndryshoStatusCeshtje()
        {
            try
            {
                List<ParameterObject> listParam = new List<ParameterObject>()
                {
                    new ParameterObject(){ parameterName = "@PROCEDIM_PENALE_ID", parameterValue = Global.idVeprimiProcedurePenale.ToString() },
                    new ParameterObject(){ parameterName = "@CESHTJE_STATUS_DATE", parameterValue = Global.DATE_STATUS_CESHTJE.ToString()},
                    new ParameterObject(){ parameterName = "@STATUS", parameterValue = Global.STATUS_CESHTJE },
                    new ParameterObject(){ parameterName = "@prBID", parameterValue = ""},
                };

                Global.callSqlCommand(Global.localConn, "sp_UpdateCeshtjeStatus", "SP", "Execute", listParam);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim sp_ShtoTrupVeprimi " + ex.Message);
                return false;
            }
        }
        #endregion

        #region data crud
        public static IDataReader callSqlCommand(string server, string db, string username, string password,
      string sqlCmdText, string sqlCmdType, string ExecuteOperation, List<ParameterObject> parameterList)
        {
            SqlParameter outputIdParam = null;
            SqlDataReader sqlDatRead = null;
            SqlConnection sqlCon = new SqlConnection("Data Source=" + server + ";Initial Catalog=" + db +
                ";Persist Security Info=True;User ID=" + username + ";Password=" + password);
            try
            {
                sqlCon.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = sqlCon;
                sqlComm.CommandText = sqlCmdText;
                sqlComm.CommandTimeout = 3600;
                if (sqlCmdType == "SP")
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                }
                else if (sqlCmdType == "Text")
                {
                    sqlComm.CommandType = CommandType.Text;
                }
                if (parameterList != null)
                {
                    for (int i = 0; i < parameterList.Count; i++)
                    {
                        if (parameterList[i].parameterName == "@BDT_11" || parameterList[i].parameterName == "@BDTRG_12" || parameterList[i].parameterName == "@BDST_22")
                        {
                            sqlComm.Parameters.Add(parameterList[i].parameterName, SqlDbType.DateTime).Value = Convert.ToDateTime(parameterList[i].parameterValue);// 500;
                        }
                        else if ((parameterList[i].parameterName == "@prBID"))
                        {
                            outputIdParam = new SqlParameter("@prBID", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            sqlComm.Parameters.Add(outputIdParam);
                        }
                        else
                        {
                            sqlComm.Parameters.AddWithValue(parameterList[i].parameterName, parameterList[i].parameterValue);
                        }
                    }
                }

                if (ExecuteOperation == "Execute")
                {
                    sqlComm.ExecuteNonQuery();

                    if (outputIdParam != null)
                    {
                        //MessageBox.Show("Return Val " + outputIdParam.Value.ToString());
                    }
                }
                else //returns dataSet
                {
                    sqlDatRead = sqlComm.ExecuteReader();
                }

                //sqlCon.Close();
                return sqlDatRead;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (sqlCon != null)
                {
                    sqlCon.Close();
                }
                return null;
            }
        }
        public static IDataReader callSqlCommand(string myConnectionString,
          string sqlCmdText, string sqlCmdType, string ExecuteOperation, List<ParameterObject> parameterList)
        {
            SqlParameter outputIdParam = null;
            SqlDataReader sqlDatRead = null;
            SqlConnection sqlCon = new SqlConnection(myConnectionString);
            try
            {
                sqlCon.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = sqlCon;
                sqlComm.CommandText = sqlCmdText;
                sqlComm.CommandTimeout = 7200;
                if (sqlCmdType == "SP")
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                }
                else if (sqlCmdType == "Text")
                {
                    sqlComm.CommandType = CommandType.Text;
                }
                if (parameterList != null)
                {
                    for (int i = 0; i < parameterList.Count; i++)
                    {
                        if (parameterList[i].parameterName == "@DATE_VEPRIMI" || parameterList[i].parameterName == "@DATE_RREGJISTRIMI" || parameterList[i].parameterName == "@CESHTJE_STATUS_DATE" ||
                            parameterList[i].parameterName == "@DATE" || parameterList[i].parameterName == "@DATE_CREATED" || parameterList[i].parameterName == "@DATA"
                            || parameterList[i].parameterName == "@BDST_22" || parameterList[i].parameterName == "@DATE_ALERTI" 
                            || parameterList[i].parameterName == "@MovHeadTime" || parameterList[i].parameterName == "@CellTS")
                        {
                            sqlComm.Parameters.Add(parameterList[i].parameterName, SqlDbType.DateTime).Value = Convert.ToDateTime(parameterList[i].parameterValue);// 500;
                        }
                        else if ((parameterList[i].parameterName == "@prBID"))
                        {
                            outputIdParam = new SqlParameter("@prBID", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };
                            sqlComm.Parameters.Add(outputIdParam);
                        }
                        else if ((parameterList[i].parameterName == "@MovHeadNr"))
                        {
                            sqlComm.Parameters.Add(parameterList[i].parameterName, SqlDbType.BigInt).Value = Convert.ToInt64(parameterList[i].parameterValue);// 500;
                        }
                        //else if ((parameterList[i].parameterName == "@ProductPrice"))
                        //{
                        //    sqlComm.Parameters.Add(parameterList[i].parameterName, SqlDbType.Decimal).Value = Convert.ToDecimal(parameterList[i].parameterValue);// 500;
                        //}
                        else
                        {
                            sqlComm.Parameters.AddWithValue(parameterList[i].parameterName, parameterList[i].parameterValue);
                        }
                    }
                }

                if (ExecuteOperation == "Execute")
                {
                    sqlComm.ExecuteNonQuery();

                    //if (outputIdParam == null)
                    //{
                    //    MessageBox.Show("Vlere kthimi gabim");
                    //    return null;
                    //}
                    if (outputIdParam != null && outputIdParam.Value.ToString() == "99")
                    {
                        MessageBox.Show("Vlere kthimi gabim ");
                        return null;
                    }
                    else if (sqlCmdText == "sp_Shto_wMovHeads" || sqlCmdText == "sp_Shto_wMovDetails" 
                        || sqlCmdText == "sp_Shto_Levizje_Nga_Porosi" || sqlCmdText == "sp_Shto_Cell")
                    {
                        if (Global.hapVeprimi.ToUpper() == "LEVIZJE" || Global.hapVeprimi.ToUpper() == "LEVIZJENGAPOROSI")
                        {
                            Global.idVeprimi = Convert.ToInt32(outputIdParam.Value.ToString());
                        }
                        else if (Global.hapVeprimi == "LEVIZJETRUPI")
                        {
                            Global.idTrupVeprimi= Convert.ToInt32(outputIdParam.Value.ToString());
                        }
                        else if (Global.hapVeprimi == "CELLS")
                        {
                            Global.cellId = Convert.ToInt32(outputIdParam.Value.ToString());
                        }
                        else if (Global.hapVeprimi == "MOVCELLS")
                        {
                            Global.movementCeId= Convert.ToInt32(outputIdParam.Value.ToString());
                        }
                    }
                }
                else //returns dataSet
                {
                    sqlDatRead = sqlComm.ExecuteReader();
                }

                //sqlCon.Close();
                return sqlDatRead;
            }
            catch (Exception ex)
            {
                MessageBox.Show("callSqlCommand, " + sqlCmdText + "," + ex.Message);
                if (sqlCon != null)
                {
                    sqlCon.Close();
                }
                return null;
            }
        }
        public static SqlCommand returnSqlCommand(string server, string db, string username, string password,
          string sqlCmdText, string sqlCmdType, string ExecuteOperation, List<ParameterObject> parameterList)
        {
            SqlConnection sqlCon = new SqlConnection("Data Source=" + server + ";Initial Catalog=" + db +
                ";Persist Security Info=True;User ID=" + username + ";Password=" + password);
            try
            {
                sqlCon.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = sqlCon;
                sqlComm.CommandText = sqlCmdText;
                sqlComm.CommandTimeout = 3600;
                if (sqlCmdType == "SP")
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                }
                else if (sqlCmdType == "Text")
                {
                    sqlComm.CommandType = CommandType.Text;
                }
                if (parameterList != null)
                {
                    for (int i = 0; i < parameterList.Count; i++)
                    {
                        sqlComm.Parameters.Add(parameterList[i].parameterName, SqlDbType.Int).Value = parameterList[i].parameterValue;// 500;
                    }
                }
                //sqlCon.Close();
                return sqlComm;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (sqlCon != null)
                {
                    sqlCon.Close();
                }
                return null;
            }
        }
        public static String callSpReturnValue(string myConnectionString,
           string sqlCmdText, string sqlCmdType, string ExecuteOperation, List<ParameterObject> parameterList)
        {
            SqlParameter outputIdParam = null;
            SqlParameter errorMsgParam = null;
            SqlConnection sqlCon = new SqlConnection(myConnectionString);
            //MessageBox.Show(myConnectionString);
            try
            {
                sqlCon.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = sqlCon;
                sqlComm.CommandText = sqlCmdText;
                sqlComm.CommandTimeout = 3600;
                if (sqlCmdType == "SP")
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                }
                else if (sqlCmdType == "Text")
                {
                    sqlComm.CommandType = CommandType.Text;
                }
                if (parameterList != null)
                {
                    for (int i = 0; i < parameterList.Count; i++)
                    {
                        if (parameterList[i].parameterName == "@BDT_11" || parameterList[i].parameterName == "@BDTRG_12"
                            || parameterList[i].parameterName == "@BDST_22" || parameterList[i].parameterName == "@BDST_22")
                        {
                            sqlComm.Parameters.Add(parameterList[i].parameterName, SqlDbType.DateTime).Value = Convert.ToDateTime(parameterList[i].parameterValue);// 500;
                        }
                        else if ((parameterList[i].parameterName == "@prBID"))
                        {
                            if (sqlCmdText == "sp_KtheDiteSot" ||
                                sqlCmdText == "sp_Kthe_Tf_Or_Tn" ||
                                sqlCmdText == "sel_CurrentInspektorId_Ppc" ||
                                sqlCmdText == "kthe_FurnitorShoqeri_Fature_Ppc" ||
                                sqlCmdText == "sel_SHoqeri_E_re_All_defaults" ||
                                sqlCmdText == "kthe_PikeShitje_ByPshid" ||
                                sqlCmdText == "kthe_UserRights")
                            {
                                outputIdParam = new SqlParameter("@prBID", SqlDbType.VarChar, 50);
                                outputIdParam.Direction = ParameterDirection.Output;
                            }
                            else
                            {
                                outputIdParam = new SqlParameter("@prBID", SqlDbType.Int);
                                outputIdParam.Direction = ParameterDirection.Output;
                            }
                            sqlComm.Parameters.Add(outputIdParam);
                        }
                        else if (parameterList[i].parameterName == "@ErrorMessage")
                        {
                            errorMsgParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1);
                            errorMsgParam.Direction = ParameterDirection.Output;
                            sqlComm.Parameters.Add(errorMsgParam);
                        }
                        else if (parameterList[i].parameterName == "@entitetPerTuLexuar")
                        {
                            outputIdParam = new SqlParameter("@entitetPerTuLexuar", SqlDbType.VarChar, 50);
                            outputIdParam.Direction = ParameterDirection.Output;
                            sqlComm.Parameters.Add(outputIdParam);
                        }
                        else if (parameterList[i].parameterName == "@distanceInMetres")
                        {
                            outputIdParam = new SqlParameter("@distanceInMetres", SqlDbType.VarChar, 50);
                            outputIdParam.Direction = ParameterDirection.Output;
                            sqlComm.Parameters.Add(outputIdParam);
                        }
                        else if (parameterList[i].parameterName == "@set_krede")
                        {
                            errorMsgParam = new SqlParameter("@set_krede", SqlDbType.VarChar, 500);
                            errorMsgParam.Direction = ParameterDirection.Output;
                            //outputIdParam = new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 1024);
                            //outputIdParam.Direction = ParameterDirection.Output;

                            sqlComm.Parameters.Add(errorMsgParam);
                        }
                        else
                        {
                            sqlComm.Parameters.AddWithValue(parameterList[i].parameterName, parameterList[i].parameterValue);
                        }
                    }
                }
                string outPutVal = "";

                if (ExecuteOperation == "Execute")
                {
                    sqlComm.ExecuteNonQuery();
                    //if (sqlCmdText == "fshiEntitetete")
                    //{
                    //    MessageBox.Show("U fshine ");
                    //}
                    if (outputIdParam != null)
                    {
                        outPutVal = outputIdParam.Value.ToString();
                    }
                    else
                    {
                        outPutVal = "";
                    }

                    if (errorMsgParam != null)
                    {
                        if (errorMsgParam.Value.ToString() != "")
                        {
                            if (sqlCmdText == "sp_KtheKredenciale_ByUser")
                            {
                                outPutVal = errorMsgParam.Value.ToString();
                            }
                            else
                            {
                                MessageBox.Show("sqlCmdText:" + sqlCmdText + ",@ErrorMessage " + errorMsgParam.Value.ToString());
                            }
                        }
                    }

                    //if (sqlCmdText == "callMyExec" && sqlComm.Parameters["@ErrorMessage"].Value != null )
                    //{
                    //    MessageBox.Show("@ErrorMessage " + sqlComm.Parameters["@ErrorMessage"].Value.ToString());
                    //}
                }
                sqlCon.Close();
                return outPutVal;
            }
            catch (Exception ex)
            {
                if (sqlCmdText.IndexOf("faturaShites") != -1)
                {
                    MessageBox.Show("Gabim Nuk lidhet me db lokale :" + sqlCmdText);
                }
                else
                {
                    MessageBox.Show("Gabim callSpReturnValue,sqlCmdText :" + sqlCmdText + ex.Message + "," + sqlCmdText);
                }

                if (sqlCon != null)
                {
                    sqlCon.Close();
                }
                return "";
            }
        }
        public static string returnValForQuery(string query,string conn)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand(query, connect);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            String retVal;
                            retVal = reader[0].ToString();// reader.GetString(0);
                            return retVal;
                        }
                        else
                        {
                            return "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return "-100";
        }
        #endregion

        public static List<String> AcceptableDateFormats = new List<String>(180);
        public  static Boolean IsDate(Object value, DateTimeFormatInfo formatInfo)
        {
            if (AcceptableDateFormats.Count == 0)
            {
                foreach (var dateFormat in new[] { "d", "dd" })
                {
                    foreach (var monthFormat in new[] { "M", "MM", "MMM" })
                    {
                        foreach (var yearFormat in new[] { "yy", "yyyy" })
                        {
                            foreach (var separator in new[] { "-", "/", formatInfo.DateSeparator })
                            {
                                String shortDateFormat;
                                shortDateFormat = dateFormat + separator + monthFormat + separator + yearFormat;
                                AcceptableDateFormats.Add(shortDateFormat);
                                AcceptableDateFormats.Add(shortDateFormat + " " + "HH:mm");
                                AcceptableDateFormats.Add(shortDateFormat + " " + "HH:mm:ss");
                                AcceptableDateFormats.Add(shortDateFormat + " " + "HH" + formatInfo.TimeSeparator + "mm");
                                AcceptableDateFormats.Add(shortDateFormat + " " + "HH" + formatInfo.TimeSeparator + "mm" + formatInfo.TimeSeparator + "ss");
                            }
                        }
                    }
                }
                AcceptableDateFormats = AcceptableDateFormats.Distinct().ToList();
            }

            DateTime unused;
            return DateTime.TryParseExact(value.ToString(), AcceptableDateFormats.ToArray(), formatInfo, DateTimeStyles.AllowWhiteSpaces, out unused);
        }

        public static void fillCombo(ref ComboBox cmbKlienti, string myConnectionString, string selectCommand, string displayMember, string ValueMember)
        {
            try
            {
                cmbKlienti.DataSource = Global.returnTableForGrid(myConnectionString, selectCommand,
                    "SP", "Execute", null,"Text");//list1
                cmbKlienti.DisplayMember = displayMember;// "KLIENTI";
                cmbKlienti.ValueMember = ValueMember;// "CID";

            }
            catch (SqlException ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
                Log.LogData("fillCombo " , ex.Message);
            }
        }
        public static void fillComboGrid(ref DataGridViewComboBoxColumn cmbKlienti, string myConnectionString, string selectCommand, string displayMember, string ValueMember)
        {
            try
            {
                cmbKlienti.DataSource = Global.returnTableForGrid(myConnectionString, selectCommand,
                    "SP", "Execute", null, "Text");//list1
                cmbKlienti.DisplayMember = displayMember;// "KLIENTI";
                cmbKlienti.ValueMember = ValueMember;// "CID";

            }
            catch (SqlException ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
                Log.LogData("fillCombo ", ex.Message);
            }
        }
        public static DataTable fillGrid(ref DataGridView dgRouta,string myConnectionString, string selectCommand, string klienti, string commandType)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(myConnectionString);
                sqlCon.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = sqlCon;
                sqlComm.CommandText = selectCommand;
                sqlComm.CommandTimeout = 3600;
                if (commandType == "Text")
                {
                    sqlComm.CommandType = CommandType.Text;
                }
                else if (commandType == "StoredProcedure")
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                }
                else if (commandType == "TableDirect")
                {
                    sqlComm.CommandType = CommandType.TableDirect;
                }
                //}
                dataAdapter = new SqlDataAdapter(sqlComm);
                // Create a command builder to generate SQL update, insert, and // delete commands based on selectCommand. These are used to // update the database.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                if (table != null)
                {
                    if (bindingSource1 == null) { bindingSource1 = new BindingSource(); }
                    //dgRouta.DataSource = bindingSource1;
                    dataAdapter.Fill(table);
                    bindingSource1.DataSource = table;
                    
                    //dgRouta.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                }
                return table;
                //Global.changeGridRowByCellValue(dgRouta, "VIZITUAR", 4);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
                return null;
            }
        }
        public static DataTable fillGridWithRef(ref DataGridView dgView, string myConnectionString, string selectCommand,
           string furnitor, string kod)
        {
            try
            {
                BindingSource myBindinSource = new BindingSource();
                DataTable myDtTbl = null;
                //dgView.DataSource = bindingSource1;
                dgView.DataSource = myBindinSource;
                myDtTbl = Global.returnTableForGrid(myConnectionString, selectCommand, "Text", "Execute", null, "Text");
                dgView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                if (myDtTbl.Rows.Count > 0)
                {
                    myBindinSource.DataSource = myDtTbl;
                }
                else
                {
                    return null;
                }

                Color grid2BackColor = ColorTranslator.FromHtml("#FFFFFF"); //ColorTranslator.FromHtml("#F8B490"); //Color.White;;
                Color grid2ForeColor = ColorTranslator.FromHtml("#4655A5"); //ColorTranslator.FromHtml("#070B10"); //Color.White;;

                dgView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgView.RowTemplate.Height = 30;
                dgView.ForeColor = grid2ForeColor;
                dgView.BackgroundColor = grid2BackColor; //Color.White;;
                dgView.AlternatingRowsDefaultCellStyle.BackColor = grid2BackColor; //Color.White;;
                dgView.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgView.RowsDefaultCellStyle.BackColor = grid2BackColor; //Color.White;;

                dgView.ColumnHeadersDefaultCellStyle.BackColor = grid2ForeColor;// Color.DodgerBlue;
                dgView.ColumnHeadersDefaultCellStyle.ForeColor = grid2BackColor; //Color.White;
                dgView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgView.EnableHeadersVisualStyles = false;
                dgView.Font = new Font("Century Gothic", 9);
                dgView.RowHeadersVisible = false;
                dgView.ClearSelection();
                dgView.ReadOnly = true;
               
                return myDtTbl;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("fillGridWithRef Error " + ex.Message);
                return null;
            }
        }
        public static DataTable fillCeshtjeNr(ref DataGridView dgView, string myConnectionString, string selectCommand,
           string furnitor, string kod)
        {
            try
            {
                DataTable myDtTbl = null;
                dgView.DataSource = bindingSource1;
                myDtTbl = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                //dgView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                //bindingSource1.DataSource = myDtTbl;

                if (myDtTbl != null && myDtTbl.Rows.Count > 0) { Global.nrCeshtjeSipasFilter = myDtTbl.Rows.Count; }
                else { Global.nrCeshtjeSipasFilter = 0; }

                return myDtTbl;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("fillGrid Error " + ex.Message);
                return null;
            }
        }
        public static bool addButtonToGridWithRef(ref DataGridView dgView, string columnName,Int32 index)
        {
            try
            {
                DataGridViewButtonColumn uninstallButtonColumn = new DataGridViewButtonColumn();
                uninstallButtonColumn.Name = columnName;
                uninstallButtonColumn.Text = columnName;
                uninstallButtonColumn.UseColumnTextForButtonValue = true;
                uninstallButtonColumn.FlatStyle = FlatStyle.System;
                uninstallButtonColumn.DefaultCellStyle.BackColor = Color.AliceBlue;
                uninstallButtonColumn.DefaultCellStyle.ForeColor = Color.Black;
                int columnIndex = index;
                if (dgView.Columns[columnName] == null)
                {
                    dgView.Columns.Insert(columnIndex, uninstallButtonColumn);
                }
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("addButtonToGridWithRef Error " + ex.Message);
                return false;
            }
        }
        public static bool addComboToGridWithRef(ref DataGridView dgView, string columnName, Int32 index,string selectQuery,string displayMember, string valueMember,int width)
        {
            try
            {
                DataGridViewComboBoxColumn myCombo = new DataGridViewComboBoxColumn();
                //Global.fillComboGrid(ref myCombo, Global.localConn,
                //"SELECT [WarehouseID],[WarehouseCode] + '-' + [WarehouseName] as Magazina FROM [warehouses]", "Magazina", "WarehouseID");
                Global.fillComboGrid(ref myCombo, Global.localConn, selectQuery, displayMember, valueMember);
                myCombo.FlatStyle = FlatStyle.System;
                myCombo.DefaultCellStyle.BackColor = Color.AliceBlue;
                myCombo.DefaultCellStyle.ForeColor = Color.Black;
                myCombo.Name = columnName;
                myCombo.Tag = columnName;
                myCombo.Width = width;
                int columnIndex = index;
                
                if (dgView.Columns[columnName] == null)
                {
                    dgView.Columns.Insert(columnIndex, myCombo);
                }
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("addButtonToGridWithRef Error " + ex.Message);
                return false;
            }
        }
        public static bool addStyleToGrid(ref DataGridView dgView, string dgForeColor,  string dgBackgroundColor, string dgAlternatingRowsDefaultCellStyleBackColor, 
            string dgRowsDefaultCellStyleBackColor, string dgRowsDefaultCellStyleForeColor)
        {
            try
            {

                dgView.EditMode = DataGridViewEditMode.EditOnEnter;
                //dgView.ReadOnly = true;
                dgView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgView.RowTemplate.Height = 30;
                dgView.ForeColor = ColorTranslator.FromHtml("#4655A5");
                dgView.BackgroundColor = ColorTranslator.FromHtml("#FFFFFF"); //Color.White;;
                dgView.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFFFFF"); //Color.White;;
                dgView.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgView.RowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFFFFF"); //Color.White;;

                dgView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#4655A5");// Color.DodgerBlue;
                dgView.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#FFFFFF"); //Color.White;
                dgView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgView.EnableHeadersVisualStyles = false;

                dgView.RowHeadersVisible = false;
                dgView.ClearSelection();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("addButtonToGridWithRef Error " + ex.Message);
                return false;
            }
        }

        public static bool IsNumeric(this string text)
        {
            double test;
            return double.TryParse(text, out test);
        }
        public static DataTable returnTableForGrid(string myConString,
           string sqlCmdText, string sqlCmdType, string ExecuteOperation, List<ParameterObject> list1,string commandType)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(myConString);
                sqlCon.Open();
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = sqlCon;
                sqlComm.CommandText = sqlCmdText;
                sqlComm.CommandTimeout = 3600;
                if (commandType == "Text")
                {
                    sqlComm.CommandType = CommandType.Text;
                }
                else if(commandType == "StoredProcedure")
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                }
                else if (commandType == "TableDirect")
                {
                    sqlComm.CommandType = CommandType.TableDirect;
                }

                if (list1 != null)
                {
                    for (int i = 0; i < list1.Count; i++)
                    {
                        sqlComm.Parameters.AddWithValue(list1[i].parameterName, list1[i].parameterValue);
                    }
                }
                //}
                dataAdapter = new SqlDataAdapter(sqlComm);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                if (table != null)
                {
                    dataAdapter.Fill(table);
                    //table.Columns.Add("Delete", typeof(String));
                    return table;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("returnTableForGrid Error " + ex.Message);
                return null;
            }
        }
        public static bool formatoContextMenu(ContextMenuStrip ctMenu, Color backColor, Color foreColor, int height)
        {
            try
            {
                ctMenu.BackColor = backColor;
                for (int i = 0; i < ctMenu.Items.Count; i++)
                {
                    ctMenu.Items[i].AutoSize = false;//ishte false
                    ctMenu.Items[i].BackColor = backColor;
                    ctMenu.Items[i].ForeColor = foreColor;
                    ctMenu.Items[i].Height = height;
                    ctMenu.Items[i].Width = 400;
                    //MessageBox.Show(ctMenu.Items[i].Name);


                    //ctMenu.Items[i].BackColor = backColor;
                    //ctMenu.Items[i].ForeColor = foreColor;
                    ////FileMenu.Text = "File Menu";
                    //ctMenu.Items[i].Font = new Font("Georgia", 16);
                    //ctMenu.Items[i].TextAlign = ContentAlignment.BottomRight;
                    //ctMenu.Items[i].ToolTipText = "Click Me";
                    //Bitmap image = null;
                    //if (ctMenu.Items[i].Name.ToUpper() == "TOOLSTRIPMENUITEM1")//porosi
                    //{
                    //    image = Bitmap.FromFile(@"E:\AgnaApp\WindowsFormsApplication2\WindowsFormsApplication2\bin\Debug\icons\porosi.PNG") as Bitmap;
                    //    Bitmap resized = new Bitmap(image, new Size(80, 80));
                    //    ctMenu.Items[i].Image = resized;
                    //    //ctMenu.Items[i].Text = "Button";
                    //    ctMenu.Items[i].ImageAlign = ContentAlignment.MiddleLeft;
                    //    ctMenu.Items[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                    //    ctMenu.Items[i].TextAlign = ContentAlignment.MiddleLeft;
                    //}
                    if (ctMenu.Items[i].Name.ToUpper() == "TOOLSTRIPMENUITEM1")//porosi
                    {
                        //image = Bitmap.FromFile(@"E:\AgnaApp\WindowsFormsApplication2\WindowsFormsApplication2\bin\Debug\icons\porosi.PNG") as Bitmap;
                        //image = Bitmap.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\icons\porosi.PNG") as Bitmap;
                        //Bitmap resized = new Bitmap(image, new Size(150, 150));
                        //ctMenu.Items[i].Image = resized;

                        //ctMenu.Items[i].BackgroundImage = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"\icons\porosi.PNG");
                        //ctMenu.Items[i].BackgroundImageLayout = ImageLayout.Tile;
                        //ctMenu.Items[i].TextDirection = ToolStripTextDirection.Vertical90;

                        //ctMenu.Items[i].ImageScaling = ToolStripItemImageScaling.SizeToFit; 
                        //ctMenu.Items[i].Text = "Button";
                        //ctMenu.Items[i].ImageAlign = ContentAlignment.MiddleRight;
                        //ctMenu.Items[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                        //ctMenu.Items[i].TextImageRelation = TextImageRelation.TextBeforeImage ;
                        //ctMenu.Items[i].TextAlign = ContentAlignment.MiddleLeft;
                        //ctMenu.ShowImageMargin = true;

                        //ctMenu.Items[i].BackgroundImage = image;// imageImage.FromFile(imagePath);
                        //ctMenu.Items[i].BackgroundImageLayout = ImageLayout.Zoom;
                        //ctMenu.Items[i].Width = 80;
                        //ctMenu.Items[i].Height = 80;

                        //this.toolStripButton1.Image = Bitmap.FromFile("c:\\NewItem.bmp");
                        //this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
                        //this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        //this.toolStripButton1.Name = "toolStripButton1";
                        //this.toolStripButton1.Text = "&New";
                        //this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                    }
                    //if (ctMenu.Items[i].Name.ToUpper() == "TOOLSTRIPMENUITEM1")//porosi
                    //{
                    //    image = Bitmap.FromFile(@"E:\AgnaApp\WindowsFormsApplication2\WindowsFormsApplication2\bin\Debug\icons\porosi.PNG") as Bitmap;
                    //    Bitmap resized = new Bitmap(image, new Size(80, 80));
                    //    ctMenu.Items[i].Image = resized;
                    //    //ctMenu.Items[i].Text = "Button";
                    //    ctMenu.Items[i].ImageAlign = ContentAlignment.MiddleLeft;
                    //    ctMenu.Items[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                    //    ctMenu.Items[i].TextAlign = ContentAlignment.MiddleLeft;
                    //}
                    //if (ctMenu.Items[i].Name.ToUpper() == "TOOLSTRIPMENUITEM1")//porosi
                    //{
                    //    image = Bitmap.FromFile(@"E:\AgnaApp\WindowsFormsApplication2\WindowsFormsApplication2\bin\Debug\icons\porosi.PNG") as Bitmap;
                    //    Bitmap resized = new Bitmap(image, new Size(80, 80));
                    //    ctMenu.Items[i].Image = resized;
                    //    //ctMenu.Items[i].Text = "Button";
                    //    ctMenu.Items[i].ImageAlign = ContentAlignment.MiddleLeft;
                    //    ctMenu.Items[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                    //    ctMenu.Items[i].TextAlign = ContentAlignment.MiddleLeft;
                    //}
                    //if (ctMenu.Items[i].Name.ToUpper() == "TOOLSTRIPMENUITEM1")//porosi
                    //{
                    //    image = Bitmap.FromFile(@"E:\AgnaApp\WindowsFormsApplication2\WindowsFormsApplication2\bin\Debug\icons\porosi.PNG") as Bitmap;
                    //    Bitmap resized = new Bitmap(image, new Size(80, 80));
                    //    ctMenu.Items[i].Image = resized;
                    //    //ctMenu.Items[i].Text = "Button";
                    //    ctMenu.Items[i].ImageAlign = ContentAlignment.MiddleLeft;
                    //    ctMenu.Items[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                    //    ctMenu.Items[i].TextAlign = ContentAlignment.MiddleLeft;
                    //}



                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("formatoContextMenu " + ex.Message);
                return false;
            }
        }

        #region EXPORT TO EXCEL
        /// <summary>
        /// Export DataTable to Excel file
        /// </summary>
        /// <param name="DataTable">Source DataTable</param>
        /// <param name="ExcelFilePath">Path to result file name</param>
        public static void ExportToExcel(this System.Data.DataTable DataTable, string ExcelFilePath = null)
        {
            try
            {
                int ColumnsCount;

                if (DataTable == null || (ColumnsCount = DataTable.Columns.Count) == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                Excel.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;

                object[] Header = new object[ColumnsCount];

                // column headings               
                for (int i = 0; i < ColumnsCount; i++)
                    Header[i] = DataTable.Columns[i].ColumnName;

                Microsoft.Office.Interop.Excel.Range HeaderRange = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, ColumnsCount]));
                HeaderRange.Value = Header;
                HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                HeaderRange.Font.Bold = true;

                // DataCells
                int RowsCount = DataTable.Rows.Count;
                object[,] Cells = new object[RowsCount, ColumnsCount];

                for (int j = 0; j < RowsCount; j++)
                    for (int i = 0; i < ColumnsCount; i++)
                        Cells[j, i] = DataTable.Rows[j][i];

                Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[2, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[RowsCount + 1, ColumnsCount])).Value = Cells;

                // check fielpath
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        Worksheet.SaveAs(ExcelFilePath);
                        Excel.Visible = true;
                        Excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized;
                        //Excel.Quit();
                        //MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    Excel.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
        public static void exportToExcel(string txtKerko, string selectCommand)
        {
            try
            {
                DataTable myDataTbl = Global.returnTableForGrid(Global.localConn, selectCommand, "Text", "Execute", null, "Text");
                Global.currentDirectory = System.IO.Directory.GetCurrentDirectory();
                string folderName = @"c:\FolderFileExcel";
                if (!System.IO.Directory.Exists(folderName))
                {
                    System.IO.Directory.CreateDirectory(folderName);
                }
                Global.currentDirectory = @"c:\FolderFileExcel\";
                Global.ExportToExcel(myDataTbl, Global.currentDirectory + "listeKallezime" + Global.ktheDateOre() + ".xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show("callGridUpdate " + ex.Message);
            }
        }
        public static string ktheDateOre()
        {
            return System.DateTime.Now.Hour.ToString() + "_" + System.DateTime.Now.Minute.ToString() + "_" + System.DateTime.Now.Second.ToString() + "_" +
                   System.DateTime.Now.Day.ToString() + "_" + System.DateTime.Now.Month.ToString() + "_" + System.DateTime.Now.Year.ToString();
        }
        #endregion

    }
}
