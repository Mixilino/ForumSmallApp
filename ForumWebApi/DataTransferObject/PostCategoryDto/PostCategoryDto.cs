using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using System.Data;

namespace ForumWebApi.DataTransferObject.PostCategoryDto
{
    public class PostCategoryReturnDto
    {
        public int PcId { get; set; }
        public string CategoryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            PostCategoryReturnDto other = (PostCategoryReturnDto)obj;
            return PcId == other.PcId && CategoryName == other.CategoryName;
        }

        public override int GetHashCode()
        {
            return PcId.GetHashCode() ^ CategoryName.GetHashCode();
        }
    }
}
