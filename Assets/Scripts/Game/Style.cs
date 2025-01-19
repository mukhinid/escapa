using Escapa.Core.Managers;
using UnityEngine;

namespace Escapa.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    internal sealed class Style : MonoBehaviour
    {
        [SerializeField]
        private StylingColor _color;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _renderer.color = _color switch
            {
                StylingColor.Background => StyleManager.Colors.Background,
                StylingColor.Enemy => StyleManager.Colors.Enemy,
                StylingColor.Player => StyleManager.Colors.Player,
                StylingColor.Text => StyleManager.Colors.Text,
                StylingColor.TextAlfa => StyleManager.Colors.TextAlfa,
                _ => Color.white,
            };
        }
    }
}
