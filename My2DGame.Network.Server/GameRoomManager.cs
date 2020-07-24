using System;
using System.Collections.Generic;

namespace My2DGame.Network.Server {
	public static class GameRoomManager {
		private static Dictionary<Guid, GameRoom> Rooms { get; } = new Dictionary<Guid, GameRoom>();
		public static void ConnectUser(Guid roomId, Guid userId) {
			Rooms[roomId].ConnectUser(userId);
		}
		public static IEnumerable<byte[]> GetData(Guid roomId) {
			return Rooms[roomId].GetData();
		}
		public static void DisconnectUser(Guid roomId, Guid userId) {
			Rooms[roomId].DisconnectUser(userId);
			if (!Rooms[roomId].GetIfExistsUsers()) {
				Rooms.Remove(roomId);
			}
		}
		public static void Save(Guid roomId, Guid itemId, byte[] data, QueryType queryType) {
			if (!Rooms.ContainsKey(roomId)) {
				Rooms[roomId] = new GameRoom();
			}
			Rooms[roomId].Save(itemId, data, queryType);
		}
		public static bool GetIfExistsRoom(Guid roomId) {
			return Rooms.ContainsKey(roomId);
		}
	}
}