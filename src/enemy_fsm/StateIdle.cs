using Fsm;
using Godot;

namespace enemy_fsm
{
    public class StateIdle : State<Enemy, Enemy.States>
    {
        [Export(PropertyHint.Range, "0,10,or_greater")]
        private float _idleDuration = 3f;

        private float _idleCount = 0f;
            
        public override void Enter()
        {
            GD.Print($"{GetType()}: {Key}");
        }

        public override void Process(float delta)
        {
            if (_idleCount > _idleDuration)
            {
                Owner.Fsm.ChangeState(Enemy.States.Attack);
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