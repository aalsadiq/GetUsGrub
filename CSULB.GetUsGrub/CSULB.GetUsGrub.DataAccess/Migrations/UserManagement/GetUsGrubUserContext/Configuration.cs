namespace CSULB.GetUsGrub.DataAccess.Migrations.UserManagement.GetUsGrubUserContext
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
						MigrationsDirectory = @"Migrations\UserManagement\GetUsGrubUserContext";
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
								new RestaurantProfile()
								{
										Id = 1,
										PhoneNumber = "(111)111-1111",
										Address = new Address()
										{
												Street1 = "Street11",
												Street2 = "Street12",
												City = "City1",
												State = "State1",
												Zip = 11111 },
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
										Latitude = 1.11,
										Longitude = 1.11
								},
								new RestaurantProfile()
								{
										Id = 2,
										PhoneNumber = "(222)222-2222",
										Address = new Address()
										{
												Street1 = "Street21",
												Street2 = "Street22",
												City = "City1",
												State = "State1",
												Zip = 22222 },
										Details = new RestaurantDetail()
										{
												HasReservations = true,
												HasDelivery = true,
												HasTakeOut = true,
												AcceptCreditCards = true,
												Attire = "Attire2",
												ServesAlcohol = true,
												HasOutdoorSeating = true,
												HasTv = true,
												HasDriveThru = true,
												Caters = true,
												AllowsPets = true,
												Category = "Category2"
										},
										Latitude = 2.22,
										Longitude = 2.22
								},
								new RestaurantProfile()
								{
										Id = 3,
										PhoneNumber = "(333)333-3333",
										Address = new Address()
										{
												Street1 = "Street31",
												Street2 = "Street32",
												City = "City1",
												State = "State1",
												Zip = 33333 },
										Details = new RestaurantDetail()
										{
												HasReservations = true,
												HasDelivery = true,
												HasTakeOut = true,
												AcceptCreditCards = true,
												Attire = "Attire3",
												ServesAlcohol = true,
												HasOutdoorSeating = true,
												HasTv = true,
												HasDriveThru = true,
												Caters = true,
												AllowsPets = true,
												Category = "Category3"
										},
										Latitude = 3.33,
										Longitude = 3.33
								},
								new RestaurantProfile()
								{
										Id = 4,
										PhoneNumber = "(444)444-4444",
										Address = new Address()
										{
												Street1 = "Street41",
												Street2 = "Street42",
												City = "City1",
												State = "State1",
												Zip = 44444 },
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
												Category = "Category4"
										},
										Latitude = 4.44,
										Longitude = 4.44
								},
								new RestaurantProfile()
								{
										Id = 5,
										PhoneNumber = "(555)555-5555",
										Address = new Address()
										{
												Street1 = "Street51",
												Street2 = "Street52",
												City = "City1",
												State = "State1",
												Zip = 55555 },
										Details = new RestaurantDetail()
										{
												HasReservations = true,
												HasDelivery = true,
												HasTakeOut = true,
												AcceptCreditCards = true,
												Attire = "Attire2",
												ServesAlcohol = true,
												HasOutdoorSeating = true,
												HasTv = true,
												HasDriveThru = true,
												Caters = true,
												AllowsPets = true,
												Category = "Category5"
										},
										Latitude = 5.55,
										Longitude = 5.55
								},
								new RestaurantProfile()
								{
										Id = 6,
										PhoneNumber = "(666)666-6666",
										Address = new Address()
										{
												Street1 = "Street61",
												Street2 = "Street62",
												City = "City1",
												State = "State1",
												Zip = 66666 },
										Details = new RestaurantDetail()
										{
												HasReservations = true,
												HasDelivery = true,
												HasTakeOut = true,
												AcceptCreditCards = true,
												Attire = "Attire3",
												ServesAlcohol = true,
												HasOutdoorSeating = true,
												HasTv = true,
												HasDriveThru = true,
												Caters = true,
												AllowsPets = true,
												Category = "Category6"
										},
										Latitude = 6.66,
										Longitude = 6.66
								},
								new RestaurantProfile()
								{
										Id = 7,
										PhoneNumber = "(777)777-7777",
										Address = new Address()
										{
												Street1 = "Street71",
												Street2 = "Street72",
												City = "City1",
												State = "State1",
												Zip = 77777 },
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
												Category = "Category7"
										},
										Latitude = 7.77,
										Longitude = 7.77
								},
								new RestaurantProfile()
								{
										Id = 8,
										PhoneNumber = "(888)888-8888",
										Address = new Address()
										{
												Street1 = "Street81",
												Street2 = "Street82",
												City = "City1",
												State = "State1",
												Zip = 88888 },
										Details = new RestaurantDetail()
										{
												HasReservations = true,
												HasDelivery = true,
												HasTakeOut = true,
												AcceptCreditCards = true,
												Attire = "Attire2",
												ServesAlcohol = true,
												HasOutdoorSeating = true,
												HasTv = true,
												HasDriveThru = true,
												Caters = true,
												AllowsPets = true,
												Category = "Category8"
										},
										Latitude = 8.88,
										Longitude = 8.88
								},
								new RestaurantProfile()
								{
										Id = 9,
										PhoneNumber = "(999)999-9999",
										Address = new Address()
										{
												Street1 = "Street91",
												Street2 = "Street92",
												City = "City1",
												State = "State1",
												Zip = 99999 },
										Details = new RestaurantDetail()
										{
												HasReservations = true,
												HasDelivery = true,
												HasTakeOut = true,
												AcceptCreditCards = true,
												Attire = "Attire3",
												ServesAlcohol = true,
												HasOutdoorSeating = true,
												HasTv = true,
												HasDriveThru = true,
												Caters = true,
												AllowsPets = true,
												Category = "Category9"
										},
										Latitude = 9.99,
										Longitude = 9.99
								},
								new RestaurantProfile()
								{
										Id = 10,
										PhoneNumber = "(000)000-0000",
										Address = new Address()
										{
												Street1 = "Street01",
												Street2 = "Street02",
												City = "City1",
												State = "State1",
												Zip = 00000 },
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
												Category = "Category10"
										},
										Latitude = 0.01,
										Longitude = 0.01
								}
						};

						context.RestaurantProfiles.AddOrUpdate(x => x.Id, (userRestaurantProfiles.ToArray()));
						context.SaveChanges();//Save the changes
																	//RestaurantMenus seed
						var userRestaurantMenus = new List<RestaurantMenu>()
						{
								new RestaurantMenu() { Id = 1, RestaurantId = 1, MenuName = "Menu1", IsActive = true},
								new RestaurantMenu() { Id = 2, RestaurantId = 2, MenuName = "Menu2", IsActive = true},
								new RestaurantMenu() { Id = 3, RestaurantId = 3, MenuName = "Menu3", IsActive = true},
								new RestaurantMenu() { Id = 4, RestaurantId = 4, MenuName = "Menu4", IsActive = true},
								new RestaurantMenu() { Id = 5, RestaurantId = 5, MenuName = "Menu5", IsActive = true},
								new RestaurantMenu() { Id = 6, RestaurantId = 6, MenuName = "Menu6", IsActive = true},
								new RestaurantMenu() { Id = 7, RestaurantId = 7, MenuName = "Menu7", IsActive = true},
								new RestaurantMenu() { Id = 8, RestaurantId = 8, MenuName = "Menu8", IsActive = true},
								new RestaurantMenu() { Id = 9, RestaurantId = 9, MenuName = "Menu9", IsActive = true},

								new RestaurantMenu() { Id = 10, RestaurantId = 10, MenuName = "Menu10", IsActive = true},
								new RestaurantMenu() { Id = 11, RestaurantId = 1, MenuName = "Menu11", IsActive = false},
								new RestaurantMenu() { Id = 12, RestaurantId = 2, MenuName = "Menu12", IsActive = false},
								new RestaurantMenu() { Id = 13, RestaurantId = 3, MenuName = "Menu13", IsActive = false},
								new RestaurantMenu() { Id = 14, RestaurantId = 4, MenuName = "Menu14", IsActive = false},
								new RestaurantMenu() { Id = 15, RestaurantId = 5, MenuName = "Menu15", IsActive = false},
								new RestaurantMenu() { Id = 16, RestaurantId = 6, MenuName = "Menu16", IsActive = false},
								new RestaurantMenu() { Id = 17, RestaurantId = 7, MenuName = "Menu17", IsActive = false},
								new RestaurantMenu() { Id = 18, RestaurantId = 8, MenuName = "Menu18", IsActive = false},
								new RestaurantMenu() { Id = 19, RestaurantId = 9, MenuName = "Menu19", IsActive = false},
								new RestaurantMenu() { Id = 20, RestaurantId = 10, MenuName = "Menu20", IsActive = false}
						};
						context.RestaurantMenus.AddOrUpdate(x => x.Id, (userRestaurantMenus.ToArray()));
						context.SaveChanges();//Save the changes

						//RestaurantMenuItems seed
						var userRestaurantMenuItems = new List<RestaurantMenuItem>()
						{
							 new RestaurantMenuItem() { Id = 1, MenuId = 1,  ItemPrice = 1.50, ItemName = "Hamburger1",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag1", Description = "Description1", IsActive = true},
							 new RestaurantMenuItem() { Id = 11, MenuId = 1,  ItemPrice = 1.50, ItemName = "Hamburger11",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag11", Description = "Description1", IsActive = false},
							 new RestaurantMenuItem() { Id = 2, MenuId = 2,  ItemPrice = 1.50, ItemName = "Hamburger2",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag2", Description = "Description2", IsActive = true},
							 new RestaurantMenuItem() { Id = 3, MenuId = 3,  ItemPrice = 1.50, ItemName = "Hamburger3",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag3", Description = "Description3", IsActive = true},
							 new RestaurantMenuItem() { Id = 4, MenuId = 4,  ItemPrice = 1.50, ItemName = "Hamburger4",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag4", Description = "Description4", IsActive = true},
							 new RestaurantMenuItem() { Id = 5, MenuId = 5,  ItemPrice = 1.50, ItemName = "Hamburger5",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag5", Description = "Description5", IsActive = true},
							 new RestaurantMenuItem() { Id = 6, MenuId = 6,  ItemPrice = 1.50, ItemName = "Hamburger6",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag6", Description = "Description6", IsActive = true},
							 new RestaurantMenuItem() { Id = 7, MenuId = 7,  ItemPrice = 1.50, ItemName = "Hamburger7",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag7", Description = "Description7", IsActive = true},
							 new RestaurantMenuItem() { Id = 8, MenuId = 8,  ItemPrice = 1.50, ItemName = "Hamburger8",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag8", Description = "Description8", IsActive = true},
							 new RestaurantMenuItem() { Id = 9, MenuId = 9,  ItemPrice = 1.50, ItemName = "Hamburger9",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag9", Description = "Description9", IsActive = true},
							 new RestaurantMenuItem() { Id = 10, MenuId = 10,  ItemPrice = 1.50, ItemName = "Hamburger10",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
									 Tag = "Tag10", Description = "Description10", IsActive = true},
							 new RestaurantMenuItem() { Id = 10, MenuId = 11,  ItemPrice = 1.50, ItemName = "Hamburger11",
									 ItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\EmptyImage.png",
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
						context.BussinessHours.AddOrUpdate(x => x.Id, (userBussinessHours.ToArray()));
						context.SaveChanges();
				}
		}
}
