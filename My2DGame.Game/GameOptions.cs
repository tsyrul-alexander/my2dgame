using System;

namespace My2DGame.Core {
	public class GameOptions {
		public string ContentFolderPath { get; set; }
		public string ServerIpAddress { get; set; }
		public int ServerPort { get; set; }
		public Guid NetworkRoomId { get; set; }
	}
}