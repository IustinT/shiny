using System;
using System.Globalization;
using Microsoft.Extensions.Logging;
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
                ShinyHost.Resolve<ILogger<ShinySerializer>>()
                    .LogError(e, nameof(Deserialize), $"{objectType} - {value}");

                return objectType.GetDefaultValue();
            }
        }

        public string Serialize(object value) => JsonConvert.SerializeObject(value);
        //public T Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value);
        //public object Deserialize(Type objectType, string value) => JsonSerializer.Serialize(value, objectType);
        //public string Serialize(object value) => JsonSerializer.Serialize(value);
    }
}
