using System;
using My2DGame.Core;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Manager {
	public interface ITrackedManager<T> where T : ISilentPropertyChanged {
		event Action<ManagerPropertyValue> ItemPropertyChanged;
		bool GetIfExistsItem(T item, out Guid networkId);
		void Initialize();
		void Add(Guid id, T item);
		void Update(ManagerPropertyValue managerPropertyValue);
		void Remove(T item);
	}
}