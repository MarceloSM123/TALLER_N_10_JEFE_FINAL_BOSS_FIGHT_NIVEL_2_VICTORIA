using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel_1");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego----");
        Application.Quit();
    }
}
