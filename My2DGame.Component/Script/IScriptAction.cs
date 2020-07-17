using Microsoft.Xna.Framework;
using My2DGame.Core.GameObject;

namespace My2DGame.Component.Script {
	public interface IScriptAction {
		IGameObject GameObject { get; set; }
		void Update(GameTime gameTime);
	}
}