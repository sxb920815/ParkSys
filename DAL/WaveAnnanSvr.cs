using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.WebControls;
using DAL.MB;
using Model;

namespace DAL
{
    public class WaveAnnanSvr:SvrBase<WaveAnnanSvr>
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Save(WaveAnnal model)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.WaveAnnal.Add(model);
                result = db.SaveChanges();
            }

            return result;
        }

        public virtual int Save(string waveCardId)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                var model = new WaveAnnal()
                {
                    WaveCardId = waveCardId,
                    CreateTime = DateTime.Now
                };
                db.WaveAnnal.Add(model);
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
                var brands = db.WaveAnnal.FirstOrDefault(x => x.WaveAnnalId == id);

                if (brands != null)
                {
                    db.WaveAnnal.Remove(brands);
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
        public virtual int Update(WaveAnnal item)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.WaveAnnal.AddOrUpdate(item);
                result = db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual WaveAnnal GetItem(long id)
        {
            using (var db = new MbContext())
            {
                return db.WaveAnnal
                    .FirstOrDefault(x => x.WaveAnnalId == id);
            }
        }

        public virtual WaveAnnal GetForID(string id)
        {
            using (var db=new MbContext())
            {
                return db.WaveAnnal.Where(x=>x.WaveCardId==id).OrderByDescending(x=>x.CreateTime).First();
            }
        }

        public virtual WaveAnnal GetFirst()
        {
            using (var db = new MbContext())
            {
                return db.WaveAnnal.OrderByDescending(x => x.CreateTime).First();
            }
        }


        public virtual Pager<WaveAnnal> GetPager(int pageIndex = 1, int pageSize = 20, DateTime? beginTime = null, DateTime? endTime = null)
        {
            using (var db = new MbContext())
            {
                var pager = new Pager<WaveAnnal>()
                {
                    Index = pageIndex,
                    Size = pageSize
                };
                var model = db.WaveAnnal.OrderByDescending(x => x.CreateTime).ToList();
                if (beginTime != null)
                {
                    model = model.FindAll(x => x.CreateTime >= beginTime).ToList();
                }
                if (endTime != null)
                {
                    model = model.FindAll(x => x.CreateTime <= endTime).ToList();
                }
                
                pager.Count = model.Count;
                pager.Data = model.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return pager;
            }
        }
    }
}
