using UnityEngine;

namespace Escapa.Game
{
    internal sealed class Scale : MonoBehaviour
    {
        private const float Columns = 6;
        private const float Rows = 10;
        private const float AspectRatio = Columns / Rows;

        private void Start()
        {
            var currentAspect = Screen.width / (float)Screen.height;

            if (currentAspect > AspectRatio)
            {
                return;
            }

            var scale = Vector3.one;

            if (Screen.height > Screen.width)
            {
                var height = Screen.width / AspectRatio;

                scale.x = height / Screen.height;
                scale.y = height / Screen.height;
            }

            transform.localScale = scale;
        }
    }
}
