using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using My2DGame.Core;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Component.Animation {
	public class AnimationComponent : BaseGameObjectComponent {
		private readonly Dictionary<int, Texture2D> _animations;
		public IDictionary<int, string> Animations { get; }
		public IntegerProperty CurrentAnimation { get; set; }
		public AnimationComponent(IDictionary<int, string> animations, int startAnimation = -1) : this(animations,
			new IntegerProperty(startAnimation)) {
		}
		public AnimationComponent(IDictionary<int, string> animations, IntegerProperty startAnimationProperty) {
			_animations = new Dictionary<int, Texture2D>();
			Animations = animations;
			CurrentAnimation = startAnimationProperty;
		}
		private void CurrentTextureOnPropertyChanged(object sender, SilentPropertyChangedEventArgs e) {
			UpdateGameObjectTexture();
		}
		public override void Initialize() {
			base.Initialize();
			foreach (var (key, value) in Animations) {
				_animations.Add(key, GameObject.Scene.AssetManager.LoadTexture(value));
			}
			UpdateGameObjectTexture();
			CurrentAnimation.PropertyChanged += CurrentTextureOnPropertyChanged;
		}
		protected virtual void UpdateGameObjectTexture() {
			if (CurrentAnimation.Value == -1) {
				return;
			}
			var texture = _animations[CurrentAnimation.Value];
			SetGameObjectTexture(texture);
		}
		protected virtual void SetGameObjectTexture(Texture2D texture2D) {
			GameObject.Texture = texture2D;
		}
	}
}
