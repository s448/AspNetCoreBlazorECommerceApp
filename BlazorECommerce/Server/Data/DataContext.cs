﻿using BlazorEcommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorECommerce.Server.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardItem>().HasKey(ci => new { ci.UserId, ci.ProductId, ci.ProductTypeId });
            modelBuilder.Entity<ProductVariant>().HasKey(p => new { p.ProductId, p.ProductTypeId });
            modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.ProductId, oi.OrderId, oi.ProductTypeId });

            modelBuilder.Entity<Category>().HasData(
                  new Category { Id = 1, Name = "Books", Url = "books" },
                  new Category { Id = 2, Name = "Movies", Url = "movies" },
                  new Category { Id = 3, Name = "Video Games", Url = "video-games" }
                );
            modelBuilder.Entity<ProductType>().HasData(
            new ProductType { Id = 1, Name = "Default" },
            new ProductType { Id = 2, Name = "Paperback" },
            new ProductType { Id = 3, Name = "E-Book" },
            new ProductType { Id = 4, Name = "Audiobook" },
            new ProductType { Id = 5, Name = "Stream" },
            new ProductType { Id = 6, Name = "Blu-ray" },
            new ProductType { Id = 7, Name = "VHS" },
            new ProductType { Id = 8, Name = "PC" },
            new ProductType { Id = 9, Name = "PlayStation" },
            new ProductType { Id = 10, Name = "Xbox" }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { CategoryId = 1, Id = 1, Description = "Crime and Punishment (pre-reform Russian: Преступленіе и наказаніе; post-reform Russian: Преступление и наказание, tr. Prestupléniye i nakazániye, IPA: [prʲɪstʊˈplʲenʲɪje ɪ nəkɐˈzanʲɪje]) is a novel by the Russian author Fyodor Dostoevsky. It was first published in the literary journal The Russian Messenger in twelve monthly installments during 1866. It was later published in a single volume. It is the second of Dostoevsky's full-length novels following his return from ten years of exile in Siberia. Crime and Punishment is considered the first great novel of his mature period of writing, The novel is often cited as one of the supreme achievements in world literature.", ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/4/4b/Crimeandpunishmentcover.png/220px-Crimeandpunishmentcover.png", Title = "Crime and Punishment" },
                new Product { CategoryId = 1, Id = 2, Description = "The Brothers Karamazov (Russian: Братья Карамазовы, Brat'ya Karamazovy, pronounced [ˈbratʲjə kərɐˈmazəvɨ]), also translated as The Karamazov Brothers, is the last novel by Russian author Fyodor Dostoevsky. Dostoevsky spent nearly two years writing The Brothers Karamazov, which was published as a serial in The Russian Messenger from January 1879 to November 1880. Dostoevsky died less than four months after its publication.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2d/Dostoevsky-Brothers_Karamazov.jpg/220px-Dostoevsky-Brothers_Karamazov.jpg", Title = "The Brothers Karamazov" },
                new Product { CategoryId = 1, Id = 3, Description = "Demons (pre-reform Russian: Бѣсы; post-reform Russian: Бесы, tr. Bésy, IPA: [ˈbʲe.sɨ]; sometimes also called The Possessed or The Devils is a novel by Fyodor Dostoevsky, first published in the journal The Russian Messenger in 1871–72. It is considered one of the four masterworks written by Dostoevsky after his return from Siberian exile, along with Crime and Punishment (1866), The Idiot (1869), and The Brothers Karamazov (1880). Demons is a social and political satire, a psychological drama, and large-scale tragedy. Joyce Carol Oates has described it as \"Dostoevsky's most confused and violent novel, and his most satisfactorily 'tragic' work.\"[1] According to Ronald Hingley, it is Dostoevsky's \"greatest onslaught on Nihilism\", and \"one of humanity's most impressive achievements—perhaps even its supreme achievement—in the art of prose fiction", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG/220px-The_first_edition_of_Dostoevsky%27s_novel_Demons_Petersburg_1873.JPG", Title = "Demons" },
                new Product
                {
                    Id = 4,
                    CategoryId = 2,
                    Title = "The Matrix",
                    Description = "The Matrix is a 1999 science fiction action film written and directed by the Wachowskis, and produced by Joel Silver. Starring Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss, Hugo Weaving, and Joe Pantoliano, and as the first installment in the Matrix franchise, it depicts a dystopian future in which humanity is unknowingly trapped inside a simulated reality, the Matrix, which intelligent machines have created to distract humans while using their bodies as an energy source. When computer programmer Thomas Anderson, under the hacker alias \"Neo\", uncovers the truth, he \"is drawn into a rebellion against the machines\" along with other people who have been freed from the Matrix.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg",
                    Featured = true,
                },
                new Product
                {
                    Id = 5,
                    CategoryId = 2,
                    Title = "Back to the Future",
                    Description = "Back to the Future is a 1985 American science fiction film directed by Robert Zemeckis. Written by Zemeckis and Bob Gale, it stars Michael J. Fox, Christopher Lloyd, Lea Thompson, Crispin Glover, and Thomas F. Wilson. Set in 1985, the story follows Marty McFly (Fox), a teenager accidentally sent back to 1955 in a time-traveling DeLorean automobile built by his eccentric scientist friend Doctor Emmett \"Doc\" Brown (Lloyd). Trapped in the past, Marty inadvertently prevents his future parents' meeting—threatening his very existence—and is forced to reconcile the pair and somehow get back to the future.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/d2/Back_to_the_Future.jpg",
                    Featured = true,
                },
                new Product
                {
                    Id = 6,
                    CategoryId = 2,
                    Title = "Toy Story",
                    Description = "Toy Story is a 1995 American computer-animated comedy film produced by Pixar Animation Studios and released by Walt Disney Pictures. The first installment in the Toy Story franchise, it was the first entirely computer-animated feature film, as well as the first feature film from Pixar. The film was directed by John Lasseter (in his feature directorial debut), and written by Joss Whedon, Andrew Stanton, Joel Cohen, and Alec Sokolow from a story by Lasseter, Stanton, Pete Docter, and Joe Ranft. The film features music by Randy Newman, was produced by Bonnie Arnold and Ralph Guggenheim, and was executive-produced by Steve Jobs and Edwin Catmull. The film features the voices of Tom Hanks, Tim Allen, Don Rickles, Wallace Shawn, John Ratzenberger, Jim Varney, Annie Potts, R. Lee Ermey, John Morris, Laurie Metcalf, and Erik von Detten. Taking place in a world where anthropomorphic toys come to life when humans are not present, the plot focuses on the relationship between an old-fashioned pull-string cowboy doll named Woody and an astronaut action figure, Buzz Lightyear, as they evolve from rivals competing for the affections of their owner, Andy Davis, to friends who work together to be reunited with Andy after being separated from him.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/1/13/Toy_Story.jpg",

                },
                new Product
                {
                    Id = 7,
                    CategoryId = 3,
                    Title = "Half-Life 2",
                    Description = "Half-Life 2 is a 2004 first-person shooter game developed and published by Valve. Like the original Half-Life, it combines shooting, puzzles, and storytelling, and adds features such as vehicles and physics-based gameplay.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/2/25/Half-Life_2_cover.jpg",
                    Featured = true,

                },
                new Product
                {
                    Id = 8,
                    CategoryId = 3,
                    Title = "Diablo II",
                    Description = "Diablo II is an action role-playing hack-and-slash computer video game developed by Blizzard North and published by Blizzard Entertainment in 2000 for Microsoft Windows, Classic Mac OS, and macOS.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/d/d5/Diablo_II_Coverart.png",
                },
                new Product
                {
                    Id = 9,
                    CategoryId = 3,
                    Title = "Day of the Tentacle",
                    Description = "Day of the Tentacle, also known as Maniac Mansion II: Day of the Tentacle, is a 1993 graphic adventure game developed and published by LucasArts. It is the sequel to the 1987 game Maniac Mansion.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/7/79/Day_of_the_Tentacle_artwork.jpg",
                },
                new Product
                {
                    Id = 10,
                    CategoryId = 3,
                    Title = "Xbox",
                    Description = "The Xbox is a home video game console and the first installment in the Xbox series of video game consoles manufactured by Microsoft.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/43/Xbox-console.jpg",
                },
                new Product
                {
                    Id = 11,
                    CategoryId = 3,
                    Title = "Super Nintendo Entertainment System",
                    Description = "The Super Nintendo Entertainment System (SNES), also known as the Super NES or Super Nintendo, is a 16-bit home video game console developed by Nintendo that was released in 1990 in Japan and South Korea.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/e/ee/Nintendo-Super-Famicom-Set-FL.jpg",
                }
                );
            modelBuilder.Entity<ProductVariant>().HasData(
               new ProductVariant
               {
                   ProductId = 1,
                   ProductTypeId = 2,
                   Price = 9.99m,
                   OriginalPrice = 19.99m
               },
               new ProductVariant
               {
                   ProductId = 1,
                   ProductTypeId = 3,
                   Price = 7.99m
               },
               new ProductVariant
               {
                   ProductId = 1,
                   ProductTypeId = 4,
                   Price = 19.99m,
                   OriginalPrice = 29.99m
               },
               new ProductVariant
               {
                   ProductId = 2,
                   ProductTypeId = 2,
                   Price = 7.99m,
                   OriginalPrice = 14.99m
               },
               new ProductVariant
               {
                   ProductId = 3,
                   ProductTypeId = 2,
                   Price = 6.99m
               },
               new ProductVariant
               {
                   ProductId = 4,
                   ProductTypeId = 5,
                   Price = 3.99m
               },
               new ProductVariant
               {
                   ProductId = 4,
                   ProductTypeId = 6,
                   Price = 9.99m
               },
               new ProductVariant
               {
                   ProductId = 4,
                   ProductTypeId = 7,
                   Price = 19.99m
               },
               new ProductVariant
               {
                   ProductId = 5,
                   ProductTypeId = 5,
                   Price = 3.99m,
               },
               new ProductVariant
               {
                   ProductId = 6,
                   ProductTypeId = 5,
                   Price = 2.99m
               },
               new ProductVariant
               {
                   ProductId = 7,
                   ProductTypeId = 8,
                   Price = 19.99m,
                   OriginalPrice = 29.99m
               },
               new ProductVariant
               {
                   ProductId = 7,
                   ProductTypeId = 9,
                   Price = 69.99m
               },
               new ProductVariant
               {
                   ProductId = 7,
                   ProductTypeId = 10,
                   Price = 49.99m,
                   OriginalPrice = 59.99m
               },
               new ProductVariant
               {
                   ProductId = 8,
                   ProductTypeId = 8,
                   Price = 9.99m,
                   OriginalPrice = 24.99m,
               },
               new ProductVariant
               {
                   ProductId = 9,
                   ProductTypeId = 8,
                   Price = 14.99m
               },
               new ProductVariant
               {
                   ProductId = 10,
                   ProductTypeId = 1,
                   Price = 159.99m,
                   OriginalPrice = 299m
               },
               new ProductVariant
               {
                   ProductId = 11,
                   ProductTypeId = 1,
                   Price = 79.99m,
                   OriginalPrice = 399m
               }
           );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVariant> productVariants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CardItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }

}


/*
 *
 */