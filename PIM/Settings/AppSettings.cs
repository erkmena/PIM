using System;

namespace PIM.Settings
{
    public class AppSettings
    {
        public TimeSpan CacheTtl { get; set; }
        public int CacheSizeLimit { get; set; }
        public string ConnectionString { get; set; }
    }
}
