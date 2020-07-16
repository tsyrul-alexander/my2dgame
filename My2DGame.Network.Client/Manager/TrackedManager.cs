using System;
using System.Collections.Generic;
using My2DGame.Core;
using My2DGame.Network.Client.Synchronizer;
using My2DGame.Network.Client.Tracker;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Manager {
	public abstract class TrackedManager<T>: ITrackedManager<T> where T : ISilentPropertyChanged {
		protected IGameSynchronizer GameSynchronizer { get; }

		protected virtual Dictionary<Guid, ITracker<T>> Trackers { get; set; } = new Dictionary<Guid, ITracker<T>>();
		protected TrackedManager(IGameSynchronizer gameSynchronizer) {
			GameSynchronizer = gameSynchronizer;
		}
		protected abstract ITracker<T> CreateTracked(T value);
		public event Action<ManagerPropertyValue> ItemPropertyChanged;
		public virtual bool GetIfExistsItem(T item, out Guid networkId) {
			foreach (var (key, tracker) in Trackers) {
				if (tracker.Value.Equals(item)) {
					networkId = key;
					return true;
				}
			}
			networkId = Guid.Empty;
			return false;
		}
		public virtual void Initialize() { }
		public virtual void Add(Guid id, T item) {
			var tracker = CreateTracked(item);
			tracker.Id = id;
			tracker.Initialize();
			tracker.PropertyChanged += TrackerOnPropertyChanged;
			Trackers.Add(tracker.Id, tracker);
		}
		protected abstract string GetManagerName();
		private void TrackerOnPropertyChanged(ITracker<T> tracker, PropertyValue propertyValue) {
			OnItemPropertyChanged(new ManagerPropertyValue {
				Id = tracker.Id,
				ManagerName = GetManagerName(),
				Value = propertyValue
			});
		}
		public virtual void Update(ManagerPropertyValue managerPropertyValue) {
			Trackers[managerPropertyValue.Id].UpdateProperty(managerPropertyValue.Value);
		}
		public void Remove(T item) {
			foreach (var (key, value) in Trackers) {
				if (value.Value.Equals(item)) {
					Trackers.Remove(key);
				}
			}
		}
		protected virtual void OnItemPropertyChanged(ManagerPropertyValue obj) {
			ItemPropertyChanged?.Invoke(obj);
		}
	}
}
