namespace WebApi_Shwetha.Services_LifetimeDemo
{
    public class TransientService
    {
        private readonly string _guid;

        public TransientService()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public string GetGuid()
        {
            return _guid;
        }
    }
}
    
