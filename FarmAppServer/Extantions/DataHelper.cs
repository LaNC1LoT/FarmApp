using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace FarmAppServer.Extantions
{
    public static class DataHelper
    {
        public static bool IsValidJson(this string input, out string errorMsg)
        {
            errorMsg = string.Empty;
            input = input.Trim();
            if ((input.StartsWith("{") && input.EndsWith("}")) || //For object
                (input.StartsWith("[") && input.EndsWith("]"))) //For array
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(input))
                        throw new Exception("Пустой параметр!");

                    //parse the input into a JObject
                    var jObject = JObject.Parse(input);

                    foreach (var jo in jObject)
                    {
                        string name = jo.Key;
                        JToken value = jo.Value;

                        //if the element has a missing value, it will be Undefined - this is invalid
                        if (value.Type == JTokenType.Undefined)
                        {
                            throw new Exception(JTokenType.Undefined.ToString());
                        }
                    }
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    errorMsg = jex.InnerException?.Message ?? jex.Message;
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    errorMsg = ex.InnerException?.Message ?? ex.Message;
                    return false;
                }
            }
            else
            {
                errorMsg = "Неверный формат Json!";
                return false;
            }
            return true;
        }
    }
}
