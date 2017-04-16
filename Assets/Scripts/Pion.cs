using UnityEngine;
using System.Collections;

public class Pion : Daman
{
	bool[,] r = new bool[5, 9];
	bool[,] r2 = new bool[5, 9];
	
	public override bool[,] PossibleMove ()
	{
		reset ();
		
		GameObject[] whitepion = GameObject.FindGameObjectsWithTag ("whitepion");
		GameObject[] redpion = GameObject.FindGameObjectsWithTag ("redpion");
		
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
					r [CurrentX - 1, CurrentY] = checkCondition (CurrentX - 1, CurrentY);
					r [CurrentX + 1, CurrentY] = checkCondition (CurrentX + 1, CurrentY);
					r [CurrentX, CurrentY + 1] = checkCondition (CurrentX, CurrentY + 1);
					r [CurrentX, CurrentY - 1] = checkCondition (CurrentX, CurrentY - 1);
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
	
	public override bool[,] TargetMove ()
	{
		return r2;
	}
	
	public override bool CheckTarget (int beforeX, int BeforeY)
	{
		reset2 ();
		
		GameObject[] whitepion = GameObject.FindGameObjectsWithTag ("whitepion");
		GameObject[] redpion = GameObject.FindGameObjectsWithTag ("redpion");
		
		if (isWhite) {
			return checktarget (redpion, CurrentX, CurrentY, beforeX, BeforeY);
		} else {
			return checktarget (whitepion, CurrentX, CurrentY, beforeX, BeforeY);
		}
	}
	
	private bool NormalizeDam (int beforeX, int beforeY)
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
	
	public void reset ()
	{
		for (int i = 0; i<5; i++) 
		{
			for (int j = 0; j<9; j++) 
			{
				r [i, j] = false;
			}
		}
	}
	
	public void reset2 ()
	{
		for (int i = 0; i<5; i++) 
		{
			for (int j = 0; j<9; j++) 
			{
				r2 [i, j] = false;
			}
		}
	}
	
	private bool checkCondition (int x, int y)
	{
		
		GameObject[] whitepion = GameObject.FindGameObjectsWithTag ("whitepion");
		GameObject[] redpion = GameObject.FindGameObjectsWithTag ("redpion");
		
		foreach (GameObject pion in whitepion) 
		{
			Pion daman = pion.GetComponent ("Pion") as Pion;
			
			if (daman.CurrentX == x && daman.CurrentY == y) 
			{
				return false;
			}
		}
		
		foreach (GameObject pion in redpion) 
		{
			Pion daman = pion.GetComponent ("Pion") as Pion;
			
			if (daman.CurrentX == x && daman.CurrentY == y) 
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
			
			
			
			if (CurrentY < 2) 
			{
				if (CurrentY == 1)
				{
					if (CurrentX == 1) 
					{
						if (daman.CurrentX == CurrentX + 1 && daman.CurrentY == CurrentY + 1)
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY + 1] = checkCondition (daman.CurrentX + 1, daman.CurrentY + 1);
							}
						}
						
						if (daman.CurrentX == CurrentX + 1 && daman.CurrentY == CurrentY) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY))
							{
								r2 [daman.CurrentX + 1, daman.CurrentY] = checkCondition (daman.CurrentX + 1, daman.CurrentY);
							}
						}
					} 
					else if (CurrentX == 2) 
					{
						if (daman.CurrentX == CurrentX && daman.CurrentY == CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX, daman.CurrentY + 1] = checkCondition (daman.CurrentX, daman.CurrentY + 1);
							}
						}
					} 
					else if (CurrentX == 3) 
					{
						if (daman.CurrentX == CurrentX - 1 && daman.CurrentY == CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY + 1] = checkCondition (daman.CurrentX - 1, daman.CurrentY + 1);
							}
						}
						if (daman.CurrentX == CurrentX - 1 && daman.CurrentY == CurrentY) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY] = checkCondition (daman.CurrentX - 1, daman.CurrentY);
							}
						}
					}
				} 
				else if (CurrentY == 0) 
				{
					if (CurrentX == 0) 
					{
						if (daman.CurrentX == CurrentX + 1 && daman.CurrentY == CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY + 1] = checkCondition (daman.CurrentX + 1, daman.CurrentY + 1);
							}
						}
						if (daman.CurrentX == CurrentX + 2 && daman.CurrentY == CurrentY) 
						{
							if (checkCondition (daman.CurrentX + 2, daman.CurrentY))
							{
								r2 [daman.CurrentX + 2, daman.CurrentY] = checkCondition (daman.CurrentX + 2, daman.CurrentY);
							}
						}
					}
					else if (CurrentX == 2) 
					{
						if (daman.CurrentX == CurrentX && daman.CurrentY == CurrentY + 1) 
						{
							if (checkCondition (daman.CurrentX, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX, daman.CurrentY] = checkCondition (daman.CurrentX, daman.CurrentY);
							}
						}
					} 
					else if (CurrentX == 4) 
					{
						if (daman.CurrentX == CurrentX - 1 && daman.CurrentY == CurrentY + 1)
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY + 1)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY + 1] = checkCondition (daman.CurrentX - 1, daman.CurrentY + 1);
							}
						}
						if (daman.CurrentX == CurrentX - 2 && daman.CurrentY == CurrentY)
						{
							if (checkCondition (daman.CurrentX - 2, daman.CurrentY))
							{
								r2 [daman.CurrentX - 2, daman.CurrentY] = checkCondition (daman.CurrentX - 2, daman.CurrentY);
							}
						}
					}
				}
			} 
			else if (CurrentY > 6) 
			{
				if (CurrentY == 7) 
				{
					if (CurrentX == 3) 
					{
						if (daman.CurrentX == CurrentX - 1 && daman.CurrentY == CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY - 1)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY - 1] = checkCondition (daman.CurrentX - 1, daman.CurrentY - 1);
							}
						}
						if (daman.CurrentX == CurrentX - 1 && daman.CurrentY == CurrentY) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY)) 
							{
								r2 [daman.CurrentX - 1, daman.CurrentY] = checkCondition (daman.CurrentX - 1, daman.CurrentY);
							}
						}
					} 
					else if (CurrentX == 2) 
					{
						if (daman.CurrentX == CurrentX && daman.CurrentY == CurrentY - 1) {
							if (checkCondition (daman.CurrentX, daman.CurrentY - 1)) {
								r2 [daman.CurrentX, daman.CurrentY - 1] = checkCondition (daman.CurrentX, daman.CurrentY - 1);
							}
						}
					}
					else if (CurrentX == 1) 
					{
						if (daman.CurrentX == CurrentX + 1 && daman.CurrentY == CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY - 1)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY - 1] = checkCondition (daman.CurrentX + 1, daman.CurrentY - 1);
							}
						}
						if (daman.CurrentX == CurrentX + 1 && daman.CurrentY == CurrentY) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY] = checkCondition (daman.CurrentX + 1, daman.CurrentY);
							}
						}
					}
				} 
				else if (CurrentY == 8) 
				{
					if (CurrentX == 4) 
					{
						if (daman.CurrentX == CurrentX - 1 && daman.CurrentY == CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX - 1, daman.CurrentY - 1))
							{
								r2 [daman.CurrentX - 1, daman.CurrentY - 1] = checkCondition (daman.CurrentX - 1, daman.CurrentY - 1);
							}
						}
						if (daman.CurrentX == CurrentX - 2 && daman.CurrentY == CurrentY)
						{
							if (checkCondition (daman.CurrentX - 2, daman.CurrentY))
							{
								r2 [daman.CurrentX - 2, daman.CurrentY] = checkCondition (daman.CurrentX - 2, daman.CurrentY);
							}
						}
					} 
					else if (CurrentX == 2)
					{
						if (daman.CurrentX == CurrentX && daman.CurrentY == CurrentY - 1)
						{
							if (checkCondition (daman.CurrentX, daman.CurrentY - 1))
							{
								r2 [daman.CurrentX, daman.CurrentY - 1] = checkCondition (daman.CurrentX, daman.CurrentY - 1);
							}
						}
						
					} 
					else if (CurrentX == 0) 
					{
						if (daman.CurrentX == CurrentX + 1 && daman.CurrentY == CurrentY - 1) 
						{
							if (checkCondition (daman.CurrentX + 1, daman.CurrentY - 1)) 
							{
								r2 [daman.CurrentX + 1, daman.CurrentY - 1] = checkCondition (daman.CurrentX + 1, daman.CurrentY - 1);
							}
						}
						
						if (daman.CurrentX == CurrentX + 2 && daman.CurrentY == CurrentY)
						{
							if (checkCondition (daman.CurrentX + 2, daman.CurrentY)) 
							{
								r2 [daman.CurrentX + 2, daman.CurrentY] = checkCondition (daman.CurrentX + 2, daman.CurrentY);
							}
						}
					}
				}
				else if (CurrentX == 0)
				{
					if(CurrentY == 2)
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
					if(CurrentY == 3)
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
					if(CurrentY == 5)
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
					if(CurrentY == 4)
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
					if(CurrentY == 6)
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
				else if (CurrentY == 2)
				{
					if(CurrentX == 1)
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
					if(CurrentX == 3)
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
				else if (CurrentY == 4)
				{
					if(CurrentX == 1)
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
					if(CurrentX == 3)
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
				else if (CurrentX == 2)
				{
					if(CurrentY == 3 || CurrentY == 5)
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
					if(CurrentY == 2 || CurrentY == 4 || CurrentY == 6)
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
				else if(CurrentY == 6)
				{
					if(CurrentX == 1)
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
					if(CurrentX == 3)
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
				else if(CurrentX == 4)
				{
					if(CurrentY == 2)
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
					if(CurrentY == 3)
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
					if(CurrentY == 4)
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
					if(CurrentY == 5)
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
					if(CurrentY == 6)
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
				else if(CurrentX == 1)
				{
					if (CurrentY == 3)
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
					if (CurrentY == 5)
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
				else if(CurrentX == 3)
				{
					if(CurrentY == 3)
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
					if(CurrentY == 5)
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