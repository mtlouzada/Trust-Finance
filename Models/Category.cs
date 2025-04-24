namespace TF.Models;
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Slug { get; set; } // URL-friendly version of the title
    }
}