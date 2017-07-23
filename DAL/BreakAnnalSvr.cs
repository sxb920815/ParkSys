using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL.MB;

namespace DAL
{
    public class BreakAnnalSvr
    {
        public virtual int Save(BreakRuleAnnal model)
        {
            using (var db=new MbContext())
            {
                db.BreakRuleAnnal.Add(model);
                return db.SaveChanges();
            }
        }

        public virtual List<BreakRuleAnnal> GetListFroIndex()
        {
            using (var db=new MbContext())
            {
                return db.BreakRuleAnnal.OrderByDescending(x => x.CreateTime).Take(10).ToList();
            }
        }


        public virtual Pager<BreakRuleAnnal> GetList(int pagerIndex=1,int pagerSize=20,string key="")
        {
            using (var db=new MbContext())
            {
                
                var breakRuleAnnal = db.BreakRuleAnnal.ToList();
                if (key!="")
                {
                    breakRuleAnnal = breakRuleAnnal.FindAll(x => x.License.Contains(key)).OrderByDescending(m=>m.CreateTime).ToList();
                }
                var pager = new Pager<BreakRuleAnnal>()
                {
                    Index=pagerIndex,
                    Size=pagerSize,
                    Count = breakRuleAnnal.Count,
                    Data = breakRuleAnnal.Skip((pagerIndex-1)*pagerSize).Take(pagerSize)
                };
                return pager;
            }
        }
    }
}
