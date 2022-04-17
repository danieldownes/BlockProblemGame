using System;

///<summery>
/// DTO to define a block. This is a piece on the board grid.
/// The Top, Bottom, Left and Right edges have colour indicators.
/// The Middle contains a blank or Dimond, denoted false / true respectively.
///</summery>
public class Block
{
	public int T;
	public int B;
	public int L;
	public int R;
	public bool M;
	
	public Block(int top, int right, int bottom, int left, bool middle)
	{
		T = top;
		B = bottom;
		L = left;
		R = right;
		M = middle;
	}
	
	public void TurnClockwise()
	{
		int t = T;
		T = L;
		L = B;
		B = R;
		R = t;
	}
	
	public void TurnAntiClockwise()
	{
		int t = T;
		T = R;
		R = B;
		B = L;
		L = t;
	}
}