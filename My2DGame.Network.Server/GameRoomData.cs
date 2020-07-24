using System.Collections.Generic;

namespace My2DGame.Network.Server {
	public class GameRoomData {
		public Dictionary<QueryType, byte[]> QueryData { get; } = new Dictionary<QueryType, byte[]>();
	}
}
