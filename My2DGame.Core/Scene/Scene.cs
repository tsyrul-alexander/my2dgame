using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using My2DGame.Core.GameObject;
using My2DGame.Core.Manager;
using My2DGame.Core.UI;
using My2DGame.Core.Utilities;

namespace My2DGame.Core.Scene {
	public class Scene : IScene {
		private bool _enabled;
		private bool _visible;
		public string Name { get; set; }
		public ISpriteBatch SpriteBatch { get; }
		public IAssetManager AssetManager { get; }
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
		public ObservableCollection<IGameObject> GameObjects { get; } = new ObservableCollection<IGameObject>();
		public Scene(ISpriteBatch spriteBatch, IAssetManager assetManager) {
			SpriteBatch = spriteBatch;
			AssetManager = assetManager;
		}
		
		public IGameObject CreateGameObject() {
			return new GameObject.GameObject {
				Scene = this
			};
		}
		public void Initialize() {
			GameObjects.ForEach(o => o.Initialize());
		}
		public void Update(GameTime gameTime) {
			GameObjects.UpdateableEach(gameTime);
		}
		public void Draw(GameTime gameTime) {
			SpriteBatch.StartDraw();
			GameObjects.DrawableEach(gameTime);
			SpriteBatch.EndDraw();
		}
		public event PropertyChangedEventHandler PropertyChanged;
		[Annotations.NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}