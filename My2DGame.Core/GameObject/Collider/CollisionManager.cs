using System.Collections.Generic;

namespace My2DGame.Core.GameObject.Collider {
	public class CollisionManager : ICollisionManager {
		private readonly List<ICollisionItem> _items = new List<ICollisionItem>();
		public virtual void Add(ICollisionItem item) {
			item.CollisionItemChanged += ItemOnCollisionItemChanged;
			_items.Add(item);
		}
		public virtual void Remove(ICollisionItem item) {
			item.CollisionItemChanged -= ItemOnCollisionItemChanged;
			_items.Remove(item);
		}
		protected virtual void ItemOnCollisionItemChanged(ICollisionItem collisionItem) {
			Collide(collisionItem);
		}
		protected virtual void Collide(ICollisionItem collisionItem) {
			foreach (var sybCollisionItem in _items) {
				if (collisionItem == sybCollisionItem) {
					continue;
				}
				if (IsCollide(collisionItem, sybCollisionItem)) {
					OnCollision(collisionItem, sybCollisionItem);
				}
			}
		}
		protected virtual void OnCollision(ICollisionItem collisionItem, ICollisionItem sybCollisionItem) {
			collisionItem.OnCollision(sybCollisionItem);
			sybCollisionItem.OnCollision(collisionItem);
		}
		protected virtual bool IsCollide(ICollisionItem item1, ICollisionItem item2) {//todo optimize
			var topEdge1 = item1.Y + item1.Height;
			var rightEdge1 = item1.X + item1.Width;
			var leftEdge1 = item1.X;
			var bottomEdge1 = item1.Y;
			var topEdge2 = item2.Y + item2.Height;
			var rightEdge2 = item2.X + item2.Width;
			var leftEdge2 = item2.X;
			var bottomEdge2 = item2.Y;
			return leftEdge1 < rightEdge2 && rightEdge1 > leftEdge2 && bottomEdge1 < topEdge2 && topEdge1 > bottomEdge2;
		}
	}
}