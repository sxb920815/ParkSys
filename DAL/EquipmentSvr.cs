using System.Data.Entity.Migrations;
using System.Linq;
using DAL.MB;
using Model;
using System.Collections.Generic;

namespace DAL
{
    public class EquipmentSvr : SvrBase<EquipmentSvr>
    {

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Save(Equipment model)
        {
            var result = -1;
            using (var db = new MbContext())
            {

                db.Equipment.Add(model);
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
                var brands = db.Equipment.FirstOrDefault(x => x.EquipmentId == id);

                if (brands != null)
                {
                    db.Equipment.Remove(brands);
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
        public virtual int Update(Equipment item)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.Equipment.AddOrUpdate(item);
                result = db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Equipment GetItem(long id)
        {
            using (var db = new MbContext())
            {
                return db.Equipment
                    .FirstOrDefault(x => x.EquipmentId == id);
            }
        }

        public  Equipment GetItemBySerialno(string serislno)
        {
            using (var db=new MbContext())
            {
                return db.Equipment.FirstOrDefault(x=>x.Serialno==serislno);
            }
        }


        public virtual List<Equipment> GetList()
        {
            using (var db=new MbContext())
            {
                return db.Equipment.ToList();
            }
        }
    }
}
