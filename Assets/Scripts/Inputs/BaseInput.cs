using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInput : MonoBehaviour
{
    [SerializeField] protected bool isMyTurn;
    [SerializeField] protected BaseInput enemyTeam;
    [SerializeField] protected List<Character> charactersList;
    public UnityEvent OnTurnEnded;

    public List<Character> CharactersList => charactersList;
    public BaseInput EnemyTeam => enemyTeam;

    public virtual void Init(BaseInput enemyTeam)
    {
        this.enemyTeam = enemyTeam;
        charactersList.ForEach(character => character.Init());
    }

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
