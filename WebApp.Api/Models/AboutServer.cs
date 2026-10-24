namespace WebApp.Api.Models
{
    [System.ComponentModel.Description("Details about this API server")]
    public class AboutServer
    {
        [System.ComponentModel.Description("Release version")]
        [System.ComponentModel.DisplayName("Version")]
        public string? Version { get; set; }

        [System.ComponentModel.Description("Release version date")]
        [System.ComponentModel.DisplayName("VersionDate")]
        public DateTime VersionDate { get; set; }
    }
}
