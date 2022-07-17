using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingEnemy : MonoBehaviour
{
    private EnemyAI enemyAI;
    public List<Transform> teleportLocations;
    public float waitBeforeTeleportingSec;
    public AudioClip warpSound;
    public AudioClip chargeSound;

    private int teleportIndex;
    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        teleportIndex = 0;
        var coroutine = WaitThenTeleport();
        StartCoroutine(coroutine);
    }

    IEnumerator WaitThenTeleport()
    {
        enemyAI.SetState(EnemyState.Chasing);
        yield return new WaitForSeconds(waitBeforeTeleportingSec);
        enemyAI.SetState(EnemyState.HoldPosition); 
        GameObject.FindGameObjectWithTag("MusicManager").GetComponent<AudioSource>().PlayOneShot(chargeSound, 0.5f);
        yield return new WaitForSeconds(1);
        var target = teleportLocations[teleportIndex++%teleportLocations.Count].position;
        GameObject.FindGameObjectWithTag("MusicManager").GetComponent<AudioSource>().PlayOneShot(warpSound, 0.5f);
        transform.position = new Vector3(target.x, target.y, transform.position.z);

        var coroutine = WaitThenTeleport();
        StartCoroutine(coroutine);
    }

}
