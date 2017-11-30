using System;
using System.Collections.Generic;
using MyPersonalDiary.Domain.Entities;

namespace MyPersonalDiary.Domain.Abstract
{
    public interface IRecordRepository 
    {
        IEnumerable<Record> GetAll(string authorName);
        void Create(Record record);
        void Delete(int id);
        Record Get(int id);
        bool CanBeDeleted(Record record);
        List<Record> FilterByDate(DateTime date, string userName);
        void Update(Record record);
    }
}
