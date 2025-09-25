using ExpressVoiture.API.Application.Services.Interface;
using ExpressVoiture.API.Domain.IRepository;
using ExpressVoiture.API.Domain.Models;
using ExpressVoiture.Shared.ViewModel;

namespace ExpressVoiture.API.Application.Services
{
    public class VoitureService : IVoitureService
    {
        private readonly IVoitureRepository _voitureRepository;
        public VoitureService(IVoitureRepository voitureRepository)
        {
            _voitureRepository = voitureRepository;
        }

        public async Task<List<AdminVehicleListViewModel>> GetListAdminVehicleViewModel()
        {

            IEnumerable<VoitureAVendre> productEntities = await _voitureRepository.GetAll(includeProperties: "Reparation,Vente");
            return MapToAdminVehicleListViewModel(productEntities);
        }

        private List<AdminVehicleListViewModel> MapToAdminVehicleListViewModel(IEnumerable<VoitureAVendre> voitureEntities)
        {
            List<AdminVehicleListViewModel> voituresViewModel = new List<AdminVehicleListViewModel>();
            foreach (VoitureAVendre voiture in voitureEntities)
            {
                voituresViewModel.Add(new AdminVehicleListViewModel
                {
                    VoitureId = voiture.VoitureId,
                    Annee = voiture.Annee,
                    Modele = voiture.Modele,
                    Marque = voiture.Marque,
                    CodeVIN = voiture.CodeVIN,
                    Finition = voiture.Finition,
                    PrixAchat = voiture.PrixAchat,
                    DateVente = voiture.Vente.DateVente,
                    DateAchat = voiture.DateAchat,
                    CoutReparations = voiture.Reparation.Cout,
                    DateDisponibiliteVente = voiture.Vente.DateDisponibiliteVente,
                    DescriptionReparations = voiture.Reparation.Description,
                    PrixVente = CalculateSellPrice(voiture.Reparation.Cout, voiture.PrixAchat)
                });
            }
            return voituresViewModel;
        }

        private double CalculateSellPrice(double coutReparation, double prixAxhat)
        {
            int benefice = 500;
            return coutReparation + prixAxhat + benefice;
        }

        public async Task<AddOrUpdateVehicleViewModel> GetAddOrUpdateVehicleViewModel(int? id)
        {
            VoitureAVendre voiture = await _voitureRepository.Get(u => u.VoitureId == id, includeProperties: "Reparation,Vente");
            return MapToAddOrUpdateVehicleViewModel(voiture);
        }

        private AddOrUpdateVehicleViewModel MapToAddOrUpdateVehicleViewModel(VoitureAVendre voiture)
        {
            AddOrUpdateVehicleViewModel voituresViewModel = new AddOrUpdateVehicleViewModel
            {
                VoitureId = voiture.VoitureId,
                Annee = voiture.Annee,
                Modele = voiture.Modele,
                Marque = voiture.Marque,
                CodeVIN = voiture.CodeVIN,
                Finition = voiture.Finition,
                PrixAchat = voiture.PrixAchat,
                DateVente = voiture.Vente.DateVente,
                DateAchat = voiture.DateAchat,
                CoutReparations = voiture.Reparation.Cout,
                DateDisponibiliteVente = voiture.Vente.DateDisponibiliteVente,
                DescriptionReparations = voiture.Reparation.Description,
                ImagePath = voiture.ImagePath,
            };
            return voituresViewModel;
        }

        public async Task SaveVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter)
        {
            VoitureAVendre VoitureAAjouter = MapToVoitureAVendreEntity(voitureAAjouter);
            await _voitureRepository.Add(VoitureAAjouter);
            await _voitureRepository.Save();
        }

