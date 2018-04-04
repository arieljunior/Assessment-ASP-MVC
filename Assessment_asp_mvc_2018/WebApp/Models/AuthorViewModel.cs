using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Nome")]
        [Required(ErrorMessage = "Informe seu nome")]
        [StringLength(10, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(10, MinimumLength = 4)]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o seu email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }
    }
}