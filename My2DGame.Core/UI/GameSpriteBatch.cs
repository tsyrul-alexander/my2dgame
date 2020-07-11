using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace My2DGame.Core.UI {
	public class GameSpriteBatch : ISpriteBatch {
		private readonly SpriteBatch _spriteBatch;
		public GameSpriteBatch(SpriteBatch spriteBatch) {
			_spriteBatch = spriteBatch;
		}
		public void StartDraw() {
			_spriteBatch.Begin();
		}
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation,
			Vector2 origin, float scale, SpriteEffects effects, float layerDepth) {
			_spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
		}
		public void EndDraw() {
			_spriteBatch.End();
		}
	}
}
