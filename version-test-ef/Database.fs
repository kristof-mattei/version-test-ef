namespace Version.Test.Ef

module Database =

    open System.Configuration
    open System.Data.SqlClient
    open System.Data.EntityClient
    open System.Data.Metadata.Edm
    // open System.Transactions
    open Microsoft.FSharp.Data.TypeProviders

    let internal runtimeConnectionString = ConfigurationManager.ConnectionStrings.["version-test-ef"].ConnectionString

    let internal getEDMConnectionString() =
        let dbConnection = new SqlConnection(runtimeConnectionString)
        let resourceArray = [| "res://*/" |]
        let assemblyList = [| System.Reflection.Assembly.GetCallingAssembly() |]
        let metaData = MetadataWorkspace(resourceArray, assemblyList)

        new EntityConnection(metaData, dbConnection)

    type internal Edmx = EdmxFile<"Model.edmx", ResolutionFolder = @".">

    let internal getDatabaseContext () = new Edmx.Model.Entities(getEDMConnectionString())
