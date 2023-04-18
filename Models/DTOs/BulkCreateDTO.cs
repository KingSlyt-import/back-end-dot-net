namespace Back_End_Dot_Net.DTOs
{
    public class BulkCreateDTO<T>
    {
        public List<T> Items { get; set; }
    }

}