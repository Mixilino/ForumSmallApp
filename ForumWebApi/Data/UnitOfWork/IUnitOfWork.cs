using ForumWebApi.Data.CommentRepo;
using ForumWebApi.Data.PostCategoryRepo;
using ForumWebApi.Data.PostRepo;
using ForumWebApi.Data.UserRoleRepo;

namespace ForumWebApi.Data.Interfaces
{
    public interface IUnitOfWork
    {
        public IPostRepository PostRepository { get; set; }
        public IPostCategoryRepository PostCategoryRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        public int Save();
    }
}
