using System.Data.Entity.Migrations;
using Model;
using System.Data;
using System;

namespace DAL.MB
{
    public class MbContextInit : DbMigrationsConfiguration<MbContext>
    {
        public MbContextInit()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected virtual void Seed(MbContext context)
        {

            V20160519(context);


            base.Seed(context);
        }

        private void V20160519(MbContext context)
        {
            context.User.AddOrUpdate(new User { 
                UserId=1,
                UserName="超级管理员",
                LoginName="admin",
                PassWord="admin",
                Permission=510,
                CreateTime=DateTime.Now
            });


            context.Equipment.AddOrUpdate(new Equipment { 
            EquipmentId=1,
            EquipmentName="测试，可删除",
            IP="192.168.1.1",
            Serialno="aaaaaa"
            
            });


            context.Area.AddOrUpdate(new Area { 
            AreaId=1,
            AreaName="测试",
            ParkingNumber=100,
            RestParkingNumber=60,
            CreateTime=DateTime.Now
            });


            context.Car.AddOrUpdate(new Car { 
            CarId=1,
            CreateTime=DateTime.Now,
            OwnerName="测试",
            OwnerPhone="1111111",
            License="浙A12345",
            AreaId=1
            });


            context.AreaAndEqu.AddOrUpdate(new AreaAndEqu { 
            Id=1,
            AreaId=1,
            EquipmentId=1,
            State="出"
            });

            context.SaveChanges();
        }
    }
}
