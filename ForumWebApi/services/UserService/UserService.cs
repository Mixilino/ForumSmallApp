using ForumWebApi.Data.Interfaces;
using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using Microsoft.Extensions.Hosting;

namespace ForumWebApi.services.UserService
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ServiceResponse<List<UserRoleResponse>> GetAll()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            return new ServiceResponse<List<UserRoleResponse>>
            {
                Data = users.Select(user => new UserRoleResponse
                {
                    role = user.role,
                    UserId = user.UserId,
                    UserName = user.UserName
                }).ToList(),
                Succes = true,
                Message = "Success"
            };
        }

        public ServiceResponse<UserRoleResponse> ChangeRole(UserChangeRoleRequest userDto)
        {
            var user = _unitOfWork.UserRepository.ChangeRole(userDto);
            ServiceResponse<UserRoleResponse> response = new ServiceResponse<UserRoleResponse>();
            if (user == null)
            {

                response.Data = null;
                response.Succes = false;
                response.Message = "User does not exist.";
                return response;
            }
            try
            {
                _unitOfWork.Save();
                response.Data = new UserRoleResponse { role = user.role, UserId = user.UserId, UserName = user.UserName };
                response.Succes = true;
                response.Message = "User role changed";
            }
            catch (Exception)
            {
                response.Data = null;
                response.Succes = false;
                response.Message = "User failed to update.";

            }
            return response;
        }

    }
}
