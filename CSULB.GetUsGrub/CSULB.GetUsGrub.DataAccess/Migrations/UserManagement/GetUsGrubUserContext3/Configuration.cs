namespace CSULB.GetUsGrub.DataAccess.Migrations.UserManagement.GetUsGrubUserContext3
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
            MigrationsDirectory = @"Migrations\UserManagement\GetUsGrubUserContext3";
        }

        protected override void Seed(CSULB.GetUsGrub.DataAccess.UserContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var users = new List<UserAccount>()
            {
               new UserAccount ("User1", "password123!@", true, true, "Private"),
               new UserAccount ("User2", "password123!@", true, true, "Private"),
               new UserAccount ("User3", "password123!@", true, true,"Private"),
               new UserAccount ("User4", "password123!@", true, true,"Private"),
               new UserAccount ("User5", "password123!@", true, true,"Private"),
               new UserAccount ("User6", "password123!@", true, true,"Private"),
               new UserAccount ("User7", "password123!@", true, true,"Private"),
               new UserAccount ("User8", "password123!@", true, true,"Private"),
               new UserAccount ("User9", "password123!@", true, true,"Private"),
               new UserAccount ("User10", "password123!@", true, true,"Private")
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
               new PasswordSalt("salt10"){Id = 10 }
            };
            context.PasswordSalts.AddOrUpdate(x => x.Id, (userPasswordSalts.ToArray()));
            context.SaveChanges();//Save the changes
                                  //SecurityAnswerSalts
                                  ////SecurityQuestions seed

            var userSecurityQuestions = new List<SecurityQuestion>()
            {
                new SecurityQuestion() { UserId = 1, Question = 1, Answer = "A1"},
                new SecurityQuestion() { UserId = 1, Question = 2, Answer = "A2"},
                new SecurityQuestion() { UserId = 1, Question = 3, Answer = "A3"},

                new SecurityQuestion() {  UserId = 2, Question = 1, Answer = "A1"},
                new SecurityQuestion() {  UserId = 2, Question = 2, Answer = "A2"},
                new SecurityQuestion() {  UserId = 2, Question = 3, Answer = "A3"},

                new SecurityQuestion() { UserId = 3, Question = 1, Answer = "A1"},
                new SecurityQuestion() { UserId = 3, Question = 2, Answer = "A2"},
                new SecurityQuestion() { UserId = 3, Question = 3, Answer = "A3"},

                new SecurityQuestion() { UserId = 4, Question = 1, Answer = "A1"},
                new SecurityQuestion() {  UserId = 4, Question = 2, Answer = "A2"},
                new SecurityQuestion() { UserId = 4, Question = 3, Answer = "A3"},

                new SecurityQuestion() { UserId = 5, Question = 1, Answer = "A1"},
                new SecurityQuestion() { UserId = 5, Question = 2, Answer = "A2"},
                new SecurityQuestion() { UserId = 5, Question = 3, Answer = "A3"},

                new SecurityQuestion() { UserId = 6, Question = 1, Answer = "A1"},
                new SecurityQuestion() { UserId = 6, Question = 2, Answer = "A2"},
                new SecurityQuestion() { UserId = 6, Question = 3, Answer = "A3"},

                new SecurityQuestion() { UserId = 7, Question = 1, Answer = "A1"},
                new SecurityQuestion() { UserId = 7, Question = 2, Answer = "A2"},
                new SecurityQuestion() { UserId = 7, Question = 3, Answer = "A3"},

                new SecurityQuestion() { UserId = 9, Question = 1, Answer = "A1"},
                new SecurityQuestion() { UserId = 9, Question = 2, Answer = "A2"},
                new SecurityQuestion() { UserId = 9, Question = 3, Answer = "A3"},

                new SecurityQuestion() { UserId = 10, Question = 1, Answer = "A1"},
                new SecurityQuestion() { UserId = 10, Question = 2, Answer = "A2"},
                new SecurityQuestion() { UserId = 10, Question = 3, Answer = "A3"}
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


            var userTokens = new List<AuthenticationToken>()
            {
               new AuthenticationToken() { Id = 1, Username = "UserTokenName1", TokenString = "TokenString1", Salt = "Salt1",  ExpiresOn = DateTime.Now },
               new AuthenticationToken() { Id = 2, Username = "UserTokenName2", TokenString = "TokenString2", Salt = "Salt2",  ExpiresOn = DateTime.Now },
               new AuthenticationToken() { Id = 3, Username = "UserTokenName3", TokenString = "TokenString3", Salt = "Salt3",  ExpiresOn = DateTime.Now },
               new AuthenticationToken() { Id = 4, Username = "UserTokenName4", TokenString = "TokenString4", Salt = "Salt4",  ExpiresOn = DateTime.Now },
               new AuthenticationToken() { Id = 5, Username = "UserTokenName5", TokenString = "TokenString5", Salt = "Salt5", ExpiresOn = DateTime.Now },
               new AuthenticationToken() { Id = 6, Username = "UserTokenName6", TokenString = "TokenString6", Salt = "Salt6",  ExpiresOn = DateTime.Now },
               new AuthenticationToken() { Id = 7, Username = "UserTokenName7", TokenString = "TokenString7", Salt = "Salt7", ExpiresOn = DateTime.Now },
               new AuthenticationToken() { Id = 8, Username = "UserTokenName8", TokenString = "TokenString8", Salt = "Salt8",  ExpiresOn = DateTime.Now },
               new AuthenticationToken() { Id = 9, Username = "UserTokenName9", TokenString = "TokenString9", Salt = "Salt9",  ExpiresOn = DateTime.Now },
               new AuthenticationToken() { Id = 10, Username = "UserTokenName10", TokenString = "TokenString10", Salt = "Salt10",  ExpiresOn = DateTime.Now }
            };
            context.AuthenticationTokens.AddOrUpdate(x => x.Id, (userTokens.ToArray()));
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
            context.UserClaims.AddOrUpdate(x => x.Id, (userClaims.ToArray()));
            context.SaveChanges();//Save the changes

            //RestaurantProfiles seed
            //Id PhoneNumber Address Details
            var userRestaurantProfiles = new List<RestaurantProfile>()
            {
                new RestaurantProfile()
                {
                    Id = 1,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                },
                new RestaurantProfile()
                {
                    Id = 2,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                },
                new RestaurantProfile()
                {
                    Id = 3,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                },
                new RestaurantProfile()
                {
                    Id = 4,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                },
                new RestaurantProfile()
                {
                    Id = 5,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                },
                new RestaurantProfile()
                {
                    Id = 6,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                },
                new RestaurantProfile()
                {
                    Id = 7,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                },
                new RestaurantProfile()
                {
                    Id = 8,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                },
                new RestaurantProfile()
                {
                    Id = 9,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                },
                new RestaurantProfile()
                {
                    Id = 10,
                    PhoneNumber = "(555)555-555",
                    Address = new Address()
                    {
                        Street1 = "Street1",
                        Street2 = "Street2",
                        City = "City1",
                        State = "State1",
                        Zip = 12345 },
                    Details = new RestaurantDetail()
                    {
                        HasReservations = true,
                        HasDelivery = true,
                        HasTakeOut = true,
                        AcceptCreditCards = true,
                        Attire = "Attire1",
                        ServesAlcohol = true,
                        HasOutdoorSeating = true,
                        HasTv = true,
                        HasDriveThru = true,
                        Caters = true,
                        AllowsPets = true,
                        Category = "Category1"
                    },
                    Latitude = 2.50,
                    Longitude = 2.50
                }
            };

            context.RestaurantProfiles.AddOrUpdate(x => x.Id, (userRestaurantProfiles.ToArray()));
            context.SaveChanges();//Save the changes
            //RestaurantMenus seed
            var userRestaurantMenus = new List<RestaurantMenu>()
            {
                new RestaurantMenu() { Id = 1, RestaurantId = 1, MenuName = "Menu1"},
                new RestaurantMenu() { Id = 2, RestaurantId = 2, MenuName = "Menu2"},
                new RestaurantMenu() { Id = 3, RestaurantId = 3, MenuName = "Menu3"},
                new RestaurantMenu() { Id = 4, RestaurantId = 4, MenuName = "Menu4"},
                new RestaurantMenu() { Id = 5, RestaurantId = 5, MenuName = "Menu5"},
                new RestaurantMenu() { Id = 6, RestaurantId = 6, MenuName = "Menu6"},
                new RestaurantMenu() { Id = 7, RestaurantId = 7, MenuName = "Menu7"},
                new RestaurantMenu() { Id = 8, RestaurantId = 8, MenuName = "Menu8"},
                new RestaurantMenu() { Id = 9, RestaurantId = 9, MenuName = "Menu9"},
                new RestaurantMenu() { Id = 10, RestaurantId = 10, MenuName = "Menu10"}
            };
            context.RestaurantMenus.AddOrUpdate(x => x.Id, (userRestaurantMenus.ToArray()));
            context.SaveChanges();//Save the changes

            //RestaurantMenuItems seed
            var userRestaurantMenuItems = new List<RestaurantMenuItem>()
            {
               new RestaurantMenuItem() { Id = 1, MenuId = 1,  ItemPrice = 1.50,
                   ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag1", Description = "Description1", IsActive = true},
               new RestaurantMenuItem() { Id = 2, MenuId = 2,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag2", Description = "Description2", IsActive = true},
               new RestaurantMenuItem() { Id = 3, MenuId = 3,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag3", Description = "Description3", IsActive = true},
               new RestaurantMenuItem() { Id = 4, MenuId = 4,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag4", Description = "Description4", IsActive = true},
               new RestaurantMenuItem() { Id = 5, MenuId = 5,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag5", Description = "Description5", IsActive = true},
               new RestaurantMenuItem() { Id = 6, MenuId = 6,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag6", Description = "Description6", IsActive = true},
               new RestaurantMenuItem() { Id = 7, MenuId = 7,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag7", Description = "Description7", IsActive = true},
               new RestaurantMenuItem() { Id = 8, MenuId = 8,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag8", Description = "Description8", IsActive = true},
               new RestaurantMenuItem() { Id = 9, MenuId = 9,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag9", Description = "Description9", IsActive = true},
               new RestaurantMenuItem() { Id = 10, MenuId = 10,  ItemPrice = 1.50, ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
                   Tag = "Tag10", Description = "Description10", IsActive = true},
            };
            context.RestaurantMenuItems.AddOrUpdate(x => x.Id, (userRestaurantMenuItems.ToArray()));
            context.SaveChanges();//Save the changes

            var userBussinessHours = new List<BusinessHour>()
            {
                new BusinessHour() { Id = 1, RestaurantId = 1, Day = "Monday", OpenTime = "12:23", CloseTime = "16:23"},
                new BusinessHour() { Id = 2, RestaurantId = 2, Day = "Tuesday", OpenTime = "12:23", CloseTime = "16:23"},
                new BusinessHour() { Id = 3, RestaurantId = 3, Day = "Wednesday", OpenTime = "12:23", CloseTime = "16:23"},
                new BusinessHour() { Id = 4, RestaurantId = 4, Day = "Thursday", OpenTime = "12:23", CloseTime = "16:23"},
                new BusinessHour() { Id = 5, RestaurantId = 5, Day = "Friday", OpenTime = "12:23", CloseTime = "16:23"},
                new BusinessHour() { Id = 6, RestaurantId = 6, Day = "Saturday", OpenTime = "12:23", CloseTime = "16:23"},
                new BusinessHour() { Id = 7, RestaurantId = 7, Day = "Sunday", OpenTime = "12:23", CloseTime = "16:23"},
                new BusinessHour() { Id = 8, RestaurantId = 8, Day = "Monday", OpenTime = "12:23", CloseTime = "16:23"},
                new BusinessHour() { Id = 9, RestaurantId = 9, Day = "Tuesday", OpenTime = "12:23", CloseTime = "16:23"},
                new BusinessHour() { Id = 10, RestaurantId = 10, Day = "Wednesday", OpenTime = "12:23", CloseTime = "16:23"}
            };
            context.BusinessHours.AddOrUpdate(x => x.Id, (userBussinessHours.ToArray()));
            context.SaveChanges();
        }
    }
}
