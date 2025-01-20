using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInput : MonoBehaviour
{
    [SerializeField] protected bool isMyTurn;
    [SerializeField] protected BaseInput enemyTeam;
    [SerializeField] protected List<Character> charactersList;
    public UnityEvent OnTurnEnded;
    public UnityEvent OnTurnStarted;

    public List<Character> CharactersList => charactersList;
    public BaseInput EnemyTeam => enemyTeam;

    public virtual void Init(BaseInput enemyTeam)
    {
        this.enemyTeam = enemyTeam;
        charactersList.ForEach(character => character.Init(OnTurnStarted));
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
}
