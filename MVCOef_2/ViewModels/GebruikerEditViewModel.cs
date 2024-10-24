namespace Interimkantoor.ViewModels
{
    public class GebruikerEditViewModel
    {
        [Required]
        public string Naam { get; set; }

        [Required]
        public string Voornaam { get; set; }

        [Required]
        public DateTime Geboortedatum { get; set; }

        [Required]
        public string Email { get; set; }

        public string GebruikerId { get; set; }
    }
}