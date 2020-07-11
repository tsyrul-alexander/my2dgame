using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using My2DGame.Core.GameObject;

namespace My2DGame.Core.Component.GameObject {
	public abstract class BaseGameObjectComponent : IGameObjectComponent {
		private bool _enabled;
		private bool _visible;
		private IGameObject _gameObject;
		public event PropertyChangedEventHandler PropertyChanged;
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
		public virtual void Update(GameTime gameTime) { }
		public virtual void Draw(GameTime gameTime) { }
		public object Clone() {
			throw new NotImplementedException();
		}
		[Annotations.NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}