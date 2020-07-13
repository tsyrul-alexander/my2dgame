using My2DGame.Core.GameObject;
using My2DGame.Network.Client.Contract;
using My2DGame.Network.Client.Utilities;

namespace My2DGame.Network.Client.Tracker {
	public class GameObjectTracker : BaseTracker<IGameObject> {
		public GameObjectTracker(IGameObject value) : base(value) { }
		public override void UpdateProperty(PropertyValue propertyValue) {
			if (Value.SetDrawable(propertyValue) || Value.SetUpdateable(propertyValue)) {
				return;
			}
		}
		protected override PropertyValue GetPropertyValue(string columnName) {
			if (columnName == nameof(IGameObject.Enabled)) {
				return new PropertyValue(columnName, Value.Enabled);
			}
			return null;
		}
	}
}