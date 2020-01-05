using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Surya
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static JObject config = null;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (StreamReader file = File.OpenText("config.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                config = (JObject)JToken.ReadFrom(reader);
            }
            Application.Run(new Form1());
        }
    }
}
