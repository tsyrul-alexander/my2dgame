using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using My2DGame.Core.UI;
using My2DGame.Game;
using My2DGame.Game.TestGame;

namespace My2DGame.Android {
	public class Game1 : Microsoft.Xna.Framework.Game {
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private readonly IGame _game;

		public Game1() {
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			_game = new TestGame(Services, Content.RootDirectory);
		}
		protected override void LoadContent() {
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_game.Initialize(new GameSpriteBatch(_spriteBatch));
		}

		protected override void Update(GameTime gameTime) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			_game.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_game.Draw(gameTime);
			base.Draw(gameTime);
		}
	}
}