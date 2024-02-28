namespace Project.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string Picture { get; set; }
        public Category Category { get; set; }
        public bool Raffeled { get; set; }
    }
}
