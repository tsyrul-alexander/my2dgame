namespace My2DGame.Core.GameObject.Collider
{
	public interface ICollisionManager {
		void Add(ICollisionItem item);
		void Remove(ICollisionItem item);
	}
}
