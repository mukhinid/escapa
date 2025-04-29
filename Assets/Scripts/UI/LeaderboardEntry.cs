using Escapa.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Escapa.UI
{
    public sealed class LeaderboardEntry : MonoBehaviour
    {
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private TextMeshProUGUI _score;

        public void Init(string avatarUrl, string name, int score)
        {
            _textMesh.text = name;
            _score.text = (score / 1000f).ToString("0.0");
            SocialManager.DownloadImage(avatarUrl, (avatar) => _avatar.overrideSprite = avatar);
        }
    }
}
