using System;

namespace My2DGame.Core.GameObject.Collider {
	public interface ICollisionItem {
		event Action<ICollisionItem> CollisionItemChanged;
		int X { get; }
		int Y { get; }
		int Width { get; }
		int Height { get; }
		void OnCollision(ICollisionItem collisionItem);
	}
}