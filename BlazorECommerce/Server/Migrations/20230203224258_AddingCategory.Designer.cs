﻿// <auto-generated />
using BlazorECommerce.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorECommerce.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230203224258_AddingCategory")]
    partial class AddingCategory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlazorECommerce.Shared.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Books",
                            Url = "books"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Movies",
                            Url = "movies"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Video Games",
                            Url = "video-games"
                        });
                });

            modelBuilder.Entity("BlazorECommerce.Shared.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Crime and Punishment (pre-reform Russian: Преступленіе и наказаніе; post-reform Russian: Преступление и наказание, tr. Prestupléniye i nakazániye, IPA: [prʲɪstʊˈplʲenʲɪje ɪ nəkɐˈzanʲɪje]) is a novel by the Russian author Fyodor Dostoevsky. It was first published in the literary journal The Russian Messenger in twelve monthly installments during 1866. It was later published in a single volume. It is the second of Dostoevsky's full-length novels following his return from ten years of exile in Siberia. Crime and Punishment is considered the first great novel of his mature period of writing, The novel is often cited as one of the supreme achievements in world literature.",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/4/4b/Crimeandpunishmentcover.png/220px-Crimeandpunishmentcover.png",
                            Price = 89.99m,
                            Title = "Crime and Punishment"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "The Brothers Karamazov (Russian: Братья Карамазовы, Brat'ya Karamazovy, pronounced [ˈbratʲjə kərɐˈmazəvɨ]), also translated as The Karamazov Brothers, is the last novel by Russian author Fyodor Dostoevsky. Dostoevsky spent nearly two years writing The Brothers Karamazov, which was published as a serial in The Russian Messenger from January 1879 to November 1880. Dostoevsky died less than four months after its publication.",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2d/Dostoevsky-Brothers_Karamazov.jpg/220px-Dostoevsky-Brothers_Karamazov.jpg",
                            Price = 15.9m,
                            Title = "The Brothers Karamazov"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Demons (pre-reform Russian: Бѣсы; post-reform Russian: Бесы, tr. Bésy, IPA: [ˈbʲe.sɨ]; sometimes also called The Possessed or The Devils is a novel by Fyodor Dostoevsky, first published in the journal The Russian Messenger in 1871–72. It is considered one of the four masterworks written by Dostoevsky after his return from Siberian exile, along with Crime and Punishment (1866), The Idiot (1869), and The Brothers Karamazov (1880). Demons is a social and political satire, a psychological drama, and large-scale tragedy. Joyce Carol Oates has described it as \"Dostoevsky's most confused and violent novel, and his most satisfactorily 'tragic' work.\"[1] According to Ronald Hingley, it is Dostoevsky's \"greatest onslaught on Nihilism\", and \"one of humanity's most impressive achievements—perhaps even its supreme achievement—in the art of prose fiction",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG/220px-The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG",
                            Price = 9.99m,
                            Title = "Demons"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Demons (pre-reform Russian: Бѣсы; post-reform Russian: Бесы, tr. Bésy, IPA: [ˈbʲe.sɨ]; sometimes also called The Possessed or The Devils is a novel by Fyodor Dostoevsky, first published in the journal The Russian Messenger in 1871–72. It is considered one of the four masterworks written by Dostoevsky after his return from Siberian exile, along with Crime and Punishment (1866), The Idiot (1869), and The Brothers Karamazov (1880). Demons is a social and political satire, a psychological drama, and large-scale tragedy. Joyce Carol Oates has described it as \"Dostoevsky's most confused and violent novel, and his most satisfactorily 'tragic' work.\"[1] According to Ronald Hingley, it is Dostoevsky's \"greatest onslaught on Nihilism\", and \"one of humanity's most impressive achievements—perhaps even its supreme achievement—in the art of prose fiction",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG/220px-The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG",
                            Price = 9.99m,
                            Title = "Demons"
                        });
                });

            modelBuilder.Entity("BlazorECommerce.Shared.Product", b =>
                {
                    b.HasOne("BlazorECommerce.Shared.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
