using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceData", menuName = "Chess/PieceData", order = 1)]
public class PieceData : ScriptableObject
{
    public Sprite WhiteSprite;
    public Sprite BlackSprite;
    public PieceType PieceType;
}

public enum PieceType
{
    None,
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}
