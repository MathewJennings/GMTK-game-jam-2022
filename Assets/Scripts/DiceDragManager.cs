using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DiceDragManager : MonoBehaviour {

    public delegate void DiceRollCompleted (GameObject dice);
    public static event DiceRollCompleted diceRollCompleted;

    [SerializeField]
    private InputAction mouseClick;
    [SerializeField]
    private int mouseDragPhysicsSpeed;

    private static Vector3 UP = -Vector3.forward;

    private Camera mainCamera;
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
        Ray ray = mainCamera.ScreenPointToRay (Mouse.current.position.ReadValue ());
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit)) {
            if (hit.collider != null) {
                GameObject hitGameObject = hit.collider.gameObject;
                if (hitGameObject.tag.Equals ("Dice")) {
                    diceThatIsDragging = hitGameObject;
                    StartCoroutine (DragUpdate (hitGameObject));
                }
            }
        }
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
        if (diceThatIsDragging != null) {
            Rigidbody rigidbody = diceThatIsDragging.GetComponent<Rigidbody> ();

            float dirX = Random.Range (0, 500);
            float dirY = Random.Range (0, 500);
            float dirZ = Random.Range (0, 500);
            rigidbody.AddForce (UP * 500);
            rigidbody.AddTorque (dirX, dirY, dirZ);
            diceThatIsResolving = diceThatIsDragging;
            diceThatIsDragging = null;
        }
    }

    void Update () {
        if (diceThatIsResolving != null) {
            Vector3 diceVelocity = diceThatIsResolving.GetComponent<Rigidbody> ().velocity;
            if (diceVelocity.Equals(Vector3.zero)) {
                if (diceRollCompleted != null) {
                    diceRollCompleted(diceThatIsResolving);
                }
                diceThatIsResolving = null;
            }
        }
    }
}