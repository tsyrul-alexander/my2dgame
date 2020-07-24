using My2DGame.Core.Scene;
using My2DGame.Network.Client.Utilities;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Tracker {
	public class SceneTracker: BaseTracker<IScene> {
		public SceneTracker(IScene value) : base(value) { }
	}
}