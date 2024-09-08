using Infastructure;
using Zenject;

namespace Services
{
    public class ServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InjectService.Instance.SetContainer(Container);
            
            BindService();
            BindModel();
        }
    
        private void BindModel()
        {

        }
    
        private void BindService()
        {
            Container.Bind<StaticDataService>().AsSingle();
        }
    }
}