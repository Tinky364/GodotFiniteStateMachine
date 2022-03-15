#if TOOLS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;

namespace Fsm
{
    [Tool]
    public class Plugin : EditorPlugin
    {
        private readonly List<string> _scripts = new List<string>();
        
        private Control _control;
        private LineEdit _lineEdit;
        private string _sourcePath;

        public override void _EnterTree()
        {
            base._EnterTree();
            RegisterStates();
            InitializeControl();
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            UnregisterStates();
            RemoveControlFromBottomPanel(_control);
            _control = null;
            _lineEdit = null;
            _sourcePath = "";
        }

        private void RegisterStates()
        {
            _scripts.Clear();
            var file = new File();
            foreach (Type type in GetStatesTypes())
            {
                string path = ClassPath(type);
                if (!file.FileExists(path)) continue;
                Script script = GD.Load<Script>(path);
                if (script == null) continue;
                AddCustomType($"{type.Namespace}_{type.Name}", nameof(Resource), script, null);
                GD.Print($"Register state: {type.Name} -> {path}");
                _scripts.Add(type.Name);
            }
        }
        
        private void UnregisterStates()
        {
            foreach (string script in _scripts)
            {
                RemoveCustomType(script);
                GD.Print($"Unregister state: {script}");
            }
            _scripts.Clear();
        }
        
        private string ClassPath(Type type)
        {
            return
                $"{_sourcePath}/{type.Namespace?.Replace(".", "/").ToLower() ?? ""}/{type.Name}.cs";
        }
        
        private static IEnumerable<Type> GetStatesTypes()
        {
            var assembly = Assembly.GetAssembly(typeof(Plugin));
            return assembly.GetTypes().Where(
                t => !t.IsAbstract && t.IsSubclassOf(typeof(StateResource))
            );
        }

        private void InitializeControl()
        {
            PackedScene scene = GD.Load<PackedScene>("res://addons/fsm/FsmControl.tscn");
            if (scene.Instance() is Control control) _control = control;
            _lineEdit =
                _control.GetNode<LineEdit>("MarginContainer/VBoxContainer/HBoxContainer/LineEdit");
            Button button = _control.GetNode<Button>("MarginContainer/VBoxContainer/Button");
            button.Connect("pressed", this, nameof(OnRegisterStatesButtonPressed));
            AddControlToBottomPanel(_control, "FSM");
        }
        
        private void OnRegisterStatesButtonPressed()
        {
            UnregisterStates();
            _sourcePath = _lineEdit.Text;
            RegisterStates();
        }
    }
}
#endif

