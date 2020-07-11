using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using My2DGame.Core.GameObject;
using My2DGame.Core.Manager;
using My2DGame.Core.UI;
using My2DGame.Core.Utility;

namespace My2DGame.Core.Scene {
	public class Scene : IScene {
		public string Name { get; set; }
		public ISpriteBatch SpriteBatch { get; }
		public IAssetManager AssetManager { get; }
		public bool Enabled { get; set; }
		public bool Visible { get; set; }
		public ObservableCollection<IGameObject> GameObjects { get; } = new ObservableCollection<IGameObject>();
		public Scene(ISpriteBatch spriteBatch, IAssetManager assetManager) {
			SpriteBatch = spriteBatch;
			AssetManager = assetManager;
		}
		
		public IGameObject CreateGameObject() {
			return new GameObject.GameObject {
				Scene = this
			};
		}
		public void Initialize() {
			GameObjects.ForEach(o => o.Initialize());
		}
		public void Update(GameTime gameTime) {
			GameObjects.UpdateableEach(gameTime);
		}
		public void Draw(GameTime gameTime) {
			SpriteBatch.StartDraw();
			GameObjects.DrawableEach(gameTime);
			SpriteBatch.EndDraw();
		}
	}
}