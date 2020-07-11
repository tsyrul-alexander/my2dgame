using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Scene;
using My2DGame.Core.Utility;

namespace My2DGame.Core.GameObject {
	public class GameObject : IGameObject {
		private bool _enabled;
		private bool _visible;
		public event PropertyChangedEventHandler PropertyChanged;
		public IScene Scene { get; internal set; }
		public GameObjectComponentCollection Components { get; } = new GameObjectComponentCollection();
		public bool Enabled {
			get => _enabled;
			set {
				if (value == _enabled)
					return;
				_enabled = value;
				OnPropertyChanged();
			}
		}
		public bool Visible {
			get => _visible;
			set {
				if (value == _visible)
					return;
				_visible = value;
				OnPropertyChanged();
			}
		}
		public virtual void Initialize() { }
		public virtual void Update(GameTime gameTime) {
			if (!Enabled) {
				return;
			}
			Components.ForEach(o => o.Update(gameTime));
		}
		public virtual void Draw(GameTime gameTime) {
			if (!Visible) {
				return;
			}
			Components.ForEach(o => o.Draw(gameTime));
		}
		public virtual object Clone() {
			var newGameObject = Scene.CreateGameObject();
			newGameObject.Enabled = Enabled;
			newGameObject.Visible = Visible;
			newGameObject.Initialize();
			return newGameObject;
		}
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
