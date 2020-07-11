using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace My2DGame.Core.Property {
	public abstract class BaseProperty<T> : IProperty<T> {
		private T _value;
		public event PropertyChangedEventHandler PropertyChanged;
		public T Value {
			get => _value;
			set {
				if (Equals(value, _value))
					return;
				_value = value;
				OnPropertyChanged();
			}
		}
		public virtual object Clone() {
			throw new System.NotImplementedException();
		}
		[Annotations.NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
