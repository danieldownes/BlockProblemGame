VERSION 5.00
Begin VB.UserControl conGenPicToggle 
   ClientHeight    =   3600
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   4800
   ScaleHeight     =   3600
   ScaleWidth      =   4800
   Begin VB.PictureBox picState 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   1095
      Index           =   1
      Left            =   0
      ScaleHeight     =   1095
      ScaleWidth      =   975
      TabIndex        =   1
      Top             =   0
      Width           =   975
   End
   Begin VB.PictureBox picState 
      Appearance      =   0  'Flat
      BackColor       =   &H80000005&
      BorderStyle     =   0  'None
      ForeColor       =   &H80000008&
      Height          =   1215
      Index           =   0
      Left            =   0
      ScaleHeight     =   1215
      ScaleWidth      =   1095
      TabIndex        =   0
      Top             =   0
      Width           =   1095
   End
End
Attribute VB_Name = "conGenPicToggle"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
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
