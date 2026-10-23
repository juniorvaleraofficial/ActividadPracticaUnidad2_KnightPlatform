using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public Texture2D completionPanel;
    public Texture2D replayButton;
    public GameObject goalVisual;

    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelCompleted = true;

            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                renderer.enabled = false;
            }

            BoxCollider2D goalCollider = GetComponent<BoxCollider2D>();

            if (goalCollider != null)
            {
                goalCollider.enabled = false;
            }

            if (goalVisual != null)
            {
                goalVisual.SetActive(false);
            }

            PlayerMovement movement = other.GetComponent<PlayerMovement>();

            if (movement != null)
            {
                movement.enabled = false;
            }
        }
    }

    private void OnGUI()
    {
        if (!levelCompleted)
        {
            return;
        }

        if (completionPanel == null || replayButton == null)
        {
            return;
        }

        float panelWidth = Mathf.Min(Screen.width * 0.5f, 900f);

        float panelHeight =
            panelWidth *
            completionPanel.height /
            completionPanel.width;

        float buttonWidth = panelWidth * 0.55f;

        float buttonHeight =
            buttonWidth *
            replayButton.height /
            replayButton.width;

        float spacing = 20f;

        float totalHeight =
            panelHeight +
            spacing +
            buttonHeight;

        float startY =
            (Screen.height - totalHeight) / 2f;

        Rect panelRect = new Rect(
            (Screen.width - panelWidth) / 2f,
            startY,
            panelWidth,
            panelHeight
        );

        Rect buttonRect = new Rect(
            (Screen.width - buttonWidth) / 2f,
            startY + panelHeight + spacing,
            buttonWidth,
            buttonHeight
        );

        GUI.DrawTexture(
            panelRect,
            completionPanel,
            ScaleMode.ScaleToFit,
            true
        );

        GUI.DrawTexture(
            buttonRect,
            replayButton,
            ScaleMode.ScaleToFit,
            true
        );

        if (GUI.Button(
            buttonRect,
            GUIContent.none,
            GUIStyle.none
        ))
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name
            );
        }
    }
}