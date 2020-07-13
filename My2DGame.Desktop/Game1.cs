using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using My2DGame.Core.UI;
using My2DGame.Game;
using My2DGame.Game.TestGame;
using My2DGame.Network.Synchronizer;

namespace My2DGame.Desktop {
	public class Game1 : Microsoft.Xna.Framework.Game {
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private readonly IGame _myGame;
		private readonly IGameSynchronizer _gameSynchronizer;

		public Game1() {
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			_myGame = new TestGame(Services, Content.RootDirectory);
			_gameSynchronizer = new GameSynchronizer(_myGame);
		}

		protected override void LoadContent() {
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_myGame.Initialize(new GameSpriteBatch(_spriteBatch));
			_gameSynchronizer.Initialize();
		}

		protected override void Update(GameTime gameTime) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			_myGame.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_myGame.Draw(gameTime);
			base.Draw(gameTime);
		}
	}
}