using System.Web.Http;

namespace SimpleMailApp
{
    public class Bootstrapper
    {
        public static void Run()
        {
            AutoMapperConfig.RegisterMappings();
            AutoFacConfig.Initialize(GlobalConfiguration.Configuration);
            
        }
    }
}