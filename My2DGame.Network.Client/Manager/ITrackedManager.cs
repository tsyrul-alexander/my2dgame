using System;
using My2DGame.Core;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Manager {
	public interface ITrackedManager<T> where T : ISilentPropertyChanged {
		event Action<ManagerPropertyValue> ItemPropertyChanged;
		event Action<CreateManagerItem> AddItem;
		event Action<RemoveManagerItem> RemoveItem;
		bool TryGetItem(T item, out Guid networkId);
		Guid GetItem(T item);
		T GetItem(Guid networkId);
		void Initialize();
		void Add(Guid id, T item);
		void Remove(T item);
		void Create(CreateManagerItem createManagerItem);
		void Update(ManagerPropertyValue managerPropertyValue);
		void Remove(RemoveManagerItem removeManagerItem);
	}
}