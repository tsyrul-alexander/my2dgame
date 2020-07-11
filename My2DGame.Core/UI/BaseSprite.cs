using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace My2DGame.Core.UI {
	public abstract class BaseSprite : ISprite {
		public Texture2D Texture { get; set; }
		public Vector2 Position { get; set; }
		public Rectangle? SourceRectangle { get; set; }
		public Color Color { get; set; } = Color.White;
		public float Rotation { get; set; }
		public Vector2 Origin { get; set; }
		public float Scale { get; set; } = 1;
		public SpriteEffects Effects { get; set; } = SpriteEffects.None;
		public float LayerDepth { get; set; }
	}
}