using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent _enterAction;
    [SerializeField] private UnityEvent _exitAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _enterAction?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _exitAction?.Invoke();
    }
}
