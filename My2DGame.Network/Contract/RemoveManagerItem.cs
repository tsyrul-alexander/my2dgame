using System;

namespace My2DGame.Network.Contract {
	[Serializable]
	public class RemoveManagerItem : INetworkObject {
		public Guid Id { get; set; }
		public Guid RequestItemId => Id;
	}
}