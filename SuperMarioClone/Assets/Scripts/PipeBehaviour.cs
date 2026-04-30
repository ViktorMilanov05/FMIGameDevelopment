using System.Collections;
using UnityEngine;

public class PipeBehaviour : MonoBehaviour
{
    [SerializeField]
    public Transform connection;
    [SerializeField]
    private Vector2 requiredInputDirection = Vector2.down;
    [SerializeField]
    private Vector3 enterDirection = Vector3.down;
    [SerializeField]
    private Vector3 exitDirection = Vector3.zero;

    private bool isEntering = false;

    IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;
        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;
        while (elapsed < duration)
        {
            float percentOfAnimation = elapsed / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, percentOfAnimation);
            player.localScale = Vector3.Lerp(startScale, endScale, percentOfAnimation);
            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;
    }

    IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovements>().enabled = false;
        player.GetComponent<Animator>().enabled = false;

        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return Move(player, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f);

        bool underground = connection.position.y < 0;
        Camera.main.GetComponent<CameraMovement>().SetUnderground(underground);

        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        isEntering = false;
        player.GetComponent<PlayerMovements>().enabled = true;
        player.GetComponent<Animator>().enabled = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isEntering) return;

        if (connection != null && other.CompareTag("Player"))
        {
            PlayerMovements playerMovements = other.GetComponent<PlayerMovements>();

            if (playerMovements == null) return;

            Vector2 input = playerMovements.MoveInput;

            if (input.sqrMagnitude > 0.1f &&
                Vector2.Dot(input.normalized, requiredInputDirection.normalized) > 0.8f)
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }
}
