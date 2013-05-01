Imports System.Collections.ObjectModel

Public NotInheritable Class TopStoreSimulated
    ''' <summary>
    ''' Fills a collection with simulated data, on top stores
    ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Get a collection of top stores (simulated)
        ''' </summary>
        ''' <returns>A collection of top stores</returns>
        Public Shared Function GetData() As ReadOnlyCollection(Of Store)

            '  AdventureWorks :
            '
            '                Select TOP 20 s.CustomerID, 
            '                              s.Name           as Store, 
            '                              Sum(so.TotalDue) as Sales,
            '
            '                              'stores.Add(new Model(' +
            '                              Cast(s.CustomerID as VarChar(5)) + ',' +
            '                              '"' + s.Name + '", ' +
            '                              Cast(Sum(so.TotalDue) as VarChar(15)) + 'M) );'
            '
            '                From       Sales.Store            s
            '                Inner Join Sales.SalesOrderHeader so on s.CustomerID = so.CustomerID
            '                Group by s.CustomerID, s.Name
            '                Order by Sum(so.TotalDue) DESC, s.Name
            '             


            Dim stores As New List(Of Store)(20)
            stores.Add(New Store(345, "test", 123))
            stores.Add(New Store(678, "Vigorous Exercise Company", 1179857.4657D))
            stores.Add(New Store(697, "Brakes and Gears", 1179475.8399D))
            stores.Add(New Store(170, "Excellent Riding Supplies", 1134747.4413D))
            stores.Add(New Store(328, "Totes & Baskets Company", 1084439.0265D))
            stores.Add(New Store(514, "Retail Mall", 1074154.3035D))
            stores.Add(New Store(155, "Corner Bicycle Supply", 1045197.0498D))
            stores.Add(New Store(72, "Outdoor Equipment Store (simulated: no models)", 1005539.7181D))
            stores.Add(New Store(227, "Health Spa. Limited", 984324.0473D))
            stores.Add(New Store(433, "Thorough Parts and Repair Services", 983871.933D))
            stores.Add(New Store(166, "Fitness Toy Store", 979881.3491D))
            stores.Add(New Store(146, "Latest Sports Equipment", 964134.7777D))
            stores.Add(New Store(670, "First Bike Store", 946105.7121D))
            stores.Add(New Store(506, "Great Bikes ", 937466.3027D))
            stores.Add(New Store(167, "Farthermost Bike Shop", 921582.9669D))
            stores.Add(New Store(546, "Field Trip Store", 903275.9454D))
            stores.Add(New Store(638, "Metropolitan Equipment", 864228.6038D))
            stores.Add(New Store(24, "Eastside Department Store", 863292.7317D))
            stores.Add(New Store(608, "Golf and Cycle Store", 839546.8838D))
            stores.Add(New Store(309, "The Gear Store", 821777.7287D))
            stores.Add(New Store(436, "Sheet Metal Manufacturing", 819823.3681D))

            Return New ReadOnlyCollection(Of Store)(stores)
        End Function
    End Class

