using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyPersonalDiary.Models
{
    public class EditRecordViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int RecordId { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}