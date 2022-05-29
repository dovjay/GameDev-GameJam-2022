using System.Collections;
using UnityEngine;

public class HazardSpike : MonoBehaviour {
    [SerializeField] float activeDuration = 1f;
    bool active = false;
    Animator anim;

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start() {
        StartCoroutine(ActivateSpike());
    }

    private IEnumerator ActivateSpike() {
        while (true) {
            active = !active;
            anim.SetBool("active", active);
            if (active) gameObject.layer = LayerMask.NameToLayer("Obstacle");
            else gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            yield return new WaitForSeconds(activeDuration);
        }
    }
}