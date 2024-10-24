namespace Interimkantoor.ViewModels
{
    public class JobDetailsViewModel
    {
        public int Id { get; set; }
        public string Beschrijving { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public string Locatie { get; set; }

        public bool IsBadge { get; set; }
        public bool IsKleding { get; set; }
        public bool IsWerkSchoenen { get; set; }

        public int AantalPlaatsen { get; set; }
        public int VrijePlaatsen { get; set; }
    }
}