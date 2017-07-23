using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.MB;
using Model;
using DAL;
using Repair.Web.Controllers;

namespace ParkSys.Controllers
{
    public class SettingController : BaseController
    {
        //
        // GET: /Setting/
        #region 区域设备绑定
        public ActionResult AreaEquipment()
        {
            using (var db = new MbContext())
            {

                return View(db.AreaAndEqu.ToList());
            }

        }
        public ActionResult AreaEquipmentEdit(long id = 0)
        {
            using (var db = new MbContext())
            {

                return View(db.AreaAndEqu.FirstOrDefault(x => x.Id == id));

            }

        }
        [HttpPost]
        public ActionResult AreaEquipmentEdit(AreaAndEqu model)
        {
            using (var db = new MbContext())
            {
                var svr = new AreaAndEquSvr();
                if (model.Id > 0)
                {
                    svr.Update(model);
                }
                else
                {
                    svr.Save(model);
                }
                return RedirectToAction("AreaEquipment");
            }

        }
        public ActionResult AreaEquipmentDelete(long id)
        {
            AreaAndEquSvr svr = new AreaAndEquSvr();
            svr.Delete(id);
            return RedirectToAction("AreaEquipment");


        }
        #endregion






        #region 区域管理
        public ActionResult AreaSys()
        {
            using (var db = new MbContext())
            {
                return View(db.Area.ToList());
            }
        }
        public ActionResult AreaEdit(long id = 0)
        {
            using (var db = new MbContext())
            {
                if (id > 0)
                {
                    return View(db.Area.FirstOrDefault(x => x.AreaId == id));
                }
                return View();
            }

        }
        [HttpPost]
        public ActionResult AreaEdit(Area model)
        {
            AreaSvr svr = new AreaSvr();
            if (model.AreaId > 0)
            {
                svr.Update(model);
            }
            else
            {
                svr.Save(model);
            }
            return RedirectToAction("AreaSys");
        }

        public ActionResult AreaDelete(long id)
        {
            AreaSvr svr = new AreaSvr();
            svr.Delete(id);
            return RedirectToAction("AreaSys");


        }
        #endregion



        #region 车辆管理
        public ActionResult CarSys(string key="")
        {
            var svr = new CarSvr();
            return View(svr.GetList(Convert.ToInt32(Request["pagerIndex"]), 20, key));
        }
        public ActionResult CarEdit(long id = 0)
        {
            using (var db = new MbContext())
            {

                return View(db.Car.FirstOrDefault(x => x.CarId == id));

            }

        }
        [HttpPost]
        public ActionResult CarEdit(Car model)
        {
            CarSvr svr = new CarSvr();
            if (model.CarId > 0)
            {
                svr.Update(model);
            }
            else
            {
                svr.Save(model);
            }

            return RedirectToAction("CarSys");

        }
        public ActionResult CarDelete(long id)
        {
            CarSvr svr = new CarSvr();
            svr.Delete(id);
            return RedirectToAction("CarSys");

        }

        #endregion





        #region 设备管理
        public ActionResult EquipmentSys()
        {
            using (var db = new MbContext())
            {
                return View(db.Equipment.ToList());
            }
        }
        public ActionResult EquipmentEdit(long id = 0)
        {
            using (var db = new MbContext())
            {

                return View(db.Equipment.FirstOrDefault(x => x.EquipmentId == id));

            }

        }
        [HttpPost]
        public ActionResult EquipmentEdit(Equipment model)
        {
            EquipmentSvr svr = new EquipmentSvr();
            if (model.EquipmentId > 0)
            {
                svr.Update(model);
            }
            else
            {
                svr.Save(model);
            }

            return RedirectToAction("EquipmentSys");

        }
        public ActionResult EquipmentDelete(long id)
        {
            EquipmentSvr svr = new EquipmentSvr();
            svr.Delete(id);
            return RedirectToAction("EquipmentSys");

        }
        #endregion






        
    }
}
