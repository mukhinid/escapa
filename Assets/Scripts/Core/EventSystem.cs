using UnityEngine;

namespace Escapa.Core
{
    public sealed class EventSystem : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
