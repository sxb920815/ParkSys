using DAL;
using DAL.MB;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace Repair.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            var userService = new UserAuthorSvr();
            if (userService.UserLogin(username, password))
            {
                return Redirect("/Home/Index");
            }
            ViewBag.UserName = username;
            ViewBag.ErrorMsg = "用户名或密码有误，请重新输入。";
            return View();
        }


        public ActionResult ChangePassword(long id)
        {
            using (var db = new MbContext())
            {
                return View(db.User.FirstOrDefault(x => x.UserId == id));
            }
        }


        [HttpPost]
        public ActionResult ChangePassword(User model)
        {
            using (var db = new MbContext())
            {
                var oldPassword = Request["OldPassword"];
                var newPassword = Request["password"];
                var user = db.User.FirstOrDefault(x => x.UserId == model.UserId);
                if (user.PassWord!=oldPassword)
                {
                    return Content("<script>alert('原密码错误');window.location.href=''</script>");
                }
                user.PassWord = newPassword;
                db.User.AddOrUpdate(user);
                if (db.SaveChanges() > 0)
                {
                    return Content("<script>alert('修改成功');window.location.href='/Login/Index'</script>");
                }
                return Content("<script>alert('新密码与原密码不能相同');window.location.href=''</script>");
            }
        }

    }
}
