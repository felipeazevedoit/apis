using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebPixCoreIn.Models
{
    public class WebPixCoreInContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WebPixCoreInContext() : base("name=WebPixCoreInContext")
        {
        }

        public System.Data.Entity.DbSet<WebPixCoreIn.Models.ClienteViewModel> ClienteViewModels { get; set; }

        public System.Data.Entity.DbSet<WebPixCoreIn.Models.MotorAuxViewModel> MotorAuxViewModels { get; set; }

        public System.Data.Entity.DbSet<WebPixCoreIn.Models.AcaoViewModel> AcaoViewModels { get; set; }

        public System.Data.Entity.DbSet<WebPixCoreIn.Models.ParametroViewModel> ParametroViewModels { get; set; }

        public System.Data.Entity.DbSet<WebPixCoreIn.Models.TipoAcaoViewModel> TipoAcaoViewModels { get; set; }
    }
}
