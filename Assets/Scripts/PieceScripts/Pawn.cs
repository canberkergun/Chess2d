using System;
using UnityEngine;

namespace PieceScripts
{
    public class Pawn : IPiece
    {
        private readonly Sprite _whiteSprite;
        private readonly Sprite _blackSprite;
        private bool _isWhite;
        private bool _hasMoved;
    
        public Pawn(PieceData data)
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
            return PieceType.Pawn;
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
            var dy = to.y - from.y;

            // Determine direction based on piece color
            var direction = _isWhite ? 1 : -1;
            var startRow = _isWhite ? 1f : 6f;

            // Straight moves (dx == 0)
            if (dx == 0)
            {
                if ( (int) dy != direction && (int) dy != 2 * direction) return false;

                if ( (int) dy == 2 * direction)
                {
                    if (_hasMoved || (int) from.y != (int) startRow) return false;
            
                    // Check if path is clear for a 2-tile move
                    if (getPieceAtPosition(from + new Vector2(0, direction * 2)) != null ||
                        getPieceAtPosition(to) != null)
                    {
                        return false;
                    }

                    _hasMoved = true;
                }
                else if ( (int) dy == direction)
                {
                    // Check if the target tile is clear for a 1-tile move
                    if (getPieceAtPosition(to) != null) return false;
                }
            }
            // Diagonal moves for capture (dx == 1)
            else if ( (int) dx == 1 && (int) dy == direction)
            {
                var targetPiece = getPieceAtPosition(to);
                if (targetPiece == null || targetPiece.IsWhite() == _isWhite) return false;
            }
            else
            {
                // Invalid move
                return false;
            }

            return true;
        }
    
    }
}