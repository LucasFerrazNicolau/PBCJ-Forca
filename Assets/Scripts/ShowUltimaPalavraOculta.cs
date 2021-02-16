using UnityEngine;
using UnityEngine.UI;

public class ShowUltimaPalavraOculta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetString("ultimaPalavraOculta");
    }
}
