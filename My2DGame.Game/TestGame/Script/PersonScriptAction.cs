using Microsoft.Xna.Framework;
using My2DGame.Component.Position;
using My2DGame.Component.Script;

namespace My2DGame.Game.TestGame.Script {
	public class PersonScriptAction : BaseScriptAction {
		private readonly GameInput _gameInput;
		public double Speed { get; set; } = 0.5;

		public PersonScriptAction(GameInput gameInput) {
			_gameInput = gameInput;
		}
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			var positionComponent = GameObject.Components.Get<PositionComponent>();
			if (_gameInput.IsRight) {
				positionComponent.X.Value += gameTime.ElapsedGameTime.TotalMilliseconds * Speed;
			}
			if (_gameInput.IsLeft) {
				positionComponent.X.Value -= gameTime.ElapsedGameTime.TotalMilliseconds * Speed;
			}
			if (_gameInput.IsUp) {
				positionComponent.Y.Value -= gameTime.ElapsedGameTime.TotalMilliseconds * Speed;
			}
			if (_gameInput.IsDown) {
				positionComponent.Y.Value += gameTime.ElapsedGameTime.TotalMilliseconds * Speed;
			}
		}
	}
}
