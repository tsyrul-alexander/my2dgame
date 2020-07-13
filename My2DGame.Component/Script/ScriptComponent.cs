using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Component.Script {
	public class ScriptComponent : BaseGameObjectComponent {
		private readonly Action<IGameObject, GameTime> _action;
		public ScriptComponent(Action<IGameObject, GameTime> action) {
			_action = action;
		}
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
			_action.Invoke(GameObject, gameTime);
		}
		public override IProperty[] GetProperties() {
			return new IProperty[0];
		}
	}
}
