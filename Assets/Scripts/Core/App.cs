using System.Collections;
using System.Collections.Generic;
using Scripts.UI;
using Scripts.Game;
using Scripts.Sound;
using SpawnSystem;
using StateMachine.States;
using UnityEngine;

namespace Scripts.Core
{
    /// <summary>
    /// Application base class. Contains links to all main systems.
    /// </summary>
    public class App : Singleton<App>
    {
        [SerializeField] private GameInitSettings gameInitSettings;
        [SerializeField] private StateMachine.Core.StateMachine stateMachine;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ObjectPooler objectPooler;
        [SerializeField] private UIRoot uI;
        public GameInitSettings GameInitSettings => gameInitSettings;
        public SoundManager SoundManager => soundManager;
        public GameManager GameManager => gameManager;
        public ObjectPooler ObjectPooler => objectPooler;
        public UIRoot GetUI => uI;

        private void Start()
        {
            stateMachine.ChangeState(new MenuState());
        }
    }
}