namespace CSULB.GetUsGrub.DataAccess.Migrations.UserManagement.UserContext
{
    using CSULB.GetUsGrub.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CSULB.GetUsGrub.DataAccess.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\UserManagement\UserContext";
        }

        protected override void Seed(CSULB.GetUsGrub.DataAccess.UserContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //UserAccounts seed
            var users = new List<UserAccount>()
            {
               new UserAccount() { Username = "User1", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
               new UserAccount() { Username = "User2", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
               new UserAccount() { Username = "User3", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
               new UserAccount() { Username = "User4", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
               new UserAccount() { Username = "User5", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
               new UserAccount() { Username = "User6", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
               new UserAccount() { Username = "User7", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
               new UserAccount() { Username = "User8", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
               new UserAccount() { Username = "User9", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
               new UserAccount() { Username = "User10", Password = "password123!@", IsActive = true, IsFirstTimeUser = true},
            };


            //users.ForEach(newUser => context.UserAccounts.AddOrUpdate(newUser)); //Adding users to database
            //context.UserAccounts.AddOrUpdate(x => x.Username, users);
            context.UserAccounts.AddOrUpdate(x => x.Username, (users.ToArray()));
            context.SaveChanges();//Save the changes

            //enable - migrations - ContextTypeName CSULB.GetUsGrub.DataAccess.UserContext - MigrationsDirectory:Migrations\testUsermanagement
            //add - migration - configuration CSULB.GetUsGrub.DataAccess.Migrations.testUsermanagement.Configuration
            //update - database - configuration CSULB.GetUsGrub.DataAccess.Migrations.testUsermanagement.Configuration - verbose

            //UserProfiles seed
            var userProfiles = new List<UserProfile>()
            {
               new UserProfile() { Id = 1, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName1"},
               new UserProfile() { Id = 2, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName2"},
               new UserProfile() { Id = 3, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName3"},
               new UserProfile() { Id = 4, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName4"},
               new UserProfile() { Id = 5, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName5"},
               new UserProfile() { Id = 6, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName6"},
               new UserProfile() { Id = 7, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName7"},
               new UserProfile() { Id = 8, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName8"},
               new UserProfile() { Id = 9, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName9"},
               new UserProfile() { Id = 10, DisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\EmptyProfile.png", DisplayName = "DisplayName10"}
            };
            context.UserProfiles.AddOrUpdate(x => x.Id, (userProfiles.ToArray()));
            context.SaveChanges();//Save the changes

            //PasswordSalts seed
            var userPasswordSalts = new List<PasswordSalt>()
            {
               new PasswordSalt() { Id = 1, Salt = "salt1"},
               new PasswordSalt() { Id = 2, Salt = "salt2"},
               new PasswordSalt() { Id = 3, Salt = "salt3"},
               new PasswordSalt() { Id = 4, Salt = "salt4"},
               new PasswordSalt() { Id = 5, Salt = "salt5"},
               new PasswordSalt() { Id = 6, Salt = "salt6"},
               new PasswordSalt() { Id = 7, Salt = "salt7"},
               new PasswordSalt() { Id = 8, Salt = "salt8"},
               new PasswordSalt() { Id = 9, Salt = "salt9"},
               new PasswordSalt() { Id = 10, Salt = "salt10"},
            };
            context.PasswordSalts.AddOrUpdate(x => x.Id, (userPasswordSalts.ToArray()));
            context.SaveChanges();//Save the changes
            //RestaurantProfiles seed
            //Id PhoneNumber Address Details
            var userRestaurantProfiles = new List<RestaurantProfile>()
            {
               new RestaurantProfile() {
                   Id = 1,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },

               new RestaurantProfile() {
                   Id = 2,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },

                new RestaurantProfile() {
                   Id = 3,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },

                new RestaurantProfile() {
                   Id = 4,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },

                new RestaurantProfile() {
                   Id = 5,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },

                new RestaurantProfile() {
                   Id = 6,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },

                new RestaurantProfile() {
                   Id = 7,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },

                new RestaurantProfile() {
                   Id = 8,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },

                new RestaurantProfile() {
                   Id = 9,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },

                new RestaurantProfile() {
                   Id = 10,
                   PhoneNumber = "(555)555-555",
                   Address = {Street1 = "Street1", Street2 ="Street2", City = "City1", State = "State1", Zip = 12345 },
                   Details = {HasReservations = true, HasDelivery = true, HasTakeOut = true, AcceptCreditCards = true,
                       Attire = "Attire1", HasOutdoorSeating = true, HasTv = true, HasDriveThru = true, Caters = true,
                       AllowsPets = true, Category = "Category1" },
                   Latitude = 2.50, Longitude = 2.50, BusinessHoursJson = "BusinessHoursJson1" },
            };

            context.RestaurantProfiles.AddOrUpdate(x => x.Id, (userRestaurantProfiles.ToArray()));
            context.SaveChanges();//Save the changes
           // RestaurantMenus seed
            //var userRestaurantMenus = new List<RestaurantMenu>()
            //{
            //    new RestaurantMenu() { Id = 1, RestaurantId = 1, MenuName = "Menu1", Items = { } },
            //    new RestaurantMenu() { Id = 2, RestaurantId = 2, MenuName = "Menu2", Items = { } },
            //    new RestaurantMenu() { Id = 3, RestaurantId = 3, MenuName = "Menu3", Items = { } },
            //    new RestaurantMenu() { Id = 4, RestaurantId = 4, MenuName = "Menu4", Items = { } },
            //    new RestaurantMenu() { Id = 5, RestaurantId = 5, MenuName = "Menu5", Items = { } },
            //    new RestaurantMenu() { Id = 6, RestaurantId = 6, MenuName = "Menu6", Items = { } },
            //    new RestaurantMenu() { Id = 7, RestaurantId = 7, MenuName = "Menu7", Items = { } },
            //    new RestaurantMenu() { Id = 8, RestaurantId = 8, MenuName = "Menu8", Items = { } },
            //    new RestaurantMenu() { Id = 9, RestaurantId = 9, MenuName = "Menu9", Items = { } },
            //    new RestaurantMenu() { Id = 10, RestaurantId = 10, MenuName = "Menu10", Items = { } }
            //};
            //context.RestaurantMenus.AddOrUpdate(x => x.Id, (userRestaurantMenus.ToArray()));
            //context.SaveChanges();//Save the changes

            ////RestaurantMenuItems seed
            //var userRestaurantMenuItems = new List<RestaurantMenuItem>()
            //{
            //   new RestaurantMenuItem() { Id = 1, MenuId = 1,  ItemPrice = 1.50,
            //       ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag1", Description = "Description1", IsActive = true},
            //   new RestaurantMenuItem() { Id = 2, MenuId = 2,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag2", Description = "Description2", IsActive = true},
            //   new RestaurantMenuItem() { Id = 3, MenuId = 3,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag3", Description = "Description3", IsActive = true},
            //   new RestaurantMenuItem() { Id = 4, MenuId = 4,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag4", Description = "Description4", IsActive = true},
            //   new RestaurantMenuItem() { Id = 5, MenuId = 5,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag5", Description = "Description5", IsActive = true},
            //   new RestaurantMenuItem() { Id = 6, MenuId = 6,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag6", Description = "Description6", IsActive = true},
            //   new RestaurantMenuItem() { Id = 7, MenuId = 7,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag7", Description = "Description7", IsActive = true},
            //   new RestaurantMenuItem() { Id = 8, MenuId = 8,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag8", Description = "Description8", IsActive = true},
            //   new RestaurantMenuItem() { Id = 9, MenuId = 9,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag9", Description = "Description9", IsActive = true},
            //   new RestaurantMenuItem() { Id = 10, MenuId = 10,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
            //       Tag = "Tag10", Description = "Description10", IsActive = true},
            //};
            //context.RestaurantMenuItems.AddOrUpdate(x => x.Id, (userRestaurantMenuItems.ToArray()));
            //context.SaveChanges();//Save the changes
            ////SecurityQuestions seed
            //var userSecurityQuestions = new List<SecurityAnswerSalt>()
            //{
            //    new SecurityAnswerSalt() { Id = 1, Salt = "Salt1", SecurityQuestion = {Id = 1, UserId = 1, Question = 1, Answer = "Answer1" }},
            //    new SecurityAnswerSalt() { Id = 2, Salt = "Salt2", SecurityQuestion = {Id = 2, UserId = 2, Question = 2, Answer = "Answer2" }},
            //    new SecurityAnswerSalt() { Id = 3, Salt = "Salt3", SecurityQuestion = {Id = 3, UserId = 3, Question = 3, Answer = "Answer3" }},
            //    new SecurityAnswerSalt() { Id = 4, Salt = "Salt4", SecurityQuestion = {Id = 4, UserId = 4, Question = 1, Answer = "Answer4" }},
            //    new SecurityAnswerSalt() { Id = 5, Salt = "Salt5", SecurityQuestion = {Id = 5, UserId = 5, Question = 2, Answer = "Answer5" }},
            //    new SecurityAnswerSalt() { Id = 6, Salt = "Salt6", SecurityQuestion = {Id = 6, UserId = 6, Question = 3, Answer = "Answer6" }},
            //    new SecurityAnswerSalt() { Id = 7, Salt = "Salt7", SecurityQuestion = {Id = 7, UserId = 7, Question = 1, Answer = "Answer7" }},
            //    new SecurityAnswerSalt() { Id = 8, Salt = "Salt8", SecurityQuestion = {Id = 8, UserId = 8, Question = 2, Answer = "Answer8" }},
            //    new SecurityAnswerSalt() { Id = 9, Salt = "Salt9", SecurityQuestion = {Id = 9, UserId = 9, Question = 3, Answer = "Answer9" }},
            //    new SecurityAnswerSalt() { Id = 10, Salt = "Salt10", SecurityQuestion = {Id = 10, UserId = 10, Question = 1, Answer = "Answer10" }},
            //};
            //context.SecurityAnswerSalts.AddOrUpdate(x => x.Id, (userSecurityQuestions.ToArray()));
            //context.SaveChanges();//Save the changes

            ////Tokens seed
            //var userTokens = new List<Token>()
            //{
            //   new Token() { Id = 1, TokenHeader = "TokenHeader1", TokenSignature = "TokenSignature1", Salt = "Salt1", IssuedOn = new DateTime(), ExpiresOn = new DateTime() },
            //   new Token() { Id = 2, TokenHeader = "TokenHeader2", TokenSignature = "TokenSignature2", Salt = "Salt2", IssuedOn = new DateTime(), ExpiresOn = new DateTime() },
            //   new Token() { Id = 3, TokenHeader = "TokenHeader3", TokenSignature = "TokenSignature3", Salt = "Salt3", IssuedOn = new DateTime(), ExpiresOn = new DateTime() },
            //   new Token() { Id = 4, TokenHeader = "TokenHeader4", TokenSignature = "TokenSignature4", Salt = "Salt4", IssuedOn = new DateTime(), ExpiresOn = new DateTime() },
            //   new Token() { Id = 5, TokenHeader = "TokenHeader5", TokenSignature = "TokenSignature5", Salt = "Salt5", IssuedOn = new DateTime(), ExpiresOn = new DateTime() },
            //   new Token() { Id = 6, TokenHeader = "TokenHeader6", TokenSignature = "TokenSignature6", Salt = "Salt6", IssuedOn = new DateTime(), ExpiresOn = new DateTime() },
            //   new Token() { Id = 7, TokenHeader = "TokenHeader7", TokenSignature = "TokenSignature7", Salt = "Salt7", IssuedOn = new DateTime(), ExpiresOn = new DateTime() },
            //   new Token() { Id = 8, TokenHeader = "TokenHeader8", TokenSignature = "TokenSignature8", Salt = "Salt8", IssuedOn = new DateTime(), ExpiresOn = new DateTime() },
            //   new Token() { Id = 9, TokenHeader = "TokenHeader9", TokenSignature = "TokenSignature9", Salt = "Salt9", IssuedOn = new DateTime(), ExpiresOn = new DateTime() },
            //   new Token() { Id = 10, TokenHeader = "TokenHeader10", TokenSignature = "TokenSignature10", Salt = "Salt10", IssuedOn = new DateTime(), ExpiresOn = new DateTime() }
            //};
            //context.Tokens.AddOrUpdate(x => x.Id, (userTokens.ToArray()));
            //context.SaveChanges();//Save the changes
            ////Claims
            //var userClaims = new List<UserClaims>()
            //{
            //   new UserClaims() { Id = 1, ClaimsJson = "ClaimsJson1" },
            //   new UserClaims() { Id = 2, ClaimsJson = "ClaimsJson2" },
            //   new UserClaims() { Id = 3, ClaimsJson = "ClaimsJson3" },
            //   new UserClaims() { Id = 4, ClaimsJson = "ClaimsJson4" },
            //   new UserClaims() { Id = 5, ClaimsJson = "ClaimsJson5" },
            //   new UserClaims() { Id = 6, ClaimsJson = "ClaimsJson6" },
            //   new UserClaims() { Id = 7, ClaimsJson = "ClaimsJson7" },
            //   new UserClaims() { Id = 8, ClaimsJson = "ClaimsJson8" },
            //   new UserClaims() { Id = 9, ClaimsJson = "ClaimsJson9" },
            //   new UserClaims() { Id = 10, ClaimsJson = "ClaimsJson10" }
            //};
            //context.Claims.AddOrUpdate(x => x.Id, (userClaims.ToArray()));
            //context.SaveChanges();//Save the changes
            ////SecurityAnswerSalts
            //var userSecurityAnswerSalts = new List<SecurityAnswerSalt>()
            //{
            //   new SecurityAnswerSalt() { Id = 1, Salt = "Salt1" },
            //   new SecurityAnswerSalt() { Id = 2, Salt = "Salt2" },
            //   new SecurityAnswerSalt() { Id = 3, Salt = "Salt3" },
            //   new SecurityAnswerSalt() { Id = 4, Salt = "Salt4" },
            //   new SecurityAnswerSalt() { Id = 5, Salt = "Salt5" },
            //   new SecurityAnswerSalt() { Id = 6, Salt = "Salt6" },
            //   new SecurityAnswerSalt() { Id = 7, Salt = "Salt7" },
            //   new SecurityAnswerSalt() { Id = 8, Salt = "Salt8" },
            //   new SecurityAnswerSalt() { Id = 9, Salt = "Salt9" },
            //   new SecurityAnswerSalt() { Id = 10, Salt = "Salt10" }
            //};
            //context.SecurityAnswerSalts.AddOrUpdate(x => x.Id, (userSecurityAnswerSalts.ToArray()));
            //context.SaveChanges();//Save the changes
        }
    }
}
