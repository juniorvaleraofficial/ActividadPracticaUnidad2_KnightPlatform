
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollision : MonoBehaviour
{
    // Evita solicitar varios reinicios en el mismo contacto.
    private bool isRestarting = false;

    // Detecta una colisión física entre el enemigo y otro objeto.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Solo reaccionamos si el objeto es el jugador.
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // Evita ejecutar el reinicio varias veces.
        if (isRestarting)
        {
            return;
        }

        isRestarting = true;

        // Obtiene y reinicia la escena que está activa.
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
