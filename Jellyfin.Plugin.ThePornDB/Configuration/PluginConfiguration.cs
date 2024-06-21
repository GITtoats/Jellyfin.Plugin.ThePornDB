using MediaBrowser.Model.Plugins;

namespace ThePornDB.Configuration
{
    public enum OrderStyle
    {
        Default = 0,
        DistanceByTitle = 1,
    }

    public enum TagStyle
    {
        Genre = 0,
        Tag = 1,
        Disabled = 2,
    }

    public enum StudioStyle
    {
        Site = 0,
        Network = 1,
        All = 2,
        Parent = 3,
    }
    
    public enum ThumbImageStyle
    {
        All = 0,
        Full = 1,
        None = 2,
        Image = 3,
        
    }
    public enum PosterImageStyle
    {
        All = 0,
        Full = 1,
        None = 2,
        Large = 3,
        Image = 4,
        Posters = 5,

    }

    public enum BackdropImageStyle
    {
        All = 0,
        Full = 1,
        None = 2,
        Large = 3,
        Image = 4,
        Full_Large = 5,         
    }

    public enum ActorsTagStyle
    {
        None = 0,
        Custom = 1,
    }

    public enum ActorsRoleStyle
    {
        None = 0,
        Gender = 1,
        NameByScene = 2,
        NamesDifferent = 3,
    }

    public enum ActorsImageStyle
    {
        Poster = 0,
        Face = 1,
    }

     public enum ActorsOverviewStyle
    {
        None = 0,
        Custom = 1,
    }

    public class PluginConfiguration : BasePluginConfiguration
    {
        public PluginConfiguration()
        {
            this.MetadataAPIToken = string.Empty;

            this.UseFilePath = true;
            this.UseOSHash = true;

            this.OrderStyle = OrderStyle.Default;
            this.TagStyle = TagStyle.Genre;

            this.AddCollectionToCollections = true;
            this.StudioStyle = StudioStyle.All;

            this.UseCustomTitle = false;
            this.CustomTitle = "{studio}: {title} ({actors})";

            this.UseOriginalTitle = false;
            this.OriginalTitle = "{studio}: {title} ({no_male})";

            this.UseTagline = false;
            this.Tagline = "{studio} - {id}";

            this.UseSortName = false;

            this.ThumbImage = ThumbImageStyle.None;
            this.PosterImage = PosterImageStyle.Full;
            this.BackdropImage = BackdropImageStyle.Full;

            this.UseUnmatchedTag = false;
            this.UnmatchedTag = "Missing From ThePornDB";

            this.DisableMediaAutoIdentify = false;
            this.DisableActorsAutoIdentify = false;

            this.ActorsRole = ActorsRoleStyle.None;
            this.ActorsTagList = "{ethnicity} {tag}";
            this.ActorsRole = ActorsRoleStyle.None;
            this.ActorsImage = ActorsImageStyle.Poster;
            this.ActorsOverview = ActorsOverviewStyle.None;
            this.ActorsOverviewFormat = "<strong style=\"color:#ff0000\">{measurements}<br/></strong>{cupsize}-{waist}-{hips}<br/>{tattoos}<br/>{piercings}<br/>{bio}";
        }

        public string MetadataAPIToken { get; set; }

        public bool UseFilePath { get; set; }

        public bool UseOSHash { get; set; }

        public OrderStyle OrderStyle { get; set; }

        public TagStyle TagStyle { get; set; }

        public bool AddCollectionToCollections { get; set; }

        public StudioStyle StudioStyle { get; set; }

        public ThumbImageStyle ThumbImage { get; set; }

        public PosterImageStyle PosterImage { get; set; }

        public BackdropImageStyle BackdropImage { get; set; }

        public bool UseCustomTitle { get; set; }

        public string CustomTitle { get; set; }

        public bool UseOriginalTitle { get; set; }

        public string OriginalTitle { get; set; }

        public bool UseTagline { get; set; }

        public string Tagline { get; set; }

        public bool UseSortName { get; set; }

        public bool UseUnmatchedTag { get; set; }

        public string UnmatchedTag { get; set; }

        public bool DisableMediaAutoIdentify { get; set; }

        public bool DisableActorsAutoIdentify { get; set; }

        public ActorsTagStyle ActorsTags { get; set; }

        public string ActorsTagList { get; set; }

        public ActorsRoleStyle ActorsRole { get; set; }

        public ActorsImageStyle ActorsImage { get; set; }

        public ActorsOverviewStyle ActorsOverview { get; set; }

        public string ActorsOverviewFormat { get; set; }
    }
}
