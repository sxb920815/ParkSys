using System.Data.Entity.Migrations;
using System.Linq;
using DAL.MB;
using Model;
using System.Collections.Generic;

namespace DAL
{
    public class AreaSvr:SvrBase<AreaSvr>
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Save(Area model)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.Area.Add(model);
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
                var brands = db.Area.FirstOrDefault(x => x.AreaId == id);

                if (brands != null)
                {
                    db.Area.Remove(brands);
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
        public virtual int Update(Area item)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.Area.AddOrUpdate(item);
                result = db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Area GetItem(long? id)
        {
            using (var db = new MbContext())
            {
                return db.Area
                    .FirstOrDefault(x => x.AreaId == id);
            }
        }

        public virtual List<Area> GetList()
        {
            using (var db=new MbContext())
            {
                return db.Area.ToList();
            }
        }


    }
}
