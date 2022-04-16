Option Explicit

'Dim intBoard(3, 3) As Integer  ' Is a 4by4 matrix of the borads layout, holding the
'                               '  piece number for each element.


Private Type cus2dCordsT
    intX As Integer
    intY As Integer
End Type

''functo
'
'Sub clsBoardSwapPieces(intPieceNum1 As Integer, intPieceNum2 As Integer)
'
'    Dim cusPiecePos1 As cus2dCordsT
'    Dim cusPiecePos2 As cus2dCordsT
'
'    Dim intTemp      As Integer
'
'
''    'Find locations of these pieces
''    cusPiecePos1 = FindLocationOfPiece(intPieceNum1)
''    cusPiecePos2 = FindLocationOfPiece(intPieceNum2)
'
'    'Swap their data
'
'
''    intTemp = intBoard(cusPiecePos1.intX, cusPiecePos1.intY)
''    intBoard(cusPiecePos1.intX, cusPiecePos1.intY) = intBoard(cusPiecePos2.intX, cusPiecePos2.intY)
''    intBoard(cusPiecePos2.intX, cusPiecePos2.intY) = intTemp
'
'End Sub


'Private Function FindLocationOfPiece(intPieceNum) As cus2dCordsT
''Used in:  clsBoard.clsBoardSwapPieces
'
'    'Dim cusRet As cus2dCordsT
'
'
'    For FindLocationOfPiece.intX = 0 To 3
'        For FindLocationOfPiece.intY = 0 To 3
'            If intBoard(FindLocationOfPiece.intX, FindLocationOfPiece.intY) = intPieceNum Then
'
'                Exit Function
'
'            End If
'        Next FindLocationOfPiece.intY
'    Next FindLocationOfPiece.intX
'End Function

'Sub ReSetPlaces()
'
'    Dim intX     As Integer
'    Dim intY     As Integer
'    Dim intTotal As Integer
'
'    For intX = 0 To 3
'        For intY = 0 To 3
'
'            SetPieceNumber intTotal, intX, intY
'
'            intTotal = intTotal + 1
'        Next intY
'    Next intX
'End Sub

'Sub SetPieceNumber(intPiece As Integer, intX As Integer, intY As Integer)
'    intBoard(intX, intY) = intPiece
'End Sub
'Function ReturnPieceNumber(intX As Integer, intY As Integer) As Integer
'    ReturnPieceNumber = intBoard(intX, intY)
'End Function
Private Sub Class_Initialize()

End Sub
