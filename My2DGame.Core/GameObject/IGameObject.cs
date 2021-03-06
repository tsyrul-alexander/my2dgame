﻿using System;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;

namespace My2DGame.Core.GameObject {
	public interface IGameObject : ISprite, ISilentPropertyChanged, IUpdateable, IDrawable, ICloneable {
		IScene Scene { get; set; }
		GameObjectComponentCollection Components { get; }
		void Initialize();
	}
}
