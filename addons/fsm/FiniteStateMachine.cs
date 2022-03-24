using Godot;
using Godot.Collections;

namespace Fsm
{
    /// <summary>
    /// Should be constructed in TOwner class.
    /// TOwner class will have access to the Fsm and its states.
    /// Finite State Machine variable must be named "Fsm"!
    /// </summary>
    /// <typeparam name="TOwner">The type of the class in which the Fsm is initialized.</typeparam>
    /// <typeparam name="TKey">The key types of states.</typeparam>
    public class FiniteStateMachine<TOwner, TKey> : Reference where TOwner : Object
    {
        /// All states associated with the Fsm is stored inside this dictionary.
        public Dictionary<TKey, State<TOwner, TKey>> States { get; }
        
        public State<TOwner, TKey> PreviousState { get; private set; }
        public State<TOwner, TKey> CurrentState { get; private set; }
        
        /// Prevents changing the state automatically.
        public bool IsStateLocked { get; private set; }

        /// <summary>
        /// Initializes the states dictionary.
        /// </summary>
        public FiniteStateMachine()
        {
            States = new Dictionary<TKey, State<TOwner, TKey>>();
            IsStateLocked = false;
        }

        /// <summary>
        /// Do not call this function. Called when a state is initialized.
        /// </summary>
        /// <param name="state"></param>
        public void AddState(State<TOwner, TKey> state)
        {
            if (States.ContainsKey(state.Key))
            {
                GD.PushError(
                    "Adding a state is failed. The Fsm already contains a state associated with " +
                    $"key {state.Key}"
                );
                return;
            }
            
            States.Add(state.Key, state);
        }

        /// <summary>
        /// Returns the state associated with the key.
        /// </summary>
        /// <param name="stateKey">The key associated with the state.</param>
        /// <returns>The state associated with the key.</returns>
        public State<TOwner, TKey> GetState(TKey stateKey)
        {
            if (!States.ContainsKey(stateKey))
            {
                GD.PushError(
                    " Getting a state is failed. The Fsm does not contain a state associated " +
                    $"with key {stateKey}"
                );
                return null;
            }
            
            return States[stateKey];
        }

        /// <summary>
        /// Changes the current state. Calls the Exit function of the previous state, and then calls
        /// the Enter function of the new state.
        /// </summary>
        /// <param name="stateKey">The key associated with the state.</param>
        /// <param name="stateLocked">Prevents changing the state automatically.</param>
        public void ChangeState(TKey stateKey, bool stateLocked = false)
        {
            if (IsStateLocked) return;
            if (!States.ContainsKey(stateKey))
            {
                GD.PushWarning(
                    "Changing the current state is failed. The Fsm does not contain a state " +
                    $"associated with key \"{stateKey}\""
                );
                return;
            }
            if (CurrentState == States[stateKey]) return;
            
            CurrentState?.Exit();
            PreviousState = CurrentState;
            IsStateLocked = stateLocked;
            CurrentState = States[stateKey];
            CurrentState?.Enter();
        }
        
        /// <summary>
        /// Stops the current state.
        /// </summary>
        public void StopCurrentState()
        {
            IsStateLocked = false;
            CurrentState?.Exit();
            CurrentState = null;
        }
        
        /// <summary>
        /// Calls the Process function of the current state.
        /// Should be called every frame from TOwner class.
        /// </summary>
        /// <param name="delta">Frame delta time.</param>
        public void Process(float delta) => CurrentState?.Process(delta);

        /// <summary>
        /// Calls the PhysicsProcess function of the current state.
        /// Should be called every physics frame from TOwner class.
        /// </summary>
        /// <param name="delta">Physics frame delta time.</param>
        public void PhysicsProcess(float delta) => CurrentState?.PhysicsProcess(delta);
        
        /// <summary>
        /// Calls the ExitTree function of all states when the Fsm leaves the tree.
        /// Should be called from TOwner class, when TOwner leaves the tree.
        /// </summary>
        public void ExitTree()
        {
            foreach (var pair in States) pair.Value.ExitTree();
        }
    }
}