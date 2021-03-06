using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwipe : MonoBehaviour
{

    private float swipeAngleDegrees;
    private float swipeScale;
    private float swipeTimeInSeconds;
    private int swipeDamage;
    private int knockBackForce;
    private GameObject attackDie;

    private float timePercentage = 0.0f;
    private Quaternion initialSwingRotation;
    private Quaternion targetSwingRotation;
    private float intermediateDegreesRotated;

    private float spriteOffset = 45.0f; //the sword sprite is at a 45 degree angle

    private HashSet<GameObject> enemiesHit = new HashSet<GameObject>();

    private static float INTERMEDIATE_ANGLES = 179f;

    public static void Create(Object prefab, GameObject attackingDie, DiceSide attackingDiceSide, AudioClip swipeSound)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject newGameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, player.transform) as GameObject;
        SwordSwipe newSwordSwipe = newGameObject.GetComponent<SwordSwipe>();
        newSwordSwipe.transform.localPosition = new Vector3(0, 0, 0);

        newSwordSwipe.swipeAngleDegrees = attackingDiceSide.swipeAngleDegrees;
        newSwordSwipe.swipeScale = attackingDiceSide.swipeScale;
        newSwordSwipe.swipeTimeInSeconds = attackingDiceSide.swipeTimeInSeconds;
        newSwordSwipe.swipeDamage = attackingDiceSide.swipeDamage;
        newSwordSwipe.knockBackForce = attackingDiceSide.knockBackForce;
        newSwordSwipe.attackDie = attackingDie;
        
        newSwordSwipe.transform.localScale = newSwordSwipe.transform.localScale * newSwordSwipe.swipeScale;
        newSwordSwipe.setInitialRotation();

        GameObject.FindGameObjectWithTag("MusicManager").GetComponent<AudioSource>().PlayOneShot(swipeSound, 0.5f);
    }

    private void setInitialRotation()
    {
        Vector3 diePosition = attackDie.transform.position;
        Vector3 swordSwipePosition = transform.position;
        Vector3 vectorToDie = diePosition - swordSwipePosition;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToDie); // Try to point the sword at the die...
        transform.Rotate(new Vector3(0, 0, spriteOffset)); // ... but then rotate 45 more degrees to account for the angle of the sword  in the sprite...
        transform.Rotate(new Vector3(0, 0, swipeAngleDegrees / 2)); // ... and then start the swing 1/2 the angle before the dice (the swing should end 1/2 the angle after the dice)

        initialSwingRotation = transform.rotation;
        initialSwingRotation = new Quaternion(initialSwingRotation.x, initialSwingRotation.y, initialSwingRotation.z, initialSwingRotation.w);
        targetSwingRotation = new Quaternion(initialSwingRotation.x, initialSwingRotation.y, initialSwingRotation.z, initialSwingRotation.w);
        if (swipeAngleDegrees > INTERMEDIATE_ANGLES)
        {
            setTargetRotation(INTERMEDIATE_ANGLES);
        }
        else
        {
            setTargetRotation(swipeAngleDegrees);
        }
    }

    private void setTargetRotation(float targetRotation)
    {
        intermediateDegreesRotated += targetRotation;
        targetSwingRotation *= Quaternion.Euler(0, 0, -targetRotation); // the target rotation is an additional "targetRotation" degrees clockwise from the starting angle
    }

    void Update()
    {
        timePercentage += Time.deltaTime / (swipeTimeInSeconds * intermediateDegreesRotated / swipeAngleDegrees);
        transform.localRotation = Quaternion.Slerp(initialSwingRotation, targetSwingRotation, timePercentage);
        if (completedRotation())
        {
            if (intermediateDegreesRotated < swipeAngleDegrees)
            {
                KeepRotating();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private bool completedRotation()
    {
        return (transform.localRotation.eulerAngles - targetSwingRotation.eulerAngles).magnitude < .05f;
    }

    private void KeepRotating()
    {
        timePercentage = 0;
        initialSwingRotation = new Quaternion(targetSwingRotation.x, targetSwingRotation.y, targetSwingRotation.z, targetSwingRotation.w);
        if (swipeAngleDegrees - intermediateDegreesRotated > INTERMEDIATE_ANGLES)
        {
            setTargetRotation(INTERMEDIATE_ANGLES);
        }
        else
        {
            setTargetRotation(swipeAngleDegrees - intermediateDegreesRotated);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemyGameObject = other.gameObject;
            if (enemiesHit.Contains(enemyGameObject))
            {
                return;
            }

            enemyGameObject.GetComponent<EnemyHealth>().TakeDamage(this.transform, swipeDamage);

            Vector3 knockbackDirection = enemyGameObject.transform.position - transform.position;
            enemyGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection.normalized * knockBackForce);

            enemiesHit.Add(enemyGameObject);
        }
    }
}
