using System;
using My2DGame.Core.Component.GameObject;

namespace My2DGame.Content.Manager.Component {
	public class JsonGameObjectComponentContentManager : BaseContentManager<IGameObjectComponent> {
		public override IGameObjectComponent Load(string content) {
			throw new NotImplementedException();
		}
		public override string Save(IGameObjectComponent item) {
			throw new NotImplementedException();
		}
	}
}