using enemy_fsm;
using Godot;

public class EnemyJumper : Enemy
{
    [Export]
    public StateJump JumpState { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        JumpState.Initialize(this);
        Fsm.ChangeState(States.Idle);
    }
    
    protected override void StateControl()
    {
        switch (Fsm.CurrentState.Key)
        {
            case States.Idle: 
                if (IdleState.Count > IdleState.Duration) Fsm.ChangeState(States.Jump);
                break;
            case States.Jump:
                if (JumpState.Count > JumpState.Duration) Fsm.ChangeState(States.Idle);
                break;
        }
    }
}
