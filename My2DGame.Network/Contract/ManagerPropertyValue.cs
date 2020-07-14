using System;

namespace My2DGame.Network.Contract {
	[Serializable]
	public class ManagerPropertyValue: INetworkObject {
		public Guid Id { get; set; }
		public string ManagerName { get; set; }
		public PropertyValue Value { get; set; }
		public Guid RequestItemId => Id;
	}
}
