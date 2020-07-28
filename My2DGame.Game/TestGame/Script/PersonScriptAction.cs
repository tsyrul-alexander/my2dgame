using Microsoft.Xna.Framework;
using My2DGame.Component.Position;
using My2DGame.Component.Script;

namespace My2DGame.Game.TestGame.Script {
	public class PersonScriptAction : BaseScriptAction {
		public bool IsRight => _gameInput.IsRight;
		public bool IsLeft => _gameInput.IsLeft;
		public bool IsUp => _gameInput.IsUp;
		public bool IsDown => _gameInput.IsDown;
		private readonly GameInput _gameInput;
		public double StepSpeed { get; set; } = 0.5;
		public double Speed { get; set; } = 0.5;
		public PersonScriptAction(GameInput gameInput) {
			_gameInput = gameInput;
		}
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			var positionComponent = GameObject.Components.Get<PositionComponent>();
			if (IsRight) {
				positionComponent.X.Value += gameTime.ElapsedGameTime.TotalMilliseconds * Speed;
			}
			if (IsLeft) {
				positionComponent.X.Value -= gameTime.ElapsedGameTime.TotalMilliseconds * Speed;
			}
			if (IsUp) {
				positionComponent.Y.Value -= gameTime.ElapsedGameTime.TotalMilliseconds * Speed;
			}
			if (IsDown) {
				positionComponent.Y.Value += gameTime.ElapsedGameTime.TotalMilliseconds * Speed;
			}
		}
	}
}
