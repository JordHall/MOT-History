using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOT_History.Data;
using MOT_History.Models;

namespace MOT_History.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private DataService DataService;

        public Car Car;
        public string MOTExpiry;
        public string Mileage;
        public string Error;
        public string DaysRemaining;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            DataService = new DataService();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        //Post form to API
        public async Task<IActionResult> OnPostAsync(string reg)
        {
            //Err handle
            if (String.IsNullOrWhiteSpace(reg)) { Error = "Error Empty Field: A UK registration number plate is required."; return Page(); }
            if (reg.Length > 7) { Error = "Error Large Value: Registration number plate was too long."; return Page(); }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var GetMOT = await DataService.GetMOTHistory(reg); //API Service call
                if (GetMOT == null)
                {
                    Error = "Error Invalid Plate: The registration number plate entered is invalid or has no history.";
                    return Page();
                }
                //Set Model variables
                Car = GetMOT.FirstOrDefault();
                MOTExpiry = Car.motTests.FirstOrDefault().expiryDate;
                Mileage = Car.motTests.FirstOrDefault().odometerValue + " " + Car.motTests.FirstOrDefault().odometerUnit;
                DaysRemaining = CalcMOTDays(MOTExpiry);
            }
            catch (Exception e)
            {
                Error = "Unhandled Error: An Unexpected Error Occured";
                return Page();
            }
            return Page();
        }

        //Calc MOT days left
        public string CalcMOTDays(string date)
        {
            DateTime Date = DateTime.Parse(date);
            var remaining = (Date - DateTime.Now).Days;
            if (remaining <= 0) return "MOT EXPIRED";
            return "Valid for " + (Date - DateTime.Now).Days.ToString() + " Days";
        }
    }
}
