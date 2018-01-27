using System.Linq;

namespace HitomiSharp
{
    public class SearchOption
    {
        public string Title;
        public string[] Artists;
        public string[] ArtistsWithout;
        public string[] Groups;
        public string[] GroupsWithout;
        public string Type;
        public string[] TypeWithout;
        public string Language;
        public string[] LanguageWithout;
        public string[] Series;
        public string[] SeriesWithout;
        public string[] Characters;
        public string[] CharactersWithout;
        public string[] Tags;
        public string[] TagsWithout;

        public SearchOption()
        {
            Artists = new string[0];
            ArtistsWithout = new string[0];
            Groups = new string[0];
            GroupsWithout = new string[0];
            TypeWithout = new string[0];
            LanguageWithout = new string[0];
            Series = new string[0];
            SeriesWithout = new string[0];
            Characters = new string[0];
            CharactersWithout = new string[0];
            Tags = new string[0];
            TagsWithout = new string[0];
        }

        public bool IsGallery(GalleryInfo gallery)
        {
            if (ArtistsWithout.Intersect(gallery.Artists).Count() > 0)
                return false;
            if (GroupsWithout.Intersect(gallery.Groups).Count() > 0)
                return false;
            if (TypeWithout.Contains(gallery.Type))
                return false;
            if (LanguageWithout.Contains(gallery.Language))
                return false;
            if (SeriesWithout.Intersect(gallery.Series).Count() > 0)
                return false;
            if (CharactersWithout.Intersect(gallery.Characters).Count() > 0)
                return false;
            if (TagsWithout.Intersect(gallery.Tags).Count() > 0)
                return false;
            if (string.IsNullOrEmpty(Title) && gallery.Title.Contains(Title))
                return true;
            if (Artists.Intersect(gallery.Artists).Count() > 0)
                return true;
            if (Groups.Intersect(gallery.Groups).Count() > 0)
                return true;
            if (Language == gallery.Language)
                return true;
            if (Series.Intersect(gallery.Series).Count() > 0)
                return true;
            if (Characters.Intersect(gallery.Characters).Count() > 0)
                return true;
            if (Tags.Intersect(gallery.Tags).Count() > 0)
                return true;
            return false;
        }
    }
}
