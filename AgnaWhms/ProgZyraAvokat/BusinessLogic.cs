using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgZyraAvokat
{
    public class BusinessLogic
    {
        //public bool dergoFaturaMagazina(string status, int bidFatura, string gpsLog)
        //{
        //    string faturePerNeServer = "";
        //    string serviceResponse = "";
        //    bool rregjistroLokalisht = false;
        //    try
        //    {
        //        ///RregjistroDheKtheFature metoda e dll   
        //        faturePerNeServer = returnStrFature(bidFatura, gpsLog); //krijo fature string per ne service by Bid 
        //        Global.fatureGpsString = "";// returnStrGps(bidFatura, gpsLog);
        //        Global.bidGlobale = bidFatura;
        //        Global.bidGlobalePerpara = bidFatura;
        //        //openReportForm("Ne Service dergohet :" + "\r\n" + "Status:" + status + "\r\n" + "Global.sqlBidGlobale:" + Global.sqlBidGlobale.ToString() + "\r\n" + "Fature per ne server:" + "\r\n" + faturePerNeServer + "\r\n", "Info");
        //        //Thirr service dhe dergo fature, merr pergjigje, Pergjigja ne format (true,response) ose (false,errorMessage)
        //        serviceResponse = dergoMerrService(faturePerNeServer, status);//
        //        if (serviceResponse == "111")
        //        {
        //            //MessageBox.Show("Kjo fature ka nje produkt qe eshte derguar njehere,NUK MUND TA DERGONI PERSERI", "KUJDES");
        //            return false;
        //        }

        //        //openReportForm(serviceResponse, "Info");
        //        if (!Global.dergoAllFatura) //Per rastin e dergimit ne fund te dites nuk duhet te kthehen faturat ne Ppc
        //        {
        //            char[] delimiterChars = { '&', ',' };
        //            if (!correctPergjigjeNgaPc(serviceResponse)) //teston faturen e kthyer nga service 
        //            {
        //                return false;
        //            }
        //            else if (correctPergjigjeNgaPc(serviceResponse))
        //            {
        //                rregjistroLokalisht = rregjistroLokalishtFull(serviceResponse, bidFatura, gpsLog); //rregjistron pergjigje lokalisht
        //            }
        //            else
        //            {
        //                //MessageBox.Show("Probleme me AMC,provoje perseri!", "Kujdes!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            //rasti kur dergohen faturat ne fund te dites dhe s duam te marrim pergjigje
        //            //openReportForm(serviceResponse, "122");
        //            char[] delimiterChars = { '&', ',' };
        //            if (!correctPergjigjeNgaPc(serviceResponse))
        //            {
        //                //openReportForm("Pa Konfirmuar per wireless", "sukses");
        //               // MessageBox.Show("Fatura e kthyer jo e sakte ", "Dergo Fatura Magazina");
        //                return false;
        //            }
        //            else if (correctPergjigjeNgaPc(serviceResponse))
        //            {
        //                openReportForm("Konfirmuar per wireless", "sukses");

        //                konfirmoFature(Convert.ToDouble(serviceResponse.Split(delimiterChars[1])[1]));
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("Gabim dergoFaturaMagazina " + ex.Message, "Dergo Fatura Magazina");
        //        return false;
        //    }
        //    return true;
        //}
    }
}
