using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace My2DGame.Core.UI {
	public interface ISprite {
		Texture2D Texture { get; set; }
		Vector2 Position { get; set; }
		Rectangle? SourceRectangle { get; set; }
		Color Color { get; set; }
		float Rotation { get; set; }
		Vector2 Origin { get; set; }
		float Scale { get; set; }
		SpriteEffects Effects { get; set; }
		float LayerDepth { get; set; }
	}
}