﻿using ForumWebApi.Data.UnitOfWork;
using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebApi.Data.PostCategoryRepo
{
    public class PostCategoryRepository : IPostCategoryRepository
    {
        private readonly DataContext _context;

        public PostCategoryRepository(DataContext context)
        {
            _context = context;
        }

        public PostCategory Add(string name)
        {
            var pc = new PostCategory { CategoryName = name };
            _context.PostCategories.Add(pc);

            return pc;
        }

        public int Delete(int id)
        {
            PostCategory pc = new PostCategory { PcId = id };
            _context.PostCategories.Attach(pc);
            _context.PostCategories.Remove(pc);

            return pc.PcId;
        }

        public async Task<List<PostCategory>> GetAll()
        {
            return await _context.PostCategories.ToListAsync();
        }

        public PostCategory? Rename(PostCategoryReturnDto categoryDto)
        {
            var category = _context.PostCategories.FirstOrDefault(c => c.PcId == categoryDto.PcId);
            if (category == null)
            {
                return null;
            }
            category.CategoryName = categoryDto.CategoryName;
            return category;
        }
    }
}
