using UnityEngine;
using System.Collections.Generic;
using SerializeReferenceEditor;
// Useful for storing card info like description, mana, and image without hardcoding
[CreateAssetMenu(menuName = "Data/Card")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public string Description { get; set; }
    [field: SerializeField] public int Mana { get; set; }
    [field: SerializeField] public Sprite Image { get; set; }
    [field: SerializeField] public CardType Type { get; set; }
    [field: SerializeReference, SR] public List<Effect> Effects { get; set; }
}


public enum CardType
{
    Attack,
    Skill,
    Power
}