using ForumWebApi.DataTransferObject.CommentDto;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace ForumWebApi.Models
{
    /// <summary>
    /// Represents a response from a service operation, which can contain data of type T and a message.
    /// </summary>
    /// <typeparam name="T">The type of the data that will be returned in response.</typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Gets or sets the data that will be returned in response. This property can be null.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service operation was successful or not.
        /// </summary>
        public bool Succes { get; set; } = true;

        /// <summary>
        /// Gets or sets a message that describes the result of the service operation.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Determines whether the current instance of the ServiceResponse<T> class is equal to another instance.
        /// </summary>
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

        /// <summary>
        /// Returns a hash code for the current instance of the ServiceResponse<T> class.
        /// </summary>
        public override int GetHashCode()
        {
            return Succes.GetHashCode() ^ Message.GetHashCode();
        }

        /// <summary>
        /// Returns a string that represents the current instance of the ServiceResponse<T> class.
        /// </summary>
        public override string ToString()
        {
            return $"Customer: Succes={Succes}, Message={Message}, Data={Data}";
        }
    }
}
