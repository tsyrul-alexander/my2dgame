using System;
using Microsoft.Xna.Framework;
using My2DGame.Component.Position;
using My2DGame.Component.Script;
using My2DGame.Component.Texture;
using My2DGame.Core.GameObject;

namespace My2DGame.Component.Utilities {
	public static class ComponentUtilities {
		public static TextureComponent CreateTextureComponent(this IGameObject gameObject, string textureName) {
			return new TextureComponent(textureName) {
				GameObject = gameObject
			};
		}
		public static PositionComponent CreatePositionComponent(this IGameObject gameObject, int x, int y) {
			return new PositionComponent(x, y) {
				GameObject = gameObject
			};
		}
		public static ScriptComponent CreateScriptComponent(this IGameObject gameObject, Action<IGameObject, GameTime> action) {
			return new ScriptComponent(action) {
				GameObject = gameObject
			};
		}
	}
}
