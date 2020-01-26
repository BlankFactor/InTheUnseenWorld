﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BMusicPlayer : BAudioPlayer
{
    public virtual void Start()
    {
        LinkToManager();
    }

    public abstract override void ChangeVolume(float _value);

    protected override void LinkToManager()
    {
        AudioManager.instance.AddMusicLisitenner(this);
    }
}