using UnityEngine;
using UnityEngine.UI;

public class HealtUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public Image[] hearts; // Arrastra aquí los 3 corazones

    [Header("Referencias del Jugador")]
    public playerHealth playerHealth; // Arrastra aquí el jugador (con el script PlayerHealth)

    void Start()
    {
        // Si no asignaste el jugador manualmente, lo busca automáticamente por tag
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerHealth = player.GetComponent<playerHealth>();
        }
    }

    void Update()
    {
        if (playerHealth == null || hearts == null) return;

        UpdateHearts(playerHealth.GetCurrentLives());
    }

    void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null) continue;

            // Opción A: ocultar/mostrar la imagen completa
            hearts[i].enabled = i < currentLives;

            // Opción B (alternativa): cambiar el color a transparente
            // Color c = hearts[i].color;
            // c.a = (i < currentLives) ? 1f : 0f;
            // hearts[i].color = c;
        }
    }
}