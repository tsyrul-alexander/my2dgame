using Microsoft.Xna.Framework;
using My2DGame.Content.Utilities;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Utilities;
using My2DGame.Network.Client.Manager;
using Newtonsoft.Json.Linq;

namespace My2DGame.Content.Manager.Json {
	public class JsonGameObjectContentManager : BaseContentManager<IGameObject> {
		public ITrackedManager<IGameObject> GameObjectTrackedManager { get; }
		private readonly IContentManager<IGameObjectComponent> _componentContentManager;
		private const string ComponentsPropertyName = "components";
		public JsonGameObjectContentManager(ITrackedManager<IGameObject> gameObjectTrackedManager,
				IContentManager<IGameObjectComponent> componentContentManager) {
			GameObjectTrackedManager = gameObjectTrackedManager;
			_componentContentManager = componentContentManager;
		}
		public override IGameObject Load(string content) {
			var gameObjectJObject = JObject.Parse(content);
			var gameObject = CreateGameObject();
			SetProperties(gameObjectJObject, gameObject);
			gameObjectJObject.SetNetworkItem(GameObjectTrackedManager, gameObject);
			return gameObject;
		}
		public override string Save(IGameObject item) {
			return new JObject {
				item.VisibleToJProperty(),
				item.EnabledToJProperty(),
				{"PositionX", item.Position.X},
				GameObjectTrackedManager.NetworkItemToJProperty(item),
				{"PositionY", item.Position.Y},
				{ComponentsPropertyName, ToJArray(_componentContentManager, item.Components)}
			}.ToString();
		}
		protected virtual void SetProperties(JObject jObject, IGameObject gameObject) {
			var xPosition = jObject.GetValue("PositionX").Value<float>();
			var yPosition = jObject.GetValue("PositionY").Value<float>();
			gameObject.Position = new Vector2(xPosition, yPosition);
			gameObject.JPropertyToEnabled(jObject);
			gameObject.JPropertyToVisible(jObject);
			var components = FromJArray(_componentContentManager, (JArray) jObject.GetValue(ComponentsPropertyName));
			components.ForEach(component => {
				gameObject.Components.Add(component);
				component.GameObject = gameObject;
			});
		}
		protected virtual IGameObject CreateGameObject() {
			return new GameObject();
		}
	}
}
