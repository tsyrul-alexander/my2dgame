using System;
using Microsoft.Extensions.DependencyInjection;
using My2DGame.Content.Utilities;
using My2DGame.Core.GameObject;
using My2DGame.Core.Scene;
using My2DGame.Network.Client.Synchronizer;
using Newtonsoft.Json.Linq;

namespace My2DGame.Content.Manager.Json
{
	public class JsonSceneContentManager: BaseContentManager<IScene> {
		protected virtual IGameSynchronizer GameSynchronizer { get; }
		protected virtual IContentManager<IGameObject> GameObjectContentManager { get; }
		protected virtual IServiceProvider ServiceProvider { get; }
		private const string GameObjectPropertyName = "game_objects";
		public JsonSceneContentManager(IGameSynchronizer gameSynchronizer, IContentManager<IGameObject> gameObjectContentManager, IServiceProvider serviceProvider) {
			GameSynchronizer = gameSynchronizer;
			GameObjectContentManager = gameObjectContentManager;
			ServiceProvider = serviceProvider;
		}
		public override IScene Load(string content) {
			var sceneObject = JObject.Parse(content);
			var scene = CreateScene();
			SetSceneParameters(scene, sceneObject);
			return scene;
		}
		public override string Save(IScene item) {
			var sceneObject = new JObject {
				{ "name", item.Name },
				{ GameObjectPropertyName, ToJArray(GameObjectContentManager, item.GameObjects) }
			};
			return sceneObject.ToString();
		}
		protected virtual void SetSceneParameters(IScene scene, JObject jObject) {
			scene.Name = jObject.GetValue("name").Value<string>();
			scene.JPropertyToEnabled(jObject);
			scene.JPropertyToVisible(jObject);
			var gameObjectJArray = (JArray) jObject.GetValue(GameObjectPropertyName);
			var gameObjects = FromJArray(GameObjectContentManager, gameObjectJArray);
			foreach (var gameObject in gameObjects) {
				gameObject.Scene = scene;
				scene.GameObjects.Add(gameObject);
			}
		}
		protected virtual IScene CreateScene() {
			return ServiceProvider.GetService<IScene>();
		}
	}
}
