using System;
using System.Net;
using My2DGame.Network.Server.Client;

namespace My2DGame.Network.Server
{
	public interface IServer {
		void Listen(IPAddress address, int port);
		void Disconnect();
		void BroadcastMessage(byte[] data, Guid senderId);
		void AddConnection(NetworkClient clientItem);
		void RemoveConnection(Guid id);
	}
}
