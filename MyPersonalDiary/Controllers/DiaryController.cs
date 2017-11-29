using System.Collections.Generic;
using AutoMapper;
using System;
using MyPersonalDiary.Infrastructure.Abstract;
using MyPersonalDiary.Domain.Entities;
using MyPersonalDiary.Domain.Abstract;
using MyPersonalDiary.Infrastructure;
using MyPersonalDiary.Models;
using System.Web.Mvc;
using System.Web;
using System.Linq;
using MyPersonalDiary.Domain.Concrete;

namespace MyPersonalDiary.Controllers
{
    [Authorize]
    public class DiaryController : Controller
    {
        IAuthProvider authProvider;
        IUnitOfWork repository;
        const int NumPerPage = 5;
        static RecordListViewModel records = new RecordListViewModel();
        public DiaryController(IAuthProvider auth, IUnitOfWork repository)
        {
            authProvider = auth;
            this.repository = repository;
        }
        public ViewResult List(int page = 1)
        {
             LoadRecords(User.Identity.Name, page);
             return View("Records",records);
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return PartialView();
        }
         [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(Mapper.Map<LoginViewModel,User>(model)))
                {
                    LoadRecords(model.Name);
                    return View("Records",records);
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (repository.Users.Exist(model.Name))
                {
                    ModelState.AddModelError("", "That name was taken!");
                    return View();
                }
                repository.Users.Create(Mapper.Map<RegisterViewModel, User>(model));
                repository.Save();
                return View("Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            authProvider.SignOut();
            return View("Login");
        }
        public ViewResult Records()
        {
            LoadRecords(User.Identity.Name);
            return View("Records", records);
        }
        public ActionResult CreateRecord()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRecord(CreateRecordViewModel createdRecord, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Record record = Mapper.Map<CreateRecordViewModel, Record>(createdRecord);
               
                if (image != null)
                {
                    record.ImageMimeType = image.ContentType;
                    record.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(record.ImageData, 0, image.ContentLength);
                }
                var user = repository.Users.Get(User.Identity.Name);
                record.DateOfCreating = DateTime.Now;
                record.AuthorName = user.Name;
                repository.Records.Create(record);
                repository.Save();
                LoadRecords(user.Name, records.Page.CurrentPage);
                return View("Records", records);
            }
            else
            {         // there is something wrong with the data values         
                return View(createdRecord);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Mapper.Map<Record, EditRecordViewModel>(repository.Records.Get(id));
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditRecordViewModel editedRecord, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Record record = Mapper.Map<EditRecordViewModel, Record>(editedRecord);

                if (image != null)
                {
                    record.ImageMimeType = image.ContentType;
                    record.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(record.ImageData, 0, image.ContentLength);
                }
                var user = repository.Users.Get(User.Identity.Name);
                record.AuthorName = user.Name;
                repository.Records.Update(record);
                repository.Save();
                LoadRecords(user.Name, records.Page.CurrentPage);
                return View("Records", records);
            }
            else
            {      
                // there is something wrong with the data values
                return View(editedRecord);
            }
        }
        public ViewResult DeleteRecord(int id)
        {
              repository.Records.Delete(id);
              repository.Save();
              LoadRecords(User.Identity.Name,records.Page.CurrentPage, true);
              return View("Records",records);
        }
        [HttpPost]
        public ActionResult FilterRecords(DateTime date)
        {
            LoadRecords(User.Identity.Name, date);
            return View("Records",records);
        }
        [NonAction]
        private void LoadRecords(string userName, int page=1,bool deleteForm=false)
        {
            int count = page;
            var findedRecords = repository.Records.GetAll(userName);
            if (deleteForm)
            {
                if (findedRecords.Count()==(page-1)*NumPerPage) count = page - 1;
                else count = page;
            }
            List <RecordViewModel> loadRecords = Mapper.Map<IEnumerable<Record>, List<RecordViewModel>>(findedRecords.Skip((count - 1) * NumPerPage).Take(NumPerPage));
           
            records.Records = CanDelete(loadRecords);
            records.Page = new Page { CurrentPage = count, ItemsPerPage = NumPerPage, TotalItems =findedRecords.Count()};
        }
        [NonAction]
        private void LoadRecords(string userName, DateTime date, int page=1)
        {
            var findedRecords = repository.Records.FilterByDate(date);
            List<RecordViewModel> filteredRecords = Mapper.Map<IEnumerable<Record>, List<RecordViewModel>>(findedRecords.Skip((page - 1) * NumPerPage).Take(NumPerPage));
            records.Records = CanDelete(filteredRecords);
            records.Page = new Page { CurrentPage = page, ItemsPerPage = NumPerPage, TotalItems = findedRecords.Count() };
        }
       
        public FileContentResult GetImage(int recordId)
        {
            Record record = repository.Records.Get(recordId);
            if (record != null&&record.ImageData!=null)
            {
                return File(record.ImageData, record.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        [NonAction]
        public bool CanBeDeleted(Record record) => repository.Records.CanBeDeleted(record);
        [NonAction]
        public List<RecordViewModel> CanDelete(List<RecordViewModel> records)
        {
            foreach (var r in records)
            {
                if (!r.CanBeDeleted)
                {
                    var record = Mapper.Map<RecordViewModel, Record>(r);
                    r.CanBeDeleted = CanBeDeleted(record);
                }
            }
            return records;
        }
    }
}
