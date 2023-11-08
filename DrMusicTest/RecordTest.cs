using DrMusicRepo;
using System.Reflection;

namespace DrMusicTest
{
    [TestClass]
    public class RecordTest
    {
        private readonly Record _record = new Record() { Id = 1, Title = "Kenni is cool", Artist = "Kenni", Duration = 420, PublicationYear = 2021 };
        private readonly Record _titleNull = new Record() { Id = 2, Title = null, Artist = "Mille", Duration = 69, PublicationYear = 2023 };
        private readonly Record _titleTooShort = new Record() { Id = 3, Title = " ", Artist = "Mikkel", Duration = 180, PublicationYear = 2015 };
        private readonly Record _artistNull = new Record() { Id = 4, Title = "Mikkel is cool", Artist = null, Duration = 200, PublicationYear = 2017 };
        private readonly Record _artistTooShort = new Record() { Id = 5, Title = "Shero", Artist = " ", Duration = 45, PublicationYear = 2019 };
        private readonly Record _durationTooShort = new Record() { Id = 6, Title = "Mille is cool", Artist = "Mille", Duration = 29, PublicationYear = 2020 };
        private readonly Record _durationTooLong = new Record() { Id = 7, Title = "Best fries", Artist = "Mille & Mikkel", Duration = 800, PublicationYear = 2022 };
        private readonly Record _publicationYearTooEarly = new Record() { Id = 8, Title = "Jesus is cool", Artist = "Disciple", Duration = 280, PublicationYear = 1889 };
        private readonly Record _publicationYearTooLate = new Record() { Id = 9, Title = "Futuristic beats", Artist = "Roboman", Duration = 404, PublicationYear = 2077 };


        // Testing the methods.

        [TestMethod]
        public void ValidateTitleTest()
        {
            _record.ValidateTitle();
            Assert.ThrowsException<ArgumentNullException>(() => _titleNull.ValidateTitle());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _titleTooShort.ValidateTitle());
        }

        [TestMethod]
        public void ValidateArtistTest()
        {
            _record.ValidateArtist();
            Assert.ThrowsException<ArgumentNullException>(() => _artistNull.ValidateArtist());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _artistTooShort.ValidateArtist());
        }

        [TestMethod]
        public void ValidateDurationTest()
        {
            _record.ValidateDuration();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _durationTooShort.ValidateDuration());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _durationTooLong.ValidateDuration());
        }
        
        [TestMethod]
        public void ValidatePublicationYearTest()
        {
            _record.ValidatePublicationYear();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _publicationYearTooEarly.ValidatePublicationYear());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _publicationYearTooLate.ValidatePublicationYear());
        }


        // Testing the boundaries of the constraints.

        [TestMethod]
        [DataRow(30)]
        [DataRow(31)]
        [DataRow(300)]
        [DataRow(599)]
        [DataRow(600)]
        public void DurationGoodTest(int duration)
        {
            Record test = _record;
            test.Duration = duration;
            test.Validate();
        }

        [TestMethod]
        [DataRow(29)]
        [DataRow(601)]
        public void DurationBadTest(int duration)
        {
            Record test = _record;
            test.Duration = duration;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => test.Validate());
        }

        [TestMethod]
        [DataRow(1900)]
        [DataRow(1901)]
        [DataRow(1960)]
        [DataRow(2022)]
        [DataRow(2023)]
        public void PublicationYearGoodTest(int publicationYear)
        {
            Record test = _record;
            test.PublicationYear = publicationYear;
            test.Validate();
        }

        [TestMethod]
        [DataRow(1899)]
        [DataRow(2024)]
        public void PublicationYearBadTest(int publicationYear)
        {
            Record test = _record;
            test.PublicationYear = publicationYear;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => test.Validate());

        }
    }
}