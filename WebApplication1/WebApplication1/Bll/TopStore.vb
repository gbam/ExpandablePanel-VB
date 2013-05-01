Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Public Class TopStore

        ''' <summary>
        ''' Collection of top stores
        ''' </summary>
        Private _Stores As List(Of Store)

        ''' <summary>
        ''' Error reading from the database
        ''' </summary>
        Private _Error As Boolean

        ''' <summary>
        ''' Constructor
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Get top stores data from AdventureWorks database or simulated
        ''' </summary>
        ''' <returns>Top stores</returns>
        Public Function GetData() As ReadOnlyCollection(Of Store)

        PopulateStoresSimulated()

        Dim result As ReadOnlyCollection(Of Store) = Nothing

        If _Stores IsNot Nothing Then
            result = New ReadOnlyCollection(Of Store)(_Stores)
        End If

        Return result
        End Function

        ''' <summary>
        ''' Populates the collection with simulated data
        ''' </summary>
        Private Sub PopulateStoresSimulated()
        _Stores = New List(Of Store)(TopStoreSimulated.GetData())
    End Sub

    ''' <summary>
    ''' Number of rows retrieved
    ''' </summary>
    ''' <returns></returns>
        Public Function GetRowCount() As Integer

            If _Stores Is Nothing Then
                Return 0
            End If

            Return _Stores.Count
        End Function

        ''' <summary>
        ''' Indicates some error ocurred when getting data
        ''' </summary>
        Public ReadOnly Property [Error]() As Boolean
            Get
                Return _Error
            End Get
        End Property

#Region "IDisposable Members"

        ''' <summary>
        ''' Releases resources used by this class
        ''' </summary>
        Public Sub Dispose()

            If _Stores IsNot Nothing Then
                _Stores.Clear()
                _Stores = Nothing
            End If

            GC.SuppressFinalize(Me)
        End Sub

#End Region
    End Class