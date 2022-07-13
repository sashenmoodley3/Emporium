using Emporium.POS.API.Models.Authorization;
using Emporium.POS.API.Models.Business;
using Emporium.POS.API.Models.SKU;
using Emporium.POS.API.Models.User;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emporium.POS.API.DBContext
{
    public class WebApiDBContext : DbContext
    {
        public WebApiDBContext(DbContextOptions<WebApiDBContext> options) : base(options) { }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserSecurityInfo> UserSecurityInfo { get; set; }
        public DbSet<BusinessInfo> BusinessInfo { get; set; }
        public DbSet<SKUDetails> SKUDetails { get; set; }
        public DbSet<ModifyKey> ModifyKey { get; set; }
    }
}
