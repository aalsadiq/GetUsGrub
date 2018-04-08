using CSULB.GetUsGrub.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace CSULB.GetUsGrub.DataAccess.Migrations.UserDbContext
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// The Database Migration Configuration class.
    /// This file contains configurations for the UserContext.
    /// Contains a seed method to populate the database with users.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/30/2018
    /// </para>
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<CSULB.GetUsGrub.DataAccess.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\UserDbContext";
        }

        /// <summary>
        /// The Seed method.
        /// Populates the database with number of users specified by the for-loop.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/30/2018
        /// </para>
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(CSULB.GetUsGrub.DataAccess.UserContext context)
        {
            var validFoodTypes = new Collection<string>
            {
                "Mexican Food",
                "Italian Cuisine",
                "Thai Food",
                "Greek Cuisine",
                "Chinese Food",
                "Japanese Cuisine",
                "American Food",
                "Mediterranean Cuisine",
                "French Food",
                "Spanish Cuisine",
                "German Food",
                "Korean Food",
                "Vietnamese Food",
                "Turkish Cuisine",
                "Caribbean Food"
            };

            // Los Angeles city's geocoordinates are (34.0522,-118.2437)
            var geoCoordinates = new Collection<GeoCoordinates>
            {
                // Within 1 mile from LA
                new GeoCoordinates()
                {
                    Latitude = 34.047041,
                    Longitude = -118.256578
                },
                // Within 5 miles from LA
                new GeoCoordinates()
                {
                    Latitude = 34.069583,
                    Longitude = -118.276620
                },
                // Within 10 miles from LA
                new GeoCoordinates()
                {
                    Latitude = 34.013574,
                    Longitude = -118.336530
                },
                // Within 15 miles from LA
                new GeoCoordinates()
                {
                    Latitude = 33.846966,
                    Longitude = -118.313570
                },
            };

            var validDayOfWeek = new Collection<string>
            {
                "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
            };

            // maxNumberOfUsers must be an even number
            const int maxUsers = 50;
            // maxNumberOfSecurityQuestions must be 3
            const int maxClaimsPerUserClaims = 3;
            const int maxBusinessHours = 3;
            const int maxRestaurantMenus = 2;
            const int maxRestaurantMenuItems = 5;

            // Directory Paths
            const string directoryPathToUserProfileDisplayPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\ProfileImages\\";
            const string directoryPathToMenuItemPicture = "C:\\Users\\Angelica\\Documents\\GetUsGrub\\get-us-grub\\src\\assets\\RestaurantImages\\";

            // Instantiate Randomizer
            var randomizer = new Random();

            // Counters
            var restaurantCounter = 0;

            // Seeding users to the database where half are individual users and the other half are restaurant users
            for (var i = 1; i <= maxUsers; i++)
            {
                // AddorUpdate to UserAccount table
                context.UserAccounts.AddOrUpdate
                (
                    new UserAccount()
                    {
                        Id = i,
                        Username = $"username{i}",
                        Password = $"password{i}",
                        IsActive = true,
                        IsFirstTimeUser = false,
                        RoleType = "public"
                    }
                );
                context.SaveChanges();

                // AddorUpdate to AuthenticationToken table
                context.AuthenticationTokens.AddOrUpdate
                (
                    new AuthenticationToken()
                    {
                        Id = i,
                        Username = $"username{i}",
                        ExpiresOn = DateTime.UtcNow,
                        Salt = $"salt{i}",
                        TokenString = $"tokenString{i}"
                    }
                );

                // AddorUpdate to PasswordSalt table
                context.PasswordSalts.AddOrUpdate
                (
                    new PasswordSalt()
                    {
                        Id = i,
                        Salt = $"salt{i}"
                    }
                );

                for (var j = 1 + 3 * (i - 1); j <= 3 + 3 * (i - 1); j++)
                {
                    // AddorUpdate to SecurityQuestion table
                    context.SecurityQuestions.AddOrUpdate
                    (
                        new SecurityQuestion()
                        {
                            Id = j,
                            UserId = i,
                            // Total of 9 security questions to choose from
                            Question = randomizer.Next(1, 9+1),
                            Answer = $"answer{j}"
                        }
                    );

                    // AddorUpdate to SecurityAnswerSalt table
                    context.SecurityAnswerSalts.AddOrUpdate
                    (
                        new SecurityAnswerSalt()
                        {
                            Id = j,
                            Salt = $"salt{j}"
                        }
                    );
                }
                context.SaveChanges();

                // Creating a list of claims
                var claims = new List<Claim>();

                // Adding claims to the list of claims
                for (var j = 1; j <= maxClaimsPerUserClaims; j++)
                {
                    claims.Add(new Claim($"claimType{j}", $"claimValue{j}"));
                }

                // AddorUpdate to UserClaims table
                context.UserClaims.AddOrUpdate
                (
                    new UserClaims()
                    {
                        Id = i,
                        Claims = claims
                    }
                );

                // AddorUpdate to UserProfile table
                context.UserProfiles.AddOrUpdate
                (
                    new UserProfile()
                    {
                        Id = i,
                        DisplayPicture = $"{directoryPathToUserProfileDisplayPicture}{i}.png",
                        DisplayName = $"displayName{i}"
                    }
                );

                // Half of the max number of users will be restaurant users
                if (i > maxUsers / 2)
                {
                    restaurantCounter++;
                    var rand = randomizer.Next(0, geoCoordinates.Count);

                    // AddorUpdate to RestaurantProfile table
                    context.RestaurantProfiles.AddOrUpdate
                    (
                        new RestaurantProfile()
                        {
                            Id = i,
                            // Truncate phone number to a maximum of 13 characters
                            PhoneNumber = $"(562)123-456{i}".Substring(0, 13),
                            Address = new Address()
                            {
                                Street1 = $"{i} Street1",
                                Street2 = $"{i} Street2",
                                City = $"city{i}",
                                State = "CA",
                                Zip = 90711 + i
                            },
                            Details = new RestaurantDetail()
                            {
                                // 1 = $0 to $10, 2 = $10.01 to $50, 3 = $50.01+
                                AvgFoodPrice = randomizer.Next(1, 3+1),
                                FoodType = validFoodTypes[randomizer.Next(0, validFoodTypes.Count)],
                                HasReservations = true,
                                HasDelivery = true,
                                HasTakeOut = true,
                                AcceptCreditCards = true,
                                Attire = "casual",
                                ServesAlcohol = true,
                                HasOutdoorSeating = true,
                                HasTv = true,
                                HasDriveThru = true,
                                Caters = true,
                                AllowsPets = true
                            },
                            GeoCoordinates = new GeoCoordinates()
                            {
                                Latitude = geoCoordinates[rand].Latitude,
                                Longitude = geoCoordinates[rand].Longitude
                            }
                        }
                    );
                    context.SaveChanges();

                    for (var j = 1 + maxBusinessHours*(restaurantCounter-1); j <= maxBusinessHours + maxBusinessHours*(restaurantCounter-1); j++)
                    {
                        // AddorUpdate to BusinessHour table
                        context.BusinessHours.AddOrUpdate
                        (
                            new BusinessHour()
                            {
                                Id = j,
                                RestaurantId = i,
                                Day = validDayOfWeek[randomizer.Next(0, validDayOfWeek.Count)],
                                OpenTime = DateTime.UtcNow,
                                CloseTime = DateTime.UtcNow.AddHours(8)
                            }
                        );
                        context.SaveChanges();
                    }

                    for (var j = 1 + maxRestaurantMenus*(restaurantCounter-1); j <= maxRestaurantMenus + maxRestaurantMenus*(restaurantCounter-1); j++)
                    {
                        // AddorUpdate to RestaurantMenu table
                        context.RestaurantMenus.AddOrUpdate
                        (
                            new RestaurantMenu()
                            {
                                Id = j,
                                RestaurantId = i,
                                MenuName = $"menuName{j}",
                                IsActive = true
                            }
                        );

                        for (var k = 1 + maxRestaurantMenuItems*(j-1); k <= maxRestaurantMenuItems + maxRestaurantMenuItems*(j-1); k++)
                        {
                            // AddorUpdate to RestaurantMenuItem table
                            context.RestaurantMenuItems.AddOrUpdate
                            (
                                new RestaurantMenuItem()
                                {
                                    Id = k,
                                    MenuId = j,
                                    ItemName = $"itemName{k}",
                                    ItemPrice = 5.99m + k,
                                    ItemPicture = $"{directoryPathToMenuItemPicture}{i}.png",
                                    Tag = $"tag{k}",
                                    Description = $"description{k}",
                                    IsActive = true
                                }
                            );
                        }
                    }
                }
                context.SaveChanges();
            }
        }
    }
}