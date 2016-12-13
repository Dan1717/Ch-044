﻿using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DB;
using DAL.Interface;
using Model.DTO;
using AutoMapper;

namespace BAL.Manager
{
	public class FollowPriceManager : BaseManager,IFollowPriceManager
	{
		public FollowPriceManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		public List<PriceFollowerDTO> GetAll()
		{
			List<PriceFollowerDTO> prices = new List<PriceFollowerDTO>();
			foreach (var item in uOW.PriceFollowerRepo.All.ToList())
			{
				var price = Mapper.Map<PriceFollowerDTO>(item);
				prices.Add(price);
			}

			return prices;
		}

		public void Insert(PriceFollowerDTO model)
		{
			var item = Mapper.Map<PriceFollower>(model);

			var followPrices = uOW.PriceFollowerRepo.All.Where(x => x.Url == model.Url && x.Email == model.Email).ToList();
			if (!followPrices.Any())
			{
				uOW.PriceFollowerRepo.Insert(item);
				uOW.Save();
			}
		}
	}
}
