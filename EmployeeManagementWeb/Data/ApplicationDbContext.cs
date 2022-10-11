﻿using EmployeeManagementWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
