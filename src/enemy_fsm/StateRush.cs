using Fsm;
using Godot;

namespace enemy_fsm
{
    public class StateRush : State<Enemy, Enemy.States>
    {
        [Export(PropertyHint.Range, "0,10,or_greater")]
        public float Duration { get; private set; } = 10f;

        public float Count { get; private set; } = 0f;

        public override void Enter()
        {
            GD.Print($"{GetType()}: {Key}");
        }

        public override void Process(float delta)
        {
            Count += delta;
        }

        public override void PhysicsProcess(float delta) { }

        public override void Exit()
        {
            Count = 0;
        }

        public override void ExitTree() { }
    }
}
