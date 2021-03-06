﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace PokemonPrinter.WebService
{

    public class WebServiceManager
    {
        private const String BASE_URL = "http://pokeapi.co/api/v2/";

        public async Task<T> GetData<T>(String url, T item)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<T>(result);
            }

            return item;
        }
    }
}
