using System.ComponentModel.DataAnnotations;

namespace Steamity.Models.InputModels
{
    public enum Genres
    {
        Action = 1,
        Survival = 2,
        Horror = 3,
        Rhytm = 4,
        Fantasy = 5,
        RPG = 6,
    }

    public class CreateGameInputModel
    {

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Title must be at most 100 characters")]
        [Required]
        public string Title { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = "Title must be at most 100 characters")]
        [Required]
        public string? Description { get; set; }

        [Required]
        public Genres? Genre { get; set; }

        
        public string? ReleaseDate { get; set; }

        [Required]
        [Range(1, 500, ErrorMessage = "Price must be between 1 and 500.")]
        public double Price { get; set; }

        public string? Platform { get; set; }

        //public IFormFile Image { get; set; }
    }

}
