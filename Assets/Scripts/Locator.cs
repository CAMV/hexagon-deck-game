using System.Collections;
using UnityEngine;


public static class Locator
{
    // private static CardsGameManager _cardsGameManager = null;

    // public static CardsGameManager GetCardsGameManager() => _cardsGameManager;

    // public static void ProvideCardsGameManager(CardsGameManager cgm) => _cardsGameManager = cgm;

    private static HexTileManager _hexTileManager = null;
    private static PlayerController _playerController = null;
    private static TextureCameraComponent _textureCamera = null;

    public static HexTileManager GetHexTileManager() => _hexTileManager;

    public static void ProvideHexTileManager(HexTileManager htm) => _hexTileManager = htm;

    public static PlayerController GetPlayerController() => _playerController;

    public static void ProvidePlayerController(PlayerController pc) => _playerController = pc;

    public static TextureCameraComponent GetTextureCamera() => _textureCamera;

    public static void ProvideTextureCamera(TextureCameraComponent pc) => _textureCamera = pc;
}
