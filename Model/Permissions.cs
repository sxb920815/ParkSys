using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Permission
    {
        public long Value { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public bool NewPage { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public List<Permission> Chlids { get; set; }
        public void Add(Permission chlid)
        {
            if (Chlids == null)
            {
                Chlids = new List<Permission>();
            }
            Chlids.Add(chlid);
        }
        public void Add(string name, string url, long value, string description = null, bool isNewPage = false)
        {
            Add(new Permission(name, url, value, description, isNewPage));
        }
        public void Insert(int index, string name, string url, long value)
        {
            Chlids.Insert(index, new Permission(name, url, value, null, false));
        }
        public Permission(string name, string url, long value)
        {
            this.Name = name;
            this.URL = url;
            this.Value = value;
        }
        public Permission(string name, string url, long value, string description, bool isNewPage)
        {
            this.Name = name;
            this.URL = url;
            this.Value = value;
            this.NewPage = isNewPage;
            this.Description = description;
        }
        public Permission(string name)
        {
            this.Name = name;
        }
        private static long flag = 1;
        private static long GetFlag()
        {
            flag = flag << 1;
            return flag;
        }
        public static List<Permission> GetPermissionsByFlag(long flag)
        {

            var myPermissons = new List<Permission>();
           
                foreach (var item in Permissons)
                {
                    var newItem = new Permission(item.Name);
                    newItem.Chlids = item.Chlids.Where(m => (m.Value & flag) > 0).ToList();
                    if (newItem.Chlids.Count > 0)
                    {
                        
                            myPermissons.Add(newItem);
                        
                    }
                }
            

            return myPermissons;
        }


        static Permission()
        {
            Permissons = new List<Permission>();

            var Role = new Permission("权限管理");
            Role.Add("用户管理", "/Roles/UserSys",GetFlag());
            Permissons.Add(Role);

            var Home = new Permission("基础信息");
            Home.Add("首页", "/Home/Index", GetFlag());
            Home.Add("违章车辆", "/Home/BreakRule", GetFlag());
            Home.Add("出入记录", "/Home/Annal", GetFlag());
            Home.Add("微波记录","/Home/WaveAnnal",GetFlag());
            Permissons.Add(Home);

            var Setting = new Permission("系统管理");
            Setting.Add("摄像机管理", "/Setting/EquipmentSys", GetFlag());
            Setting.Add("区域管理", "/Setting/AreaSys", GetFlag());
            Setting.Add("摄像机区域绑定", "/Setting/AreaEquipment", GetFlag());
            Setting.Add("车辆管理", "/Setting/CarSys", GetFlag());
            Permissons.Add(Setting);

            //var 

           
        }
        public static List<Permission> Permissons { get; set; }
    }
}
