using WebPixPrincipalAPI.Model;
using WebPixPrincipalRepository.Entity;


namespace CustomExtensions
{
    using System;
    //Extension methods must be defined in a static class
    public static class ConvertExtension
    {
        public static UsuarioViewModel ToUsuarioViewModel(this Usuario usuario)
        {
            UsuarioViewModel userViewModel = new UsuarioViewModel();

            userViewModel.ID = usuario.ID;
            userViewModel.Nome = usuario.Nome;
            userViewModel.Descricao = usuario.Descricao;
            userViewModel.DataCriacao = usuario.DataCriacao;
            userViewModel.DateAlteracao = usuario.DateAlteracao;
            userViewModel.UsuarioCriacao = usuario.UsuarioEdicao;
            userViewModel.UsuarioEdicao = usuario.UsuarioEdicao;
            userViewModel.Ativo = usuario.Ativo;
            userViewModel.Status = usuario.Status;
            userViewModel.idCliente = usuario.idCliente;

            userViewModel.Login = usuario.Login;
            userViewModel.SobreNome = usuario.SobreNome;
            userViewModel.Email = usuario.Email;
            //userViewModel.PerfilUsuario = usuario.PerfilUsuario;
            userViewModel.Senha = usuario.Senha;

            return userViewModel;
        }
    }
}

