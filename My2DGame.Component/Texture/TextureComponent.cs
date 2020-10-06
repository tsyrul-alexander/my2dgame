using Microsoft.Xna.Framework;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Component.Texture {
	public class TextureComponent : BaseGameObjectComponent {
		public StringProperty TextureName { get; }
		public IntegerProperty Wight { get; }
		public IntegerProperty Height { get; }
		public TextureComponent(string textureName, int wight = -1, int height = -1):
			this(new StringProperty(textureName), new IntegerProperty(wight < 0 ? 1 : wight),
				new IntegerProperty(height < 0 ? 1 : height)) { }
		public TextureComponent(StringProperty textureName, IntegerProperty wight, IntegerProperty height) {
			TextureName = textureName;
			Wight = wight;
			Height = height;
		}
		public override void Initialize() {
			base.Initialize();
			LoadTexture();
		}
		public void LoadTexture() {
			GameObject.Texture = GameObject.Scene.AssetManager.LoadTexture(TextureName.Value);
			var gameObjectHeight = GameObject.Texture.Height;
			var gameObjectWidth = GameObject.Texture.Width;
			GameObject.Scale = new Vector2((float)1 / ((float)gameObjectHeight / (float)Height.Value), (float)1 / ((float)gameObjectWidth / (float)Wight.Value));
		}
		public override void SetSilentValue(string propertyName, object value) {
			base.SetSilentValue(propertyName, value);
			if (propertyName == nameof(TextureName)) {
				TextureName.SetSilentValue((string)value);
			}
		}
	}
}
