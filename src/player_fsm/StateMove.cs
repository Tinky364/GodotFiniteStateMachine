using Fsm;
using Godot;

namespace player_fsm
{
    public class StateMove : State<Player, Player.States>
    {
        [Export(PropertyHint.Range, "0,10,or_greater")]
        private float _moveDuration;

        private float _moveCount = 0;
        
        public override void Enter()
        {
            GD.Print($"{GetType()}: {Key}");
        }

        public override void Process(float delta)
        {
            if (_moveCount > _moveDuration)
            {
                Owner.Fsm.ChangeState(Player.States.Idle);
            }
            else _moveCount += delta;
        }

        public override void PhysicsProcess(float delta) { }

        public override void Exit()
        {
            _moveCount = 0;
        }

        public override void ExitTree() { }
    }
}