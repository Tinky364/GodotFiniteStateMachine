using enemy_fsm;
using Godot;

public class EnemyRusher : Enemy
{
    [Export]
    public StateRush RushState { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        RushState.Initialize(this);
        Fsm.ChangeState(States.Idle);
    }
    
    protected override void StateControl()
    {
        switch (Fsm.CurrentState.Key)
        {
            case States.Idle: 
                if (IdleState.Count > IdleState.Duration) Fsm.ChangeState(States.Rush);
                break;
            case States.Rush:
                if (RushState.Count > RushState.Duration) Fsm.ChangeState(States.Idle);
                break;
        }
    }
}
