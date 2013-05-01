Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

''' <summary>
''' Business logic layer class, to access 'TopModel' in data access layer
''' </summary>
Public NotInheritable Class TopModel

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Get top models for one customer/store
    ''' </summary>
    ''' <param name="customerId">Customer ID = Store ID</param>
    ''' <param name="simulated">True = Simulated data ; False = Read from AdventureWorks database</param>
    ''' <returns>A collection of top models sold by that store</returns>
    Public Shared Function GetTopModels(customerId As Integer, simulated As Boolean) As ReadOnlyCollection(Of Model)

        Dim models As ReadOnlyCollection(Of Model)

        If simulated Then
            models = PopulateModelsSimulated(customerId)
        Else
            models = PopulateModelsStores(customerId)
        End If

        Return models
    End Function

    ''' <summary>
    ''' Populates the collection with simulated data
    ''' </summary>
    ''' <param name="customerId">Customer ID = Store ID</param>
    ''' <returns>A collection of top models sold by that store</returns>
    Private Shared Function PopulateModelsSimulated(customerId As Integer) As ReadOnlyCollection(Of Model)

        Dim models As ReadOnlyCollection(Of Model) = TopModelSimulated.GetData(customerId)

        Return models
    End Function

    ''' <summary>
    ''' Populates the collection with data read from the AdventureWorks database
    ''' </summary>
    ''' <param name="customerId">Customer ID = Store ID</param>
    ''' <returns>A collection of top models sold by that store</returns>s
    Private Shared Function PopulateModelsStores(customerId As Integer) As ReadOnlyCollection(Of Model)

        Dim models As List(Of Model) = Nothing

        ' Using adap As AdventureWorksTableAdapters.TopModelTableAdapter = New ExpandPanelGridView.Dal.AdventureWorksTableAdapters.TopModelTableAdapter()

        Dim tab As DataTable = Nothing

        Try
            ' tab = adap.GetData(customerId)

            models = New List(Of Model)(tab.Rows.Count)

            For Each row As DataRow In tab.Rows

                '  models.Add(New Model(customerId, row.ProductModelID, row.Model, row.Qty, row.TotalPrice, row.LastSale))
            Next
        Catch ex As System.Exception
            ' TODO: Error LOG

            Throw
        Finally
            If tab IsNot Nothing Then
                tab.Dispose()
                tab = Nothing
            End If
        End Try
        ' End Using

        Dim result As ReadOnlyCollection(Of Model) = Nothing

        If models IsNot Nothing Then
            result = New System.Collections.ObjectModel.ReadOnlyCollection(Of Model)(models)
        End If

        Return result
    End Function

End Class
