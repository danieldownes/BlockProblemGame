'Ex-D Software Development(TM)
'All Rights Reserved (c)2001

'Option Explicit

Public Event Click()

Public Event Viewing()
Public Event Hovering()
Public Event Clicking()

Private Type POINTAPI
    x As Integer
    y As Integer
End Type


Private ppiOldPoint  As POINTAPI
Private blnMouseMove As Boolean
Private blnHover     As Boolean
Private blnClicking  As Boolean


Function ViewingPic(imgImage As IPictureDisp)
    imgPic(0).Picture = imgImage
End Function

Function HoverPic(imgImage As IPictureDisp)
    imgPic(1).Picture = imgImage
End Function

Function ClickPic(imgImage As IPictureDisp)
    imgPic(2).Picture = imgImage
End Function

Sub UpDate()
    Dim ppiNewPoint As POINTAPI
    
    GetCursorPos ppiNewPoint

    If ppiOldPoint.x <> ppiNewPoint.x And ppiOldPoint.y <> ppiNewPoint.y Then
        blnMouseMove = True
    Else
        blnMouseMove = False
    End If
    
    DoEvents
    
    ppiOldPoint.x = ppiNewPoint.x
    ppiOldPoint.y = ppiNewPoint.y
    'Debug.Print ppiOldPoint.Y
    'Debug.Print "blnMouseMove: " & blnMouseMove & " blnHover: " & blnHover & " blnClicking: " & blnClicking
    
    'Hovering
    If blnMouseMove = True And blnHover = True Then
        imgPic(0).Visible = False
        imgPic(1).Visible = True
        imgPic(2).Visible = False
        
        blnHover = False
        
        RaiseEvent Hovering
        
        'Debug.Print "Hovering"
        
        Exit Sub
    End If
    
    'Not Hovering (Viewing)
    If blnMouseMove = True And blnHover = False Then
        imgPic(0).Visible = True
        imgPic(1).Visible = False
        imgPic(2).Visible = False
        
        RaiseEvent Viewing
        
        'Debug.Print "View"
    End If
    
    
    DoEvents
    
    'Clicking
    If blnHover = True And blnClicking = True Then
        imgPic(0).Visible = False
        imgPic(1).Visible = False
        imgPic(2).Visible = True
        
        RaiseEvent Clicking
    End If
End Sub

Private Sub imgPic_MouseDown(Index As Integer, Button As Integer, Shift As Integer, x As Single, y As Single)
    blnClicking = True
End Sub

Private Sub imgPic_MouseMove(Index As Integer, Button As Integer, Shift As Integer, x As Single, y As Single)
    blnHover = True
End Sub

Private Sub imgPic_MouseUp(Index As Integer, Button As Integer, Shift As Integer, x As Single, y As Single)
    If blnHover = True Then RaiseEvent Click
    blnClicking = False
End Sub







Private Sub UserControl_Initialize()
    Dim intN As Integer
    
    For intN = 0 To 2
        With imgPic(intN)
            .Visible = False
            .Top = -1
            .Left = -1
        End With
    Next intN
    imgPic(0).Visible = True
End Sub

Private Sub UserControl_Resize()
    Dim intN As Integer
    
    For intN = 0 To 2
        imgPic(intN).Width = UserControl.Width + 1
        imgPic(intN).Height = UserControl.Height + 1
    Next intN
    
End Sub