using System;
using My2DGame.Component.Position;
using My2DGame.Component.Utilities;
using My2DGame.Core.Manager;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;
using My2DGame.Network.Client.Synchronizer;

namespace My2DGame.Game.TestGame {
	public class TestGame : BaseGame {
		private readonly IGameSynchronizer _gameSynchronizer;
		private readonly GameInput _gameInput;
		private readonly IServiceProvider _serviceProvider;
		private readonly string _assetFolderPath;
		public TestGame(IGameSynchronizer gameSynchronizer, GameInput gameInput, IServiceProvider serviceProvider, string assetFolderPath) {
			_gameSynchronizer = gameSynchronizer;
			_gameInput = gameInput;
			_serviceProvider = serviceProvider;
			_assetFolderPath = assetFolderPath;
		}
		public override void Initialize(ISpriteBatch spriteBatch) {
			ActiveScene = new Scene(spriteBatch, new AssetManager(new FileManager(), _serviceProvider, _assetFolderPath));
			var testGO = ActiveScene.CreateGameObject();
			//_gameSynchronizer.GameObjectTrackedManager.Create(Guid.Parse("00cff980-706c-4ba4-88c9-072b708a7f1c"), testGO);
			testGO.Components.Add(testGO.CreateTextureComponent("wood_box"));
			var pc = testGO.CreatePositionComponent(50, 50);
			_gameSynchronizer.ComponentPropertyTrackedManager.Create(Guid.Parse("3f37f38b-fe55-4746-af8f-3373cc2e86d8"), pc.X);
			_gameSynchronizer.ComponentPropertyTrackedManager.Create(Guid.Parse("fc7cca88-edff-4127-821c-9aea1b5dabd3"), pc.Y);
			testGO.Components.Add(pc);
			testGO.Components.Add(testGO.CreateScriptComponent((o, time) => {
				if (!_gameInput.IsRight) {
					return;
				}
				var positionComponent = testGO.Components.Get<PositionComponent>();
				positionComponent.X.SetValue(positionComponent.X.Value + 0.005 * time.ElapsedGameTime.TotalMilliseconds);
			}));
			ActiveScene.GameObjects.Add(testGO);
			base.Initialize(spriteBatch);
		}
	}
}