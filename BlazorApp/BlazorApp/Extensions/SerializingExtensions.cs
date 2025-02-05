using System.Text.Json;

namespace BlazorApp.Extensions
{
    public static class SerializingExtensions
    {
        public static string Serialize<T>(this T obj) =>
            JsonSerializer.Serialize(obj, JsonSerializerOptions.Default);

        public static TOut Deserialize<TOut>(this string obj) =>
            JsonSerializer.Deserialize<TOut>(obj, JsonSerializerOptions.Default);

        public static string Serialize<T>(this T obj, JsonSerializerOptions options) =>
    JsonSerializer.Serialize(obj, options);

        public static TOut Deserialize<TOut>(this string obj, JsonSerializerOptions options) =>
            JsonSerializer.Deserialize<TOut>(obj, options);
    }
}
