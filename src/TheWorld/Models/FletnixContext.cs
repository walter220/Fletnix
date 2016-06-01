using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Storage;
using TheWorld.Models;

namespace TheWorld.Models
{
    public class FletnixContext : IdentityDbContext<FletnixUser>
    {
        public FletnixContext()
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=tcp:wally.database.windows.net,1433;Database=Fletnix;User ID=walter@wally;Password=Nots@666;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.customer_mail_address);

                entity.HasIndex(e => e.paypal_account).HasName("AK_Customer_Paypal").IsUnique();

                entity.Property(e => e.customer_mail_address)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.Property(e => e.paypal_account)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(e => e.subscription_end).HasColumnType("date");

                entity.Property(e => e.subscription_start).HasColumnType("date");
            });

            modelBuilder.Entity<CustomerFeedback>(entity =>
            {
                entity.HasKey(e => new { e.movie_id, e.customer_mail_address });

                entity.Property(e => e.customer_mail_address)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(e => e.comments)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(e => e.feedback_date).HasColumnType("date");

                entity.HasOne(d => d.customer_mail_addressNavigation).WithMany(p => p.CustomerFeedback).HasForeignKey(d => d.customer_mail_address).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.movie).WithMany(p => p.CustomerFeedback).HasForeignKey(d => d.movie_id).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.genre_name);

                entity.Property(e => e.genre_name)
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.Property(e => e.description)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.movie_id);

                entity.Property(e => e.movie_id).ValueGeneratedNever();

                entity.Property(e => e.cover_image).HasColumnType("image");

                entity.Property(e => e.description)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(e => e.price).HasColumnType("numeric");

                entity.Property(e => e.title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(e => e.URL)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.HasOne(d => d.previous_partNavigation).WithMany(p => p.Inverseprevious_partNavigation).HasForeignKey(d => d.previous_part);
            });

            modelBuilder.Entity<MovieAwards>(entity =>
            {
                entity.HasKey(e => new { e.movie_id, e.award, e.result });

                entity.Property(e => e.award)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(e => e.result)
                    .HasMaxLength(10)
                    .HasColumnType("varchar");

                entity.Property(e => e.number_of_awards)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.HasOne(d => d.movie).WithMany(p => p.MovieAwards).HasForeignKey(d => d.movie_id).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Movie_Cast>(entity =>
            {
                entity.HasKey(e => new { e.movie_id, e.person_id, e.role });

                entity.Property(e => e.role)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.HasOne(d => d.movie).WithMany(p => p.Movie_Cast).HasForeignKey(d => d.movie_id);

                entity.HasOne(d => d.person).WithMany(p => p.Movie_Cast).HasForeignKey(d => d.person_id);
            });

            modelBuilder.Entity<Movie_Director>(entity =>
            {
                entity.HasKey(e => new { e.movie_id, e.person_id });

                entity.HasOne(d => d.movie).WithMany(p => p.Movie_Director).HasForeignKey(d => d.movie_id);

                entity.HasOne(d => d.person).WithMany(p => p.Movie_Director).HasForeignKey(d => d.person_id);
            });

            modelBuilder.Entity<Movie_Genre>(entity =>
            {
                entity.HasKey(e => new { e.movie_id, e.genre_name });

                entity.Property(e => e.genre_name)
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.HasOne(d => d.genre_nameNavigation).WithMany(p => p.Movie_Genre).HasForeignKey(d => d.genre_name);

                entity.HasOne(d => d.movie).WithMany(p => p.Movie_Genre).HasForeignKey(d => d.movie_id);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.person_id);

                entity.Property(e => e.person_id).ValueGeneratedNever();

                entity.Property(e => e.firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");

                entity.Property(e => e.gender)
                    .HasMaxLength(1)
                    .HasColumnType("char");

                entity.Property(e => e.lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar");
            });

            modelBuilder.Entity<Watchhistory>(entity =>
            {
                entity.HasKey(e => new { e.movie_id, e.customer_mail_address, e.watch_date });

                entity.Property(e => e.customer_mail_address)
                    .HasMaxLength(255)
                    .HasColumnType("varchar");

                entity.Property(e => e.watch_date).HasColumnType("date");

                entity.Property(e => e.price).HasColumnType("numeric");

                entity.HasOne(d => d.customer_mail_addressNavigation).WithMany(p => p.Watchhistory).HasForeignKey(d => d.customer_mail_address).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.movie).WithMany(p => p.Watchhistory).HasForeignKey(d => d.movie_id).OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerFeedback> CustomerFeedback { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieAwards> MovieAwards { get; set; }
        public DbSet<Movie_Cast> Movie_Cast { get; set; }
        public DbSet<Movie_Director> Movie_Director { get; set; }
        public DbSet<Movie_Genre> Movie_Genre { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Watchhistory> Watchhistory { get; set; }
    }
}