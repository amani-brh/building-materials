using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace building_materials.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacteurTransports",
                columns: table => new
                {
                    IdMoyenTransport = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FacteurCO2 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacteurTransports", x => x.IdMoyenTransport);
                });

            migrationBuilder.CreateTable(
                name: "Producteurs",
                columns: table => new
                {
                    IdProducteur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LieuProduction = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producteurs", x => x.IdProducteur);
                });

            migrationBuilder.CreateTable(
                name: "Provenances",
                columns: table => new
                {
                    IdProvenance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pays = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DistanceKm = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provenances", x => x.IdProvenance);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    IdType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.IdType);
                });

            migrationBuilder.CreateTable(
                name: "Familles",
                columns: table => new
                {
                    IdFamille = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFamille = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Familles", x => x.IdFamille);
                    table.ForeignKey(
                        name: "FK_Familles_Types_IdType",
                        column: x => x.IdType,
                        principalTable: "Types",
                        principalColumn: "IdType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materiaux",
                columns: table => new
                {
                    IdMateriau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Origine = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdFamille = table.Column<int>(type: "int", nullable: false),
                    IdProvenance = table.Column<int>(type: "int", nullable: false),
                    IdProducteur = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiaux", x => x.IdMateriau);
                    table.ForeignKey(
                        name: "FK_Materiaux_Familles_IdFamille",
                        column: x => x.IdFamille,
                        principalTable: "Familles",
                        principalColumn: "IdFamille",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materiaux_Producteurs_IdProducteur",
                        column: x => x.IdProducteur,
                        principalTable: "Producteurs",
                        principalColumn: "IdProducteur",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materiaux_Provenances_IdProvenance",
                        column: x => x.IdProvenance,
                        principalTable: "Provenances",
                        principalColumn: "IdProvenance",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaracteristiqueEnvironnementales",
                columns: table => new
                {
                    IdCaract = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMateriau = table.Column<int>(type: "int", nullable: false),
                    EmissionCO2 = table.Column<double>(type: "float", nullable: false),
                    PollutionEau = table.Column<double>(type: "float", nullable: false),
                    PollutionAir = table.Column<double>(type: "float", nullable: false),
                    ConsommationEau = table.Column<double>(type: "float", nullable: false),
                    UniteFonctionnelle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UtilisationNetteEauDouce = table.Column<double>(type: "float", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristiqueEnvironnementales", x => x.IdCaract);
                    table.ForeignKey(
                        name: "FK_CaracteristiqueEnvironnementales_Materiaux_IdMateriau",
                        column: x => x.IdMateriau,
                        principalTable: "Materiaux",
                        principalColumn: "IdMateriau",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    IdTransport = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMateriau = table.Column<int>(type: "int", nullable: false),
                    IdMoyenTransport = table.Column<int>(type: "int", nullable: false),
                    DistanceKm = table.Column<int>(type: "int", nullable: false),
                    EmissionCO2 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.IdTransport);
                    table.ForeignKey(
                        name: "FK_Transports_FacteurTransports_IdMoyenTransport",
                        column: x => x.IdMoyenTransport,
                        principalTable: "FacteurTransports",
                        principalColumn: "IdMoyenTransport",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_Materiaux_IdMateriau",
                        column: x => x.IdMateriau,
                        principalTable: "Materiaux",
                        principalColumn: "IdMateriau",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaracteristiqueEnvironnementales_IdMateriau",
                table: "CaracteristiqueEnvironnementales",
                column: "IdMateriau");

            migrationBuilder.CreateIndex(
                name: "IX_Familles_IdType",
                table: "Familles",
                column: "IdType");

            migrationBuilder.CreateIndex(
                name: "IX_Materiaux_IdFamille",
                table: "Materiaux",
                column: "IdFamille");

            migrationBuilder.CreateIndex(
                name: "IX_Materiaux_IdProducteur",
                table: "Materiaux",
                column: "IdProducteur");

            migrationBuilder.CreateIndex(
                name: "IX_Materiaux_IdProvenance",
                table: "Materiaux",
                column: "IdProvenance");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_IdMateriau",
                table: "Transports",
                column: "IdMateriau");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_IdMoyenTransport",
                table: "Transports",
                column: "IdMoyenTransport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaracteristiqueEnvironnementales");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "FacteurTransports");

            migrationBuilder.DropTable(
                name: "Materiaux");

            migrationBuilder.DropTable(
                name: "Familles");

            migrationBuilder.DropTable(
                name: "Producteurs");

            migrationBuilder.DropTable(
                name: "Provenances");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
