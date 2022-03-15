using enemy_fsm;
using Fsm;
using Godot;

public class Enemy : Node2D
{
    [Export]
    public StateIdle IdleState { get; private set; }
    [Export]
    public StateAttack AttackState { get; private set; }
    
    public enum States { Idle, Attack }
    public FiniteStateMachine<Enemy, States> Fsm { get; private set; }
    
    public override void _Ready()
    {
        base._Ready();
        Fsm = new FiniteStateMachine<Enemy, States>();
        IdleState.Initialize(this);
        AttackState.Initialize(this);
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