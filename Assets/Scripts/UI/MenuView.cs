using UnityEngine;
using UnityEngine.Events;

public class MenuView : BaseView
{
    public UnityAction OnStart;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        OnStart?.Invoke();
    }
}
