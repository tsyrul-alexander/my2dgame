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
				OnPropertyChanged();
			}
		}
		public Vector2 Position {
			get => _position;
			set {
				if (_position.Equals(value)) {
					return;
				}
				_position = value;
				OnPropertyChanged();
			}
		}
		public Rectangle? SourceRectangle { get; set; }
		public Color Color { get; set; } = Color.White;
		public float Rotation { get; set; }
		public Vector2 Origin { get; set; }
		public float Scale { get; set; } = 1;
		public SpriteEffects Effects { get; set; } = SpriteEffects.None;
		public float LayerDepth { get; set; }
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null, bool isSilent = false) {
			PropertyChanged?.Invoke(this, new SilentPropertyChangedEventArgs(propertyName, isSilent));
		}
	}
}