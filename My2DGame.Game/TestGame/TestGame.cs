using System;
using My2DGame.Component.Position;
using My2DGame.Component.Utilities;
using My2DGame.Core.Manager;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;

namespace My2DGame.Game.TestGame {
	public class TestGame : BaseGame {
		private readonly IServiceProvider _serviceProvider;
		private readonly string _assetFolderPath;
		public TestGame(IServiceProvider serviceProvider, string assetFolderPath) {
			_serviceProvider = serviceProvider;
			_assetFolderPath = assetFolderPath;
		}
		public override void Initialize(ISpriteBatch spriteBatch) {
			ActiveScene = new Scene(spriteBatch, new AssetManager(new FileManager(), _serviceProvider, _assetFolderPath));
			var testGO = ActiveScene.CreateGameObject();
			testGO.Components.Add(testGO.CreateTextureComponent("wood_box"));
			testGO.Components.Add(testGO.CreatePositionComponent(50, 50));
			testGO.Components.Add(testGO.CreateScriptComponent((o, time) => {
				var positionComponent = testGO.Components.Get<PositionComponent>();
				positionComponent.X.SetValue(positionComponent.X.Value + 0.005 * time.ElapsedGameTime.TotalMilliseconds);;
			}));
			ActiveScene.GameObjects.Add(testGO);
			base.Initialize(spriteBatch);
		}
	}
}