using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace DrMusicRepo
{
    public class RecordRepoList
    {
        private int _nextId = 1;
        private readonly List<Record> _records = new();

        public RecordRepoList() { }

        public IEnumerable<Record> GetAll(string? titleIncludes = null, string? artistIncludes = null, int? durationMin = null, int? durationMax = null, int? publicationYearMin = null, int? publicationYearMax = null, string? orderBy = null)
        {
            IEnumerable<Record> result = new List<Record>(_records);
            if(titleIncludes != null)
            {
                result = result.Where(r => r.Title.Contains(titleIncludes));
            }
            if(artistIncludes != null)
            {
                result = result.Where(r => r.Artist.Contains(artistIncludes));
            }
            if(durationMin != null)
            {
                result = result.Where(r => r.Duration >= durationMin);
            }
            if(durationMax != null)
            {
                result = result.Where(r => r.Duration <= durationMax);
            }
            if(publicationYearMin != null)
            {
                result = result.Where(r => r.PublicationYear >= publicationYearMin);
            }
            if (publicationYearMax != null)
            {
                result = result.Where(r => r.PublicationYear <= publicationYearMax);
            }

            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "title":
                    case "title_asc":
                        result = result.OrderBy(r => r.Title);
                        break;
                    case "title_desc":
                        result = result.OrderByDescending(r => r.Title);
                        break;
                    case "artist":
                    case "artist_asc":
                        result = result.OrderBy(r => r.Artist);
                        break;
                    case "artist_desc":
                        result = result.OrderByDescending(r => r.Artist);
                        break;
                    case "duration":
                    case "duration_asc":
                        result = result.OrderBy(r => r.Duration);
                        break;
                    case "duration_desc":
                        result = result.OrderByDescending(r => r.Duration);
                        break;
                    case "publicationYear":
                    case "publicationYear_asc":
                        result = result.OrderBy(r => r.PublicationYear);
                        break;
                    case "publicationYear_desc":
                        result = result.OrderByDescending(r => r.PublicationYear);
                        break;
                }
            }
            return result;
        }  
        
        public Record? GetById(int id)
        {
            return _records.Find(r => r.Id == id);
        }

        public Record Add(Record record)
        {
            record.Validate();
            record.Id = _nextId++;
            _records.Add(record);
            return record;
        }

        public Record? Remove(int id)
        {
            Record? record = GetById(id);
            if(record != null)
            {
                _records.Remove(record);
                return record;
            }
            return record;
        }

        public Record? Update(int id , Record record)
        {
            record.Validate();
            Record? recordToUpdate = GetById(id);
            if(recordToUpdate != null)
            {
                recordToUpdate.Title = record.Title;
                recordToUpdate.Artist = record.Artist;
                recordToUpdate.Duration = record.Duration;
                recordToUpdate.PublicationYear = record.PublicationYear;
                return recordToUpdate;
            }
            return null;
        }
    }
}
