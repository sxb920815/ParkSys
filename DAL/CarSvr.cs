using System.Data.Entity.Migrations;
using System.Linq;
using DAL.MB;
using Model;

namespace DAL
{
    public class CarSvr
    { /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Save(Car model)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.Car.Add(model);
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
                var brands = db.Car.FirstOrDefault(x => x.CarId == id);

                if (brands != null)
                {
                    db.Car.Remove(brands);
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
        public virtual int Update(Car item)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.Car.AddOrUpdate(item);
                result = db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Car GetItem(string license)
        {
            using (var db = new MbContext())
            {
                return db.Car
                    .FirstOrDefault(x => x.License == license);
            }
        }



        public  Car GetItemByLicense(string license)
        {
            using (var db=new MbContext())
            {
                return db.Car.FirstOrDefault(x=>x.License==license);
            }
        }

        public virtual Pager<Car> GetList(int pagerIndex = 1, int pagerSize = 20, string key = "")
        {
            using (var db = new MbContext())
            {

                var breakRuleAnnal = db.Car.ToList();
                if (key != "")
                {
                    breakRuleAnnal = breakRuleAnnal.FindAll(x => x.License.Contains(key) ||x.OwnerName.Contains(key)).OrderByDescending(m => m.CreateTime).ToList();
                }
                var pager = new Pager<Car>()
                {
                    Index = pagerIndex,
                    Size = pagerSize,
                    Count = breakRuleAnnal.Count,
                    Data = breakRuleAnnal.Skip((pagerIndex - 1) * pagerSize).Take(pagerSize)
                };
                return pager;
            }
        }
    }
}
