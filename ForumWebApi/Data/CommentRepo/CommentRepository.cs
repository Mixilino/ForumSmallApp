using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using ForumWebApi.services.CommentService;
using Microsoft.EntityFrameworkCore;

namespace ForumWebApi.Data.CommentRepo
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public Comment Add(CommentCreateDto commentDto, UserResponseDto userDto)
        {
            Comment comment = new Comment
            {
                UserId = userDto.UserId,
                CommentText = commentDto.CommentText,
                PostId = commentDto.PostId,
                DateCreated = DateTime.Now,
            };
            _context.Comments.Add(comment);
            return comment;
        }


        public void Delete(int CommentId, UserResponseDto userDto)
        {

            Comment comment = _context.Comments.SingleOrDefault(c => c.CommentId == CommentId && c.UserId == userDto.UserId);
            if (comment == null)
            {
                return;
            }
            _context.Comments.Remove(comment);
        }

        public Comment Change(CommentChangeDto commentDto, UserResponseDto userDto)
        {
            Comment comment = _context.Comments.Include(c => c.User).SingleOrDefault(c => c.CommentId == commentDto.CommentId);
            if (comment == null)
            {
                return null;
            }
            comment.CommentText = commentDto.CommentText;
            return comment;
        }
    }
}
