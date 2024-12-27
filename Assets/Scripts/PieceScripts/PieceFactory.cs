using System.Collections;
using System.Collections.Generic;
using PieceScripts;
using UnityEngine;

public class PieceFactory 
{
    public static IPiece CreatePiece(PieceData data)
    {
        return data.PieceType switch
        {
            PieceType.Queen => new Queen(data),
            PieceType.King => new King(data),
            PieceType.Bishop => new Bishop(data),
            PieceType.Pawn => new Pawn(data),
            PieceType.Rook => new Rook(data),
            PieceType.Knight => new Knight(data),
            _ => null
        };
    }
}
