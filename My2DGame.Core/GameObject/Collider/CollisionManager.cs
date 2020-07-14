using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace My2DGame.Core.GameObject.Collider {
	public class CollisionManager : ICollisionManager {
		private readonly List<ICollisionItem> _items = new List<ICollisionItem>();
		public virtual void Add(ICollisionItem item) {
			_items.Add(item);
		}
		public bool Enabled { get; set; } = true;
		public virtual void Update(GameTime gameTime) {
			if (!Enabled) {
				return;
			}
			foreach (var collisionItem in _items) {
				foreach (var sybCollisionItem in _items) {
					if (collisionItem == sybCollisionItem) {
						continue;
					}
					if (IsCollide(collisionItem, sybCollisionItem)) {
						collisionItem.OnCollision(sybCollisionItem);
					}
				}
			}
		}
		public virtual bool IsCollide(ICollisionItem item1, ICollisionItem item2) {//todo optimize
			var topEdge1 = item1.Y + item1.Height;
			var rightEdge1 = item1.X + item1.Width;
			var leftEdge1 = item1.X;
			var bottomEdge1 = item1.Y;
			var topEdge2 = item2.Y + item2.Height;
			var rightEdge2 = item2.X + item2.Width;
			var leftEdge2 = item2.X;
			var bottomEdge2 = item2.Y;
			if (leftEdge1 < rightEdge2 && rightEdge1 > leftEdge2 && bottomEdge1 < topEdge2 && topEdge1 > bottomEdge2) {
				return true;
			}
			return false;
		}
	}
}