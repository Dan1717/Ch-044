﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using ExtendedXmlSerialization;
using log4net;
using Model.DB;
using Model.DTO;
using Model.Product;

namespace BAL.Manager.ParseManagers
{
    public class TVManager : BaseManager, ITVManager
    {
        public TVManager(IUnitOfWork uOW) : base(uOW)
        {
        }


        /// <summary>
        /// Get all TV's from database.
        /// </summary>
        /// <returns></returns>
        public List<TVDTO> GetAllTVs()
        {
            List<TVDTO> TVs = new List<TVDTO>();
            ExtendedXmlSerializer ser = new ExtendedXmlSerializer();
            foreach (var tvDb in uOW.GoodRepo.All.Where(g => g.Category == Common.Enum.GoodCategory.TV))
            {
                var tv = ser.Deserialize(tvDb.XmlData, typeof(TV)) as TV;
                tv.Id = tvDb.Id;
                TVs.Add(Mapper.Map<TVDTO>(tv));
            }

            return TVs;
        }

        /// <summary>
        /// Get TV by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TV GetTVById(int id)
        {
            Good good = null;
            ExtendedXmlSerializer ser = new ExtendedXmlSerializer();
            try
            {
                good = uOW.GoodRepo.All.Where(g => g.Id == id).First();
                if (good == null)
                    throw new Exception("Not Found");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            var tv = ser.Deserialize(good.XmlData, typeof(TV)) as TV;
            tv.Id = id;
            return tv;
        }
    }
}
