using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
	public static BoardManager Instance{ set; get; }

	private bool[,] allowedMoves{ set; get; }

	private bool[,] targetMoves{ set; get; }

	public Daman[,] Damans{ set; get; }

	private Daman selectedDaman;
	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;
	private int selectionX = -1;
	private int selectionY = -1;
	public List<GameObject> DamanPrefabs;
	public List<GameObject> activeDaman = new List<GameObject> ();
	private Quaternion orientation = Quaternion.Euler(0, 180, 0);
	public bool isWhiteTurn = true;
	public bool aiMinimax = true;
	private bool pionAttack = false;
	private int beforeMoveX;
	private int beforeMoveY;
	public GameObject pionRedIndi;
	public GameObject pionWhiteIndi;

	private void Start ()
	{
		Instance = this;
		SpawnAllDamans ();
	}
	
	private void Update ()
	{
		UpdateSelection ();
		DrawDamBoard ();

		pionWhiteIndi.SetActive (isWhiteTurn);
		pionRedIndi.SetActive (!isWhiteTurn);

		if (Input.GetMouseButtonDown (0)) 
		{
			if (selectionX >= 0 && selectionY >= 0) 
			{
				if (selectedDaman == null) 
				{
					//select the daman
					SelectDaman (selectionX, selectionY);
				} 
				else 
				{
					//move the daman
					MoveDaman (selectionX, selectionY);
				}
			}
		}
	}

	private void SelectDaman (int x, int y)
	{
		if (Damans [x, y] == null)
			return;

		if (Damans [x, y].isWhite != isWhiteTurn)
			return;

		beforeMoveX = x;
		beforeMoveY = y;

		allowedMoves = Damans [x, y].PossibleMove ();
		selectedDaman = Damans [x, y];
		BoardHighlights.Instance.HighlightAllowedMoves (allowedMoves);

		Damans [x, y].CheckTarget (1, 0);
		targetMoves = Damans [x, y].TargetMove ();
		BoardHighlightsTarget.Instance.HighlightAllowedMoves (targetMoves);	
	}

	private void MoveDaman (int x, int y)
	{
		if (allowedMoves [x, y] && !pionAttack) 
		{
			Damans [selectedDaman.CurrentX, selectedDaman.CurrentY] = null;
			selectedDaman.transform.position = GetTileCenter (x, y);
			selectedDaman.SetPosition (x, y);
			Damans [x, y] = selectedDaman;
			isWhiteTurn = !isWhiteTurn;

			BoardHighlights.Instance.Hidehighlights ();
			BoardHighlightsTarget.Instance.Hidehighlights ();
			selectedDaman = null;
			actionMinimax ();
		} 
		else if (targetMoves [x, y]) 
		{
			//set first value
			pionAttack = true;

			beforeMoveX = selectedDaman.CurrentX;
			beforeMoveY = selectedDaman.CurrentY;

			//set destroy dam target
			int destroyDamanX = selectedDaman.CurrentX + ((x - selectedDaman.CurrentX) / 2);
			int destroyDamanY = selectedDaman.CurrentY + ((y - selectedDaman.CurrentY) / 2);

			activeDaman.Remove (Damans [destroyDamanX, destroyDamanY].gameObject);
			Destroy (Damans [destroyDamanX, destroyDamanY].gameObject);

			//dam move
			Damans [selectedDaman.CurrentX, selectedDaman.CurrentY] = null;
			selectedDaman.transform.position = GetTileCenter (x, y);
			selectedDaman.SetPosition (x, y);
			Damans [x, y] = selectedDaman;

			selectedDaman.GetComponent<AudioSource>().Play();

			//set condition to next target dam
			BoardHighlights.Instance.Hidehighlights ();
			BoardHighlightsTarget.Instance.Hidehighlights ();

			if (Damans [x, y].CheckTarget (beforeMoveX, beforeMoveY)) 
			{
				targetMoves = Damans [x, y].TargetMove ();
				selectedDaman = Damans [x, y];
				BoardHighlightsTarget.Instance.HighlightAllowedMoves (targetMoves);
			} 
			else 
			{
				actionMinimaxAttack();
				if(aiMinimax)
				{

				}
				else
				{
					//isWhiteTurn = !isWhiteTurn;
					selectedDaman = null;
					pionAttack = false;
				}
			}
		} 
		else 
		{
			BoardHighlights.Instance.Hidehighlights ();
			BoardHighlightsTarget.Instance.Hidehighlights ();

			if (!pionAttack) 
			{
				selectedDaman = null;
			} 
			else 
			{
				if(!aiMinimax)
				{
					//isWhiteTurn = !isWhiteTurn;
					selectedDaman = null;
					pionAttack = false;
				}
			}
		}
	}

	private void UpdateSelection ()
	{
		if (!Camera.main)
			return;

		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("DamPlane"))) 
		{
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		} 
		else 
		{
			selectionX = -1;
			selectionY = -1;
		}
	}
	
	private void SpawnDaman (int index, int x, int y)
	{
		GameObject go = Instantiate (DamanPrefabs [index], GetTileCenter (x, y), Quaternion.Euler (-90, 90, 0)) as GameObject;
		go.transform.SetParent (transform);
		Damans [x, y] = go.GetComponent<Daman> ();
		Damans [x, y].SetPosition (x, y);
		activeDaman.Add (go);

		//var rotationVector = transform.rotation.eulerAngles;
		//rotationVector.x = 0;
		//transform.rotation = Quaternion.Euler (rotationVector);
	}

	private void SpawnAllDamans ()
	{
		activeDaman = new List<GameObject> ();
		Damans = new Daman[5, 9];

		//Spawn white team!

		//whitePion
		SpawnDaman (0, 0, 0);

		SpawnDaman (0, 2, 0);

		SpawnDaman (0, 4, 0);

		SpawnDaman (0, 1, 1);

		SpawnDaman (0, 2, 1);

		SpawnDaman (0, 3, 1);

		for (int i = 0; i < 5; i++)
			SpawnDaman (0, i, 2);

		for (int i = 0; i < 5; i++)
			SpawnDaman (0, i, 3);

		//Spawn red team!
		
		//redPion

		SpawnDaman (1, 0, 8);
		
		SpawnDaman (1, 2, 8);
		
		SpawnDaman (1, 4, 8);
		
		SpawnDaman (1, 1, 7);
		
		SpawnDaman (1, 2, 7);
		
		SpawnDaman (1, 3, 7);

		for (int i = 0; i < 5; i++)
			SpawnDaman (1, i, 6);

		for (int i = 0; i < 5; i++)
			SpawnDaman (1, i, 5);

	}

	private Vector3 GetTileCenter (int x, int y)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * y) + TILE_OFFSET;
		return origin;
	}
	
	private void DrawDamBoard ()
	{
		Vector3 widthLine = Vector3.right * 5;
		Vector3 heigthLine = Vector3.forward * 9;
		
		for (int i = 0; i <= 9; i++) 
		{
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);
			for (int j = 0; j <= 5; j++) 
			{
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heigthLine);
			}
		}

		//Draw the selection
		if (selectionX >= 0 && selectionY >= 0) 
		{
			Debug.DrawLine (
				Vector3.forward * selectionY + Vector3.right * selectionX,
				Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

			Debug.DrawLine (
				Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
				Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
		}
	}
	// minimax
	public class MNode
	{
		public Daman daman;
		public int targetX;
		public int targetY;
		public int score = 0;
		public bool attack = false;
		public int player;
	}

	List<MNode> nodes = new List<MNode> ();
	List<MNode> nodesAttack = new List<MNode> ();

	//skema action minimax

	void actionMinimaxAttack()
	{
		nodesAttack = new List<MNode> ();
		actionMove ();

		MNode node = minimax (1, true);

		exec (node, node.attack);

		isWhiteTurn = true;
	}

	void actionMinimax()
	{
		actionMove ();
		actionAttack ();

		MNode node = minimax (1, true);

		exec (node, node.attack);

		isWhiteTurn = true;
	}

	void actionMove()
	{
		nodes = new List<MNode> ();

		foreach (Daman daman in Damans) 
		{
			if(daman != null)
			{
				if(!daman.isWhite)
				{
					getScoreNodeMove(daman);
				}
			}
		}
	}

	void actionAttack()
	{
		nodesAttack = new List<MNode> ();

		foreach (Daman daman in Damans) 
		{
			if (daman != null)
			{
				if(!daman.isWhite)
				{
					getScoreNodeAttack(daman);
				}
			}
		}
	}

	void exec(MNode daman, bool Attack)
	{
		if (Attack) 
		{
			beforeMoveX = daman.daman.CurrentX;
			beforeMoveY = daman.daman.CurrentY;

			//set destroy dam target
			int destroyDamanX = daman.daman.CurrentX + ((daman.targetX - daman.daman.CurrentX) / 2);
			int destroyDamanY = daman.daman.CurrentY + ((daman.targetY - daman.daman.CurrentY) / 2);

			activeDaman.Remove (Damans [destroyDamanX, destroyDamanY].gameObject);
			Destroy (Damans [destroyDamanX, destroyDamanY].gameObject);

			Damans [daman.daman.CurrentX, daman.daman.CurrentY] = null;
			daman.daman.transform.position = GetTileCenter (daman.targetX, daman.targetY);
			daman.daman.SetPosition (daman.targetX, daman.targetY);
			Damans [daman.targetX, daman.targetY] = daman.daman;
		} 
		else 
		{
			Damans[daman.daman.CurrentX, daman.daman.CurrentY] = null;
			daman.daman.transform.position = GetTileCenter(daman.targetX, daman.targetY);
			daman.daman.SetPosition(daman.targetX, daman.targetY);
			Damans[daman.targetX, daman.targetY] = daman.daman;
		}
	}

	//skema mencari node

	void getScoreNodeMove(Daman daman)
	{
		bool[,] a = daman.PossibleMove ();

		int hasil = 0;

		for (int i = 0; i < 5; i++) 
		{
			for(int j = 0; j < 9; j++)
			{
				if(a[i, j])
				{
					MNode node = new MNode();
					node.daman = daman;
					node.targetX = i;
					node.targetY = j;

					bool[,] possibleMove = PossibleMove (i,j);

					node.score = getScoreMove(possibleMove);

					nodes.Add(node);
				}
			}
		}
	}

	void getScoreNodeAttack(Daman daman)
	{
		daman.CheckTarget (1, 0);
		bool[,] a = daman.TargetMove ();

		int hasil = 0;

		for (int i = 0; i < 5; i++) 
		{
			for(int j = 0; j < 9; j++)
			{
				if(a [i, j])
				{
					Debug.Log(a[i,j]);
					MNode node = new MNode();
					node.daman = daman;
					node.targetX = i;
					node.targetY = j;

					node.attack = true;
					node.score = 10;

					nodesAttack.Add(node);
				}
			}
		}
	}

	//mencari score untuk perbandingan

	int getScoreMove(bool[,] a)
	{
		int hasil = 0;

		for (int i = 0; i < 5; i++) 
		{
			for(int j = 0; j < 9; j++)
			{
				if(a[i,j])
				{
					hasil++;
				}
			}
		}

		return hasil;
	}

	int getScoreAttack(bool[,] a)
	{
		int hasil = 0;

		for (int i = 0; i < 5; i++) 
		{
			for(int j = 0; j < 9; j++)
			{
				if(a[i, j])
				{
					hasil++;
				}
			}
		}

		return hasil;
	}

	//algoritma minimax yang akan dilakukan

	public MNode minimax(int depth, bool maximizingPlayer)
	{
		if (depth == 0) 
		{
			return null;
		}

		Debug.Log (nodesAttack.Count);

		MNode nodeReturn = new MNode ();

		if (maximizingPlayer) 
		{
			int bestValue = 0;
			foreach (MNode node in nodes) 
			{
				if (node.score > bestValue) 
				{
					bestValue = node.score;
					nodeReturn = node;

				}
			}

			foreach (MNode nodeAttack in nodesAttack) 
			{
				if (nodeAttack.score > bestValue) 
				{
					bestValue = nodeAttack.score;
					nodeReturn = nodeAttack;

				}
			}

			if (nodeReturn.score != 0) 
			{
				Debug.Log (nodeReturn.score);
			}

			return nodeReturn;

		} 
		else 
		{
			int bestValue = 0;
			foreach(MNode node in nodes)
			{
				if(node.score < bestValue)
				{
					bestValue = node.score;
					nodeReturn = node;
				}
			}

			foreach(MNode nodeAttack in nodesAttack)
			{
				if(nodeAttack.score < bestValue)
				{
					bestValue = nodeAttack.score;
					nodeReturn = nodeAttack;
				}
			}

			return nodeReturn;
		}

		return null;
	}

	//testing condition

	bool[,] r = new bool[5, 9];
	bool[,] r2 = new bool[5, 9];

	private bool[,] PossibleMove(int CurrentX, int CurrentY)
	{
		//white team move
		if (CurrentY < 2) 
		{
			if (CurrentY == 1) 
			{
				if (CurrentX == 1) 
				{
					r [CurrentX + 1, CurrentY + 1] = checkCondition (CurrentX + 1, CurrentY + 1);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX - 1, CurrentY - 1] = checkCondition (CurrentX - 1, CurrentY - 1);
				} 
				else if (CurrentX == 2) 
				{
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
				} 
				else if (CurrentX == 3) 
				{
					r [CurrentX - 1, CurrentY + 1] = checkCondition (CurrentX - 1, CurrentY + 1);
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY - 1] = checkCondition (CurrentX + 1, CurrentY - 1);
				}
			} 
			else if (CurrentY == 0) 
			{
				if (CurrentX == 0) 
				{
					r [CurrentX + 1, CurrentY + 1] = checkCondition (CurrentX + 1, CurrentY + 1);
					r [CurrentX + 2, CurrentY] = checkCondition (CurrentX + 2, CurrentY);
				} 
				else if (CurrentX == 2) 
				{
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX + 2, CurrentY] = checkCondition (CurrentX + 2, CurrentY);
					r [CurrentX - 2, CurrentY] = checkCondition (CurrentX - 2, CurrentY);
				} 
				else if (CurrentX == 4) 
				{
					r [CurrentX - 1, CurrentY + 1] = checkCondition (CurrentX - 1, CurrentY + 1);
					r [CurrentX - 2, CurrentY] = checkCondition (CurrentX - 2, CurrentY);
				}
			}
		} 
		else if (CurrentY > 6) 
		{
			if (CurrentY == 7) 
			{
				if (CurrentX == 3) 
				{
					r [CurrentX - 1, CurrentY - 1] = checkCondition (CurrentX - 1, CurrentY - 1);
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY + 1] = checkCondition (CurrentX + 1, CurrentY + 1);
				} 
				else if (CurrentX == 2) 
				{
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
				} 
				else if (CurrentX == 1) 
				{
					r [CurrentX + 1, CurrentY - 1] = checkCondition (CurrentX + 1, CurrentY - 1);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX - 1, CurrentY + 1] = checkCondition (CurrentX - 1, CurrentY + 1);
				}
			} 
			else if (CurrentY == 8) 
			{
				if (CurrentX == 4) 
				{
					r [CurrentX - 1, CurrentY - 1] = checkCondition (CurrentX - 1, CurrentY - 1);
					r [CurrentX - 2, CurrentY] = checkCondition (CurrentX - 2, CurrentY);
				} 
				else if (CurrentX == 2) 
				{
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
					r [CurrentX - 2, CurrentY] = checkCondition (CurrentX - 2, CurrentY);
					r [CurrentX + 2, CurrentY] = checkCondition (CurrentX + 2, CurrentY);
				} 
				else if (CurrentX == 0) 
				{
					r [CurrentX + 1, CurrentY - 1] = checkCondition (CurrentX + 1, CurrentY - 1);
					r [CurrentX + 2, CurrentY] = checkCondition (CurrentX + 2, CurrentY);
				}
			}
		} 
		else 
		{
			if (CurrentX == 0) 
			{
				if (CurrentY == 2)
				{
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX + 1, CurrentY + 1] = checkCondition (CurrentX + 1, CurrentY + 1);
				}
				else if(CurrentY == 3 || CurrentY == 5)
				{
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
				}
				else if (CurrentY == 4)
				{
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
					r [CurrentX + 1, CurrentY - 1] = checkCondition (CurrentX + 1, CurrentY - 1);
					r [CurrentX + 1, CurrentY + 1] = checkCondition (CurrentX + 1, CurrentY + 1);
				}
				else if(CurrentX == 6)
				{
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
					r [CurrentX + 1, CurrentY - 1] = checkCondition (CurrentX + 1, CurrentY - 1);
				}
			}
			else if (CurrentY == 2)
			{
				if(CurrentX == 1 || CurrentX == 3)
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
			}
			else if (CurrentY == 4)
			{
				if (CurrentX == 1 || CurrentX == 3)
				{
					Debug.Log(CurrentX + ','+CurrentY);
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
				}
			}
			else if (CurrentX == 2)
			{
				if(CurrentY == 3 || CurrentY == 5)
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
			}
			else if (CurrentY == 6)
			{
				if (CurrentX == 1 || CurrentX == 3)
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
			}
			else if (CurrentX == 4)
			{
				if (CurrentY == 2)
					r [CurrentX - 1, CurrentY + 1] = checkCondition (CurrentX - 1, CurrentY + 1);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
				if (CurrentY == 3 || CurrentY == 5)
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
				if(CurrentY == 4)
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
					r [CurrentX - 1, CurrentY - 1] = checkCondition (CurrentX - 1, CurrentY - 1);
					r [CurrentX - 1, CurrentY + 1] = checkCondition (CurrentX - 1, CurrentY + 1);
				if (CurrentY == 6)
					r [CurrentX - 1, CurrentY - 1] = checkCondition (CurrentX - 1, CurrentY - 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
			}
			else if (CurrentX == 1)
			{
				if (CurrentY == 3 || CurrentY == 5)
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
					r [CurrentX - 1, CurrentY - 1] = checkCondition (CurrentX - 1, CurrentY - 1);
					r [CurrentX - 1, CurrentY + 1] = checkCondition (CurrentX - 1, CurrentY + 1);
					r [CurrentX + 1, CurrentY - 1] = checkCondition (CurrentX + 1, CurrentY - 1);
					r [CurrentX + 1, CurrentY + 1] = checkCondition (CurrentX + 1, CurrentY + 1);
			}
			else if (CurrentX == 2)
			{
				if (CurrentY == 2 || CurrentY == 4 || CurrentY == 6)
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
					r [CurrentX - 1, CurrentY - 1] = checkCondition (CurrentX - 1, CurrentY - 1);
					r [CurrentX - 1, CurrentY + 1] = checkCondition (CurrentX - 1, CurrentY + 1);
					r [CurrentX + 1, CurrentY - 1] = checkCondition (CurrentX + 1, CurrentY - 1);
					r [CurrentX + 1, CurrentY + 1] = checkCondition (CurrentX + 1, CurrentY + 1);
			}
			else if (CurrentX == 3)
			{
				if (CurrentY == 3 || CurrentY == 5)
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
					r [CurrentX - 1, CurrentY - 1] = checkCondition (CurrentX - 1, CurrentY - 1);
					r [CurrentX - 1, CurrentY + 1] = checkCondition (CurrentX - 1, CurrentY + 1);
					r [CurrentX + 1, CurrentY - 1] = checkCondition (CurrentX + 1, CurrentY - 1);
					r [CurrentX + 1, CurrentY + 1] = checkCondition (CurrentX + 1, CurrentY + 1);
			}
		}

		NormalizeDam (1, 0);

		return r;
	}

	private bool NormalizeDam(int beforeX, int beforeY)
	{
		r2 [1, 0] = false;
		r2 [3, 0] = false;
		r2 [0, 1] = false;
		r2 [4, 1] = false;
		
		r2 [1, 8] = false;
		r2 [3, 8] = false;
		r2 [0, 7] = false;
		r2 [4, 7] = false;
		
		r2 [beforeX, beforeY] = false;
		
		r [1, 0] = false;
		r [3, 0] = false;
		r [0, 1] = false;
		r [4, 1] = false;
		
		r [1, 8] = false;
		r [3, 8] = false;
		r [0, 7] = false;
		r [4, 7] = false;
		
		for (int i = 0; i<5; i++) 
		{
			for (int j = 0; j<9; j++) 
			{
				if (r2 [i, j]) 
				{
					return true;
				}
			}
		}
		
		return false;
	}

	private bool checkCondition(int x, int y)
	{
		GameObject[] whitepion = GameObject.FindGameObjectsWithTag("whitepion");
		GameObject[] redpion = GameObject.FindGameObjectsWithTag("redpion");

		foreach (GameObject pion in whitepion) 
		{
			Pion daman = pion.GetComponent ("Pion") as Pion;

			if(daman.CurrentX == x && daman.CurrentY == y)
			{
				return false;
			}
		}

		foreach (GameObject pion in redpion) 
		{
			Pion daman = pion.GetComponent ("Pion") as Pion;

			if(daman.CurrentX == x && daman.CurrentY == y)
			{
				return false;
			}
		}

		return true;
	}

	private bool checktarget (GameObject[] targets, int x, int y, int beforeX, int beforeY)
	{
		//bool status = false;
		
		foreach (GameObject pion in targets) 
		{
			Pion daman = pion.GetComponent ("Pion") as Pion;

			if (daman.CurrentY < 2) 
			{
				if (daman.CurrentY == 1) 
				{
					if (daman.CurrentX == 1) 
					{
						if (daman.CurrentX == daman.CurrentX + 1 && daman.CurrentY == daman.CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY + 1] = checkCondition (daman.CurrentX + 1, daman.CurrentY + 1);
							}
						}
						
						if (daman.CurrentX == daman.CurrentX + 1 && daman.CurrentY == daman.CurrentY) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY] = checkCondition (daman.CurrentX + 1, daman.CurrentY);
							}
						}
					} 
					else if (daman.CurrentX == 2) 
					{
						if (daman.CurrentX == daman.CurrentX && daman.CurrentY == daman.CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX, daman.CurrentY + 1] = checkCondition (daman.CurrentX, daman.CurrentY + 1);
							}
						}
					} 
					else if (daman.CurrentX == 3) 
					{
						if (daman.CurrentX == daman.CurrentX - 1 && daman.CurrentY == daman.CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY + 1] = checkCondition (daman.CurrentX - 1, daman.CurrentY + 1);
							}
						}
						if (daman.CurrentX == daman.CurrentX - 1 && daman.CurrentY == daman.CurrentY) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY] = checkCondition (daman.CurrentX - 1, daman.CurrentY);
							}
						}
					}
				} 
				else if (daman.CurrentY == 0) 
				{
					if (daman.CurrentX == 0) 
					{
						if (daman.CurrentX == daman.CurrentX + 1 && daman.CurrentY == daman.CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY + 1] = checkCondition (daman.CurrentX + 1, daman.CurrentY + 1);
							}
						}
						if (daman.CurrentX == daman.CurrentX + 2 && daman.CurrentY == daman.CurrentY) 
						{
							if (checkCondition (daman.CurrentX + 2, daman.CurrentY)) 
							{
								r2 [daman.CurrentX + 2, daman.CurrentY] = checkCondition (daman.CurrentX + 2, daman.CurrentY);
							}
						}
					} 
					else if (daman.CurrentX == 2) 
					{
						if (daman.CurrentX == daman.CurrentX && daman.CurrentY == daman.CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX, daman.CurrentY] = checkCondition (daman.CurrentX, daman.CurrentY);
							}
						}
					} 
					else if (daman.CurrentX == 4) 
					{
						if (daman.CurrentX == daman.CurrentX - 1 && daman.CurrentY == daman.CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY + 1] = checkCondition (daman.CurrentX - 1, daman.CurrentY + 1);
							}
						}
						if (daman.CurrentX == daman.CurrentX - 2 && daman.CurrentY == daman.CurrentY) 
						{
							if (checkCondition (daman.CurrentX - 2, daman.CurrentY)) 
							{
								r2 [daman.CurrentX - 2, daman.CurrentY] = checkCondition (daman.CurrentX - 2, daman.CurrentY);
							}
						}
					}
				}
			} 
			else if (daman.CurrentY > 6) 
			{
				if (daman.CurrentY == 7) 
				{
					if (daman.CurrentX == 3) 
					{
						if (daman.CurrentX == daman.CurrentX - 1 && daman.CurrentY == daman.CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY - 1)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY - 1] = checkCondition (daman.CurrentX - 1, daman.CurrentY - 1);
							}
						}
						if (daman.CurrentX == daman.CurrentX - 1 && daman.CurrentY == daman.CurrentY) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY] = checkCondition (daman.CurrentX - 1, daman.CurrentY);
							}
						}
					} else if (daman.CurrentX == 2) 
					{
						if (daman.CurrentX == daman.CurrentX && daman.CurrentY == daman.CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX, daman.CurrentY - 1)) 
							{
								r2 [daman.CurrentX, daman.CurrentY - 1] = checkCondition (daman.CurrentX, daman.CurrentY - 1);
							}
						}
					} else if (daman.CurrentX == 1) 
					{
						if (daman.CurrentX == daman.CurrentX + 1 && daman.CurrentY == daman.CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY - 1)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY - 1] = checkCondition (daman.CurrentX + 1, daman.CurrentY - 1);
							}
						}
						if (daman.CurrentX == daman.CurrentX + 1 && daman.CurrentY == daman.CurrentY) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY] = checkCondition (daman.CurrentX + 1, daman.CurrentY);
							}
						}
					}
				} else if (daman.CurrentY == 8) 
				{
					if (daman.CurrentX == 4) 
					{
						if (daman.CurrentX == daman.CurrentX - 1 && daman.CurrentY == daman.CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY - 1)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY - 1] = checkCondition (daman.CurrentX - 1, daman.CurrentY - 1);
							}
						}
						if (daman.CurrentX == daman.CurrentX - 2 && daman.CurrentY == daman.CurrentY) 
						{
							if (checkCondition (daman.CurrentX - 2, daman.CurrentY)) 
							{
								r2 [daman.CurrentX - 2, daman.CurrentY] = checkCondition (daman.CurrentX - 2, daman.CurrentY);
							}
						}
					} else if (daman.CurrentX == 2) 
					{
						if (daman.CurrentX == daman.CurrentX && daman.CurrentY == daman.CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX, daman.CurrentY - 1)) 
							{
								r2 [daman.CurrentX, daman.CurrentY - 1] = checkCondition (daman.CurrentX, daman.CurrentY - 1);
							}
						}
						
					} else if (daman.CurrentX == 0) 
					{
						if (daman.CurrentX == daman.CurrentX + 1 && daman.CurrentY == daman.CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY - 1)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY - 1] = checkCondition (daman.CurrentX + 1, daman.CurrentY - 1);
							}
						}
						
						if (daman.CurrentX == daman.CurrentX + 2 && daman.CurrentY == daman.CurrentY) 
						{
							if (checkCondition (daman.CurrentX + 2, daman.CurrentY)) 
							{
								r2 [daman.CurrentX + 2, daman.CurrentY] = checkCondition (daman.CurrentX + 2, daman.CurrentY);
							}
						}
					}
				}
				else if (daman.CurrentX == 0)
				{
					if(daman.CurrentY == 2)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
					}
					if(daman.CurrentY == 3)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
					}
					if(daman.CurrentY == 5)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
					}
					if(daman.CurrentY == 4)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
					}
					if(daman.CurrentY == 6)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
					}
				}
				else if (daman.CurrentY == 2)
				{
					if(daman.CurrentX == 1)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
					}
					if(daman.CurrentX == 3)
					{
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
					}
				}
				else if (daman.CurrentY == 4)
				{
					if(daman.CurrentX == 1)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
					}
					if(daman.CurrentX == 3)
					{
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
					}
				}
				else if (daman.CurrentX == 2)
				{
					if(daman.CurrentY == 3 || daman.CurrentY == 5)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
					}
					if(daman.CurrentY == 2 || daman.CurrentY == 4 || daman.CurrentY == 6)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
					}
				}
				else if(daman.CurrentY == 6)
				{
					if(daman.CurrentX == 1)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
					}
					if(daman.CurrentX == 3)
					{
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
					}
				}
				else if(daman.CurrentX == 4)
				{
					if(daman.CurrentY == 2)
					{
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
					}
					if(daman.CurrentY == 3)
					{
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
					}
					if(daman.CurrentY == 4)
					{
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY - 1);
						}
					}
					if(daman.CurrentY == 5)
					{
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
					}
					if(daman.CurrentY == 6)
					{
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
					}
				}
				else if(daman.CurrentX == 1)
				{
					if (daman.CurrentY == 3)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY + 1);
						}
					}
					if (daman.CurrentY == 5)
					{
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY))
						{
							r2[daman.CurrentX + 1, daman.CurrentY] = checkCondition(daman.CurrentX + 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX + 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX + 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX + 1, daman.CurrentY + 1);
						}
					}
				}
				else if(daman.CurrentX == 3)
				{
					if(daman.CurrentY == 3)
					{
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY + 1))
						{
							r2[daman.CurrentX, daman.CurrentY + 1] = checkCondition(daman.CurrentX, daman.CurrentY + 1);
						}
					}
					if(daman.CurrentY == 5)
					{
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY - 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY - 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY - 1);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY))
						{
							r2[daman.CurrentX - 1, daman.CurrentY] = checkCondition(daman.CurrentX - 1, daman.CurrentY);
						}
						if(checkCondition(daman.CurrentX - 1, daman.CurrentY + 1))
						{
							r2[daman.CurrentX - 1, daman.CurrentY + 1] = checkCondition(daman.CurrentX - 1, daman.CurrentY + 1);
						}
						if(checkCondition(daman.CurrentX, daman.CurrentY - 1))
						{
							r2[daman.CurrentX, daman.CurrentY - 1] = checkCondition(daman.CurrentX, daman.CurrentY - 1);
						}
					}
				}
			} 
		}
		
		return NormalizeDam (beforeX, beforeY);
	}
}
