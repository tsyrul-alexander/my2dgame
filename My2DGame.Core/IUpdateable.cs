using Microsoft.Xna.Framework;

namespace My2DGame.Core {
	public interface IUpdateable {
		bool Enabled { get; set; }
		void Update(GameTime gameTime);
	}
}
