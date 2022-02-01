using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MusicEF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new SampleContextFactory().CreateDbContext(args))
            {
                if (!context.Artist.Any())
                {
                    AddToDB(context).GetAwaiter().GetResult();
                }

                Linq.First(context).GetAwaiter().GetResult();
                Linq.Second(context).GetAwaiter().GetResult();
                Linq.Third(context).GetAwaiter().GetResult();
            }
        }

        public static async Task AddToDB(ApplicationContext context)
        {
            var artists = new List<Artist>
            {
                new Artist { Name = "John Lennon", DateOfBirth = new DateTime(1940, 10, 9) },
                new Artist { Name = "Paul McCartney", DateOfBirth = new DateTime(1942, 6, 18) },
                new Artist { Name = "George Harrison", DateOfBirth = new DateTime(1943, 2, 25) },
                new Artist { Name = "Paul McCartney", DateOfBirth = new DateTime(1940, 7, 7) },
                new Artist { Name = "Sandra", DateOfBirth = new DateTime(1962, 5, 18) }
            };
            await context.Artist.AddRangeAsync(artists);
            var rock = new Genre { Title = "Rock" };
            var pop = new Genre { Title = "Pop" };
            var disco = new Genre { Title = "Disco" };
            var psychedelia = new Genre { Title = "Psychedelia" };
            var beat = new Genre { Title = "Beat" };
            await context.Genre.AddRangeAsync(rock, pop, disco, psychedelia, beat);
            var songs = new List<Song>
            {
                new Song { Title = "A Hard Day's Night", Genre = rock, Duration = new TimeSpan(0, 2, 34), ReleasedDate = new DateTime(1964, 7, 10) },
                new Song { Title = "And I Love Her", Genre = pop, Duration = new TimeSpan(0, 2, 32), ReleasedDate = new DateTime(1964, 7, 20) },
                new Song { Title = "Don't Be Aggressive", Genre = pop, Duration = new TimeSpan(0, 4, 45), ReleasedDate = new DateTime(1992, 1, 1) },
                new Song { Title = "Калинка-Малинка", Genre = pop, Duration = new TimeSpan(0, 4, 45), ReleasedDate = new DateTime(1892, 1, 1) }
            };
            await context.Song.AddRangeAsync(songs);
            var artistSong = new List<ArtistSong>
            {
                new ArtistSong { Song = songs.FirstOrDefault<Song>(p => p.Title == "A Hard Day's Night"), Artist = artists.FirstOrDefault<Artist>(p => p.Name == "John Lennon") }
            };
            await context.AddRangeAsync(artistSong);
            _ = context.SaveChangesAsync();
        }
    }
}
