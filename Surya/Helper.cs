using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surya
{
    public static class Helper
    {
        public static Image TryAndGetImage(string itemCode)
        {
            Image imageFile;
            try
            {
                imageFile = System.Drawing.Image.FromFile(Program.config.GetValue("imageDir").ToString() + "\\" + itemCode + ".png");
            }
            catch (Exception)
            {
                try
                {
                    imageFile = System.Drawing.Image.FromFile(Program.config.GetValue("imageDir").ToString() + "\\" + itemCode.ToUpper() + ".jpg");
                }
                catch (Exception)
                {
                    try
                    {
                        imageFile = System.Drawing.Image.FromFile(Program.config.GetValue("imageDir").ToString() + "\\" + itemCode + ".jpeg");
                    }
                    catch (Exception)
                    {
                        imageFile = System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + "\\Resources\\default.jpg");
                    }
                }
            }

            return imageFile;
        }
    }
}
