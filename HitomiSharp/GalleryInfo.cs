using Newtonsoft.Json;

namespace HitomiSharp
{
    [JsonObject]
    public readonly struct GalleryInfo
    {
        [JsonProperty(PropertyName = "n")]
        public string Title { get; }
        [JsonProperty(PropertyName = "a")]
        public string[] Artists { get; }
        [JsonProperty(PropertyName = "g")]
        public string[] Groups { get; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; }
        [JsonProperty(PropertyName = "l"), JsonConverter(typeof(Language))]
        public Language Language { get; }
        [JsonProperty(PropertyName = "p")]
        public string[] Series { get; }
        [JsonProperty(PropertyName = "c")]
        public string[] Characters { get; }
        [JsonProperty(PropertyName = "t")]
        public string[] Tags { get; }
        [JsonProperty(PropertyName = "id")]
        public int ID { get; }
        [JsonIgnore]
        public string Url => $"https://hitomi.la/galleries/{ID}.html";

        public GalleryInfo(
            int id,
            string title,
            string[] artists,
            string[] groups,
            string type,
            Language langauge,
            string[] series,
            string[] characters,
            string[] tags)
        {
            Title = title;
            Artists = artists;
            Groups = groups;
            Type = type;
            Language = langauge;
            Series = series;
            Characters = characters;
            Tags = tags;
            ID = id;
        }
    }
}
