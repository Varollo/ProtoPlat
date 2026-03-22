using UnityEngine.InputSystem;

namespace ProtoPlat.Input
{
    public class InputManagerAction<TValue> where TValue : struct
    {
        private readonly InputAction _action;

        public InputManagerAction(InputAction action)
        {
            _action = action;
        }

        public TValue Value => _action.ReadValue<TValue>();
        public bool IsPressed => _action.IsPressed();
        public bool IsJustPressed => _action.triggered;
    }
}
