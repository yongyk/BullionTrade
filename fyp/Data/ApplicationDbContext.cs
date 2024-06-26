﻿using fyp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace fyp.Data

{
    public class ApplicationDbContext: IdentityDbContext <IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ShoppingCart>ShoppingCarts { get; set; } 
        
        public DbSet<OrderHeader> OrderHeaders { get; set; }    
        public DbSet<OrderDetail> OrderDetails { get; set; }    
        
        public DbSet<AppointmentSlot> AppointmentSlots { get; set; }

        public DbSet<Selling> Sellings { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Gold Bar"},
                new Category { Id=2, Name="Gold Coin"},
                new Category { Id=3, Name="Gold Dinar"}
                );

            
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "pamp gold bar 10 g", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tortor pretium viverra suspendisse potenti. " +
                "Condimentum vitae sapien pellentesque habitant morbi. Aliquam ultrices sagittis orci a scelerisque purus. Amet cursus sit amet dictum sit amet justo. Auctor urna nunc id cursus metus. Magnis dis parturient montes nascetur ridiculus mus. " +
                "Mauris rhoncus aenean vel elit. Commodo sed egestas egestas fringilla phasellus faucibus scelerisque eleifend. Dictum sit amet justo donec enim diam. Tristique senectus et netus et malesuada fames ac turpis egestas. Mauris commodo quis imperdiet massa tincidunt nunc pulvinar. Dignissim convallis aenean et tortor at." +
                " Ut eu sem integer vitae justo. Sapien pellentesque habitant morbi tristique. Nisl purus in mollis nunc sed id semper risus in.",
                Price=5000, CategoryId=2,
                    ImageUrl = "",
                    ProductBrand = "Pamp",
                    ProductMetal = "Gold",
                    ProductPurity = "999.9",
                    Quantity = 300
                },

                   new Product
                   {
                       Id = 2,
                       Name = "pamp gold bar 50 g",
                       Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tortor pretium viverra suspendisse potenti. " +
                "Condimentum vitae sapien pellentesque habitant morbi. Aliquam ultrices sagittis orci a scelerisque purus. Amet cursus sit amet dictum sit amet justo. Auctor urna nunc id cursus metus. Magnis dis parturient montes nascetur ridiculus mus. " +
                "Mauris rhoncus aenean vel elit. Commodo sed egestas egestas fringilla phasellus faucibus scelerisque eleifend. Dictum sit amet justo donec enim diam. Tristique senectus et netus et malesuada fames ac turpis egestas. Mauris commodo quis imperdiet massa tincidunt nunc pulvinar. Dignissim convallis aenean et tortor at." +
                " Ut eu sem integer vitae justo. Sapien pellentesque habitant morbi tristique. Nisl purus in mollis nunc sed id semper risus in.",
                       Price = 50000,
                       CategoryId = 2,
                       ImageUrl="",
                       ProductBrand = "Pamp",
                       ProductMetal = "Gold",
                       ProductPurity = "999",
                       Quantity = 300
                   },
                  
                  
                      new Product
                      {
                          Id = 3,
                          Name = "pamp gold bar 5 g",
                          Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tortor pretium viverra suspendisse potenti. " +
                "Condimentum vitae sapien pellentesque habitant morbi. Aliquam ultrices sagittis orci a scelerisque purus. Amet cursus sit amet dictum sit amet justo. Auctor urna nunc id cursus metus. Magnis dis parturient montes nascetur ridiculus mus. " +
                "Mauris rhoncus aenean vel elit. Commodo sed egestas egestas fringilla phasellus faucibus scelerisque eleifend. Dictum sit amet justo donec enim diam. Tristique senectus et netus et malesuada fames ac turpis egestas. Mauris commodo quis imperdiet massa tincidunt nunc pulvinar. Dignissim convallis aenean et tortor at." +
                " Ut eu sem integer vitae justo. Sapien pellentesque habitant morbi tristique. Nisl purus in mollis nunc sed id semper risus in.",
                          Price = 500, CategoryId=3,
                          ImageUrl = "",
                          ProductBrand = "Pamp",
                          ProductMetal = "Gold",
                          ProductPurity = "999",
                          Quantity = 300

                      }
            
             );

            modelBuilder.Entity<AppointmentSlot>().HasData(
               new AppointmentSlot { Id = 1, Date = "01/06/2024", Time="15:00" },
               new AppointmentSlot { Id = 2, Date = "02/06/2024", Time = "16:00" },
               new AppointmentSlot { Id = 3, Date = "03/06/2024", Time = "10:00" },
               new AppointmentSlot { Id = 4, Date = "04/06/2024", Time = "11:00" }

               );
            modelBuilder.Entity<Article>().HasData(
                new Article { Id = 1, Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vitae turpis massa sed elementum tempus egestas sed sed risus. Nunc vel risus commodo viverra maecenas accumsan lacus. " +
                "Venenatis lectus magna fringilla urna. Faucibus turpis in eu mi bibendum neque. Augue eget arcu dictum varius duis at consectetur lorem donec. Nisl pretium fusce id velit. Diam in arcu cursus euismod quis viverra. Nunc sed augue lacus viverra vitae congue eu consequat. Auctor elit sed vulputate mi sit amet mauris. Est pellentesque elit ullamcorper dignissim cras tincidunt. Ut tristique et egestas quis ipsum suspendisse ultrices gravida dictum. Id nibh tortor id aliquet lectus proin nibh." +
                " Nisl rhoncus mattis rhoncus urna neque viverra justo nec. Amet nisl suscipit adipiscing bibendum est ultricies integer quis auctor. Augue mauris augue neque gravida in. Pharetra magna ac placerat vestibulum lectus mauris ultrices. Lobortis mattis aliquam faucibus purus in massa tempor." , DateCreated = "01/06/2024", Author= "Richard", Title=" Gold & Silver will outperform stocks, Real Estate", ImageUrl="" },
                  new Article
                  {
                      Id = 2,
                      Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vitae turpis massa sed elementum tempus egestas sed sed risus. Nunc vel risus commodo viverra maecenas accumsan lacus. " +
                "Venenatis lectus magna fringilla urna. Faucibus turpis in eu mi bibendum neque. Augue eget arcu dictum varius duis at consectetur lorem donec. Nisl pretium fusce id velit. Diam in arcu cursus euismod quis viverra. Nunc sed augue lacus viverra vitae congue eu consequat. Auctor elit sed vulputate mi sit amet mauris. Est pellentesque elit ullamcorper dignissim cras tincidunt. Ut tristique et egestas quis ipsum suspendisse ultrices gravida dictum. Id nibh tortor id aliquet lectus proin nibh." +
                " Nisl rhoncus mattis rhoncus urna neque viverra justo nec. Amet nisl suscipit adipiscing bibendum est ultricies integer quis auctor. Augue mauris augue neque gravida in. Pharetra magna ac placerat vestibulum lectus mauris ultrices. Lobortis mattis aliquam faucibus purus in massa tempor.",
                      DateCreated = "02/06/2024",
                      Author = "Ali",
                      Title = " Gold & Silver will outperform stocks, Real Estate 2",
                      ImageUrl=""
                  },
                    new Article
                    {
                        Id = 3,
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vitae turpis massa sed elementum tempus egestas sed sed risus. Nunc vel risus commodo viverra maecenas accumsan lacus. " +
                "Venenatis lectus magna fringilla urna. Faucibus turpis in eu mi bibendum neque. Augue eget arcu dictum varius duis at consectetur lorem donec. Nisl pretium fusce id velit. Diam in arcu cursus euismod quis viverra. Nunc sed augue lacus viverra vitae congue eu consequat. Auctor elit sed vulputate mi sit amet mauris. Est pellentesque elit ullamcorper dignissim cras tincidunt. Ut tristique et egestas quis ipsum suspendisse ultrices gravida dictum. Id nibh tortor id aliquet lectus proin nibh." +
                " Nisl rhoncus mattis rhoncus urna neque viverra justo nec. Amet nisl suscipit adipiscing bibendum est ultricies integer quis auctor. Augue mauris augue neque gravida in. Pharetra magna ac placerat vestibulum lectus mauris ultrices. Lobortis mattis aliquam faucibus purus in massa tempor.",
                        DateCreated = "03/06/2024",
                        Author = "Abu",
                        Title = " Gold & Silver will outperform stocks, Real Estate 3",
                        ImageUrl=""
                    }
                );


            modelBuilder.Entity<ShoppingCart>()
       .Property(s => s.Id)
       .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    
    }
}
