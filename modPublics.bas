Attribute VB_Name = "modPublics"
Option Explicit


Enum enuColour
    R = 0
    G
    B
    Y
End Enum

Enum enuLocation
    eTop = 0
    eBottom
    eLeft
    eRight
End Enum



Type cusPieceDataT
    blnType        As Boolean    ' Dimond(True) or Arrows(Flase)?
    intPieceDat(3) As enuColour  ' Use 'enuLocation' to return/set the 'enuColour' of that location
End Type


Sub setPieceDatType(objTo As Object, cusData As cusPieceDataT)
    Dim intTop As Integer, intBottom As Integer, intLeft As Integer, intRight As Integer
    Dim blnType As Boolean
    
    'Set points
    intTop = cusData.intPieceDat(0)
    intBottom = cusData.intPieceDat(1)
    intLeft = cusData.intPieceDat(2)
    intRight = cusData.intPieceDat(3)
    
    'Set the Type
    blnType = cusData.blnType
        
    objTo.setData intTop, intBottom, intLeft, intRight, blnType

End Sub

Function getPieceDatType(objFrom As Object) As cusPieceDataT
    
    Dim intN As Integer
        
    'Get points
    For intN = 0 To 3
        getPieceDatType.intPieceDat(intN) = objFrom.getData(intN)
    Next intN
    
    'Get the Type
    getPieceDatType.blnType = objFrom.getData(4)
    
End Function

