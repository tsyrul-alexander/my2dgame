using Microsoft.Xna.Framework;
using My2DGame.Component.Position;
using My2DGame.Component.Script;
using My2DGame.Core.GameObject;

namespace My2DGame.Game.TestGame.Script {
	class PersonScriptAction : BaseScriptAction {
		private readonly GameInput _gameInput;
		public PersonScriptAction(GameInput gameInput) {
			_gameInput = gameInput;
		}
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			if (_gameInput.IsRight) {
				var positionComponent = GameObject.Components.Get<PositionComponent>();
				positionComponent.X.SetValue(positionComponent.X.Value + gameTime.ElapsedGameTime.TotalMilliseconds * 0.5);
			}
		}
	}
}
