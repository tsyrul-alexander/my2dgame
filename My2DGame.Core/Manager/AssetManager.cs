using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace My2DGame.Core.Manager {
	public class AssetManager : IAssetManager {
		public string AssetFolderPath { get; }
		private readonly IFileManager _fileManager;
		private const string TextureFolder = "Texture";
		private readonly ContentManager _contentManager;
		public AssetManager(IFileManager fileManager, IServiceProvider serviceProvider, string assetFolderPath) {
			AssetFolderPath = assetFolderPath;
			_fileManager = fileManager;
			_contentManager = new ContentManager(serviceProvider);
		}
		public virtual Texture2D LoadTexture(string textureName) {
			var texturePath = GetTexturePath(textureName);
			return _contentManager.Load<Texture2D>(texturePath);
		}
		protected virtual string GetTexturePath(string textureName) {
			return _fileManager.CombinePath(GetTextureFolder(), textureName);
		}
		protected virtual string GetTextureFolder() {
			return _fileManager.CombinePath(AssetFolderPath, TextureFolder);
		}
	}
}
