using DAL;
using DAL.MB;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using System.Diagnostics.Eventing.Reader;
using Repair.Web.Controllers;

namespace ParkSys.Controllers
{
    public class RolesController : BaseController
    {
        //
        // GET: /Roles/

        public ActionResult Roles(long id = 0)
        {
            using (var db = new MbContext())
            {
                return View(db.User.FirstOrDefault(x => x.UserId == id));
            }
        }

        [HttpPost]
        public ActionResult Edit(long userId)
        {
            var list = Request["RolePermission"].Split(',');
            long permission = 0;
            foreach (var item in list)
            {
                permission += long.Parse(item);
            }
            using (var db=new MbContext())
            {
                var model = db.User.FirstOrDefault(x=>x.UserId==userId);
                model.Permission = permission;
                db.User.AddOrUpdate(model);
                if (db.SaveChanges() > 0)
                {
                    return Content("<script>alert('操作成功');window.location.href='/Roles/UserSys'</script>");
                }
                return Content("<script>alert('操作失败');window.location.href='/Roles/UserSys'</script>");
            }
            
        }

        #region 用户管理
        public ActionResult UserSys()
        {
            using (var db = new MbContext())
            {
                return View(db.User.ToList());
            }
        }
        public ActionResult UserEdit(long id = 0)
        {
            using (var db = new MbContext())
            {

                return View(db.User.FirstOrDefault(x => x.UserId == id));

            }

        }
        [HttpPost]
        public ActionResult UserEdit(User model)
        {
            using (var db = new MbContext())
            {
                var svr = new UserSvr();
                if (model.UserId > 0)
                {
                    var user = db.User.FirstOrDefault(x => x.UserId == model.UserId);
                    model.PassWord = user.PassWord;
                    model.Permission = user.Permission;
                    user.CreateTime = user.CreateTime;
                    svr.Update(model);
                }
                else
                {
                    model.Permission = 0;
                    model.PassWord = "12345678";
                    model.CreateTime = DateTime.Now;
                    svr.Save(model);
                }
                return RedirectToAction("UserSys");
            }

        }
        public ActionResult UserDelete(long id)
        {
            UserSvr svr = new UserSvr();
            svr.Delete(id);
            return RedirectToAction("UserSys");

        }
        #endregion

    }
}
