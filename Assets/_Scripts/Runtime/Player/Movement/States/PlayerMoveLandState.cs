namespace ProtoPlat.Player.Movement.States
{
    public class PlayerMoveLandState : PlayerMoveRunState
    {
        public override string AnimationName => "land";

        public override bool CanExit()
        {
            return ElapsedTime > 0.2f;
        }
    }
}
