using System.Linq;
using DAL;
using DAL.MB;
using Model;

namespace BLL
{

    /// <summary>
    /// 设置
    /// </summary>
    public  class SettingBll
    {
        public virtual int EquipmentAdd(string equipmentName,string ip,string serialno)
        {
            var equ = new Equipment()
            {
                EquipmentName = equipmentName,
                IP = ip,
                Serialno = serialno
            };
            var result = new EquipmentSvr().Save(equ);
            return result;

        }

        
        public virtual Pager<Equipment> GetEquipment(string equipmentName=null,int index=1,int pageSize=20)
        {
            Pager<Equipment> pager=new Pager<Equipment>();
            pager.Index = index;
            pager.Size = pageSize;
            using (var db=new MbContext())
            {
                pager.Data = db.Equipment.ToList();
                if (equipmentName != null)
                {
                    pager.Data = db.Equipment.ToList().FindAll(x => x.EquipmentName == equipmentName);
                }
            }
            
            return pager;
        }
    }
}
