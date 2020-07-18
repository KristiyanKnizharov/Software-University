namespace BookShop
{
    using Data;
    using System;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using BookShop.Models.Enums;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);


            //Problem 02
            //string input = Console.ReadLine();
            //string result = GetBooksByAgeRestriction(db, input);

            //Problem 03
            //string result = GetGoldenBooks(db);

            //Problem 04
            //string result = GetBooksByPrice(db);

            //Problem 05
            //int data = int.Parse(Console.ReadLine());
            //string result = GetBooksNotReleasedIn(db, data);

            //Problem 06
            //string input = Console.ReadLine();
            //string result = GetBooksByCategory(db, input);

            //Problem 07
            //string date = Console.ReadLine();
            //string result = GetBooksReleasedBefore(db, date);

            //Problem 08
            //string input = Console.ReadLine();
            //string result = GetAuthorNamesEndingIn(db, input);

            //Problem 09
            //string input = Console.ReadLine();
            //string result = GetBookTitlesContaining(db, input);

            //Problem 10
            //string input = Console.ReadLine();
            //string result = GetBooksByAuthor(db, input);

            //Problem 11
            //int input = int.Parse(Console.ReadLine());
            //int result = CountBooks(db, input);

            //Problem 12
            //string result = CountCopiesByAuthor(db);

            //Problem 13
            //string result = GetTotalProfitByCategory(db);

            //Problem 14
            //string result = GetMostRecentBooks(db);
            
            //Problem 15
            //string result = IncreasePrices(db);

            //Problem 16
            //string result = RemoveBooks(db);

            //Console.WriteLine(result);
        }

        //Problem 02
        public static string GetBooksByAgeRestriction
            (BookShopContext context, string command)
        {
            StringBuilder sb = new StringBuilder();

            List<String> bookTitles = context
                .Books
                .AsEnumerable()
                .Where(b => b
                    .AgeRestriction.ToString().ToLower() == command.ToLower())
                .Select(b => b.Title)
                .OrderBy(bt => bt)
                .ToList();

            return String.Join(Environment.NewLine, bookTitles);
        }

        //Problem 03
        public static string GetGoldenBooks(BookShopContext context)
        {
            var bookTitles = context
                    .Books
                    .AsEnumerable()
                    .Where(b => b.EditionType == EditionType.Gold
                            && b.Copies < 5000)
                    .OrderBy(b => b.BookId)
                    .Select(b => b.Title)
                    .ToList();

            return string.Join(Environment.NewLine, bookTitles);
        }

        //Problem 04
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();
            var bookTitles = context
                    .Books
                    .AsEnumerable()
                    .Where(b => b.Price > 40)
                    .Select(book => new
                    {
                        book.Title,
                        book.Price
                    })
                    .OrderByDescending(b => b.Price)
                    .ToList();
            foreach (var book in bookTitles)
            {
                sb.AppendLine(book.Title + " - " + $"${book.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 05
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var bookTitles = context
                .Books
                .AsEnumerable()
                .Where(b => b.ReleaseDate.Value.Year.ToString() != year.ToString())
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in bookTitles)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 06
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var categories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            var book = new List<string>();

            foreach (var category in categories)
            {
                var curentBook = context.Books
                                  .Where(b => b.BookCategories.Any(c => c.Category.Name.ToLower() == category.ToLower()))
                                  .Select(b => new { b.Title })
                                  .ToList();

                foreach (var cb in curentBook)
                {
                    book.Add(cb.Title);
                }
            }

            foreach (var t in book.OrderBy(t => t))
            {
                sb.AppendLine(t);
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 07
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();

            var books = context.Books
                               .Where(b => b.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.CurrentCulture))
                               .Select(b => new { b.Title, b.EditionType, b.Price, b.ReleaseDate })
                               .OrderByDescending(b => b.ReleaseDate)
                               .ToList();


            foreach (var b in books)
            {
                sb.AppendLine($"{b.Title} - {b.EditionType} - ${b.Price:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 08
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var authors = context
                               .Authors
                               .AsEnumerable()
                               .Where(a => a.FirstName.EndsWith(input))
                               .Select(a => new
                               {
                                   Name = a.FirstName + " " + a.LastName
                               })
                               .OrderBy(a => a.Name)
                               .ToList();


            foreach (var author in authors)
            {
                sb.AppendLine($"{author.Name}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 09
        public static string GetBookTitlesContaining
            (BookShopContext context, string input)
        {
            var bookTitiles = context
                    .Books
                    .AsEnumerable()
                    .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                    .Select(b => b.Title)
                    .OrderBy(b => b)
                    .ToList();

            return string.Join(Environment.NewLine, bookTitiles);
        }

        //Problem 10
        public static string GetBooksByAuthor
            (BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var booksWithAuthor = context
                .Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(book => new
                {
                    Title = book.Title,
                    Author = $"({book.Author.FirstName} {book.Author.LastName})",
                    book.BookId
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in booksWithAuthor)
            {
                sb.AppendLine($"{book.Title} {book.Author}");
            }

            return sb.ToString().TrimEnd();

        }

        //Problem 11
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int bookTitiles = context
                                .Books
                                .AsEnumerable()
                                .Where(b => b.Title.Length > lengthCheck)
                                .Count();

            return bookTitiles;
        }

        //Problem 12
        public static string CountCopiesByAuthor(BookShopContext db)
        {
            StringBuilder sb = new StringBuilder();

            var bookTitiles = db
                                .Authors
                                .Select(a => new
                                {
                                    Name = $"{a.FirstName} {a.LastName}",
                                    Copies = a.Books.Select(b => b.Copies).Sum()
                                })
                                .OrderByDescending(b => b.Copies)
                                .ToList();

            foreach (var book in bookTitiles)
            {
                sb.AppendLine($"{book.Name} - {book.Copies}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 13
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var bookTitiles = context
                                .Categories
                                .Select(c => new
                                {
                                    c.Name,
                                    Copies = c.CategoryBooks
                                        .Select(cb => cb.Book.Copies).Sum(),
                                    Profit = c.CategoryBooks.Select(cb => cb.Book.Copies * cb.Book.Price).Sum()
                                })
                                .OrderByDescending(c => c.Profit)
                                .ToList();

            foreach (var book in bookTitiles)
            {
                sb.AppendLine($"{book.Name} ${book.Profit:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 14
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var books = context.Categories
                               .Select(c => new
                               {
                                   c.Name,
                                   booksNameYear = c.CategoryBooks.Select(b => new
                                   {
                                       b.Book.Title,
                                       b.Book.ReleaseDate
                                   }).OrderByDescending(y => y.ReleaseDate).Take(3)
                               })
                               .OrderBy(o => o.Name)
                               .ToList();

            var sb = new StringBuilder();

            foreach (var i in books)
            {
                sb.AppendLine($"--{i.Name}");

                foreach (var b in i.booksNameYear)
                {
                    sb.AppendLine($"{b.Title } ({b.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //Problem 14
        public static void IncreasePrices(BookShopContext context)
        {

            var prices = context
                    .Books
                    .Where(r => r.ReleaseDate.Value.Year < 2010);

            foreach (var p in prices)
            {
                p.Price += 5;
            }
            context.SaveChanges();
        }

        //Problem 15
        public static int RemoveBooks(BookShopContext context)
        {
            var entityForRemove = context
                                .Books
                                .Where(b => b.Copies < 4200);

            var result = entityForRemove.Count();

            context.Books.RemoveRange(entityForRemove);

            context.SaveChanges();


            return result;
        }
    }
}
