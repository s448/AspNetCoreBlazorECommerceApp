using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorECommerce.Server.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Crime and Punishment (pre-reform Russian: Преступленіе и наказаніе; post-reform Russian: Преступление и наказание, tr. Prestupléniye i nakazániye, IPA: [prʲɪstʊˈplʲenʲɪje ɪ nəkɐˈzanʲɪje]) is a novel by the Russian author Fyodor Dostoevsky. It was first published in the literary journal The Russian Messenger in twelve monthly installments during 1866. It was later published in a single volume. It is the second of Dostoevsky's full-length novels following his return from ten years of exile in Siberia. Crime and Punishment is considered the first great novel of his mature period of writing, The novel is often cited as one of the supreme achievements in world literature.", "https://upload.wikimedia.org/wikipedia/en/thumb/4/4b/Crimeandpunishmentcover.png/220px-Crimeandpunishmentcover.png", 89.99m, "Crime and Punishment" },
                    { 2, "The Brothers Karamazov (Russian: Братья Карамазовы, Brat'ya Karamazovy, pronounced [ˈbratʲjə kərɐˈmazəvɨ]), also translated as The Karamazov Brothers, is the last novel by Russian author Fyodor Dostoevsky. Dostoevsky spent nearly two years writing The Brothers Karamazov, which was published as a serial in The Russian Messenger from January 1879 to November 1880. Dostoevsky died less than four months after its publication.", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2d/Dostoevsky-Brothers_Karamazov.jpg/220px-Dostoevsky-Brothers_Karamazov.jpg", 15.9m, "The Brothers Karamazov" },
                    { 3, "Demons (pre-reform Russian: Бѣсы; post-reform Russian: Бесы, tr. Bésy, IPA: [ˈbʲe.sɨ]; sometimes also called The Possessed or The Devils is a novel by Fyodor Dostoevsky, first published in the journal The Russian Messenger in 1871–72. It is considered one of the four masterworks written by Dostoevsky after his return from Siberian exile, along with Crime and Punishment (1866), The Idiot (1869), and The Brothers Karamazov (1880). Demons is a social and political satire, a psychological drama, and large-scale tragedy. Joyce Carol Oates has described it as \"Dostoevsky's most confused and violent novel, and his most satisfactorily 'tragic' work.\"[1] According to Ronald Hingley, it is Dostoevsky's \"greatest onslaught on Nihilism\", and \"one of humanity's most impressive achievements—perhaps even its supreme achievement—in the art of prose fiction", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG/220px-The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG", 9.99m, "Demons" },
                    { 4, "Demons (pre-reform Russian: Бѣсы; post-reform Russian: Бесы, tr. Bésy, IPA: [ˈbʲe.sɨ]; sometimes also called The Possessed or The Devils is a novel by Fyodor Dostoevsky, first published in the journal The Russian Messenger in 1871–72. It is considered one of the four masterworks written by Dostoevsky after his return from Siberian exile, along with Crime and Punishment (1866), The Idiot (1869), and The Brothers Karamazov (1880). Demons is a social and political satire, a psychological drama, and large-scale tragedy. Joyce Carol Oates has described it as \"Dostoevsky's most confused and violent novel, and his most satisfactorily 'tragic' work.\"[1] According to Ronald Hingley, it is Dostoevsky's \"greatest onslaught on Nihilism\", and \"one of humanity's most impressive achievements—perhaps even its supreme achievement—in the art of prose fiction", "https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG/220px-The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG", 9.99m, "Demons" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
