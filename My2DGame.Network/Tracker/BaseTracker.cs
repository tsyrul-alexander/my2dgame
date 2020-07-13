using System;
using System.ComponentModel;
using System.Diagnostics;
using My2DGame.Core.Utilities;

namespace My2DGame.Network.Tracker {
	public abstract class BaseTracker<T> : ITracker<T> where T : INotifyPropertyChanged {
		public event Action<T, string, object> PropertyChanged;
		public virtual T Value { get; }
		public BaseTracker(T value) {
			Value = value;
		}
		public virtual void Initialize() {
			Value.PropertyChanged += ValueOnPropertyChanged;
		}
		public abstract void UpdateProperty(PropertyValue propertyValue);
		protected virtual void ValueOnPropertyChanged(object sender, PropertyChangedEventArgs e) {
			Debug.Write(e.PropertyName);
			OnPropertyChanged(e.PropertyName, Value.GetPropertyValue(e.PropertyName));
		}
		protected virtual void OnPropertyChanged(string propertyName, object propertyValue) {
			PropertyChanged?.Invoke(Value, propertyName, propertyValue);
		}
	}
}