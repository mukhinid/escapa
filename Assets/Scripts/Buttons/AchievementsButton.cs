﻿using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class AchievementsButton : ImageButtonBase
    {
        public override void Action()
        {
            if (Social.localUser.authenticated)
            {
                Social.ShowAchievementsUI();
            }
        }

        private void Start()
        {
            gameObject.SetActive(Social.localUser.authenticated);
        }
    }
}