using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.DTOs;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Iu_InstaShare_Api.MockDatas
{
    public static class MockSeeds
    {
        public static void Seeds(DataDbContext dbContext)
        {
            using var hmac = new HMACSHA512();

            dbContext.UserProfiles.Add(new Models.UserProfileModel
            {
                Id = 16,
                FirstName = "Dora",
                LastName = "Destiny",
                Email = "d@d.de",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Test,1.2")),
                PasswordSalt = hmac.Key
            });

            dbContext.UserProfiles.Add(new Models.UserProfileModel
            {
                Id = 17,
                FirstName = "Ernst",
                LastName = "Eber",
                Email = "e@e.de",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Test,1.2")),
                PasswordSalt = hmac.Key
            });

            dbContext.UserProfiles.Add(new Models.UserProfileModel
            {
                Id = 18,
                FirstName = "Freddy",
                LastName = "Fauna",
                Email = "f@f.de",
                City = "Frankfurt",
                Street = "Farweg 12",
                Zip = "32142",
                PhoneNumber = "1234567890",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Test,1.2")),
                PasswordSalt = hmac.Key
            });

            dbContext.Friends.Add(new Models.FriendsModel
            {
                Id = 1,
                UserId = 16,
                FriendId = 18,
                Status = FriendsStatusEnum.ACCEPTED
            });

            dbContext.Friends.Add(new Models.FriendsModel
            {
                Id = 2,
                UserId = 18,
                FriendId = 16,
                Status = FriendsStatusEnum.ACCEPTED
            });

            dbContext.Friends.Add(new Models.FriendsModel
            {
                Id = 3,
                UserId = 16,
                FriendId = 17,
                Status = FriendsStatusEnum.ASKED
            });

            dbContext.Books.Add(new Models.BookModel
            {
                Id = 1,
                Title = "Dumplin",
                Author = "Julie Murphy",
                ISBN = "9780062327192",
                PublishingYear = new DateTime(2018,03,21,02,00,00),
                Category = BookCategoryEnum.HUMOR,
                LendOut = true,
                UserId = 16
            });

            dbContext.Books.Add(new Models.BookModel
            {
                Id = 2,
                Title = "Dublin Street",
                Author = "Samantha Young",
                ISBN = "5360023452143",
                PublishingYear = new DateTime(2017,01,01,02,00,00),
                Category = BookCategoryEnum.MYSTERY,
                LendOut = false,
                UserId = 16
            });

            dbContext.Books.Add(new Models.BookModel
            {
                Id = 3,
                Title = "Eagle Strike",
                Author = "Anthony Horowith",
                PublishingYear = new DateTime(2013,01,01,02,00,00),
                Category = BookCategoryEnum.THRILLER,
                LendOut = false,
                UserId = 17
            });

            dbContext.Books.Add(new Models.BookModel
            {
                Id = 4,
                Title = "Funkeln der Ewigkeit",
                Author = "Jennifer L. Armentrout",
                PublishingYear = new DateTime(2015,01,01,02,00,00),
                Category = BookCategoryEnum.ROMANCE,
                LendOut = false,
                UserId = 18
            });

            dbContext.Lends.Add(new Models.LendModel
            {
                Id = 1,
                BorrowerId = 16,
                BookId = 4,
                LendStatus = LendStatusEnum.REQUESTMADE,
                Note = "Hey, klingt nach einem coolen Buch! Würde ich auch gerne lesen :D",
                LendFrom = new DateTime(2024,08,12,02,00,00),
                LendTo = new DateTime(2024,09,01,02,00,00)
            });

            dbContext.Lends.Add(new Models.LendModel
            {
                Id = 2,
                BorrowerId = 17,
                BookId = 1,
                LendStatus = LendStatusEnum.ACCEPTED,
                Note = "Bin gespannt...",
                LendFrom = new DateTime(2024,07,01,02,00,00),
                LendTo = new DateTime(2024,10,01,02,00,00)
            });

            dbContext.SaveChanges();
        }
    }
}
