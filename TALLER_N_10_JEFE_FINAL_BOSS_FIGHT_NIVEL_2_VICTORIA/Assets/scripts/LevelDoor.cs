using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    public string nombreSiguienteEscena = "Nivel_2";  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.gameObject.name == "personaje"){
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
    }
}