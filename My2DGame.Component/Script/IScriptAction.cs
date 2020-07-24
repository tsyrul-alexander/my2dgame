using Microsoft.Xna.Framework;
using My2DGame.Core.GameObject;

namespace My2DGame.Component.Script {
	public interface IScriptAction {
		void Initialize(IGameObject gameObject);
		void Update(GameTime gameTime);
	}
}