using System.Collections.Generic;
using UnityEngine;

namespace DTK.UI
{
    public class UIManager
    {
        private readonly Dictionary<string, IUIPanel> _panels = new Dictionary<string, IUIPanel>();
        private readonly Stack<string> _stack = new Stack<string>();

        #region Registration
        public void Register(string id, IUIPanel panel)
        {
            if (_panels.ContainsKey(id))
            {
                Debug.LogWarning($"[UIManager] Overwriting registered panel: {id}");
            }
            _panels[id] = panel;
        }

        public void Unregister(string id)
        {
            _panels.Remove(id);
        }
        #endregion

        #region Navigation
        public void Open(string id)
        {
            if (!_panels.TryGetValue(id, out IUIPanel panel))
            {
                Debug.LogWarning($"[UIManager] No panel registered with id '{id}'");
                return;
            }

            panel.Show();
            _stack.Push(id);
        }

        public void Close()
        {
            if (_stack.Count == 0)
            {
                Debug.LogWarning("[UIManager] Close called with nothing open.");
                return;
            }

            string id = _stack.Pop();
            _panels[id].Hide();
        }

        public void CloseAll()
        {
            while (_stack.Count > 0)
            {
                Close();
            }
        }

        public bool IsOpen(string id) => _stack.Contains(id);
        #endregion
    }
}