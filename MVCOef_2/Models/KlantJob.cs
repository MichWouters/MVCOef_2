namespace Interimkantoor.Models
{
    public class KlantJob
    {
        [Key]
        public int Id { get; set; }

        public string KlantId { get; set; }
        public int JobId { get; set; }

        public Klant? Klant { get; set; }

        [JsonIgnore]
        public Job? Job { get; set; }
    }
}