﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace WeLoveFood.Infrastructure.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetControllerName(this Type controllerType)
            => controllerType.Name.Replace(nameof(Controller), string.Empty);
    }
}
