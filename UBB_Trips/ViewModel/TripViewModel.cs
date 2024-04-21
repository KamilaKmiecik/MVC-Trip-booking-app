using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using UBB_Trips.Models;

namespace UBB_Trips.ViewModels
{

    public class TripViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Start Date")]
        public DateTime TripSTART { get; set; }

        [Display(Name = "End Date")]
        public DateTime TripEND { get; set; }

        public double Days { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }


        [Display(Name = "Trip Type")]
        public TripTypeModel Type { get; set; }

        [Display(Name = "Food and Accommodation")]
        public FoodAcoomodationModel Food { get; set; }

        public CountryModel Country { get; set; }


        [Display(Name = "Image")]
        public string? ImageURL { get; set; }

        public string? Alt { get; set; }

    }

    public class TripTypeViewModel
    {
        public string Name { get; set; }
    }

    public class FoodAcoomodationViewModel
    {
        public string Name { get; set; }
    }

    public class CountryViewModel
    {
        public string Name { get; set; }
    }
}
