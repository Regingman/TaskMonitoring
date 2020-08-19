using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskMonitoring.Data;

namespace TaskMonitoring.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListEmployee>()
                        .HasOne(m => m.Project)
                        .WithMany(t => t.ListEmployee)
                        .HasForeignKey(m => m.ProjectId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ListEmployee>()
                        .HasOne(m => m.ApplicationUser)
                        .WithMany(t => t.ListEmployee)
                        .HasForeignKey(m => m.ApplicationUserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Module>()
                        .HasOne(m => m.Project)
                        .WithMany(t => t.Module)
                        .HasForeignKey(m => m.ProjectId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Task>()
                        .HasOne(m => m.Module)
                        .WithMany(t => t.Task)
                        .HasForeignKey(m => m.ModuleId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Task>()
                        .HasOne(m => m.ApplicationUser)
                        .WithMany(t => t.Task)
                        .HasForeignKey(m => m.ApplicationUserId)
                        .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

        }



        public virtual DbSet<TaskMonitoring.Data.ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Module> Modules { get; set; }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<ListEmployee> ListEmployees { get; set; }
    }


    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ListEmployee> ListEmployee { get; set; }
        public virtual ICollection<Task> Task { get; set; }

    }

    public class Project
    {

        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Дата завершения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<ListEmployee> ListEmployee { get; set; }

        public virtual ICollection<Module> Module { get; set; }

    }

    public class ListEmployee
    {
        public int Id { get; set; }
        public String ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }

    public class Module
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [Display(Name = "Дата завершения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Task> Task { get; set; }
    }

    public class Task
    {
        public int Id { get; set; }
        [Display(Name = "Описание")]
        public string Name { get; set; }
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
        public String ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Display(Name = "Дата завершения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
    }


}

