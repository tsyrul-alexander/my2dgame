using System;
using My2DGame.Core;
using My2DGame.Network.Client.Contract;

namespace My2DGame.Network.Client.Tracker {
	public abstract class BaseTracker<T> : ITracker<T> where T : ISilentPropertyChanged {
		public Guid Id { get; set; }
		public event Action<ITracker<T>, PropertyValue> PropertyChanged;
		public virtual T Value { get; }
		protected BaseTracker(T value) {
			Value = value;
		}
		public virtual void Initialize() {
			Value.PropertyChanged += ValueOnPropertyChanged;
		}
		public abstract void UpdateProperty(PropertyValue propertyValue);
		protected abstract PropertyValue GetPropertyValue(string columnName);
		protected virtual void ValueOnPropertyChanged(object sender, SilentPropertyChangedEventArgs e) {
			if (e.IsSilent) {
				return;
			}
			var propertyValue = GetPropertyValue(e.PropertyName);
			if (propertyValue == null) {
				return;
			}
			OnPropertyChanged(propertyValue);
		}
		protected virtual void OnPropertyChanged(PropertyValue propertyValue) {
			PropertyChanged?.Invoke(this, propertyValue);
		}
	}
}