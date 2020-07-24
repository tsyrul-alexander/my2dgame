using System;
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
		private bool _enabled = true;
		private bool _visible = true;
		public string Name { get; set; }
		public IServiceProvider ServiceProvider { get; }
		public ISpriteBatch SpriteBatch { get; }
		public IAssetManager AssetManager { get; }
		public ICollisionManager CollisionManager { get; }
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
		public ObservableCollection<IGameObject> GameObjects { get; } = new ObservableCollection<IGameObject>();
		public Scene(ISpriteBatch spriteBatch, IAssetManager assetManager, ICollisionManager collisionManager, IServiceProvider serviceProvider) {
			SpriteBatch = spriteBatch;
			AssetManager = assetManager;
			CollisionManager = collisionManager;
			ServiceProvider = serviceProvider;
		}
		
		public IGameObject CreateGameObject() {
			return new GameObject.GameObject {
				Scene = this
			};
		}
		public IGameObject AddGameObject() {
			var gameObject = CreateGameObject();
			GameObjects.Add(gameObject);
			return gameObject;
		}
		public void Initialize() {
			GameObjects.ForEach(o => o.Initialize());
		}
		public void SetActive(bool isActive) {
			Enabled = isActive;
			Visible = isActive;
		}
		public void Update(GameTime gameTime) {
			if (!Enabled) {
				return;
			}
			GameObjects.UpdateableEach(gameTime);
		}
		public void Draw(GameTime gameTime) {
			if (!Visible) {
				return;
			}
			SpriteBatch.StartDraw();
			GameObjects.DrawableEach(gameTime);
			SpriteBatch.EndDraw();
		}
		public event SilentPropertyChangedEventHandler PropertyChanged;
		public void SetSilentValue(string propertyName, object value) {
			if (propertyName == nameof(Enabled)) {
				_enabled = (bool)value;
			}
		}
		protected virtual void OnPropertyChanged(object value, [CallerMemberName] string propertyName = null, bool isSilent = false) {
			PropertyChanged?.Invoke(this, new SilentPropertyChangedEventArgs(propertyName, value, isSilent));
		}
	}
}