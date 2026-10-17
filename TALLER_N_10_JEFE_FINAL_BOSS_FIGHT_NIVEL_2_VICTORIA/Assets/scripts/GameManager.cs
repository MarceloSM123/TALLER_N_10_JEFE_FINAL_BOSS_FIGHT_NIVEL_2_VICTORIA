using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int coinCount = 0;

    // Evento que la UI escuchará cuando cambie el contador
    public event Action<int> OnCoinChanged;

    void Awake()
    {
        // Patrón Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Notificamos el valor inicial (0) por si la UI necesita sincronizarse
        OnCoinChanged?.Invoke(coinCount);
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        Debug.Log("Monedas: " + coinCount);

        // Avisamos a todos los suscriptores (la UI)
        OnCoinChanged?.Invoke(coinCount);
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}