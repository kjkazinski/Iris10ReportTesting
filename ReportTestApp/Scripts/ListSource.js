function activity() {
    var activitySet = new kendo.data.HierarchicalDataSource({
        data: [
            {
               id: 0, text: "Activity", items: [
                  { id: 1, text: "Activity_Key" },
                  { id: 2, text: "Name" },
                  { id: 3, text: "Description" },
                  { id: 4, text: "NameDesc" },
                  { id: 5, text: "DescName" },
                  { id: 6, text: "Perform_Standard" },
                  { id: 7, text: "Work_Unit" },
                  { id: 8, text: "WorkComp_Key" },
                  { id: 9, text: "UOM_Key" },
                  { id: 10, text: "Work_Methods" },
                  { id: 11, text: "Inspection" },
                  { id: 12, text: "Authorize" },
                  { id: 13, text: "Active" },
                  { id: 14, text: "User1" },
                  { id: 15, text: "User2" },
                  { id: 16, text: "User3" },
                  { id: 17, text: "User4" },
                  { id: 18, text: "User5" },
                  { id: 19, text: "User6" },
                  { id: 20, text: "User7" },
                  { id: 21, text: "User8" },
                  { id: 22, text: "User9" },
                  { id: 23, text: "User10" },
                  { id: 24, text: "CreateDate" },
                  { id: 25, text: "DateStamp" },
                  { id: 26, text: "SecurityUser_Key" }
                ]
            }
        ]
    });
    return activitySet;
}

function transact() {
    var transactSet = new kendo.data.HierarchicalDataSource({
        data: [
            {
                id: 0, text: "Transact", items: [
                   {
                       id: 1, text: "Activity_Key", items: [
                           { id: 16, text: "User3" },
                          { id: 17, text: "User4" },
                          { id: 18, text: "User5" },
                          { id: 19, text: "User6" }
                       ]
                   },
                  { id: 2, text: "Name" },
                  { id: 3, text: "Description" },
                ]
            }
        ]
    });
    return transactSet;
}


function DropDownSet() {

    var fieldData = [
                    { text: "Activity", value: "Activity" },
                    { text: "Transact", value: "Transact" }
    ];
    return fieldData;
}

//new kendo.data.HierarchicalDataSource({
//    data: [
//        {
//            text: "Activity", items: [
//              { text: "Activity_Key" },
//              { text: "Name" },
//              { text: "Description" },
//              { text: "NameDesc" },
//              { text: "DescName" },
//              { text: "Perform_Standard" },
//              { text: "Work_Unit" },
//              { text: "WorkComp_Key" },
//              { text: "UOM_Key" },
//              { text: "Work_Methods" },
//              { text: "Inspection" },
//              { text: "Authorize" },
//              { text: "Active" },
//              { text: "User1" },
//              { text: "User2" },
//              { text: "User3" },
//              { text: "User4" },
//              { text: "User5" },
//              { text: "User6" },
//              { text: "User7" },
//              { text: "User8" },
//              { text: "User9" },
//              { text: "User10" },
//              { text: "CreateDate" },
//              { text: "DateStamp" },
//              { text: "SecurityUser_Key" }
//            ]
//        }
//    ]
//});