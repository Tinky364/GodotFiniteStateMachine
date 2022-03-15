using Fsm;
using Godot;
using player_fsm;

public class Player : Node2D
{
    [Export]
    public StateIdle IdleState { get; private set; }
    [Export]
    public StateMove MoveState { get; private set; }
    
    public enum States { Idle, Move }
    public FiniteStateMachine<Player, States> Fsm { get; private set; }
    
    public override void _Ready()
    {
        base._Ready();
        Fsm = new FiniteStateMachine<Player, States>();
        IdleState.Initialize(this);
        MoveState.Initialize(this);
        Fsm.ChangeState(States.Idle);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        Fsm.Process(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Fsm.PhysicsProcess(delta);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Fsm.ExitTree();
    }
}