namespace SimpleMailApp
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
               /* cfg.CreateMap<Models.SendMessageRequest, Models.Endpoint.SendGrid.MessageModel>()
                    .ForMember("subject", opt => opt.MapFrom(x => x.Subject))
                    .ForMember("subject", opt => opt.MapFrom(x => x.Subject))
                    .ForMember("subject", opt => opt.MapFrom(x => x.Subject));


                cfg.CreateMap<Models.SendMessageRequest, Models.Endpoint.SendGrid.ContentModel>()
                    .ForMember("value", opt => opt.MapFrom(x => x.Message))
                    .ForMember("type", opt => opt.UseValue("text/html"));*/



            });
        }
    }
}