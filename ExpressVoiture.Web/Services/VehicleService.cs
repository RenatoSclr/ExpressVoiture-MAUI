using ExpressVoiture.Domain.IRepository;
using ExpressVoiture.Domain.Models;
using ExpressVoiture.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using ExpressVoiture.Services.IService;

namespace ExpressVoiture.Services
{
    public class VehicleService :IVehicleService
    {
        private readonly IVoitureRepository _voitureRepository;
        private readonly IFileService _fileService;
        public VehicleService(IVoitureRepository voitureRepository, IFileService fileService) 
        {
            _voitureRepository = voitureRepository;
            _fileService = fileService;
        }

        public List<AdminVehicleListViewModel> GetListAdminVehicleViewModel()
        {

            IEnumerable<VoitureAVendre> productEntities = _voitureRepository.GetAll(includeProperties: "Reparation,Vente");
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

        public double CalculateSellPrice(double coutReparation, double prixAxhat)
        {
            int benefice = 500;
            return coutReparation + prixAxhat + benefice;
        }

        public AddOrUpdateVehicleViewModel GetAddOrUpdateVehicleViewModel(int? id)
        {
            VoitureAVendre voiture = _voitureRepository.Get(u=> u.VoitureId == id, includeProperties: "Reparation,Vente");
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

        public void SaveVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter, IFormFile file)
        {
            voitureAAjouter = _fileService.CreateFile(voitureAAjouter, file);
            VoitureAVendre VoitureAAjouter = MapToVoitureAVendreEntity(voitureAAjouter);
            _voitureRepository.Add(VoitureAAjouter);
            _voitureRepository.Save();
        }

        public void UpdateVoitureAVendre(AddOrUpdateVehicleViewModel voitureAAjouter, IFormFile file)
        {
            voitureAAjouter = _fileService.CreateFile(voitureAAjouter, file);

            VoitureAVendre voitureExistante = GetVoitureAVendreById(voitureAAjouter.VoitureId);

            if (voitureExistante != null)
            {
                MapToVoitureAVendreEntity(voitureAAjouter, voitureExistante);
                _voitureRepository.Update(voitureExistante);
                _voitureRepository.Save();
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
        public void DeleteVoitureAVendre(int? id)
        {
            VoitureAVendre? voiture = GetVoitureAVendreById(id);

            voiture = _fileService.DeleteFile(voiture);

            _voitureRepository.Remove(voiture);
            _voitureRepository.Save();
        }

        public VoitureAVendre GetVoitureAVendreById(int? id)
        {
            return _voitureRepository.Get(u => u.VoitureId == id, includeProperties: "Reparation,Vente");
        }

        public DeleteVehicleViewModel GetDeleteVehicleViewModel(int? id)
        {
            VoitureAVendre voiture = _voitureRepository.Get(u => u.VoitureId == id);
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
        
    }
}
