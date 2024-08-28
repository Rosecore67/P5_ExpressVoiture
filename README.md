Express Voitures

Express Voitures est une application web permettant de gérer un catalogue de véhicules, d'ajouter des réparations, de suivre les finances associées aux véhicules, et de gérer les utilisateurs et les rôles.

Table des matières

  Fonctionnalités
  Prérequis
  Installation
  Utilisation
  Structure du Projet
  Contribuer
  Licence

Fonctionnalités

  Ajouter, modifier et supprimer des voitures.
  
  Gérer les réparations et les finances des véhicules.
  
  Gestion des utilisateurs et des rôles (Admin, Visiteur).
  
  Authentification avec gestion des rôles pour sécuriser certaines fonctionnalités.
  
  Affichage conditionnel des informations en fonction des droits de l'utilisateur.

Prérequis

Avant de commencer, assurez-vous d'avoir les éléments suivants installés sur votre machine :

  .NET 6.0 SDK
  
  SQL Server ou une autre base de données compatible
  
  Un éditeur de code comme Visual Studio ou Visual Studio Code
  

Installation

1.Clonez le dépôt depuis GitHub :
  git clone https://github.com/votre-nom-utilisateur/express-voitures.git
  cd express-voitures

2.Restaurez les dépendances du projet :
  dotnet restore

3.Mettez en place la base de données :
    - Configurez la chaîne de connexion dans appsettings.json.
    - Appliquez les migrations pour créer la base de données :
                  dotnet ef database update

Lancez l'application :
    dotnet run
    L'application devrait maintenant être accessible à l'adresse https://localhost:5001.

Utilisation

Toutes les personnes allant sur le site ont accès à Voiture/Index et Voiture/Details.

Compte Administrateur

   Connectez-vous en tant qu'administrateur pour gérer les voitures, les utilisateurs, les rôles et autres fonctionnalités administratives.
   Pour créer un compte administrateur, vous pouvez initialiser les rôles et l'utilisateur administrateur par défaut en modifiant Program.cs.

Compte Visiteur

  Pour toutes les personnes s'inscrivant sur le site, le compte de base est mis sur "Visiteur".
  Sous visiteur la personne n'a accès qu'à la vue de la page Voiture/Index et Voiture/Details.

Gestion des Véhicules

  Ajouter un véhicule : Accessible via le menu "Ajouter une voiture" (seulement pour les administrateurs).
  Modifier ou supprimer un véhicule : Cliquez sur les boutons "Modifier" ou "Supprimer" dans la vue "Détails de la voiture".

Réparations et Finances

  Gérer les réparations et les finances depuis la vue "Détails de la voiture".

Structure du Projet

  Controllers : Contient les contrôleurs qui gèrent les requêtes HTTP et retournent les vues ou les réponses API.
  Models : Contient les entités de base de données, les ViewModels et les services.
  Views : Contient les vues Razor utilisées pour afficher les données aux utilisateurs.
  wwwroot : Contient les fichiers statiques (CSS, JavaScript, images).
  Areas : Contient les pages Razor pour la gestion des comptes utilisateurs.

Contribuer

Les contributions sont les bienvenues ! Veuillez suivre les étapes suivantes pour contribuer :

  Fork le projet.
  Créez une branche pour votre fonctionnalité (git checkout -b feature/nouvelle-fonctionnalité).
  Committez vos modifications (git commit -m 'Ajouter nouvelle fonctionnalité').
  Poussez sur la branche (git push origin feature/nouvelle-fonctionnalité).
  Ouvrez une Pull Request.

Licence

Ce projet est sous licence MIT. Consultez le fichier LICENSE pour plus d'informations.
