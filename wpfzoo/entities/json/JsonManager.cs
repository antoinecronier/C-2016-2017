using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD:wpfzoo/json/JsonManager.cs
namespace wpfzoo.entities.json
=======

namespace wpfzoo.json
>>>>>>> master:wpfzoo/entities/json/JsonManager.cs
{
    public class JsonManager
    {
        private static volatile JsonManager instance;
        private static object syncRoot = new Object();

        private JsonManager() { }

        public static JsonManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new JsonManager();
                    }
                }

                return instance;
            }
        }


        public T ReadFile<T>(String path, String file)
        {

            T toReturn = default(T);
            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                JObject jObject = (JObject)JToken.ReadFrom(reader);
                toReturn = jObject.ToObject<T>();
            }

            return toReturn;
        }
    }

}
