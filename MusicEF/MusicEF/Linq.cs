using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicEF
{
    public class Linq
    {
        public static async Task First(ApplicationContext context)
        {
            var result = await context.Song
                .Join(context.Genre, p => p.GenreId, e => e.Id, (p, e) => new { Id = p.Id, Title = p.Title, Genre = e.Title })
                .Join(context.ArtistSong, p => p.Id, e => e.SongId, (p, e) => new { Title = p.Title, ArtistId = e.ArtistID, Genre = p.Genre })
                .Join(context.Artist, p => p.ArtistId, e => e.Id, (p, e) => new { Title = p.Title, Genre = p.Genre, Artist = e.Name })
                .ToListAsync();
            Console.WriteLine("First task:");
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Title}, {item.Artist}, {item.Genre}");
            }
        }

        public static async Task Second(ApplicationContext context)
        {
            var result = await context.Song.GroupBy(p => p.Genre!.Title).Select(g => new { g.Key, Count = g.Count() }).ToListAsync();
            Console.WriteLine();
            Console.WriteLine("Second task:");
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}, {item.Count}");
            }
        }

        public static async Task Third(ApplicationContext context)
        {
            var minDateOfBirth = context.Artist.Max(p => p.DateOfBirth);
            var result = await context.Song.Where(p => p.ReleasedDate < minDateOfBirth).ToListAsync();
            Console.WriteLine();
            Console.WriteLine("Third task:");
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Title}");
            }
        }
    }
}
