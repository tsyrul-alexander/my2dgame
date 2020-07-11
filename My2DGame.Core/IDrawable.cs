using Microsoft.Xna.Framework;

namespace My2DGame.Core {
	public interface IDrawable {
		bool Visible { get; set; }
		void Draw(GameTime gameTime);
	}
}
