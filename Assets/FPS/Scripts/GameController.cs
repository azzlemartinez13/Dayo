using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //public EnemySpawnController EnemySpawnController;


    [SerializeField]
    private GameObject _win_ParticleSystemGO;

    [SerializeField]
    private GameObject _winUIGO;

    [SerializeField]
    private GameObject _loseUIGO;

    public void GameWon()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        _win_ParticleSystemGO.SetActive(value: true);
        _winUIGO.SetActive(value: true);
    }

    public void GameLost()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        _loseUIGO.SetActive(value: true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}