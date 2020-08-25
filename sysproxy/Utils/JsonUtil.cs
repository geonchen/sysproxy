using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;


namespace sysproxy.Utils
{
    public static class JsonUtil
    {

        public static string SerializeObject<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                string json = Encoding.UTF8.GetString(stream.ToArray());
                return json;
            }

        }

        public static T DeserializeObject<T>(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            byte[] buff = Encoding.UTF8.GetBytes(json);
            using (var stream = new MemoryStream(buff))
            {
                
                T model = (T)serializer.ReadObject(stream);
                return model;
            }
        }
    }
}
