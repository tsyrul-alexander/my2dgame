using My2DGame.Core.Scene;
using My2DGame.Network.Client.Utilities;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Tracker {
	public class SceneTracker: BaseTracker<IScene> {
		public SceneTracker(IScene value) : base(value) { }
		public override void UpdateProperty(PropertyValue propertyValue) {
			if (Value.SetDrawable(propertyValue) || Value.SetUpdateable(propertyValue)) {
				return;
			}
		}
		protected override PropertyValue GetPropertyValue(string columnName) {
			throw new System.NotImplementedException();
		}
	}
}