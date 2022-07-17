using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DiceDragManager : MonoBehaviour {

    public delegate void DiceHover (GameObject dice);
    public static event DiceHover diceStartHover;
    public static event DiceHover diceEndHover;

    public delegate void DiceRollCompleted (GameObject dice);
    public static event DiceRollCompleted diceRollCompleted;

    [SerializeField]
    private InputAction mouseClick;
    [SerializeField]
    private int mouseDragPhysicsSpeed;
    [SerializeField]
    private float diceStopRollMaxVelocity;

    private static Vector3 UP = -Vector3.forward;

    private Camera mainCamera;
    private GameObject diceThatIsBeingHovered;
    private GameObject diceThatIsDragging;
    private GameObject diceThatIsResolving;
    private WaitForFixedUpdate waitforFixedUpdate = new WaitForFixedUpdate ();

    private void Awake () {
        mainCamera = Camera.main;
    }

    private void OnEnable () {
        mouseClick.Enable ();
        mouseClick.performed += MousePressed;
        mouseClick.canceled += MouseReleased;
    }

    private void OnDisable () {
        mouseClick.performed -= MousePressed;
        mouseClick.canceled -= MouseReleased;
        mouseClick.Disable ();
    }

    private void MousePressed (InputAction.CallbackContext context) {
        GameObject hitDice = rayCastHitADice ();
        if (hitDice != null) {
            diceThatIsDragging = hitDice;
            StartCoroutine (DragUpdate (hitDice));
        }
    }

    // Returns the dice GameObject if one was hit, else null
    private GameObject rayCastHitADice () {
        Ray ray = mainCamera.ScreenPointToRay (Mouse.current.position.ReadValue ());
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit)) {
            if (hit.collider != null) {
                GameObject hitGameObject = hit.collider.gameObject;
                if (hitGameObject.tag.Equals ("Dice")) {
                    return hitGameObject;
                }
            }
        }
        return null;
    }

    private IEnumerator DragUpdate (GameObject clickedObject) {
        float initialDistanceToCamera = Vector3.Distance (clickedObject.transform.position, mainCamera.transform.position);
        while (mouseClick.ReadValue<float> () != 0) {
            Ray ray = mainCamera.ScreenPointToRay (Mouse.current.position.ReadValue ());
            Vector3 direction = ray.GetPoint (initialDistanceToCamera) - clickedObject.transform.position;
            diceThatIsDragging.GetComponent<Rigidbody> ().velocity = direction * mouseDragPhysicsSpeed;
            yield return waitforFixedUpdate;
        }
    }

    private void MouseReleased (InputAction.CallbackContext context) {
        if (diceThatIsDragging != null)
        {
            Rigidbody rigidbody = diceThatIsDragging.GetComponent<Rigidbody>();

            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            rigidbody.AddForce(UP * 500);
            rigidbody.AddTorque(dirX, dirY, dirZ);
            diceThatIsResolving = diceThatIsDragging;
            diceThatIsDragging = null;
            endHover();
        }
    }

    private void Update () {
        tryResolveDiceRoll ();
        tryDiceHover ();
    }

    private void tryResolveDiceRoll () {
        if (diceThatIsResolving != null) {
            Vector3 diceVelocity = diceThatIsResolving.GetComponent<Rigidbody> ().velocity;
            if (diceVelocity.magnitude < diceStopRollMaxVelocity) {
                if (diceRollCompleted != null) {
                    diceRollCompleted (diceThatIsResolving);
                }
                diceThatIsResolving = null;
            }
        }
    }

    private void tryDiceHover () {
        if (diceStartHover != null) {
            GameObject hitDice = rayCastHitADice ();
            if (hitDice != null && diceThatIsBeingHovered == null) {
                diceThatIsBeingHovered = hitDice;
                diceStartHover (diceThatIsBeingHovered);
            } else if (hitDice == null && diceThatIsBeingHovered != null) {
                if (diceThatIsDragging == null)
                {
                    endHover();
                }
            }
        }
    }

    private void endHover()
    {
        diceEndHover(diceThatIsBeingHovered);
        diceThatIsBeingHovered = null;
    }
}