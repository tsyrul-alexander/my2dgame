using System;
using System.Collections.Generic;

namespace My2DGame.Network.Server {
	public class GameRoom {
		private Dictionary<Guid, byte[]> Data { get; } = new Dictionary<Guid, byte[]>();
		private List<Guid> Users { get; } = new List<Guid>();
		public bool GetIfExistsUsers() {
			return Users.Count != 0;
		}
		public void ConnectUser(Guid userId) {
			Users.Add(userId);
		}
		public void DisconnectUser(Guid userId) {
			Users.Remove(userId);
		}
		public void Save(Guid itemId, byte[] data) {
			Data[itemId] = data;
		}
		public Dictionary<Guid, byte[]> GetData() {
			return Data;
		}
	}
}