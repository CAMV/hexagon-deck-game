using System.Collections;
using UnityEngine;


public class Locator
{
    private static CardsGameManager _cardsGameManager = null;

    public static CardsGameManager GetCardsGameManager() => _cardsGameManager;

    public static void ProvideCardsGameManager(CardsGameManager cgm) => _cardsGameManager = cgm;
}
