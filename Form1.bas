Dim Board As New clsBoard

Dim Piece(15) As New clsPiece

Dim intCurPiece As Integer   ' Current Selected Piece


Sub SolveGame()

    
End Sub

Function CheckForWin() As Boolean

    Dim intX As Integer
    Dim intY As Integer
    Dim iP As Integer
    
    'getData(below) :
    ' 0 - intTop
    ' 1 - intBottom
    ' 2 - intLeft
    ' 3 - intRight
    ' 4 - blnType
    
    iP = 0
    
    For intX = 0 To 3
        For intY = 0 To 3
            
            'Horizontal scan
            Select Case intX
                                    
                Case 3
                    If Piece(iP).getData(3) = Piece(iP - 1).getData(2) Then CheckForWin = False: GoTo WIN_CHECK
                
                Case Else
                    If Piece(iP).getData(2) = Piece(iP + 1).getData(3) Then CheckForWin = False: GoTo WIN_CHECK
                                
            End Select
            
            'Vertical scan
            Select Case intY
                
                Case 3
                    If Piece(iP).getData(0) = Piece(iP - 4).getData(1) Then CheckForWin = False: GoTo WIN_CHECK
                
                Case Else
                    If Piece(iP).getData(1) = Piece(iP + 4).getData(0) Then CheckForWin = False: GoTo WIN_CHECK
                
            End Select
            
            iP = iP + 1
        Next intY
    Next intX
    
    CheckForWin = True

WIN_CHECK:
End Function

Sub UpDateGUI()
    
    Dim intX As Integer
    Dim intY As Integer
    Dim intTotal As Integer
    
    intTotal = 0
    
    For intX = 0 To 3
        For intY = 0 To 3
            
            'Sync data of the actual piece with its respective graphic
            setPieceDatType cusGPiece(intTotal), getPieceDatType(Piece(intTotal))
            
            intTotal = intTotal + 1
        Next intY
    Next intX
End Sub





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


Private Sub cusGPiece_Rotate(Index As Integer)
    Piece(Index).turnPiece bLeft
    UpDateGUI
End Sub

Private Sub cusGPiece_Selectt(Index As Integer)

    Dim cusTemp As cusPieceDataT
    
    'Is this the piece to move?
    If intCurPiece = -1 Then    '(Yes)
        
        'Select this piece
        intCurPiece = Index
        cusGPiece(Index).Selected = True
    
    Else    'Swap it with this piece
    
        'Swap this piece's data with other pre-selected piece's data
        cusTemp = getPieceDatType(Piece(Index))
        setPieceDatType Piece(Index), getPieceDatType(Piece(intCurPiece))
        setPieceDatType Piece(intCurPiece), cusTemp
        
        'Flag that no piece is selected (update GUI before-hand)
        cusGPiece(intCurPiece).Selected = False
        intCurPiece = -1
        
        
    
    End If
    
    'Update GUI
    UpDateGUI
End Sub

