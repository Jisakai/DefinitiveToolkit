using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DTK.Input
{
    public class InputManager
    {
        private readonly InputActionAsset _actions;
        private readonly Dictionary<string, InputAction> _cache = new Dictionary<string, InputAction>();

        public InputManager(InputActionAsset actions)
        {
            _actions = actions;
            _actions.Enable();
        }

        #region Polling
        public bool GetButtonDown(string actionName) => Resolve(actionName)?.WasPressedThisFrame() ?? false;
        public bool GetButtonUp(string actionName) => Resolve(actionName)?.WasReleasedThisFrame() ?? false;
        public bool GetButton(string actionName) => Resolve(actionName)?.IsPressed() ?? false;

        public float GetAxis(string actionName) => Resolve(actionName)?.ReadValue<float>() ?? 0f;
        public Vector2 GetVector2(string actionName) => Resolve(actionName)?.ReadValue<Vector2>() ?? Vector2.zero;
        #endregion

        #region Action Maps
        public void EnableMap(string mapName) => _actions.FindActionMap(mapName)?.Enable();
        public void DisableMap(string mapName) => _actions.FindActionMap(mapName)?.Disable();
        #endregion

        private InputAction Resolve(string actionName)
        {
            if (_cache.TryGetValue(actionName, out InputAction cached))
                return cached;

            InputAction action = _actions.FindAction(actionName);

            if (action == null)
            {
                Debug.LogWarning($"[InputManager] No action found named '{actionName}'");
                return null;
            }

            _cache[actionName] = action;
            return action;
        }
        
        #region Raw Keyboard
        public bool GetKey(Key key) => Keyboard.current != null && Keyboard.current[key].isPressed;
        public bool GetKeyDown(Key key) => Keyboard.current != null && Keyboard.current[key].wasPressedThisFrame;
        public bool GetKeyUp(Key key) => Keyboard.current != null && Keyboard.current[key].wasReleasedThisFrame;
        #endregion
    }
}