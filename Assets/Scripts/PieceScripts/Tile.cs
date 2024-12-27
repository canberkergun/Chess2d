using System;
using Managers;
using UnityEditor;
using UnityEngine;


public class Tile : MonoBehaviour
{
    #region Fields

    private IPiece _piece;
    
    [SerializeField] private SpriteRenderer spriteRenderer, pieceRenderer;
    [SerializeField] private GameObject highlight;
    
    private Vector2 _position;
    private Vector2 _tilePos;

    private Color _tileColor;
    [SerializeField] private Color baseColor = Color.white, offsetColor = new Color(0.37f, 0.26f,  0.08f);
    

    #endregion

    #region Private Methods

    private void OnMouseEnter()
    {
        HighlightPiece(true);
    }
    
    private void OnMouseExit()
    {
        HighlightPiece(false);
    }

    private void OnMouseDown()
    {
        GameManager.Instance.SelectTile(this);
    }
    
    private void HighlightPiece(bool isHighlighted)
    {
        if (_piece != null && _piece.IsWhite() == GameManager.Instance.IsWhiteTurn)
        {
            // Change the tile's or piece's appearance to indicate it's active
            highlight.SetActive(isHighlighted);
        }
    }
    

    #endregion

    #region Public Methods

    
    public void Init(bool isOffset, Vector2 tilePos)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
        _tileColor = spriteRenderer.color;
        _position = tilePos;
    }
    
    public Vector2 GetPosition()
    {
        return _position;
    }
    
    public void PlacePiece(IPiece piece, bool isWhite)
    {
        _piece = piece;
        pieceRenderer.sprite = _piece.GetSprite(isWhite);
    }

    public void RemovePiece()
    {
        _piece = null;
        pieceRenderer.sprite = null;
    }
    
    public void HighlightTile(bool isHighlight)
    {
        spriteRenderer.color = isHighlight ? Color.yellow: _tileColor;
    }
    
    public IPiece GetPiece() => _piece;
    public bool HasPiece() => _piece != null;

    #endregion
    
    
}
