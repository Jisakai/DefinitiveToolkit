using DTK.Core.Services;

namespace DTK.Core.Save
{
    public class SaveService : IService
    {
        private readonly SaveManager _manager;

        public SaveService(SaveManager manager)
        {
            _manager = manager;
        }

        public void Register(ISaveable saveable) => _manager.Register(saveable);
        public void Unregister(ISaveable saveable) => _manager.Unregister(saveable);
        public void Save(string slotName) => _manager.Save(slotName);
        public bool Load(string slotName) => _manager.Load(slotName);
        public bool SlotExists(string slotName) => _manager.SlotExists(slotName);
        public void DeleteSlot(string slotName) => _manager.DeleteSlot(slotName);
    }
}