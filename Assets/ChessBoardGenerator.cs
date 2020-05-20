using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject BlackSquare;
    [SerializeField]
    GameObject WhiteSquare;

    [SerializeField]
    int BoardWidth;
    [SerializeField]
    int BoardLength;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < BoardWidth; i++)
        {
            for(int j = 0; j < BoardLength; j++)
            {
                var Square = (i + j) % 2 == 0 ? BlackSquare : WhiteSquare;
                var Cube = Instantiate(Square, this.transform);
                var Position = Cube.transform.localPosition;
                Position = Position + Vector3.right * j;
                Position = Position + Vector3.forward * i;
                Cube.transform.localPosition = Position;
            }
        }
    }
}
