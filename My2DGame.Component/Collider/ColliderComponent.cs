using System;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject.Collider;
using My2DGame.Core.Property;

namespace My2DGame.Component.Collider {
	public class ColliderComponent : BaseGameObjectComponent, ICollisionItem {
		public event Action<ColliderComponent, ICollisionItem> Collision;
		public IntegerProperty XProperty { get; }
		public IntegerProperty YProperty { get; }
		public IntegerProperty WidthProperty { get; }
		public IntegerProperty HeightProperty { get; }
		public ColliderComponent(int x, int y, int width, int height) {
			XProperty = new IntegerProperty(x);
			YProperty = new IntegerProperty(y);
			WidthProperty = new IntegerProperty(width);
			HeightProperty = new IntegerProperty(height);
		}
		public override void Initialize() {
			base.Initialize();
			GameObject.Scene.CollisionManager.Add(this);
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
	}
}