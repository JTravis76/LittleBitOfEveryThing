# Simple Window Service
This console application demostrates how to create a Windows Service.

## Setup

From an `administrative` command prompt.

*Installing*
```
C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil /user= /password= WebApp.WindowService.exe
```

*Uninstalling*
```
C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil WebApp.WindowService.exe /u
```

This service creates a custom log within Event Viewer named "Simple Window Service" located under "Applications and Service Logs".

Run the following command with Powershell.

```
New-EventLog -LogName "Simple Window Service" -Source SimpleWindowService

Write-EventLog -LogName "Simple Window Service" -Source SimpleWindowService -EventId 0 -Message "This is a TEST" -EntryType Information
```


## Troubleshoot

May get the following error: 'This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms.'

> This error was throw while using AES to process encrytped string in the App.config.

In order to enable FIPS you need to follow steps mentioned below
* This security setting affects the following registry value in Windows Server 2008 and in Windows Vista: HKLM\System\CurrentControlSet\Control\Lsa\FIPSAlgorithmPolicy\Enabled 
This registry value reflects the current FIPS setting. If this setting is enabled, the value is 1. If this setting is disabled, the value is 0. 
[source](https://blogs.msdn.microsoft.com/brijs/2010/08/10/issue-getting-this-implementation-is-not-part-of-the-windows-platform-fips-validated-cryptographic-algorithms-exception-while-building-outlook-vsto-add-in-in-vs-2010/)
