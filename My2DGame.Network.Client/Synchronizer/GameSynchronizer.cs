using System;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;
using My2DGame.Core.Scene;
using My2DGame.Network.Client.Client;
using My2DGame.Network.Client.Contract;
using My2DGame.Network.Client.Manager;

namespace My2DGame.Network.Client.Synchronizer {
	public class GameSynchronizer : IGameSynchronizer {
		public INetworkClient NetworkClient { get; }
		public ITrackedManager<IScene> SceneTrackedManager { get; }
		public ITrackedManager<IGameObject> GameObjectTrackedManager { get; }
		public ITrackedManager<IGameObjectComponent> GameObjectComponentTrackedManager { get; }
		public ITrackedManager<IProperty> ComponentPropertyTrackedManager { get; }
		public GameSynchronizer(INetworkClient networkClient) {
			SceneTrackedManager = new SceneTrackedManager(this);
			GameObjectTrackedManager = new GameObjectTrackedManager(this);
			GameObjectComponentTrackedManager = new GameObjectComponentTrackedManager(this);
			ComponentPropertyTrackedManager = new ComponentPropertyTrackedManager(this);
			NetworkClient = networkClient;
			SceneTrackedManager.ItemPropertyChanged += TrackedManagerOnItemPropertyChanged;
			GameObjectTrackedManager.ItemPropertyChanged += TrackedManagerOnItemPropertyChanged;
			GameObjectComponentTrackedManager.ItemPropertyChanged += TrackedManagerOnItemPropertyChanged;
			ComponentPropertyTrackedManager.ItemPropertyChanged += TrackedManagerOnItemPropertyChanged;
		}
		private void TrackedManagerOnItemPropertyChanged(ManagerPropertyValue obj) {
			NetworkClient.Send(obj);
		}
		public virtual void Initialize(string ipAddress, int port) {
			NetworkClient.Connect(ipAddress, port);
			SceneTrackedManager.Initialize();
			GameObjectTrackedManager.Initialize();
			GameObjectComponentTrackedManager.Initialize();
			ComponentPropertyTrackedManager.Initialize();
			//SceneTrackedManager.Create(Game.ActiveScene);
			//Game.ActiveScene.GameObjects.ForEach(o => {//todo refactor
			//	GameObjectTrackedManager.Create(o);
			//	o.Components.ForEach(component => {
			//		GameObjectComponentTrackedManager.Create(component);
			//		component.GetProperties().ForEach(property => ComponentPropertyTrackedManager.Create(property));
			//	});
			//});
			NetworkClient.Message += NetworkClientOnMessage;
		}
		private void NetworkClientOnMessage(INetworkObject obj) {
			if (obj is ManagerPropertyValue managerPropertyValue) {
				SetManagerPropertyValue(managerPropertyValue);
			}
		}
		protected virtual void SetManagerPropertyValue(ManagerPropertyValue managerPropertyValue) {
			if (managerPropertyValue.ManagerName == nameof(Manager.SceneTrackedManager)) {
				SetScenePropertyValue(managerPropertyValue);
			} else if (managerPropertyValue.ManagerName == nameof(Manager.GameObjectTrackedManager)) {
				SetGameObjectPropertyValue(managerPropertyValue);
			} else if (managerPropertyValue.ManagerName == nameof(Manager.GameObjectComponentTrackedManager)) {
				SetGameObjectComponentPropertyValue(managerPropertyValue);
			} else if (managerPropertyValue.ManagerName == nameof(Manager.ComponentPropertyTrackedManager)) {
				SetComponentPropertyPropertyValue(managerPropertyValue);
			} else {
				throw new NotImplementedException(nameof(managerPropertyValue.ManagerName));
			}
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
