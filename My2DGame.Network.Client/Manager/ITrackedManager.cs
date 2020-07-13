using System;
using My2DGame.Core;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Manager {
	public interface ITrackedManager<T> where T : ISilentPropertyChanged {
		event Action<ManagerPropertyValue> ItemPropertyChanged;
		void Initialize();
		void Create(Guid id, T item);
		void Update(ManagerPropertyValue managerPropertyValue);
		void Remove(T item);
	}
}