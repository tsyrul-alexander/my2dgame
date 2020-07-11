using System;
using System.Collections.Generic;
using System.Text;
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
	}
}
