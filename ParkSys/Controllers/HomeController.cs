using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.MB;
using DAL;
using System.Threading.Tasks;
using BLL;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace Repair.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
           // State();
            var svr = new CardAnnalSvr();
            return View(svr.GetListForIndex(0));

        }

        public ActionResult BreakRule(string key = "", DateTime? beginTime = null, DateTime? endTime = null)
        {
            var svr = new CardAnnalSvr();
            return View(svr.GetList(Convert.ToInt32(Request["pageIndex"]), 20, 1, key,beginTime,endTime));
        }

        public ActionResult Annal(string key = "",DateTime? beginTime=null,DateTime? endTime=null)
        {
            var svr = new CardAnnalSvr();
            return View(svr.GetList(Convert.ToInt32(Request["pageIndex"]), 20, 0, key,beginTime,endTime));
        }

        public ActionResult AreaPark()
        {
            using (var db = new MbContext())
            {
                return View(db.Area.ToList());
            }
        }


        public ActionResult WaveAnnal(DateTime? beginTime=null,DateTime? endTime=null)
        {
            var pageIndex = 0;
            if (Request["pageIndex"] != null)
            {
                pageIndex = Convert.ToInt32(Request["pageIndex"]);
            }
            
            return View(WaveAnnanSvr.Instance.GetPager(pageIndex, 20,beginTime,endTime));

        }


        public void State()
        {
            Task t = new Task(() =>
            {
                while (true)
                {
                    var annalBll = new AnnalBll();


                    var inputStream = Request.InputStream;
                    var strLen = Convert.ToInt32(inputStream.Length);
                    var strArr = new byte[strLen];
                    inputStream.Read(strArr, 0, strLen);
                    var requestMes = Encoding.UTF8.GetString(strArr);
                    var license = "";
                    var serialno = "";
                    var imagePath = "";
                    var key = "";
                    var value = "";

                    if (requestMes != "")
                    {
                        JsonReader reader = new JsonTextReader(new StringReader(requestMes));
                        while (reader.Read())
                        {
                            if (reader.TokenType.ToString() == "PropertyName")
                            {
                                key = reader.Value.ToString();
                            }
                            if (reader.TokenType.ToString() == "String")
                            {
                                value = reader.Value.ToString();
                            }
                            if (key != "" && value != "")
                            {
                                switch (key)
                                {
                                    case "license":
                                        license = value;
                                        break;
                                    case "serialno":
                                        serialno = value;
                                        break;
                                    case "imagePath":
                                        imagePath = value;
                                        break;
                                    default:
                                        break;
                                }
                                key = "";
                                value = "";
                            }
                        }
                    }
                    // 根据设备序列号得到相关信息
                    var equipemnt = annalBll.GetEquipment(serialno);
                    var areaAndEqu = annalBll.GetAreaAndEqu(equipemnt.EquipmentId);

                    //todo 保存图片到本地
                    imagePath = annalBll.SaveImages(imagePath, license, equipemnt.IP);
                    // 写入记录
                    annalBll.CreateAnnal(license, imagePath, equipemnt, areaAndEqu);
                    //todo 更改剩余车位
                    annalBll.UpdateParkNumber(areaAndEqu);
                    Thread.Sleep(500);
                }
            });
            t.Start();


        }
    }
}
