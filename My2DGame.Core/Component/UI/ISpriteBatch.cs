using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace My2DGame.Core.Component.UI
{
	public interface ISpriteBatch {
		void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation,
			Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

	}
}
