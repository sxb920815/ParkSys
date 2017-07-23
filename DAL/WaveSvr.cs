using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DAL.MB;
using Model;

namespace DAL
{
    public class WaveSvr
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Save(Wave model)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.Wave.Add(model);
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
                var brands = db.Wave.FirstOrDefault(x => x.WaveId == id);

                if (brands != null)
                {
                    db.Wave.Remove(brands);
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
        public virtual int Update(Wave item)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.Wave.AddOrUpdate(item);
                result = db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Wave GetItem(long id)
        {
            using (var db = new MbContext())
            {
                return db.Wave
                    .FirstOrDefault(x => x.WaveId == id);
            }
        }

        public virtual List<Wave> GetList()
        {
            using (var db=new MbContext())
            {
                return db.Wave.ToList();
            }
        } 
    }
}
