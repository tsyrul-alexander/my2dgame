using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using My2DGame.Core.Component.UI;

namespace My2DGame.Desktop.UI {
	public class DesktopSpriteBatch : ISpriteBatch {
		private readonly SpriteBatch _spriteBatch;
		public DesktopSpriteBatch(SpriteBatch spriteBatch) {
			_spriteBatch = spriteBatch;
		}
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation,
			Vector2 origin, float scale, SpriteEffects effects, float layerDepth) {
			_spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
		}
	}
}
