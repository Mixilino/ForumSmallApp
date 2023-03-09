using ForumWebApi.DataTransferObject.CommentDto;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace ForumWebApi.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Succes { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ServiceResponse<T> other = (ServiceResponse<T>)obj;
            if (Data == null) return Succes == other.Succes && Message == other.Message && other.Data == null;
            return Succes == other.Succes && Message == other.Message && Data.Equals(other.Data);
        }

        public override int GetHashCode()
        {
            return Succes.GetHashCode() ^ Message.GetHashCode();
        }

        public override string ToString()
        {
            return $"Customer: Succes={Succes}, Message={Message}, Data={Data}";
        }
    }
}
