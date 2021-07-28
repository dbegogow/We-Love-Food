using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WeLoveFood.Areas.Identity.Pages.Account.Infrastructure
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<string> ModelStateErrorMessages(
            this ModelStateDictionary dictionary)
            => dictionary
                .Where(ms => ms.Key.Contains("#"))
                .SelectMany(me => me.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
    }
}
