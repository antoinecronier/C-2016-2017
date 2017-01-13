using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfzoo.json
{
    public class JsonManager
    {
        public String ReadFile(String path, String file)
        {
            JObject o1 = JObject.Parse(File.ReadAllText(path + file));

            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
            }

            return null;
        }
    }

}
