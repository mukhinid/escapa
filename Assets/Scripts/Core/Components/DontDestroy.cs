using UnityEngine;

namespace Escapa.Core.Components
{
    public sealed class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
