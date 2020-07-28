using System;
using System.Runtime.CompilerServices;

namespace My2DGame.Core.Property {
	public abstract class BaseProperty<T> : IProperty<T> {
		private T _value;
		public event SilentPropertyChangedEventHandler PropertyChanged;
		public T Value {
			get => _value;
			set {
				if (GetIsEqualNewPropertyValue(value))
					return;
				_value = value;
				OnPropertyChanged(value);
			}
		}
		protected virtual bool GetIsEqualNewPropertyValue(T newValue) {
			return Equals(Value, newValue);
		}
		public virtual object Clone() {
			throw new NotImplementedException();
		}
		public virtual object GetValue() {
			return Value;
		}
		public void SetSilentValue(object value) {
			_value = (T)value;
			OnPropertyChanged(value, nameof(Value), true);
		}
		public void SetSilentValue(string propertyName, object value) {
			if (propertyName == nameof(Value)) {
				Value = (T)value;
			}
			OnPropertyChanged(value, propertyName, true);
		}
		protected virtual void OnPropertyChanged(object value, [CallerMemberName] string propertyName = null, bool isSilent = false) {
			PropertyChanged?.Invoke(this, new SilentPropertyChangedEventArgs(propertyName, value, isSilent));
		}
	}
}
