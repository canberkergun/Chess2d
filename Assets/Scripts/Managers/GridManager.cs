using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private int width;
        [SerializeField] private int height;

        [SerializeField] private GameObject cam;

        [SerializeField] private PieceData[] pieceDatas;
    
        private Dictionary<Vector2, Tile> _tiles;
    
        public static GridManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    
        private void Start()
        {
            SpawnTiles();
        }

        private void SpawnTiles()
        {
            _tiles = new Dictionary<Vector2, Tile>();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var tilePos = new Vector2(x, y);
                    var spawnedTile = Instantiate(tilePrefab, tilePos, Quaternion.identity, gameObject.transform).GetComponent<Tile>();
                    spawnedTile.name = $"Tile {x+1} {y+1}";
                
                    var isOffset = (x + y) % 2 == 1;
                
                    spawnedTile.Init(isOffset, tilePos);
                
                    _tiles[tilePos] = spawnedTile;
                
                    if (y <= 1 || y >= 6)
                    {
                        SpawnItems(tilePos);
                    }
                }
            }
        
            cam.transform.position = new Vector3(((float) width / 2) - 0.5f, ((float) height / 2) -0.5f, -10);
        }

        private void SpawnItems(Vector2 pos)
        {
            var isWhite =  pos.y <= 1;
            var pieceType = GetPieceType(pos);

            if (pieceType == PieceType.None) return;
            var data = Array.Find(pieceDatas, piece => piece.PieceType == pieceType);

            if (data == null) return;
            
            var piece = PieceFactory.CreatePiece(data);
            _tiles[pos].PlacePiece(piece, isWhite);
        }
    
        public Tile GetTileAtPosition(Vector2 position)
        {
            _tiles.TryGetValue(position, out Tile tile);
            return tile;
        }
        
        private static PieceType GetPieceType(Vector2 pos)
        {
            if ( (int) pos.y == 1 || (int) pos.y == 6) return PieceType.Pawn;
            return (int) pos.x switch
            {
                0 or 7 => PieceType.Rook,
                1 or 6 => PieceType.Knight,
                2 or 5 => PieceType.Bishop,
                3 => PieceType.Queen,
                4 => PieceType.King,
                _ => PieceType.None
            };
        }
    }
}
