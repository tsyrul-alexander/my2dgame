using My2DGame.Core.Component.GameObject;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Tracker {
	public class GameObjectComponentTracker : BaseTracker<IGameObjectComponent> {
		public GameObjectComponentTracker(IGameObjectComponent value) : base(value) { }
	}
}