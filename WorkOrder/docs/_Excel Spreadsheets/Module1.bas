Attribute VB_Name = "Module1"
Public Sub WriteFile()
'MUST Add Reference: Microsoft Scripting Runtime
Dim fso As New FileSystemObject
' Declare a TextStream.
Dim stream As TextStream

' Create a TextStream.
Set stream = fso.CreateTextFile("C:\Users\jtravis\desktop\WorkOrderExport.txt", True)

stream.WriteLine "USE [WorkOrder]"
stream.WriteLine "GO"

For r = 2 To 271
    If Not Cells(r, 1).Value = "" Then
        
        stream.WriteLine "INSERT INTO [dbo].[Orders]([WorkOrderNumber],[Equipment],[Unit],[Status],[MaterialStatus],[Priority],[BreakIn],[CallOut],[WorkType],[Description],[Questionaire],[Operation],[PlannedCost],[RevisionCode],[EditDate],[User],[MocRequired],[AFE]) VALUES ("
        
        stream.Write "'" & Cells(r, 1).Value & "'" 'WorkOrderNumber (CHANGE PREFIX FOR 'Preventative Maintenance')
        stream.Write ",'" & Cells(r, 2).Value & "'" 'Equipment
        stream.Write ",'" & Cells(r, 3).Value & "'" 'Unit
        stream.Write ",'" & Cells(r, 4).Value & "'" 'WorkOrderStatus
        stream.Write ",'" & Cells(r, 5).Value & "'" 'MaterialStatus
        stream.Write ",'" & Cells(r, 6).Value & "'" 'Priority
        
        If Cells(r, 7).Value = "No" Then stream.WriteLine ",0" Else stream.WriteLine ",1" 'BreakIn
        If Cells(r, 8).Value = "No" Then stream.WriteLine ",0" Else stream.WriteLine ",1" 'CallOut
        
        stream.Write ",'" & Cells(r, 9).Value & "'" 'TypeOfWork
        stream.WriteLine ",'" & Cells(r, 10).Value & "'" 'Desc Of Work (REPLACE ' WITH '')
        stream.WriteLine ",'" & Cells(r, 11).Value & "'" 'Questionire (REPLACE ' WITH '')
        stream.WriteLine ",'" & Cells(r, 12).Value & "'" 'Operations (REPLACE ' WITH '')
        stream.Write "," & Cells(r, 13).Value & "" 'PlannedCost (SET BLANK CELLS TO 0 FIRST)
        stream.Write ",''" 'Revision Code 'SKIP
        stream.Write ",'" & Cells(r, 16).Value & "'" 'EditDate
        stream.Write ",'" & Cells(r, 17).Value & "'" 'Edit User
        'stream.Write "," & Cells(r, 20).Value & "" 'Actual Cost Total (SET BLANK CELLS TO 0 FIRST)
        stream.Write ",0" 'MocRequired
        stream.WriteLine ",0);" 'AFE
        stream.WriteLine "GO"

    End If
Next r

' Close the file.
stream.Close
End Sub

Public Sub ManpowerExport()
    Dim fso As New FileSystemObject
    ' Declare a TextStream.
    Dim stream As TextStream
    
    ' Create a TextStream.
    Set stream = fso.CreateTextFile("C:\Users\jtravis\desktop\Manpower-Operations Export.txt", True)
    
    stream.WriteLine "USE [WorkOrder]"
    stream.WriteLine "GO"
    
    Dim craft(20) As String
    craft(0) = "MW"
    craft(2) = "PF"
    craft(4) = "AW"
    craft(6) = "CAP"
    craft(8) = "BM"
    craft(10) = "LB"
    craft(12) = "OP"
    craft(14) = "EL"
    craft(16) = "IEL"
    craft(18) = "IPF"
    
    Dim nStep As Integer
    
    Dim sData() As String
    For r = 5 To 271
        nStep = 10
                
        sData = Split(Sheets(Sheet2.Name).Cells(r, 15).Value, "|")
        
        If UBound(sData) > 0 Then
            For a = 0 To UBound(sData) Step 2
                   
                If Len(sData(a)) > 0 Then
                    
                    stream.WriteLine "INSERT INTO [dbo].[Operations] ([WorkOrderNumber],[Step],[Craft],[Desc],[Work],[PeopleUsed],[Duration],[WorkStatus],[EditDate],[EditUser],[PlannedCost]) VALUES "
        
                    stream.Write "('" & Sheets(Sheet2.Name).Cells(r, 1).Value & "'"
                    stream.Write "," & nStep 'STEP
                    stream.Write ",'" & craft(a) & "'" 'CRAFT
                    stream.Write ",NULL" 'DESC
                    stream.Write "," & sData(a) 'WORK HRS
                    stream.Write "," & sData(a + 1) 'PEOPLE USED
                    stream.Write "," & (sData(a) * sData(a + 1)) & "" 'Duration
                    stream.Write ",''" 'WORKSTATUS
                    stream.Write ",'" & Sheets(Sheet2.Name).Cells(r, 16).Value & "'" 'Edit DATE
                    stream.Write ",'" & Sheets(Sheet2.Name).Cells(r, 17).Value & "'" 'Edit USER
                    stream.Write ",0);" 'PlannedCost
                    stream.WriteLine ""
                    
                    nStep = nStep + 10
                End If
                
            
            Next a
        End If
    Next r
    
    ' Close the file.
    stream.Close
