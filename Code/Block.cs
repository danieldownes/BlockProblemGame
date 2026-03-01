using System;

public enum Colour
{
	Red = 0,
	Green = 1,
	Blue = 2,
	Yellow = 3
}

public enum Direction
{
	Top,
	Right,
	Bottom,
	Left
}

///<summary>
/// DTO to define a block. This is a piece on the board grid.
/// The Top, Bottom, Left and Right edges have colour indicators.
/// The Middle contains a blank or Diamond, denoted false / true respectively.
///</summary>
public class Block
{
	public Colour T;
	public Colour B;
	public Colour L;
	public Colour R;
	public bool M;

	public Block(Colour top, Colour right, Colour bottom, Colour left, bool middle)
	{
		T = top;
		B = bottom;
		L = left;
		R = right;
		M = middle;
	}

	/// <summary>
	/// Access an edge colour by direction.
	/// </summary>
	public Colour this[Direction d]
	{
		get => d switch
		{
			Direction.Top => T,
			Direction.Right => R,
			Direction.Bottom => B,
			Direction.Left => L,
			_ => throw new ArgumentOutOfRangeException(nameof(d))
		};
		set
		{
			switch (d)
			{
				case Direction.Top: T = value; break;
				case Direction.Right: R = value; break;
				case Direction.Bottom: B = value; break;
				case Direction.Left: L = value; break;
				default: throw new ArgumentOutOfRangeException(nameof(d));
			}
		}
	}

	public void TurnClockwise()
	{
		Colour t = T;
		T = L;
		L = B;
		B = R;
		R = t;
	}

	public void TurnAntiClockwise()
	{
		Colour t = T;
		T = R;
		R = B;
		B = L;
		L = t;
	}
}
