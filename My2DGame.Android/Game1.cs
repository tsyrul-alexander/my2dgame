using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using My2DGame.Core;
using My2DGame.Core.UI;
using My2DGame.Game;
using My2DGame.Game.TestGame;

namespace My2DGame.Android {
	public class Game1 : Microsoft.Xna.Framework.Game {
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private readonly IGame _game;
		private readonly GameInput _gameInput;
		public Game1() {
			_gameInput = new GameInput();
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);
			_game = new TestGame(_gameInput, Services, new GameOptions{
				ContentFolderPath = Content.RootDirectory,
				LogFolderPath = Environment.CurrentDirectory,
				ServerIpAddress = "10.0.21.46",
				ServerPort = 9973,
				NetworkRoomId = Guid.Parse("9a3a5d67-85ab-4509-bc29-72d025b435be")
			});
		}
		protected override void LoadContent() {
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_game.Initialize(new GameSpriteBatch(_spriteBatch));
		}

		protected override void Update(GameTime gameTime) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			_gameInput.IsRight = Keyboard.GetState().IsKeyDown(Keys.D);
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