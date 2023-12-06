namespace Steamity.Models.ViewModels.GameViewModels.WishlistViewModels
{
    public class WishlistGameViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageId { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }
        public double Price { get; internal set; }
    }
}
