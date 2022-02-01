namespace MusicEF
{
    public class ArtistSong
    {
        public int ArtistID { get; set; }
        public Artist Artist { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
