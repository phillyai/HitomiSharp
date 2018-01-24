using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HitomiSharp
{
    public readonly struct GalleryInfo
    {
        [JsonProperty(PropertyName = "n")]
        public readonly string Title;
        [JsonProperty(PropertyName = "a")]
        public readonly string[] Artists;
        [JsonProperty(PropertyName = "g")]
        public readonly string[] Groups;
        [JsonProperty(PropertyName = "type")]
        public readonly string Type;
        [JsonProperty(PropertyName = "l")]
        public readonly string Language;
        [JsonProperty(PropertyName = "p")]
        public readonly string[] Series;
        [JsonProperty(PropertyName = "c")]
        public readonly string[] Characters;
        [JsonProperty(PropertyName = "t")]
        public readonly string[] Tags;
        [JsonProperty(PropertyName = "id")]
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
