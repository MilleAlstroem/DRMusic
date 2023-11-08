using System.Data;

namespace DrMusicRepo
{
    public class Record
    {
        public int Id { get; set; }
        public string? Title { get; set; } // Title not null and at least 1 character
        public string? Artist { get; set; } // Artist not null and at least 1 character
        public int Duration { get; set; } // Duration between 30 and 600 seconds
        public int PublicationYear { get; set; } // PublicationYear between 1900 and 2023

        public Record(string title, string artist, int duration, int publicationYear)
        {
            Title=title;
            Artist=artist;
            Duration=duration;
            PublicationYear=publicationYear;
        }

        public Record() { }

        public void ValidateTitle()
        {
            if (Title == null)
            {
                throw new ArgumentNullException("Title cannot be null");
            }
            if (Title.Length < 1)
            {
                throw new ArgumentOutOfRangeException("Title must be at least 1 character long");
            }
        }

        public void ValidateArtist()
        {
            if (Artist == null)
            {
                throw new ArgumentNullException("Artist cannot be null");
            }
            if (Artist.Length < 1)
            {
                throw new ArgumentOutOfRangeException("Artist must be at least 1 character long");
            }
        }

        public void ValidateDuration()
        {
            if (Duration < 30 || Duration > 600)
            {
                throw new ArgumentOutOfRangeException("Duration must be between 30 and 600 seconds");
            }
        }

        public void ValidatePublicationYear()
        {
            if (PublicationYear < 1900 || PublicationYear > 2023)
            {
                throw new ArgumentOutOfRangeException("PublicationYear must be between 1900 and 2023");
            }
        }

        public void Validate()
        {
            ValidateTitle();
            ValidateArtist();
            ValidateDuration();
            ValidatePublicationYear();
        }

    }
}