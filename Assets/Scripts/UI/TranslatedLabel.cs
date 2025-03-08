using Escapa.Core.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TranslatedLabel : MonoBehaviour
    {
        [SerializeField]
        private LanguageTokens _token;

        private TextMeshProUGUI _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _textMesh.SetText(LanguageManager.GetString(_token));
        }
    }
}
