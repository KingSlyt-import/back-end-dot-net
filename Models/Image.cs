namespace Back_End_Dot_Net.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string Meta { get; set; }
        public bool Hide { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
