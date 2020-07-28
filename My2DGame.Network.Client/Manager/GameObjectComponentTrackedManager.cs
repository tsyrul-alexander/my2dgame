using System;
using System.Linq;
using My2DGame.Component.Animation;
using My2DGame.Component.Position;
using My2DGame.Component.Script;
using My2DGame.Component.Texture;
using My2DGame.Component.Utilities;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Network.Client.Tracker;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Manager {
	public class GameObjectComponentTrackedManager : TrackedManager<IGameObjectComponent> {
		public ITrackedManager<IGameObject> GameObjectTrackedManager { get; }
		public GameObjectComponentTrackedManager(ITrackedManager<IGameObject> gameObjectTrackedManager) {
			GameObjectTrackedManager = gameObjectTrackedManager;
		}
		protected override ITracker<IGameObjectComponent> CreateTracked(IGameObjectComponent value) {
			return new GameObjectComponentTracker(value);
		}
		protected override CreateManagerItem GetCreateManagerItem(ITracker<IGameObjectComponent> tracker) {
			var gameObjectNetworkId = GameObjectTrackedManager.GetItem(tracker.Value.GameObject);
			var componentInfo = GetComponentInfo(tracker.Value);
			return new CreateComponentManagerItem {
				Id = tracker.Id,
				ComponentName = componentInfo.name,
				ParentItemId = gameObjectNetworkId,
				ManagerName = GetManagerName(),
				Values = componentInfo.values.Concat(new[] {
					new PropertyValue(nameof(IGameObjectComponent.Enabled), tracker.Value.Enabled),
					new PropertyValue(nameof(IGameObjectComponent.Visible), tracker.Value.Visible)
				}).ToArray()
			};
		}
		protected override string GetManagerName() {
			return nameof(GameObjectComponentTrackedManager);
		}
		protected override IGameObjectComponent CreateItem(CreateManagerItem createManagerItem) {
			var createComponentItemInfo = (CreateComponentManagerItem)createManagerItem;
			var gameObject = GameObjectTrackedManager.GetItem(createComponentItemInfo.ParentItemId.Value);
			return CreateItem(gameObject, createComponentItemInfo.ComponentName);
		}
		protected override void OnTrackerItemCreated(CreateManagerItem createManagerItem, ITracker<IGameObjectComponent> tracker) {
			base.OnTrackerItemCreated(createManagerItem, tracker);
			tracker.Value.Initialize();
		}
		protected virtual IGameObjectComponent CreateItem(IGameObject gameObject, string name) {
			switch (name) {
				case nameof(PositionComponent):
					return gameObject.AddPositionComponent();
				case nameof(TextureComponent):
					return gameObject.AddTextureComponent();
				case nameof(AnimationComponent):
					return gameObject.AddAnimationComponent();
				case nameof(ScriptComponent):
					return gameObject.AddScriptComponent();
				default:
					throw new NotImplementedException(name);
			}
		}
		protected virtual (string name, PropertyValue[] values) GetComponentInfo(IGameObjectComponent gameObjectComponent) {
			switch (gameObjectComponent) {
				case PositionComponent positionComponent:
					return (nameof(PositionComponent), new[] {
						new PropertyValue(nameof(positionComponent.X), positionComponent.X.Value),
						new PropertyValue(nameof(positionComponent.Y), positionComponent.Y.Value)
					});
				case TextureComponent textureComponent:
					return (nameof(TextureComponent), new [] {
						new PropertyValue(nameof(textureComponent.TextureName), textureComponent.TextureName.Value) 
					});
				case AnimationComponent animationComponent:
					return (nameof(AnimationComponent), new [] {
						new PropertyValue(nameof(animationComponent.CurrentAnimation), animationComponent.CurrentAnimation.Value),
						new PropertyValue(nameof(animationComponent.Animations), animationComponent.Animations)
					});
				case ScriptComponent scriptComponent:
					return (nameof(ScriptComponent), new [] {
						new PropertyValue(nameof(scriptComponent.Actions), scriptComponent.Actions)
					});
				default:
					throw new NotImplementedException(nameof(gameObjectComponent));
			}
		}
	}
}