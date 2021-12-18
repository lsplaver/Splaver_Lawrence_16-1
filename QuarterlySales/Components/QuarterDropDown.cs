using Microsoft.AspNetCore.Mvc;
using QuarterlySales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySales.Components
{
    public class QuarterDropDown : ViewComponent
    {
        public IViewComponentResult Invoke(string selectedValue)
        {
            int[] quarter = { 1, 2, 3, 4 };
            Dictionary<string, string> quarterDictionary = new Dictionary<string, string>();

            for (int x = 0, y = 1; x < quarter.Count(); x++, y++)
            {
                quarterDictionary.Add(y.ToString(), quarter.ElementAt(x).ToString());
            }

            DropDownViewModel vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = SalesGridDTO.DefaultFilter,
                Items = quarterDictionary
            };

            return View(SharedPath.Select, vm);
        }
    }
}
