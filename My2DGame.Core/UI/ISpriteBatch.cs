using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace My2DGame.Core.UI
{
	public interface ISpriteBatch {
		void StartDraw();
		void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation,
			Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);
		void EndDraw();
	}
}
