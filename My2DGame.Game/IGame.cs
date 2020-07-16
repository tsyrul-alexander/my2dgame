using System.Collections.Generic;
using Microsoft.Xna.Framework;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;

namespace My2DGame.Game {
	public interface IGame {
		IList<IScene> Scenes { get; set; }
		void Initialize(ISpriteBatch spriteBatch);
		void Draw(GameTime gameTime);
		void Update(GameTime gameTime);
	}
}