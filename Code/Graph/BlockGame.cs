using System;
using System.Collections.Generic;

public class BlockGame
{
    public List<Block> blocks;

    public void Init()
    {
        blocks = new List<Block>();
        populateFromGridData();

        var graph = new AdjacencyGraph();
        graph.Init();

        graph.AddBlock(NodeType.BlockCentreDiamond);
        graph.AddBlock(NodeType.BlockCentreDiamond);
        graph.AddBlock(NodeType.BlockCentreArrow);
        graph.AddBlock(NodeType.BlockCentreArrow);
    }

    private void populateFromGridData()
    {
        foreach (var type in BlockTypes.Data)
        {
            for (int j = 0; j < type.Count; j++)
            {
                blocks.Add(new Block(type.Top, type.Right, type.Bottom, type.Left, type.HasDiamond));
            }
        }
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
