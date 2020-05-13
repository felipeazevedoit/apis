namespace SegurancaBO
{
    public static class LogBo
    {
        public static void Send(string acao,string url,int  idUsuario,int  idCliente, int tokenId, string ip)
        {
            var log = new Entity.Log
            {
                Descricao = url,
                Nome = acao,
                TokenId = tokenId,
                idCliente = idCliente,
                IdUsuario = idUsuario,
                Ip = ip
            };

            Repository.Log.Save(log);

        }
    }
}
