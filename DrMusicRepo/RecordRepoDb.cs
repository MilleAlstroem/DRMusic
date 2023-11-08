using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMusicRepo
{
    public class RecordRepoDb
    {
        private readonly RecordsDbContext _context;

        public RecordRepoDb(RecordsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Record> GetAll(string? titleIncludes = null, string? artistIncludes = null, int? durationMin = null, int? durationMax = null, int? publicationYearMin = null, int? publicationYearMax = null, string? orderBy = null)
        {
            IQueryable<Record> query = _context.Records.AsQueryable();
            if(titleIncludes != null)
            {
                query = query.Where(record => record.Title.Contains(titleIncludes));
            }
            if(artistIncludes != null)
            {
                query = query.Where(record => record.Artist.Contains(artistIncludes));
            }
            if(durationMin != null)
            {
                query = query.Where(record => record.Duration >= durationMin);
            }
            if(durationMax != null)
            {
                query = query.Where(record => record.Duration <= durationMax);
            }
            if(publicationYearMin != null)
            {
                query = query.Where(record => record.PublicationYear >= publicationYearMin);
            }
            if(publicationYearMax != null)
            {
                query = query.Where(record => record.PublicationYear <= publicationYearMax);
            }
            if(orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch(orderBy)
                {
                    case "title":
                    case "title_asc":
                        query = query.OrderBy(r => r.Title);
                        break;
                    case "title_desc":
                        query = query.OrderByDescending(r => r.Title);
                        break;
                    case "artist":
                    case "artist_asc":
                        query = query.OrderBy(r => r.Artist);
                        break;
                    case "artist_desc":
                        query = query.OrderByDescending(r => r.Artist);
                        break;
                    case "duration":
                    case "duration_asc":
                        query = query.OrderBy(r => r.Duration);
                        break;
                    case "duration_desc":
                        query = query.OrderByDescending(r => r.Duration);
                        break;
                    case "publicationYear":
                    case "publicationYear_asc":
                        query = query.OrderBy(r => r.PublicationYear);
                        break;
                    case "publicationYear_desc":
                        query = query.OrderByDescending(r => r.PublicationYear);
                        break;
                    default:
                        break;
                }
            }
            return query;
        }

        public Record? GetById(int id)
        {
            return _context.Records.FirstOrDefault(record => record.Id == id);
        }

        public Record Add(Record record)
        {
            record.Validate();
            _context.Records.Add(record);
            _context.SaveChanges();
            return record;
        }

        public Record? Remove(int id)
        {
            Record record = GetById(id);
            if(record != null)
            {
                _context.Records.Remove(record);
                _context.SaveChanges();
                return record;
            }
            return null;
        }

        public Record? Update(int id, Record newData)
        {
            newData.Validate();
            Record? recordToUpdate = GetById(id);
            if(recordToUpdate != null)
            {
                recordToUpdate.Title = newData.Title;
                recordToUpdate.Artist = newData.Artist;
                recordToUpdate.Duration = newData.Duration;
                recordToUpdate.PublicationYear = newData.PublicationYear;
                _context.SaveChanges();
                return recordToUpdate;
            }
            return null;
        }

    }
}
