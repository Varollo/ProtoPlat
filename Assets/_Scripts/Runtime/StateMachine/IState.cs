namespace ProtoPlat.StateMachines
{
    public interface IState
    {
        void EnterState();
        void ExitState();
        bool CanEnter();
        bool CanExit();
    }
}
