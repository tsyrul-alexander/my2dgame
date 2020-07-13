using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Core.Component.GameObject {
	public abstract class BaseGameObjectComponent : IGameObjectComponent {
		private bool _enabled;
		private bool _visible;
		private IGameObject _gameObject;
		public event SilentPropertyChangedEventHandler PropertyChanged;
		public IGameObject GameObject {
			get => _gameObject;
			set {
				if (Equals(value, _gameObject))
					return;
				_gameObject = value;
				OnPropertyChanged();
			}
		}
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
		public abstract IProperty[] GetProperties();
		public virtual void Update(GameTime gameTime) { }
		public virtual void Draw(GameTime gameTime) { }
		public object Clone() {
			throw new NotImplementedException();
		}
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null, bool isSilent = false) {
			PropertyChanged?.Invoke(this, new SilentPropertyChangedEventArgs(propertyName, isSilent));
		}
	}
}