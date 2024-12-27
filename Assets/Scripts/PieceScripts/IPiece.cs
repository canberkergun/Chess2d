using System;
using UnityEngine;

public interface IPiece
{
    Sprite GetSprite(bool isWhite);
    PieceType GetPieceType();
    bool IsValidMove(Vector2 from, Vector2 to, Func<Vector2, IPiece> getPieceAtPosition, bool isWhiteTurn);
    bool IsWhite();
}