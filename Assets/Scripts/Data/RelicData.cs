using UnityEngine;

public abstract class RelicData : ScriptableObject
{
    public string relicName;
    public string description;
    public Sprite icon;

    string Player;
    // Called when the relic is acquired
    public virtual void OnAcquire(Player player) { }

    // Called at the start of a turn
    public virtual void OnTurnStart(Player player) { }

    // Called when a battle ends
    public virtual void OnBattleEnd(Player player) { }
}
