using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class EmployeeModel
    {
        [Display(Name = "Fornavn: ")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"[A-ZÆØÅ][a-zæøå ,.'-]{2,50}", ErrorMessage = "Du må ha stor forbokstav og fornavnet må være lengre enn 3 bokstaver. Max-lengde er 50 ")]
        public string firstName { get; set; }

        [Display(Name = "Etternavn: ")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"[A-ZÆØÅ][a-zæøå]{1,50}", ErrorMessage = "Du må ha stor forbokstav og etternavnet må være lengre enn 2 bokstaver. Max-lengde er 50 ")]
        public string lastName { get; set; }

        [Display(Name = "Brukernavn(Epost): ")]
        [Required(ErrorMessage = "Epost må oppgis")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Feil ved opprettelse av epost. Eksempel på struktur: 'Ola@nordmann.no'")]
        public string email { get; set; }

        [Required(ErrorMessage = "Passord må oppgis")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Passordet må inneholde minimum 8 tegn")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")]
        [Display(Name = "Passord:")]
        public string password { get; set; }

        public bool passwordStrength { get; set; }

        [Display(Name = "Gjenta valgt passord")]
        [Required(ErrorMessage = "Passordet må gjentas!")]
        [CompareAttribute("password", ErrorMessage = "Passordene er ikke like")]
        public string passwordVerifier { get; set; }


        [Display(Name = "Telefon: ")]
        [Required(ErrorMessage = "Telefon må oppgis")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "Telefonnummeret må være 8 siffer langt")]
        public string phone { get; set; }

        public int employeeID { get; set; }

        public bool employeeAdmin { get; set; }

        public bool databaseAdmin { get; set; }
        public bool productAdmin { get; set; }
        public bool customerAdmin { get; set; }
    }
}
