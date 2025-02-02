using System.CodeDom;

namespace Demo.ASP.NET.Core.WebAPI.Server.Exceptions
{
    public class CustomNotFoundException : Exception
    {
        public CustomNotFoundException()
        {
            
        }

        public CustomNotFoundException(string message) : base(message)
        {
        }
    }
}
