using System;
using System.Globalization;
using Newtonsoft.Json;

//using System.Text.Json;


namespace Shiny.Infrastructure
{
    public class ShinySerializer : ISerializer
    {
        public T Deserialize<T>(string value) => JsonConvert.DeserializeObject<T>(value);

        public object Deserialize(Type objectType, string value)
        {
            try
            {
                if (objectType == typeof(DateTime) && DateTime
                    .TryParseExact(value, "MM/dd/yyyy HH:mm:ss", null,
                        DateTimeStyles.None, out var dtValue))
                {
                    return dtValue;
                }


                return JsonConvert.DeserializeObject(value, objectType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string Serialize(object value) => JsonConvert.SerializeObject(value);
        //public T Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value);
        //public object Deserialize(Type objectType, string value) => JsonSerializer.Serialize(value, objectType);
        //public string Serialize(object value) => JsonSerializer.Serialize(value);
    }
}
