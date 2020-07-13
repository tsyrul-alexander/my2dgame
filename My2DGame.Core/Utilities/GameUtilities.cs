using System.Collections.Generic;
using Microsoft.Xna.Framework;
using My2DGame.Core.UI;

namespace My2DGame.Core.Utilities {
	public static class GameUtilities {
		public static void UpdateableEach(this IEnumerable<IUpdateable> collection, GameTime gameTime) {
			collection.ForEach(updateable => updateable.Update(gameTime));
		}
		public static void DrawableEach(this IEnumerable<IDrawable> collection, GameTime gameTime) {
			collection.ForEach(updateable => updateable.Draw(gameTime));
		}
		public static void Draw(this ISpriteBatch spriteBatch, ISprite sprite) {
			if (sprite.Texture == null) {
				return;
			}
			spriteBatch.Draw(sprite.Texture, sprite.Position, sprite.SourceRectangle, sprite.Color, sprite.Rotation,
				sprite.Origin, sprite.Scale, sprite.Effects, sprite.LayerDepth);
		}
	}
}
