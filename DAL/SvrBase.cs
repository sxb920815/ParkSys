using System;

namespace DAL
{
        public class SvrBase<T> where T : class, new()
        {
            protected static T _Instance = null;
            protected static readonly object _SyncRoot = new Object();

            /// <summary>
            /// 单件
            /// </summary>
            /// <returns></returns>
            public static T Instance
            {
                get
                {
                    if (_Instance == null)
                    {
                        lock (_SyncRoot)
                        {
                            if (_Instance == null)
                            {
                                try
                                {
                                    _Instance = new T();
                                }
                                catch
                                {
                                    throw;
                                }
                            }
                        }
                    }

                    return _Instance;
                }
            }
        }
    
}
