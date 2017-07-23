using BLL;
using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sznfc.vicRFID;

namespace ParkSys
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            State();
                      
        }

        public void State()
        {

            var annalBll = new AnnalBll();


            var inputStream = Request.InputStream;
            var strLen = Convert.ToInt32(inputStream.Length);
            var strArr = new byte[strLen];
            inputStream.Read(strArr, 0, strLen);
            var requestMes = Encoding.UTF8.GetString(strArr);

            var license = "";
            var serialno = "";
            var imagePath = "";
            var key = "";
            var value = "";
            if (requestMes != "")
            {
                JsonReader reader = new JsonTextReader(new StringReader(requestMes));
                while (reader.Read())
                {
                    if (reader.TokenType.ToString() == "PropertyName")
                    {
                        key = reader.Value.ToString();
                    }
                    if (reader.TokenType.ToString() == "String")
                    {
                        value = reader.Value.ToString();
                    }
                    if (key != "" && value != "")
                    {
                        switch (key)
                        {
                            case "license":
                                license = value;
                                break;
                            case "serialno":
                                serialno = value;
                                break;
                            case "imagePath":
                                imagePath = value;
                                break;
                            default:
                                break;
                        }
                        key = "";
                        value = "";
                    }
                }



                // 根据设备序列号得到相关信息
                var equipemnt = annalBll.GetEquipment(serialno);
                var areaAndEqu = annalBll.GetAreaAndEqu(equipemnt.EquipmentId);
                //todo 保存图片到本地
                imagePath = annalBll.SaveImages(imagePath, license, equipemnt.IP);

                // 写入记录
                annalBll.CreateAnnal(license, imagePath, equipemnt, areaAndEqu);
                //todo 更改剩余车位
                annalBll.UpdateParkNumber(areaAndEqu);
            }


        }


        
    }
}