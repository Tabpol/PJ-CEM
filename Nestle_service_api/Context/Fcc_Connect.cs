using Microsoft.EntityFrameworkCore;
using Nestle_service_api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestle_service_api.Context
{
    public class Fcc_Connect : DbContext
    {
        public Fcc_Connect(DbContextOptions<Fcc_Connect> options) : base(options)
        {

        }
        public DbSet<tbMessageLog> tbMessageLog { get; set; }
        //public DbSet<sp_o_getAddress_Province> sp_o_getAddress_Province { get; set; }

    }
}
