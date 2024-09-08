using Services;
using Zenject;

namespace Infastructure
{
    public class BootstrapState : IState
    {
        private const string Bootstrap = "Bootstrap";
        private readonly GameStateMachine _stateMachine;
        
        private SceneLoader _sceneLoader;
        [Inject] private StaticDataService _staticDataService;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            InjectService.Instance.Inject(this);

            RegisterServices();
            _sceneLoader.Load(Bootstrap, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadState, string>("Menu");
        
        private void RegisterServices()
        {
            _staticDataService.Load();
            //TODO: ProgressService
        }

        public void Exit()
        {
            
        }
    }
}