using My2DGame.Core.GameObject;
using My2DGame.Network.Client.Utilities;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Tracker {
	public class GameObjectTracker : BaseTracker<IGameObject> {
		public GameObjectTracker(IGameObject value) : base(value) { }
	}
}