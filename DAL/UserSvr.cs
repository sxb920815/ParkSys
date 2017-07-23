using System.Data.Entity.Migrations;
using System.Linq;
using DAL.MB;
using Model;

namespace DAL
{
    public class UserSvr
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Save(User model)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.User.Add(model);
                result = db.SaveChanges();
            }

            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete(long id)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                var brands = db.User.FirstOrDefault(x => x.UserId == id);

                if (brands != null)
                {
                    db.User.Remove(brands);
                    result = db.SaveChanges();
                }
            }
            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int Update(User item)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.User.AddOrUpdate(item);
                result = db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual User GetItem(long id)
        {
            using (var db = new MbContext())
            {
                return db.User
                    .FirstOrDefault(x => x.UserId == id);
            }
        }
    }
}
