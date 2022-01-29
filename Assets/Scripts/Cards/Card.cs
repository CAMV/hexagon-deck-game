using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Card", menuName = "Card Game/Card")]
public class Card : ScriptableObject
{
    [SerializeField]
    private CardEffect _frontEffect;

    [SerializeField]
    private CardEffect _backEffect;

    public CardEffect FrontEffect
    {
        get => _frontEffect;
    }
    public CardEffect BackEffect
    {
        get => _backEffect;
    }
}
