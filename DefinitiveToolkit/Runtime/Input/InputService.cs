using UnityEngine;
using DTK.Core.Services;
using UnityEngine.InputSystem;

namespace DTK.Input
{
    public class InputService : IService
    {
        private readonly InputManager _manager;

        public InputService(InputManager manager)
        {
            _manager = manager;
        }

        public bool GetButtonDown(string actionName) => _manager.GetButtonDown(actionName);
        public bool GetButtonUp(string actionName) => _manager.GetButtonUp(actionName);
        public bool GetButton(string actionName) => _manager.GetButton(actionName);

        public float GetAxis(string actionName) => _manager.GetAxis(actionName);
        public Vector2 GetVector2(string actionName) => _manager.GetVector2(actionName);

        public void EnableMap(string mapName) => _manager.EnableMap(mapName);
        public void DisableMap(string mapName) => _manager.DisableMap(mapName);
        
        public bool GetKey(Key key) => _manager.GetKey(key);
        public bool GetKeyDown(Key key) => _manager.GetKeyDown(key);
        public bool GetKeyUp(Key key) => _manager.GetKeyUp(key);
    }
}