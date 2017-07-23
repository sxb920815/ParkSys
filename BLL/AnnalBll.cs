using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.IO;

namespace BLL
{
    public class AnnalBll
    {

        //写入车辆出入记录 
        public virtual bool CreateAnnal(string license, string imagePath, Equipment equipemnt, AreaAndEqu areaAndEqu)
        {
            var cardAnnalSvr = new CardAnnalSvr();
            var waveAnnalSvr = new WaveAnnanSvr();

            var waveAnnal = waveAnnalSvr.GetFirst();
            TimeSpan ts = DateTime.Now - waveAnnal.CreateTime;

            var car = new CarSvr().GetItem(license);
            var annal = cardAnnalSvr.GetItem(license);
            if ((DateTime.Now-annal.CreateTime).TotalSeconds<10)
            {
                return false;
            }
            var cardAnnal = new CardAnnal
            {
                CreateTime = DateTime.Now,
                AreaId = areaAndEqu.AreaId,
                EquipmentId = equipemnt.EquipmentId,
                License = license,
                ImagePath = imagePath,
                State = areaAndEqu.State
            };
            if (car != null || ts.TotalSeconds <= 10)
            {

                cardAnnal.IsBreak = 0;

            }
            else
            {
                cardAnnal.IsBreak = 1;
            }


            return cardAnnalSvr.Save(cardAnnal) > 0;

        }

        //写入微波记录
        public virtual bool CreateWaveAnnal(string waveCardId)
        {
            int timeSpan1 = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Timespan"]);


            var annal = new CardAnnalSvr().GetFirst();
            var ts = DateTime.Now - annal.CreateTime;
            if (ts.TotalSeconds <= timeSpan1)
            {
                annal.IsBreak = 0;
                new CardAnnalSvr().Update(annal);

            }


            var waveAnnal = WaveAnnanSvr.Instance.GetForID(waveCardId);
            if ((DateTime.Now - waveAnnal.CreateTime).TotalSeconds <= timeSpan1)
            {
                return false;
            }

            var result = new WaveAnnanSvr().Save(waveCardId);
                   
            return true;

        }

        //获取设备信息
        public virtual Equipment GetEquipment(string serialno)
        {
            var equSvr = new EquipmentSvr();
            return equSvr.GetItemBySerialno(serialno);
        }
        //获取区域设备绑定信息
        public virtual AreaAndEqu GetAreaAndEqu(long id)
        {
            var areaAndEquSvr = new AreaAndEquSvr();
            return areaAndEquSvr.GetItemByEquId(id);
        }

        //更改区域车位
        public virtual bool UpdateParkNumber(AreaAndEqu areaAndEqu)
        {
            var areaSvr = new AreaSvr();
            var area = areaSvr.GetItem(areaAndEqu.AreaId);
            if (areaAndEqu.State == "出")
            {
                if (area.RestParkingNumber < area.ParkingNumber)
                {
                    area.RestParkingNumber++;
                }

            }
            else
            {
                if (area.RestParkingNumber > 0)
                {
                    area.RestParkingNumber--;
                }

            }
            return areaSvr.Update(area) > 0;
        }





        //保存图片到本地
        public virtual string SaveImages(string imagePath, string license, string IP)
        {
            var path = CreatePath();
            imagePath = "http://" + IP + "//" + imagePath;

            string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            string imgName = dt + license + ".jpg";

            path = path + "/" + imgName;

            System.Net.WebClient wc = new System.Net.WebClient();
            wc.DownloadFile(imagePath, path);
            var dt1 = DateTime.Now;
            path = "../../CarImages/" + dt1.ToString("yyyyMM") + "/" + dt1.ToString("MMdd") + "/" + imgName;
            return path;
        }




        //判断文件夹是否存在，否则创建文件夹
        public string CreatePath()
        {
            var path = System.Configuration.ConfigurationManager.AppSettings["path"];
            var dt = DateTime.Now;
            path += "\\" + dt.ToString("yyyy") + dt.ToString("MM") + "\\" + dt.ToString("MM") + dt.ToString("dd");
            if (Directory.Exists(path))
            {

            }
            else
            {
                var directoryInfo = new DirectoryInfo(path);
                directoryInfo.Create();
            }

            return path;
        }
    }
}
