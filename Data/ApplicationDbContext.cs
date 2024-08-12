
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("TaskManagementDbConn")
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }

        public virtual DbSet<Tasks> Tasks { get; set; }


    }
}