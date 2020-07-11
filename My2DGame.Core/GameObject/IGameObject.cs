using System;
using System.ComponentModel;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Scene;

namespace My2DGame.Core.GameObject {
	public interface IGameObject : INotifyPropertyChanged, IUpdateable, IDrawable, ICloneable {
		IScene Scene { get; }
		GameObjectComponentCollection Components { get; }
		void Initialize();
	}
}
