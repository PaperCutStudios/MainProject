using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	PlayerTable ptPlayer1 = new PlayerTable();
	PlayerTable ptPlayer2 = new PlayerTable();
	PlayerTable ptPlayer3 = new PlayerTable();
	PlayerTable ptPlayer4 = new PlayerTable();

	// Use this for initialization
	void Start () {
		ptPlayer1.ActivityInfo = WriteActivityInfo (2, 0, 6, 8, 1, 0, 1, 3, 2, 1, 3, 5, 6, 3, 4, 1, 0, 14, 6, 0);
		ptPlayer2.ActivityInfo = WriteActivityInfo (1, 2, 0, 4, 8, 1, 0, 1, 5, 0, 4, 3, 5, 6, 4, 0, 1, 0, 16, 9);
		ptPlayer3.ActivityInfo = WriteActivityInfo (0, 5, 1, 2, 3, 1, 0, 1, 0, 2, 5, 6, 4, 3, 4, 0, 8, 0, 1, 7);
		ptPlayer4.ActivityInfo = WriteActivityInfo (3, 6, 1, 2, 0, 4, 6, 1, 0, 1, 5, 2, 4, 3, 5, 11, 5, 0, 1, 0);

		ptPlayer1.AvailabilityInfo = WriteAvailabilityInfo (0, 2, 5, 4, 1, 0, 4, 8, 17, 1);
		ptPlayer2.AvailabilityInfo = WriteAvailabilityInfo (0, 5, 2, 6, 1, 0, 12, 5, 3, 1);
		ptPlayer3.AvailabilityInfo = WriteAvailabilityInfo (0, 3, 2, 6, 1, 0, 9, 2, 11, 1);
		ptPlayer4.AvailabilityInfo = WriteAvailabilityInfo (0, 2, 4, 5, 1, 0, 3, 4, 12, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int[,] WriteActivityInfo( int a, int b, int c, int d, int e, int f, int g, int h, int i, int j, int k, int l, int m, int n, int o, int p, int q, int r, int s, int t)
	{
		int[,] Output = new int[4, 5];
		Output [0, 0] = a;
		Output [0, 1] = b;
		Output [0, 2] = c;
		Output [0, 3] = d;
		Output [0, 4] = e;
		Output [1, 0] = f;
		Output [1, 1] = g;
		Output [1, 2] = h;
		Output [1, 3] = i;
		Output [1, 4] = j;
		Output [2, 0] = k;
		Output [2, 1] = l;
		Output [2, 2] = m;
		Output [2, 3] = n;
		Output [2, 4] = o;
		Output [3, 0] = p;
		Output [3, 1] = q;
		Output [3, 2] = r;
		Output [3, 3] = s;
		Output [3, 4] = t;

		return Output;

	}
	int [,] WriteAvailabilityInfo(int a, int b, int c, int d, int e, int f, int g, int h, int i, int j)
	{
		int [,] Output = new int[2,5];
		Output [0, 0] = a;
		Output [0, 1] = b;
		Output [0, 2] = c;
		Output [0, 3] = d;
		Output [0, 4] = e;
		Output [1, 0] = f;
		Output [1, 1] = g;
		Output [1, 2] = h;
		Output [1, 3] = i;
		Output [1, 4] = j;

		return Output;
	}
}
