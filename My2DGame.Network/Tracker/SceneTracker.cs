using My2DGame.Core.Scene;
using My2DGame.Network.Utilities;

namespace My2DGame.Network.Tracker {
	public class SceneTracker: BaseTracker<IScene> {
		public SceneTracker(IScene value) : base(value) { }
		public override void UpdateProperty(PropertyValue propertyValue) {
			if (Value.SetDrawable(propertyValue) || Value.SetUpdateable(propertyValue)) {
				return;
			}
		}
	}
}