# ExpressVoiture

ExpressVoiture est une solution complète de gestion de véhicules d'occasion comprenant une API REST, une application web MVC et une application mobile MAUI. La plateforme permet de consulter des véhicules disponibles à la vente, avec des fonctionnalités avancées de gestion pour les administrateurs.

## Architecture

Le projet est composé de trois parties principales :

- **ExpressVoiture.API** : API REST ASP.NET Core servant de backend centralisé
- **ExpressVoiture.Web** : Application web MVC ASP.NET Core pour l'interface client web
- **ExpressVoiture.MAUI** : Application mobile permettant la gestion des véhicules depuis un appareil mobile

## Technologies

- **Framework**: ASP.NET Core (API & MVC)
- **Mobile**: .NET MAUI
- **Base de données**: SQL Server

## Fonctionnalités

### Pour tous les utilisateurs
- **Consultation des véhicules** : Accédez à une liste de véhicules disponibles à la vente d'occasion
- **Détails des véhicules** : Visualisez les informations complètes de chaque véhicule (marque, modèle, année, finition, réparations, prix, etc.)

### Pour les administrateurs
- **Gestion complète des véhicules** via l'application web ou mobile :
  - Ajouter de nouveaux véhicules
  - Modifier les informations des véhicules existants
  - Supprimer des véhicules

## Accès Admin

Pour accéder aux fonctionnalités d'administration (web et mobile), connectez-vous avec les identifiants suivants :

- **Adresse e-mail** : `admin@123.com`
- **Mot de passe** : `Admin@123`

## Installation

### 1. Cloner le dépôt
```bash
git clone https://github.com/RenatoSclr/ExpressVoiture.git
cd ExpressVoiture
