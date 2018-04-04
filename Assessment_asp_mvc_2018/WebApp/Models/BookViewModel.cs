using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Informe o título")]
        public string Title { get; set; }

        [Display(Name = "Indentificação")]
        [Required(ErrorMessage = "Informe a indentificação do livro")]
        public string Isbn { get; set; }

        [Display(Name = "Ano")]
        public int Year { get; set; }

        [Display(Name = "Autor(es)")]
        public List<AuthorViewModel> Authors { get; set; }
    }
}