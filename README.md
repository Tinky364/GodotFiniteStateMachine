# Godot C#  Generic Finite State Machine

Generic finite state machine for Godot engine using CSharp.

### State
States are resources that can be added to Nodes and edited from the editor interface of the engine. \
Important Fields: \
Owner -> to access the owner class. \
Owner.Fsm -> to access the Fsm class.

### Finite State Machine
Fsm must be constructed from Nodes with state resources. Fsm objects always must be named "Fsm". \
ChangeState(stateKey) -> to change the current state.

## Usage
1.Add "addons/fsm" directory to your project and activate the plugin from the project settings.

2.In the object who will have access the Fsm and its states, create states`
keys and a field called Fsm. This object will be the Owner.
```csharp
using Fsm;

public class Player : Node2D
{
    public enum States { Idle, Move }
    public FiniteStateMachine<Player, States> Fsm { get; private set; }
}
```
\
3.Create new State classes for your Fsm, or use compatible ones. Namespace of the class should be 
same with the file directory.
```csharp
using Fsm;

namespace player_fsm
{
    public class StateIdle : State<Player, Player.States>
    {
        public override void Enter() { }

        public override void Process(float delta) { }

        public override void PhysicsProcess(float delta) { }

        public override void Exit() { }

        public override void ExitTree() { }
    }
}
```
\
4.Create an object of the state in the Owner, and export it to the editor.
```csharp
using Fsm;

public class Player : Node2D
{
    [Export]
    public StateIdle IdleState { get; private set; }
    
    public enum States { Idle, Move }
    public FiniteStateMachine<Player, States> Fsm { get; private set; }
}
```
\
5.In the _Ready function of the Owner, construct Fsm and call Initialize function of the states.
```csharp
public override void _Ready()
{
    Fsm = new FiniteStateMachine<Player, States>();
    IdleState.Initialize(this);
    MoveState.Initialize(this);
}
```
\
6.Call functions of the Fsm in the Owner.
```csharp
public override void _Process(float delta)
{
    Fsm.Process(delta);
}

public override void _PhysicsProcess(float delta)
{
    Fsm.PhysicsProcess(delta);
}

public override void _ExitTree()
{
    Fsm.ExitTree();
}
```
\
7.Register new state resources by pressing the button Register States in the FSM tab in the 
editor. Src path is the directory path of your scripts. if your files in "res://scripts", type "scripts". \
![ScreenShot](https://raw.github.com/Tinky364/GodotFiniteStateMachine/master/img/fsm.png)

8.Create the Owner Node. Pick a state resource from the dropdown list to create it, and chose a Key value associated with this state. \
![ScreenShot](https://raw.github.com/Tinky364/GodotFiniteStateMachine/master/img/state_resource.png)


For a more detailed example, you can check the sample project.