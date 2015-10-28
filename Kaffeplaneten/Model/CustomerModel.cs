using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten.Models
{
    public class CustomerModel
    {


        [Display(Name = "Skriv fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"[A-ZÆØÅ][a-zæøå ,.'-]{2,50}", ErrorMessage = "Du må ha stor forbokstav og fornavnet må være lengre enn 3 bokstaver. Max-lengde er 50 ")]
        public string firstName { get; set; }

        [Display(Name = "Skriv Etternavn:")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"[A-ZÆØÅ][a-zæøå]{1,50}", ErrorMessage = "Du må ha stor forbokstav og etternavnet må være lengre enn 2 bokstaver. Max-lengde er 50 ")]
        public string lastName { get; set; }

        [Display(Name = "Skriv Brukernavn(Epost)")]
        [Required(ErrorMessage = "Epost må oppgis")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Feil ved opprettelse av epost. Eksempel på struktur: 'Ola@nordmann.no'")]
        public string email { get; set; }

        [Required(ErrorMessage = "Passord må oppgis")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Passordet må inneholde minimum 8 tegn")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")]
        [Display(Name = "Skriv inn et passord")]
        public string password { get; set; }
        public bool passwordStrength { get; set; }

        [Display(Name = "Gjenta valgt passord")]
        [Required(ErrorMessage = "Passordet må gjentas!")]
        [CompareAttribute("password", ErrorMessage = "Passordene er ikke like")]
        public string passwordVerifier { get; set; }


        [Display(Name = "Skriv Telefon")]
        [Required(ErrorMessage = "Telefon må oppgis")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "Telefonnummeret må være 8 siffer langt")]
        public string phone { get; set; }

        public int customerID { get; set; }

        [Display(Name = "Skriv postnummer")]
        [Required(ErrorMessage = "Postnummer må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnummeret må være 4 siffer langt")]
        public string zipCode { get; set; }

        [Display(Name = "Skriv poststed:")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public string province { get; set; }

        [Display(Name = "Skriv adresse:")]
        [Required(ErrorMessage = "Adresse må oppgis")]
        public string adress { get; set; }

        [Display(Name = "Samme levering og betalingsadresse?")]
        public bool sameAdresses { get; set; }

        [Display(Name = "Skriv Betalingsadresse")]
        [Required(ErrorMessage = "Betalingsadresse må oppgis")]
        public string payAdress { get; set; }

        [Display(Name = "Skriv Bet. Postnummer")]
        [Required(ErrorMessage = "Betalingspostnummer må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnummeret må være 4 siffer")]
        public string payZipcode { get; set; }

        [Display(Name = "Skriv Bet. Poststed")]
        [Required(ErrorMessage = "Betalingspoststed må oppgis")]
        public string payProvince { get; set; }
    }
}
    
