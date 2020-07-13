using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;
using My2DGame.Core.Scene;
using My2DGame.Network.Client.Client;
using My2DGame.Network.Client.Contract;
using My2DGame.Network.Client.Manager;

namespace My2DGame.Network.Client.Synchronizer {
	public interface IGameSynchronizer {
		INetworkClient NetworkClient { get; }
		ITrackedManager<IScene> SceneTrackedManager { get; }
		ITrackedManager<IGameObject> GameObjectTrackedManager { get; }
		ITrackedManager<IGameObjectComponent> GameObjectComponentTrackedManager { get; }
		ITrackedManager<IProperty> ComponentPropertyTrackedManager { get; }
		void Initialize(string ipAddress, int port);
		void SetScenePropertyValue(ManagerPropertyValue propertyValue);
		void SetGameObjectPropertyValue(ManagerPropertyValue propertyValue);
		void SetGameObjectComponentPropertyValue(ManagerPropertyValue propertyValue);
		void SetComponentPropertyPropertyValue(ManagerPropertyValue propertyValue);
	}
}