using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace ePizza.UI.Utils
{
    public static class TempDataExtension
    {
        public static void Set<T>(this ITempDataDictionary tempData,string key,T value)
        {
            JsonSerializerOptions options
                = new JsonSerializerOptions();

            tempData[key] = JsonSerializer.Serialize(value,options);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class 
        {
            tempData.TryGetValue(key, out var value);

            if (value != null)
            {
               return  JsonSerializer.Deserialize<T>((string)value)!;
            }
            throw new Exception("Keyn Not Found");
        }

        public static T Peek<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object obj = tempData.Peek(key);
            return obj == null ? null : JsonSerializer.Deserialize<T>((string)obj);
        }
    }
}
