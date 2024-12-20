using Bogus;
using ForumWebApi.Models;
using Microsoft.EntityFrameworkCore;
using ForumWebApi.Data;

namespace ForumWebApi.Data.Seeder
{
    public class DataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedData()
        {
            // Clear existing data
            _context.Votes.RemoveRange(_context.Votes);
            _context.Posts.RemoveRange(_context.Posts);
            _context.Users.RemoveRange(_context.Users);
            _context.PostCategories.RemoveRange(_context.PostCategories);
            await _context.SaveChangesAsync();

            // Generate Categories
            var categories = new List<PostCategory>();
            var categoryNames = new[] { "Technology", "Sports", "Politics", "Entertainment", "Science", 
                                      "Gaming", "Food", "Travel", "Health", "Business" };
            
            foreach (var name in categoryNames)
            {
                categories.Add(new PostCategory { CategoryName = name });
            }
            await _context.PostCategories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();

            // Generate Users
            byte[] passwordHash, passwordSalt;
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Password123!"));
            }

            var userFaker = new Faker<User>()
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.PasswordHash, f => passwordHash)
                .RuleFor(u => u.PasswordSalt, f => passwordSalt)
                .RuleFor(u => u.role, f => f.Random.Enum<UserRoles>());

            var users = userFaker.Generate(10);
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();

            // Generate Posts
            var postFaker = new Faker<Post>()
                .RuleFor(p => p.PostTitle, f => f.Lorem.Sentence())
                .RuleFor(p => p.PostText, f => f.Lorem.Paragraphs(3))
                .RuleFor(p => p.DatePosted, f => f.Date.Past(2))
                .RuleFor(p => p.ContentFlag, f => ContentFlagEnum.Normal)
                .RuleFor(p => p.User, f => f.PickRandom(users))
                .RuleFor(p => p.PostCategories, f => f.Make(f.Random.Int(1, 3), () => f.PickRandom(categories)).ToList());

            var posts = postFaker.Generate(150);
            await _context.Posts.AddRangeAsync(posts);
            await _context.SaveChangesAsync();

            // Generate some votes
            var random = new Random();
            foreach (var post in posts)
            {
                var numberOfVotes = random.Next(0, 20);
                var votes = new List<Vote>();
                
                var randomUsers = users.OrderBy(x => Guid.NewGuid()).Take(numberOfVotes);
                foreach (var user in randomUsers)
                {
                    votes.Add(new Vote
                    {
                        Post = post,
                        User = user,
                        UpVote = random.Next(0, 2) == 1
                    });
                }
                await _context.Votes.AddRangeAsync(votes);
            }
            await _context.SaveChangesAsync();
        }
    }
} 