using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace FTP
{
    public class Provider
    {
        //private static ILog logger = LogManager.GetLogger(typeof(Provider));

        private FileSystemWatcher fileSystemWatcher;
        private Queue<string> fullList;
        private static int maxPoolSize = 90000;
        private Semaphore poolSemaphore;
        private string extension = "jpg";
        private string path = string.Empty;
        public event Action<PlateInfo> OnCallback;



        public Provider(string ftpPath)
        {
            path = ftpPath;
            fullList = new Queue<string>(maxPoolSize);
            poolSemaphore = new Semaphore(0, maxPoolSize);

            startSender();
        }

        /// <summary>
        /// 文件监视
        /// </summary>
        public void Start()
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    //logger.Info("创建目录：" + path);
                }
                fileSystemWatcher = new FileSystemWatcher(path, "*." + extension);
                fileSystemWatcher.IncludeSubdirectories = true;
                fileSystemWatcher.EnableRaisingEvents = true;
                fileSystemWatcher.InternalBufferSize = 9999999;

                //TODO:设置监视文件状态
                fileSystemWatcher.Created += new FileSystemEventHandler(fileSystemWatcher_Created);

                //logger.Info("启动监视，等待数据···");
            }
            catch
            {
                throw;
            }
        }

        private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains("RECYCLER") || e.FullPath.Contains("RECYCLE"))
            {
                //TODO:不计算回收站创建的文件
                return;
            }
            try
            {
                if (fullList.Count >= maxPoolSize)
                {
                    //logger.WarnFormat("识别服务接收文件失败,文件池溢出.");
                }
                else
                {
                    fullList.Enqueue(e.FullPath);
                    poolSemaphore.Release();
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
            }
        }

        private void startSender()
        {
            try
            {
                Thread t = new Thread(senderMain);
                t.Start();
            }
            catch
            {
                throw;
            }
        }

        private void senderMain()
        {
            while (true)
            {
                try
                {
                    poolSemaphore.WaitOne();

                    string fullName = fullList.Dequeue();

                    ConvertFullName(fullName);
                }
                catch { }
            }
        }

        private void ConvertFullName(string fullName)
        {
            //%y-%M/%M-%d/%y%M%d%h%m%s%S-%22-%09.jpg
            //2013-01/01-06/20130106152730110-PZC2AW01800060-浙APJ896.jpg
            //yyyyMMddHHmmssfff-SBBH-HPHM.jpg

            try
            {

                FileInfo fileInfo = new FileInfo(fullName);

                var keys = fileInfo.Name.Split('-');
                var name = keys[0];
                var sj = keys[1].Substring(0,keys[1].Length-4);
                var plateInfo = new PlateInfo {HPHM = sj == "无牌" ? "-" : sj};

                new CardAnnalSvr().FTPAnnal(fullName, plateInfo.HPHM, plateInfo.SBBH);
                if (OnCallback != null)
                {
                    OnCallback(plateInfo);
                }

                //输出格式化完成后的图片路径
                //logger.Info(fullName);
            }
            catch (Exception ex)
            {
                File.Delete(fullName);
                //logger.Error(ex);
            }
        }


        public void Close()
        {
            this.Dispose(true);
        }

        #region IDisposable Members

        /// <summary>
        /// 指示是否已被释放的资源
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// 实现IDisposable接口
        /// </summary>
        public void Dispose()
        {
            // 当显式调用Dispose，通过Dispose方法的真正清洁管理资源
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                this.isDisposed = true;

                if (disposing)
                {
                }
            }
        }

        // 析构函数
        ~Provider()
        {
            // 表示本次调用是隐式调用，由Finalize方法调用，即托管资源释放由GC来完成
            Dispose(false);
        }
        #endregion
    }
}
