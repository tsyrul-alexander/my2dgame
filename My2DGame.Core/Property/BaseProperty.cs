using System;
using System.Runtime.CompilerServices;

namespace My2DGame.Core.Property {
	public abstract class BaseProperty<T> : IProperty<T> {
		private T _value;
		public event SilentPropertyChangedEventHandler PropertyChanged;
		public T Value {
			get => _value;
			protected set {
				if (GetIsEqualNewPropertyValue(value))
					return;
				_value = value;
				OnPropertyChanged();
			}
		}
		protected virtual bool GetIsEqualNewPropertyValue(T newValue) {
			return Equals(Value, newValue);
		}
		public virtual void SetValue(T value) {
			Value = value;
		}
		public virtual object Clone() {
			throw new NotImplementedException();
		}
		public virtual object GetValue() {
			return Value;
		}
		public void SetSilentValue(object value) {
			_value = (T)value;
			OnPropertyChanged(nameof(Value), true);
		}
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null, bool isSilent = false) {
			PropertyChanged?.Invoke(this, new SilentPropertyChangedEventArgs(propertyName, isSilent));
		}
	}
}
