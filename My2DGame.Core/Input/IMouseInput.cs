using Microsoft.Xna.Framework;

namespace My2DGame.Core.Input {
	public interface IMouseInput {
		Vector2 MouseLocation { get; set; }
		bool MousePressed { get; set; }
		bool MouseReleased { get; set; }
	}
}
