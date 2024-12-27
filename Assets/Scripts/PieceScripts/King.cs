using System;
using System.Collections.Generic;
using UnityEngine;

namespace PieceScripts
{
    public class King : IPiece
    {
        private readonly Sprite _whiteSprite;
        private readonly Sprite _blackSprite;
        private bool _isWhite;
        public King(PieceData data)
        {
            _whiteSprite = data.WhiteSprite;
            _blackSprite = data.BlackSprite;
        }

        public Sprite GetSprite(bool isWhite)
        {
            _isWhite = isWhite;
            return isWhite ? _whiteSprite : _blackSprite;
        }

        public PieceType GetPieceType()
        {
            return PieceType.King;
        }
    
        public bool IsWhite()
        {
            return _isWhite;
        }
    
        public bool IsValidMove(Vector2 from, Vector2 to, Func<Vector2, IPiece> getPieceAtPosition, bool isWhiteTurn)
        {
            if (IsWhite() != isWhiteTurn)
                return false;
        
            var dx = Mathf.Abs(from.x - to.x);
            var dy = Mathf.Abs(from.y - to.y);

            // Ensure move is within valid range
            if (dx > 1 || dy > 1) return false;

            // Check the piece at the target position
            var targetPiece = getPieceAtPosition(to);
        
            if (targetPiece != null)
            {
                // Ensure the piece is an enemy
                return targetPiece.IsWhite() != IsWhite();
            }

            // If the target position is empty, it's a valid move
            return true;
        }

        
    }
    
    
}