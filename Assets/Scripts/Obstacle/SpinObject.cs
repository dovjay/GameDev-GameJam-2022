using UnityEngine;

public class SpinObject : MonoBehaviour {
    [SerializeField] Vector3 rotationAxis;
    [SerializeField] float rotationSpeed;

    private void Update() {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}