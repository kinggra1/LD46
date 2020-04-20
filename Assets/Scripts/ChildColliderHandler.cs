using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        GetComponentInParent<Rigidbody2D>().SendMessage("OnTriggerEnter2D", collision);
        transform.parent.SendMessageUpwards("OnTriggerEnter2D", collision);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        transform.parent.SendMessageUpwards("OnCollisionEnter2D", collision);
        GetComponentInParent<Rigidbody2D>().SendMessage("OnTriggerEnter2D", collision);
    }
}
