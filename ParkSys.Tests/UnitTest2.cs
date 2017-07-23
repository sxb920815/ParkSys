using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using BLL;

namespace ParkSys.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {

            var css = "{ \"#header\" : {background:\"red\"}, layout : [5,4,1],color:\"cyan\" }";
            JsonReader reader = new JsonTextReader(new StringReader(css));
            var key = "";
            var value = "";
            var background = "";
            var color = "";
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
                if (key!="" && value!="")
                {
                    switch (key)
                    {
                        case "background":
                            background = value;
                            break;
                        case "color":
                            color = value;
                            break;
                        default:
                            break;
                    }
                   
                }
                
            }
            Console.WriteLine(background + "\t\t" + color);
        }


        [TestMethod]
        public void TestCreatePath()
        {
            var path = System.Configuration.ConfigurationManager.AppSettings["path"];
            var dt = DateTime.Now;
            path += "\\" + dt.ToString("yyyy") + dt.ToString("MM") + "\\" + dt.ToString("MM") + dt.ToString("dd");
            if (Directory.Exists(path))
            {
                
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.Create();
            }
        }

        [TestMethod]
        public void A()
        {
            var a = DateTime.Now;
            var b = Convert.ToDateTime("2016-09-20 21:28");
            var ts = (a - b).TotalSeconds;
        }


        [TestMethod]
        public void T()
        {
            new AnnalBll().CreateWaveAnnal("5567");
        }
    }
}
