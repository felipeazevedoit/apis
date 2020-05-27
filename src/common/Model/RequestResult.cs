using System.Collections.Generic;

namespace TServices.Comum.Model
{
    public class RequestResult<TEntity> where TEntity : class
    {
        public RequestResult()
        {
            ListEntities = new List<TEntity>();
            Success = true;
        }

        public TEntity Entity { get; set; }
        public IEnumerable<TEntity> ListEntities { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}