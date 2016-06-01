using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace TheWorld.Migrations
{
    public partial class IdentityEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
//            migrationBuilder.DropForeignKey(name: "FK_Movie_Cast_Movie_movie_id", table: "Movie_Cast");
//            migrationBuilder.DropForeignKey(name: "FK_Movie_Cast_Person_person_id", table: "Movie_Cast");
//            migrationBuilder.DropForeignKey(name: "FK_Movie_Director_Movie_movie_id", table: "Movie_Director");
//            migrationBuilder.DropForeignKey(name: "FK_Movie_Director_Person_person_id", table: "Movie_Director");
//            migrationBuilder.DropForeignKey(name: "FK_Movie_Genre_Genre_genre_name", table: "Movie_Genre");
//            migrationBuilder.DropForeignKey(name: "FK_Movie_Genre_Movie_movie_id", table: "Movie_Genre");
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
//            migrationBuilder.CreateTable(
//                name: "CustomerFeedback",
//                columns: table => new
//                {
//                    movie_id = table.Column<int>(nullable: false),
//                    customer_mail_address = table.Column<string>(type: "varchar", nullable: false),
//                    comments = table.Column<string>(type: "varchar", nullable: false),
//                    feedback_date = table.Column<DateTime>(type: "date", nullable: false),
//                    rating = table.Column<int>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_CustomerFeedback", x => new { x.movie_id, x.customer_mail_address });
//                    table.ForeignKey(
//                        name: "FK_CustomerFeedback_Customer_customer_mail_address",
//                        column: x => x.customer_mail_address,
//                        principalTable: "Customer",
//                        principalColumn: "customer_mail_address",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_CustomerFeedback_Movie_movie_id",
//                        column: x => x.movie_id,
//                        principalTable: "Movie",
//                        principalColumn: "movie_id",
//                        onDelete: ReferentialAction.Restrict);
//                });
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirsteTime = table.Column<DateTime>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FletnixUser", x => x.Id);
                });
//            migrationBuilder.CreateTable(
//                name: "MovieAwards",
//                columns: table => new
//                {
//                    movie_id = table.Column<int>(nullable: false),
//                    award = table.Column<string>(type: "varchar", nullable: false),
//                    result = table.Column<string>(type: "varchar", nullable: false),
//                    number_of_awards = table.Column<string>(type: "varchar", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_MovieAwards", x => new { x.movie_id, x.award, x.result });
//                    table.ForeignKey(
//                        name: "FK_MovieAwards_Movie_movie_id",
//                        column: x => x.movie_id,
//                        principalTable: "Movie",
//                        principalColumn: "movie_id",
//                        onDelete: ReferentialAction.Restrict);
//                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_FletnixUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_FletnixUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_FletnixUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
//            migrationBuilder.AddForeignKey(
//                name: "FK_Movie_Cast_Movie_movie_id",
//                table: "Movie_Cast",
//                column: "movie_id",
//                principalTable: "Movie",
//                principalColumn: "movie_id",
//                onDelete: ReferentialAction.Cascade);
//            migrationBuilder.AddForeignKey(
//                name: "FK_Movie_Cast_Person_person_id",
//                table: "Movie_Cast",
//                column: "person_id",
//                principalTable: "Person",
//                principalColumn: "person_id",
//                onDelete: ReferentialAction.Cascade);
//            migrationBuilder.AddForeignKey(
//                name: "FK_Movie_Director_Movie_movie_id",
//                table: "Movie_Director",
//                column: "movie_id",
//                principalTable: "Movie",
//                principalColumn: "movie_id",
//                onDelete: ReferentialAction.Cascade);
//            migrationBuilder.AddForeignKey(
//                name: "FK_Movie_Director_Person_person_id",
//                table: "Movie_Director",
//                column: "person_id",
//                principalTable: "Person",
//                principalColumn: "person_id",
//                onDelete: ReferentialAction.Cascade);
//            migrationBuilder.AddForeignKey(
//                name: "FK_Movie_Genre_Genre_genre_name",
//                table: "Movie_Genre",
//                column: "genre_name",
//                principalTable: "Genre",
//                principalColumn: "genre_name",
//                onDelete: ReferentialAction.Cascade);
//            migrationBuilder.AddForeignKey(
//                name: "FK_Movie_Genre_Movie_movie_id",
//                table: "Movie_Genre",
//                column: "movie_id",
//                principalTable: "Movie",
//                principalColumn: "movie_id",
//                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Movie_Cast_Movie_movie_id", table: "Movie_Cast");
            migrationBuilder.DropForeignKey(name: "FK_Movie_Cast_Person_person_id", table: "Movie_Cast");
            migrationBuilder.DropForeignKey(name: "FK_Movie_Director_Movie_movie_id", table: "Movie_Director");
            migrationBuilder.DropForeignKey(name: "FK_Movie_Director_Person_person_id", table: "Movie_Director");
            migrationBuilder.DropForeignKey(name: "FK_Movie_Genre_Genre_genre_name", table: "Movie_Genre");
            migrationBuilder.DropForeignKey(name: "FK_Movie_Genre_Movie_movie_id", table: "Movie_Genre");
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("CustomerFeedback");
            migrationBuilder.DropTable("MovieAwards");
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("AspNetUsers");
            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Cast_Movie_movie_id",
                table: "Movie_Cast",
                column: "movie_id",
                principalTable: "Movie",
                principalColumn: "movie_id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Cast_Person_person_id",
                table: "Movie_Cast",
                column: "person_id",
                principalTable: "Person",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Director_Movie_movie_id",
                table: "Movie_Director",
                column: "movie_id",
                principalTable: "Movie",
                principalColumn: "movie_id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Director_Person_person_id",
                table: "Movie_Director",
                column: "person_id",
                principalTable: "Person",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Genre_Genre_genre_name",
                table: "Movie_Genre",
                column: "genre_name",
                principalTable: "Genre",
                principalColumn: "genre_name",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Genre_Movie_movie_id",
                table: "Movie_Genre",
                column: "movie_id",
                principalTable: "Movie",
                principalColumn: "movie_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
