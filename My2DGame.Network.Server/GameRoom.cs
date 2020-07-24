using System;
using System.Collections.Generic;
using System.Linq;

namespace My2DGame.Network.Server {
	public class GameRoom {
		private IDictionary<Guid, GameRoomData> Data { get; } = new Dictionary<Guid, GameRoomData>();
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
		public void Save(Guid itemId, byte[] data, QueryType queryType) {
			var roomData = GetRoomData(itemId);
			if (roomData == null) {
				roomData = new GameRoomData();
				Data[itemId] = roomData;
			}
			roomData.QueryData[queryType] = data;
		}
		public IEnumerable<byte[]> GetData() {
			var data = GetQueryTypeData(QueryType.Create);
			data = data.Concat(GetQueryTypeData(QueryType.Update));
			return data.Concat(GetQueryTypeData(QueryType.Remove));
		}
		protected virtual IEnumerable<byte[]> GetQueryTypeData(QueryType queryType) {
			return Data.Where(pair => pair.Value.QueryData.ContainsKey(queryType))
			.Select(pair => pair.Value.QueryData[queryType]);
		}
		public GameRoomData GetRoomData(Guid itemId) {
			if (Data.ContainsKey(itemId)) {
				return Data[itemId];
			}
			return null;
		}
	}
}