﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Jarvis.Models;
using Jarvis.ViewModel;
using Microsoft.AspNet.Identity;

namespace Jarvis.Controllers
{
    public class FRDController : Controller
    {
        private ApplicationDbContext _context;

        public FRDController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var frds = _context.FRDS.Include(c => c.User).ToList();

            var userFrds = _context.FRDS.Where(f => f.UserId == userId).Include(c => c.User);

            if (User.IsInRole(RoleName.CanManageFRD))
                return View("List", frds);

            return View("ReadOnlyList", userFrds);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var frd = _context.FRDS.Include(f => f.User).Include(f => f.Channels).Include(f => f.TargetAudiences).SingleOrDefault(f => f.Id == id);

            if (frd == null)
            {
                return HttpNotFound();
            }

            var viewModel = new FRDDetailsViewModel
            {
                FRDS = frd,
                UnitApprovals = _context.UnitApprovals.Where(u => u.FRDId == id).Include(u => u.Department)
            };

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FRD frd = _context.FRDS.Find(id);
            if (frd == null)
            {
                return HttpNotFound();
            }

            FRDviewModel viewModel = new FRDviewModel();
            viewModel.ChannelLists = new List<SelectList>();
            viewModel.AudienceLists = new List<SelectList>();

            foreach (var channelMapping in frd.FRDChannelMappings.OrderBy(c => c.ChannelNumber))
            {
                viewModel.ChannelLists.Add(new SelectList(_context.Channels, "ID", "Name",
                channelMapping.ChannelId));
            }
            for (int i = viewModel.ChannelLists.Count; i < Constants.NumberOfChannels; i++)
            {
                viewModel.ChannelLists.Add(new SelectList(_context.Channels, "ID", "Name"));
            }

