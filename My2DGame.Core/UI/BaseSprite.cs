using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace My2DGame.Core.UI {
	public abstract class BaseSprite : ISprite, ISilentPropertyChanged {
		private Texture2D _texture;
		private Vector2 _position;
		public event SilentPropertyChangedEventHandler PropertyChanged;
		public Texture2D Texture {
			get => _texture;
			set {
				if (_texture == value) {
					return;
				}
				_texture = value;
				OnPropertyChanged(value, nameof(Texture), true);
			}
		}
		public Vector2 Position {
			get => _position;
			set {
				if (_position.Equals(value)) {
					return;
				}
				_position = value;
				OnPropertyChanged(value, nameof(Position), true);
			}
		}
		public Rectangle? SourceRectangle { get; set; }
		public Color Color { get; set; } = Color.White;
		public float Rotation { get; set; }
		public Vector2 Origin { get; set; }
		public Vector2 Scale { get; set; }
		public SpriteEffects Effects { get; set; } = SpriteEffects.None;
		public float LayerDepth { get; set; }
		public void SetSilentValue(string propertyName, object value) {
			if (propertyName == nameof(Position)) {
				Position = (Vector2)value;
			}
		}
		protected virtual void OnPropertyChanged(object value, [CallerMemberName] string propertyName = null, bool isSilent = false) {
			PropertyChanged?.Invoke(this, new SilentPropertyChangedEventArgs(propertyName, value, isSilent));
		}
	}
}