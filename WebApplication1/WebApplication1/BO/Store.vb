Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel


Public Class Store
    ''' <summary>
    ''' One store
    ''' </summary>
        Private _CustomerId As Integer
        Private _Name As String
        Private _Sales As Decimal

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="customerId">Customer ID = Store ID</param>
        ''' <param name="name">      Store name</param>
        ''' <param name="sales">     Sales value</param>
        Sub New(customerId As Integer, name As String, sales As Decimal)

            _CustomerId = customerId
            _Name = name
            _Sales = sales
        End Sub

        ''' <summary>
        ''' Customer ID = Store ID
        ''' </summary>
        Public ReadOnly Property CustomerId() As Integer
            Get
                Return _CustomerId
            End Get
        End Property

        ''' <summary>
        ''' Store name
        ''' </summary>
        Public ReadOnly Property StoreName() As String
            Get
                Return _Name
            End Get
        End Property

        ''' <summary>
        ''' Sales value
        ''' </summary>
        Public ReadOnly Property Sales() As Decimal
            Get
                Return _Sales
            End Get
        End Property
    End Class
