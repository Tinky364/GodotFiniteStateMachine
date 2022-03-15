using Fsm;
using Godot;

namespace enemy_fsm
{
    public class StateAttack : State<Enemy, Enemy.States>
    {
        [Export(PropertyHint.Range, "0,10,or_greater")]
        private float _attackDuration = 10f;

        private float _attackCount = 0f;
        
        public override void Enter()
        {
            GD.Print($"{GetType()}: {Key}");
        }

        public override void Process(float delta)
        {
            if (_attackCount > _attackDuration)
            {
                Owner.Fsm.ChangeState(Enemy.States.Idle);
            }
            else _attackCount += delta;
        }

        public override void PhysicsProcess(float delta) { }

        public override void Exit()
        {
            _attackCount = 0;
        }

        public override void ExitTree() { }
    }
}
