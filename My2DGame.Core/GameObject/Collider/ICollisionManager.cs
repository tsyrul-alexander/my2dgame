namespace My2DGame.Core.GameObject.Collider
{
	public interface ICollisionManager: IUpdateable {
		void Add(ICollisionItem item);
	}
}
