using AutoMapper;
using ForumWebApi.Data.Interfaces;
using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebApi.services.PostCategoryService
{
    public class PostCategoryService : IPostCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostCategoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ServiceResponse<PostCategoryReturnDto> Create(string name)
        {
            PostCategory pc = _unitOfWork.PostCategoryRepository.Add(name);
            ServiceResponse<PostCategoryReturnDto> response = new ServiceResponse<PostCategoryReturnDto>();

            try
            {
                _unitOfWork.Save();
                response.Data = new PostCategoryReturnDto { CategoryName = pc.CategoryName, PcId = pc.PcId };
                response.Succes = true;
                response.Message = "message";
            }
            catch (Exception)
            {
                response.Data = null;
                response.Succes = false;
                response.Message = "Category already exist.";

            }
            return response;

        }

        public ServiceResponse<List<PostCategoryReturnDto>> GetAll()
        {
            var categories = _unitOfWork.PostCategoryRepository.GetAll();

            return new ServiceResponse<List<PostCategoryReturnDto>>
            {
                Data = categories.Select(c => new PostCategoryReturnDto { CategoryName=c.CategoryName, PcId = c.PcId}).ToList(),
                Succes = true,
                Message = "Success"
            };
        }


        public ServiceResponse<int?> Delete(int id)
        {
            int deletedId = _unitOfWork.PostCategoryRepository.Delete(id);
            try
            {
                _unitOfWork.Save();
                return new ServiceResponse<int?>
                {
                    Data = deletedId,
                    Message = "Succes",
                    Succes = true
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<int?>
                {
                    Data = null,
                    Message = "Category does not exist",
                    Succes = false
                };
            }
            
        }

        public async Task<ServiceResponse<PostCategoryReturnDto>> Update(PostCategoryReturnDto categoryDto)
        {
            var category = await _unitOfWork.PostCategoryRepository.Rename(categoryDto);
            ServiceResponse<PostCategoryReturnDto> response = new ServiceResponse<PostCategoryReturnDto>();
            if(category == null)
            {
                response.Succes = false;
                response.Message = "Category not found";
                response.Data = null;
                return response;
            }
            try
            {
                _unitOfWork.Save();
                response.Succes = true;
                response.Message = "Category renamed";
                response.Data = new PostCategoryReturnDto { CategoryName = category.CategoryName, PcId = category.PcId };
            }
            catch (Exception)
            {
                response.Succes = false;
                response.Message = "Error";
                response.Data = null;

            }
            return response;
        }
    }
}
