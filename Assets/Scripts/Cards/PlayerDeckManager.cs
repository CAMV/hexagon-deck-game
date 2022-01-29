using UnityEditor;
using UnityEngine;

public class PlayerDeckManager : MonoBehaviour
{

    public enum DeckType
    {
        Deck, Graveyard, Exile, Hand
    }

    private Deck _deck;
    private Deck _graveyard;
    private Deck _exiled;
    private Deck _hand;

    public bool UseHandCard(int index, bool isFront)
    {
        if (index < 0 || index >= _hand.Count)
            return false;

        var cardToUse = _hand.PopCard(index);

        if (isFront)
            cardToUse.FrontEffect.Use();

        _graveyard.AddCard(cardToUse, DeckPosition.First);

        return true;
    }

    public void ResetDeck()
    {
        for (int i = 0; i < _graveyard.Count; i++)
            _deck.AddCard(_graveyard.PopCard(DeckPosition.First));

        _deck.Shuffle();
    }

    public void DrawCard()
    {
        if (_deck.Count < 1)
            ResetDeck();

        _hand.AddCard(_deck.PopCard(DeckPosition.First));
    }
}
