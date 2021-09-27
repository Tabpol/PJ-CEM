using Microsoft.EntityFrameworkCore;
using Nestle_service_api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nestle_service_api.Context
{
    public class Nestle_Connect : DbContext
    {

        public Nestle_Connect(DbContextOptions<Nestle_Connect> options) : base(options)
        {

        }
        public DbSet<tb_title> tb_title { get; set; }
        public DbSet<tb_sex> tb_sexs { get; set; }
        public DbSet<tb_age_range> tb_age_range { get; set; }
        public DbSet<tb_members> tb_members { get; set; }
        public DbSet<tb_animal_type> tb_animal_type { get; set; }
        public DbSet<tb_master_link> tb_master_link { get; set; }
        public DbSet<tb_RegisterHeading> tb_RegisterHeading { get; set; }
        public DbSet<tb_RegisterDetail> tb_RegisterDetail { get; set; }
        public DbSet<tb_province> tb_province { get; set; }
        public DbSet<tb_district> tb_district { get; set; }
        public DbSet<tb_sub_district> tb_sub_district { get; set; }
        public DbSet<tb_zip_code> tb_zip_code { get; set; }
        public DbSet<tb_user_login> tb_user_login { get; set; }
        public DbSet<tb_segment> tb_segment { get; set; }
        public DbSet<tb_logs_inbound> tb_logs_inbound { get; set; }
        public DbSet<tb_logs_outbound> tb_logs_outbound { get; set; }
        public DbSet<tb_inbound_case> tb_inbound_case { get; set; }
        public DbSet<tb_outbound_frist_call> tb_outbound_frist_call { get; set; }
        public DbSet<tb_outbound_second_call> tb_outbound_second_call { get; set; }
        public DbSet<tb_outbound_telesale> tb_outbound_telesale { get; set; }
    }

}
