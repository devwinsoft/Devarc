﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devarc;

public enum CHANNEL
{
    BGM,
    EFFECT,
    UI,
    MAX,
}

[System.Serializable]
public class SoundData
{
    public SOUND_ID soundID;
    public float waitTime;
    public float fadeTime;
}

public class SoundManager : MonoSingleton<SoundManager>
{
    public SoundChannel[] Channels => mChannels;
    SoundChannel[] mChannels = new SoundChannel[(int)CHANNEL.MAX];

    Dictionary<string, List<SOUND>> mSoundDatas = new Dictionary<string, List<SOUND>>();
    Dictionary<string, AudioClip> mClips = new Dictionary<string, AudioClip>();

    protected override void onAwake()
    {
        createChannel(CHANNEL.BGM, 2);
        createChannel(CHANNEL.EFFECT, 10);
        createChannel(CHANNEL.UI, 10);
    }

    protected override void onDestroy()
    {
    }


    public IEnumerator LoadBundles(string tableFileName, string addressKey)
    {
        var textAsset = AssetManager.Instance.GetAsset<TextAsset>(tableFileName);
        if (textAsset != null)
        {
            Table.SOUND.LoadJson(textAsset.text, (data) =>
            {
                register(data);
            });
        }

        // Load AudioClips...
        yield return AssetManager.Instance.LoadBundle_Assets<AudioClip>(addressKey);
    }

    public void LoadResources(string tableFileName)
    {
        var textAsset = AssetManager.Instance.GetAsset<TextAsset>(tableFileName);
        if (textAsset != null)
        {
            Table.SOUND.LoadJson(textAsset.text, (data) =>
            {
                register(data);

                // Load AudioClips...
                AssetManager.Instance.LoadResource_Asset<AudioClip>(data.path);
            });
        }
    }

    void register(SOUND data)
    {
        List<SOUND> list;
        if (mSoundDatas.TryGetValue(data.sound_id, out list) == false)
        {
            list = new List<SOUND>();
            mSoundDatas.Add(data.sound_id, list);
        }
        list.Add(data);
    }


    public bool IsPlaying(CHANNEL channel)
    {
        return mChannels[(int)channel].IsPlaying;
    }

    public int PlaySound(CHANNEL channel, string soundID)
    {
        return PlaySound(channel, soundID, 0, 0f);
    }

    public int PlaySound(CHANNEL channel, string soundID, int groupID, float fadeIn)
    {
        List<SOUND> list = null;
        if (mSoundDatas.TryGetValue(soundID, out list) == false || list.Count == 0)
        {
            Debug.LogError($"[SoundManager] Cannot find sound_id: {soundID}");
            return 0;
        }

        var soundData = list[Random.Range(0, list.Count - 1)];
        var clip = AssetManager.Instance.GetAudioClip(soundData.path);
        if (clip == null)
        {
            Debug.LogError($"[SoundManager] Cannot find audio clip: sound_id={soundID}, path={soundData.path}");
            return 0;
        }
        var chennel = mChannels[(int)channel];
        return chennel.Play(groupID, clip, soundData.volume, soundData.loop, 0f, fadeIn);
    }


    public void FadeOut(CHANNEL channel, float _fadeOutTime)
    {
        mChannels[(int)channel].FadeOutAll(_fadeOutTime);
    }

    public void FadeOutGroup(CHANNEL channel, int groupID, float _fadeOutTime)
    {
        mChannels[(int)channel].FadeOutGroup(groupID, _fadeOutTime);
    }

    public void Stop(CHANNEL channel, int _soundSEQ)
    {
        mChannels[(int)channel].Stop(_soundSEQ);
    }

    public void StopGroup(CHANNEL channel, int _soundSEQ)
    {
        mChannels[(int)channel].StopGroup(_soundSEQ);
    }


    void createChannel(CHANNEL _channel, int _pool)
    {
        GameObject obj = new GameObject(_channel.ToString());
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        SoundChannel compo = obj.AddComponent<SoundChannel>();
        compo.Init(_channel, _pool);
        mChannels[(int)_channel] = compo;
    }

}