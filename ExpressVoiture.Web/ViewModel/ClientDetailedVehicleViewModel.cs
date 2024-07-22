namespace ExpressVoiture.ViewModel
{
    public class ClientDetailedVehicleViewModel
    {
        public int Annee { get; set; }

        public string? Marque { get; set; }

        public string? Modele { get; set; }

        public string? Finition { get; set; }
        public string? CodeVIN { get; set; }

        public string? DescriptionReparations { get; set; }
        public double CoutReparations { get; set; }
        public DateTime DateDisponibiliteVente { get; set; }

        public double PrixVente { get; set; }

        public DateTime? DateVente { get; set; }
        public string? ImagePath { get; set; }
    }
}
