using System;
using Microsoft.Extensions.DependencyInjection;
using My2DGame.Component.Utilities;
using My2DGame.Core;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;
using My2DGame.Game.TestGame.Script;
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
			//LoadScene(Options.ContentFolderPath, "test_scene");
			var mainScene = CreateScene();
			var gameScene = CreateScene();
			var testGameObject = gameScene.CreateGameObject();
			testGameObject.AddTextureComponent("wood_box");
			testGameObject.AddScriptComponent(typeof(PersonScriptAction));
			gameScene.GameObjects.Add(testGameObject);
			_gameSynchronizer = CreateGameSynchronizer();
			_gameSynchronizer.Initialize();
			mainScene.Initialize();
			gameScene.Initialize();
			mainScene.SetActive(false);
			Scenes.Add(mainScene);
			Scenes.Add(gameScene);

			var contentManager = GetContentManager<IScene>();
			var content = contentManager.Save(gameScene);
			var scene = contentManager.Load(content);
		}
	}
}