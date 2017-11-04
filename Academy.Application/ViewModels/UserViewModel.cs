﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Academy.Application.ViewModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "Seu nome pode ter no máximo 100 caracteres")]
        [MinLength(3, ErrorMessage = "Seu nome precisa conter no mínimo 3 caracteres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        [MaxLength(100, ErrorMessage = "Seu sobrenome pode ter no máximo 100 caracteres")]
        [MinLength(3, ErrorMessage = "Seu sobrenome precisa conter no mínimo 3 caracteres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MaxLength(100, ErrorMessage = "A senha pode ter no máximo 100 caracteres")]
        [MinLength(6, ErrorMessage = "A senha precisa conter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DateOfBirth { get; set; }
    }
}
