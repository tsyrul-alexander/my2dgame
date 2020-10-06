using Microsoft.Xna.Framework;
using My2DGame.Core;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Component.Position {
	public class PositionComponent : BaseGameObjectComponent {
		public DoubleProperty X { get; }
		public DoubleProperty Y { get; }
		public PositionComponent(double x, double y): this(new DoubleProperty(x, 0.5), new DoubleProperty(y, 0.5)) { }
		public PositionComponent(DoubleProperty x, DoubleProperty y) {
			X = x;
			Y = y;
		}
		public override void Initialize() {
			base.Initialize();
			UpdateGameObjectPosition();
			X.PropertyChanged += PositionXOnPropertyChanged;
			Y.PropertyChanged += PositionYOnPropertyChanged;
		}
		private void PositionYOnPropertyChanged(object sender, SilentPropertyChangedEventArgs e) {
			PositionOnPropertyChanged(sender, e);
			OnPropertyChanged(Y.Value, nameof(Y), e.IsSilent);
		}
		private void PositionXOnPropertyChanged(object sender, SilentPropertyChangedEventArgs e) {
			PositionOnPropertyChanged(sender, e);
			OnPropertyChanged(X.Value, nameof(X), e.IsSilent);
		}
		private void PositionOnPropertyChanged(object sender, SilentPropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(IntegerProperty.Value)) {
				UpdateGameObjectPosition();
			}
		}
		public override void SetSilentValue(string propertyName, object value) {
			base.SetSilentValue(propertyName, value);
			if (propertyName == nameof(X)) {
				X.SetSilentValue(value);
			}
			if (propertyName == nameof(Y)) {
				Y.SetSilentValue(value);
			}
			OnPropertyChanged(value, propertyName, true);
		}
		protected virtual void UpdateGameObjectPosition() {
			GameObject.Position = new Vector2((float) X.Value, (float) Y.Value);
		}
	}
}