﻿using Academy.Application.ViewModels.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace Academy.Application.ViewModels
{
    public class UserUpdatePasswordViewModel : Entity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MaxLength(100, ErrorMessage = "A senha pode ter no máximo 100 caracteres")]
        [MinLength(6, ErrorMessage = "A senha precisa conter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmação de senha é obrigatório")]
        [Compare("Password", ErrorMessage = "Senha e Confirmação de senha devem ser iguais")]
        public string ConfirmPassword { get; set; }

        public bool IsVerified { get; set; }
    }
}
