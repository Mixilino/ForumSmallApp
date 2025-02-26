﻿using ForumWebApi.Data.Interfaces;
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
            var user = _unitOfWork.UserRepository.GetById(userDto.UserId);
            var requestedUserId = user?.UserId;
            var isAdmin = user?.role == UserRoles.Admin;
            // Get base query
            var query = _unitOfWork.PostRepository.GetAll()
                .Where(p => isAdmin || p.ContentFlag == ContentFlagEnum.Normal || p.UserId == requestedUserId);

            var posts = query.OrderBy(p => p.PostId).ToList();

            // Map to DTOs
            var postDtos = posts.Select(post => new PostResponseDto
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
                Voted = post.Votes.Any(v => v.User.UserId == requestedUserId),
                Upvote = post.Votes.Any(v => v.User.UserId == requestedUserId) && post.Votes.First(v => v.User.UserId == requestedUserId).UpVote,
                VotesCount = post.Votes.Count(v => v.UpVote) - post.Votes.Count(v => !v.UpVote),
                PostId = post.PostId,
                ContentFlag = post.ContentFlag
            }).ToList();

            return new ServiceResponse<List<PostResponseDto>>
            {
                Data = postDtos,
                Message = "Success",
                Succes = true
            };
        }

        public ServiceResponse<PostPaginatedResponseDto> GetAllPaginated(UserResponseDto userDto, int? cursor = null, int pageSize = 10)
        {
            var user = _unitOfWork.UserRepository.GetById(userDto.UserId);
            var isAdmin = user?.role == UserRoles.Admin;

            // Get base query
            var query = _unitOfWork.PostRepository.GetAll()
                .Where(p => isAdmin || p.ContentFlag == ContentFlagEnum.Normal || p.UserId == userDto.UserId);

            // Get total count
            var totalPosts = query.Count();

            // Apply cursor pagination
            if (cursor.HasValue)
            {
                query = query.Where(p => p.PostId > cursor.Value);
            }

            // Get one extra post to determine if there's a next page
            var posts = query
                .OrderBy(p => p.PostId)
                .Take(pageSize + 1)
                .ToList();

            // Determine next cursor
            int? nextCursor = null;
            if (posts.Count > pageSize)
            {
                nextCursor = posts[pageSize - 1].PostId;
                posts = posts.Take(pageSize).ToList();
            }

            // Map to DTOs
            var postDtos = posts.Select(post => new PostResponseDto
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
                Voted = post.Votes.Any(v => v.User.UserId == userDto.UserId),
                Upvote = post.Votes.Any(v => v.User.UserId == userDto.UserId) ?
                    post.Votes.First(v => v.User.UserId == userDto.UserId).UpVote : false,
                VotesCount = post.Votes.Count(v => v.UpVote) - post.Votes.Count(v => !v.UpVote),
                PostId = post.PostId,
                ContentFlag = post.ContentFlag
            }).ToList();

            return new ServiceResponse<PostPaginatedResponseDto>
            {
                Data = new PostPaginatedResponseDto
                {
                    Posts = postDtos,
                    TotalPosts = totalPosts,
                    NextCursor = nextCursor
                },
                Message = "Success",
                Succes = true
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
                    PostId = post.PostId,
                    VotesCount = 0,
                    ContentFlag = ContentFlagEnum.Normal
                };
                response.Succes = true;
                response.Message = "Post created succesfully";
            }
            catch (Exception e)
            {
                response.Data = null;
                response.Succes = false;
                response.Message = "Error when creating post." + e.Message;

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
                    PostId = post.PostId,
                    VotesCount = post.Votes.Count(v => v.UpVote) - post.Votes.Count(v => !v.UpVote),
                    ContentFlag = post.ContentFlag
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
                if (_unitOfWork.Save() != 0)
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
                    Voted = voted,
                    Upvote = voted ? post.Votes.Find(v => v.User.UserId == userDto.UserId).UpVote : false,
                    PostId = post.PostId,
                    VotesCount = post.Votes.Count(v => v.UpVote) - post.Votes.Count(v => !v.UpVote),
                    ContentFlag = post.ContentFlag
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

        public ServiceResponse<PostResponseDto> FlagContent(int PostId, bool flag, UserResponseDto userDto)
        {
            var post = _unitOfWork.PostRepository.GetById(PostId);

            if (post == null)
            {
                return new ServiceResponse<PostResponseDto>
                {
                    Data = null,
                    Succes = false,
                    Message = "Post does not exist."
                };
            }

            try
            {
                post.ContentFlag = flag ? ContentFlagEnum.Flagged : ContentFlagEnum.Normal;
                _unitOfWork.Save();

                bool voted = post.Votes.Any(v => v.User.UserId == userDto.UserId);
                var response = new PostResponseDto
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
                    PostId = post.PostId,
                    ContentFlag = post.ContentFlag,
                    VotesCount = post.Votes.Count(v => v.UpVote) - post.Votes.Count(v => !v.UpVote)
                };

                return new ServiceResponse<PostResponseDto>
                {
                    Data = response,
                    Succes = true,
                    Message = flag ? "Post has been flagged" : "Post flag has been removed"
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<PostResponseDto>
                {
                    Data = null,
                    Succes = false,
                    Message = "Failed to update post flag status"
                };
            }
        }

        public ServiceResponse<PostPaginatedResponseDto> GetAllPaginatedWithFilters(UserResponseDto userDto, PostFilterDto filter)
        {
            var user = _unitOfWork.UserRepository.GetById(userDto.UserId);
            var isAdmin = user?.role == UserRoles.Admin;

            // Get filtered query from repository
            var query = _unitOfWork.PostRepository.GetFilteredPosts(filter, isAdmin, userDto.UserId);

            // Get total count
            var totalPosts = query.Count();

            // Apply cursor pagination
            if (filter.Cursor.HasValue)
            {
                query = query.Where(p => p.PostId > filter.Cursor.Value);
            }

            // Get one extra post to determine if there's a next page
            var posts = query
                .OrderBy(p => p.PostId)
                .Take(filter.PageSize + 1)
                .ToList();

            // Determine next cursor
            int? nextCursor = null;
            if (posts.Count > filter.PageSize)
            {
                nextCursor = posts[filter.PageSize - 1].PostId;
                posts = posts.Take(filter.PageSize).ToList();
            }

            // Map to DTOs
            var postDtos = posts.Select(post => new PostResponseDto
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
                Voted = post.Votes?.Any(v => v.User?.UserId == userDto.UserId) ?? false,
                Upvote = post.Votes?.FirstOrDefault(v => v.User?.UserId == userDto.UserId)?.UpVote ?? false,
                VotesCount = post.Votes?.Sum(v => v.UpVote ? 1 : -1) ?? 0,
                PostId = post.PostId,
                ContentFlag = post.ContentFlag
            }).ToList();

            return new ServiceResponse<PostPaginatedResponseDto>
            {
                Data = new PostPaginatedResponseDto
                {
                    Posts = postDtos,
                    TotalPosts = totalPosts,
                    NextCursor = nextCursor
                },
                Message = "Success",
                Succes = true
            };
        }
    }
}
