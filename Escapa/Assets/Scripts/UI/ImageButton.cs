using Escapa.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.UI
{
    [RequireComponent(typeof(Image))]
    public sealed class ImageButton : MonoBehaviour
    {
        private Image image;

        private void Awake() => image = GetComponent<Image>();

        private void OnEnable() => DifficultyManager.DifficultyChanged += OnDifficultyChanged;

        private void Start()
        {
            image.color = StyleManager.Colors.Text;
        }

        private void OnDisable() => DifficultyManager.DifficultyChanged -= OnDifficultyChanged;

        private void OnDifficultyChanged() => image.color = StyleManager.Colors.Text;
    }
}
