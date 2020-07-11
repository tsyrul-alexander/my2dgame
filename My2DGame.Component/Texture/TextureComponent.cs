using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Component.Texture {
	public class TextureComponent : BaseGameObjectComponent {
		public StringProperty TextureName { get; set; }
		internal TextureComponent(string textureName) {
			TextureName = new StringProperty {Value = textureName};
		}
		public override void Initialize() {
			GameObject.Texture = GameObject.Scene.AssetManager.LoadTexture(TextureName.Value);
			base.Initialize();
		}
	}
}
