using UnityEngine;

namespace Scripts.UI
{
    public class UIRoot : MonoBehaviour
    {

        [SerializeField] private MenuView menuView;
        public MenuView MenuView => menuView;

        [SerializeField] private GameView gameView;
        public GameView GameView => gameView;

        [SerializeField] private GameOverView gameOverView;
        public GameOverView GameOverView => gameOverView;

    }
}