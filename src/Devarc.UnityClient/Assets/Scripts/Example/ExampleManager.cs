using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Collections.Generic;
using Devarc;
using System.Text;
using UnityEngine.UI;

public class ExampleManager : MonoBehaviour
{
    public AuthNetwork authNetwork;
    public GameNetwork gameNetwork;

    public TMP_InputField authAddress;
    public TMP_InputField gameAddress;
    public TMP_InputField inputID;
    public TMP_InputField inputPW;
    public ScrollRect scrollRect;
    public TextMeshProUGUI logText;

    string sessionID = string.Empty;
    int secret = 0;
    StringBuilder mStrBuilder = new StringBuilder();
    List<string> mLogMessages = new List<string>();

    private void Start()
    {
        authAddress.text = "http://localhost:3000/msgpack";
        gameAddress.text = "ws://localhost:4000/Echo";

        gameNetwork.Init(gameAddress.text);

        gameNetwork.Socket.OnOpen += onConnected;
        gameNetwork.Socket.OnClose += (sender, evt) =>
        {
            Debug.LogFormat($"Diconnected: code={evt.Code}, reason={evt.Reason}");
        };
        gameNetwork.Socket.OnError += (sender, evt) =>
        {
            Debug.LogError(evt.Message);
            gameNetwork.DisConnect();
        };

        logText.text = string.Empty;
        Application.logMessageReceived += (log, stack, type) =>
        {
            switch (type)
            {
                case LogType.Error:
                    mLogMessages.Insert(0, $"<color=red>[{DateTime.Now.ToString("HH:mm:ss")}] {log}</color>");
                    break;
                case LogType.Warning:
                    mLogMessages.Insert(0, $"<color=yellow>[{DateTime.Now.ToString("HH:mm:ss")}] {log}</color>");
                    break;
                default:
                    mLogMessages.Insert(0, $"[{DateTime.Now.ToString("HH:mm:ss")}] {log}");
                    break;
            }
            
            if (mLogMessages.Count > 50)
            {
                mLogMessages.RemoveAt(0);
            }

            mStrBuilder.Clear();
            foreach (string msg in mLogMessages)
            {
                mStrBuilder.AppendLine(msg);
            }
            logText.text = mStrBuilder.ToString();
            scrollRect.verticalNormalizedPosition = 0f;
        };
    }


    public void OnClick_RequestLogin()
    {
        authNetwork.Init(authAddress.text, "packet");

        var request = new C2Auth.RequestLogin();
        request.accountID = inputID.text;
        request.password = EncryptUtil.Encrypt_MD5(inputPW.text);

        authNetwork.RequestLogin(request, (response, errorType, errorMsg) =>
        {
            switch (errorType)
            {
                case UnityWebRequest.Result.Success:
                    sessionID = response.sessionID;
                    secret = response.secret;
                    Debug.Log(JsonUtility.ToJson(response));
                    break;
                default:
                    Debug.LogError(errorMsg);
                    break;
            }
        });
    }

    public void OnClick_ConnectLogin()
    {
        if (gameNetwork.IsConnected)
            onConnected(null, null);
        else
            gameNetwork.Connect();
    }

    void onConnected(object sender, EventArgs e)
    {
        C2Game.RequestLogin request = new C2Game.RequestLogin();
        request.sessionID = sessionID;
        request.secret = secret;
        gameNetwork.SendData(request);
    }
}