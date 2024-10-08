﻿using System.ComponentModel.DataAnnotations;

namespace P5_ExpressVoiture.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Le nom du rôle est requis.")]
        [Display(Name  = "Nom du rôle")]
        public string NomRole { get; set; }
    }
}
