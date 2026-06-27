using UnityEngine;
using TMPro;

public class UINivel : MonoBehaviour
{
    public static UINivel Instance;
    public TextMeshProUGUI textoNivel;
    public float duracion = 3f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        textoNivel.text = $"Day {GameManager.Instance.nivelActual}";
    }
}
