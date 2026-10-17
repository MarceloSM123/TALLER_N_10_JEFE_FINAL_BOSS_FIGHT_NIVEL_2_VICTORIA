using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Valor de la moneda")]
    public int coinValue = 1;

    // Se ejecuta cuando algo entra al Trigger de la moneda
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos que sea el jugador (por tag)
        if (other.CompareTag("Player"))
        {
            // Le avisamos al GameManager que sume una moneda
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCoin(coinValue);
                if(AudioManager.Instance != null ){
    AudioManager.Instance.ReproducirSfx(AudioManager.Instance.sonidoMoneda);
}
            }

            // Destruimos la moneda
            Destroy(gameObject);
        }
    }
}