using System;
using System.Collections.Generic;
using System.Text;

namespace HitomiSharp
{
    public readonly struct GalleryInfo
    {
        public string Title { get; }
        public string[] Artists { get; }
        public string[] Groups { get; }
        public string Type { get; }
        public Language Language { get; }
        public string[] Series { get; }
        public string[] Characters { get; }
        public string[] Tags { get; }

        public string ID { get; }
        public string Url => $"https://hitomi.la/galleries/{ID}.html";

        public GalleryInfo(
            string id,
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
