using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Devarc;
using System.Text;
using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ExampleScene : BaseScene
{
    public CHARACTER_ID charID;
    public SKILL_ID skillID;
    public SOUND_ID soundID;
    public EFFECT_ID effectID;
    public STRING_ID stringID;

    public TMP_Dropdown domains;
    public TMP_InputField inputID;
    public TMP_InputField inputPW;
    public ScrollRect scrollRect;
    public TextMeshProUGUI logText;

    StringBuilder mStrBuilder = new StringBuilder();
    List<string> mLogMessages = new List<string>();


    protected override void onAwake()
    {
        if (!AppManager.IsCreated())
        {
            AppManager.Create("AppManager");
        }
        Debug.Log("ExampleScene::onAwake");
    }


    public override IEnumerator OnEnterScene()
    {
        Debug.Log("ExampleScene::OnEnterScene");

        Application.logMessageReceived += LogCallback;
        logText.text = string.Empty;
        logText.OnPreRenderText += (info) =>
        {
            scrollRect.normalizedPosition = new Vector2(0, 0);
        };

        AppManager.authNetwork.InitConnection(domains.captionText.text, 3000);

        AppManager.Instance.LoadResources(SystemLanguage.Korean);
        yield return AppManager.Instance.LoadBundles(SystemLanguage.Korean);
    }


    public override void OnLeaveScene()
    {
        AppManager.Instance.UnloadResources();
        AppManager.Instance.UnloadBundles();

        Application.logMessageReceived -= LogCallback;
    }


    void LogCallback(string log, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Error:
                mLogMessages.Add($"<color=red>[{DateTime.Now.ToString("HH:mm:ss")}] {log}</color>");
                break;
            case LogType.Warning:
                mLogMessages.Add($"<color=yellow>[{DateTime.Now.ToString("HH:mm:ss")}] {log}</color>");
                break;
            default:
                mLogMessages.Add($"[{DateTime.Now.ToString("HH:mm:ss")}] {log}");
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
        LayoutRebuilder.ForceRebuildLayoutImmediate(logText.GetComponent<RectTransform>());
    }



    public void OnClick_Clear()
    {
        mStrBuilder.Clear();
        mLogMessages.Clear();
        logText.text = string.Empty;
        LayoutRebuilder.ForceRebuildLayoutImmediate(logText.GetComponent<RectTransform>());
    }


    public void OnClick_RequestLogin()
    {
        var request = new C2Auth.RequestLogin();
        request.accountID = inputID.text;
        request.password = EncryptUtil.Encrypt_Base64(inputPW.text);

        AppManager.authNetwork.InitConnection(domains.captionText.text, 3000);
        AppManager.authNetwork.RequestLogin(request, (response, errorType, errorMsg) =>
        {
            switch (errorType)
            {
                case UnityWebRequest.Result.Success:
                    AppManager.Instance.sessionID = response.sessionID;
                    AppManager.Instance.secret = response.secret;
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
        if (AppManager.gameNetwork.IsConnected)
        {
            Debug.LogWarning("Already connected.");
        }
        else
        {
            AppManager.gameNetwork.Connect(domains.captionText.text, 4000);
        }
    }


    public void OnClick_Test1()
    {
        AppManager.gameNetwork.DisConnect();
    }


    public void OnClick_Test2()
    {
        SoundManager.Instance.PlaySound(CHANNEL.EFFECT, soundID);
        StartCoroutine(download());
    }


    IEnumerator download()
    {
        long totalSize = 0;
        Dictionary<string, long> patchList = null;
        yield return DownloadManager.Instance.GetPatchList((_size, _list) =>
        {
            totalSize = _size;
            patchList = _list;
        });

        Debug.LogFormat("Start to download contents: {0:N0} kb", (float)totalSize / 1000f);
        bool success = false;
        yield return DownloadManager.Instance.Download(patchList, (result, process) =>
        {
            success = (result == AsyncOperationStatus.Succeeded);
        });

        Debug.Log($"Download completed: success={success}");
        if (success)
        {
            yield return AssetManager.Instance.LoadBundleAssets<GameObject>("effect");
            SceneTransManager.Instance.LoadScene("TestScene");
        }
    }
}
