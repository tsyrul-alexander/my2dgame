using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using My2DGame.Core.GameObject;
using My2DGame.Core.GameObject.Collider;
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
		public ICollisionManager CollisionManager { get; }
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
		public Scene(ISpriteBatch spriteBatch, IAssetManager assetManager, ICollisionManager collisionManager) {
			SpriteBatch = spriteBatch;
			AssetManager = assetManager;
			CollisionManager = collisionManager;
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
			CollisionManager.Update(gameTime);
		}
		public void Draw(GameTime gameTime) {
			SpriteBatch.StartDraw();
			GameObjects.DrawableEach(gameTime);
			SpriteBatch.EndDraw();
		}
		public event SilentPropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null, bool isSilent = false) {
			PropertyChanged?.Invoke(this, new SilentPropertyChangedEventArgs(propertyName, isSilent));
		}
	}
}