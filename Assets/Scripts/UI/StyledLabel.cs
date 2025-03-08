using Escapa.Core.Managers;
using TMPro;
using UnityEngine;

namespace Escapa.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class StyledLabel : MonoBehaviour
    {
        [SerializeField]
        private bool isAlfa;

        private TextMeshProUGUI textMesh;

        public void SetText(string text) => textMesh.SetText(text);

        private void Awake() => textMesh = GetComponent<TextMeshProUGUI>();

        private void OnEnable() => DifficultyManager.DifficultyChanged += OnDifficultyChanged;

        private void Start() => textMesh.color = isAlfa ? StyleManager.Colors.TextAlfa : StyleManager.Colors.Text;

        private void OnDisable() => DifficultyManager.DifficultyChanged -= OnDifficultyChanged;

        private void OnDifficultyChanged() => textMesh.color = StyleManager.Colors.Text;
    }
}
