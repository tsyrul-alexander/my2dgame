using System;
using Microsoft.Xna.Framework;
using My2DGame.Component.Collider;
using My2DGame.Component.Position;
using My2DGame.Component.Script;
using My2DGame.Component.Texture;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;

namespace My2DGame.Component.Utilities {
	public static class ComponentUtilities {
		public static TextureComponent AddTextureComponent(this IGameObject gameObject, string textureName) {
			var component = new TextureComponent(textureName);
			gameObject.AddComponent(component);
			return component;
		}
		public static PositionComponent AddPositionComponent(this IGameObject gameObject, int x = default,
			int y = default) {
			var component = new PositionComponent(x, y);
			gameObject.AddComponent(component);
			return component;
		}
		public static ScriptComponent AddScriptComponent(this IGameObject gameObject, Type scriptActionType) {
			var component = new ScriptComponent(scriptActionType.FullName);
			gameObject.AddComponent(component);
			return component;
		}

		public static ColliderComponent AddColliderComponent(this IGameObject gameObject, int x = default,
			int y = default, int width = default, int height = default) {
			var component = new ColliderComponent(x, y, width, height);
			gameObject.AddComponent(component);
			return component;
		}
		public static IGameObjectComponent AddComponent(this IGameObject gameObject, IGameObjectComponent component) {
			component.GameObject = gameObject;
			gameObject.Components.Add(component);
			return component;
		}
	}
}
