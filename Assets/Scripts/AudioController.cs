using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Função utiliada para impedir que o GameObject de áudio seja destruído durante troca de Cenas, mantendo a música tocando
        DontDestroyOnLoad(gameObject);
    }
}
