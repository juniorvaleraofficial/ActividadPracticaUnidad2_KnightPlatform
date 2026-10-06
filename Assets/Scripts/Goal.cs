using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelCompleted = true;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            PlayerMovement movement = other.GetComponent<PlayerMovement>();

            if (movement != null)
            {
                movement.enabled = false;
            }
        }
    }

    private void OnGUI()
    {
        if (levelCompleted)
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = 36;
            boxStyle.alignment = TextAnchor.MiddleCenter;
            boxStyle.fontStyle = FontStyle.Bold;

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 22;
            buttonStyle.fontStyle = FontStyle.Bold;

            float boxWidth = 500;
            float boxHeight = 220;

            float boxX = (Screen.width - boxWidth) / 2;
            float boxY = (Screen.height - boxHeight) / 2;

            GUI.Box(
                new Rect(boxX, boxY, boxWidth, boxHeight),
                "¡NIVEL COMPLETADO!",
                boxStyle
            );

            if (GUI.Button(
                new Rect(
                    Screen.width / 2 - 100,
                    boxY + 140,
                    200,
                    50
                ),
                "Jugar de nuevo",
                buttonStyle
            ))
            {
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().name
                );
            }
        }
    }
}