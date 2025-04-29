using Escapa.Core.Controllers;
using Escapa.Core.Managers;
using Escapa.Utility;
using TMPro;
using UnityEngine;

namespace Escapa.UI.Controllers
{
    public sealed class SocialGuiController : GuiControllerBase
    {
        [SerializeField]
        private GameObject _invite;
        [SerializeField]
        private GameObject _loginButton;
        [SerializeField]
        private GameObject _leaderBoard;
        [SerializeField]
        private TextMeshProUGUI _leaderBoardTitle;
        [SerializeField]
        private LeaderboardEntry[] _leaderBoardEntries;

        private void Start()
        {
            var auth = SocialManager.IsAuthorized;
            if (auth)
            {
                _invite.SetActive(false);
                _loginButton.SetActive(false);

                SocialManager.GetLeaderboard((name, entries) =>
                {
                    _leaderBoardTitle.text = name;
                    for (int i = 0; i < _leaderBoardEntries.Length; i++)
                    {
                        if (i >= entries.Count)
                        {
                            _leaderBoardEntries[i].gameObject.SetActive(false);
                        }
                        else
                        {
                            _leaderBoardEntries[i].gameObject.SetActive(true);
                            _leaderBoardEntries[i].Init(entries[i].Item1, entries[i].Item2, entries[i].Item3);
                        }
                    }
                    _leaderBoard.SetActive(true);
                });
            }
            else
            {
                _leaderBoard.SetActive(false);
                var mesh = _loginButton.GetComponentInChildren<TextMeshProUGUI>();
                mesh.color = StyleManager.Colors.Background;
            }
        }

        public void GoToMenu() => LoadScene(GameScenes.Menu);

        public void Authorize()
        {
            SocialManager.Authorize(result =>
            {
                _invite.SetActive(!result);
                _loginButton.SetActive(!result);
                _leaderBoard.SetActive(result);
            });
        }
    }
}
