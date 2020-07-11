using System;
using System.ComponentModel;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;

namespace My2DGame.Core.GameObject {
	public interface IGameObject : ISprite, INotifyPropertyChanged, IUpdateable, IDrawable, ICloneable {
		IScene Scene { get; }
		GameObjectComponentCollection Components { get; }
		void Initialize();
	}
}
