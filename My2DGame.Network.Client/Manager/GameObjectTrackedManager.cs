using System;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Scene;
using My2DGame.Network.Client.Tracker;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Manager {
	public class GameObjectTrackedManager : TrackedManager<IGameObject> {
		protected readonly ITrackedManager<IScene> SceneTrackedManager;
		public GameObjectTrackedManager(ITrackedManager<IScene> sceneTrackedManager) {
			SceneTrackedManager = sceneTrackedManager;
		}
		protected override ITracker<IGameObject> CreateTracked(IGameObject value) {
			return new GameObjectTracker(value);
		}
		protected override CreateManagerItem GetCreateManagerItem(ITracker<IGameObject> tracker) {
			var sceneNetworkId = SceneTrackedManager.GetItem(tracker.Value.Scene);
			return new CreateManagerItem {
				Id = tracker.Id,
				ManagerName = GetManagerName(),
				ParentItemId = sceneNetworkId,
				Values = new[] {
					new PropertyValue(nameof(IGameObject.Enabled), tracker.Value.Enabled),
					new PropertyValue(nameof(IGameObject.Visible), tracker.Value.Visible)
					//new PropertyValue(nameof(IGameObject.Color), tracker.Value.Color)
				}
			};
		}
		protected override string GetManagerName() {
			return nameof(GameObjectTrackedManager);
		}
		protected override IGameObject CreateItem(CreateManagerItem createManagerItem) {
			var scene = SceneTrackedManager.GetItem(createManagerItem.ParentItemId.Value);
			return scene.AddGameObject();
		}
		protected override void OnTrackerItemCreated(CreateManagerItem createManagerItem, ITracker<IGameObject> tracker) {
			base.OnTrackerItemCreated(createManagerItem, tracker);
			tracker.Value.Initialize();
		}
	}
}
