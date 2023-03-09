using ForumWebApi.Data.Interfaces;
using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using System.ComponentModel.Design;

namespace ForumWebApi.services.CommentService
{
    public class CommentService : ICommentService
    {

        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public CommentService()
        {
        }

        public ServiceResponse<CommentResponseDto> Create(CommentCreateDto commentDto, UserResponseDto userDto)
        {

            var comment = _unitOfWork.CommentRepository.Add(commentDto, userDto);

            ServiceResponse<CommentResponseDto> response = new ServiceResponse<CommentResponseDto>();

            try
            {
                _unitOfWork.Save();
                UserResponseDto u = new UserResponseDto { UserName = userDto.UserName, UserId = userDto.UserId };
                response.Data = new CommentResponseDto
                {
                    CommentId = comment.CommentId,
                    DateCreated = comment.DateCreated,
                    User = u,
                    CommentText = comment.CommentText,
                    PostId = comment.PostId,
                    UserId = comment.UserId
                };
                response.Succes = true;
                response.Message = "Comment created succesfully";
            }
            catch (Exception)
            {
                response.Data = null;
                response.Succes = false;
                response.Message = "Error when creating comment.";
            }
            return response;

        }

        public ServiceResponse<bool> Delete(int CommentId, UserResponseDto userDto)
        {
            _unitOfWork.CommentRepository.Delete(CommentId, userDto);

            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                if (_unitOfWork.Save() != 0)
                {
                    response.Data = true;
                    response.Succes = true;
                    response.Message = "Comment deleted succesfully";
                }
                else
                {
                    response.Data = false;
                    response.Succes = false;
                    response.Message = "Comment does not exist.";
                }
            }
            catch (Exception)
            {
                response.Data = false;
                response.Succes = false;
                response.Message = "Comment failed to delete.";

            }
            return response;
        }

        public ServiceResponse<CommentResponseDto> Change(CommentChangeDto commentDto, UserResponseDto userDto)
        {
            Comment? comment = _unitOfWork.CommentRepository.Change(commentDto, userDto);

            ServiceResponse<CommentResponseDto> response = new ServiceResponse<CommentResponseDto>();
            if (comment == null)
            {

                response.Data = null;
                response.Succes = false;
                response.Message = "Comment does not exist.";
                return response;
            }
            try
            {
                if (comment.UserId != userDto.UserId)
                {
                    response.Data = null;
                    response.Succes = false;
                    response.Message = "Invalid user.";
                    return response;
                }
                _unitOfWork.Save();
                response.Data = new CommentResponseDto
                {
                    CommentId = comment.CommentId,
                    CommentText = comment.CommentText,
                    DateCreated = comment.DateCreated,
                    PostId = comment.PostId,
                    User= new UserResponseDto { UserId=comment.User.UserId, UserName=comment.User.UserName},
                    UserId = comment.UserId,
                };
                response.Succes = true;
                response.Message = "Comment updated succesfully";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.Data = null;
                response.Succes = false;
                response.Message = "Comment failed to update.";
            }
            Console.WriteLine(response);
            return response;
        }

    }
}
