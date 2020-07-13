using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;
using My2DGame.Core.Scene;
using My2DGame.Network.Manager;

namespace My2DGame.Network.Synchronizer {
	public interface IGameSynchronizer {
		ITrackedManager<IScene> SceneTrackedManager { get; }
		ITrackedManager<IGameObject> GameObjectTrackedManager { get; }
		ITrackedManager<IGameObjectComponent> GameObjectComponentTrackedManager { get; }
		ITrackedManager<IProperty> ComponentPropertyTrackedManager { get; }
		void Initialize();
		void SetScenePropertyValue(ManagerPropertyValue propertyValue);
		void SetGameObjectPropertyValue(ManagerPropertyValue propertyValue);
		void SetGameObjectComponentPropertyValue(ManagerPropertyValue propertyValue);
		void SetComponentPropertyPropertyValue(ManagerPropertyValue propertyValue);
	}
}