Imports System.Threading.Thread
<System.ComponentModel.DefaultEvent("aaTextChanged")>
Public Class CtrlTextbox
    Inherits BaseControl

#Region "Khai bao"

    Private _aaSelectAllFocus As Boolean
    Private _aaReadOnly As Boolean
    Private _aaMultiline As Boolean
    Private _aaText As String
    Private _aaTextAlign As HorizontalAlignment
    Private _aaOnlyNumber As Boolean
    Private _aaEnter As Boolean
    Private _aaLabelSize As Integer
    Public Event aaTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Private _aaCustomFormat As String = "##,##0.########"

    Dim decimalString As String = CurrentThread.CurrentCulture.NumberFormat.PercentDecimalSeparator     'Lấy ra định dạng dấu thập phân( thường là dấu chấm ".")
    Dim DinhDangHangNgan As String = CurrentThread.CurrentCulture.NumberFormat.PercentGroupSeparator    'Lấy ra định dạng hàng ngàn(thường là dấu phẩy)

    Dim decimalChar As Char = Convert.ToChar(decimalString)
    Dim DauTru As String = "-"
    Dim decimalDauTru As Char = Convert.ToChar(DauTru)
    Dim PosDauTru As Integer = -1
    Dim PosDauCham As Integer = -1
    Dim isTextChange As Boolean = False

    Dim indexSelectionStart As Integer = 0
    Dim KySo As String = "0"

#End Region

#Region "Tien ich"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _aaSelectAllFocus = True
        _aaReadOnly = False
        _aaText = "TextBox"
        _aaTextAlign = HorizontalAlignment.Left
        txtText.Text = "TextBox"
        txtText.Multiline = False
        _aaOnlyNumber = False
        _aaEnter = False
        _aaLabelSize = 9
        _aaCustomFormat = "##,##0.########"

    End Sub

#End Region

#Region "Property"

    Public Property aaCharacterCasing() As CharacterCasing
        Get
            Return txtText.CharacterCasing
        End Get
        Set(ByVal value As CharacterCasing)
            txtText.CharacterCasing = value
        End Set
    End Property

    Public Property aaCustomFormat() As String
        Get
            Return _aaCustomFormat
        End Get
        Set(ByVal value As String)
            _aaCustomFormat = value
        End Set
    End Property

    Public Property aaFontText() As Font
        Get
            Return txtText.Font
        End Get
        Set(ByVal value As Font)
            txtText.Font = value
        End Set
    End Property

    Public Property aaSelectAllFocus() As Boolean
        Get
            Return _aaSelectAllFocus
        End Get
        Set(ByVal value As Boolean)
            _aaSelectAllFocus = value
        End Set
    End Property

    Public Property aaTextAlign() As HorizontalAlignment
        Get
            Return Me.txtText.TextAlign
        End Get
        Set(ByVal value As HorizontalAlignment)
            Me.txtText.TextAlign = value
        End Set
    End Property

    Public Property aaReadOnly() As Boolean
        Get
            Return txtText.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            txtText.ReadOnly = value
            If value = True Then
                txtText.BackColor = Color.White
            End If
        End Set
    End Property

    Public Property aaText() As String
        Get
            If aaOnlyNumber = True Then
                If IsNumeric(txtText.Text) = True Then
                    Return txtText.Text.Replace(DinhDangHangNgan, "")
                Else
                    Return 0
                End If
            Else
                Return txtText.Text
            End If
        End Get
        Set(ByVal value As String)
            If aaOnlyNumber = True Then
                txtText.Text = IIf(IsNumeric(value) = True, Decimal.Parse(value).ToString(aaCustomFormat), 0)
            Else
                txtText.Text = value
            End If
        End Set
    End Property

    Public Property aaLabelSize() As Integer
        Get
            Return _aaLabelSize
        End Get
        Set(ByVal value As Integer)
            _aaLabelSize = value
            normalFont = New Font("Arial", value, FontStyle.Regular)
        End Set
    End Property

    Public Property aaMultiline() As Boolean
        Get
            Return txtText.Multiline
            Me.Invalidate()
        End Get
        Set(ByVal value As Boolean)
            txtText.Multiline = value
            If value = True Then
                Me.txtText.Height = (Me.Height - 2 - Me._aaTopMargin - Me._aaBottomMargin) ' tru di 2 bien tren va duoi
                Dim point2 As New Point(Me.aaLabelWidth + 2, (Me.Height - Me.txtText.Height) / 2)
                Me.txtText.Location = point2
                Me.txtText.ScrollBars = ScrollBars.Both
            End If
            If value = False Then
                Dim point3 As New Point(Me.aaLabelWidth + 2, (Me.Height - txtText.Height) / 2)
                Me.txtText.Location = point3
            End If
        End Set
    End Property

    Public Property aaOnlyNumber() As Boolean
        Get
            Return _aaOnlyNumber
        End Get
        Set(ByVal value As Boolean)
            _aaOnlyNumber = value
        End Set
    End Property

    Public Property aaForeColor() As Color
        Get
            Return txtText.ForeColor
        End Get
        Set(ByVal value As Color)
            txtText.ForeColor = value
        End Set
    End Property

    Public Overrides Property aaLabelWidth() As Integer
        Get
            Return MyBase.aaLabelWidth
            setWidth(Me.txtText)
            setLocation(Me.txtText)
        End Get
        Set(ByVal value As Integer)
            MyBase.aaLabelWidth = value
            setWidth(Me.txtText)
            setLocation(Me.txtText)
        End Set
    End Property
