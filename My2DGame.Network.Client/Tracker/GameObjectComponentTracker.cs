using My2DGame.Core.Component.GameObject;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Tracker {
	public class GameObjectComponentTracker : BaseTracker<IGameObjectComponent> {
		public GameObjectComponentTracker(IGameObjectComponent value) : base(value) { }
		public override void UpdateProperty(PropertyValue propertyValue) { }
		protected override PropertyValue GetPropertyValue(string columnName) {
			if (nameof(IGameObjectComponent.Enabled) == columnName) {
				return new PropertyValue(columnName, Value.Enabled);
			}
			return null;
		}
	}
}