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
        public string ImageURL { get; set; }

        public string Alt { get; set; }

        public static IHtmlContent EnumDropDownListFor<TModel, TEnum>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, TEnum model, string optionLabel = null, object htmlAttributes = null)
        {
            var enumType = Nullable.GetUnderlyingType(typeof(TEnum)) ?? typeof(TEnum);
            var values = Enum.GetValues(enumType);
            var items = new List<SelectListItem>();

            if (optionLabel != null)
            {
                items.Add(new SelectListItem { Text = optionLabel, Value = string.Empty });
            }

            foreach (var value in values)
            {
                items.Add(new SelectListItem
                {
                    Text = Enum.GetName(enumType, value),
                    Value = value.ToString(),
                    Selected = value.Equals(model)
                });
            }

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }


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
