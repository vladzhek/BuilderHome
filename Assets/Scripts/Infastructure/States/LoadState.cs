using Services;
using Zenject;

namespace Infastructure
{
    public class LoadState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        private StaticDataService _staticDataService;

        [Inject]
        private void Construct(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public LoadState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string scene)
        {
            InjectService.Instance.Inject(this);
            
            _sceneLoader.Load(scene, OnLoaded);
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }
    }
}