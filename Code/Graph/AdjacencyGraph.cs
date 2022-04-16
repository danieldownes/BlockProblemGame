using Godot;
using System;

public class Grid
{
	public Block[,] Map;

	public void Init(int width, int height)
	{
		//Map = new Map int[width, height];
	}
	
}



/*
Private Sub Form_Load()
	'Load pieces...

	
	Dim intNum    As Integer
	Dim intTotal  As Integer
	Dim intN      As Integer
	Dim cusTemp   As cusPieceDataT
	Dim intTemp   As Integer
	
	intTotal = 0
	
	'Get block data...
	Open App.Path & "/data.dat" For Input As #1

	Do

		Input #1, intNum, cusTemp.intPieceDat(0), cusTemp.intPieceDat(1), cusTemp.intPieceDat(2), cusTemp.intPieceDat(3), intTemp
		
		cusTemp.blnType = intTemp
		
		For intN = 1 To intNum
			setPieceDatType Piece(intTotal), cusTemp
			
			'Overall number of pieces
			intTotal = intTotal + 1
		Next intN

MsgBox Str(intTotal)

	Loop Until EOF(1)
	Close #1
	
'    'Set all piece numbers
'    Board.ReSetPlaces
	
	intCurPiece = -1
	
	UpDateGUI

End Sub

*/
