using FluentValidation;
using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.Data.PostCategoryRepo;

namespace ForumWebApi.Validators
{
    public class PostCreateDtoValidator : AbstractValidator<PostCreateDto>
    {
        public PostCreateDtoValidator(IPostCategoryRepository postCategoryRepository)
        {
            RuleFor(x => x.PostTitle)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Title must be at least 3 characters long");

            RuleFor(x => x.PostText)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Description must be at least 3 characters long");

            RuleFor(x => x.PostCategoryIds)
                .NotEmpty()
                .WithMessage("At least one category must be selected")
                .Must((categoryIds) =>
                {
                    if (categoryIds == null || !categoryIds.Any())
                        return false;

                    var existingCategories = postCategoryRepository.GetAll().Result;
                    return categoryIds.All(id => existingCategories.Any(c => c.PcId == id));
                })
                .WithMessage("One or more selected categories do not exist");
        }
    }
} 