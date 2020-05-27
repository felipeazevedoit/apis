using System;
using System.Collections.Generic;
using System.Text;

namespace TServices.Comum.Extensions
{
    public static class DateTimeExtension
    {
        public static bool MaiorIdade (this DateTime? value)
        {
            var today = DateTime.Now;
            var idade = today.Year - value?.Year;

            if (!idade.HasValue)
                return false;

            if (value > today.AddYears(-idade.Value)) idade--;

            if (idade < 18)
                return false;

            return true;
        }
    }
}
