using System;
using My2DGame.Core;
using My2DGame.Network.Contract;

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
		public virtual void UpdateProperty(PropertyValue propertyValue) {
			Value.SetSilentValue(propertyValue.Name, propertyValue.GetValue());
		}
		protected virtual PropertyValue GetPropertyValue(string columnName, object value) {
			return new PropertyValue(columnName, value);
		}
		protected virtual void ValueOnPropertyChanged(object sender, SilentPropertyChangedEventArgs e) {
			if (e.IsSilent) {
				return;
			}
			var propertyValue = GetPropertyValue(e.PropertyName, e.Value);
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