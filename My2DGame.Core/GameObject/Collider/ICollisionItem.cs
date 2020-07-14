namespace My2DGame.Core.GameObject.Collider {
	public interface ICollisionItem {
		int X { get; }
		int Y { get; }
		int Width { get; }
		int Height { get; }
		void OnCollision(ICollisionItem collisionItem);
	}
}