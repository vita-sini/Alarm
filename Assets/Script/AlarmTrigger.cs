using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmTrigger : MonoBehaviour
{
    public event UnityAction Entered;
    public event UnityAction Exited;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Players>(out Players player))
        {
            Entered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Players>(out Players player))
        {
            Exited?.Invoke();
        }
    }
}
