namespace GitGrub.GetUsGrub.Helpers
{
    public class CustomException : System.Exception
    {
        public CustomException() : base() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, System.Exception inner) : base(message, inner) { }
    }
}