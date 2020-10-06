using Microsoft.Xna.Framework;
using My2DGame.Core.Input;

namespace My2DGame.Game.EnglishStory {
	public class GameInput: IMouseInput {
		public Vector2 MouseLocation { get; set; }
		public bool MousePressed { get; set; }
		public bool MouseReleased { get; set; }
	}
}
