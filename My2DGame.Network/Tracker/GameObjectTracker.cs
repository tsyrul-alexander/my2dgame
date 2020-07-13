using My2DGame.Core.GameObject;
using My2DGame.Network.Utilities;

namespace My2DGame.Network.Tracker {
	public class GameObjectTracker : BaseTracker<IGameObject> {
		public GameObjectTracker(IGameObject value) : base(value) { }
		public override void Initialize() {
			base.Initialize();

		}
		public override void UpdateProperty(PropertyValue propertyValue) {
			if (Value.SetDrawable(propertyValue) || Value.SetUpdateable(propertyValue)) {
				return;
			} else if (propertyValue.Name == nameof(IGameObject.Color)) {
				Value.Color = propertyValue.GetColor();
			}
		}
	}
}