using UnityEngine;

namespace Managers
{
    public sealed class GameManager : MonoBehaviour
    {
        private Tile _selectedTile;
        public static GameManager Instance { get; private set; }

        private bool _isWhiteTurn = true;
    
        public bool IsWhiteTurn => _isWhiteTurn;
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void SelectTile(Tile tile)
        {
            if (_selectedTile == null)
            {
                if (!tile.HasPiece()) return;
                
                var movingPiece = tile.GetPiece();
                if (movingPiece.IsWhite() != IsWhiteTurn) return;
                
                _selectedTile = tile;
                _selectedTile.HighlightTile(true);
            }
            else
            {
                TryMovePiece(_selectedTile, tile);
                _selectedTile.HighlightTile(false);
                _selectedTile = null;
            }
        }

        private void EndTurn()
        {
            _isWhiteTurn = !_isWhiteTurn;
        }

        private void TryMovePiece(Tile fromTile, Tile toTile)
        {
            var movingPiece = fromTile.GetPiece();
        
            if (movingPiece == null || movingPiece.IsWhite() != IsWhiteTurn) return; // Ensure it's the piece's turn
        
            var currentPosition = fromTile.GetPosition();
            var targetPosition = toTile.GetPosition();

            if (movingPiece.IsValidMove(currentPosition, targetPosition, position =>
                    GridManager.Instance.GetTileAtPosition(position)?.GetPiece(), IsWhiteTurn))
            {
                IPiece toPiece = toTile.GetPiece();

                if (toPiece != null)
                {
                    Debug.Log($"Capturing a {toPiece.GetPieceType()} at {targetPosition}");
                    toTile.RemovePiece(); // Capture logic
                }

                // Move the piece
                toTile.PlacePiece(movingPiece, movingPiece.IsWhite());
                fromTile.RemovePiece();
            
                EndTurn();
            }
            else
            {
                Debug.Log("Invalid move!");
            }
        }
    }
}

