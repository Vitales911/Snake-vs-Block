using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGame : MonoBehaviour
{
    public int score;
    [SerializeField] Text scoreText;
    bool time = true;
    [SerializeField] private GameObject panelGameWon;
    [SerializeField] private GameObject panelGameOver;


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void NewMenuGame()
    {
        SceneManager.LoadScene(0);
    }

    public void StoptGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        
        if(time == true)
        {
            Time.timeScale = 0;
            time = false;
        }
        else if (time == false)
        {
            Time.timeScale = 1;
            time = true;
        }

    }
    private void Start()
    {
        StartCoroutine(scoreAdd());
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }
    IEnumerator scoreAdd()
    {
        while (true)
        {
            score++;
            yield return new WaitForSeconds(2.5f);
        }
    }
}