#End Region


#Region "Events"

    Private Sub CtrlTextbox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.setLocation(Me.txtText)
        Me.setWidth(Me.txtText)
        Me.txtText.BackColor = Color.White
    End Sub

    Private Sub CtrlTextBox_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.setWidth(Me.txtText)
        Me.setLocation(Me.txtText)
        If Me.txtText.Multiline Then
            Me.txtText.Height = (Me.Height - 2 - Me._aaTopMargin - Me._aaBottomMargin) ' tru di 2 bien tren va duoi
            Dim point2 As New Point(Me.aaLabelWidth + 2, (Me.Height - Me.txtText.Height) / 2)
            Me.txtText.Location = point2
            Me.txtText.ScrollBars = ScrollBars.Both
        End If
        Me.Invalidate()
    End Sub

    Private Sub txtText_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtText.Enter
        If _aaEnter = True Then
            If aaSelectAllFocus = True Then
                Me.txtText.SelectAll()
            Else
                Me.txtText.SelectionLength = 0
            End If
        End If
    End Sub

    Private Sub txtText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtText.TextChanged
        Try
            If aaOnlyNumber = True Then
                isTextChange = True
            End If
            RaiseEvent aaTextChanged(sender, e)
        Catch ex As Exception
            MessageBox.Show("Lỗi: txtText_TextChanged " & ex.Message)
        End Try
    End Sub

    Private Sub txtText_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtText.KeyPress
        RaiseEvent aaKeyPress(sender, e)
        Try
            If aaOnlyNumber = True Then

                If [Char].IsDigit(e.KeyChar) OrElse [Char].IsControl(e.KeyChar) Then
                    If txtText.Text.IndexOf(DauTru) <> -1 And txtText.SelectionStart <= txtText.Text.IndexOf(DauTru) Then
                        e.Handled = True
                    End If
                ElseIf e.KeyChar = decimalString AndAlso txtText.Text.IndexOf(decimalString) = -1 Or e.KeyChar = DauTru AndAlso txtText.Text.IndexOf(DauTru) = -1 Then
                    If (e.KeyChar = DauTru And txtText.Text.Length > 0 And txtText.SelectionStart <> 0) Then
                        e.Handled = True
                    ElseIf (e.KeyChar = decimalString And txtText.SelectionStart = 0) Or (e.KeyChar = decimalString And txtText.Text.IndexOf(DauTru) <> -1 And txtText.Text.IndexOf(DauTru) > txtText.SelectionStart) Then
                        e.Handled = True
                    ElseIf e.KeyChar = decimalString And txtText.Text.Length > 0 Then  'kiem tra dau cham "." ko dc sat ngay dau tru "-"
                        'do khi nhap dau cham "." vao nhung chua hien thi ra textbox nen ko lay dc vi tri cua dau cham

                        'lay ra vi tri cua dau tru "-"
                        PosDauTru = txtText.Text.IndexOf(DauTru)

                        'lay ra vi tri con tro
                        Dim PosConTro As Integer = txtText.SelectionStart

                        If PosConTro - PosDauTru = 1 Then
                            e.Handled = True
                        Else
                            e.Handled = False
                        End If
                    Else
                        e.Handled = False
                    End If

                Else
                    e.Handled = True    'khong cho them ky tu vao
                End If
                KySo = e.KeyChar
                indexSelectionStart = txtText.SelectionStart

            End If
        Catch ex As Exception
            'Throw ex
            MessageBox.Show("Lỗi:: Keypress " & ex.Message)
        End Try
    End Sub

    Private Sub txtText_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtText.KeyUp
        RaiseEvent aaKeyUp(sender, e)
        Try
            If aaOnlyNumber = True Then

                If isTextChange = True Then
                    'format số
                    'lay ra vi tri dau tru
                    PosDauTru = txtText.Text.IndexOf(DauTru)

                    'lay ra vi tri dau cham
                    PosDauCham = txtText.Text.IndexOf(decimalString)

                    If PosDauCham - PosDauTru = 0 Then  'ko co ca 2 dau cham va dau tru
                        'kiem tra so ky tu >=2 vi khi nhap vao ky tu thu 3 thi do chua hien thi ra text nen van hieu la 2
                        If txtText.Text.Length >= 2 Then
                            'txtText.Text = txtText.Text.Replace(",", "")
                            txtText.Text = txtText.Text.Replace(DinhDangHangNgan, "")

                            'txttext.Text = Decimal.Parse(txttext.Text).ToString("#,##0")
                            txtText.Text = Decimal.Parse(txtText.Text).ToString(aaCustomFormat)
                            'txttext.SelectionStart = txttext.Text.Length
                            txtText.SelectionStart = indexSelectionStart + 1
                            indexSelectionStart += 1
                            If txtText.Text.Replace(DinhDangHangNgan, "").Length > 3 And txtText.Text.Replace(DinhDangHangNgan, "").Length Mod 3 = 1 Then
                                txtText.SelectionStart = indexSelectionStart + 1
                                indexSelectionStart += 1

                            End If
                            If indexSelectionStart > txtText.Text.Length Then
                                indexSelectionStart = txtText.Text.Length
                            End If
                        End If
                    ElseIf PosDauTru <> -1 And PosDauCham = -1 Then  'truong hop chi co dau tru
                        'kiem tra so ky tu >=2 vi khi nhap vao ky tu thu 3 thi do chua hien thi ra text nen van hieu la 2
                        If txtText.Text.Length >= 2 Then
                            txtText.Text = txtText.Text.Replace(DinhDangHangNgan, "")
                            'If KySo <> "0" Then
                            If txtText.Text <> "-0" Then    'cho nhập dấu - trước số 0
                                txtText.Text = Decimal.Parse(txtText.Text).ToString(aaCustomFormat)
                            End If

                            'End If

                            'txttext.SelectionStart = txttext.Text.Length
                            txtText.SelectionStart = indexSelectionStart + 1 + 1   'do phai cong voi dau tru
                            indexSelectionStart += 1
                            If txtText.Text.Replace(DinhDangHangNgan, "").Length + 1 > 3 And txtText.Text.Replace(DinhDangHangNgan, "").Length + 1 Mod 3 = 1 Then
                                txtText.SelectionStart = indexSelectionStart + 1
                                indexSelectionStart += 1

                            End If
                            If indexSelectionStart > txtText.Text.Length Then
                                indexSelectionStart = txtText.Text.Length
                            End If
                        End If
                    ElseIf PosDauTru = -1 And PosDauCham <> -1 Then 'truong hop chi co dau cham
                        'kiem tra so ky tu >=2 vi khi nhap vao ky tu thu 3 thi do chua hien thi ra text nen van hieu la 2
                        If txtText.Text.Length >= 2 Then
                            'cat ra tri vi truoc dau cham va sau dau cham
                            'Dim strHangNgan As Double = 0
                            Dim strThapPhan As Double = 0
                            Dim strArr As String()
                            strArr = txtText.Text.Split(decimalString)
                            'strHangNgan = strArr(0).Replace(",", "")
                            strThapPhan = strArr(1)
                            'If strThapPhan.ToString.Length > 0 Then

                            If txtText.Text.IndexOf(decimalString) < txtText.SelectionStart And KySo <> "0" Then
                                txtText.Text = txtText.Text.Replace(DinhDangHangNgan, "")
                                txtText.Text = Decimal.Parse(txtText.Text).ToString(aaCustomFormat)
                            ElseIf txtText.Text.IndexOf(decimalString) > txtText.SelectionStart Then
                                txtText.Text = txtText.Text.Replace(DinhDangHangNgan, "")
                                txtText.Text = Decimal.Parse(txtText.Text).ToString(aaCustomFormat)
                            End If

                            'txttext.SelectionStart = txttext.Text.Length
                            txtText.SelectionStart = indexSelectionStart + 1
                            indexSelectionStart += 1
                            If txtText.Text.Substring(0, txtText.Text.IndexOf(decimalString)).Replace(DinhDangHangNgan, "").Length > 3 And txtText.Text.Substring(0, txtText.Text.IndexOf(decimalString)).Replace(DinhDangHangNgan, "").Length Mod 3 = 1 Then
                                txtText.SelectionStart = indexSelectionStart + 1
                                indexSelectionStart += 1

                            End If
                            If indexSelectionStart > txtText.Text.Length Then
                                indexSelectionStart = txtText.Text.Length
                            End If
                            'End If

                        End If
                    ElseIf PosDauTru <> -1 And PosDauCham <> -1 Then    'truong hop co ca dau cham va dau tru
                        'kiem tra so ky tu >=2 vi khi nhap vao ky tu thu 3 thi do chua hien thi ra text nen van hieu la 2
                        If txtText.Text.Length >= 2 Then
                            'cat ra tri vi truoc dau cham va sau dau cham
                            'Dim strHangNgan As Double = 0
                            Dim strThapPhan As Double = 0
                            Dim strArr As String()
                            strArr = txtText.Text.Split(decimalString)
                            'strHangNgan = strArr(0).Replace(",", "")
                            strThapPhan = strArr(1)
                            If strThapPhan.ToString.Length > 0 Then
                                txtText.Text = txtText.Text.Replace(DinhDangHangNgan, "")
                                txtText.Text = Decimal.Parse(txtText.Text).ToString(aaCustomFormat)
                                'txttext.SelectionStart = txttext.Text.Length
                                txtText.SelectionStart = indexSelectionStart + 1 + 1   'do phai cong voi dau tru
                                indexSelectionStart += 1
                                If txtText.Text.Replace(DinhDangHangNgan, "").Length + 1 > 3 And txtText.Text.Replace(DinhDangHangNgan, "").Length + 1 Mod 3 = 1 Then
                                    txtText.SelectionStart = indexSelectionStart + 1
                                    indexSelectionStart += 1

                                End If
                                If indexSelectionStart > txtText.Text.Length Then
                                    indexSelectionStart = txtText.Text.Length
                                End If
                            End If

                        End If
                    End If
                End If

                isTextChange = False
            End If
        Catch ex As Exception
        End Try

        Try
            If (IIf((((e.KeyCode = Keys.Return) AndAlso Me.aaAcceptNextControl = True) AndAlso Not Me.txtText.Multiline), 1, 0) <> 0) Then
                SetNextControl(Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtText_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtText.KeyDown
        RaiseEvent aaKeyDown(sender, e)
    End Sub

    Private Sub txtText_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtText.MouseDown
        If _aaEnter Then
            If aaSelectAllFocus = True Then
                txtText.SelectAll()
            Else
                Me.txtText.SelectionLength = 0
            End If
        End If
        Me._aaEnter = False
    End Sub

    Private Sub txtText_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtText.Leave
        Try
            If aaOnlyNumber = True Then
                If txtText.Text.Length = 0 Then
                    txtText.Text = "0"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Lỗi: Leave " & ex.Message)
        End Try
    End Sub

    Private Sub txtText_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtText.GotFocus
        Try
            txtText.SelectionStart = txtText.Text.Length + 1
            If aaSelectAllFocus = True Then
                If _aaOnlyNumber = True Then
                    If txtText.Text = 0 Then
                        txtText.Text = ""
                    Else
                        txtText.SelectAll()
                    End If
                Else
                    txtText.SelectAll()
                End If
            Else
                If _aaOnlyNumber = True Then
                    If txtText.Text = 0 Then
                        txtText.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Declare Events"

    Public Delegate Sub aaKeyPressHandler(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    Public Event aaKeyPress As aaKeyPressHandler

    Public Delegate Sub aaKeyUpHandler(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event aaKeyUp As aaKeyUpHandler

    Public Delegate Sub aaKeyDownHandler(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    Public Event aaKeyDown As aaKeyDownHandler

#End Region

End Class
