﻿using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class LeaderboardsButton : ImageButtonBase
    {
        public override void Action() => Social.ShowLeaderboardUI();
        
        private new void Start()
        {
            base.Start();
            gameObject.SetActive(Social.localUser.authenticated);
        }
    }
}