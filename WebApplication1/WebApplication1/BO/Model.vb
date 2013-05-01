Public Class Model
    Private _ProductModelID As Integer
    Private _CustomerId As Integer
    Private _Name As String
    Private _Qty As Integer
    Private _TotalPrice As Double
    Private _LastSale As DateTime


    Public Sub New(customerId As Integer, productModelId As Integer, name As String, qty As Integer, totalPrice As [Decimal], lastSale As DateTime)
        _ProductModelId = productModelId
        _CustomerId = customerId
        _Name = name
        _Qty = qty
        _TotalPrice = totalPrice
        _LastSale = lastSale
    End Sub

    '''<summary>
    ''' Model ID
    ''' </summary>
    Public ReadOnly Property ProductModelId() As Integer
        Get
            Return _ProductModelId
        End Get
    End Property

    ''' <summary>
    ''' Customer ID = Store ID
    ''' </summary>
    Public ReadOnly Property CustomerId() As Integer
        Get
            Return _CustomerId
        End Get
    End Property
    ''' <summary>
    ''' Model name
    ''' </summary>
    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    ''' <summary>
    ''' Quantity sold
    ''' </summary>
    Public ReadOnly Property Qty() As Integer
        Get
            Return _Qty
        End Get
    End Property

    ''' <summary>
    ''' Total price of all invoices (issued by that customer)
    ''' </summary>
    Public ReadOnly Property TotalPrice() As [Decimal]
        Get
            Return _TotalPrice
        End Get
    End Property

    ''' <summary>
    ''' Last sale date
    ''' </summary>
    Public ReadOnly Property LastSale() As DateTime
        Get
            Return _LastSale
        End Get
    End Property
End Class
