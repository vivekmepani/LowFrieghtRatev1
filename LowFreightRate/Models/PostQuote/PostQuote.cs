using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace LowFreightRate.Models.PostQuote
{
    public class PostQuote
    {
#pragma warning disable CS0649 // Field 'PostQuote.Posted' is never assigned to, and will always have its default value
        internal DateTime Posted;
#pragma warning restore CS0649 // Field 'PostQuote.Posted' is never assigned to, and will always have its default value

        public long  ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        
        public int Number { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string commodity { get; set; }

        [Required]
        public string Dimensions { get; set; }

        [Required]
        public string Weight { get; set; }
        [MaxLength(26)]
        public string NumberOfPallets { get; set; }

        public string DeliveryTime { get; set; }

        [Required]
        public string AdditionalCargoDetails { get; set; }
        
        public ServiceType ServiceTypes { get; set; }
        public ExpeditedService ExpeditedServices { get; set; }
        public TypeofTrailer TypeofTrailers { get; set; }
        public SpecialService SpecialServices { get; set; }
       
        
        public FreightService FreightServices { get; set; }
    }
    public enum FreightService
    {
        
        [Display(Name = "Ocean Freight Shipping")]
        OceanFreight_Shipping,
        [Display(Name = "FTL Freight Shipping")]
        FTLFreight_Shipping,
        [Display(Name = "LTL Freight Shipping")]
        LTLFreight_Shipping,
        [Display(Name = "Intermodal Freight Shipping")]
        IntermodalFreight_Shipping,

    }
    public enum SpecialService
    {
        Not_Applicable,
        [Display(Name = "Residential")]
        Residential,
        [Display(Name = "Power Tailgate")]
        Power_Tailgate,
        [Display(Name = "Tarp")]
        Tarp,
        [Display(Name = "Temperature Control")]
        TemperatureControl,

    }
    public enum TypeofTrailer
    {
        Not_Applicable,
        [Display(Name = "Flat Deck")]
        Flat_Deck,
        [Display(Name = "Dry Van")]
        Dry_Van,
       
    }
    public enum ServiceType
    {
        Not_Applicable,
        [Display(Name = "Door to Door")]
        Door_To_Door,
        [Display(Name = "Port to Port")]
        Port_to_Port,
        [Display(Name = "Door to Port")]
        Door_to_Port,
        [Display(Name = "Port to Door")]
        Port_to_Door,

    }
    public enum ExpeditedService
    {
        Not_Applicable,
        Yes,
        No,

    }
}
