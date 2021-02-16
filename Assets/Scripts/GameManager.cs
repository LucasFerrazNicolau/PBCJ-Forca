using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int numTentativas; // Armazena as tentativas válidas da rodada
    private int maxnumTentativas; // Número máximo de tentativas para a Forca ou Salvação
    int score; // Armazena a pontuação do jogador

    public GameObject letra; // Prefab da letra no Game
    public GameObject centro; // Objeto que indica o centro da tela

    private string palavraOculta = ""; // Palavra a ser descoberta
    //private string[] palavrasOcultas = new string[] { "carro", "elefante", "futebol" }; // Vetor de palavras ocultas (usado nos labs anteriores)

    private int tamanhoPalavraOculta; // Tamanho da palavra oculta
    private char[] letrasOcultas; // Letras da palavra oculta
    private bool[] letrasDescobertas; // Indicador de quais letras já foram descobertas

    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("Centro Da Tela");
        InitGame();
        InitLetras();

        numTentativas = 0;
        maxnumTentativas = 10;
        UpdateNumTentativas();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTeclado();
    }

    void InitLetras()
    {
        int numLetras = tamanhoPalavraOculta;

        for (int i = 0; i < numLetras; i++)
        {
            Vector3 novaPosicao = new Vector3(
                centro.transform.position.x + ((i - (numLetras - 1) / 2.0f) * 80),
                centro.transform.position.y,
                centro.transform.position.z);
            GameObject l = Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "Letra " + (i + 1);
            l.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    void InitGame()
    {
        //palavraOculta = "Elefante"; // usado no lab 1a
        //int numeroAleatorio = Random.Range(0, palavrasOcultas.Length); // usado nos labs 1b e 2a
        //palavraOculta = palavrasOcultas[numeroAleatorio]; // usado nos labs 1b e 2a
        palavraOculta = GetPalavraDoArquivo(); // usado no lab 2b

        tamanhoPalavraOculta = palavraOculta.Length;
        palavraOculta = palavraOculta.ToUpper();
        letrasOcultas = new char[tamanhoPalavraOculta];
        letrasDescobertas = new bool[tamanhoPalavraOculta];
        letrasOcultas = palavraOculta.ToCharArray();
    }

    void CheckTeclado()
    {
        if (Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada);

            if (letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122)
            {
                numTentativas++;
                UpdateNumTentativas();
                if (numTentativas >= maxnumTentativas)
                {
                    SceneManager.LoadScene("Lab1_Forca");
                }

                for (int i = 0; i < tamanhoPalavraOculta; i++)
                {
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = char.ToUpper(letraTeclada);
                        if (letrasOcultas[i] == letraTeclada)
                        {
                            letrasDescobertas[i] = true;
                            GameObject.Find("Letra " + (i + 1)).GetComponent<Text>().text = letraTeclada.ToString();

                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore();

                            CheckPalavraDescoberta();
                        }
                    }
                }
            }
        }
    }

    void UpdateNumTentativas()
    {
        GameObject.Find("NumTentativas").GetComponent<Text>().text = numTentativas + " | " + maxnumTentativas;
    }

    void UpdateScore()
    {
        GameObject.Find("ScoreUI").GetComponent<Text>().text = "Score " + score;
    }

    void CheckPalavraDescoberta()
    {
        bool condicao = true;

        for (int i = 0; i < tamanhoPalavraOculta; i++)
        {
            condicao = condicao && letrasDescobertas[i];
        }

        if (condicao)
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);
            SceneManager.LoadScene("Lab1_Salvo");
        }
    }

    string GetPalavraDoArquivo()
    {
        TextAsset t = (TextAsset)Resources.Load("palavras", typeof(TextAsset));
        string s = t.text;
        string[] palavras = s.Split(' ');
        int palavraAleatoria = Random.Range(0, palavras.Length);
        return palavras[palavraAleatoria];
    }
}
