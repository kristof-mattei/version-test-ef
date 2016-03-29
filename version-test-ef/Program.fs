namespace Version.Test.Ef

module Main =

    open System

    [<EntryPoint>]
    let main argv = 
    
        let setup () = 
            use context = Database.getDatabaseContext ()

            let versionTracker = new Database.Edmx.Model.VersionTracker(LastChanged = DateTime.Now)

            context.VersionTrackers.AddObject(versionTracker)
            try
                
                context.SaveChanges() |> ignore
            with 
            | e -> ()

            versionTracker.Id

        let versionTrackerId = setup ()

        // now let's add an item
        // to items, linking it to the given versionTrackerId
        // and at the same time updating the LastUpdated of the VersionTracker, with a wrong rowVersion
        // this should cause a failure, and roll back the WHOLE transaction
        let insertItemAndUpdateLastUpdated id = 
            use context = Database.getDatabaseContext ()

            let updatedVersionTracker = context.VersionTrackers.CreateObject(Id = id, LastChanged = System.DateTime.Now, Version = [||])
            context.VersionTrackers.Attach(updatedVersionTracker)
            context.ObjectStateManager.GetObjectStateEntry(updatedVersionTracker.EntityKey).SetModifiedProperty("LastChanged")
            context.ObjectStateManager.GetObjectStateEntry(updatedVersionTracker.EntityKey).SetModifiedProperty("Version")
            
            // add 2 items
            let item = new Database.Edmx.Model.Item(VersionTrackerId = id)
            context.Items.AddObject(item)
            
            let item = new Database.Edmx.Model.Item(VersionTrackerId = id)
            context.Items.AddObject(item)


            try
                context.SaveChanges() |> ignore
            with 
            | e -> ()

        insertItemAndUpdateLastUpdated versionTrackerId

        0 // return an integer exit code
