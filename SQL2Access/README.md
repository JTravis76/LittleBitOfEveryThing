SQL2ACCESS is a console application that execute SQL statements against a MS Access 2007+ database (accdb).
Also can export data in XML, CSV format or generate SQL insert statements.

Simply, write your SQL statement in a text file with extension (.sql). 
> **IMPORTANT!** All statement must end with a semi-colon (;), including comments for proper parsing.

Point to your ACCDB database. Done!

If you like to export data via SELECT statement, just enter a path to export.

See Usage:

`sql2access.exe ?`


### Objectives:
Design this project, because I was working for a company that use MS Access as an medical application. 
Not all workstations had the full version of MS Access, only Access Runtime, to make edits. Used this application 
to INSERT, UPDATE, DELETE, and DROP data on those workstations. Hope it would help someone else.

NOTE: Will need to compile to the version of MS Access you have installed on the client machine. You can found
which version by looking in the Control Panel -> Administrative Tools -> ODBC Data Sources (32 bit) & (64-bit).
Open each and look under the Drivers tab for accdb.

