using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPersonalDiary.Infrastructure;

namespace MyPersonalDiary.Models
{
    public class RecordListViewModel
    {
        public List<RecordViewModel> Records { get; set; }
        public Page Page { get; set; }
        public RecordListViewModel()
        {
            Records = new List<RecordViewModel>();
            Page = new Page();
        }

    }
}