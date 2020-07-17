using System;
using System.Collections.ObjectModel;
using My2DGame.Core.GameObject;
using My2DGame.Core.GameObject.Collider;
using My2DGame.Core.Manager;
using My2DGame.Core.UI;

namespace My2DGame.Core.Scene {
	public interface IScene : ISilentPropertyChanged, IUpdateable, IDrawable {
		string Name { get; set; }
		IServiceProvider ServiceProvider { get; }
		ISpriteBatch SpriteBatch { get; }
		IAssetManager AssetManager { get; }
		ICollisionManager CollisionManager { get; }
		ObservableCollection<IGameObject> GameObjects { get; }
		IGameObject CreateGameObject();
		void Initialize();
		void SetActive(bool isActive);
	}
}