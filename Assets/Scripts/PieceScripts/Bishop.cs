using System;
using UnityEngine;

namespace PieceScripts
{
    public class Bishop : IPiece
    {
        private readonly Sprite _whiteSprite;
        private readonly Sprite _blackSprite;
        private bool _isWhite;
        public Bishop(PieceData data)
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
            return PieceType.Bishop;
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
            if ((int) dx != (int) dy) return false;
        
            //Checking if there are pieces in between
            for (var i = 1; i < dx; i++)
            {
                var xIncrease = i;
                var yIncrease = i;
                
                if (from.x > to.x) 
                { xIncrease = -i;}

                if (from.y > to.y)
                { yIncrease = -i; }
            
                var inBetweenPiece = getPieceAtPosition(new Vector2(from.x + xIncrease, from.y + yIncrease));
                
                if (inBetweenPiece != null)
                {
                    return false;
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