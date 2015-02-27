using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class Message : MonoBehaviour 
{
	void Awake()
	{
		Messenger<string>.AddListener ("UpdateServerDataReceived", SetMessage);
	}

	void SetMessage (string msg)
	{
		this.GetComponent<Text> ().text = msg;
	}

	public void DestroyAlert()
	{
		Messenger.Broadcast ("DestroyAlert");
	}

	public void LoadData()
	{
		TextAsset txt = (TextAsset)Resources.Load("Dummy", typeof(TextAsset));
		this.GetComponent<Text> ().text = txt.text;
	}

}
