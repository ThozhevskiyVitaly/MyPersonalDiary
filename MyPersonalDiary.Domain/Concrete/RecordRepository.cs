using System;
using System.Collections.Generic;
using System.Linq;
using MyPersonalDiary.Domain.Abstract;
using MyPersonalDiary.Domain.Entities;
using System.Data.Entity;

namespace MyPersonalDiary.Domain.Concrete
{
    public class RecordRepository : IRecordRepository
    {
        private EfDbContext db;
        public RecordRepository(EfDbContext context)
        {
            db = context;
        }
        public void Create(Record item)=>db.Records.Add(item);
        public void Delete(int id)
        {
            Record record=db.Records.Find(id);
            if (record != null) db.Records.Remove(record);
        }
        public IEnumerable<Record> GetAll(string authorName)
        {
           List <Record> records= db.Records.Where(r => r.AuthorName == authorName).ToList();
           foreach (var r in records)
           {
                if (r.CanBeDeleted == true)
                {
                    if (!CanBeDeleted(r))
                    {
                        r.CanBeDeleted = false;
                        Update(r);
                        db.SaveChanges();
                    }
                }
           }
           return records;
        }
        public bool CanBeDeleted(Record record)=>(DateTime.Now - record.DateOfCreating).Days < 1;
        public List<Record> FilterByDate(DateTime date, string userName) =>(from record in GetAll(userName) where record.DateOfCreating.Date == date.Date select record).ToList();
        public Record Get(int id) => db.Records.Find(id);
        public void Update(Record record)
        {
            var updateRecord = Get(record.RecordId);
            updateRecord.ImageData = record.ImageData;
            updateRecord.ImageMimeType = record.ImageMimeType;
            updateRecord.Content = record.Content;
            db.Entry(updateRecord).State = EntityState.Modified;
        }
           
    }
}
