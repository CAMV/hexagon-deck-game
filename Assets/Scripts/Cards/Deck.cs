using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeckPosition
{
    Last, First, Random
}

public class Deck
{
    private List<Card> _cards = new List<Card>();
    private List<int> _order = new List<int>();

    public int Count
    {
        get
        {
            return _cards.Count;
        }
    }

    public void AddCards(List<Card> cardsToAdd, DeckPosition deckPos = DeckPosition.Last)
    {
        foreach (var card in cardsToAdd)
        {
            AddCard(card);
        }
    }

    public void AddCard(Card cardToAdd, DeckPosition deckPos = DeckPosition.Last)
    {
        switch (deckPos)
        {
            case DeckPosition.First:
                _order.Add(_cards.Count);
                break;
            case DeckPosition.Last:
                _order.Add(_order[0]);
                _order[0] = _cards.Count;
                break;
        }

        _cards.Add(cardToAdd);
    }

    public Card PopCard(DeckPosition deckPos)
    {

        Card cardToReturn = null;
        int indexToRemove = -1;

        switch (deckPos)
        {
            case DeckPosition.First:
                indexToRemove = 0;
                break;
            case DeckPosition.Last:
                indexToRemove = _cards.Count - 1;
                break;
            case DeckPosition.Random:
                indexToRemove = Random.Range(0, _cards.Count);
                break;
        }

        if (indexToRemove < 0)
            return null;

        cardToReturn = _cards[_order[indexToRemove]];
        _cards.RemoveAt(_order[indexToRemove]);
        _order.RemoveAt(indexToRemove);

        return cardToReturn;
    }

    public void Shuffle()
    {
        _order.Clear();

        var _cIndexes = new List<int>();

        for (int i = 0; i < _cards.Count; i++)
            _cIndexes.Add(i);

        for (int i = 0; i < _cards.Count; i++)
        {
            var pick = Random.Range(0, _cIndexes.Count);
            _order.Add(pick);
            _cIndexes.RemoveAt(pick);
        }
    }
}
