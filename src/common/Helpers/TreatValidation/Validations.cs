using System;
using System.Text.RegularExpressions;

namespace TServices.Comum.Helpers.TreatValidation
{
    public static class Validations
    {
        public static bool ValidateCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                return true;
            }

            if (long.Parse(cnpj) == 0)
            {
                return false;
            }

            if (cnpj.Length != 14)
            {
                cnpj = $"{long.Parse(cnpj):D14}";
            }

            var calcArr = new int[14];

            for (var x = 0; x < calcArr.Length; x++)
            {
                calcArr[x] = int.Parse(cnpj[x].ToString());
            }

            var multp = 5;
            var sum = 0;

            for (var x = 0; x < 12; x++)
            {
                sum += calcArr[x] * multp;
                multp--;

                if (multp < 2)
                {
                    multp = 9;
                }
            }

            Math.DivRem(sum, 11, out var dv1);

            if (dv1 < 2)
            {
                dv1 = 0;
            }
            else
            {
                dv1 = 11 - dv1;
            }

            if (dv1 != calcArr[12])
            {
                return false;
            }

            multp = 6;
            sum = 0;

            for (var x = 0; x < 13; x++)
            {
                sum += calcArr[x] * multp;
                multp--;

                if (multp < 2)
                {
                    multp = 9;
                }
            }

            Math.DivRem(sum, 11, out var dv2);

            if (dv2 < 2)
            {
                dv2 = 0;
            }
            else
            {
                dv2 = 11 - dv2;
            }

            if (dv2 != calcArr[13])
            {
                return false;
            }

            return true;
        }

        public static bool ValidateCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return true;
            }

            if (long.Parse(cpf) == 0)
            {
                return false;
            }

            if (cpf.Length != 11)
            {
                cpf = $"{long.Parse(cpf):D11}";
            }

            var calcArr = new int[11];

            for (var x = 0; x < calcArr.Length; x++)
            {
                calcArr[x] = int.Parse(cpf[x].ToString());
            }

            var sum = 0;

            for (var x = 1; x <= 9; x++)
            {
                sum += calcArr[x - 1] * (11 - x);
            }

            Math.DivRem(sum, 11, out var dv1);
            dv1 = 11 - dv1;
            dv1 = dv1 > 9 ? 0 : dv1;

            if (dv1 != calcArr[9])
            {
                return false;
            }

            sum = 0;

            for (var x = 1; x <= 10; x++)
            {
                sum += calcArr[x - 1] * (12 - x);
            }

            Math.DivRem(sum, 11, out var dv2);
            dv2 = 11 - dv2;
            dv2 = dv2 > 9 ? 0 : dv2;

            if (dv2 != calcArr[10])
            {
                return false;
            }

            return true;
        }
    }
}