using enemy_fsm;
using Fsm;
using Godot;

public abstract class Enemy : Node2D
{
    [Export]
    public StateIdle IdleState { get; private set; }
    
    public enum States { Idle, Jump, Rush }
    public FiniteStateMachine<Enemy, States> Fsm { get; private set; }
    
    public override void _Ready()
    {
        Fsm = new FiniteStateMachine<Enemy, States>();
        IdleState.Initialize(this);
    }

    public override void _Process(float delta)
    {
        Fsm.Process(delta);
        StateControl();
    }

    public override void _PhysicsProcess(float delta)
    {
        Fsm.PhysicsProcess(delta);
    }

    public override void _ExitTree()
    {
        Fsm.ExitTree();
    }
    
    protected abstract void StateControl();
}