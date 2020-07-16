using System;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;
using My2DGame.Core.Scene;
using My2DGame.Network.Client.Client;
using My2DGame.Network.Client.Manager;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Synchronizer {
	public class GameSynchronizer : IGameSynchronizer {
		private readonly GameSynchronizerOptions _options;
		public INetworkClient NetworkClient { get; }
		public ITrackedManager<IScene> SceneTrackedManager { get; }
		public ITrackedManager<IGameObject> GameObjectTrackedManager { get; }
		public ITrackedManager<IGameObjectComponent> GameObjectComponentTrackedManager { get; }
		public ITrackedManager<IProperty> ComponentPropertyTrackedManager { get; }
		public GameSynchronizer(INetworkClient networkClient, GameSynchronizerOptions options) {
			_options = options;
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
			NetworkClient.Send(obj, _options.RoomId);
		}
		public virtual void Initialize() {
			NetworkClient.Connect(_options.IpAddress, _options.Port);
			SceneTrackedManager.Initialize();
			GameObjectTrackedManager.Initialize();
			GameObjectComponentTrackedManager.Initialize();
			ComponentPropertyTrackedManager.Initialize();
			NetworkClient.Message += NetworkClientOnMessage;
			NetworkClient.Send(null, _options.RoomId);
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
