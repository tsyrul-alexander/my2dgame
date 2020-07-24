using System;
using Microsoft.Xna.Framework;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Component.Script {
	public class ScriptComponent : BaseGameObjectComponent {
		public StringProperty ActionProperty { get; }
		private IScriptAction _action;
		public ScriptComponent(string actionTypeName): this(new StringProperty(actionTypeName)) {}
		public ScriptComponent(StringProperty actionProperty) {
			ActionProperty = actionProperty;
		}
		public override void Initialize() {
			base.Initialize();
			_action = (IScriptAction)GameObject.Scene.ServiceProvider.GetService(Type.GetType(ActionProperty.Value));
			_action.Initialize(GameObject);
		}
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			_action.Update(gameTime);
		}
		public IScriptAction GetScriptAction() {
			return _action;
		}
	}
}
