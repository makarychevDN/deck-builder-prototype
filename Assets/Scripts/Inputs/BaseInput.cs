using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInput : MonoBehaviour
{
    [SerializeField] protected bool isMyTurn;
    [SerializeField] protected BaseInput oppositeTeam;
    [SerializeField] protected List<Character> charactersList;
    public UnityEvent OnTurnEnded;
    public UnityEvent OnTurnStarted;
    public UnityEvent OnLose;

    public List<Character> CharactersList => charactersList;
    public BaseInput OppositeTeam => oppositeTeam;

    public virtual void Init(BaseInput enemyTeam)
    {
        this.oppositeTeam = enemyTeam;

        foreach(var character in charactersList)
        {
            character.Init(OnTurnStarted);
            character.OnDeath.AddListener(RemoveCharacter);
        }
    }

    public virtual void StartTurn()
    {
        isMyTurn = true;
        OnTurnStarted.Invoke();
    }

    public virtual void EndTurn()
    {
        isMyTurn = false;
        OnTurnEnded.Invoke();
    }

    public virtual async void RemoveCharacter(Character character)
    {
        await Task.Yield();

        charactersList.Remove(character);
        Destroy(character.gameObject);

        if (charactersList.Count == 0)
            OnLose.Invoke();
    }
}
