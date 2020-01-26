using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BAudioPlayer : MonoBehaviour
{
    /// <summary>
    /// 连接音频管理器
    /// </summary>
    protected abstract void LinkToManager();

    /// <summary>
    /// 添加音效源并初始化
    /// </summary>
    /// <param name="_go"></param>
    /// <returns></returns>
    public AudioSource CreateAudioSource(GameObject _go)
    {
        AudioSource _as;

        _as = _go.AddComponent<AudioSource>();
        _as.playOnAwake = false;
        _as.loop = false;

        return _as;
    }

    public abstract void ChangeVolume(float _value);
}
