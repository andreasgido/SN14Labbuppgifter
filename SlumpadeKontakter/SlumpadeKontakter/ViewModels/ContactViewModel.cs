using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SlumpadeKontakter.ViewModels
{
    public class ContactViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Förnamn måste anges")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Namnet måste innehålla minst 2 tecken"))]
        [DisplayName("Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn måste anges")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ("Namnet måste innehålla minst 2 tecken"))]
        [DisplayName("Efternamn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Epost måste anges")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = ("Epost-addressen måste innehålla minst 6 tecken"))]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Ange en giltig epost-address")]
        [DisplayName("Epost")]
        public string Email { get; set; }
    }
}
