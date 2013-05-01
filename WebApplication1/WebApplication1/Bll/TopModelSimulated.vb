Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Public NotInheritable Class TopModelSimulated
    ''' <summary>
    ''' Fills a collection with simulated data, on top models sold by stores
    ''' </summary>
    Private Sub New()
    End Sub

    ' AdventureWorks :
    '
    '           Declare @CustomerID Int
    '
    '           Set @CustomerID = 678
    '
    '           Select TOP 5 so.CustomerID,
    '                        m.ProductModelID, 
    '                        m.Name            as Model, 
    '                        Sum(sd.OrderQty)  as Qty, 
    '                        Sum(sd.LineTotal) as TotalPrice, 
    '                        Max(so.OrderDate) as LastSale,
    '
    '                        'new Model(' +
    '                        Cast(so.CustomerID as VarChar(5)) + ',' +
    '                        Cast(m.ProductModelID as VarChar(5)) + ', ' +
    '                        '"' + m.Name + '", ' +
    '                        Cast(Sum(sd.OrderQty) as VarChar(15)) + ', ' +
    '                        Cast(Sum(sd.LineTotal) as VarChar(15)) + 'M, ' +
    '                        ' new DateTime(' + 
    '                          Cast(Year(Max(so.OrderDate)) as VarChar(4)) + ',' +
    '                          Cast(Month(Max(so.OrderDate)) as VarChar(2)) + ',' +
    '                          Cast(Day(Max(so.OrderDate)) as VarChar(2)) + ')),'
    '
    '           From       Sales.SalesOrderHeader  so
    '           Inner Join Sales.SalesOrderDetail  sd on so.SalesOrderID   = sd.SalesOrderID
    '           Inner Join Production.Product      p  on sd.ProductID      =  p.ProductID
    '           Inner Join Production.ProductModel m  on  p.ProductModelID =  m.ProductModelID
    '
    '           Where so.CustomerID = @CustomerID
    '
    '           Group by so.CustomerID, m.ProductModelID, m.Name
    '           Order by so.CustomerID, Sum(sd.LineTotal) DESC, m.Name
    '         








    ' Simulates no models
    '                new Model(72,26, "Road-250", 160, 221823.894002M,  new DateTime(2004,6,1)),
    '                new Model(72,30, "Road-650", 265, 119873.654927M,  new DateTime(2003,6,1)),
    '                new Model(72,29, "Road-550-W", 192, 119465.043308M,  new DateTime(2004,6,1)),
    '                new Model(72,27, "Road-350-W", 104, 100896.943434M,  new DateTime(2004,6,1)),
    '                new Model(72,6, "HL Road Frame", 92, 75505.119000M,  new DateTime(2004,6,1)),
    '                 

    Private Shared _Models As Model() = New Model() {New Model(678, 20, "Mountain-200", 231, 297308.86464D, New DateTime(2004, 6, 1)), New Model(678, 19, "Mountain-100", 161, 245704.274625D, New DateTime(2002, 6, 1)), New Model(678, 5, "HL Mountain Frame", 180, 137825.711D, New DateTime(2004, 6, 1)), New Model(678, 21, "Mountain-300", 60, 38879.64D, New DateTime(2003, 6, 1)), New Model(678, 23, "Mountain-500", 53, 15360.9252D, New DateTime(2004, 6, 1)), New Model(697, 20, "Mountain-200", 334, 434820.842969D, New DateTime(2004, 5, 1)), _
     New Model(697, 19, "Mountain-100", 87, 176774.478D, New DateTime(2002, 5, 1)), New Model(697, 21, "Mountain-300", 119, 75848.561692D, New DateTime(2003, 5, 1)), New Model(697, 23, "Mountain-500", 184, 50508.2832D, New DateTime(2004, 5, 1)), New Model(697, 22, "Mountain-400-W", 99, 45148.440668D, New DateTime(2004, 5, 1)), New Model(170, 26, "Road-250", 230, 322888.7025D, New DateTime(2004, 6, 1)), New Model(170, 30, "Road-650", 301, 136022.1383D, New DateTime(2003, 6, 1)), _
     New Model(170, 29, "Road-550-W", 175, 111312.678D, New DateTime(2004, 6, 1)), New Model(170, 25, "Road-150", 38, 81584.556D, New DateTime(2002, 6, 1)), New Model(170, 27, "Road-350-W", 60, 61235.64D, New DateTime(2004, 6, 1)), New Model(328, 26, "Road-250", 217, 305558.37D, New DateTime(2004, 5, 1)), New Model(328, 30, "Road-650", 311, 138840.9021D, New DateTime(2003, 5, 1)), New Model(328, 29, "Road-550-W", 160, 101372.331D, New DateTime(2004, 5, 1)), _
     New Model(328, 25, "Road-150", 33, 70849.746D, New DateTime(2002, 5, 1)), New Model(328, 27, "Road-350-W", 69, 70420.986D, New DateTime(2004, 5, 1)), New Model(514, 26, "Road-250", 137, 193262.004588D, New DateTime(2004, 4, 1)), New Model(514, 30, "Road-650", 430, 184415.472544D, New DateTime(2003, 4, 1)), New Model(514, 29, "Road-550-W", 125, 78585.854864D, New DateTime(2004, 4, 1)), New Model(514, 27, "Road-350-W", 75, 75308.270468D, New DateTime(2004, 4, 1)), _
     New Model(514, 28, "Road-450", 65, 56861.61D, New DateTime(2002, 4, 1)), New Model(155, 26, "Road-250", 207, 290112.9075D, New DateTime(2004, 6, 1)), New Model(155, 30, "Road-650", 296, 133773.8385D, New DateTime(2003, 6, 1)), New Model(155, 29, "Road-550-W", 140, 87854.4195D, New DateTime(2004, 6, 1)), New Model(155, 25, "Road-150", 35, 75143.67D, New DateTime(2002, 6, 1)), New Model(155, 27, "Road-350-W", 60, 61235.64D, New DateTime(2004, 6, 1)), _
     New Model(227, 30, "Road-650", 412, 183039.368294D, New DateTime(2003, 4, 1)), New Model(227, 26, "Road-250", 92, 129846.6D, New DateTime(2004, 4, 1)), New Model(227, 27, "Road-350-W", 112, 106924.2314D, New DateTime(2004, 4, 1)), New Model(227, 29, "Road-550-W", 120, 75838.765636D, New DateTime(2004, 4, 1)), New Model(227, 28, "Road-450", 71, 62110.374D, New DateTime(2002, 4, 1)), New Model(433, 26, "Road-250", 183, 261211.5675D, New DateTime(2004, 5, 1)), _
     New Model(433, 29, "Road-550-W", 154, 99974.407676D, New DateTime(2004, 5, 1)), New Model(433, 30, "Road-650", 214, 94082.9567D, New DateTime(2003, 5, 1)), New Model(433, 27, "Road-350-W", 86, 87771.084D, New DateTime(2004, 5, 1)), New Model(433, 25, "Road-150", 38, 81584.556D, New DateTime(2002, 5, 1)), New Model(166, 30, "Road-650", 419, 180078.177593D, New DateTime(2003, 4, 1)), New Model(166, 26, "Road-250", 117, 163853.669414D, New DateTime(2004, 4, 1)), _
     New Model(166, 27, "Road-350-W", 86, 85621.03264D, New DateTime(2004, 4, 1)), New Model(166, 29, "Road-550-W", 117, 74696.6655D, New DateTime(2004, 4, 1)), New Model(166, 6, "HL Road Frame", 64, 51420.2893D, New DateTime(2004, 4, 1)), New Model(146, 20, "Mountain-200", 274, 353378.207516D, New DateTime(2004, 4, 1)), New Model(146, 19, "Mountain-100", 84, 170654.496D, New DateTime(2002, 4, 1)), New Model(146, 21, "Mountain-300", 91, 58967.454D, New DateTime(2003, 4, 1)), _
     New Model(146, 23, "Mountain-500", 116, 38243.304D, New DateTime(2004, 4, 1)), New Model(146, 5, "HL Mountain Frame", 37, 28471.2816D, New DateTime(2004, 4, 1)), New Model(670, 26, "Road-250", 204, 286029.0225D, New DateTime(2004, 4, 1)), New Model(670, 30, "Road-650", 268, 121455.1702D, New DateTime(2003, 4, 1)), New Model(670, 29, "Road-550-W", 169, 107639.0715D, New DateTime(2004, 4, 1)), New Model(670, 27, "Road-350-W", 75, 76544.55D, New DateTime(2004, 4, 1)), _
     New Model(670, 25, "Road-150", 24, 51527.088D, New DateTime(2002, 4, 1)), New Model(506, 20, "Mountain-200", 303, 390430.68794D, New DateTime(2004, 4, 1)), New Model(506, 19, "Mountain-100", 73, 148244.562D, New DateTime(2002, 4, 1)), New Model(506, 21, "Mountain-300", 92, 59240.043476D, New DateTime(2003, 4, 1)), New Model(506, 5, "HL Mountain Frame", 36, 27971.7766D, New DateTime(2004, 4, 1)), New Model(506, 22, "Mountain-400-W", 45, 20776.23D, New DateTime(2004, 4, 1)), _
     New Model(167, 26, "Road-250", 167, 232100.7975D, New DateTime(2004, 5, 1)), New Model(167, 30, "Road-650", 338, 150102.5353D, New DateTime(2003, 5, 1)), New Model(167, 25, "Road-150", 49, 105201.138D, New DateTime(2002, 5, 1)), New Model(167, 29, "Road-550-W", 109, 68093.778D, New DateTime(2004, 5, 1)), New Model(167, 28, "Road-450", 37, 32367.378D, New DateTime(2002, 5, 1)), New Model(546, 20, "Mountain-200", 336, 435915.838928D, New DateTime(2004, 5, 1)), _
     New Model(546, 5, "HL Mountain Frame", 56, 44150.5964D, New DateTime(2004, 5, 1)), New Model(546, 21, "Mountain-300", 66, 42767.604D, New DateTime(2003, 5, 1)), New Model(546, 23, "Mountain-500", 129, 39199.6884D, New DateTime(2004, 5, 1)), New Model(546, 22, "Mountain-400-W", 82, 37591.433276D, New DateTime(2004, 5, 1)), New Model(638, 26, "Road-250", 156, 218160.613125D, New DateTime(2004, 5, 1)), New Model(638, 30, "Road-650", 218, 96977.069852D, New DateTime(2003, 5, 1)), _
     New Model(638, 27, "Road-350-W", 94, 91585.554075D, New DateTime(2004, 5, 1)), New Model(638, 29, "Road-550-W", 112, 70686.912D, New DateTime(2004, 5, 1)), New Model(638, 6, "HL Road Frame", 59, 47942.237D, New DateTime(2004, 5, 1)), New Model(24, 26, "Road-250", 99, 137779.634414D, New DateTime(2004, 6, 1)), New Model(24, 27, "Road-350-W", 151, 135147.907975D, New DateTime(2004, 6, 1)), New Model(24, 30, "Road-650", 277, 125396.318294D, New DateTime(2003, 6, 1)), _
     New Model(24, 29, "Road-550-W", 114, 71959.4685D, New DateTime(2004, 6, 1)), New Model(24, 6, "HL Road Frame", 59, 48801.1368D, New DateTime(2004, 6, 1)), New Model(608, 19, "Mountain-100", 168, 261704.322667D, New DateTime(2002, 6, 1)), New Model(608, 20, "Mountain-200", 124, 149188.6506D, New DateTime(2003, 6, 1)), New Model(608, 5, "HL Mountain Frame", 145, 107895.8054D, New DateTime(2003, 6, 1)), New Model(608, 21, "Mountain-300", 52, 33695.688D, New DateTime(2003, 6, 1)), _
     New Model(608, 14, "ML Mountain Frame", 46, 9625.776D, New DateTime(2003, 6, 1)), New Model(309, 20, "Mountain-200", 213, 273133.256984D, New DateTime(2004, 5, 1)), New Model(309, 19, "Mountain-100", 90, 183074.46D, New DateTime(2002, 5, 1)), New Model(309, 21, "Mountain-300", 83, 53783.502D, New DateTime(2003, 5, 1)), New Model(309, 5, "HL Mountain Frame", 37, 27958.03D, New DateTime(2004, 5, 1)), New Model(309, 23, "Mountain-500", 46, 13636.5528D, New DateTime(2004, 5, 1)), _
     New Model(436, 26, "Road-250", 194, 273410.865D, New DateTime(2004, 5, 1)), New Model(436, 30, "Road-650", 229, 105445.2633D, New DateTime(2003, 5, 1)), New Model(436, 29, "Road-550-W", 157, 99787.638D, New DateTime(2004, 5, 1)), New Model(436, 27, "Road-350-W", 52, 53070.888D, New DateTime(2004, 5, 1)), New Model(436, 17, "ML Road Frame-W", 54, 18201.7971D, New DateTime(2004, 5, 1))}

    ''' <summary>
    ''' Get a collection of top models sold by one store
    ''' </summary>
    ''' <param name="customerId">Customer ID = Store ID</param>
    ''' <returns>Collection of top models</returns>
    Public Shared Function GetData(customerId As Integer) As ReadOnlyCollection(Of Model)

        Dim models As List(Of Model) = Nothing

        Dim first As Integer = -1
        Dim last As Integer = -1

        ' Find first row on _Models array
        For i As Integer = 0 To _Models.Length - 1

            If _Models(i).CustomerId = customerId Then

                first = i
                Exit For
            End If
        Next

        If first <> -1 Then

            ' Find last row on _Models array

            For i As Integer = first + 1 To _Models.Length - 1

                If _Models(i).CustomerId <> customerId Then

                    last = i - 1
                    Exit For
                End If
            Next

            ' Lines inserted by Amauri - 22.Sep.2008 - Thank's to 'LeMoustique' - BEGIN
            If last = -1 Then
                last = _Models.Length - 1
            End If
            ' Lines inserted by Amauri - 22.Sep.2008 - Thank's to 'LeMoustique' - END

            ' Copies row from _Models array tho the collection

            models = New List(Of Model)(last - first + 1)

            For i As Integer = first To last

                models.Add(_Models(i))
            Next
        End If

        Dim result As ReadOnlyCollection(Of Model) = Nothing

        If models IsNot Nothing Then
            result = New ReadOnlyCollection(Of Model)(models)
        End If

        Return result
    End Function
End Class
