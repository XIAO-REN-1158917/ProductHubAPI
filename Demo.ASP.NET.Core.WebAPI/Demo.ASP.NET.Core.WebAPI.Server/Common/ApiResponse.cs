//This is a demo of independently encapsulating a response format, demonstrating my understanding that,
//in general, a unified format should be established in team collaborations.
//This approach facilitates smoother communication between the front-end and back-end,
//enhancing the maintainability and scalability of the code.


namespace Demo.ASP.NET.Core.WebAPI.Server.Common
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; } // Status of response
        public string Message { get; set; } // Prompt
        public T Data { get; set; } // Data
        public object Metadata { get; set; } // Metadata（for example, pagination information.）

        public ApiResponse(bool status, string message, T data, object metadata = null)
        {
            Status = status;
            Message = message;
            Data = data;
            Metadata = metadata;
        }
    }
}
