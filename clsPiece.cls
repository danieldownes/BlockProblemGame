VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPiece"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
'Repersents a single piece on the borad


Option Explicit




Enum enuDirection
    bLeft = False  ' Anti-Clockwise
    bRight = True  ' Clockwise
End Enum



Dim clsData As cusPieceDataT


Sub turnPiece(Optional blnDirection As enuDirection = bLeft)
    
    Dim intN     As Integer
    Dim intTemp  As Integer
    
    'Select direction...
    If blnDirection = bLeft Then
        
        intTemp = clsData.intPieceDat(0)
        For intN = 1 To 3
            clsData.intPieceDat(intN - 1) = clsData.intPieceDat(intN)
        Next intN
        clsData.intPieceDat(3) = intTemp
    
    Else
        
        intTemp = clsData.intPieceDat(3)
        For intN = 3 To 1
            clsData.intPieceDat(intN + 1) = clsData.intPieceDat(intN)
        Next intN
        clsData.intPieceDat(0) = intTemp
        
    End If
    
End Sub


Sub setData(intTop As Integer, intBottom As Integer, intLeft As Integer, intRight As Integer, blnType As Boolean)
'Not called directly due to large number of arguments
'Use the 'getPieceDatType' helper function
    
    'Set the data of the four points
    clsData.intPieceDat(0) = intTop
    clsData.intPieceDat(1) = intBottom
    clsData.intPieceDat(2) = intLeft
    clsData.intPieceDat(3) = intRight
    
    'Set the Type
    clsData.blnType = blnType

End Sub

Function getData(intDataToReturen As Integer) As Variant
'Use the 'getPieceDatType' helper function

    'intDataToReturen:
    ' 0 - intTop
    ' 1 - intBottom
    ' 2 - intLeft
    ' 3 - intRight
    ' 4 - blnType

    Select Case intDataToReturen
    
        Case 0
            getData = clsData.intPieceDat(0)
            
        Case 1
            getData = clsData.intPieceDat(1)
            
        Case 2
            getData = clsData.intPieceDat(2)
            
        Case 3
            getData = clsData.intPieceDat(3)
            
        Case 4
            getData = clsData.blnType
        
    End Select
End Function
