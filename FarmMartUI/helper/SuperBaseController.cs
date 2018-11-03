using AutoMapper;
using FarmMartBLL.Core;
using FarmMartBLL.ServiceAPI;
using FarmMartDAL.Model;
using FarmMartUI.helper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FarmMartUI.helper
{
  
    public class SuperBaseController : Controller
    {
        private IRepositoryService<Farm> FarmService;
        private IRepositoryService<CropVariety> CropVarietyService;
        private IRepositoryService<AnimalGender> AnimalGengerService;
        private IRepositoryService<Livestock> LivetockService;
        private IRepositoryService<Measurement> MeasurementService;
        private readonly IRepositoryService<CropType> CropTypeService;
        

        public SuperBaseController()
        {
             FarmService = new FarmService();
            AnimalGengerService = new AnimalGenderService();
            CropVarietyService = new CropVarietyService();
            LivetockService = new LivestockService();
            MeasurementService = new MeasurementService();
            CropTypeService = new CropTypeService();
        }

        public IEnumerable<SelectListItem> GetMyFarm(int? selected)
        {
            string userId = User.Identity.GetUserId();

            if (!selected.HasValue)
                selected = 0;

            var allFarms = FarmService.Get().Where(x => x.ApplicationUserId == userId).ToList();
            allFarms.Insert(0, new Farm { Id = 0, FarmName = "Please Select" });
            return allFarms.Select(x => new SelectListItem { Text = x.FarmName, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        public IEnumerable<SelectListItem> GetAllLivestock(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allFarms = LivetockService.Get().ToList();
            allFarms.Insert(0, new Livestock { Id = 0, Name = "Please Select" });
            return allFarms.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        public IEnumerable<SelectListItem> GetMyAnimalGender(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allFarms = AnimalGengerService.Get().ToList();
            allFarms.Insert(0, new AnimalGender { Id = 0, Name = "Please Select" });
            return allFarms.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        public IEnumerable<SelectListItem> GetMeasurement(int? selected)
        {
            if (!selected.HasValue)
                selected = 0;

            var allMeasurement = MeasurementService.Get().ToList();
            allMeasurement.Insert(0, new Measurement { Id = 0, Name = "Please Select" });
            return allMeasurement.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == selected });
        }

        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }
        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }
        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }
        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }
        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
            ? (List<Alert>)TempData[Alert.TempDataKey]
            : new List<Alert>();
            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });
            TempData[Alert.TempDataKey] = alerts;
        }
        public void SendFeedBack(string from, string subjectHeader, string message)
        {
            string Mailbox = ConfigurationManager.AppSettings["MailBox"].ToString();
            string MailHost = ConfigurationManager.AppSettings["MailHost"].ToString();
            string MailPassword = ConfigurationManager.AppSettings["MailPassword"].ToString();

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(from);
                mail.To.Add(Mailbox);
                mail.Subject = subjectHeader;
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = MailHost;
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential("teslimbakare@gmail.com", MailPassword);
                    smtp.EnableSsl = true;

                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (Exception e)
                    {
                        var msg = e.Message;
                    }

                }
            }

        }
        public void SaveCropImage(object CropOrLivestockToSavePicture)
        {
            if (CropOrLivestockToSavePicture is CropVariety cropVariety)
            {
                SaveCrop(cropVariety);
            }
            //if (CropOrLivestockToSavePicture is LivestockBreed)
            //{

            //}
            void SaveCrop(CropVariety CropVariety)
            {
                if (Request.Files.Count > 0)
                {
                    int fileCount = Request.Files.Count;

                    for (int i = 0; i < fileCount; i++)
                    {
                        var file = Request.Files[i];

                        if (file.ContentLength == 0)
                            break;


                        var fileName = cropVariety.Id.ToString() + "_" + i.ToString() + "_" + Path.GetFileName(file.FileName);

                        var path = Path.Combine(Server.MapPath("~/CropProfile"), fileName);

                        try
                        {
                            file.SaveAs(path);
                            cropVariety.PhotoPath = fileName;
                            CropVarietyService.Update(cropVariety);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }
    }
}