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
            _renderer.color = StyleManager.Colors.Player;
        }
    }
}
