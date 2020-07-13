using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;
using My2DGame.Core.Scene;
using My2DGame.Core.Utilities;
using My2DGame.Game;
using My2DGame.Network.Manager;

namespace My2DGame.Network.Synchronizer {
	public class GameSynchronizer : IGameSynchronizer {
		public IGame Game { get; }
		public ITrackedManager<IScene> SceneTrackedManager { get; }
		public ITrackedManager<IGameObject> GameObjectTrackedManager { get; }
		public ITrackedManager<IGameObjectComponent> GameObjectComponentTrackedManager { get; }
		public ITrackedManager<IProperty> ComponentPropertyTrackedManager { get; }
		public GameSynchronizer(IGame game) {
			Game = game;
			SceneTrackedManager = new SceneTrackedManager(this);
			GameObjectTrackedManager = new GameObjectTrackedManager(this);
			GameObjectComponentTrackedManager = new GameObjectComponentTrackedManager(this);
			ComponentPropertyTrackedManager = new ComponentPropertyTrackedManager(this);
		}
		public virtual void Initialize() {
			SceneTrackedManager.Initialize();
			GameObjectTrackedManager.Initialize();
			GameObjectComponentTrackedManager.Initialize();
			ComponentPropertyTrackedManager.Initialize();
			SceneTrackedManager.Create(Game.ActiveScene);
			Game.ActiveScene.GameObjects.ForEach(o => {//todo refactor
				GameObjectTrackedManager.Create(o);
				o.Components.ForEach(component => {
					GameObjectComponentTrackedManager.Create(component);
					component.GetProperties().ForEach(property => ComponentPropertyTrackedManager.Create(property));
				});
			});
		}
		public void SetScenePropertyValue(ManagerPropertyValue propertyValue) {
			SceneTrackedManager.Update(propertyValue);
		}
		public void SetGameObjectPropertyValue(ManagerPropertyValue propertyValue) {
			GameObjectTrackedManager.Update(propertyValue);
		}
		public void SetGameObjectComponentPropertyValue(ManagerPropertyValue propertyValue) {
			GameObjectComponentTrackedManager.Update(propertyValue);
		}
		public void SetComponentPropertyPropertyValue(ManagerPropertyValue propertyValue) {
			ComponentPropertyTrackedManager.Update(propertyValue);
		}
	}
}
