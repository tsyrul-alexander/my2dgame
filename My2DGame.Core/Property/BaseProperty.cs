using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace My2DGame.Core.Property {
	public abstract class BaseProperty<T> : IProperty<T> {
		private T _value;
		public event PropertyChangedEventHandler PropertyChanged;
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
			throw new System.NotImplementedException();
		}
		public virtual object GetValue() {
			return Value;
		}
		[Annotations.NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