            foreach (var audienceMapping in frd.FRDAudienceMappings.OrderBy(t => t.AudienceNumber))
            {
                viewModel.AudienceLists.Add(new SelectList(_context.TargetAudiences, "ID", "Name",
                audienceMapping.TargetAudienceId));
            }
            for (int i = viewModel.AudienceLists.Count; i < Constants.NumberOfTargetAudience; i++)
            {
                viewModel.AudienceLists.Add(new SelectList(_context.TargetAudiences, "ID", "Name"));
            }
            viewModel.Id = frd.Id;
            viewModel.Name = frd.Name;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FRDviewModel viewModel)
        {
            var frdToUpdate = _context.FRDS.Include(f => f.FRDChannelMappings).Include(f => f.FRDAudienceMappings).Where(f => f.Id == viewModel.Id).Single();
            if (TryUpdateModel(frdToUpdate, "", new string[] { "Name"}))
            {
                if (frdToUpdate.FRDChannelMappings == null)
                {
                    frdToUpdate.FRDChannelMappings = new List<FRDChannelMapping>();
                }

                if (frdToUpdate.FRDAudienceMappings == null)
                {
                    frdToUpdate.FRDAudienceMappings = new List<FRDAudienceMapping>();
                }

                string[] channels = viewModel.Channels.Where(c =>
                !string.IsNullOrEmpty(c)).ToArray();
                for (int i = 0; i < channels.Length; i++)
                {

                    var channelToEdit = frdToUpdate.FRDChannelMappings.Where(c =>
                    c.ChannelNumber == i).FirstOrDefault();

                    var channel = _context.Channels.Find(int.Parse(channels[i]));

                    if (channelToEdit == null)
                    {

                        frdToUpdate.FRDChannelMappings.Add(new FRDChannelMapping
                        {
                            ChannelNumber = i,
                            Channel = channel,
                            ChannelId = channel.Id
                        });
                    }

                    else
                    {

                        if (channelToEdit.ChannelId != int.Parse(channels[i]))
                        {

                            channelToEdit.Channel = channel;
                        }
                    }
                }

                for (int i = channels.Length; i < Constants.NumberOfChannels; i++)
                {
                    var channelMappingToEdit = frdToUpdate.FRDChannelMappings.Where(c =>
                    c.ChannelNumber == i).FirstOrDefault();

                    if (channelMappingToEdit != null)
                    {
                        _context.FRDChannelMappings.Remove(channelMappingToEdit);
                    }
                }

                string[] audiences = viewModel.TargetAudiences.Where(t => !string.IsNullOrEmpty(t)).ToArray();
                for (int i = 0; i < audiences.Length; i++)
                {

                    var audienceToEdit = frdToUpdate.FRDAudienceMappings.Where(t =>
                    t.AudienceNumber == i).FirstOrDefault();

                    var audience = _context.TargetAudiences.Find(int.Parse(audiences[i]));

                    if (audienceToEdit == null)
                    {

                        frdToUpdate.FRDAudienceMappings.Add(new FRDAudienceMapping
                        {
                            AudienceNumber = i,
                            TargetAudiences = audience,
                            TargetAudienceId = audience.Id
                        });
                    }

                    else
                    {

                        if (audienceToEdit.TargetAudienceId != int.Parse(audiences[i]))
                        {

                            audienceToEdit.TargetAudiences = audience;
                        }
                    }
                }

                for (int i = audiences.Length; i < Constants.NumberOfTargetAudience; i++)
                {
                    var audienceMappingToEdit = frdToUpdate.FRDAudienceMappings.Where(t =>
                    t.AudienceNumber == i).FirstOrDefault();

                    if (audienceMappingToEdit != null)
                    {
                        _context.FRDAudienceMappings.Remove(audienceMappingToEdit);
                    }
                }


                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public ActionResult GetItems(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var viewModel = new FRDItemsViewModel
            {
                Items = _context.Items.Where(u => u.FRDId == id).Include(u => u.User).Include(u => u.Comments.Select(c => c.User))
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetItems(NewCommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var newComment = new Comment();

                newComment.ItemId = comment.ItemId;
                newComment.DateAdded = DateTime.Now;
                newComment.Details = comment.Details;
                newComment.UserId = User.Identity.GetUserId();

                _context.Comments.Add(newComment);
                _context.SaveChanges();

                var viewModel = new FRDItemsViewModel
                {
                    Items = _context.Items.Where(u => u.FRDId == comment.FRDId).Include(u => u.User).Include(u => u.Comments.Select(c => c.User))
                };

            }

            return RedirectToAction("GetItems", comment.FRDId);
        }



        public ActionResult Create()
        {
            FRDviewModel viewModel = new FRDviewModel();        
            viewModel.ChannelLists = new List<SelectList>();
            viewModel.AudienceLists = new List<SelectList>();

            for (int i = 0; i < Constants.NumberOfChannels; i++)
            {
                viewModel.ChannelLists.Add(new SelectList(_context.Channels, "ID", "Name"));
            }

            for (int i = 0; i < Constants.NumberOfTargetAudience; i++)
            {
                viewModel.AudienceLists.Add(new SelectList(_context.TargetAudiences, "ID", "Name"));
            }

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( FRDviewModel viewModel)
        {
            FRD frd = new FRD();

            frd.Name = viewModel.Name;
            frd.ReferenceNumber = viewModel.ReferenceNumber;
            frd.isApproved = false;
            frd.UserId = User.Identity.GetUserId();
            frd.CreationDate = DateTime.Now;

            frd.FRDChannelMappings = new List<FRDChannelMapping>();
            frd.FRDAudienceMappings = new List<FRDAudienceMapping>();

            string[] channels = viewModel.Channels.Where(c => !string.IsNullOrEmpty(c)).ToArray();

            for (int i = 0; i < channels.Length; i++)
            {
                frd.FRDChannelMappings.Add(new FRDChannelMapping
                {
                    Channel = _context.Channels.Find(int.Parse(channels[i])),
                    ChannelNumber = i
                });
            }

            string[] audiences = viewModel.TargetAudiences.Where(t => !string.IsNullOrEmpty(t)).ToArray();

            for (int i = 0; i < audiences.Length; i++)
            {
                frd.FRDAudienceMappings.Add(new FRDAudienceMapping
                {
                    TargetAudiences = _context.TargetAudiences.Find(int.Parse(audiences[i])),
                    AudienceNumber = i
                });
            }


            if (ModelState.IsValid)
            {

                var units = _context.Departments.Where(d => d.CanApproveFRD == true);


                foreach (var unit in units)
                {
                    UnitApproval newUnit = new UnitApproval();

                    newUnit.DepartmentId = unit.Id;
                    newUnit.FRDId = frd.Id;

                    _context.UnitApprovals.Add(newUnit);
                }

                _context.FRDS.Add(frd);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

        
            viewModel.ChannelLists = new List<SelectList>();
            for (int i = 0; i < Constants.NumberOfChannels; i++)
            {
                viewModel.ChannelLists.Add(new SelectList(_context.Channels, "ID", "Name",
                viewModel.Channels[i]));
            }

            viewModel.AudienceLists = new List<SelectList>();
            for (int i = 0; i < Constants.NumberOfTargetAudience; i++)
            {
                viewModel.AudienceLists.Add(new SelectList(_context.TargetAudiences, "ID", "Name",
                viewModel.TargetAudiences[i]));
            }

            return View(viewModel);
        }

        public ActionResult Approve(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FRD frd = _context.FRDS.Include(f => f.User).Include(d => d.User.Department).Include(m => m.User.Department.Manager).SingleOrDefault(f => f.Id == id);
            if (frd == null)
            {
                return HttpNotFound();
            }

            return View(frd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Approve(FRDApproveMessage model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                if (userId != model.UserId)
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

                var ManagerEmail = _context.Users.FirstOrDefault(u => u.Id == model.ManagerID).Email;

                var body = "<h3>Request For FRD Approval</h3><p>The FRD <strong>" + model.FrdName + "</strong> by <strong>" + model.DemandOwnerEmail + "</strong> is currently awaiting your approval</p><p><a href='https://localhost:44391/frd/confirm/" + model.FrdId + "'>View FRD</a></p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(ManagerEmail));

                message.From = new MailAddress("cmpe406gradproject@outlook.com");

                message.Subject = "FRD Approval";
                message.Body = string.Format(body, model.FrdId, model.DemandOwnerEmail, model.FrdName, model.ManagerID);
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.Default;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "cmpe406gradproject@outlook.com",
                        Password = "c05L!vyCwKBL"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FRD frd = _context.FRDS.Include(f => f.User).SingleOrDefault(f => f.Id == id);

            if (frd == null)
            {
                return HttpNotFound();
            }

            return View(frd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirm([Bind(Include = "id,Name,ReferenceNumber,CreationDate,UserId,isApproved")] FRD frd)
        {

            if (ModelState.IsValid)
            {
                _context.Entry(frd).State = EntityState.Modified;
                _context.SaveChanges();


                _context.SaveChanges();

                var distributionList = _context.DistributionLists.Include(d => d.User).ToList();

                foreach (var member in distributionList)
                {
                    var memberEmail = member.User.Email;

                    var body = "<h3>Request For FRD Approval</h3><p>The FRD <strong> " + frd.Name + "</strong> is currently awaiting your approval</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(memberEmail));

                    message.From = new MailAddress("cmpe406gradproject@outlook.com");

                    message.Subject = "FRD Approval";
                    message.Body = string.Format(body);
                    message.IsBodyHtml = true;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.SubjectEncoding = System.Text.Encoding.Default;

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = "cmpe406gradproject@outlook.com",
                            Password = "c05L!vyCwKBL"
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "smtp-mail.outlook.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(message);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(frd);
        }

        public ActionResult UnitConfirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FRD frd = _context.FRDS.Include(f => f.User).SingleOrDefault(f => f.Id == id);

            if (frd == null)
            {
                return HttpNotFound();
            }

            return View(frd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnitConfirm(UnitConfirmViewModel unitInfo)
        {
            var frd = _context.FRDS.SingleOrDefault(f => f.Id == unitInfo.FrdId);

            if (ModelState.IsValid)
            {


                var UserId = User.Identity.GetUserId();

                var currentUser = _context.Users.SingleOrDefault(c => c.Id == UserId);

                var DepartmentId = currentUser.DepartmentId;

                var unit = _context.UnitApprovals.SingleOrDefault(u => u.DepartmentId == DepartmentId && u.FRDId == unitInfo.FrdId);

                unit.IsApproved = unitInfo.IsApproved;
                unit.PendingApproval = false;
                unit.ApprovalDate = DateTime.Now;
                unit.ApprovedBy = currentUser.Email;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(frd);
        }
    }

}