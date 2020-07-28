using System;
using System.Collections.Generic;
using System.Linq;
using My2DGame.Component.Animation;
using My2DGame.Component.Collider;
using My2DGame.Component.Position;
using My2DGame.Component.Script;
using My2DGame.Component.Texture;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;

namespace My2DGame.Component.Utilities {
	public static class ComponentUtilities {
		public static AnimationComponent AddAnimationComponent(this IGameObject gameObject,
			IDictionary<int, string> animations = null, int startAnimation = -1) {
			var component = new AnimationComponent(animations ?? new Dictionary<int, string>(), startAnimation);
			gameObject.AddComponent(component);
			return component;
		}
		public static TextureComponent AddTextureComponent(this IGameObject gameObject, string textureName = "") {
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
		public static ScriptComponent AddScriptComponent(this IGameObject gameObject, params Type[] scriptActionTypes) {
			var component = new ScriptComponent(scriptActionTypes.Select(type => type?.FullName).ToArray());
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