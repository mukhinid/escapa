using System.Collections.Generic;
using Escapa.Core.Managers;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Game
{
    [RequireComponent(typeof(EdgeCollider2D))]
    [RequireComponent(typeof(LineRenderer))]
    internal sealed class Edge : MonoBehaviour
    {
        private const float RESOLUTION = 6 / 10f;

        [SerializeField]
        private EdgeType _type;

        private MainCamera _camera;
        private EdgeCollider2D _collider;
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<MainCamera>();
            _collider = GetComponent<EdgeCollider2D>();
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            var position = Vector3.zero;
            var start = Vector2.zero;
            var end = Vector2.zero;

            var width = Screen.height * RESOLUTION;

            switch (_type)
            {
                case EdgeType.Left:
                {
                    position.x = -width * _camera.UnitsPerPixel / 2f;

                    start = new Vector2(0, -Screen.height * _camera.UnitsPerPixel / 2f);
                    end = new Vector2(0, Screen.height * _camera.UnitsPerPixel / 2f);

                    break;
                }
                case EdgeType.Top:
                {
                    position.y = Screen.height * _camera.UnitsPerPixel / 2f;

                    start = new Vector2(-width * _camera.UnitsPerPixel / 2f, 0);
                    end = new Vector2(width * _camera.UnitsPerPixel / 2f, 0);

                    break;
                }
                case EdgeType.Right:
                {
                    position.x = width * _camera.UnitsPerPixel / 2f;

                    start = new Vector2(0, -Screen.height * _camera.UnitsPerPixel / 2f);
                    end = new Vector2(0, Screen.height * _camera.UnitsPerPixel / 2f);

                    break;
                }
                case EdgeType.Bottom:
                {
                    position.y = -Screen.height * _camera.UnitsPerPixel / 2f;

                    start = new Vector2(-width * _camera.UnitsPerPixel / 2f, 0);
                    end = new Vector2(width * _camera.UnitsPerPixel / 2f, 0);

                    break;
                }
            }

            transform.position = position;
            _collider.SetPoints(new List<Vector2> { start, end});
            _lineRenderer.SetPositions(new Vector3[] { start, end });
            _lineRenderer.startColor = StyleManager.Colors.Player;
            _lineRenderer.endColor = StyleManager.Colors.Player;
        }
    }
}
