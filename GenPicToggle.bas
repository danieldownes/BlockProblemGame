'Ex-D Software Development(TM)
'All Rights Reserved (c)2001

Option Explicit

Public Event ValueChange(blnValue As Boolean)

Dim conBlnValue As Boolean


Property Let Value(blnValue As Boolean)
    conBlnValue = blnValue
    
    upDateCon
    
    PropertyChanged "Value"
End Property
Property Get Value() As Boolean
    Value = conBlnValue
End Property



Function NormalPic(picNorm As IPictureDisp)
    picState(0).Picture = picNorm
End Function

Function SelectedPic(picSel As IPictureDisp)
    picState(1).Picture = picSel
End Function

Private Sub upDateCon()
    'Swap pictures based on value
    If conBlnValue = True Then
        picState(0).Visible = False
        picState(1).Visible = True
    Else
        picState(0).Visible = True
        picState(1).Visible = False
    End If
End Sub








Private Sub picState_Click(Index As Integer)
    conBlnValue = Not conBlnValue
    upDateCon
    
    RaiseEvent ValueChange(conBlnValue)
End Sub


Private Sub UserControl_Initialize()
    conBlnValue = False
End Sub

Private Sub UserControl_Resize()
    Dim intN As Integer
    
    For intN = 0 To 1
        picState(intN).Width = UserControl.Width
        picState(intN).Height = UserControl.Height
    Next intN
    
End Sub
