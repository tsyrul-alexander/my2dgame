using Microsoft.Xna.Framework.Graphics;

namespace My2DGame.Core.Manager
{
	public interface IAssetManager {
		Texture2D LoadTexture(string assetName);
	}
}
