using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;
using My2DGame.Core.Utilities;

namespace My2DGame.Core.GameObject {
	public class GameObject : BaseSprite, IGameObject {
		private bool _enabled = true;
		private bool _visible = true;
		public IScene Scene { get; set; }
		public GameObjectComponentCollection Components { get; } = new GameObjectComponentCollection();
		public bool Enabled {
			get => _enabled;
			set {
				if (value == _enabled)
					return;
				_enabled = value;
				OnPropertyChanged(value);
			}
		}
		public bool Visible {
			get => _visible;
			set {
				if (value == _visible)
					return;
				_visible = value;
				OnPropertyChanged(value);
			}
		}
		public virtual void Initialize() {
			Components.ForEach(component => component.Initialize());
		}
		public virtual void Update(GameTime gameTime) {
			if (!Enabled) {
				return;
			}
			Components.UpdateableEach(gameTime);
		}
		public virtual void Draw(GameTime gameTime) {
			if (!Visible) {
				return;
			}
			Components.DrawableEach(gameTime);
			Scene.SpriteBatch.Draw(this);
		}
		public virtual object Clone() {
			var newGameObject = Scene.CreateGameObject();
			newGameObject.Enabled = Enabled;
			newGameObject.Visible = Visible;
			newGameObject.Initialize();
			return newGameObject;
		}
	}
}
