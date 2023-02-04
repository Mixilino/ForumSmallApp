using ForumWebApi.Data.Interfaces;
using ForumWebApi.DataTransferObject;
using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using System;
using System.Xml.Linq;

namespace ForumWebApi.services.PostService
{
    public class PostService : IPostService
    {

        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ServiceResponse<List<PostResponseDto>> GetAll(UserResponseDto userDto)
        {
            var posts = _unitOfWork.PostRepository.GetAll();
            var user = _unitOfWork.UserRepository.GetById(userDto.UserId);
            var isAdmin = user?.role == UserRoles.Admin;
            return new ServiceResponse<List<PostResponseDto>>
            {
                Data = posts.Where(p=> isAdmin || p.PostState==PostStateEnum.Verified || p.UserId == userDto.UserId).Select(post => new PostResponseDto {
                    User = new UserResponseDto { UserName = post.User.UserName, UserId = post.User.UserId },
                    Comments = post.Comments.Select(c => new CommentResponseDto
                    {
                        CommentId = c.CommentId,
                        CommentText = c.CommentText,
                        DateCreated = c.DateCreated,
                        PostId = c.PostId,
                        UserId = c.UserId,
                        User = new UserResponseDto { UserId = c.UserId, UserName = c.User.UserName }
                    }).ToList(),
                    PostTitle= post.PostTitle,
                    PostText = post.PostText,
                    PostCategories = post.PostCategories.Select(pc => new PostCategoryReturnDto { PcId = pc.PcId, CategoryName = pc.CategoryName }).ToList(),
                    DatePosted = post.DatePosted,
                    Voted = post.Votes.Any(v => v.User.UserId == userDto.UserId),
                    Upvote = post.Votes.Any(v => v.User.UserId == userDto.UserId) ? post.Votes.Find(v => v.User.UserId == userDto.UserId).UpVote : false,
                    PostId = post.PostId,
                    PostState= post.PostState,
                    VotesCount = post.Votes.Count(v => v.UpVote) - post.Votes.Count(v => !v.UpVote)
                }).ToList(),
                Succes = true,
                Message = "Success"
            };
        }

        public ServiceResponse<PostResponseDto> Create(PostCreateDto postDto, UserResponseDto userDto)
        {
           
            var post = _unitOfWork.PostRepository.Add(postDto, userDto);
            var user = _unitOfWork.UserRepository.GetById(userDto.UserId);
            var isAdmin = user?.role == UserRoles.Admin;

            ServiceResponse<PostResponseDto> response = new ServiceResponse<PostResponseDto>();

            try
            {
                _unitOfWork.Save();
                UserResponseDto u = new UserResponseDto { UserName = post.User.UserName, UserId = post.User.UserId };
                response.Data = new PostResponseDto
                {
                    User = u,
                    Comments = new List<CommentResponseDto> { },
                    PostTitle = post.PostTitle,
                    PostText = post.PostText,
                    PostCategories = post.PostCategories.Select(pc => new PostCategoryReturnDto { PcId = pc.PcId, CategoryName = pc.CategoryName }).ToList(),
                    DatePosted = post.DatePosted,
                    Voted = false,
                    Upvote = false,
                    PostState = isAdmin ? PostStateEnum.Verified : PostStateEnum.In_Verification,
                    PostId = post.PostId,
                    VotesCount = 0
                };
                response.Succes = true;
                response.Message = "Post created succesfully";
            }
            catch (Exception)
            {
                response.Data = null;
                response.Succes = false;
                response.Message = "Error when creating post.";

            }
            return response;

        }

        public ServiceResponse<PostResponseDto> Change(PostChangeDto postDto, UserResponseDto userDto)
        {
            Post? post = _unitOfWork.PostRepository.Change(postDto, userDto);

            ServiceResponse<PostResponseDto> response = new ServiceResponse<PostResponseDto>();
            if (post == null)
            {

                response.Data = null;
                response.Succes = false;
                response.Message = "Post does not exist.";
                return response;
            }
            try
            {
                if (post.UserId != userDto.UserId)
                {
                    response.Data = null;
                    response.Succes = false;
                    response.Message = "Invalid user.";
                    return response;
                }
                _unitOfWork.Save();
                bool voted = post.Votes.Any(v => v.User.UserId == userDto.UserId);
                response.Data = new PostResponseDto
                {
                    User = new UserResponseDto { UserName = post.User.UserName, UserId=post.User.UserId },
                    Comments = post.Comments.Select(c => new CommentResponseDto
                    {
                        CommentId = c.CommentId,
                        CommentText = c.CommentText,
                        DateCreated = c.DateCreated,
                        PostId = c.PostId,
                        UserId = c.UserId,
                        User = new UserResponseDto { UserId = c.UserId, UserName = c.User.UserName }
                    }).ToList(),
                    PostTitle = post.PostTitle,
                    PostText = post.PostText,
                    PostCategories = post.PostCategories.Select(pc => new PostCategoryReturnDto { PcId = pc.PcId, CategoryName = pc.CategoryName }).ToList(),
                    DatePosted = post.DatePosted,
                    Voted = voted,
                    Upvote = voted ? post.Votes.Find(v => v.User.UserId == userDto.UserId).UpVote : false,
                    PostState= post.PostState,
                    PostId = post.PostId,
                    VotesCount = post.Votes.Count(v=>v.UpVote) - post.Votes.Count(v => !v.UpVote)
                };
                response.Succes = true;
                response.Message = "Post updated succesfully";
            }
            catch (Exception)
            {
                response.Data = null;
                response.Succes = false;
                response.Message = "Post failed to update.";

            }
            return response;
        }

