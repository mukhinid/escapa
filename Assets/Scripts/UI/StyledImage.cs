using Escapa.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.UI
{
    [RequireComponent(typeof(Image))]
    public sealed class StyledImage : MonoBehaviour
    {
        private Image _image;

        private void Awake() => _image = GetComponent<Image>();

        private void OnEnable() => DifficultyManager.DifficultyChanged += OnDifficultyChanged;

        private void Start() => _image.color = StyleManager.Colors.Text;

        private void OnDisable() => DifficultyManager.DifficultyChanged -= OnDifficultyChanged;

        private void OnDifficultyChanged() => _image.color = StyleManager.Colors.Text;
    }
}
