using UnityEngine;
using System.Collections;

public abstract class Daman : MonoBehaviour
{
	public int CurrentX{ set; get;}
	public int CurrentY{ set; get;}
	public bool isWhite;

	public void SetPosition(int x, int y)
	{
		CurrentX = x;
		CurrentY = y;
	}
	public virtual bool[,] PossibleMove()
	{
		return new bool[5,9];
	}

	public virtual bool[,] TargetMove()
	{
		return new bool[5,9];
	}

	public virtual bool CheckTarget(int beforeX, int BeforeY){
		return new bool();
	}
}