        public ServiceResponse<bool> Delete(int PostId, UserResponseDto userDto)
        {
            var user = _unitOfWork.UserRepository.GetById(userDto.UserId);
            var isAdmin = user?.role == UserRoles.Admin;
            _unitOfWork.PostRepository.Delete(PostId, userDto, isAdmin);

            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                if (_unitOfWork.Save()!=0)
                {
                    response.Data = true;
                    response.Succes = true;
                    response.Message = "Post deleted succesfully";
                }
                else
                {
                    response.Data = false;
                    response.Succes = false;
                    response.Message = "Post does not exist.";
                }
            }
            catch (Exception)
            {
                response.Data = false;
                response.Succes = false;
                response.Message = "Post failed to delete.";

            }
            return response;
        }

        public ServiceResponse<PostResponseDto> Vote(int PostId, bool vote, UserResponseDto userDto)
        {
            Post? post = _unitOfWork.PostRepository.Vote(PostId, vote, userDto);

            ServiceResponse<PostResponseDto> response = new ServiceResponse<PostResponseDto>();
            if (post == null)
            {

                response.Data = null;
                response.Succes = false;
                response.Message = "Post does not exist.";
                return response;
            }
            try
            {
                _unitOfWork.Save();
                bool voted = post.Votes.Any(v => v.User.UserId == userDto.UserId);
                response.Data = new PostResponseDto
                {
                    User = new UserResponseDto { UserName = post.User.UserName, UserId = post.User.UserId },
                    Comments = post.Comments.Select(c => new CommentResponseDto
                    {
                        CommentId = c.CommentId,
                        CommentText = c.CommentText,
                        DateCreated = c.DateCreated,
                        PostId = c.PostId,
                        UserId = c.UserId,
                        User = new UserResponseDto { UserId = c.UserId, UserName = c.User.UserName }
                    }).ToList(),
                    PostTitle = post.PostTitle,
                    PostText = post.PostText,
                    PostCategories = post.PostCategories.Select(pc => new PostCategoryReturnDto { PcId = pc.PcId, CategoryName = pc.CategoryName }).ToList(),
                    DatePosted = post.DatePosted,
                    PostState= post.PostState,
                    Voted = voted,
                    Upvote = voted ? post.Votes.Find(v => v.User.UserId == userDto.UserId).UpVote : false,
                    PostId = post.PostId,
                    VotesCount = post.Votes.Count(v => v.UpVote) - post.Votes.Count(v => !v.UpVote)
                };
                response.Succes = true;
                response.Message = "Post updated succesfully";
            }
            catch (Exception)
            {
                response.Data = null;
                response.Succes = false;
                response.Message = "Post failed to update.";

            }
            return response;
        }

        public ServiceResponse<PostResponseDto> ChangeState(int PostId, PostStateEnum NewState, UserResponseDto userDto)
        {
            Post? post = _unitOfWork.PostRepository.ChangeState(PostId, NewState);

            ServiceResponse<PostResponseDto> response = new ServiceResponse<PostResponseDto>();
            if (post == null)
            {

                response.Data = null;
                response.Succes = false;
                response.Message = "Post does not exist.";
                return response;
            }
            try
            {
                _unitOfWork.Save();
                bool voted = post.Votes.Any(v => v.User.UserId == userDto.UserId);
                response.Data = new PostResponseDto
                {
                    User = new UserResponseDto { UserName = post.User.UserName, UserId = post.User.UserId },
                    Comments = post.Comments.Select(c => new CommentResponseDto
                    {
                        CommentId = c.CommentId,
                        CommentText = c.CommentText,
                        DateCreated = c.DateCreated,
                        PostId = c.PostId,
                        UserId = c.UserId,
                        User = new UserResponseDto { UserId = c.UserId, UserName = c.User.UserName }
                    }).ToList(),
                    PostTitle = post.PostTitle,
                    PostText = post.PostText,
                    PostCategories = post.PostCategories.Select(pc => new PostCategoryReturnDto { PcId = pc.PcId, CategoryName = pc.CategoryName }).ToList(),
                    DatePosted = post.DatePosted,
                    Voted = voted,
                    Upvote = voted ? post.Votes.Find(v => v.User.UserId == userDto.UserId).UpVote : false,
                    PostState = post.PostState,
                    PostId = post.PostId,
                    VotesCount = post.Votes.Count(v => v.UpVote) - post.Votes.Count(v => !v.UpVote)
                };
                response.Succes = true;
                response.Message = "Post updated succesfully";
            }
            catch (Exception)
            {
                response.Data = null;
                response.Succes = false;
                response.Message = "Post failed to update.";

            }
            return response;
        }
    }
}
