namespace WebApi_Shwetha.Services_LifetimeDemo
{
    public class ScopedService
    {
        private readonly string _guid;

        public ScopedService()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public string GetGuid()
        {
            return _guid;
        }
    }
}

