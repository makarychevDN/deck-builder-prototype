using UnityEngine;
using UnityEngine.Events;

public class BaseInput : MonoBehaviour
{
    [SerializeField] protected bool isMyTurn;
    public UnityEvent OnTurnEnded;

    public virtual void StartTurn()
    {
        isMyTurn = true;
    }

    public virtual void EndTurn()
    {
        isMyTurn = false;
        OnTurnEnded.Invoke();
    }
}
