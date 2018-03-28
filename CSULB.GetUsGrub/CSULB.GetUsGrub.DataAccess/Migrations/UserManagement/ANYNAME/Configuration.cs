namespace CSULB.GetUsGrub.DataAccess.Migrations.UserManagement.ANYNAME
{
    using CSULB.GetUsGrub.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Claims;

    internal sealed class Configuration : DbMigrationsConfiguration<CSULB.GetUsGrub.DataAccess.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\UserManagement\ANYNAME";
        }

        protected override void Seed(CSULB.GetUsGrub.DataAccess.UserContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data
            var users = new List<UserAccount>()
            {
               new UserAccount ("User1", "password123!@", true, true),
               new UserAccount ("User2", "password123!@", true, true),
               new UserAccount ("User3", "password123!@", true, true),
               new UserAccount ("User4", "password123!@", true, true),
               new UserAccount ("User5", "password123!@", true, true),
               new UserAccount ("User6", "password123!@", true, true),
               new UserAccount ("User7", "password123!@", true, true),
               new UserAccount ("User8", "password123!@", true, true),
               new UserAccount ("User9", "password123!@", true, true),
               new UserAccount ("User10", "password123!@", true, true),
            };

            //var users = new UserAccount("User1", "password123!@", true, true);
            //var username = users.Username;
            //users.ForEach(newUser => context.UserAccounts.AddOrUpdate(newUser)); //Adding users to database
            //context.UserAccounts.AddOrUpdate(x => x.Username, users);
            context.UserAccounts.AddOrUpdate(userAccount => userAccount.Username, (users.ToArray()));
            //context.UserAccounts.AddOrUpdate(userAccount => userAccount.Username, (users));
            context.SaveChanges();//Save the changes

            //enable - migrations - ContextTypeName CSULB.GetUsGrub.DataAccess.UserContext - MigrationsDirectory:Migrations\testUsermanagement
            //add - migration - configuration CSULB.GetUsGrub.DataAccess.Migrations.testUsermanagement.Configuration
            //update - database - configuration CSULB.GetUsGrub.DataAccess.Migrations.testUsermanagement.Configuration - verbose
            //Worked
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
            //context.SaveChanges();//Save the changes
            //Works
            //PasswordSalts seed
            var userPasswordSalts = new List<PasswordSalt>()
            {
               new PasswordSalt("salt1"){ Id = 1},
               new PasswordSalt("salt2"){ Id = 2},
               new PasswordSalt("salt3"){ Id = 3},
               new PasswordSalt("salt4"){ Id = 4},
               new PasswordSalt("salt5"){ Id = 5},
               new PasswordSalt("salt6"){ Id = 6},
               new PasswordSalt("salt7"){Id = 7},
               new PasswordSalt("salt8"){Id = 8 },
               new PasswordSalt("salt9"){Id = 9 },
               new PasswordSalt("salt10"){Id = 10 },
            };
            context.PasswordSalts.AddOrUpdate(x => x.Id, (userPasswordSalts.ToArray()));
            context.SaveChanges();//Save the changes
                                  //SecurityAnswerSalts
                                  ////SecurityQuestions seed

            var userSecurityQuestions = new List<SecurityQuestion>()
            {
                new SecurityQuestion() { Id = 1, UserId = 1, Question = 1, Answer = "A1"},
                new SecurityQuestion() { Id = 2, UserId = 2, Question = 2, Answer = "A2"},
                new SecurityQuestion() { Id = 3, UserId = 3, Question = 3, Answer = "A3"},
                new SecurityQuestion() { Id = 4, UserId = 4, Question = 1, Answer = "A4"},
                new SecurityQuestion() { Id = 5, UserId = 5, Question = 2, Answer = "A5"},
                new SecurityQuestion() { Id = 6, UserId = 6, Question = 3, Answer = "A6"},
                new SecurityQuestion() { Id = 7, UserId = 7, Question = 1, Answer = "A7"},
                new SecurityQuestion() { Id = 8, UserId = 8, Question = 2, Answer = "A8"},
                new SecurityQuestion() { Id = 9, UserId = 9, Question = 3, Answer = "A9"},
                new SecurityQuestion() { Id = 10, UserId = 10, Question = 1, Answer = "A10"}
            };
            context.SecurityQuestions.AddOrUpdate(x => x.Id, (userSecurityQuestions.ToArray()));
            context.SaveChanges();//Save the changes

            //Works
            var userSecurityAnswerSalts = new List<SecurityAnswerSalt>()
            {
               new SecurityAnswerSalt() { Id = 1, Salt = "Salt1" },
               new SecurityAnswerSalt() { Id = 2, Salt = "Salt2" },
               new SecurityAnswerSalt() { Id = 3, Salt = "Salt3" },
               new SecurityAnswerSalt() { Id = 4, Salt = "Salt4" },
               new SecurityAnswerSalt() { Id = 5, Salt = "Salt5" },
               new SecurityAnswerSalt() { Id = 6, Salt = "Salt6" },
               new SecurityAnswerSalt() { Id = 7, Salt = "Salt7" },
               new SecurityAnswerSalt() { Id = 8, Salt = "Salt8" },
               new SecurityAnswerSalt() { Id = 9, Salt = "Salt9" },
               new SecurityAnswerSalt() { Id = 10, Salt = "Salt10" }
            };
            context.SecurityAnswerSalts.AddOrUpdate(x => x.Id, (userSecurityAnswerSalts.ToArray()));
            context.SaveChanges();//Save the changes


            var userTokens = new List<Token>()
            {
               new Token() { Id = 1, TokenHeader = "TokenHeader1", TokenSignature = "TokenSignature1", Salt = "Salt1", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now },
               new Token() { Id = 2, TokenHeader = "TokenHeader2", TokenSignature = "TokenSignature2", Salt = "Salt2", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now },
               new Token() { Id = 3, TokenHeader = "TokenHeader3", TokenSignature = "TokenSignature3", Salt = "Salt3", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now },
               new Token() { Id = 4, TokenHeader = "TokenHeader4", TokenSignature = "TokenSignature4", Salt = "Salt4", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now },
               new Token() { Id = 5, TokenHeader = "TokenHeader5", TokenSignature = "TokenSignature5", Salt = "Salt5", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now },
               new Token() { Id = 6, TokenHeader = "TokenHeader6", TokenSignature = "TokenSignature6", Salt = "Salt6", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now },
               new Token() { Id = 7, TokenHeader = "TokenHeader7", TokenSignature = "TokenSignature7", Salt = "Salt7", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now },
               new Token() { Id = 8, TokenHeader = "TokenHeader8", TokenSignature = "TokenSignature8", Salt = "Salt8", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now },
               new Token() { Id = 9, TokenHeader = "TokenHeader9", TokenSignature = "TokenSignature9", Salt = "Salt9", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now },
               new Token() { Id = 10, TokenHeader = "TokenHeader10", TokenSignature = "TokenSignature10", Salt = "Salt10", IssuedOn = DateTime.Now, ExpiresOn = DateTime.Now }
            };
            context.Tokens.AddOrUpdate(x => x.Id, (userTokens.ToArray()));
            context.SaveChanges();//Save the changes

            //Claims
            var claims = new Collection<Claim>()
            {
                new Claim("ReadIndividualProfile", "True"),
                new Claim("WriteIndividualProfile", "True")
            };
            //var claimsJson = JsonConvert.SerializeObject(claims);
            var userClaims = new List<UserClaims>()
            {
               new UserClaims() { Id = 1, Claims = claims },
               new UserClaims() { Id = 2, Claims = claims },
               new UserClaims() { Id = 3, Claims = claims },
               new UserClaims() { Id = 4, Claims = claims },
               new UserClaims() { Id = 5, Claims = claims },
               new UserClaims() { Id = 6, Claims = claims },
               new UserClaims() { Id = 7, Claims = claims },
               new UserClaims() { Id = 8, Claims = claims },
               new UserClaims() { Id = 9, Claims = claims },
               new UserClaims() { Id = 10, Claims = claims }
            };
            context.Claims.AddOrUpdate(x => x.Id, (userClaims.ToArray()));
            context.SaveChanges();//Save the changes

            //RestaurantProfiles seed
            //Id PhoneNumber Address Details
            var userRestaurantProfiles = new List<RestaurantProfile>()
            {
                new RestaurantProfile(
                    1,
                    "(555)555-555",
                    new Address("Street1","Street2", "City1","State1", 12345),
                    new RestaurantDetail(0, true, true, true, true, "Attire1", true, true, true, true, true, true,"Category1"),
                    2.50,
                    2.50),
                new RestaurantProfile(
                    2,
                    "(555)555-555",
                    new Address("Street1", "Street2", "City1", "State1", 12345),
                    new RestaurantDetail(1, true, true, true, true, "Attire1", true, true, true, true, true, true, "Category1"),
                    2.50,
                    2.50),
                new RestaurantProfile(
                    3,
                    "(555)555-555",
                    new Address("Street1", "Street2", "City1", "State1", 12345),
                    new RestaurantDetail(2, true, true, true, true, "Attire1", true, true, true, true, true, true, "Category1"),
                    2.50,
                    2.50),
                new RestaurantProfile(
                    4,
                    "(555)555-555",
                    new Address("Street1","Street2", "City1","State1", 12345),
                    new RestaurantDetail(0, true, true, true, true, "Attire1", true, true, true, true, true, true,"Category1"),
                    2.50,
                    2.50),
                new RestaurantProfile(
                    5,
                    "(555)555-555",
                    new Address("Street1", "Street2", "City1", "State1", 12345),
                    new RestaurantDetail(1, true, true, true, true, "Attire1", true, true, true, true, true, true, "Category1"),
                    2.50,
                    2.50),
                new RestaurantProfile(
                    6,
                    "(555)555-555",
                    new Address("Street1", "Street2", "City1", "State1", 12345),
                    new RestaurantDetail(2, true, true, true, true, "Attire1", true, true, true, true, true, true, "Category1"),
                    2.50,
                    2.50),
                new RestaurantProfile(
                    7,
                    "(555)555-555",
                    new Address("Street1","Street2", "City1","State1", 12345),
                    new RestaurantDetail(0, true, true, true, true, "Attire1", true, true, true, true, true, true,"Category1"),
                    2.50,
                    2.50),
                new RestaurantProfile(
                    8,
                    "(555)555-555",
                    new Address("Street1", "Street2", "City1", "State1", 12345),
                    new RestaurantDetail(1, true, true, true, true, "Attire1", true, true, true, true, true, true, "Category1"),
                    2.50,
                    2.50),
                new RestaurantProfile(
                    9,
                    "(555)555-555",
                    new Address("Street1", "Street2", "City1", "State1", 12345),
                    new RestaurantDetail(2, true, true, true, true, "Attire1", true, true, true, true, true, true, "Category1"),
                    2.50,
                    2.50),
                 new RestaurantProfile(
                    10,
                    "(555)555-555",
                    new Address("Street1","Street2", "City1","State1", 12345),
                    new RestaurantDetail(0, true, true, true, true, "Attire1", true, true, true, true, true, true,"Category1"),
                    2.50,
                    2.50),
            };

            context.RestaurantProfiles.AddOrUpdate(x => x.Id, (userRestaurantProfiles.ToArray()));
            context.SaveChanges();//Save the changes
            //RestaurantMenus seed
            var userRestaurantMenus = new List<RestaurantMenu>()
            {
                new RestaurantMenu(1, 1, 0001, "Menu1", true),
                new RestaurantMenu(2, 2, 0002, "Menu2", true),
                new RestaurantMenu(3, 3, 0003, "Menu3", true),
                new RestaurantMenu(4, 4, 0004, "Menu4", true),
                new RestaurantMenu(5, 5, 0005, "Menu5", true),
                new RestaurantMenu(6, 6, 0006, "Menu6", true),
                new RestaurantMenu(7, 7, 0007, "Menu7", true),
                new RestaurantMenu(8, 8, 0008, "Menu8", true),
                new RestaurantMenu(9, 9, 0009, "Menu9", true),
                new RestaurantMenu(10, 10, 0010, "Menu10", true)

            };
            context.RestaurantMenus.AddOrUpdate(x => x.Id, (userRestaurantMenus.ToArray()));
            context.SaveChanges();//Save the changes

            //RestaurantMenuItems seed
            var userRestaurantMenuItems = new List<RestaurantMenuItem>()
            {
               new RestaurantMenuItem(1, 1, 0001, "MenuItem1", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag1", "Description1", true),
               new RestaurantMenuItem(2, 2, 0002, "MenuItem2", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag2", "Description2", true),
               new RestaurantMenuItem(3, 3, 0003, "MenuItem3", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag3", "Description3", true),
               new RestaurantMenuItem(4, 4, 0004, "MenuItem4", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag4", "Description4", true),
               new RestaurantMenuItem(5, 5, 0005, "MenuItem5", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag5", "Description5", true),
               new RestaurantMenuItem(6, 6, 0006, "MenuItem6", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag6", "Description6", true),
               new RestaurantMenuItem(7, 7, 0007, "MenuItem7", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag7", "Description7", true),
               new RestaurantMenuItem(8, 8, 0008, "MenuItem8", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag8", "Description8", true),
               new RestaurantMenuItem(9, 9, 0009, "MenuItem9", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag9", "Description9", true),
               new RestaurantMenuItem(10, 10, 0010, "MenuItem10", 1.50m, "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   "Tag10", "Description10", true),
            };
            context.RestaurantMenuItems.AddOrUpdate(x => x.Id, (userRestaurantMenuItems.ToArray()));
            context.SaveChanges();//Save the changes

            var userBusinessHours = new List<BusinessHour>()
            {
                new BusinessHour(1, 1, "Monday", "12:23", "16:23"),
                new BusinessHour(2, 2, "Tuesday", "12:23", "16:23"),
                new BusinessHour(3, 3, "Wednesday", "12:23", "16:23"),
                new BusinessHour(4, 4, "Thursday", "12:23", "16:23"),
                new BusinessHour(5, 5, "Friday", "12:23", "16:23"),
                new BusinessHour(6, 6, "Saturday", "12:23", "16:23"),
                new BusinessHour(7, 7, "Sunday", "12:23", "16:23"),
                new BusinessHour(8, 8, "Monday", "12:23", "16:23"),
                new BusinessHour(9, 9, "Tuesday", "12:23", "16:23"),
                new BusinessHour(10, 10, "Wednesday", "12:23", "16:23")
            };
            context.BusinessHours.AddOrUpdate(x => x.Id, (userBusinessHours.ToArray()));
            context.SaveChanges();
        }
    }
}