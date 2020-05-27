using System;

namespace TServices.Comum.Helpers.Message
{
    public static class MessagesDefaultResponse
    {
        public static string MessageInvalid(string value)
        {
            return $"{value} inválido.";
        }

        public static string MessageRegisterRemovedSuccess(string value)
        {
            return $"{value} removido(a) com sucesso";
        }

        public static string MessageRegisterInsertSuccess(string value)
        {
            return $"{value} inserido(a) com sucesso";
        }

        public static string MessageRegisterUpdatedSuccess(string value)
        {
            return $"{value} atualizado(a) com sucesso";
        }

        public static string MessageRequiredField(string nameField)
        {
            return $"O campo {nameField} é obrigatório !";
        }

        public static string MessageExistData(string nameField)
        {
            return $"{nameField} já existe !";
        }

        public static string MessageNotExistData(string nameField)
        {
            return $"{nameField} não existe !";
        }

        public static string MessageDefaultError() => "Ocorreu um erro inesperado";
    }
}