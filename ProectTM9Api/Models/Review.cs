namespace ProectTM9Api.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Feedback { get; set; }
        public bool IsPublished { get; set; } = false;
    }
}
