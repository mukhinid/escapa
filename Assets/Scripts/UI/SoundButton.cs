﻿using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.UI
{
    public sealed class SoundButton : ImageButtonBase
    {
        [SerializeField]
        private Sprite spriteOn;
        [SerializeField]
        private Sprite spriteOff;

        private ISoundController soundController;

        public override void Action()
        {
            soundController.Mute();
            Image.overrideSprite = soundController.IsMuted ? spriteOff : spriteOn;
        }

        private new void Awake()
        {
            base.Awake();
            soundController = GameObject.FindWithTag(Tags.SystemController).GetComponent<ISoundController>();
        }

        private new void Start()
        {
            base.Start();
            Image.overrideSprite = soundController.IsMuted ? spriteOff : spriteOn;
        }
    }
}