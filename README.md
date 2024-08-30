# Express Voitures

Express Voitures est une application web permettant de gérer un catalogue de véhicules, d'ajouter des réparations, de suivre les finances associées aux véhicules, et de gérer les utilisateurs et les rôles.

## Table des matières

- [Fonctionnalités](#fonctionnalités)
- [Prérequis](#prérequis)
- [Installation](#installation)
- [Utilisation](#utilisation)
- [Structure du Projet](#structure-du-projet)
- [Contribuer](#contribuer)
- [Licence](#licence)

## Fonctionnalités

- Ajouter, modifier et supprimer des voitures.
- Gérer les réparations et les finances des véhicules.
- Gestion des utilisateurs et des rôles (Admin, Visiteur).
- Authentification avec gestion des rôles pour sécuriser certaines fonctionnalités.
- Affichage conditionnel des informations en fonction des droits de l'utilisateur.

## Prérequis

Avant de commencer, assurez-vous d'avoir les éléments suivants installés sur votre machine :

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads) ou une autre base de données compatible
- Un éditeur de code comme [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)

## Installation

1. Clonez le dépôt depuis GitHub :

   ```bash
   git clone https://github.com/votre-nom-utilisateur/express-voitures.git
   cd express-voitures
   ```

2. Restaurez les dépendances du projet :

   ```bash
   dotnet restore
   ```

3. Mettez en place la base de données :

   - Configurez la chaîne de connexion dans `appsettings.json`.
   - Appliquez les migrations pour créer la base de données :

     ```bash
     dotnet ef database update
     ```

4. Lancez l'application :

   ```bash
   dotnet run
   ```

   L'application devrait maintenant être accessible à l'adresse `https://localhost:5001`.

## Utilisation

### Compte Administrateur

1. Connectez-vous en tant qu'administrateur pour gérer les voitures, les utilisateurs, les rôles et autres fonctionnalités administratives.
2. Pour créer un compte administrateur, vous pouvez initialiser les rôles et l'utilisateur administrateur par défaut en modifiant `Program.cs`.

### Gestion des Véhicules

- Ajouter un véhicule : Accessible via le menu "Ajouter une voiture" (seulement pour les administrateurs).
- Modifier ou supprimer un véhicule : Cliquez sur les boutons "Modifier" ou "Supprimer" dans la vue "Détails de la voiture".

### Réparations et Finances

- Gérer les réparations et les finances depuis la vue "Détails de la voiture".

## Structure du Projet

- **Controllers** : Contient les contrôleurs qui gèrent les requêtes HTTP et retournent les vues ou les réponses API.
- **Models** : Contient les entités de base de données, les ViewModels et les services.
- **Views** : Contient les vues Razor utilisées pour afficher les données aux utilisateurs.
- **wwwroot** : Contient les fichiers statiques (CSS, JavaScript, images).
- **Areas** : Contient les pages Razor pour la gestion des comptes utilisateurs.

## Contribuer

Les contributions sont les bienvenues ! Veuillez suivre les étapes suivantes pour contribuer :

1. Fork le projet.
2. Créez une branche pour votre fonctionnalité (`git checkout -b feature/nouvelle-fonctionnalité`).
3. Committez vos modifications (`git commit -m 'Ajouter nouvelle fonctionnalité'`).
4. Poussez sur la branche (`git push origin feature/nouvelle-fonctionnalité`).
5. Ouvrez une Pull Request.

## Licence

Ce projet est sous licence MIT. Consultez le fichier [LICENSE](LICENSE) pour plus d'informations.
