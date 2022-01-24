using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MOT_History.Models;

namespace MOT_History.Data
{
    public class DataService
    {
        protected string APIUrl = "https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration=";
        protected string key = "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno"; //API Key
        protected HttpClient http;

        public DataService()
        {
            http = new HttpClient();
            http.DefaultRequestHeaders.Add("Accept", "application/json+v6");
            //http.DefaultRequestHeaders.Add("key", "x-api-key");
            http.DefaultRequestHeaders.Add("x-api-key", key);
            http.DefaultRequestHeaders.Add("description", "");
        }

        //Create API string
        public string BuildAPIUrl(string reg)
        {
            return APIUrl + reg;
        }

        //Call API and catch errors
        public async Task<List<Car>> GetMOTHistory(string reg)
        {
            try
            {
                var test = await http.GetFromJsonAsync<List<Car>>(BuildAPIUrl(reg));
                return test;
            }
            catch (HttpRequestException e)
            {
                var test = e;
                return null;
            }
            catch (NotSupportedException e)
            {
                var test = e;
                return null;
            }
            catch (JsonException e)
            {
                var test = e;
                return null;
            }
        }
    }
}
