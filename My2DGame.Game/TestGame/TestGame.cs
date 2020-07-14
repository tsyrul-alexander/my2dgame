using System;
using My2DGame.Component.Collider;
using My2DGame.Component.Position;
using My2DGame.Component.Utilities;
using My2DGame.Core.GameObject.Collider;
using My2DGame.Core.Manager;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;
using My2DGame.Network.Client.Synchronizer;
using My2DGame.Network.Client.Utilities;

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
			ActiveScene = new Scene(spriteBatch, new AssetManager(new FileManager(), _serviceProvider, _assetFolderPath), new CollisionManager());
			var testGO = ActiveScene.CreateGameObject();
			//_gameSynchronizer.GameObjectTrackedManager.Create(Guid.Parse("00cff980-706c-4ba4-88c9-072b708a7f1c"), testGO);
			var tc = testGO.CreateTextureComponent("wood_box");
			_gameSynchronizer.Add(Guid.Parse("8bd02f12-9393-44e8-a8f9-2219b9eccc05"), tc.TextureName);
			testGO.Components.Add(tc);
			var pc = testGO.CreatePositionComponent();
			_gameSynchronizer.Add(Guid.Parse("3f37f38b-fe55-4746-af8f-3373cc2e86d8"), pc.X);
			_gameSynchronizer.Add(Guid.Parse("fc7cca88-edff-4127-821c-9aea1b5dabd3"), pc.Y);
			testGO.Components.Add(pc);
			var col1 = testGO.CreateColliderComponent(0, 0, 512, 512);
			col1.Collision += (component, item) => {

			};
			testGO.Components.Add(col1);
			testGO.Components.Add(testGO.CreateScriptComponent((o, time) => {
				if (!_gameInput.IsRight) {
					return;
				}
				var positionComponent = testGO.Components.Get<PositionComponent>();
				positionComponent.X.SetValue(positionComponent.X.Value + 0.1 * time.ElapsedGameTime.TotalMilliseconds);
			}));
			var test2GO = ActiveScene.CreateGameObject();
			test2GO.Components.Add(test2GO.CreateTextureComponent("wood_box"));
			test2GO.Components.Add(test2GO.CreatePositionComponent(650));
			var col2 = test2GO.CreateColliderComponent(0, 0, 512, 512);
			col2.Collision += (component, item) => {

			};
			test2GO.Components.Add(col2);
			ActiveScene.GameObjects.Add(testGO);
			ActiveScene.GameObjects.Add(test2GO);
			base.Initialize(spriteBatch);
		}
	}
}