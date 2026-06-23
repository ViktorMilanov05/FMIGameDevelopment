using System.Collections;
using UnityEngine;

public class FlagPoleBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform flag;

    [SerializeField]
    private Transform poleBottom;

    [SerializeField]
    private Transform castle;

    [SerializeField]
    private float speed = 6f;

    private Vector3 bigMarioOffset = new(0, 0.5f);

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(collision.transform));
        }    
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMovements>().enabled = false;

        var playerBehaviourScript = player.GetComponent<PlayerBehaviour>();
        playerBehaviourScript.StartHolding();

        var marioOffset = playerBehaviourScript.isBig() == true ? bigMarioOffset : Vector3.zero;

        yield return MoveTo(player, poleBottom.position + marioOffset);
        playerBehaviourScript.StopHolding();
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.down + Vector3.right);
        yield return MoveTo(player, castle.position + marioOffset);

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2);

        GameManager.Instance.NextLevel();
    }

    private IEnumerator MoveTo(Transform element, Vector3 destination)
    {
        while(Vector3.Distance(element.position, destination) > 0.1f)
        {
            element.position = Vector3.MoveTowards(element.position, destination, speed * Time.deltaTime);
            yield return null;
        }  
        element.position = destination;
    }
}
