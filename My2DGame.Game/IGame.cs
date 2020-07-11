using Microsoft.Xna.Framework;
using My2DGame.Core.UI;

namespace My2DGame.Game {
	public interface IGame {
		void Initialize(ISpriteBatch spriteBatch);
		void Draw(GameTime gameTime);
		void Update(GameTime gameTime);
	}
}