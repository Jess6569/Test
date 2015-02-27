using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class TCPclient : MonoBehaviour 
{
	/// <summary>
	/// The alert box.
	/// </summary>
	public GameObject m_alertBox;
	GameObject m_alertInstance;
	TcpClient m_tcpclnt;
	Stream m_stream;

	string m_ip = "67.221.175.55";

	float m_timeToPing;

	void Awake()
	{
		//Register event for Destroying the alert Box
		Messenger.AddListener ("DestroyAlert",DestroyAlert);
	}

	void Start () 
	{
		m_alertInstance = Instantiate (m_alertBox) as GameObject;
		try 
		{
			//Create new TCP client and connect to the desired IP
			m_tcpclnt = new TcpClient();
			m_tcpclnt.Connect(m_ip,9999);
			m_stream = m_tcpclnt.GetStream();

		}
		catch (Exception e) 
		{
			print("Error..... " + e.StackTrace);
		}
	}

	void Update()
	{
		try 
		{
			//For each second ping the server for updated information
			if (Time.time > m_timeToPing && m_stream != null) 
			{
				m_timeToPing = Time.time + 2;
				m_stream.ReadTimeout = 1000;
				byte[] data = new Byte[256];
				int bytes = m_stream.Read (data, 0, 256);
				Messenger<string>.Broadcast ("UpdateServerDataReceived", System.Text.Encoding.ASCII.GetString (data, 0, bytes));
				m_stream.Dispose();
				m_stream.Close();
				m_tcpclnt.Close();
				m_tcpclnt = new TcpClient();
				m_tcpclnt.Connect(m_ip,9999);
				m_stream = m_tcpclnt.GetStream();
			}
		}
		catch (Exception e) 
		{
			Messenger<string>.Broadcast ("UpdateServerDataReceived", "...");
		}
	}

	void OnApplicationQuit() 
	{
		m_tcpclnt.Close();
	}

	public void DestroyAlert()
	{
		Destroy (m_alertInstance.gameObject);
	}
}
