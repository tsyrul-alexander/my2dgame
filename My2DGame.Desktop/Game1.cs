using System;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using My2DGame.Core;
using My2DGame.Core.UI;
using My2DGame.Game;
using My2DGame.Game.EnglishStory;

namespace My2DGame.Desktop {
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
			//_game = new TestGame(_gameInput, Services, new GameOptions {
			//	ContentFolderPath = Content.RootDirectory,
			//	LogFolderPath = Environment.CurrentDirectory,
			//	ServerIpAddress = "localhost",
			//	ServerPort = 9973,
			//	NetworkRoomId = Guid.Parse("9a3a5d67-85ab-4509-bc29-72d025b435be")
			//});
			_game = new EnglishStoryGame(_gameInput, Services, new GameOptions {
				ContentFolderPath = Content.RootDirectory,
				LogFolderPath = Environment.CurrentDirectory,
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
			var mouseState = Mouse.GetState(Window);
			_gameInput.MouseLocation = mouseState.Position.ToVector2();
			_gameInput.MousePressed = mouseState.LeftButton == ButtonState.Pressed;
			_gameInput.MouseReleased = mouseState.LeftButton == ButtonState.Released;
			//_gameInput.IsRight = Keyboard.GetState().IsKeyDown(Keys.D);
			//_gameInput.IsLeft = Keyboard.GetState().IsKeyDown(Keys.A);
			//_gameInput.IsUp = Keyboard.GetState().IsKeyDown(Keys.W);
			//_gameInput.IsDown = Keyboard.GetState().IsKeyDown(Keys.S);
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