using System.Threading.Tasks;
using Automata.IO;
using Newtonsoft.Json;

namespace Game.Code.Assets
{
    public static class FileExtensions
    {
        public static async Task<T> FromJson<T>(this IFile file)
        {
            var text = await file.ReadAsync();
            var obj = JsonConvert.DeserializeObject<T>(text,
                new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore});

            return obj;
        }

        public static async Task ToJson<T>(this IFile file, T obj)
        {
            var json = JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore});

            await file.WriteAsync(json);
        }
    }
}