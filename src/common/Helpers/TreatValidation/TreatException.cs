using System;

namespace TServices.Comum.Helpers.TreatValidation
{
    public class TreatException : Exception
    {
        public TreatException(string message) : base(message)
        {
        }

        public string MessageFriendly()
        {
            return "Ocorreu um erro Inesperado";
        }
    }
}