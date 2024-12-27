using System;
using UnityEngine;

namespace PieceScripts
{
    public class Queen : IPiece
    {
        private readonly Sprite _whiteSprite;
        private readonly Sprite _blackSprite;
        private bool _isWhite;
        public Queen(PieceData data)
        {
            _whiteSprite = data.WhiteSprite;
            _blackSprite = data.BlackSprite;
        }

        public Sprite GetSprite(bool isWhite)
        {
            _isWhite = isWhite;
            return _isWhite ? _whiteSprite : _blackSprite;
        }

        public PieceType GetPieceType()
        {
            return PieceType.Queen;
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

            if (dx == 0)
            {
                var direction = from.y - to.y < 0 ? 1 : -1;

                for (var i = 1; i < dy; i++)
                {
                    var piece = getPieceAtPosition(new Vector2(from.x, from.y + i * direction));
                    if (piece != null)
                    {
                        return false;
                    }
                }
            }
            else if (dy == 0)
            {
                var direction = from.x - to.x < 0 ? 1 : -1;

                for (var i = 1; i < dx; i++)
                {
                    var piece = getPieceAtPosition(new Vector2(from.x + i * direction, from.y));
                    if (piece != null)
                    {
                        return false;
                    }
                }
            }
            else if ( (int) dx == (int) dy)
            {
                var direction = from.x - to.x < 0 ? 1 : -1;

                for (var i = 1; i < dx; i++)
                {
                    var piece = getPieceAtPosition(new Vector2(from.x + i * direction, from.y + i * direction));
                    if (piece != null)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
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