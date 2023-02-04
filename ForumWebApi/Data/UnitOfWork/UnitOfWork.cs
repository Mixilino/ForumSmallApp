using ForumWebApi.Data.CommentRepo;
using ForumWebApi.Data.Interfaces;
using ForumWebApi.Data.PostCategoryRepo;
using ForumWebApi.Data.PostRepo;
using ForumWebApi.Data.UserRoleRepo;

namespace ForumWebApi.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        public IPostRepository PostRepository { get; set; }
        public IPostCategoryRepository PostCategoryRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        public UnitOfWork(DataContext context)
        {
            this.context = context;
            PostRepository = new PostRepository(context);
            PostCategoryRepository = new PostCategoryRepository(context);
            CommentRepository = new CommentRepository(context);
            UserRepository = new UserRepository(context);
        }

        public int Save()
        {
            return this.context.SaveChanges();
        }
    }
}
