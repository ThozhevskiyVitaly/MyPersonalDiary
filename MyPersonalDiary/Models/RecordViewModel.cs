using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyPersonalDiary.Models
{
    public class RecordViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int RecordId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateOfCreating { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public bool CanBeDeleted { get; set; }
        
    }
}