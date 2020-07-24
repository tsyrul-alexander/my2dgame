using System;
using Microsoft.Extensions.Logging;
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
		public ILogger Logger { get; }
		public GameSynchronizer(INetworkClient networkClient, GameSynchronizerOptions options, 
			ITrackedManager<IScene> sceneTrackedManager, ITrackedManager<IGameObject> gameObjectTrackedManager,
			ITrackedManager<IGameObjectComponent> gameObjectComponentTrackedManager, ITrackedManager<IProperty> componentPropertyTrackedManager,
			ILogger<GameSynchronizer> logger) {
			_options = options;
			SceneTrackedManager = sceneTrackedManager;
			GameObjectTrackedManager = gameObjectTrackedManager;
			GameObjectComponentTrackedManager = gameObjectComponentTrackedManager;
			ComponentPropertyTrackedManager = componentPropertyTrackedManager;
			Logger = logger;
			NetworkClient = networkClient;
		}
		private void SceneTrackedManagerOnRemoveItem(RemoveManagerItem obj) {
			NetworkClient.Send(obj, _options.RoomId, QueryType.Remove);
		}
		private void SceneTrackedManagerOnAddItem(CreateManagerItem obj) {
			NetworkClient.Send(obj, _options.RoomId, QueryType.Create);
		}
		private void TrackedManagerOnItemPropertyChanged(ManagerPropertyValue obj) {
			NetworkClient.Send(obj, _options.RoomId, QueryType.Update);
		}
		public virtual void Initialize() {
			NetworkClient.Connect(_options.IpAddress, _options.Port);
			SceneTrackedManager.Initialize();
			GameObjectTrackedManager.Initialize();
			GameObjectComponentTrackedManager.Initialize();
			ComponentPropertyTrackedManager.Initialize();
			SceneTrackedManager.AddItem += SceneTrackedManagerOnAddItem;
			GameObjectTrackedManager.AddItem += SceneTrackedManagerOnAddItem;
			GameObjectComponentTrackedManager.AddItem += SceneTrackedManagerOnAddItem;
			ComponentPropertyTrackedManager.AddItem += SceneTrackedManagerOnAddItem;
			SceneTrackedManager.ItemPropertyChanged += TrackedManagerOnItemPropertyChanged;
			GameObjectTrackedManager.ItemPropertyChanged += TrackedManagerOnItemPropertyChanged;
			GameObjectComponentTrackedManager.ItemPropertyChanged += TrackedManagerOnItemPropertyChanged;
			ComponentPropertyTrackedManager.ItemPropertyChanged += TrackedManagerOnItemPropertyChanged;
			SceneTrackedManager.RemoveItem += SceneTrackedManagerOnRemoveItem;
			GameObjectTrackedManager.RemoveItem += SceneTrackedManagerOnRemoveItem;
			GameObjectComponentTrackedManager.RemoveItem += SceneTrackedManagerOnRemoveItem;
			ComponentPropertyTrackedManager.RemoveItem += SceneTrackedManagerOnRemoveItem;
			NetworkClient.Message += NetworkClientOnMessage;
			NetworkClient.Send(null, _options.RoomId, QueryType.GetAll);
		}
		private void NetworkClientOnMessage(INetworkObject obj) {
			Logger.Log(LogLevel.Debug, obj.RequestItemId.ToString());
			if (obj is ManagerPropertyValue managerPropertyValue) {
				SetManagerPropertyValue(managerPropertyValue);
			} else if (obj is CreateManagerItem createManagerItem) {
				CreateManagerPropertyItem(createManagerItem);
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
		protected virtual void CreateManagerPropertyItem(CreateManagerItem createManagerItem) {
			if (createManagerItem.ManagerName == nameof(Manager.SceneTrackedManager)) {
				CreateSceneItem(createManagerItem);
			} else if (createManagerItem.ManagerName == nameof(Manager.GameObjectTrackedManager)) {
				CreateGameObjectItem(createManagerItem);
			} else if (createManagerItem.ManagerName == nameof(Manager.GameObjectComponentTrackedManager)) {
				CreateGameObjectComponentItem(createManagerItem);
			} else if (createManagerItem.ManagerName == nameof(Manager.ComponentPropertyTrackedManager)) {
				CreateManagerPropertyItem(createManagerItem);
			} else {
				throw new NotImplementedException(nameof(createManagerItem.ManagerName));
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
		public void CreateSceneItem(CreateManagerItem createManagerItem) {
			SceneTrackedManager.Create(createManagerItem);
		}
		public void CreateGameObjectItem(CreateManagerItem createManagerItem) {
			GameObjectTrackedManager.Create(createManagerItem);
		}
		public void CreateGameObjectComponentItem(CreateManagerItem createManagerItem) {
			GameObjectComponentTrackedManager.Create(createManagerItem);
		}
		public void CreateComponentPropertyItem(CreateManagerItem createManagerItem) {
			ComponentPropertyTrackedManager.Create(createManagerItem);
		}
	}
}
