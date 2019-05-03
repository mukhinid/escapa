﻿using Escapa.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.Buttons
{
    [RequireComponent(typeof(TextMeshProUGUI), typeof(Button))]
    public abstract class TextButtonBase : MonoBehaviour
    {
        public bool disableTranslating;
        public LanguageTokens token;
        
        protected TextMeshProUGUI TextMesh;

        protected void Awake()
        {
            TextMesh = GetComponent<TextMeshProUGUI>();
        }
        
        protected void OnEnable()
        {
            StyleManager.StyleChanged += OnStyleChanged;
        }

        protected void Start()
        {
            if (!disableTranslating)
            {
                TextMesh.SetText(LanguageManager.GetString(token));
                
            }
            TextMesh.color = StyleManager.CurrentTheme.Text;
        }

        protected void OnDisable()
        {
            StyleManager.StyleChanged -= OnStyleChanged;
        }


        protected void OnStyleChanged(Theme theme)
        {
            TextMesh.color = theme.Text;
        }
        
        public abstract void Action();
    }
}