End Sub

Public Sub WriteActualLabor()
'MUST Add Reference: Microsoft Scripting Runtime
Dim fso As New FileSystemObject
' Declare a TextStream.
Dim stream As TextStream

' Create a TextStream.
Set stream = fso.CreateTextFile("C:\Users\jtravis\desktop\ActualLabor-Export.txt", True)

stream.WriteLine "USE [WorkOrder]"
stream.WriteLine "GO"

Dim sData() As String
For r = 2 To 271
    If Not Cells(r, 1).Value = "" Or Not Cells(r, 19) = "" Then
        
        sData = Split(Sheets(Sheet2.Name).Cells(r, 19).Value, "|")
        If UBound(sData) > 0 Then
            For a = 0 To UBound(sData) Step 4
                   
                If Len(sData(a)) > 0 Then
                    
                    stream.WriteLine "INSERT INTO [dbo].[ActualLabor]([WorkOrderNumber],[DateWorked],[Employee],[Craft],[RegHrs],[EditDate],[EditUser],[OtHrs],[DtHrs]) VALUES "
                    
                    stream.Write "('" & Sheets(Sheet2.Name).Cells(r, 1).Value & "'"
                    stream.Write ",'" & sData(a) & "'"
                    stream.Write ",'" & sData(a + 1) & "'"
                    stream.Write ",'" & sData(a + 2) & "'"
                    stream.Write "," & sData(a + 3)
                    stream.Write ",'" & VBA.Now & "'"
                    stream.Write ",'admin'"
                    stream.Write ",0"
                    stream.WriteLine ",0);"
                    
                    stream.WriteLine "GO"
                End If
            Next a
        End If
        
    End If
Next r

' Close the file.
stream.Close
End Sub

Public Sub WriteFileInventory()
'MUST Add Reference: Microsoft Scripting Runtime
Dim fso As New FileSystemObject
' Declare a TextStream.
Dim stream As TextStream

' Create a TextStream.
Set stream = fso.CreateTextFile("C:\Users\jtravis\desktop\InventoryExport.txt", True)

stream.WriteLine "USE [WorkOrder]"
stream.WriteLine "GO"

For r = 31 To 32172
    If Not Cells(r, 1).Value = "" Then
        
        stream.WriteLine "INSERT INTO [dbo].[Inventory] VALUES ("
        
        stream.Write "'" & Cells(r, 1).Value & "'"
        stream.Write ",'" & Cells(r, 2).Value & "'"
        stream.Write ",'" & Cells(r, 3).Value & "'"
        stream.Write ",'" & Cells(r, 4).Value & "'"
        stream.Write ",'" & Cells(r, 5).Value & "'"
        stream.Write ",'" & Cells(r, 6).Value & "'"
        
        stream.Write "," & Cells(r, 7).Value & ""
        stream.Write "," & Cells(r, 8).Value & ""
        stream.Write "," & Cells(r, 9).Value & ""
        
        stream.Write ",'" & Cells(r, 10).Value & "'"
        
        stream.Write "," & Cells(r, 11).Value & ""
        stream.Write "," & Cells(r, 12).Value & ""
        
        Dim v As String
        For c = 13 To 26
            v = v + " " + Cells(r, c).Value
        Next c
        
        stream.Write ",'" & Trim(v) & "'"

        stream.WriteLine ");"
        stream.WriteLine "GO"

        v = ""
    End If
Next r

' Close the file.
stream.Close
End Sub


Sub WriteBOMStatements()
Dim fso As New FileSystemObject
' Declare a TextStream.
Dim stream As TextStream

' Create a TextStream.
Set stream = fso.CreateTextFile("C:\Users\jtravis\desktop\BOM-Export.sql", True)

stream.WriteLine "USE [WorkOrder]"
stream.WriteLine "GO"

stream.WriteLine "SET IDENTITY_INSERT [dbo].[BOM] ON"

    For r = 2 To 55076
        If Not Cells(r, 1).Value = "" Then
            stream.WriteLine "INSERT INTO [dbo].[BOM] (Id,Equipment,Item,Component,[Description],Quantity,UOM) VALUES ("
            stream.Write Cells(r, 1).Value
            stream.Write ",N'" & Cells(r, 2).Value & "'"
            stream.Write ",N'" & Cells(r, 3).Value & "'"
            stream.Write ",N'" & Cells(r, 4).Value & "'"
            stream.Write ",N'" & Cells(r, 5).Value & "'"
            stream.Write "," & Cells(r, 6)
            stream.WriteLine ",N'" & Cells(r, 7).Value & "');"
        End If
    Next r

stream.WriteLine "SET IDENTITY_INSERT [dbo].[BOM] OFF"
End Sub
