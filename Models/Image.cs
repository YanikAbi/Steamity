namespace Steamity.Models
{
    public class Image
    {
        public string? ImageId { get; set; }

        public int GameId { get; set; }

        public Game? Game { get; set; }
    }
}
