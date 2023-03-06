namespace ForumWebApi.DataTransferObject.UserDto
{
    public class UserResponseDto
    {
        public UserResponseDto()
        {

        }
        public UserResponseDto(int userId, string userName)
        {
            this.UserName = userName;
            this.UserId = userId;
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
