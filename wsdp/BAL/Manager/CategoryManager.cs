﻿using BAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using Model.DB;
using log4net;
using Model.DTO;
using AutoMapper;

namespace BAL.Manager {
	public class CategoryManager : BaseManager, ICategoryManager {

		static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");

		public CategoryManager(IUnitOfWork uOW) : base(uOW)
		{
		}

		public CategoryDTO Get(int id, bool includeChildren = false)
		{
			var category = uOW.CategoryRepo.GetByID(id);
			if (category == null) return null;
			var result = Mapper.Map<CategoryDTO>(category);
			result.ChildrenCategory = new List<CategoryDTO>();

			if (includeChildren && category.ChildrenCategory != null) {
				foreach (var child in category.ChildrenCategory) {
					result.ChildrenCategory.Add(Get(child.Id, true));
				}
			}

			return result;
		}

		public int Add(string name, int parentId = -1)
		{
			var parent = parentId != -1 ? uOW.CategoryRepo.GetByID(parentId) : null;
			var newCategory = new Category() {Name = name, ParentCategory = parent};
			uOW.CategoryRepo.Insert(newCategory);
			uOW.Save();
			return newCategory.Id;
		}

		public bool Delete(int id)
		{
			try
			{
				var category = uOW.CategoryRepo.GetByID(id);
				if (category == null) return false;

				if (category.ChildrenCategory != null) {
					foreach (var children in category.ChildrenCategory) {
						children.ParentCategory = category.ParentCategory;
					}

					uOW.Save();
				}

				uOW.CategoryRepo.Delete(id);
				uOW.Save();
				return true;
			} catch (Exception ex) {
				logger.Error(ex.Message);
			}

			return false;
		}

		public bool Rename(int id, string name)
		{
			try {
				var category = uOW.CategoryRepo.GetByID(id);
				if (category == null) return false;
				category.Name = name;
				uOW.Save();
				return true;
			} catch (Exception ex) {
				logger.Error(ex.Message);
			}

			return false;
		}

		public bool ChangeParent(int id, int parentId) {
			try {
				var parent = uOW.CategoryRepo.GetByID(parentId);
				if (parent == null) return false;
				uOW.CategoryRepo.GetByID(id).ParentCategory = parent;
				uOW.Save();
				return true;
			} catch (Exception ex) {
				logger.Error(ex.Message);
			}

			return false;
		}
	}
}