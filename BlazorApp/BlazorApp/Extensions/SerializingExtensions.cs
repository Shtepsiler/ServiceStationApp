using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApp.Extensions
{
    public static class SerializingExtensions
    {
        private static readonly JsonSerializerOptions DefaultOptions = new()
        {
            PropertyNameCaseInsensitive = true, // Дозволяє працювати зі змінними в різних регістрах
            Converters = { new JsonStringEnumConverter() } // Конвертує Enum у string і назад
        };

        public static string Serialize<T>(this T obj) =>
            JsonSerializer.Serialize(obj, DefaultOptions);

        public static TOut Deserialize<TOut>(this string obj) =>
            JsonSerializer.Deserialize<TOut>(obj, DefaultOptions);

        public static string Serialize<T>(this T obj, JsonSerializerOptions options) =>
            JsonSerializer.Serialize(obj, options);

        public static TOut Deserialize<TOut>(this string obj, JsonSerializerOptions options) =>
            JsonSerializer.Deserialize<TOut>(obj, options);
    }
}
