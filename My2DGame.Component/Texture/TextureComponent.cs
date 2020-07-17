using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Component.Texture {
	public class TextureComponent : BaseGameObjectComponent {
		public StringProperty TextureName { get; set; }
		public TextureComponent(string textureName): this(new StringProperty(textureName)) { }
		public TextureComponent(StringProperty textureName) {
			TextureName = textureName;
		}
		public override void Initialize() {
			GameObject.Texture = GameObject.Scene.AssetManager.LoadTexture(TextureName.Value);
			base.Initialize();
		}
		public override IProperty[] GetProperties() {
			return new IProperty[] { TextureName };
		}
	}
}
