using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectableManager {

    public static int paperPieces;
    public static int scrapPieces;

    public static void Reset()
    {
        paperPieces = 0;
        scrapPieces = 0;
    }

    public static void Collect(Collectable.CollectableType type)
    {
        switch(type)
        {
            case Collectable.CollectableType.PIECE:
                paperPieces += 1;
                MonoBehaviour.print(paperPieces);
                break;
            case Collectable.CollectableType.SCRAP:
                scrapPieces += 1;
                MonoBehaviour.print(scrapPieces);
                break;
        };     
    }
}
