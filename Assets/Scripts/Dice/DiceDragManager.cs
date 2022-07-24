using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private GameObject player;
    private GameObject diceThatIsBeingHovered;
    private GameObject diceThatIsDragging;
    private List<GameObject> diceThatAreResolving;
    private WaitForFixedUpdate waitforFixedUpdate = new WaitForFixedUpdate();

    private void Awake () {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        diceThatAreResolving = new List<GameObject>();
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
            diceThatIsDragging.GetComponent<LineToDieController>().setUpLine(player.transform, diceThatIsDragging.transform);
            StartCoroutine (DragUpdate (hitDice));
        }
    }

    // Returns the dice GameObject if one was hit, else null
    private GameObject rayCastHitADice () {
        Ray ray = mainCamera.ScreenPointToRay (Mouse.current.position.ReadValue ());
        if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Dice")))
        {
            if (hit.collider != null)
            {
                GameObject hitGameObject = hit.collider.gameObject;
                return hitGameObject;
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
            rigidbody.AddTorque(dirX, dirY, dirZ);
            rigidbody.AddForce(UP * 500);
            diceThatAreResolving.Add(diceThatIsDragging);
            diceThatIsDragging = null;
            endHover();
        }
    }

    private void Update () {
        tryResolveDiceRoll ();
        tryDiceHover ();
    }

    private void tryResolveDiceRoll ()
    {
        List<bool> toBeRemoved = new List<bool>();
        for(int i = diceThatAreResolving.Count-1; i >= 0; i--) {
            var dieThatIsResolving = diceThatAreResolving[i];
            Vector3 diceVelocity = dieThatIsResolving.GetComponent<Rigidbody>().velocity;
            if (diceVelocity.magnitude < diceStopRollMaxVelocity) {
                if (diceRollCompleted != null) {
                    diceRollCompleted (dieThatIsResolving);
                }
                dieThatIsResolving.GetComponent<LineToDieController>().removeLine();
                diceThatAreResolving.Remove(dieThatIsResolving);
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