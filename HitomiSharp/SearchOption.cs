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

        public bool IsProfit(GalleryInfo gallery)
        {
            if (gallery.Artists != null)
                if (ArtistsWithout.Intersect(gallery.Artists).Count() > 0)
                    return false;
            if (gallery.Groups != null)
                if (GroupsWithout.Intersect(gallery.Groups).Count() > 0)
                    return false;
            if (TypeWithout.Contains(gallery.Type))
                return false;
            if (LanguageWithout.Contains(gallery.Language))
                return false;
            if (gallery.Series != null)
                if (SeriesWithout.Intersect(gallery.Series).Count() > 0)
                    return false;
            if (gallery.Characters != null)
                if (CharactersWithout.Intersect(gallery.Characters).Count() > 0)
                    return false;
            if (gallery.Tags != null)
                if (TagsWithout.Intersect(gallery.Tags).Count() > 0)
                return false;
            if (!string.IsNullOrEmpty(Title))
                if (!gallery.Title.ToUpper().Contains(Title.ToUpper()))
                    return false;
            if (Artists.Length > 0)
                if (!ContainsAll(Artists, gallery.Artists))
                    return false;
            if (Groups.Length > 0)
                if (!ContainsAll(Groups, gallery.Groups))
                    return false;
            if (!string.IsNullOrEmpty(Language))
                if (Language != gallery.Language)
                    return false;
            if (Series.Length > 0)
                if (!ContainsAll(Series, gallery.Series))
                    return false;
            if (Characters.Length > 0)
                if (!ContainsAll(Characters, gallery.Characters))
                    return false;
            if (Tags.Length > 0)
                if (!ContainsAll(Tags, gallery.Tags))
                    return false;
            return true;
        }

        /// <summary>
        /// Check all elements in source are in dest
        /// </summary>
        private static bool ContainsAll(string[] source, string[] dest)
        {
            if (dest == null)
            {
                if (source.Length == 0) return true;
                else return false;
            }
            foreach (var s in source)
                if (!dest.Contains(s))
                    return false;
            return true;
        }
    }
}
