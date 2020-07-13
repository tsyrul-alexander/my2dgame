using Microsoft.Xna.Framework;
using My2DGame.Core;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Component.Position {
	public class PositionComponent : BaseGameObjectComponent {
		public DoubleProperty X { get; }
		public DoubleProperty Y { get; }
		public PositionComponent(int x, int y) {
			X = new DoubleProperty(x, 0.5);
			Y = new DoubleProperty(y, 0.5);
		}
		public override void Initialize() {
			base.Initialize();
			UpdateGameObjectPosition();
			X.PropertyChanged += PositionOnPropertyChanged;
			Y.PropertyChanged += PositionOnPropertyChanged;
		}
		public override IProperty[] GetProperties() {
			return new IProperty[] {X, Y};
		}
		private void PositionOnPropertyChanged(object sender, SilentPropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(IntegerProperty.Value)) {
				UpdateGameObjectPosition();
			}
		}
		protected virtual void UpdateGameObjectPosition() {
			GameObject.Position = new Vector2((float) X.Value, (float) Y.Value);
		}
	}
}