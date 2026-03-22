using UnityEngine;
using UnityEngine.InputSystem;

namespace ProtoPlat.Input
{
    public static class InputManager
    {
        private static readonly InputSystemActions _inputActions = new();

        public static Vector2 MousePosition => Mouse.current.position.ReadValue();
        public static InputManagerAction<Vector2> Move { get; } = new(_inputActions.Player.Move);
        public static InputManagerAction<bool> Jump { get; } = new(_inputActions.Player.Jump);

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            _inputActions.Enable();
        }
    }
}
