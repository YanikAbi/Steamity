namespace Steamity.Models.InputModels
{
    public class EditGameInputModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public string ReleaseDate { get; set; }

    }
}
