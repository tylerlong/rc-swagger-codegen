using System.IO;
using Newtonsoft.Json;

namespace RingCentral.Test
{
    public partial class Config
    {
        private static Config instance = null;
        private Config() { }

        internal static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    var jsonStr = File.ReadAllText("./bin/Debug/config.json");
                    instance = JsonConvert.DeserializeObject<Config>(jsonStr);
                }
                return instance;
            }
        }
    }

    public partial class Config
    {
        public bool? production;
        public string server;
        public string appKey;
        public string appSecret;
        public string username;
        public string extension;
        public string password;
    }
}