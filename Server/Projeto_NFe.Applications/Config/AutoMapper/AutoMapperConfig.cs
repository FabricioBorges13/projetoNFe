using AutoMapper;

namespace Projeto_NFe.Applications.Config.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(AutoMapperConfig));
            });
        }
        public static void Reset() => Mapper.Reset();
    }
}
