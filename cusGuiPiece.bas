Option Explicit

Dim intButton As Integer  ' Mouse button that was clicked
Dim blnState  As Integer  ' Selected (or not)

Public Event Click(intButton As Integer)
Public Event Selectt()
Public Event Rotate()

Sub setData(intTop As Integer, intBottom As Integer, intLeft As Integer, intRight As Integer, blnType As Boolean)
'Not called directly due to large number of arguments
'Use the 'getPieceDatType' helper function
    
    Dim intN As Integer
    Dim vbColours(3) As Long
    
    vbColours(0) = vbRed
    vbColours(1) = vbGreen
    vbColours(2) = vbBlue
    vbColours(3) = vbYellow
    
    'Set the colours of the four points
    linGUI(0).BorderColor = vbColours(intTop)
    linGUI(1).BorderColor = vbColours(intBottom)
    linGUI(2).BorderColor = vbColours(intLeft)
    linGUI(3).BorderColor = vbColours(intRight)
    
    'Set Type
    shpType.Visible = blnType
    
End Sub


Private Sub UserControl_Click()
    RaiseEvent Click(intButton)
    
    'Select or rotate
    If intButton = vbLeftButton Then  ' Select
    
        RaiseEvent Selectt
        
    Else  ' Rotate
    
        RaiseEvent Rotate
        
    End If
    
    intButton = 0
End Sub



Private Sub UserControl_Initialize()
    Selected = False
End Sub

Private Sub UserControl_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    intButton = Button
End Sub

Property Let Selected(bState As Boolean)
    If bState = True Then
        UserControl.Picture = picSelected.Picture
    
    Else
        UserControl.Picture = picNotSelected.Picture
    
    End If
    
    blnState = bState
End Property
Property Get Selected() As Boolean
    Selected = blnState
End Property
