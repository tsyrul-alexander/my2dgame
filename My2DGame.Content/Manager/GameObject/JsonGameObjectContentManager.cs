using System;
using My2DGame.Core.GameObject;
using Newtonsoft.Json.Linq;

namespace My2DGame.Content.Manager.GameObject {
	public class JsonGameObjectContentManager : BaseContentManager<IGameObject> {
		public override IGameObject Load(string content) {
			throw new NotImplementedException();
		}
		public override string Save(IGameObject item) {
			return new JObject {
				{"PositionX", item.Position.X},
				{"PositionY", item.Position.Y}
			}.ToString();
		}
	}
}
