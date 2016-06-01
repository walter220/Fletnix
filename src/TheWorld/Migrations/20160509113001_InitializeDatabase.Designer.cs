using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using TheWorld.Models;

namespace TheWorld.Migrations
{
    [DbContext(typeof(FletnixContext))]
    [Migration("20160509113001_InitializeDatabase")]
    partial class InitializeDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheWorld.Models.Customer", b =>
                {
                    b.Property<string>("customer_mail_address")
                        .HasAnnotation("MaxLength", 255)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.Property<string>("paypal_account")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.Property<DateTime?>("subscription_end")
                        .HasAnnotation("Relational:ColumnType", "date");

                    b.Property<DateTime>("subscription_start")
                        .HasAnnotation("Relational:ColumnType", "date");

                    b.HasKey("customer_mail_address");

                    b.HasIndex("paypal_account")
                        .IsUnique()
                        .HasAnnotation("Relational:Name", "AK_Customer_Paypal");
                });

            modelBuilder.Entity("TheWorld.Models.Genre", b =>
                {
                    b.Property<string>("genre_name")
                        .HasAnnotation("MaxLength", 50)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.Property<string>("description")
                        .HasAnnotation("MaxLength", 255)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.HasKey("genre_name");
                });

            modelBuilder.Entity("TheWorld.Models.Movie", b =>
                {
                    b.Property<int>("movie_id");

                    b.Property<string>("URL")
                        .HasAnnotation("MaxLength", 255)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.Property<byte[]>("cover_image")
                        .HasAnnotation("Relational:ColumnType", "image");

                    b.Property<string>("description")
                        .HasAnnotation("MaxLength", 255)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.Property<int?>("duration");

                    b.Property<int?>("minimum_age");

                    b.Property<int?>("previous_part");

                    b.Property<decimal>("price")
                        .HasAnnotation("Relational:ColumnType", "numeric");

                    b.Property<int?>("publication_year");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.HasKey("movie_id");
                });

            modelBuilder.Entity("TheWorld.Models.Movie_Cast", b =>
                {
                    b.Property<int>("movie_id");

                    b.Property<int>("person_id");

                    b.Property<string>("role")
                        .HasAnnotation("MaxLength", 255)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.HasKey("movie_id", "person_id", "role");
                });

            modelBuilder.Entity("TheWorld.Models.Movie_Director", b =>
                {
                    b.Property<int>("movie_id");

                    b.Property<int>("person_id");

                    b.HasKey("movie_id", "person_id");
                });

            modelBuilder.Entity("TheWorld.Models.Movie_Genre", b =>
                {
                    b.Property<int>("movie_id");

                    b.Property<string>("genre_name")
                        .HasAnnotation("MaxLength", 50)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.HasKey("movie_id", "genre_name");
                });

            modelBuilder.Entity("TheWorld.Models.Person", b =>
                {
                    b.Property<int>("person_id");

                    b.Property<string>("firstname")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.Property<string>("gender")
                        .HasAnnotation("MaxLength", 1)
                        .HasAnnotation("Relational:ColumnType", "char");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.HasKey("person_id");
                });

            modelBuilder.Entity("TheWorld.Models.Watchhistory", b =>
                {
                    b.Property<int>("movie_id");

                    b.Property<string>("customer_mail_address")
                        .HasAnnotation("MaxLength", 255)
                        .HasAnnotation("Relational:ColumnType", "varchar");

                    b.Property<DateTime>("watch_date")
                        .HasAnnotation("Relational:ColumnType", "date");

                    b.Property<bool>("invoiced");

                    b.Property<decimal>("price")
                        .HasAnnotation("Relational:ColumnType", "numeric");

                    b.HasKey("movie_id", "customer_mail_address", "watch_date");
                });

            modelBuilder.Entity("TheWorld.Models.Movie", b =>
                {
                    b.HasOne("TheWorld.Models.Movie")
                        .WithMany()
                        .HasForeignKey("previous_part");
                });

            modelBuilder.Entity("TheWorld.Models.Movie_Cast", b =>
                {
                    b.HasOne("TheWorld.Models.Movie")
                        .WithMany()
                        .HasForeignKey("movie_id");

                    b.HasOne("TheWorld.Models.Person")
                        .WithMany()
                        .HasForeignKey("person_id");
                });

            modelBuilder.Entity("TheWorld.Models.Movie_Director", b =>
                {
                    b.HasOne("TheWorld.Models.Movie")
                        .WithMany()
                        .HasForeignKey("movie_id");

                    b.HasOne("TheWorld.Models.Person")
                        .WithMany()
                        .HasForeignKey("person_id");
                });

            modelBuilder.Entity("TheWorld.Models.Movie_Genre", b =>
                {
                    b.HasOne("TheWorld.Models.Genre")
                        .WithMany()
                        .HasForeignKey("genre_name");

                    b.HasOne("TheWorld.Models.Movie")
                        .WithMany()
                        .HasForeignKey("movie_id");
                });

            modelBuilder.Entity("TheWorld.Models.Watchhistory", b =>
                {
                    b.HasOne("TheWorld.Models.Customer")
                        .WithMany()
                        .HasForeignKey("customer_mail_address");

                    b.HasOne("TheWorld.Models.Movie")
                        .WithMany()
                        .HasForeignKey("movie_id");
                });
        }
    }
}
