using System;

namespace My2DGame.Core.Property {
	public class DoubleProperty : BaseProperty<double> {
		private readonly double _propertyChangeStep;
		private double _lastChangedValue;
		public DoubleProperty(double value = default, double propertyChangeStep = 0.001) {
			_propertyChangeStep = propertyChangeStep;
			Value = value;
		}
		protected override void OnPropertyChanged(object value, string propertyName = null, bool isSilent = false) {
			if (propertyName == nameof(Value) && Math.Abs(Value - _lastChangedValue) < _propertyChangeStep) {
				return;
			}
			_lastChangedValue = Value;
			base.OnPropertyChanged(Value, propertyName, isSilent);
		}
		public override object Clone() {
			return new DoubleProperty(Value);
		}
	}
}