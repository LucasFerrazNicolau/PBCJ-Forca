using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("score", 0);
    }

    public void StartGameWorld()
    {
        SceneManager.LoadScene("Lab1");
    }

    // Função utilizada para chamar a Cena de Créditos
    public void EndGame()
    {
        SceneManager.LoadScene("Lab1_Credits");
    }

    // Função para encerrar o jogo de fato (não funciona em Modo de Edição)
    public void CloseGame()
    {
        Application.Quit();
    }
}
