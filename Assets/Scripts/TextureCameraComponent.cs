using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCameraComponent : MonoBehaviour
{
    public Camera camera;

    // Start is called before the first frame update
    void Awake()
    {
        Locator.ProvideTextureCamera(this);
    }
}
