using System.Data.Entity.Migrations;
using System.Linq;
using DAL.MB;
using Model;

namespace DAL
{
    public class AreaAndEquSvr
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Save(AreaAndEqu model)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.AreaAndEqu.Add(model);
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
                var brands = db.AreaAndEqu.FirstOrDefault(x => x.Id == id);

                if (brands != null)
                {
                    db.AreaAndEqu.Remove(brands);
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
        public virtual int Update(AreaAndEqu item)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.AreaAndEqu.AddOrUpdate(item);
                result = db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual AreaAndEqu GetItem(long id)
        {
            using (var db = new MbContext())
            {
                return db.AreaAndEqu
                    .FirstOrDefault(x => x.Id == id);
            }
        }


        public  AreaAndEqu GetItemByEquId(long id)
        {
            using (var db=new MbContext())
            {
                return db.AreaAndEqu.FirstOrDefault(x=>x.EquipmentId==id);
            }
        }
    }
}
