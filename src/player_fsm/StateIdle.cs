using Fsm;
using Godot;

namespace player_fsm
{
    public class StateIdle : State<Player, Player.States>
    {
        [Export(PropertyHint.Range, "0,10,or_greater")]
        private float _idleDuration = 2f;

        private float _idleCount = 0f;
        
        public override void Enter()
        {
            GD.Print($"{GetType()}: {Key}");
        }

        public override void Process(float delta)
        {
            if (_idleCount > _idleDuration)
            {
                Owner.Fsm.ChangeState(Player.States.Move);
            }
            else _idleCount += delta;
        }

        public override void PhysicsProcess(float delta) { }

        public override void Exit()
        {
            _idleCount = 0;
        }

        public override void ExitTree() { }
    }
}
