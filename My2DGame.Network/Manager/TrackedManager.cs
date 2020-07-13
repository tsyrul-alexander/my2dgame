using System;
using System.Collections.Generic;
using System.ComponentModel;
using My2DGame.Network.Synchronizer;
using My2DGame.Network.Tracker;

namespace My2DGame.Network.Manager {
	public abstract class TrackedManager<T>: ITrackedManager<T> where T : INotifyPropertyChanged {
		public IGameSynchronizer GameSynchronizer { get; }
		protected readonly Dictionary<Guid, ITracker<T>> _trackers = new Dictionary<Guid, ITracker<T>>();
		public TrackedManager(IGameSynchronizer gameSynchronizer) {
			GameSynchronizer = gameSynchronizer;
		}
		protected abstract ITracker<T> CreateTracked(T value);
		public virtual void Initialize() { }
		public virtual void Create(T item) {
			var tracker = CreateTracked(item);
			tracker.Initialize();
			_trackers.Add(Guid.NewGuid(), tracker);
		}
		public virtual void Update(ManagerPropertyValue managerPropertyValue) {
			_trackers[managerPropertyValue.Id].UpdateProperty(managerPropertyValue.Value);
		}
		public void Remove(T item) {
			foreach (var (key, value) in _trackers) {
				if (value.Value.Equals(item)) {
					_trackers.Remove(key);
				}
			}
		}
	}
}
