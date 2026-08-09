using DTK.Core.Services;

namespace DTK.UI
{
    public class UIService : IService
    {
        private readonly UIManager _manager;

        public UIService(UIManager manager)
        {
            _manager = manager;
        }

        public void Register(string id, IUIPanel panel) => _manager.Register(id, panel);
        public void Unregister(string id) => _manager.Unregister(id);

        public void Open(string id) => _manager.Open(id);
        public void Close() => _manager.Close();
        public void CloseAll() => _manager.CloseAll();
        public bool IsOpen(string id) => _manager.IsOpen(id);
    }
}