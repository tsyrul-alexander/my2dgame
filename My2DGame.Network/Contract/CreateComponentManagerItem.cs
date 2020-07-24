using System;

namespace My2DGame.Network.Contract {
	[Serializable]
	public class CreateComponentManagerItem : CreateManagerItem {
		public string ComponentName { get; set; }
	}
}
