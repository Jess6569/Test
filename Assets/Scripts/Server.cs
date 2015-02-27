using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class Server : MonoBehaviour {
	private bool mRunning;
	private bool send;

	public static string msg = "";
	
	public Thread mThread;
	public TcpListener tcp_Listener = null;
	
	void Awake() 
	{
		mRunning = true;
		ThreadStart ts = new ThreadStart(Receive);
		mThread = new Thread(ts);
		mThread.Start();
	}
	
	public void stopListening() 
	{
		mRunning = false;
	}
	
	void Receive() 
	{
		tcp_Listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
		tcp_Listener.Start();
		print("Server Start");
		while (mRunning)
		{
			// check if new connections are pending, if not, be nice and sleep 100ms
			if (!tcp_Listener.Pending())
			{
				Thread.Sleep(100);
			}
			else
			{
				if(send)
				{
					Socket sckt = tcp_Listener.AcceptSocket();
					ASCIIEncoding asen=new ASCIIEncoding();
					sckt.Send(asen.GetBytes("The string was recieved by the server."));
					Thread.Sleep(1000);
					sckt.Close();
					send = false;
				}
			}
		}
	}

	public void SendMessageToClient()
	{
		send = true;
	}

	void OnApplicationQuit() { // stop listening thread
		stopListening(); // wait fpr listening thread to terminate (max. 500ms)
			mThread.Join(500);
	}
}