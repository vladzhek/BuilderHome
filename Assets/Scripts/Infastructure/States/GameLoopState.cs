
namespace Infastructure
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        
        public GameLoopState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            InjectService.Instance.Inject(this);
            
            _sceneLoader.Load("Game", OnLoaded);
        }

        private void OnLoaded()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}