using NUnit.Framework;
using Projeto_NFe.Applications.Config.AutoMapper;

namespace Projeto_NFe.Application.Tests.Initializer
{
    [TestFixture]
    public class TestBase
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            AutoMapperConfig.Reset();
            AutoMapperConfig.Initialize();
        }
    }

}
