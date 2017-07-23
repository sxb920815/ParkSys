using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using log4net;
using log4net.Core;
using Microsoft.Ajax.Utilities;
using Model;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using BLL;
using Newtonsoft.Json;
using Sznfc.vicRFID;
using FTP;


namespace ParkSys
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {


        



        protected void Application_Start()
        {

            

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GetFtp();

        }


        private void GetFtp()
        {
            Task t = new Task(() => {
                FTP.Provider p = new FTP.Provider("D:\\TCYD\\CarImages");
                p.OnCallback += p_OnCallback;
                p.Start();

                Thread.Sleep(500);
            
            });

            t.Start();
        }
        static void p_OnCallback(FTP.PlateInfo obj)
        {

        }



        #region 接收微波数据
        bool readCardRun = true;
        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码
            readCardRun = false;
        }
        private Sznfc.vicRFID.ActiveReader myReader = new Sznfc.vicRFID.ActiveReader();
        private void ReadCard(int timeSpan)
        {
            OpenPort();


            Task t = new Task(() =>
            {
                while (true)
                {
                    var values = new List<Sznfc.vicRFID.ActiveReader.TagIds>();
                    uint count = 500;
                    Byte dataFlag = 0;
                    int status = 0;
                    status = myReader.GetTagIds(ref dataFlag, ref count, ref values);
                    if (status == 0)
                    {
                        foreach (ActiveReader.TagIds tempTagId in values)
                        {

                            int tempStr = tempTagId.Id[5] * 65536 + tempTagId.Id[6] * 256 + tempTagId.Id[7];
                            var result = new AnnalBll().CreateWaveAnnal(tempStr.ToString());
                        }
                    }
                    else
                    {
                        OpenPort();
                    }

                    Thread.Sleep(timeSpan);
                }
            });
            t.Start();
        }
        private void OpenPort()
        {
            var wave = new WaveSvr().GetList();
            foreach (var item in wave)
            {
                int result;
                Boolean opened = false;
                byte linkType = 2;
                var linkString = item.IP;
                string version = "";
                myReader.CloseReader();
                result = myReader.OpenReader(linkType, linkString);
                if (result == 0)
                {
                    result = myReader.SetBaudRate(0);
                    if (result == 0)
                    {
                        opened = true;
                    }
                }

                if (opened)
                {
                    result = myReader.GetFirmwareVersion(ref version);

                    result = myReader.GetSdkVersion(ref version);


                }
            }
        }
        #endregion

       

    }
}