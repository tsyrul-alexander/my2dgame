using Microsoft.Xna.Framework;
using My2DGame.Core.GameObject;

namespace My2DGame.Component.Script {
	public abstract class BaseScriptAction : IScriptAction {
		public IGameObject GameObject { get; set; }
		public virtual void Update(GameTime gameTime) { }
	}
}
