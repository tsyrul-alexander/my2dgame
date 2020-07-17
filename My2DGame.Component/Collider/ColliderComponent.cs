using System;
using My2DGame.Core;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.GameObject.Collider;
using My2DGame.Core.Property;

namespace My2DGame.Component.Collider {
	public class ColliderComponent : BaseGameObjectComponent, ICollisionItem {
		public event Action<ColliderComponent, ICollisionItem> Collision;
		public  event Action<ICollisionItem> CollisionItemChanged;
		public IntegerProperty XProperty { get; }
		public IntegerProperty YProperty { get; }
		public IntegerProperty WidthProperty { get; }
		public IntegerProperty HeightProperty { get; }
		public ColliderComponent(int x, int y, int width, int height) : this(new IntegerProperty(x),
			new IntegerProperty(y), new IntegerProperty(width), new IntegerProperty(height)) { }
		public ColliderComponent(IntegerProperty xProperty, IntegerProperty yProperty, IntegerProperty widthProperty, IntegerProperty heightProperty) {
			XProperty = xProperty;
			YProperty = yProperty;
			WidthProperty = widthProperty;
			HeightProperty = heightProperty;
		}
		public override void Initialize() {
			base.Initialize();
			GameObject.Scene.CollisionManager.Add(this);
			XProperty.PropertyChanged += PropertyOnPropertyChanged;
			YProperty.PropertyChanged += PropertyOnPropertyChanged;
			WidthProperty.PropertyChanged += PropertyOnPropertyChanged; 
			HeightProperty.PropertyChanged += PropertyOnPropertyChanged;
			GameObject.PropertyChanged += GameObjectOnPropertyChanged;
		}
		private void GameObjectOnPropertyChanged(object sender, SilentPropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(IGameObject.Position)) {
				OnCollisionItemChanged();
			}
		}
		private void PropertyOnPropertyChanged(object sender, SilentPropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(IProperty<object>.Value)) {
				OnCollisionItemChanged();
			}
		}
		public override IProperty[] GetProperties() {
			return new IProperty[] {
				XProperty, YProperty, WidthProperty, HeightProperty
			};
		}
		public virtual void OnCollision(ICollisionItem collisionItem) {
			Collision?.Invoke(this, collisionItem);
		}
		public int X => (int)(XProperty.Value + GameObject.Position.X);
		public int Y => (int)(YProperty.Value + GameObject.Position.Y);
		public int Width => WidthProperty.Value;
		public int Height => HeightProperty.Value;
		protected virtual void OnCollisionItemChanged() {
			CollisionItemChanged?.Invoke(this);
		}
	}
}