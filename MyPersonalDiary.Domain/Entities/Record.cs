using System;

namespace MyPersonalDiary.Domain.Entities
{
    public class Record
    {
        public int RecordId { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreating { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public string AuthorName { get; set; }
    }
}
