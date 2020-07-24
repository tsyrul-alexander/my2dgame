using System;

namespace My2DGame.Network.Contract {
	[Serializable]
	public class CreateManagerItem : INetworkObject {
		public Guid Id { get; set; }
		public Guid? ParentItemId { get; set; }
		public string ManagerName { get; set; }
		public Guid RequestItemId => Id;
		public PropertyValue[] Values { get; set; }
	}
}