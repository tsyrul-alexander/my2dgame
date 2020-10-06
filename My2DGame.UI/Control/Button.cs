using System;
using Microsoft.Xna.Framework;
using My2DGame.Component.Utilities;
using My2DGame.Core.GameObject;
using My2DGame.Core.Input;

namespace My2DGame.UI.Control {
	public class Button : GameObject {
		public event Action<Button, EventArgs> MouseClick;
		public IMouseInput MouseInput { get; }
		public Rectangle Size { get; }
		private bool _lastMousePressed;
		public Button(IMouseInput mouseInput, string textureName, Vector2 position, Rectangle size) {
			MouseInput = mouseInput;
			Size = size;
			this.Scale = new Vector2(0.5f, 0.5f);
			this.AddPositionComponent(position.X, position.Y);
			this.AddTextureComponent(textureName, size.Width, size.Height);
		}
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			if (_lastMousePressed && MouseInput.MouseReleased && Size.Intersects(new Rectangle((MouseInput.MouseLocation - Position).ToPoint(), new Point(1, 1)))) {
				OnMouseClick();
			}
			_lastMousePressed = MouseInput.MousePressed;
		}
		protected virtual void OnMouseClick() {
			MouseClick?.Invoke(this, EventArgs.Empty);
		}
	}
}