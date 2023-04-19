using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UtilitiesServices
    {
        public void GerarLog(Guid key, object request, object response, string method, bool useCompression)
        {
            var data = new Dictionary<string, object>();
            data.Add("Request", request);
            data.Add("Response", response);

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.None
            };
            string path = AppDomain.CurrentDomain.BaseDirectory + "/log";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            path = string.Format("{0}/{1:yyyy-MM-dd}", path, DateTime.Now.Date);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            var value = JsonConvert.SerializeObject(data, jsonSerializerSettings);
            using (StreamWriter file = new StreamWriter(string.Format("{0}/{1}_{2:yyyy-MM-dd}_{3}.json", path, method, DateTime.Now, key), true))
            {
                if (useCompression)
                {
                    file.Write(ToCompress(value));
                }
                else
                {
                    file.Write(value);
                }
            }
        }
        public async Task<string> ToCompress(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            byte[] inArray;
            await using (MemoryStream memoryStream2 = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                using MemoryStream memoryStream = new MemoryStream();
                using (DeflateStream destination = new DeflateStream(memoryStream, CompressionLevel.Optimal, leaveOpen: true))
                {
                    memoryStream2.CopyTo(destination);
                }

                inArray = memoryStream.ToArray();
            }

            return Convert.ToBase64String(inArray);
        }
    }
}
