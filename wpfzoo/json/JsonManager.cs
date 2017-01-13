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


        public T ReadFile<T>(String path, String file) where T : class
        {
            T result = default(T);

            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                JObject jObject = (JObject)JToken.ReadFrom(reader);
                result = jObject.ToObject<T>();
            }

            return result;
        }
    }
}
