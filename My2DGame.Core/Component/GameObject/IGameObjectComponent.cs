using System;
using System.ComponentModel;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;

namespace My2DGame.Core.Component.GameObject {
	public interface IGameObjectComponent : ISilentPropertyChanged, IUpdateable, IDrawable, ICloneable {
		IGameObject GameObject { get; set; }
		void Initialize();
		IProperty[] GetProperties();
	}
}