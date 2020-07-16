using System;
using System.Linq;
using My2DGame.Core.GameObject;
using My2DGame.Core.Scene;
using My2DGame.Network.Client.Synchronizer;
using Newtonsoft.Json.Linq;

namespace My2DGame.Content.Manager.Scene
{
	public class JsonSceneContentManager: BaseContentManager<IScene> {
		private readonly IGameSynchronizer _gameSynchronizer;
		private readonly IContentManager<IGameObject> _gameObjectContentManager;
		private const string GameObjectPropertyName = "game_objects";
		public JsonSceneContentManager(IGameSynchronizer gameSynchronizer, IContentManager<IGameObject> gameObjectContentManager) {
			_gameSynchronizer = gameSynchronizer;
			_gameObjectContentManager = gameObjectContentManager;
		}
		public override IScene Load(string content) {
			throw new NotImplementedException();
		}
		public override string Save(IScene item) {
			var sceneObject = new JObject {
				{ "name", item.Name },
				{ GameObjectPropertyName, new JArray(item.GameObjects.Select(o => _gameObjectContentManager.Save(o)).Cast<object>().ToArray()) }
			};
			return sceneObject.ToString();
		}
	}
}
