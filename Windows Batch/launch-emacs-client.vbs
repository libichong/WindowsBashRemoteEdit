Set objShell = WScript.CreateObject("WScript.Shell")
Set fso = CreateObject("Scripting.FileSystemObject")

If WScript.Arguments.Count = 1 Then

  strComputer = "."

  Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\cimv2")

  Set colItems = objWMIService.ExecQuery("Select * From Win32_Process")

  Dim isRunning
  isRunning = False

  For Each objItem in colItems
    If InStr(objItem.CommandLine, "emacs.exe") Then
      isRunning = True
    End If
  Next

  If isRunning Then
    objShell.Run(fso.GetParentFolderName(WScript.ScriptFullName) & "/emacsclientw.exe -n """ & WScript.Arguments(0) & """")
  Else
    objShell.Run(fso.GetParentFolderName(WScript.ScriptFullName) & "/runemacs.exe """ & WScript.Arguments(0) & """")
  End If

Else
  objShell.Run(fso.GetParentFolderName(WScript.ScriptFullName) & "/runemacs.exe")
End If