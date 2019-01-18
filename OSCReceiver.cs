using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System.Linq;

[AddComponentMenu("Scripts/OSCReceiver")]
public class OSCReceiver : MonoBehaviour
{
    public string remoteIp = "127.0.0.1";
    public int sendToPort = 6448;
    public int listenerPort = 12000;
    public GameObject controller; 
    public GameObject spotOSC;

    private float xRot = 0;
    private float yRot = 0;
    private float zRot = 0;

    private Osc oscHandler;
    
    public GameObject messageCanvas;
    public Text messageText;
    
    private string message;
    private float lightLevel = 8;
    
    

    ~OSCReceiver()
    {
        if (oscHandler != null)
        {            
            oscHandler.Cancel();
        }

        // speed up finalization
        oscHandler = null;
        System.GC.Collect();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(messageText){
            messageText.text = message;
        }
        spotOSC.GetComponent<Light>().intensity = lightLevel;
    	spotOSC.transform.localEulerAngles = new Vector3(xRot, yRot, zRot);
    
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
       
    }

    void OnDisable()
    {
        // close OSC UDP socket
        Debug.Log("closing OSC UDP socket in OnDisable");
        oscHandler.Cancel();
        oscHandler = null;
    }

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {

        UDPPacketIO udp = GetComponent<UDPPacketIO>();
        udp.init(remoteIp, sendToPort, listenerPort);
        
	    oscHandler = GetComponent<Osc>();
        oscHandler.init(udp);

        oscHandler.SetAddressHandler("/remoteIP", setRemoteIP);        
        oscHandler.SetAddressHandler("/wek/outputs", Example);
        oscHandler.SetAddressHandler("/find", Find);
        oscHandler.SetAddressHandler("/text", textFromOSC);
        oscHandler.SetAddressHandler("/light/direction", lightFromOSC);
        oscHandler.SetAddressHandler("/light/intensity", lightFromOSC);

        if (messageCanvas == null) {
            messageCanvas = transform.Find("OscMessageCanvas").gameObject;
            if (messageCanvas != null) {
                messageText = messageCanvas.transform.Find("MessageText").GetComponent<Text>();
            }
        }
        
        if (spotOSC == null) {
            spotOSC = transform.Find("SpotLight").gameObject;
            
        }
                 
        string localIP;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }
        setText("This IP is: " + localIP);
        
    }
    
    void setText(string str){
        message = str;
    }

    public void setRemoteIP(OscMessage m) {
        Debug.Log("Called light from OSC >> " + Osc.OscMessageToString(m));
    }

    public void lightFromOSC(OscMessage m) {
        
        Debug.Log("Called light from OSC >> " + Osc.OscMessageToString(m));
        //setText((string) m.Values[0]);
        string[] addressParts = m.Address.Split('/');
        Debug.Log("   Address Last Parts: " + addressParts[addressParts.Length-1]);
        if(addressParts[addressParts.Length-1] == "intensity") {
            lightLevel = (float) (((int) m.Values[0]) * 0.01);
        }
        if (addressParts[addressParts.Length-1] == "direction") {
            xRot = (float) (((int) m.Values[0]) * 0.01);
            yRot = (float) (((int) m.Values[1]) * 0.01);
            zRot = (float) (((int) m.Values[2]) * 0.01);
        }
    }
    
    public void textFromOSC(OscMessage m)
    {
        Debug.Log("Called text from OSC > " + Osc.OscMessageToString(m));
        setText((string) m.Values[0]);
    }

    public void Example(OscMessage m)
    {
    	Debug.Log("Called Example One > " + Osc.OscMessageToString(m));
	    Debug.Log("Message Values > " + m.Values[0] + " " + m.Values[1]);
	    yRot = (float) m.Values[0];
	    zRot = (float) m.Values[1];    
    }

    public void Find(OscMessage m)
    {
        Debug.Log("Finding > " + m.Address);
        //Find()
    	Debug.Log("Called Example One > " + Osc.OscMessageToString(m));
    }

}