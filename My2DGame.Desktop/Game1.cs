using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using My2DGame.Core.UI;
using My2DGame.Game;
using My2DGame.Game.TestGame;
using My2DGame.Network.Client.Client;
using My2DGame.Network.Client.Client.TCP;
using My2DGame.Network.Client.Synchronizer;

namespace My2DGame.Desktop {
	public class Game1 : Microsoft.Xna.Framework.Game {
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private readonly IGame _myGame;
		private readonly IGameSynchronizer _gameSynchronizer;
		private readonly INetworkClient _networkClient;
		private readonly GameInput _gameInput;

		public Game1() {
			_gameInput = new GameInput();
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			_networkClient = new ClientTcp();
			_gameSynchronizer = new GameSynchronizer(_networkClient);
			_myGame = new TestGame(_gameSynchronizer, _gameInput, Services, Content.RootDirectory);
		}

		protected override void LoadContent() {
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_gameSynchronizer.Initialize("localhost", 9976);
			_myGame.Initialize(new GameSpriteBatch(_spriteBatch));
		}

		protected override void Update(GameTime gameTime) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			_gameInput.IsRight = Keyboard.GetState().IsKeyDown(Keys.D);
			_myGame.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_myGame.Draw(gameTime);
			base.Draw(gameTime);
		}
		protected override void OnExiting(object sender, EventArgs args) {
			base.OnExiting(sender, args);
			if (_networkClient.GetIsConnect()) {
				_networkClient.Disconnect();
			}
 		}
	}
}