VERSION 5.00
Begin VB.UserControl cusGuiPiece 
   ClientHeight    =   735
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   735
   ScaleHeight     =   49
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   49
   Begin VB.PictureBox picNotSelected 
      Height          =   255
      Left            =   480
      Picture         =   "cusGuiPiece.ctx":0000
      ScaleHeight     =   195
      ScaleWidth      =   195
      TabIndex        =   1
      Top             =   0
      Visible         =   0   'False
      Width           =   255
   End
   Begin VB.PictureBox picSelected 
      Height          =   255
      Left            =   0
      Picture         =   "cusGuiPiece.ctx":1C96
      ScaleHeight     =   195
      ScaleWidth      =   195
      TabIndex        =   0
      Top             =   0
      Visible         =   0   'False
      Width           =   255
   End
   Begin VB.Shape shpType 
      BackColor       =   &H000080FF&
      BackStyle       =   1  'Opaque
      BorderColor     =   &H0080C0FF&
      BorderStyle     =   6  'Inside Solid
      Height          =   195
      Left            =   270
      Shape           =   3  'Circle
      Top             =   270
      Width           =   195
   End
   Begin VB.Line linGUI 
      BorderWidth     =   5
      Index           =   2
      X1              =   24
      X2              =   24
      Y1              =   32
      Y2              =   48
   End
   Begin VB.Line linGUI 
      BorderColor     =   &H00000000&
      BorderWidth     =   5
      Index           =   0
      X1              =   24
      X2              =   24
      Y1              =   16
      Y2              =   0
   End
   Begin VB.Line linGUI 
      BorderWidth     =   5
      Index           =   1
      X1              =   32
      X2              =   48
      Y1              =   24
      Y2              =   24
   End
   Begin VB.Line linGUI 
      BorderWidth     =   5
      Index           =   3
      X1              =   16
      X2              =   0
      Y1              =   24
      Y2              =   24
   End
End
Attribute VB_Name = "cusGuiPiece"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
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
