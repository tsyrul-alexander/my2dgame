using System;
using Microsoft.Extensions.DependencyInjection;
using My2DGame.Core;
using My2DGame.Core.UI;
using My2DGame.Network.Client.Synchronizer;

namespace My2DGame.Game.TestGame {
	public class TestGame : BaseGame {
		private readonly GameInput _gameInput;
		private IGameSynchronizer _gameSynchronizer;
		public TestGame(GameInput gameInput, IServiceProvider serviceProvider, GameOptions options):base(serviceProvider, options) {
			_gameInput = gameInput;
		}
		public override void ConfigureGameServices(IServiceCollection serviceCollection) {
			base.ConfigureGameServices(serviceCollection);
			serviceCollection.AddSingleton(_gameInput);
		}
		public override void Initialize(ISpriteBatch spriteBatch) {
			base.Initialize(spriteBatch);
			var mainScene = CreateScene();
			var gameScene = LoadScene(Options.ContentFolderPath, "test_scene");
			_gameSynchronizer = CreateGameSynchronizer();
			_gameSynchronizer.Initialize();
			mainScene.Initialize();
			Scenes.Add(mainScene);
		}
	}
}