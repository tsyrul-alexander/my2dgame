using System;
using My2DGame.Component.Animation;
using My2DGame.Component.Collider;
using My2DGame.Component.Position;
using My2DGame.Component.Script;
using My2DGame.Component.Texture;
using My2DGame.Content.Utilities;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Property;
using Newtonsoft.Json.Linq;

namespace My2DGame.Content.Manager.Json {
	public class JsonGameObjectComponentContentManager : BaseContentManager<IGameObjectComponent> {
		protected virtual IContentManager<IProperty> PropertyContentManager { get; }
		private const string PropertyPropertyName = "property";
		private const string TypePropertyName = "type";
		private const string TextureComponentName = "texture";
		private const string PositionComponentName = "position";
		private const string ColliderComponentName = "collider";
		private const string ScriptComponentName = "script";
		private const string AnimationComponentName = "animation";

		public JsonGameObjectComponentContentManager(IContentManager<IProperty> propertyContentManager) {
			PropertyContentManager = propertyContentManager;
		}
		public override IGameObjectComponent Load(string content) {
			var componentJObject = JObject.Parse(content);
			var type = componentJObject.GetValue(TypePropertyName).Value<string>();
			var property = (JObject) componentJObject.GetValue(PropertyPropertyName);
			var component = CreateComponent(type, property);
			component.JPropertyToEnabled(componentJObject);
			component.JPropertyToVisible(componentJObject);
			return component;
		}

		public override string Save(IGameObjectComponent item) {
			var (componentType, propertyJObject) = GetComponentType(item);
			return new JObject {
				item.EnabledToJProperty(),
				item.VisibleToJProperty(),
				{TypePropertyName, componentType},
				{PropertyPropertyName, propertyJObject}
			}.ToString();
		}
		protected virtual IGameObjectComponent CreateComponent(string componentType, JObject propertyJObject) {
			switch (componentType) {
				case TextureComponentName:
					return GetTextureComponent(propertyJObject);
				case PositionComponentName:
					return GetPositionComponent(propertyJObject);
				case ColliderComponentName:
					return GetColliderComponent(propertyJObject);
				case ScriptComponentName:
					return GetScriptComponent(propertyJObject);
				case AnimationComponentName:
					return GetAnimationComponent(propertyJObject);
				default:
					throw new NotImplementedException(componentType);
			}
		}
		private IGameObjectComponent GetAnimationComponent(JObject propertyJObject) {
			return new AnimationComponent(JArrayToDictionary<int, string>((JArray)propertyJObject.GetValue("animations")),
				(IntegerProperty)PropertyContentManager.Load(propertyJObject.GetValue("current_animation").ToString()));
		}
		private IGameObjectComponent GetScriptComponent(JObject propertyJObject) {
			return new ScriptComponent(
				(StringProperty) PropertyContentManager.Load(propertyJObject.GetValue("action").ToString()));
		}
		private IGameObjectComponent GetColliderComponent(JObject propertyJObject) {
			return new ColliderComponent(
				(IntegerProperty) PropertyContentManager.Load(propertyJObject.GetValue("x").ToString()),
				(IntegerProperty) PropertyContentManager.Load(propertyJObject.GetValue("y").ToString()),
				(IntegerProperty) PropertyContentManager.Load(propertyJObject.GetValue("width").ToString()),
				(IntegerProperty) PropertyContentManager.Load(propertyJObject.GetValue("height").ToString()));
		}
		private IGameObjectComponent GetPositionComponent(JObject propertyJObject) {
			return new PositionComponent(
				(DoubleProperty) PropertyContentManager.Load(propertyJObject.GetValue("x").ToString()),
				(DoubleProperty) PropertyContentManager.Load(propertyJObject.GetValue("y").ToString()));
		}
		private IGameObjectComponent GetTextureComponent(JObject propertyJObject) {
			return new TextureComponent(
				(StringProperty) PropertyContentManager.Load(propertyJObject.GetValue("texture_name").ToString()));
		}
		protected virtual (string componentType, JObject propertyJObject) GetComponentType(IGameObjectComponent item) {
			switch (item) {
				case TextureComponent textureComponent:
					return (TextureComponentName, GetTexturePropertyJObject(textureComponent));
				case PositionComponent positionComponent:
					return (PositionComponentName, GetPositionPropertyJObject(positionComponent));
				case ColliderComponent colliderComponent:
					return (ColliderComponentName, GetColliderPropertyJObject(colliderComponent));
				case ScriptComponent scriptComponent:
					return (ScriptComponentName, GetScriptPropertyJObject(scriptComponent));
				case AnimationComponent animationComponent:
					return (AnimationComponentName, GetAnimationPropertyJObject(animationComponent));
				default:
					throw new NotImplementedException(nameof(item));
			}
		}
		private JObject GetAnimationPropertyJObject(AnimationComponent animationComponent) {
			return new JObject(
				new JProperty("current_animation",
					GetContentManagerItemJObject(PropertyContentManager, animationComponent.CurrentAnimation)),
				new JProperty("animations",
					DictionaryToJArray(animationComponent.Animations)));
		}
		private JObject GetScriptPropertyJObject(ScriptComponent scriptComponent) {
			return new JObject(new JProperty("action",
				GetContentManagerItemJObject(PropertyContentManager, scriptComponent.ActionProperty)));
		}
		private JObject GetColliderPropertyJObject(ColliderComponent colliderComponent) {
			return new JObject(
				new JProperty("x", GetContentManagerItemJObject(PropertyContentManager, colliderComponent.XProperty)),
				new JProperty("y", GetContentManagerItemJObject(PropertyContentManager, colliderComponent.YProperty)),
				new JProperty("height",
					GetContentManagerItemJObject(PropertyContentManager, colliderComponent.HeightProperty)),
				new JProperty("width",
					GetContentManagerItemJObject(PropertyContentManager, colliderComponent.WidthProperty)));
		}
		private JObject GetPositionPropertyJObject(PositionComponent positionComponent) {
			return new JObject(
				new JProperty("x", GetContentManagerItemJObject(PropertyContentManager, positionComponent.X)),
				new JProperty("y", GetContentManagerItemJObject(PropertyContentManager, positionComponent.Y)));
		}
		private JObject GetTexturePropertyJObject(TextureComponent textureComponent) {
			return new JObject(new JProperty("texture_name",
				GetContentManagerItemJObject(PropertyContentManager, textureComponent.TextureName)));
		}
	}
}