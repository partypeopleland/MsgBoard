﻿using System;
using System.IO;
using System.Web.Mvc;
using MsgBoard.Models.Entity;
using MsgBoard.Services;
using MsgBoard.ViewModel.Member;
using System.Transactions;

namespace MsgBoard.Controllers
{
    public class MemberController : BaseController
    {
        private readonly MemberService _memberService;

        public MemberController()
        {
            _memberService = new MemberService();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Title = "會員登入";
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberLoginViewModel model)
        {
            ViewBag.Title = "會員登入";

            if (!ModelState.IsValid) return View(model);

            var connection = ConnectionFactory.GetConnection();
            var loginResult = _memberService.CheckUserPassword(connection, model.Account, model.Password);
            Session["Auth"] = loginResult.Auth;
            Session["IsAdmin"] = loginResult.IsAdmin;

            if (loginResult.Auth)
            {
                return RedirectToAction("Index", "Post");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "新增會員";
            return View();
        }

        [HttpPost]
        public ActionResult Create(MemberCreateViewModel model)
        {
            ViewBag.Title = "新增會員";

            if (ModelState.IsValid)
            {
                using (var tranScope = new TransactionScope())
                {
                    using (var connection = ConnectionFactory.GetConnection())
                    {
                        // Table User
                        var fileName = _memberService.SaveMemberPic(model, Server.MapPath(FileUploadPath));
                        var user = _memberService.ConvertToUserEntity(model, $"{FileUploadPath}/{fileName}");
                        var userId = _memberService.CreateUser(connection, user);

                        // Table Password
                        var password = _memberService.ConvertToPassEntity(userId, user.Guid, model.Password);
                        _memberService.CreatePassword(connection, password);
                    }

                    tranScope.Complete();
                }

                return RedirectToAction("Index", "Post");
            }
            return View(model);
        }
    }
}