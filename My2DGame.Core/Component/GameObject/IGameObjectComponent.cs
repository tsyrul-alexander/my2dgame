using System;
using System.ComponentModel;
using My2DGame.Core.GameObject;

namespace My2DGame.Core.Component.GameObject {
	public interface IGameObjectComponent : INotifyPropertyChanged, IUpdateable, IDrawable, ICloneable {
		IGameObject GameObject { get; set; }
		void Initialize();
	}
}