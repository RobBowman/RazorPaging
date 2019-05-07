using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AppInsightsSimpleDemo.Web.Pages
{
    public class CalculatorModel : PageModel
    {
        private readonly ILogger _logger;

        public CalculatorModel(ILogger<CalculatorModel> logger)
        {
            _logger = logger;
        }

        [TempData]
        public string ResultInfo { get; set; }

        [Required]
        [BindProperty]
        public double Weight { get; set; }

        [Required]
        [BindProperty]
        public double Height { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostCalculateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = (Weight / ((Height / 100) * (Height / 100)));

            if (result < 18.5)
            {
                ResultInfo = $"Underweight (BMI: {result})! Time to eat a few donuts!";
            }
            else if (result < 24.9)
            {
                ResultInfo = $"Normal weight (BMI: {result})! It's OK to eat a few donuts!";
            }
            else if (result < 29.9)
            {
                ResultInfo = $"Overweight (BMI: {result})! But have you heard of anybody who died because of a donut?";
            }
            else
            {
                ResultInfo = $"Obese (BMI: {result})! OK, just one donut, right?";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostTellPrettyAsync()
        {
            ResultInfo = "You are the prettiest person in the world! Time for a donut and calculating your BMI!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostThrowExceptionAsync()
        {
            //throw new Exception("I meant to throw this");

            // All the following logs will be picked up by Application Insights.
            // and all of them will have ("MyKey", "MyValue") in Properties.
            using (_logger.BeginScope(new Dictionary<string, object> { { "MyKey", "MyValue" } }))
            {
                _logger.LogWarning("An example of a Warning trace..");
                _logger.LogError("An example of an Error level message");
            }

            int x = 50;
            int y = 0;
            int z;

            z = x / y;

            ResultInfo = "Exception thrown - check app insights";
            return RedirectToPage();
        }
    }
}