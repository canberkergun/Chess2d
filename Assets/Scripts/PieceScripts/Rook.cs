using System;
using UnityEngine;

namespace PieceScripts
{
    public class Rook : IPiece
    {
        private readonly Sprite _whiteSprite;
        private readonly Sprite _blackSprite;
        private bool _isWhite;
    
        public Rook(PieceData data)
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
            return PieceType.Rook;
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

            float startDirection;
        
            // Ensure move is within valid range
            if (!(dx == 0 || dy == 0)) return false;

            var isHorizontal = dx > dy;

            if (isHorizontal)
            { 
                startDirection = from.x - to.x > 0 ? to.x : from.x;
            }
            else
            {
                startDirection = from.y - to.y > 0 ? to.y : from.y;
            }

            var limit = isHorizontal ? dx : dy;
        
            for (var i = 1; i < limit; i++)
            {
                if (isHorizontal)
                {
                    var inBetweenPiece = getPieceAtPosition(new Vector2(startDirection + i, from.y));
                    if (inBetweenPiece != null)
                    {
                        return false;
                    }
                }
                else
                {
                    var inBetweenPiece = getPieceAtPosition(new Vector2(from.x, startDirection + i));
                    if (inBetweenPiece != null)
                    {
                        return false;
                    }
                }
            }
        
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