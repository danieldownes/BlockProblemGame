VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   2070
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   2400
   LinkTopic       =   "Form1"
   ScaleHeight     =   2070
   ScaleWidth      =   2400
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtOut 
      Height          =   1455
      Left            =   0
      TabIndex        =   2
      Text            =   "Click 'Go!' to begin"
      Top             =   0
      Width           =   2415
   End
   Begin VB.TextBox txtTrys 
      Height          =   285
      Left            =   1320
      TabIndex        =   1
      Text            =   "Ready!"
      Top             =   1680
      Width           =   975
   End
   Begin VB.CommandButton cmdGo 
      Caption         =   "Go!"
      Height          =   495
      Left            =   0
      TabIndex        =   0
      Top             =   1560
      Width           =   1215
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Dim Board As clsBoard

Dim Piece(15) As clsPiece


'Dim dat(12, 3) As Integer

'Dim numof(12) As Integer

Dim ava(12, 4) As Boolean





Dim ok As Boolean

Private Function findOri(ori, offset)
    findOri = ori + offset
    If findOri > 3 Then findOri = findOri - 4
End Function


Private Sub cmdGo_Click()
    
retry:

    txtTrys.Text = Str(Val(txtTrys.Text) + 1)
    
    'reset/set avableity...
    For n = 0 To 12
        If n < 4 Or n > 9 Then
            snum = 0
            Do
                If snum < numof(n) Then
                    ava(n, snum) = True
                Else
                    ava(n, snum) = False
                End If
                    
                snum = snum + 1
            Loop Until snum > 4
        End If
    Next n
    

    'Find starting block type
    If Rnd > 0.5 Then bBlType = True Else bBlType = False
    

    For cy = 0 To 3
        For cx = 0 To 3
            
            bBlType = Not bBlType
            If bBlType Then
                Highest = 0
                Lowest = 2
            Else
                Highest = 10
                Lowest = 13
            End If
    
reran:          'Produce block number
            Board(cx, cy, 0) = Int((Highest - Lowest + 1) * Rnd + Lowest)
            
            ch = 0
            ok = False
            Do
                                
                If ch > 4 Then GoTo reran
                
                If ava(Board(cx, cy, 0), ch) = True Then
                    ok = True
                    ava(Board(cx, cy, 0), ch) = False
                End If
                
                ch = ch + 1
                
            Loop Until ok = True
            
            'MsgBox ("Out Of cage!")
            
            'Produce orientaion of block
            Board(cx, cy, 1) = Int((2 - 0 + 1) * Rnd)
            
            DoEvents
            Form1.Refresh
        Next cx
    Next cy
    
    
    
    'check for win
    For cy = 0 To 3
        For cx = 0 To 3
        
            If cx <> 3 Then
                If dat(Board(cx, cy, 0), findOri(1, Board(cx, cy, 0))) <> _
                dat(Board(cx + 1, cy, 0), findOri(3, Board(cx + 1, cy, 0))) Then
                       
                    GoTo retry
                
                End If
            End If
        
            If cy <> 3 Then
                If dat(Board(cx, cy, 0), findOri(1, Board(cx, cy, 0))) <> _
                dat(Board(cx, cy + 1, 0), findOri(3, Board(cx, cy + 1, 0))) Then
                    
                    GoTo retry
                    
                End If
            End If

        Next cx
    Next cy
            

    
    'Update display
    
    
    End
    
End Sub

Private Sub Form_Load()
'Load pieces

    Dim orr(3) As Integer
    
    'Get block data...
    Open App.Path & "/data.dat" For Input As #1
    
    For n = 0 To 12
    'MsgBox ("load")
        
        If n < 4 Or n > 9 Then
            Input #1, num, orr(0), orr(1), orr(2), orr(3)
            
            MsgBox (Str(num)) & Str(orr(0)) & Str(orr(1)) & Str(orr(2)) & Str(orr(3))
            
            numof(n) = num
        
            For p = 0 To 3
            
                dat(n, p) = orr(p)
            
            Next p
        
        End If
    
    Next n
    
    Close #1
    
End Sub
