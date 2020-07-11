using My2DGame.Core.Component.UI;
using My2DGame.Core.Scene;

namespace My2DGame.Game.TestGame {
	public class TestGame : IGame {
		private readonly ISpriteBatch _spriteBatch;
		private IScene _mainScene;
		public TestGame(ISpriteBatch spriteBatch) {
			_spriteBatch = spriteBatch;
			_mainScene = new Scene(_spriteBatch);
		}
		public void Start() {
			
		}
	}
}