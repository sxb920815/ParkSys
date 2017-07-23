using System.Data.Entity.Migrations;
using System.Linq;
using DAL.MB;
using Model;
using System.Collections.Generic;
using System;

namespace DAL
{
    public class CardAnnalSvr
    {

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual int Save(CardAnnal model)
        {
            var result = -1;
            using (var db=new MbContext())
            {

                db.CardAnnal.Add(model);
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
                var brands = db.CardAnnal.FirstOrDefault(x => x.CardAnnalId == id);

                if (brands != null)
                {
                    db.CardAnnal.Remove(brands);
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
        public virtual int Update(CardAnnal item)
        {
            var result = -1;

            using (var db = new MbContext())
            {
                db.CardAnnal.AddOrUpdate(item);
                result = db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual CardAnnal GetItem(long id)
        {
            using (var db = new MbContext())
            {
                return db.CardAnnal
                    .FirstOrDefault(x => x.CardAnnalId == id);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual CardAnnal GetItem(string license)
        {
            using (var db = new MbContext())
            {
                return db.CardAnnal
                    .OrderByDescending(x=>x.CreateTime)
                    .FirstOrDefault(x => x.License == license);
            }
        }


        public virtual List<CardAnnal> GetListForIndex(int isBreak)
        {
            using (var db=new MbContext())
            {
                return db.CardAnnal.ToList().FindAll(x=>x.IsBreak==isBreak).OrderByDescending(x=>x.CreateTime).Take(10).ToList();
            }
        }

        public virtual List<CardAnnal> GetistAll()
        {
            using (var db=new MbContext())
            {
                return db.CardAnnal.OrderByDescending(x => x.CreateTime).Take(6).ToList();
            }
        }


        public virtual Pager<CardAnnal> GetList(int pagerIndex = 1, int pagerSize = 20,int isBreak=0, string key = "",DateTime? beginTime=null,DateTime? endTime=null)
        {
            using (var db = new MbContext())
            {

                var breakRuleAnnal = db.CardAnnal.ToList().FindAll(x=>x.IsBreak==isBreak);
                if (key != "")
                {
                    breakRuleAnnal = breakRuleAnnal.FindAll(x => x.License.Contains(key)).OrderByDescending(m => m.CreateTime).ToList();
                }
                if (beginTime!=null)
                {
                    breakRuleAnnal = breakRuleAnnal.FindAll(x => x.CreateTime >= beginTime).ToList();
                }
                if (endTime!=null)
                {
                    breakRuleAnnal = breakRuleAnnal.FindAll(x => x.CreateTime <= endTime).ToList();
                }
                var pager = new Pager<CardAnnal>()
                {
                    Index = pagerIndex,
                    Size = pagerSize,
                    Count = breakRuleAnnal.Count,
                    Data = breakRuleAnnal.Skip((pagerIndex - 1) * pagerSize).Take(pagerSize)
                };
                return pager;
            }
        }


        public virtual CardAnnal GetFirst()
        {
            using (var db=new MbContext())
            {
                return db.CardAnnal.OrderByDescending(x => x.CreateTime).First();
            }
        }

        public virtual int AddAnnal(string license,string imagePath)
        {
            var result = -1;
            using (var db=new MbContext())
            {
                var model = new CardAnnal()
                {
                    AreaId=1,
                    CreateTime=DateTime.Now,
                    IsBreak=0,
                    License=license,
                    ImagePath=imagePath
                };
            }
            return result;

        }


        public void FTPAnnal(string fullName, string p,string sbbh)
        {
            var paths = fullName.Split('\\');
            var path = "../../" + paths[2] + "/" + paths[3] + "/" + paths[4] + "/" + paths[5];
            
            var cardAnnalSvr = new CardAnnalSvr();

            var waveAnnalSvr = new WaveAnnanSvr();
            var waveAnnal = waveAnnalSvr.GetFirst();
            TimeSpan ts = DateTime.Now - waveAnnal.CreateTime;

            var car = new CarSvr().GetItem(p);
            var equId = 2L;
            var state = "进";
            var equ=new EquipmentSvr().GetItemBySerialno(sbbh);
            if (equ!=null)
            {
                equId = equ.EquipmentId;
                var aae=new AreaAndEquSvr().GetItemByEquId(equId);
                if (aae != null)
                    state = aae.State;
            }
            var cardAnnal = new CardAnnal
            {
                CreateTime = DateTime.Now,
                AreaId = 1,
                EquipmentId = equId,
                License = p,
                ImagePath = path,
                State = state
            };
            if (car != null || ts.TotalSeconds <= 10)
            {

                cardAnnal.IsBreak = 0;

            }
            else
            {
                cardAnnal.IsBreak = 1;
            }


            cardAnnalSvr.Save(cardAnnal);



        }
    }
}
