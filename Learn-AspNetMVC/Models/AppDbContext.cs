﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Learn_AspNetMVC.Models.Contacts;

namespace Learn_AspNetMVC.Models
{
    public class AppDbContext : DbContext
	{

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			//{
			//	var tableName = entityType.GetTableName();
			//	if (tableName.StartsWith("AspNet"))
			//	{
			//		entityType.SetTableName(tableName.Substring(6));
			//	}
			//}
		}
		public DbSet<ContactModel> Contacts { get; set; }
	}
}
