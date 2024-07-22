using ExpressVoiture.Domain.IRepository;
using ExpressVoiture.Domain.Models;
using ExpressVoiture.Services.IService;
using ExpressVoiture.ViewModel;

namespace ExpressVoiture.Services
{
    public class HomeService 
    {
        private readonly IVoitureRepository _voitureRepository;
        private readonly IVehicleService _vehicleService;

        public HomeService(IVoitureRepository voitureRepository, IVehicleService vehicleService)
        {
            _voitureRepository = voitureRepository;
            _vehicleService = vehicleService;
        }

        public List<ClientVehicleListViewModel> GetAllClientVehicleListViewModel()
        {
            IEnumerable<VoitureAVendre> productEntities = _voitureRepository.GetAll(includeProperties:"Reparation,Vente");
            return MapToClientVehicleListViewModel(productEntities);
        }

        private List<ClientVehicleListViewModel> MapToClientVehicleListViewModel(IEnumerable<VoitureAVendre> voitureEntities)
        {
            List<ClientVehicleListViewModel> voituresViewModel = new List<ClientVehicleListViewModel>();
            foreach (VoitureAVendre voiture in voitureEntities)
            {
                voituresViewModel.Add(new ClientVehicleListViewModel
                {
                    VoitureId = voiture.VoitureId,
                    Annee = voiture.Annee,
                    Modele = voiture.Modele,
                    Marque = voiture.Marque,
                    Finition = voiture.Finition,
                    ImagePath = voiture.ImagePath,
                    PrixVente = _vehicleService.CalculateSellPrice(voiture.Reparation.Cout, voiture.PrixAchat)
                });
            }
            return voituresViewModel;
        }

        public ClientDetailedVehicleViewModel GetClientDetailsViewModel(int? id) 
        {
            VoitureAVendre voitureToMap = _vehicleService.GetVoitureAVendreById(id);

            return MapToClientDetailedVehicleViewModel(voitureToMap);
        }

        private ClientDetailedVehicleViewModel MapToClientDetailedVehicleViewModel(VoitureAVendre voitureToMap)
        {
            ClientDetailedVehicleViewModel voituresViewModel = new ClientDetailedVehicleViewModel
            {
                Annee = voitureToMap.Annee,
                Modele = voitureToMap.Modele,
                Marque = voitureToMap.Marque,
                Finition = voitureToMap.Finition,
                CoutReparations = voitureToMap.Reparation.Cout,
                CodeVIN = voitureToMap.CodeVIN,
                DateDisponibiliteVente = voitureToMap.Vente.DateDisponibiliteVente,
                DateVente = voitureToMap.Vente.DateVente,
                DescriptionReparations = voitureToMap.Reparation.Description,
                PrixVente = _vehicleService.CalculateSellPrice(voitureToMap.Reparation.Cout, voitureToMap.PrixAchat),
                ImagePath = voitureToMap.ImagePath,
            };
            return voituresViewModel;
        }
    }
}
