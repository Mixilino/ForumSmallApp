using FluentValidation;
using ForumWebApi.DataTransferObject.PostDto;
using ForumWebApi.Data.PostCategoryRepo;
using Microsoft.Extensions.Localization;
using ForumWebApi.Resources;

namespace ForumWebApi.Validators
{
    public class PostUpdateDtoValidator : AbstractValidator<PostUpdateDto>
    {
        private const int POST_TITLE_MIN_LENGTH = 3;
        private const int POST_TITLE_MAX_LENGTH = 50;
        private const int POST_TEXT_MIN_LENGTH = 10;
        private const int POST_TEXT_MAX_LENGTH = 1000;

        public PostUpdateDtoValidator(
            IStringLocalizer<ValidationMessages> localizer,
            IPostCategoryRepository postCategoryRepository)
        {
            RuleFor(x => x.PostTitle)
                .NotEmpty().WithMessage(localizer["TitleRequired"].Value)
                .Length(POST_TITLE_MIN_LENGTH, POST_TITLE_MAX_LENGTH)
                    .WithMessage(localizer["TitleLength", POST_TITLE_MIN_LENGTH, POST_TITLE_MAX_LENGTH].Value);

            RuleFor(x => x.PostText)
                .NotEmpty().WithMessage(localizer["ContentRequired"].Value)
                .Length(POST_TEXT_MIN_LENGTH, POST_TEXT_MAX_LENGTH)
                    .WithMessage(localizer["ContentLength", POST_TEXT_MIN_LENGTH, POST_TEXT_MAX_LENGTH].Value);

            RuleFor(x => x.PostCategoryIds)
                .NotEmpty()
                .WithMessage(localizer["CategoryRequired"].Value)
                .Must((categoryIds) =>
                {
                    if (categoryIds == null || !categoryIds.Any())
                        return false;

                    var existingCategories = postCategoryRepository.GetAll().Result;
                    return categoryIds.All(id => existingCategories.Any(c => c.PcId == id));
                })
                .WithMessage(localizer["CategoryInvalid"].Value);
        }
    }
} 