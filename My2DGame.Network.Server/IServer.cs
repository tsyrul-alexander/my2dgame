using System;
using My2DGame.Network.Server.Client;

namespace My2DGame.Network.Server
{
	public interface IServer {
		void Listen();
		void Disconnect();
		void BroadcastMessage(byte[] data, Guid senderId);
		void AddConnection(NetworkClient clientItem);
		void RemoveConnection(Guid id);
	}
}