        public async Task UpdateVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter)
        {
            VoitureAVendre voitureExistante = await GetVoitureAVendreById(voitureAAjouter.VoitureId);

            if (voitureExistante != null)
            {
                MapToVoitureAVendreEntity(voitureAAjouter, voitureExistante);
                await _voitureRepository.Update(voitureExistante);
                await _voitureRepository.Save();
            }
        }

        private VoitureAVendre MapToVoitureAVendreEntity(AddOrUpdateVehicleViewModel voitureAAjouter, VoitureAVendre voitureExistante = null)
        {
            VoitureAVendre voiture = voitureExistante ?? new VoitureAVendre();

            voiture.Annee = voitureAAjouter.Annee;
            voiture.CodeVIN = voitureAAjouter.CodeVIN;
            voiture.DateAchat = voitureAAjouter.DateAchat;
            voiture.Finition = voitureAAjouter.Finition;
            voiture.Marque = voitureAAjouter.Marque;
            voiture.Modele = voitureAAjouter.Modele;
            voiture.PrixAchat = voitureAAjouter.PrixAchat;
            voiture.ImagePath = voitureAAjouter.ImagePath;


            voiture.Reparation = voiture.Reparation ?? new Reparation { VoitureId = voiture.VoitureId };
            voiture.Reparation.Description = voitureAAjouter.DescriptionReparations;
            voiture.Reparation.Cout = voitureAAjouter.CoutReparations;

            voiture.Vente = voiture.Vente ?? new Vente { VoitureId = voiture.VoitureId };
            voiture.Vente.DateDisponibiliteVente = voitureAAjouter.DateDisponibiliteVente;
            voiture.Vente.DateVente = voitureAAjouter.DateVente;

            return voiture;
        }
        public async Task DeleteVoitureAVendre(int? id)
        {
            VoitureAVendre? voiture = await GetVoitureAVendreById(id);

            if (voiture is null)
            {
                throw new Exception($"Voiture with ID {id} not found.");
            }

            if (!string.IsNullOrEmpty(voiture.ImagePath))
            {
                string path = Path.Combine("wwwroot", voiture.ImagePath.TrimStart('\\'));
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            await _voitureRepository.Remove(voiture);
            await _voitureRepository.Save();
        }

        public async Task<VoitureAVendre> GetVoitureAVendreById(int? id)
        {
            return await _voitureRepository.Get(u => u.VoitureId == id, includeProperties: "Reparation,Vente");
        }

        public async Task<DeleteVehicleViewModel> GetDeleteVehicleViewModel(int? id)
        {
            VoitureAVendre voiture = await _voitureRepository.Get(u => u.VoitureId == id);
            return MapToDeleteVehicleViewModel(voiture);
        }

        private DeleteVehicleViewModel MapToDeleteVehicleViewModel(VoitureAVendre voiture)
        {
            DeleteVehicleViewModel voituresViewModel = new DeleteVehicleViewModel
            {
                VoitureId = voiture.VoitureId,
                Annee = voiture.Annee,
                Modele = voiture.Modele,
                Marque = voiture.Marque,
                Finition = voiture.Finition
            };
            return voituresViewModel;
        }

        public async Task<ClientDetailedVehicleViewModel> GetClientDetailedVehicle(int id)
        {
            VoitureAVendre vehicle = await _voitureRepository.Get(v => v.VoitureId == id, includeProperties: "Reparation,Vente");
            if (vehicle is null) return null;

            var result = new ClientDetailedVehicleViewModel
            {
                Annee = vehicle.Annee,
                Modele = vehicle.Modele,
                Marque = vehicle.Marque,
                Finition = vehicle.Finition,
                CoutReparations = vehicle.Reparation.Cout,
                CodeVIN = vehicle.CodeVIN,
                DateDisponibiliteVente = vehicle.Vente.DateDisponibiliteVente,
                DateVente = vehicle.Vente.DateVente,
                DescriptionReparations = vehicle.Reparation.Description,
                PrixVente = CalculateSellPrice(vehicle.Reparation.Cout, vehicle.PrixAchat),
                ImagePath = vehicle.ImagePath
            };

            return result;
        }

        public async Task<IEnumerable<ClientVehicleListViewModel>> GetAllClientVehicle(string includeProperties)
        {
            IEnumerable<VoitureAVendre> voitures = await _voitureRepository.GetAll(includeProperties);

            if (voitures is null) return null;

            var result = voitures.Select(vehicle => new ClientVehicleListViewModel
            {
                VoitureId = vehicle.VoitureId,
                Annee = vehicle.Annee,
                Modele = vehicle.Modele,
                Marque = vehicle.Marque,
                Finition = vehicle.Finition,
                ImagePath = vehicle.ImagePath,
                DateVente = vehicle.Vente.DateVente,
                PrixVente = CalculateSellPrice(vehicle.Reparation.Cout, vehicle.PrixAchat)
            }).ToList();

            return result;
        }
    }
}
