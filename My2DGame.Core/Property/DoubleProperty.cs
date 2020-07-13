using System;

namespace My2DGame.Core.Property {
	public class DoubleProperty : BaseProperty<double> {
		private double _lastChangedValue;
		public DoubleProperty(double value = default) {
			Value = value;
		}
		protected override void OnPropertyChanged(string propertyName = null) {
			if (propertyName == nameof(Value) && Math.Abs(Value - _lastChangedValue) < 0.1) {
				return;
			}
			_lastChangedValue = Value;
			base.OnPropertyChanged(propertyName);
		}
		public override object Clone() {
			return new DoubleProperty{
				Value = Value
			};
		}
	}
}