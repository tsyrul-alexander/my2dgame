using System.ComponentModel;
using Microsoft.Xna.Framework;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Component.Texture {
	public class PositionComponent : BaseGameObjectComponent {
		public IntegerProperty X { get; }
		public IntegerProperty Y { get; }
		public PositionComponent(int x, int y) {
			X = new IntegerProperty {Value = x};
			Y = new IntegerProperty {Value = y};
		}
		public override void Initialize() {
			base.Initialize();
			UpdateGameObjectPosition();
			X.PropertyChanged += PositionOnPropertyChanged;
			Y.PropertyChanged += PositionOnPropertyChanged;
		}
		private void PositionOnPropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(IntegerProperty.Value)) {
				UpdateGameObjectPosition();
			}
		}
		protected virtual void UpdateGameObjectPosition() {
			GameObject.Position = new Vector2(X.Value, Y.Value);
		}
	}
}