using System;
using System.ComponentModel;

namespace My2DGame.Core.Component.GameObject {
	public interface IGameObjectComponent : INotifyPropertyChanged, IUpdateable, IDrawable, ICloneable { }
}