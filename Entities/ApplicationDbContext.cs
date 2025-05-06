using Entities.Domain;
using Entities.Identities;
using Entities.Seeders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ApplicationDbContext : IdentityDbContext<UserApplication,RoleApplication,Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region RelationShips
            builder.Entity<Member>()
                .HasOne(u => u.User)
                .WithOne(m => m.Member)
                .HasForeignKey<Member>(u => u.UserId);

            builder.Entity<Member>()
                .HasOne(m => m.Membership)
                .WithMany(m => m.Members)
                .HasForeignKey(m => m.MembershipId);

            builder.Entity<Trainer>()
                .HasOne(u => u.User)
                .WithOne(t => t.Trainer)
                .HasForeignKey<Trainer>(u => u.UserId);


            builder.Entity<Payment>()
                .HasOne(m => m.Member)
                .WithMany(p => p.Payments)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WorkoutPlan>()
                .HasOne(t => t.Trainer)
                .WithMany(w => w.WorkoutPlans)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkoutExercise>()
                .HasOne(w => w.WorkoutPlan)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(w => w.WorkoutPlanId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Attendance>()
                .HasOne(m => m.Member)
                .WithMany(a => a.Attendances)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Class>()
                .HasOne(t => t.Trainer)
                .WithMany(c => c.Classes)
                .HasForeignKey(t => t.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(c => c.Class)
                .WithMany(b => b.Bookings)
                .HasForeignKey(c => c.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(m => m.Member)
                .WithMany(b => b.Bookings)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Property Configuration
            builder.Entity<UserApplication>()
                .Property(d => d.DisplayName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Entity<UserApplication>()
                .Property(a => a.Address)
                .HasMaxLength(70)
                .IsRequired();

            builder.Entity<Member>()
                .Property(d => d.DateOfBirth)
                .HasColumnType("date")
                .IsRequired();

            builder.Entity<Trainer>()
                .Property(s => s.Specialties)
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Trainer>()
                .Property(c => c.Certifications)
                .HasMaxLength(250)
                .IsRequired();

            builder.Entity<Membership>()
                .Property(n => n.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<Membership>()
                .Property(p => p.Price)
                .HasPrecision(10,3)
                .IsRequired();

            builder.Entity<Membership>()
                .Property(d => d.DurationInDays)
                .HasDefaultValue(1)
                .IsRequired();

            builder.Entity<Payment>()
                .Property(a => a.Amount)
                .HasPrecision(10, 3)
                .IsRequired();

            builder.Entity<Payment>()
                .Property(d => d.PaymentDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Entity<WorkoutPlan>()
                .Property(w => w.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<WorkoutPlan>()
                .Property(d => d.Description)
                .HasMaxLength(250)
                .IsRequired();

            builder.Entity<WorkoutExercise>()
                .Property(e => e.ExerciseName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<WorkoutExercise>()
                .Property(s => s.Sets)
                .HasDefaultValue(3)
                .IsRequired();

             builder.Entity<WorkoutExercise>()
                .Property(s => s.Reps)
                .HasDefaultValue(12)
                .IsRequired();

            builder.Entity<Attendance>()
                .Property(c => c.CheckInTime)
                .HasColumnType("date")
                .IsRequired();

            builder.Entity<Attendance>()
                .Property(c => c.CheckOutTime)
                .HasColumnType("date")
                .IsRequired();

            builder.Entity<Class>()
                .Property(c => c.ClassName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<Class>()
                .Property(c => c.Capacity)
                .HasDefaultValue(10)
                .IsRequired();

            builder.Entity<Booking>()
                .Property(d => d.BookingDate)
                .HasColumnType("date")
                .IsRequired();

            #endregion

            #region Seeders
            MembershipSeeder.SeedMembership(builder);

            #endregion
        }

        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Booking> Booking {  get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercise { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlan { get; set; }
    }
}
