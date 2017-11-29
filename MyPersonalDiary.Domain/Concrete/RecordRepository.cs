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
        public IEnumerable<Record> GetAll(string authorName)=> db.Records.Where(r=>r.AuthorName==authorName);
        public bool CanBeDeleted(Record record)=>(DateTime.Now - record.DateOfCreating).Days < 1;
        public List<Record> FilterByDate(DateTime date) =>(from record in db.Records.ToList() where record.DateOfCreating.Date == date.Date select record).ToList();
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
