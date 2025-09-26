namespace ExpressVoiture.Shared.ViewModel
{
    public class ClientVehicleListDto
    {
        public int VoitureId { get; set; }
        public int Annee { get; set; }
        public string? Marque { get; set; }
        public string? Modele { get; set; }
        public string? Finition { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? DateVente { get; set; }
        public double PrixVente { get; set; }
    }
}
