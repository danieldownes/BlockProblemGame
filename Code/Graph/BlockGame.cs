using System;

public class BlockGame
{
    public List<Block> blocks;

    public void Init()
    {
        blocks = new List<Block>();
        populateFromGridData();

		Console.WriteLine(blocks[2].B);
    }

    private void populateFromGridData()
    {
        var data = GridData.Data;

        for (int i = 0; i < data.GetLength(0); i++)
        {
            int total = data[i, 0];

            blocks.Add(new Block(
                data[i, 1],
                data[i, 2],
                data[i, 3],
                data[i, 4],
                data[i, 5] == 1));
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
