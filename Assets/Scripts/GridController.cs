using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    [SerializeField] private Tilemap objectTilemap;
    [SerializeField] private Tilemap overlayTileMap;
    [SerializeField] private RuleTile highlightTile;

    private Vector3Int previousHighlightTile;

    public void HighlightTile(Vector3 highlightWorldPos, bool onlyIfGameobject = true)
    {
        Vector3Int highlightCellPos = objectTilemap.WorldToCell(highlightWorldPos);

        if (highlightCellPos != previousHighlightTile && (!onlyIfGameobject || onlyIfGameobject && objectTilemap.GetInstantiatedObject(highlightCellPos) != null) )
        {
            overlayTileMap.SetTile(highlightCellPos, highlightTile);
            overlayTileMap.SetTile(previousHighlightTile, null);
        }
        else if (highlightCellPos != previousHighlightTile)
        {
            overlayTileMap.SetTile(previousHighlightTile, null);
        }

        previousHighlightTile = highlightCellPos;
    }

    public bool GetTileGameObject(Vector3 objectWorldPos, out GameObject tileGO)
    {
        Vector3Int objectCellPos = objectTilemap.WorldToCell(objectWorldPos);
        tileGO = objectTilemap.GetInstantiatedObject(objectCellPos);
        return tileGO != null;
    }
}
