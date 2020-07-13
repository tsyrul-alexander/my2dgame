using Microsoft.Xna.Framework;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;

namespace My2DGame.Game {
	public class BaseGame : IGame {
		public virtual IScene ActiveScene { get; set; }
		public virtual void Initialize(ISpriteBatch spriteBatch) {
			ActiveScene.Initialize();
		}
		public virtual void Draw(GameTime gameTime) {
			ActiveScene.Draw(gameTime);
		}
		public virtual void Update(GameTime gameTime) {
			ActiveScene.Update(gameTime);
		}
	}
}
