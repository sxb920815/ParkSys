using System.Data.Entity.Migrations;
using System.Linq;
using DAL.MB;
using Model;
using System.Collections.Generic;
using System;

namespace DAL
{
    public class FreshenSvr : SvrBase<FreshenSvr>
    {
        public virtual int Save(Freshen model)
        {
            var result = -1;
            using (var db=new MB.MbContext())
            {
                db.Freshen.Add(model);
                result = db.SaveChanges();
            }
            return result;
        }

        public virtual int Update(Freshen model)
        {
            var result = -1;
            using (var db=new MB.MbContext())
            {
                db.Freshen.AddOrUpdate(model);
                result = db.SaveChanges();
            }
            return result;
        }

        public virtual int Delete(long id)
        {
            var result = -1;
            using (var db=new MbContext())
            {
                var model = db.Freshen.FirstOrDefault(x=>x.FreshenId==id);
                if (model!=null)
                {
                    db.Freshen.Remove(model);
                    result = db.SaveChanges();
                }
            }
            return result;
        }

        public virtual bool GetTime(DateTime TimeNow)
        {
            using (var db=new MbContext())
            {
               var model =db.Freshen.Where(x => x.beginTime >= TimeNow && x.endTime <= TimeNow).ToList();
               if (model!=null)
               {
                   return true;
               }
               return false;
            }
        }

    }
}
