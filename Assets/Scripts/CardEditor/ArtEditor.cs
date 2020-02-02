using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtEditor : MonoBehaviour
{

    [SerializeField]
    private Painter painter = null;

    public Texture2D ExportTexture () {
        return painter.ExportTexture();
    }

}
