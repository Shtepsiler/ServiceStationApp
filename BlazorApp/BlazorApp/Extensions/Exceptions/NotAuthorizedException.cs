namespace BlazorApp.Extensions.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public string Error { get; set; }

        public NotAuthorizedException() : this(default)
        {
        }

        public NotAuthorizedException(string error) : base() =>
            Error = error;
    }
}
