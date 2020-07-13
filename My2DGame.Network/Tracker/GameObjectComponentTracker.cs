using My2DGame.Core.Component.GameObject;

namespace My2DGame.Network.Tracker {
	public class GameObjectComponentTracker : BaseTracker<IGameObjectComponent> {
		public GameObjectComponentTracker(IGameObjectComponent value) : base(value) { }
		public override void UpdateProperty(PropertyValue propertyValue) { }
	}
}