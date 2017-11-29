using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalDiary.Models
{
    public class CreateRecordViewModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(500,MinimumLength =20,ErrorMessage ="Length between 20 and 500 characters!")]
        public string Content { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}