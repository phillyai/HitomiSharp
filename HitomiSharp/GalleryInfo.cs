using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HitomiSharp
{
    public readonly struct GalleryInfo
    {
        [JsonProperty(PropertyName = "n")]
        public readonly string Title;
        [JsonProperty(PropertyName = "a", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly string[] Artists;
        [JsonProperty(PropertyName = "g", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly string[] Groups;
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly string Type;
        [JsonProperty(PropertyName = "l", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly string Language;
        [JsonProperty(PropertyName = "p", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly string[] Series;
        [JsonProperty(PropertyName = "c", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly string[] Characters;
        [JsonProperty(PropertyName = "t", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly string[] Tags;
        [JsonProperty(PropertyName = "id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public readonly int ID;
        [JsonIgnore]
        public string Url => $"https://hitomi.la/galleries/{ID}.html";

        public GalleryInfo(
            int id,
            string title,
            string[] artists,
            string[] groups,
            string type,
            string language,
            string[] series,
            string[] characters,
            string[] tags)
        {
            Title = title;
            Artists = artists;
            Groups = groups;
            Type = type;
            Language = language;
            Series = series;
            Characters = characters;
            Tags = tags;
            ID = id;
        }
    }
}
