using System;
using System.Collections.Generic;
using System.Linq;
using My2DGame.Core;
using My2DGame.Network.Client.Tracker;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Manager {
	public abstract class TrackedManager<T>: ITrackedManager<T> where T : ISilentPropertyChanged {
		protected virtual Dictionary<Guid, ITracker<T>> Trackers { get; } = new Dictionary<Guid, ITracker<T>>();
		protected abstract ITracker<T> CreateTracked(T value);
		public event Action<ManagerPropertyValue> ItemPropertyChanged;
		public event Action<CreateManagerItem> AddItem;
		public event Action<RemoveManagerItem> RemoveItem;
		public virtual bool TryGetItem(T item, out Guid networkId) {
			foreach (var (key, tracker) in Trackers) {
				if (tracker.Value.Equals(item)) {
					networkId = key;
					return true;
				}
			}
			networkId = Guid.Empty;
			return false;
		}
		public Guid GetItem(T item) {
			return Trackers.First(pair => pair.Value.Value.Equals(item)).Key;
		}
		public T GetItem(Guid networkId) {
			return Trackers[networkId].Value;
		}
		public virtual void Initialize() { }
		public virtual void Add(Guid id, T item) {
			var tracker = CreateTracker(id, item);
			Trackers.Add(tracker.Id, tracker);
			OnAddItem(GetCreateManagerItem(tracker));
		}
		protected virtual ITracker<T> CreateTracker(Guid id, T item) {
			var tracker = CreateTracked(item);
			tracker.Id = id;
			tracker.Initialize();
			tracker.PropertyChanged += TrackerOnPropertyChanged;
			return tracker;
		}
		protected virtual RemoveManagerItem GetRemoveManagerItem(ITracker<T> tracker) {
			return new RemoveManagerItem {
				Id = tracker.Id
			};
		}
		protected abstract CreateManagerItem GetCreateManagerItem(ITracker<T> tracker);
		protected abstract string GetManagerName();
		private void TrackerOnPropertyChanged(ITracker<T> tracker, PropertyValue propertyValue) {
			OnItemPropertyChanged(new ManagerPropertyValue {
				Id = tracker.Id,
				ManagerName = GetManagerName(),
				Value = propertyValue
			});
		}
		public virtual void Create(CreateManagerItem createManagerItem) {
			var item = CreateItem(createManagerItem);
			var tracker = CreateTracker(createManagerItem.Id, item);
			foreach (var propertyValue in createManagerItem.Values) {
				tracker.UpdateProperty(propertyValue);
			}
			Trackers.Add(createManagerItem.Id, tracker);
			OnTrackerItemCreated(createManagerItem, tracker);
		}
		protected virtual void OnTrackerItemCreated(CreateManagerItem createManagerItem, ITracker<T> tracker) {
		}
		protected abstract T CreateItem(CreateManagerItem createManagerItem);
		public virtual void Update(ManagerPropertyValue managerPropertyValue) {
			Trackers[managerPropertyValue.Id].UpdateProperty(managerPropertyValue.Value);
		}
		public void Remove(RemoveManagerItem removeManagerItem) {
			throw new NotImplementedException();
		}
		public void Remove(T item) {
			foreach (var (key, value) in Trackers) {
				if (value.Value.Equals(item)) {
					Trackers.Remove(key);
					OnRemoveItem(GetRemoveManagerItem(value));
				}
			}
		}
		protected virtual void OnItemPropertyChanged(ManagerPropertyValue obj) {
			ItemPropertyChanged?.Invoke(obj);
		}
		protected virtual void OnAddItem(CreateManagerItem obj) {
			AddItem?.Invoke(obj);
		}
		protected virtual void OnRemoveItem(RemoveManagerItem obj) {
			RemoveItem?.Invoke(obj);
		}
	}
}
