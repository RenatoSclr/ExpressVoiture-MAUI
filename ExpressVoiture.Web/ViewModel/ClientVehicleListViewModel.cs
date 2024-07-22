namespace ExpressVoiture.ViewModel
{
    public class ClientVehicleListViewModel
    {
        public int VoitureId { get; set; }
        public int Annee { get; set; }
        public string? Marque { get; set; }
        public string? Modele { get; set; }
        public string? Finition { get; set; }
        public string? ImagePath { get; set; }
        public double PrixVente { get; set; }
    }
}
