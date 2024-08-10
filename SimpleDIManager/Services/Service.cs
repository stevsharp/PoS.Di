namespace SimpleDIManager.Services
{
    public class Service : IService
    {
        private readonly ILogger _logger;

        public Service(ILogger logger)
        {
            _logger = logger;
        }

        public void Serve()
        {
            _logger.Log("Service is serving");
        }
    }
}
