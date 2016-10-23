﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL;
using BAL.Manager.ParseManagers;
using BAL.Interface;
using Model.DTO;

namespace WebApp.Controllers
{
    public class PhoneController : Controller
    {
        private IPhoneManager phoneManager;
        private IPhoneParseManager phoneParseManager;

        public PhoneController(IPhoneManager phoneManager, IPhoneParseManager phoneParseManager)
        {
            this.phoneManager = phoneManager;
            this.phoneParseManager = phoneParseManager;
        }
        // GET: Phone
        public ActionResult Index()
        {
            List<PhoneSimpleDTO> phones = phoneManager.GetAllPhones();
            return View(phones);
        }

        public ActionResult ConcretePhone(int id)
        {
            var phone = phoneManager.GetPhoneById(id);
            return View(phone);
        }

        //just for parsing and filling DB
        public ActionResult Load()
        {
            phoneParseManager.ParseGoodsFromCategory(@"http://www.moyo.ua/telecommunication/smart/");
            return View();
        }
    }
}