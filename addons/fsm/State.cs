using Godot;

namespace Fsm
{
    /// <summary>
    /// All state classes should be subclass of this class.
    /// All subclasses of this class should be placed in "res://addons/fsm/{DirectoryName}".
    /// The namespace of subclasses should be "Fsm/{DirectoryName}".
    /// </summary>
    /// <typeparam name="TOwner">The type of the class in which the Fsm is initialized.</typeparam>
    /// <typeparam name="TKey">The key type of the state.</typeparam>
    public abstract class State<TOwner, TKey> : StateResource where TOwner : Object
    {
        /// The key value of the state.
        [Export]
        public TKey Key { get; private set; } 
        
        /// The class in which the Fsm is initialized.
        protected TOwner Owner { get; private set; } 
        
        protected State()
        {
            ResourceLocalToScene = true;
        }
        
        /// <summary>
        /// Initializes the owner of the state and its associated key.
        /// Should be called after the state is constructed.
        /// </summary>
        /// <param name="owner">The instance of the class in which the Fsm is initialized.</param>
        public virtual void Initialize(TOwner owner)
        {
            if (!(owner.Get("Fsm") is FiniteStateMachine<TOwner, TKey> fsm))
            {
                GD.PushError("Finite State Machine variable name is not Fsm!");
                return;
            }
            Owner = owner;
            fsm.AddState(this);
        }
        
        /// <summary>
        /// Called from the Fsm when the current state is changed to this state.
        /// Do not call this function.
        /// </summary>
        public abstract void Enter();
            
        /// <summary>
        /// Called every frame from the Fsm when this state is the current state.
        /// Do not call this function.
        /// </summary>
        public abstract void Process(float delta);
            
        /// <summary>
        /// Called every physics frame from the Fsm when this state is the current state.
        /// Do not call this function.
        /// </summary>
        public abstract void PhysicsProcess(float delta);
            
        /// <summary>
        /// Called from the Fsm when the current state is changed to another state from this state.
        /// Do not call this function.
        /// </summary>
        public abstract void Exit();

        /// <summary>
        /// Called from the Fsm when the Fsm leaves the tree.
        /// Do not call this function.
        /// </summary>
        public abstract void ExitTree();
    }
}