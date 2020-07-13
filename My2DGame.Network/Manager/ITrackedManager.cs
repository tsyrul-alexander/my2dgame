using System;
using System.ComponentModel;

namespace My2DGame.Network.Manager {
	public interface ITrackedManager<T> where T : INotifyPropertyChanged {
		void Initialize();
		void Create(T item);
		void Update(ManagerPropertyValue managerPropertyValue);
		void Remove(T item);
	}
}