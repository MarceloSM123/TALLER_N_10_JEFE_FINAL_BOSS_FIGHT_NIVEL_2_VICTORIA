using UnityEngine;
using UnityEngine.UI; // Para usar "Text" (Legacy)

public class CoinUI : MonoBehaviour
{
    [Header("Referencia al texto")]
    public Text coinText; // Legacy Text

    void Start()
    {
        // Nos suscribimos al evento del GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCoinChanged += UpdateCoinText;

            // Sincronizamos con el valor actual por si ya había monedas
            UpdateCoinText(GameManager.Instance.GetCoinCount());
        }
        else
        {
            Debug.LogWarning("CoinUI: No se encontró GameManager en la escena.");
        }
    }

    void OnDestroy()
    {
        // Nos desuscribimos para evitar errores al cambiar de escena
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCoinChanged -= UpdateCoinText;
        }
    }

    void UpdateCoinText(int count)
    {
        if (coinText != null)
            coinText.text = "Monedas: " + count;
    }
}