Imports System.Windows.Forms.Control
Imports System.Management
Imports Microsoft.Win32
Imports System.Security.Cryptography
Imports System.Text

Public Class BaseControl

#Region "Khai bao"
    Public normalFont As Font   'Font của label
    Public _aaLabel As String   ' Nhãn của textbox control
    Public _aaLabelWidth As Integer 'Độ rộng label
    Public _aaLeftMargin As Integer ' Lề trái
    Public _aaRightMargin As Integer    'Lề phải
    Public _aaBackLabel As Color    ' Màu nền label
    Public _aaForeColorLabel As Color   ' Màu chữ label
    Public _aaBorderLine As Boolean     ' Khung viền label
    Public _aaTopMargin As Integer      ' Lề trên
    Public _aaBottomMargin As Integer   ' Lề dưới
    Private _aaEnterNextControl As Boolean  ' Trạng thái Enter sẽ di chuyển focus qua control kế tiếp

#End Region

#Region "Tien ich"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        normalFont = New Font("Arial", Me.Font.Size, FontStyle.Regular)

        _aaLabel = "Label"
        _aaLabelWidth = 100
        Me._aaLeftMargin = 5
        Me._aaRightMargin = 7
        Me._aaForeColorLabel = Color.Black
        Me._aaBackLabel = Color.LightGray
        _aaBorderLine = False
        Me._aaBottomMargin = 5
        Me._aaTopMargin = 5
        _aaEnterNextControl = True
    End Sub

#End Region

#Region "Property"

    Public Property aaAcceptNextControl() As Boolean
        Get
            Return _aaEnterNextControl
        End Get
        Set(ByVal value As Boolean)
            _aaEnterNextControl = value
        End Set
    End Property

    Public Property aaLabel() As String
        Get
            Return _aaLabel
        End Get
        Set(ByVal value As String)
            _aaLabel = value
            Me.Invalidate()
        End Set
    End Property

    Public Overridable Property aaLabelWidth() As Integer
        Get
            Return _aaLabelWidth
        End Get
        Set(ByVal value As Integer)
            If value >= 5 Then
                _aaLabelWidth = value
            Else
                _aaLabelWidth = 0
            End If
            Me.Invalidate()
        End Set
    End Property

    Public Property aaForeColorLabel() As Color
        Get
            Return _aaForeColorLabel
        End Get
        Set(ByVal value As Color)
            _aaForeColorLabel = value
            Me.Invalidate()
        End Set
    End Property

    Private Property aaBackColorLabel() As Color
        Get
            Return _aaBackLabel
        End Get
        Set(ByVal value As Color)
            _aaBackLabel = value
            Me.Invalidate()
        End Set
    End Property

    Public Property aaBorderLine() As Boolean
        Get
            Return _aaBorderLine
        End Get
        Set(ByVal value As Boolean)
            _aaBorderLine = value
            Me.Invalidate()
        End Set
    End Property

#End Region

#Region "Sub & function"

    Friend Overridable Sub setWidth(ByVal ctrl As Control)
        ctrl.Width = (Me.Width - Me.aaLabelWidth - Me._aaRightMargin)
        Me.Invalidate()
    End Sub

    Friend Overridable Sub setLocation(ByVal ctrl As Control)
        Dim x As Integer = Me.aaLabelWidth + Me._aaLeftMargin
        Dim y As Integer = Convert.ToInt32(CDbl(((CDbl(Me.Height) / 2) - (CDbl(ctrl.Height) / 2))))
        Dim point As New Point(x, y)
        ctrl.Location = point
        Me.Invalidate()
    End Sub

    Friend Sub SetNextControl(ByVal ctrl As Control)
        ctrl.FindForm.SelectNextControl(ctrl, True, True, True, True)
    End Sub

#End Region

#Region "Event"

    Private Sub BaseControl_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Try
            Dim g As Graphics
            g = e.Graphics

            g.DrawRectangle(Pens.DimGray, aaLabelWidth, 0, Me.Width - aaLabelWidth - 1, Me.Height - 1)

            'Tô màu bắt đầu từ LabelWidth trở đi
            Dim rect1 As New Rectangle(aaLabelWidth + 1, 1, Me.Width - aaLabelWidth - 2, Me.Height - 2)
            Dim brush1 As New System.Drawing.Drawing2D.LinearGradientBrush(rect1, Color.White, Color.White, Drawing2D.LinearGradientMode.Vertical)
            g.FillRectangle(brush1, rect1)

            'Vẽ hình chữ nhật bao quanh Caption
            If aaBorderLine = True Then
                g.DrawRectangle(Pens.DimGray, 0, 0, aaLabelWidth, Me.Height - 1)
            End If
            'Ve caption 
            If aaLabelWidth > 0 Then
                g.DrawString(aaLabel, normalFont, New SolidBrush(Me._aaForeColorLabel), 5.0!, Me.Height / 2 - 8)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
