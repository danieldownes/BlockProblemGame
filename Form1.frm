VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form frmMain 
   Caption         =   "Block Puzzel"
   ClientHeight    =   3855
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4665
   Icon            =   "Form1.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   3855
   ScaleWidth      =   4665
   StartUpPosition =   3  'Windows Default
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   3840
      Top             =   1920
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      MaskColor       =   12632256
      _Version        =   393216
   End
   Begin BlockGame.conGenPicButton conGenPicButton1 
      Height          =   375
      Left            =   3600
      TabIndex        =   18
      Top             =   3000
      Width           =   855
      _ExtentX        =   1508
      _ExtentY        =   661
   End
   Begin BlockGame.conGenPicToggle cusTogRotRight 
      Height          =   375
      Left            =   3600
      TabIndex        =   17
      Top             =   960
      Width           =   855
      _ExtentX        =   1508
      _ExtentY        =   661
   End
   Begin BlockGame.conGenPicToggle cusTogRotLeft 
      Height          =   375
      Left            =   3600
      TabIndex        =   16
      Top             =   480
      Width           =   855
      _ExtentX        =   1508
      _ExtentY        =   661
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   12
      Left            =   480
      TabIndex        =   12
      Top             =   2640
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   13
      Left            =   1200
      TabIndex        =   13
      Top             =   2640
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   14
      Left            =   1920
      TabIndex        =   14
      Top             =   2640
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   15
      Left            =   2640
      TabIndex        =   15
      Top             =   2640
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   8
      Left            =   480
      TabIndex        =   8
      Top             =   1935
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   9
      Left            =   1200
      TabIndex        =   9
      Top             =   1935
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   10
      Left            =   1920
      TabIndex        =   10
      Top             =   1935
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   11
      Left            =   2640
      TabIndex        =   11
      Top             =   1935
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   4
      Left            =   480
      TabIndex        =   4
      Top             =   1200
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   5
      Left            =   1200
      TabIndex        =   5
      Top             =   1200
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   6
      Left            =   1920
      TabIndex        =   6
      Top             =   1200
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   7
      Left            =   2640
      TabIndex        =   7
      Top             =   1200
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   0
      Left            =   480
      TabIndex        =   0
      Top             =   480
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   1
      Left            =   1200
      TabIndex        =   1
      Top             =   480
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   2
      Left            =   1920
      TabIndex        =   2
      Top             =   480
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
   Begin BlockGame.cusGuiPiece cusGPiece 
      Height          =   765
      Index           =   3
      Left            =   2640
      TabIndex        =   3
      Top             =   480
      Width           =   765
      _ExtentX        =   1349
      _ExtentY        =   1349
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

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

