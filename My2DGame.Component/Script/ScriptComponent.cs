using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Utilities;

namespace My2DGame.Component.Script {
	public class ScriptComponent : BaseGameObjectComponent {
		public string[] Actions { get; set; }
		private readonly IList<IScriptAction> _actions;
		public ScriptComponent(params string[] actionTypeNames) {
			Actions = actionTypeNames;
			_actions = new List<IScriptAction>();
		}
		public override void Initialize() {
			base.Initialize();
			foreach (var action in Actions) {
				var scriptAction = (IScriptAction) GameObject.Scene.ServiceProvider.GetService(Type.GetType(action));
				_actions.Add(scriptAction);
				scriptAction.Initialize(GameObject);
			}
		}
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			_actions.ForEach(action => action.Update(gameTime));
		}
		public T GetScriptAction<T>() where T : IScriptAction {
			return (T)_actions.First(action => action is T);
		}
	}
}
