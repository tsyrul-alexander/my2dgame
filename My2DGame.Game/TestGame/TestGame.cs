using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using My2DGame.Component.Animation;
using My2DGame.Component.Texture;
using My2DGame.Component.Utilities;
using My2DGame.Core;
using My2DGame.Core.Property;
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
			var gameScene = CreateScene();
			var testGameObject = gameScene.AddGameObject();
			var textureComponent = testGameObject.AddTextureComponent(@"BeachBall\beach_ball_00");
			var positionComponent = testGameObject.AddPositionComponent();
			testGameObject.AddColliderComponent(0, 0, 323, 316);
			testGameObject.AddAnimationComponent(new Dictionary<int, string> {
				{0, @"Brick\grey_brick\animation_1\grey_brick_animation_1_frame_01"},
				{1, @"Brick\grey_brick\animation_1\grey_brick_animation_1_frame_02"}
			});
			testGameObject.AddScriptComponent(typeof(PersonScriptAction), typeof(PersonAnimationScriptAction));
			_gameSynchronizer = GetGameSynchronizer();
			_gameSynchronizer.SceneTrackedManager.Add(Guid.Parse("eaa77993-1b03-4060-82a2-f00111ae6efe"), gameScene);
			//_gameSynchronizer.Initialize();
			_gameSynchronizer.GameObjectTrackedManager.Add(Guid.NewGuid(), testGameObject);
			_gameSynchronizer.GameObjectComponentTrackedManager.Add(Guid.NewGuid(), positionComponent);
			_gameSynchronizer.GameObjectComponentTrackedManager.Add(Guid.NewGuid(), textureComponent);
			gameScene.Initialize();
			var contentManager = GetContentManager<IScene>();
			var content = contentManager.Save(gameScene);
			File.WriteAllText(@"D:\Temp\Game\azaza.json", content);
			var scene = contentManager.Load(File.ReadAllText(@"D:\Temp\Game\azaza.json"));
			scene.Initialize();
			Scenes.Add(scene);
		}
	}
}