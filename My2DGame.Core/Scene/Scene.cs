using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using My2DGame.Core.Component.UI;
using My2DGame.Core.GameObject;
using My2DGame.Core.Utility;

namespace My2DGame.Core.Scene {
	public class Scene : IScene {
		public string Name { get; set; }
		public ISpriteBatch SpriteBatch { get; }
		public bool Enabled { get; set; }
		public bool Visible { get; set; }
		public ObservableCollection<IGameObject> GameObjects { get; } = new ObservableCollection<IGameObject>();
		public Scene(ISpriteBatch spriteBatch) {
			SpriteBatch = spriteBatch;
		}
		public IGameObject CreateGameObject() {
			return new GameObject.GameObject {
				Scene = this
			};
		}
		public void Update(GameTime gameTime) {
			GameObjects.ForEach(o => o.Update(gameTime));
		}
		public void Draw(GameTime gameTime) {
			GameObjects.ForEach(o => o.Draw(gameTime));
		}
	}
}