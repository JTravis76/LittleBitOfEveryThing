//                                          \\\|///
//                                          \ _ _ /
//                                          ( o o )
//+--------------------------------------oOOo-(_)-oOOo-------------------------------------+
//           Name:	Avoli.cs
//          Owner:	Avoli, Inc. (http://www.avoli.com)
//         Author:	Eddie Drye (http://www.eddiedrye.com)
//    Application:	Avoli Framework Foundation Classes 
//                  (Helper Classes for Presentation/Business/Application/Database/Utility)
//                  NOTE: The Avoli.Framework consists of the Avoli.Foundation Classes & Base Forms
//        Version:  2.1
//        Created:	April 11, 2004
//      Copyright:	2004 - Present Year (All rights reserved)
// Managed Rights:	This code is NOT to be utilized, distributed or modified except through
//					the written permission of the owner.  If you believe your access to this
//                  code is against copyright or managed rights use you may be eligible for
//                  a finders fee for your assistance.
//
//+------------------------------------------------Oooo------------------------------------+
//                                           oooO  (  )
//                                          (  )   ) /
//                                           \ (  (_/
//                                            \_)


//                                  '''
//                                 (O O)
//     +-----------------------oOO--(_)-----------------------------------------------+
//     | TERMS:                                                                       |
//     |                                                                              |
//     | THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,  |
//     | EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED        |
//     | WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.       |
//     | YOU AGREE AND UNDERSTAND THE AUTHOR IS NOT LIABLE FOR DAMAGES.  PLEASE DO    |
//     | NOT USE ANY METHOD HERE ON A PRODUCTION ENVIRONMENT UNTIL FULLY TESTED.      |
//     |                                                                              |
//     | YOU AGREE AND UNDERSTAND THAT IF FULL PAYMENT IS NOT RECEIVED FOR WORK       |
//     | PERFORMED WHERE THIS AVOLI.DLL IS USED IN CREATION OF SOFTWARE WORK PRODUCTS |
//     | AVOLI, INC. OR ANY REPRESENTITVE WORKING FOR AVOLI, INC. HAS THE RIGHT TO    |
//     | SHUT YOUR THE SOFTWARE DOWN UNTIL SUCH PAYMENT IS MADE IN FULL.              |
//     |                                                                              |
//     | IF YOU DO NOT AGREE TO THESE TERMS YOU ARE NOT TO ASK FOR SERVICES TO BE     |
//     | PERFORMED AS A WORK PRODUCT AND SEEK SERVICES ELSEWHERE.                     |
//     |                                                                              |
//     |                                                    Inquires: www.avoli.com   |
//     +----------------------------------oOO-----------------------------------------+
//                                |__|__|
//                                 || ||
//                                ooO Ooo
//

// METHODS WITH SIMILAR FUNCTIONALITY
// Avoli.IO.Import.ToControl
// Avoli.DataStorage.File.XML.ToControl / ToControlByNodeName / ToControlByNameValues / ToControlByNameDefault

// COMMAND LINE PARAMS
// -------------------
// debug=true/false             (Populated by InitSmartClient)
// relkeyid=12345               (Populated by frmScan - Relational Key ID used for scan storage to File or DB.
// template=RunSheet1           (Utilized by frmScan - TemplateName tells the Scan Engine how to process OCR/OMR)
// relcon=<CONNECTION STRING>   (Connection to other databases outside ours  Ex: ADM)

// NOTE: Unrecoverable error caption is located in frmSplash

#region TO DO LIST
//                                           /'^'\
//                                          ( o o )
//+-------------------------------------oOOO--(_)--OOOo------------------------------------+
// PAY FOR ASSISTANCE ON THESE
//  UnitsOfMeasureConversion (Conversion of VB.net code to C#)
//
//	TO DO
//  *** Fix frmClockAnalog.cs (OnTick is not working!)

// System.Windows.Forms.PowerStatus;
// System.Windows.Forms.PowerLineStatus.Online;
// System.Windows.Forms.PowerState.Hibernate;
// System.Windows.Forms.ScreenOrientation.Angle180;
// System.Windows.Forms.SystemInformation.BootMode.ToString();
// System.Windows.Forms.BatteryChargeStatus.Low;

//	Change method to include saConnectionStrings
//		Fetch all Procedure values in ONE call - Returns MULTIPLE tables
//		string[] saProcNames = {"sp_select_states", "sp_select_products 1"};
//		string[] saProcValues = {"States", "Active Products"};
//		System.Data.DataSet ds = Avoli.DataStorage.Database.General.GetDataSet(strConnection, saProcNames, saProcValues);
//		Avoli.DataStorage.Database.DataSetObject.ToControl(objSmartClient, ds, 0, listView1, "", false);
//	Change DataReader to use NextResult method (Will be similar to GetDataSet method with multiple result sets)
//		SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["con"]);
//		con.Open();
//		SqlCommand cmd = new SqlCommand("SELECT TOP 3 * FROM Employees ORDER BY LastName; SELECT TOP 3 * FROM Employees ORDER BY LastName DESC", con);
//      objCommand.CommandTimeout = 40;
//		SqlDataReader dr = cmd.ExecuteReader();
//		do 
//		{
//			while(dr.Read())
//			{
//				System.Diagnostics.Debug.WriteLine(dr["LastName"]);
//			}
//		} while(dr.NextResult());
//		con.Close();
// 
//	Add CheckBoxList comparisons to all control methods
//  Evaluation.Formulas	(Test out formulas)
//	Avoli.Timing.AutoRun				// Done but needs including into scheduler
//	Change ALL routines to check string values as: if (objSmartClient.ConfigFilePath == "" || objSmartClient.ConfigFilePath == String.Empty || objSmartClient.ConfigFilePath == null)
//	Avoli.Syndication.ExchangeRates	...	http://finance.yahoo.com/currency?u
//	Avoli.Syndication.Market			...	http://finance.yahoo.com/mo?u
//	Avoli.IO.Files.FileType (A method of determine file type (XML v.s. INI) where the file extension is the same)
//  Fingerprint Scanning Method
//	frmClassGenerator = Works for MS Access.  Get it working with SQL Server (Avoli.SmartClient objSmartClient = new Avoli.SmartClient();)
//						Server, UserName, Password, Database, Namespace
//	Syndication: Comics & UPSTracker (http://pscode.com/vb/scripts/ShowCode.asp?txtCodeId=4781&lngWId=10)
//
// NEEDS FINISHING
// Combine: Avoli.Arrays.LocateInArray & Avoli.Arrays.LocateInArrayFuzzy
// Avoli.Time.WorldClock.LocationTime <-- SQL Server table [General Countries] needs to contain Time Zone & this routine modified & tested
// Avoli.Syndication.NewsReader.FeedToDatabase(objSmartClient, Avoli.Syndication.NewsReaderChannels.Top_Stories);
// Avoli.SystemInfo.Drives.DriveHide()
// Avoli.SystemInfo.Drives.DriveShow()
// Avoli.Net.POP3.GetMail (Split out only important values TO/FROM/DATE/SUBJECT/BODY from the mail text into PROPERTY values)
// Avoli.DataStorage.Database.Exporter.ToFixedWidthFile(strConnection, "select * from customers order by custid", @"c:\documents and settings\owner\my documents\Avoli.exports\fixed.txt")
// Avoli.DataStorage.File.Binary
// Avoli.IO.Files.Conversions.FromCSVFileToTextFixedWidth
// ** Avoli.Net.WinSock
// Avoli.Controls.Standard.General.SelectExactValues
// string strWeatherConditions = Avoli.DataStorage.Syndication.Weather("Greensboro", "North Carolina");
// Avoli.Notifications.Email.InstantMessaging
// Avoli.Syndication.NewsReader.FeedToDatabase(objSmartClient, Avoli.Syndication.NewsReaderChannels.Top_Stories);
// Avoli.DataStorage.Database.SQLServer.CrossTable.TableCopy
// Avoli.Scripting
//
// NEEDS FIXING
// 1) Stamping
// NOTE: The Remove or Write are screwing up the exe.  Perhaps it's the strDelimToken???
// bool blnResult = Avoli.DataStorage.Stamp.Remove(@"c:\avoli\testfiles\test.exe", true);
// blnResult = Avoli.DataStorage.Stamp.Write(@"c:\avoli\testfiles\test.exe", "superman", true, false);
// string strStamp = Avoli.DataStorage.Stamp.Read(@"c:\avoli\testfiles\test.exe");
//
// 2) Avoli.DataStorage.File.XML.ToStringArrayRange (Not locating the XML path passed)
// 3) Avoli.DataStorage.Database.Exporter.ToHTMLTable (Utilize a conversion for Fontsize (TwipsPerPixelX) & Landscape/Portrait to get accurate LPP)
// 4) Avoli.DataEntry.Populate(dsInfo, 0, gbCompanies); <--- Needs to check DataReader.GetSchema's AllowDBNull & Incremental field
//    I need to figure out how to convert a DataSet to a DataReader to do this.
// 5) *** Change all Application Name references to use System.Application.GetName (TEST everything including LiveUpdate)
// 6) RegExp not working
//			Avoli.SuperString ss = "Are the Ardvarks on the Attack?";
//			string[] sa = ss.StringGetAllWordsStartingWith("A");
//
// NEW METHODS
// Monitors.MineProfiles
//		(Search company URL for profile information)
//		(Search any company patents - http://patft.uspto.gov/netahtml/PTO/search-bool.html)
//		(Search any company trademarks - http://tess2.uspto.gov/bin/gate.exe?f=search@state=kh7int.1.1)
//		(Stock quoter... string[,] saQuoteInfo = Avoli.Syndication.Stocks.GetQuoteToStringArray("TGIC");)
//		(IMPORTANT: Monitor profiling url's need to be in a file which LiveUpdate can pick up)
//
// NEEDS TESTING
// Avoli.Monitors.MineFiles		(Get it kicking off Avoli.Batch.Emails etc.)
// Avoli.Monitors.MineFiles	(Test out each type)
// Avoli.Monitors.MineData		(Used for exporting data on a timed basis)
// Avoli.Forms.Collection.ToPrinter("");
// Avoli.IO.Import.FixedWidthToArray(@"c:\avoli\testfiles\fixed.txt", @"c:\avoli\testfiles\fixed.met");
// Avoli.Net.MapDrive
// Avoli.Net.UnMapDrive
//
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion


#region Avoli Data Entry GUI Population From XML (Avoli.DataStorage.File.XML.Control)
// This allows default control values to be located conveniently in an XML file
// <dataselector>
//		<cboDatabases>Wilmington|Portsmouth|Toledo</cboDatabases>
//		<cboCategories>Trip|Recurring PCS|Patient Evaluation|Email or Fax</cboCategories>
// </dataselector>

// Syntax: 
// this.Visible = true;
// this.Refresh();
// Avoli.Controls.Populate.FromXML(out strStopWatch, this, objSmartClient.ConfigFilePath, "//main/dataselector");
#endregion

#region Avoli Data Entry GUI Population from XML (Avoli.DataStorage.File.XML.ToControlByNodeName)
// This allows default control values to be located conveniently in an XML file
// <flavors>
//		<flavor>French Vanilla</flavor>
//		<flavor>Gorilla Grape</flavor>	
// </flavors>

// Syntax: 
// this.Visible = true;
// this.Refresh();
// Avoli.DataStorage.File.XML.ToControl(@"c:\test.xml", "", cboStates);
// Avoli.DataStorage.File.XML.ToControl(@"c:\test.xml", "", dataGrid1);
// Avoli.DataStorage.File.XML.ToControl(@"c:\countries.xml", "flavors", treeView1);
// Avoli.DataStorage.File.XML.ToControl(@"c:\test.xml", "", cboReports);
#endregion

#region Avoli Data Entry GUI Population from XML (Avoli.DataStorage.File.XML.ToControlByNameValues)
// This allows default control values to be located conveniently in an XML file
// <cbotablesvalues>
//		<value>Tag Companies</value>
//		<value>Tag Plans</value>
// </cbotablesvalues>

// Syntax: 
// lblMessages.Text = "Enforcing XML control default values...";
// lblMessages.Refresh();
// Avoli.DataStorage.File.XML.ToControlByNameValues(this, null); // Search for any form control XML based values
// lblMessages.Text = "Idle";
// lblMessages.Refresh();
#endregion

#region Avoli Data Entry GUI Population (Single or Multiple datatable dataset method)
//	string[] saPopulationResults = {"sp_select_general_user_by_id_all " + lstUsers.SelectedValue.ToString()};
//	string[] saDataTableNames = {"userinfo"};
//	objSmartClient.DataSet = Avoli.DataStorage.Database.General.GetDataSet(objSmartClient, saPopulationResults, saDataTableNames);
#endregion

#region Avoli Data Entry Validations
//	private void form_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
//	{	// this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_KeyPress);
//
//		if (e.KeyChar == 13)
//		{
//			System.Windows.Forms.SendKeys.Send("{TAB}");
//			e.Handled = false;
//		}
//		else if (e.KeyChar == 27)
//		{
//			picCloseForm_Click(sender, e);
//			e.Handled = false;
//		}
//		else
//		{
//			// Chances are the keypress is coming from a control who's
//			// Parent is a GroupBox then we're to evalate the keypress
//			if (this.ActiveControl.Parent.Name.ToString().StartsWith("gb") && dsInfo != null)
//			{
//				this.ActiveControl.Refresh();
//
//				// Validate excluding certain control types
//				bool blnValidateThisControl = true;
//				if (this.ActiveControl is System.Windows.Forms.ComboBox)
//					blnValidateThisControl = false;
//				if (blnValidateThisControl)
//					Avoli.Controls.Standard.Validate(dsInfo, 0, this, e.KeyChar.ToString(), lblMessages);
//			}
//		}
//	}
//
//	private void form_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) 
//	{	// this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
//
//		// Chances are the keypress is coming from a control who's
//		// Parent is a GroupBox then we're to evalate the keypress
//		try
//		{
//			if (this.ActiveControl.Parent.Name.ToString().StartsWith("gb"))
//			{
//				this.ActiveControl.Refresh();
//
//				// Validate excluding certain control types
//				bool blnValidateThisControl = true;
//				if (this.ActiveControl is System.Windows.Forms.ComboBox)
//					blnValidateThisControl = false;
//				if (blnValidateThisControl)
//				Avoli.Controls.Standard.Validate(dsInfo, 0, this, e(), lblMessages);
//			}
//		}
//		catch {}
//	}
#endregion


#region COOL PRODUCT NAMES
// Cheetah
// Javelin
// Blast
// RedDragon
// Velocity
// Revolver
// Conduit (Content Syndication)
// Tinderbox
// Itinerary
// Release Canidate
// Install Kits
// Matterhorn
// Sanitation
// RPM
// Perpetuum
//
// Smurf Attack
// Ping of Death
// Man in the Middle Attack
// Dictionary Attack
#endregion

#region REVISIONS HISTORY
//                                           ''~``
//                                          ( o o )
//+-------------------------------------oooO--(_)--Oooo------------------------------------+
//     History: Date			    Description of Change
//     -------------------------    --------------------------------------------------------
//              July 6, 2011        Added Crypto.LetterShift.Transform method
//              July 20, 2009       Added Retries count with 1 second delay on many File Read Methods (An instance came up where files were being locked by the authoring app upon writes and our file read apps were butting heads with it)
//              June 18, 2009       Modified Calculations.GetVariance to allow for the more popular standard deviation variance
//              June 16, 2009       Modified Avoli.DataEntry.SavePictureBoxesToDB for null image check
//              June 15, 2008       Created Captcha Hive (images creation)
//              May 29, 2008        Created HideConnectionStringPassword
//              May 15, 2008        Revised docked panels to have transparency for themes
//              October 28, 2006    Converted to .Net Framework 2.0 & Visual Studio 2005
//				July 29, 2006	    Added Batch class
//				July 28, 2006	    Fix bug in ShuffleByWidth (Was an array problem)
//				July 22, 2006	    Added EmailSignature to SMTPSend
//		    	Jan 23, 2006	    Avoli.Applications.SystemTrayInit
//			    Jan 22, 2006	    Avoli.Security.Crypto.MorseCode
//				Jan 15, 2006	    Avoli.SmartClient.UserEmailAddress
//				Nov 22, 2005	    Avoli.Arrays.BubbleSortStringArray
//				Nov 22, 2005	    Avoli.Arrays.BubbleSortIntArray
//				Nov 22, 2005	    Avoli.DataStorage.File.TXT.QuoteOfTheDay
//				Nov 22, 2005	    Avoli.DataStorage.File.TXT.LinesPrefixedByString
//				Nov 21, 2005	    Avoli.Randomizer.LineFromFile
//				Nov 21, 2005	    Avoli.Colors
//				Nov 6, 2005		    Eval
//				Nov 5, 2005		    Avoli.Controls.Standard.GUISave
//				Nov 5, 2005		    Avoli.Controls.Standard.GUIPopulate
//				Nov 3, 2005		    Avoli.Controls.Standard.GUIPopulateDetectedDropdowns
//				Nov 3, 2005		    Avoli.Controls.Children.Clear.All
//				Oct 27, 2005	    Avoli.IO.Files.Touch
//				Oct 17, 2005	    Avoli.DataStorage.Stamp.Validate
//				Oct 17, 2005	    Avoli.DataStorage.Stamp.Remove
//				Oct 17, 2005	    Avoli.DataStorage.Stamp.Read
//				Oct 17, 2005	    Avoli.DataStorage.Stamp.Write
//				Oct 17, 2005	    Avoli.DataStorage.Stamp
//				Oct 17, 2005	    Avoli.DataStorage.Syndication.Quote
//				Oct 17, 2005	    Avoli.DataStorage.Syndication.Weather
//				Oct 17, 2005	    Avoli.DataStorage.Syndication
//				Oct 17, 2005	    Avoli.Net.WinSock
//				Oct 16, 2005	    Avoli.Exceptions.Severity
//				Oct 15, 2005	    Avoli.Imaging.ToGrayScale
//				Oct 15, 2005	    Avoli.IO.Files.Conversions.FixedWidthToArray
//				Oct 14, 2005	    Avoli.Exceptions.Publish
//				Oct 14, 2005	    Avoli.DataStorage.File.XML.Read
//				Oct 12, 2005	    Avoli.DataStorage.File.XML.DirectoryLister.getXMLString
//				Oct 11, 2005	    Avoli.DataStorage.File.XML.FormattedString
//				Oct 11, 2005	    Avoli.SystemInfo.Processes.KillBlackListedInstances
//				Oct 7, 2005		    Avoli.Timing.AutoRun
//				Oct 6, 2005		    Avoli.Controls.Standard.ClearChildControlsByType
//				Oct 5, 2005		    Avoli.Controls.Standard.ClearChildControlsByName
//				Oct 5, 2005		    Avoli.Controls.Standard.MoveSelected
//				Oct 3, 2005		    Avoli.Controls.Standard.ToStringContains
//				Oct 3, 2005		    Avoli.Timing.AutoRun
//				Sept 30, 2005	    Avoli.SystemInfo.Services.EnumToControl
//				Sept 27, 2005	    Avoli.Arrays.GrowArray
//				Sept 26, 2005	    Avoli.SuperString.StringInsertSpacesBeforeUpper
//				Sept 25, 2005	    Avoli.SystemInfo.Drives.IsBelowSpaceThreshold
//				Sept 20, 2005	    Avoli.SystemInfo.Drives.GetDriveInfo
//				Sept 19, 2005	    Avoli.DataStorage.LocalSystem.Registry
//				Sept 17, 2005	    Avoli.DataStorage.LocalSystem.EventLog
//				Sept 16, 2005	    Avoli.IO.Files.Corrupt (Fixed to corrupt on original file length)
//				Sept 15, 2005	    Avoli.DataStorage.File.INI.ViewSection
//				Sept 15, 2005	    Avoli.DataStorage.File.INI.RemoveSection
//				Sept 13, 2005	    Modified TabbedToTreeView to allow for BLANK & \t lines
//				Sept 13, 2005	    Avoli.IO.Files.TabbedToTreeView
//				Sept 11, 2005	    Avoli.SuperString.HTMLSourceFromURL
//				Sept 11, 2005	    Avoli.SuperString.URLAvailable
//				Sept 10, 2005	    Avoli.DataStorage.File.INI.SectionExists
//				Sept 10, 2005	    Avoli.DataStorage.File.INI.ToArray
//				Sept 10, 2005	    Avoli.DataStorage.File.INI.ToControl
//				Sept 09, 2005	    Avoli.Arrays.ReDim
//				Sept 09, 2005	    Avoli.DataStorage.File.INI.Write
//				Sept 09, 2005	    Avoli.DataStorage.File.INI.Read
//				Sept 09, 2005	    Moved DevTools class ViewArray routines to the Arrays class
//				Sept 03, 2005	    Avoli.Imaging.ScreenCapture(this, @"c:\captured.jpg");
//				Sept 03, 2005	    Started documenting changes
//
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region EXAMPLE CONNECTION STRINGS (All Tested)
// string strConnection = "Data source=bappnts29ph; persist security info=False; initial catalog=nhtr; user id=papers; password=xxxxx; Connection Timeout=8";
// string strConnection = @"Data source=LAP5V1L5Q1\AVOLI; persist security info=False; initial catalog=dms; user id=avoli; password=xxxxx; Connection Timeout=8";
#endregion

#region SOFTWARE DEVELOPMENT PRACTICES
//                                           \\|//
//                                          ~~O-O~~
//                                         ////^\\\\
//+-------------------------------------oOOo--(_)--oOOo------------------------------------+
// Core Practices Encompassed
// 1)  Paired Programming (Where 2 programmers work together at one keyboard)
//      BENEFITS
//      Design quality: Shorter programs, better designs, fewer bugs.[2] Program code must be readable to both partners, not just the driver, in order to be checked in. Pairs typically consider more design alternatives than programmers working solo, and arrive at simpler, more-maintainable designs, as well as catch design defects very early.[3] 
//      Reduced cost of development: Bugs being a particularly expensive part of software development, especially if they're caught late in the development process, the large reduction in defect rate due to pair programming significantly reduces software development costs.[2] 
//      Learning and training: Knowledge passes easily between pair programmers: they share knowledge of the specifics of the system, and they pick up programming techniques from each other as they work.[2][4] New hires quickly pick up the practices of the team through pairing.[5] 
//      Overcoming difficult problems: Pairs often find that seemingly "impossible" problems become easy or even quick, or at least possible, to solve when they work together.[2][6] 
//      Improved morale: Programmers report greater joy[7] in their work and greater confidence that their work is correct.[2] 
//      Decreased management risk: Since knowledge of the system is shared among programmers, there is less risk to management if one programmer leaves the team.[2] 
//      Increased discipline and better time management: Programmers are less likely to skip writing unit tests, spend time web-surfing or on personal email,[8] or other violations of discipline, when they are working with a pair partner. The pair partner "keeps them honest".[9][10] 
//      Resilient flow. Pairing leads to a different kind of flow than programming alone, but it does lead to flow.[citation needed] Pairing flow happens more quickly: one programmer asks the other, "What were we working on?" Pairing flow is also more resilient to interruptions: one programmer deals with the interruption while the other keeps working. 
//      Fewer interruptions: People are more reluctant to interrupt a pair than they are to interrupt someone working alone.[11] 
//      Fewer workstations required: Since two people use one workstation, one fewer workstation is required. 
//
//      DRAWBACKS
//      Developer egos: Experienced developers may find it tedious to tutor a less experienced developer in a paired environment. 
//      Developer intimidation: A less experienced developer may feel intimidated pairing with a more experienced developer which may result in less participation. 
//      Developer work preference: Some engineers prefer to work alone, and may find the paired environment cumbersome. 
//      Tutoring cost: Experienced developers working alone may be capable of producing code that is clean and accurate at the outset, and the additional theoretical gain from pairing might not be worth the cost of an additional developer. This is especially true when producing more trivial parts of the system.[citation needed] 
//      Potential conflict: Differences in coding style may result in conflict, and personality conflicts can result in one or both developers feeling awkward or uncomfortable. 
//      Chat sessions: Sometimes employees might talk together too much, straying excessively into off-topic subjects, such as major news events, personal problems, etc. 
//      Annoying personal habits: Sometimes people can find each other much more annoying when working up close than at separate workstations. 
//
// 2)  Continuous Integration
//      MAINTAIN A CODE REPOSITORY
//      This practice advocates the use of a revision control system for the project's source code. All 
//      artifacts that are needed to build the project should be placed in the repository. In this practice and 
//      in the revision control community, the convention is that the system should be buildable from a fresh 
//      checkout and not require additional dependencies. Fowler also mentions that where branching is supported 
//      by tools, its use should be minimised. Instead, it is preferred that changes are integrated rather 
//      than creating multiple versions of the software that are maintained simultaneously. The mainline 
//      (or trunk) should be the place for the working version of the software.
//
//      AUTOMATE THE BUILD
//      The system should be buildable using a single command. Many build tools exist, such as make, which 
//      has existed for many years. Other more recent tools like Ant or MSBuild are frequently used in Continuous 
//      Integration environments. Automation of the build should include automating the integration, which often includes deployment into a production-like environment. In many cases, the build script not only compiles binaries, but also generates documentation, website pages, statistics and distribution media (such as Windows MSI files or RPM files).
//
//      MAKE YOUR BUILD SELF-TESTING
//      This touches on another aspect of Extreme programming: Test-driven development. The system should 
//      be written with tests that verify that it performs as intended. These tests should be run as part of the 
//      build. Automated tests are increasingly valuable as the system ages.
//
//      EVERYONE COMMUNICATES EVERY DAY
//      By committing regularly, every committer can reduce the number of conflicting changes. Checking 
//      in weeks worth of work runs the risk of conflicting with other features and can be very difficult to
//      solve. Early, small conflicts in an area of the system cause team members to communicate about the 
//      change they are making.
//
//      BUILD VERIFICATION
//      Commits to the current working version should be built to verify they have been integrated 
//      correctly. A common practice is to use Automated Continuous Integration, although this may be done 
//      manually. In fact, some prefer this approach. For many, continuous integration is synonymous with 
//      using Automated Continuous Integration where a continuous integration server or daemon monitors 
//      the version control system for changes, then automatically runs the build process.
//
//      KEEP THE BUILD FAST
//      The build needs to be fast, so that if there is a problem with integration, it is quickly identified.
//
//      TEST IN A CLONE OF THE PRODUCTION ENVIRONMENT
//      Having a test environment can lead to failures in tested systems when they are deployed to 
//      the production environment, because the production environment may differ from the test environment 
//      in a significant way.
//
//      MAKE IT EASY TO GET THE LATEST DELIVERABLES
//      Making builds readily available to stakeholders and testers can reduce the amount of rework 
//      necessary when rebuilding a feature that doesn't meet requirements. Additionally, early testing reduces 
//      the chances that defects survive until deployment. Finding issues earlier also, in some cases, reduces 
//      the amount of work necessary to resolve them.
//
//      EVERYONE CAN SEE THE RESULTS OF THE LATEST BUILD
//      It should be easy to find out whether the build is broken and who made the change.
//
// 3)  Test Driven Development via Test First Programming
//      ADD A TEST (Focus on requirements before writing code)
//      ----------
//      In test-driven development, each new feature begins with writing a test. In order to write 
//      a test, the developer must understand the specification and the requirements of the feature clearly. 
//      This may be accomplished through use cases and user stories to cover the requirements and exception 
//      conditions. This could also imply an invariant, or modification of an existing test. This is a 
//      differentiating feature of test-driven development versus writing unit tests (NUnit) after the code is 
//      written: it makes you focus on the requirements before writing the code, a subtle but important 
//      difference.
//
//      RUN ALL TESTS AND SEE THE NEW ONES FAIL
//      ---------------------------------------
//      This validates that the test harness is working correctly and that the new test does not mistakenly pass without requiring any new code.
//      The new test should also fail for the expected reason. This step tests the test itself, in the negative. A "negative test" is something familiar to testers, to make sure a feature fails when it should fail (e.g. bad input data). It typically follows or is "paired" with one or more "positive tests" that make sure things work as expected (e.g. good input data). ("Make sure it works, then change one thing to make it break and make sure it breaks.") The entire suite of unit tests act to serve this need, cross-checking each other to make sure "negative tests" fail for the expected reasons.
//      This technique avoids the syndrome of writing tests that always pass, and therefore aren't worth much. Running the new test to see it fail the first time is a vital "sanity check".
//
//      WRITE SOME CODE FOR THE TEST
//      ----------------------------
//      The next step is to write some code that will cause the test to pass. The new code written at 
//      this stage will not be perfect and may, for example, pass the test in an inelegant way. That is 
//      acceptable because later steps will improve and hone it.
//
//      RUN THE AUTOMATED TESTS AND SEE THEM SUCCEED
//      --------------------------------------------
//      If all test cases now pass, the programmer can be confident that the code meets all the tested 
//      requirements.
//
//      REFACTOR CODE
//      --------------
//      Now the code can be cleaned up as necessary. By re-running the test cases, the developer can be 
//      confident that refactoring is not damaging any existing functionality. The concept of removing duplication
//      is an important aspect of any software design.
//
// 4)  Acceptance Testing (Customer/Owner or Business Unit testing)
//      User Acceptance Testing (UAT) is a process to obtain confirmation by a Subject Matter Expert (SME), 
//      preferably the owner or client of the object under test, through trial or review, that the modification 
//      or addition meets mutually agreed-upon requirements. In software development, UAT is one of the final 
//      stages of a project and often occurs before a client or customer accepts the new system.
//
// 5)  Real User Involvement (duh!)
// 6)  Incremental Deployment (Integration / Deployment / Packaging)
// 7)  Team Continuity (Code Stewardship - Ethical accountability and forward thinking / User Bill of Rights / Developer Bill of Rights)
// 8)  Root Cause Analysis (By directing corrective measures at root causes, it is hoped that the likelihood of problem recurrence will be minimized.)
// 9)  Single Code Base (repository)
// 10) Rule of law (Know the business of your business - If it requires you follow law/regulations such as FDA, follow it!)
//
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region KEY ROLES
// Line of Business	(Drives initiatives through need)
// Manager			(Oversee overall corporate vision through project directives to subordinates while with team development, delegate to lead)
// Lead				(Lead role core team communication, specs/feasibility determination, planning, managing/directing, oversee deligated tasks
//					 delegating, communicate to manager status, track items via issue log, maintain support initiatives,
//					 and in some instances development)
// Development		(Work through lead developers specs towards completion of initiatives via the issue log)
// Testing			(Test creation, execution, tracking, documentation)
// System of Record	(Avoli.CaptureDocs or another document management system)
#endregion

#region GMP - Good Manufacturing Practices Documentation (FDA - 21 CRF Parts 210 & 211)
/*                                           \\|//       
 *                                          ~~O-O~~
 *                                         ////^\\\\
 *+-------------------------------------oOOo--(_)--oOOo------------------------------------+
 * SYSTEM INFORMATION
 * System Name: Application Name goes here
 * System ID:   APP01
 *  
 * 1) What is the system?       
 * 2) Where will it be used?    
 * 3) How will it be used?      
 *+----------------------------------------------------------------------------------------+
 * STEP 1: CHANGE REQUEST HISTORY
 * DATE         DOCUMENT #      CR#         DESCRIPTION OF APPROVED CHANGE
 * 06/17/2008   HP08-324        CR07-010    Changed ABCD
 *                                          Tested / Promoted to Staging for Testing Group
 * 06/18/2008   HP08-324                    Fixed Deviations found by Testing Group
 *                                          Tested / Promoted to Staging for Testing Group
 * 
 * STEP 2: URS - User Requirements Specification (Created by System Owner/Business Analyst)
 * (IT provides review ONLY on this document)
 * 1.0	PURPOSE
 * 2.0	SCOPE
 * 3.0	REGULATORY REFERENCES
 * 4.0	RESPONSIBILITY / ORGANIZATIONAL STRUCTURE
 * 5.0	DEFINITIONS / ACRONYMS
 * 6.0	RELATED DOCUMENTS
 * 7.0	COMPUTER SYSTEM DESCRIPTION
 * 7.1	CURRENT BUSINESS / SYSTEM PROCESS
 * 7.2	PROPOSED BUSINESS / SYSTEM PROCESS
 * 8.0	USER REQUIREMENTS
 * 8.1	GENERAL REQUIREMENTS
 * 8.2	HARDWARE REQUIREMENTS
 * 8.3	ADDITIONAL SOFTWARE REQUIREMENTS
 * 8.4	REPORTS
 * 8.5	DATA
 * 8.6	SECURITY
 * 8.7	ELECTRONIC RECORDS & SIGNATURES
 * 8.8	INTERFACES
 * 8.9	MAINTENANCE AND SYSTEM ADMINISTRATION
 * 9.0	ATTACHMENTS
 * 
 * STEP 2.2: FRS - Functional Requirements Specification (Created by IT)
 * 1.0	PURPOSE
 * 2.0	SCOPE
 * 3.0	REFERENCES / RELATED DOCUMENTS
 * 4.0	RESPONSIBILITIES
 * 4.1	AUTHOR/ORIGINATOR OR DESIGNEE
 * 4.2	SYSTEM OWNER
 * 4.3	COMPUTER VALIDATION
 * 4.4	QUALITY ASSURANCE
 * 4.5	INFORMATION TECHNOLOGY
 * 5.0	DEFINITIONS / ACRONYMS
 * 6.0	SYSTEM DESCRIPTION
 * 7.0	ASSUMPTIONS AND CONSTRAINTS
 * 7.1	SYSTEM / BUSINESS OWNER
 * 7.2	XXX
 * 8.0	REQUIREMENT AND SPECIFICATION TABLE
 * 8.1	GENERAL FUNCTIONS
 * 8.2	REPORTING
 * 8.3	DATA
 * 8.4	SECURITY
 * 8.5	REGULATORY
 * 8.6	INTERFACE
 * 8.7	DOCUMENTATION
 * 8.8	ENVIRONMENT
 * 8.9	USER SUPPORT AND TRAINING
 * 8.10 MAINTENANCE AND SYSTEM ADMINISTRATION
 * 8.11 AVAILABILITY
 * 
 * STEP 2.3: SDS - System Design Specifications (Created by IT)
 * 1.0	PURPOSE
 * 2.0	SCOPE
 * 3.0	REFERENCES / RELATED DOCUMENTS
 * 4.0	RESPONSIBILITIES
 * 4.1	AUTHOR/ORIGINATOR OR DESIGNEE
 * 4.2	SYSTEM OWNER
 * 4.3	COMPUTER VALIDATION
 * 4.4	QUALITY ASSURANCE
 * 4.5	INFORMATION TECHNOLOGY
 * 5.0	DEFINITIONS / ACRONYMS
 * 6.0	SYSTEM DESCRIPTION
 * 7.0	REQUIREMENT AND SPECIFICATION TABLE
 * 7.1	SAFETY
 * 7.2	COMPUTER / AUTOMATED SYSTEM RESOURCE
 * 7.3	COMPUTER / AUTOMATED SYSTEM HARDWARE CONFIGURATION
 * 7.4	SOFTWARE DESIGN CONFIGURATION
 * 7.5	SOFTWARE COMPONENT
 * 7.6	MODULE/COMPONENT CONFIGURATION
 * 7.7	MODULE/COMPONENT DETAILS
 * 7.8	ERROR AND INTERLOCKS
 * 7.9	DATA DESIGN
 * 7.10 DATABASE DESCRIPTION
 * 7.11 LOGICAL SECURITY & AUDIT TRAILS
 * 7.12 PHYSICAL SECURITY
 * 7.13 NETWORK DESIGN CONFIGURATION
 * 
 * STEP 3: IQ - Installation Qualification Document
 * 1.0	PURPOSE
 * 2.0	BACKGROUND
 * 3.0	SYSTEM DESCRIPTION
 * 4.0	SYSTEM ARCHITECTURE
 * 5.0	RESPONSIBILITIES
 * 5.1	AUTHOR/ORIGINATOR OR DESIGNEE
 * 5.2	SYSTEM OWNER
 * 5.3	COMPUTER VALIDATION
 * 5.4	QUALITY ASSURANCE
 * 5.5	INFORMATION TECHNOLOGY
 * 6.0	PROCEDURE
 * 7.0	SIGNATURE IDENTIFICATION FORM
 * 8.0	INSTALLATION QUALIFICATION
 * 8.1	PREREQUISITE REFERENCE VERIFICATION
 * 8.2	MANUALS / TECHNICAL DOCUMENTATION VERIFICATION
 * 8.3	FLOW DIAGRAM (IT)
 * 8.4	SOFTWARE /HARDWARE VERIFICATION (IT)
 * 8.5	UTILITY REQUIREMENTS VERIFICATION (IT)
 * 8.6	ENVIRONMENTAL REQUIREMENTS (IT)
 * 8.7	SYSTEM BACKUP VERIFICATION (IT)
 * 8.8	HARDWARE INSTALLATION (IT)
 * 8.9	INPUT/OUTPUT (I/O) VERIFICATION (IT)
 * 8.10	PREVENTIVE MAINTENANCE VERIFICATION (IT)
 * 8.11	CODE REVIEW VERIFICATION (IT)
 * 8.12	DATA BASE MODIFICATION (IT)
 * 8.13	SOFTWARE INSTALLATION (IT)
 * 9.0	DEVIATION REPORT FORM
 * 10.0	ATTACHMENT LIST	28
 * 
 * STEP 4: OQ - Operational Qualification Document
 * 1.0	PURPOSE
 * 2.0	BACKGROUND
 * 3.0	SYSTEM DESCRIPTION
 * 4.0	SYSTEM ARCHITECTURE
 * 5.0	RESPONSIBILITIES
 * 6.0	PROCEDURE
 * 7.0	SIGNATURE IDENTIFICATION FORM
 * 8.0	PREREQUISITE REFERENCE VERIFICATION
 * 9.0	OPERATIONAL QUALIFICATION
 * 9.1	Policies, Procedures, SOP’s and WI’s Documentation Verification
 * 9.2	Power Failure Verification (IT)
 * 9.3	Security Verification (IT)
 * 9.4	Functional Testing Verification (IT - Permissions Specific)
 * 10.0	DEVIATION REPORT FORM
 * 11.0	ATTACHMENT LIST
 * 
 * STEP 5: PQ - Performance Qualification (Live test with owner)
 * 1.0	PURPOSE
 * 2.0	BACKGROUND
 * 3.0	SYSTEM DESCRIPTION
 * 4.0	SYSTEM ARCHITECTURE
 * 5.0	RESPONSIBILITIES
 * 6.0	PROCEDURE
 * 7.0	SIGNATURE IDENTIFICATION FORM
 * 8.0	PREREQUISITE REFERENCE VERIFICATION
 * 9.0	OPERATIONAL QUALIFICATION
 * 9.1	Policies, Procedures, SOP’s and WI’s Documentation Verification
 * 9.2	Power Failure Verification
 * 9.3	Security Verification
 * 9.4	Functional Testing Verification
 * 10.0	DEVIATION REPORT FORM
 * 11.0	ATTACHMENT LIST
 * 
 * STEP 6: TM - Traceability Matrix (Tracking requirements & validations)
 * 1.0	PURPOSE
 * 2.0	SCOPE
 * 3.0	DELIVERABLE
 * 4.0	XXX (ENTER HEADING)
 * 4.1	XXX SUMMARY TABLE
 * 4.2	XXX DISCREPANCY
 * 5.0	XXX (ENTER HEADING)
 * 5.1	XXX SUMMARY TABLE
 * 5.2	XXX DISCREPANCY
 *                                        .oooO
 *                                       (   )   Oooo.
 *+---------------------------------------\ (----(   )-------------------------------------+
 *                                         \_)    ) /
 *                                               (_/
 * 
 * */
#endregion

#region SOP - Standard Operating Procedures (FDA - 21 CRF Parts 210 & 211)
// ACCESS CONTROL
// 1.		The system will ensure that only authorized individuals can use the system.	
// 2.		The system will allow for different level of access based on user responsibilities (e.g. user, administrator). 	
// 3.		If the system requires sequenced steps, the system must include operational checks to ensure that the actions are performed in the correct sequence. 	
// 4.		If it is a requirement of the system that data input or instructions can only come from specific input devices (e.g. instruments, terminals); the system must check for the correct device.	
//
// AUDIT TRAIL
// 5.		An electronic audit trail function must be automatically generated for all operator entries.	
// 6.		The audit trail must completely be outside the control and access of users (except for read only access of the audit trail file).	
// 7.		It must be impossible for the user to disable the audit trail function.	
// 8.		The operator name, date, time and indication of record (or file) creation, modification must be recorded in the audit trail.	
// 9.		If the predicate regulation requires it, the reason for a change must be included in the audit trail.	
// 10.		The system date and time must be protected from unauthorized change.	
// 11.		When data is changed or deleted, all of the previous values must be electronically available.	
// 12.		The audit trail data must be protected from accidental or intentional modification or deletion (read-only access).	
// 13.		The electronic audit trails must be maintained and retrievable for at least as long as its respective electronic records.	
// 14.		The electronic audit trails must be readily available for inspections and audits.	
// 15.		Selected portions of the audit trail must have the capability to be viewed and printed by inspectors.	
// 16.		Selected portions of the audit trail must be capable of being extracted in a transportable electronic format that can be read by regulatory agencies.	
//
// INSPECTION OF RECORDS
// 17.		The system will be able to detect invalid records where applicable (e.g. invalid field entries, fields left blank that should contain data, values outside of limits).	
// 18.		The system will allow viewing of the entire contents of the records.	
// 19.		The system will allow printing of the entire contents of the records.	
// 20.		The system will allow generation of all records electronically in a format that can be put on a portable medium (e.g. diskette or CD) or transferred electronically.	
//
// RECORD RETENTION
// 21.		The electronic records must be protected against intentional or accidental modification or deletion.	
// 22.		If the data is archived off the system the audit trail must be archived as well and all of the archived data must be accurately retrieved even after system upgrades.	
//
// ELECTRONIC SIGNATURES (if applicable)	
// 23.		All electronically signed records must contain the following information associated with the signing: (1) full printed name of the signer (2) date and time of signing (3) meaning of signature (e.g. review, approval).	
// 24.		The date and time stamps must be applied automatically (vs. keyed in by the user)	
// 25.		The date and time stamp must be derived in a consistent way in order to be able to reconstruct the sequence of events.	
// 26.		Changes to signatures must be included in the audit trail.	
// 27.		The printed name, date, time and the signature meaning must appear in every human readable form of the electronic record (e.g. all screens and printed reports).	
// 28.		The electronic signatures must be linked (via technology, not procedures) to their corresponding electronic records to ensure that the signature can not be excised, copied, or otherwise transferred falsify an electronic record by any means.	
// 29.		The system must verify that each electronic signature must be unique to one individual.	
// 30.		The system must verify that electronic signature must not ever reused by, or reassigned to, anyone other than the original owner.	
// 31.		The system must verify that the individual has the authority to electronically sign a record before allowing to do so.	
// 32.		For reissuing forgotten passwords, system must verify the requestor’s identity.
//
// NON-BIOMETRIC ELECTRONIC SIGNATURES (if applicable)	
// 33.		The signature must be made up of at least two distinct identification components, such as an identification code and password.	
// 34.		If continuous signing sessions are used, two electronic signature components are required for the initial signing and only one component is required for subsequent signing.	
// 35.		Two electronic signature components are required for each signing during a non-continuous signing session.	
// 36.		If a user leaves the workstation, there must be an automatic control ensures that it is treated as a non-continuous session.	
// 37.		There must be an automatic logoff, or password protected screen saver that launches, after a short period of inactivity.	
// 38.		Controls must be in place to maintain the uniqueness of each combined identification code and password, such that no two individuals can have the same combination of identification code and password.	
// 39.		Technical controls must be in place to prevent the re-use of identification codes.	
// 40.		The issuance of the identification codes and passwords must be periodically checked, recalled or revised by the system (e.g. password aging). 	
// 41.		After three unsuccessful attempts to unauthorized use the software must freeze the account and record each unsuccessful attempt in the audit trail.	
//
// BIOMETRIC ELECTRONIC SIGNATURES (if applicable)
// 42.		Biometric electronic signatures must be designed to ensure that they can be used only by their genuine owners.	
//
// TRAINING
// 43.		There must be documentation to show that persons who develop the system have the education, training and experience to perform their assigned tasks (including temporary and contract staff).	
#endregion

#region FDA 21 CFR Part 11 Software Validation Checklist
//The system or software you are providing to Avoli does fall under the guidelines and 
//regulations of the United States Food and Drug Administration.  We are therefore, required to know 
//how the system is in compliance with 21 CFR Part 11.  In order to find that, please response to the 
//attached 21 CFR Part 11 requirements for the system, sign and return it as soon as possible.
//
//    ACCESS CONTROL (Avoli.Security - TABLE: General Log & General History)
//1.		The system will ensure that only authorized individuals can use the system. (Avoli.User/Group Inherited Security)
//2.		The system will allow for different level of access based on user responsibilities (e.g. user, administrator). 	
//3.		If the system requires sequenced steps, the system must include operational checks to ensure that the actions are performed in the correct sequence. 	
//4.		If it is a requirement of the system that data input or instructions can only come from specific input devices (e.g. instruments, terminals); the system must check for the correct device.	
//
//    AUDIT TRAIL (Avoli.DataEntry.Save / Avoli.DataEntry.Update / Avoli.DataEntry.Delete - TABLE: General History)
//5.		An electronic audit trail function must be automatically generated for all operator entries.	
//6.		The audit trail must completely be outside the control and access of users (except for read only access of the audit trail file).	
//7.		It must be impossible for the user to disable the audit trail function.	
//8.		The operator name, date, time and indication of record (or file) creation, modification must be recorded in the audit trail.	
//9.		If the predicate regulation requires it, the reason for a change must be included in the audit trail.	
//10.		The system date and time must be protected from unauthorized change.	
//11.		When data is changed or deleted, all of the previous values must be electronically available.	
//12.		The audit trail data must be protected from accidental or intentional modification or deletion (read-only access).	
//13.		The electronic audit trails must be maintained and retrievable for at least as long as its respective electronic records.	
//14.		The electronic audit trails must be readily available for inspections and audits.	
//15.		Selected portions of the audit trail must have the capability to be viewed and printed by inspectors.	
//16.		Selected portions of the audit trail must be capable of being extracted in a transportable electronic format that can be read by regulatory agencies.	
//
//    INSPECTION OF RECORDS (SQL Server Database - TABLE: General History)
//17.		The system will be able to detect invalid records where applicable (e.g. invalid field entries, fields left blank that should contain data, values outside of limits).	
//18.		The system will allow viewing of the entire contents of the records.	
//19.		The system will allow printing of the entire contents of the records.	
//20.		The system will allow generation of all records electronically in a format that can be put on a portable medium (e.g. diskette or CD) or transferred electronically.	
//
//    RECORD RETENTION (SQL Server Database)
//21.		The electronic records must be protected against intentional or accidental modification or deletion.	
//22.		If the data is archived off the system the audit trail must be archived as well and all of the archived data must be accurately retrieved even after system upgrades.	
//
//    ELECTRONIC SIGNATURES (if applicable)	
//23.		All electronically signed records must contain the following information associated with the signing: 
//              (1) full printed name of the signer (2) date and time of signing (3) meaning of signature (e.g. review, approval).	
//24.		The date and time stamps must be applied automatically (vs. keyed in by the user)	
//25.		The date and time stamp must be derived in a consistent way in order to be able to reconstruct the sequence of events.	
//26.		Changes to signatures must be included in the audit trail.	
//27.		The printed name, date, time and the signature meaning must appear in every human readable form of the electronic record (e.g. all screens and printed reports).	
//28.		The electronic signatures must be linked (via technology, not procedures) to their corresponding electronic records to ensure that the signature can not be excised, copied, or otherwise transferred falsify an electronic record by any means.	
//29.		The system must verify that each electronic signature must be unique to one individual.	
//30.		The system must verify that electronic signature must not ever reused by, or reassigned to, anyone other than the original owner.	
//31.		The system must verify that the individual has the authority to electronically sign a record before allowing to do so.	
//32.		For reissuing forgotten passwords, system must verify the requestor’s identity.	
//
//    NON-BIOMETRIC ELECTRONIC SIGNATURES (if applicable)	
//33.		The signature must be made up of at least two distinct identification components, such as an identification code and password.	
//34.		If continuous signing sessions are used, two electronic signature components are required for the initial signing and only one component is required for subsequent signing.	
//35.		Two electronic signature components are required for each signing during a non-continuous signing session.	
//36.		If a user leaves the workstation, there must be an automatic control ensures that it is treated as a non-continuous session.	
//37.		There must be an automatic logoff, or password protected screen saver that launches, after a short period of inactivity.	
//38.		Controls must be in place to maintain the uniqueness of each combined identification code and password, such that no two individuals can have the same combination of identification code and password.	
//39.		Technical controls must be in place to prevent the re-use of identification codes.	
//40.		The issuance of the identification codes and passwords must be periodically checked, recalled or revised by the system (e.g. password aging). 	
//41.		After three unsuccessful attempts to unauthorized use the software must freeze the account and record each unsuccessful attempt in the audit trail.	
//
//    BIOMETRIC ELECTRONIC SIGNATURES (if applicable)
//42.		Biometric electronic signatures must be designed to ensure that they can be used only by their genuine owners.	
//
//    TRAINING
//43.		There must be documentation to show that persons who develop the system have the education, training and experience to perform their assigned tasks (including temporary and contract staff).	
#endregion

#region Events
//private void picClose_Click(object sender, System.EventArgs e) 
//{	// this.picClose.Click += new System.EventHandler(this.picClose_Click);
//}

//private void form_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) 
//{	// this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
//}

//private void form_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
//{	// this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_KeyPress);
//    if (e.KeyChar == 13)
//    {
//    }
//}

//private void listview1_SelectedIndexChanged(object sender, EventArgs e)
//{	// this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
//}

//private void txtValue_TextChanged(object sender, System.EventArgs e) 
//{	// this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
//}

//private void Mouse_Down(object sender, System.Windows.Forms.MouseEventArgs e)
//{	// this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Mouse_Down);
//    Avoli.Controls.Events.MoveForm(objSmartClient, this, sender, e);
//}

//private void pnlHeader_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
//{	// this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
//}


//// CONTEXT MENU (Righ Click on DataGridView)
// private ContextMenu contextMenuForGrid = new ContextMenu();
//
//// Advanced Custom Event Handlers - Context Menu (Put in Load Event)
//contextMenuForGrid.MenuItems.Add("Move to Archive", new EventHandler(event_MoveToArchive));
//contextMenuForGrid.MenuItems.Add("Delete To RecycleBin", new EventHandler(event_Delete));
//contextMenuForGrid.MenuItems.Add("Register", new EventHandler(event_Process));

//private void dataGrid_MouseUp(object sender, MouseEventArgs e)
//{   // Load context menu on right mouse click
//    DataGridView.HitTestInfo hitTestInfo;
//    if (e.Button == MouseButtons.Right)
//    {
//        hitTestInfo = dataGrid.HitTest(e.X, e.Y);                                                   // Establish click coordinates
//        dataGrid.CurrentCell = dataGrid[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];             // Change current row by the cell clicked (Cool!)
//        if (hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex == 0)       // Clicked on 1st column
//            contextMenuForGrid.Show(dataGrid, new Point(e.X, e.Y));
//        if (hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex == 1)       // Clicked on 2nd column
//            contextMenuForGrid.Show(dataGrid, new Point(e.X, e.Y));
//    }
//}

//// Advanced Custom Event Handlers
//private void event_MoveToArchive(object sender, EventArgs e)
//{
//    string strFile = dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["Name"].Value.ToString();
//    string strDir = dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["Directory"].Value.ToString();
//    string strSourcePath = strDir + @"\" + strFile;
//    string strDestPath = strDir + @"\archive\" + strFile;
//    if (Avoli.Confirmation.Show(objSmartClient, "Are you sure you wish to move " + strSourcePath + " to " + strDestPath + "?", "", 2))
//    {
//        Avoli.IO.Files.Move(strSourcePath, strDestPath, strFile, -1, false, false, false);
//        Avoli.MessageBox.Show("Moved " + strSourcePath + " to " + strDestPath);
//        picDropZones_Click(sender, e);  // Refresh display of Grid
//    }
//}

//private void event_Delete(object sender, EventArgs e)
//{
//    string strFile = dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["Name"].Value.ToString();
//    string strDir = dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["Directory"].Value.ToString();
//    string strSourcePath = strDir + @"\" + strFile;
//    string strDestPath = strDir + @"\archive\" + strFile;
//    if (Avoli.Confirmation.Show(objSmartClient, "Are you sure you wish to delete " + strSourcePath + "?", "", 2))
//    {
//        Avoli.IO.RecycleBin.Move(strSourcePath);
//        Avoli.MessageBox.Show("Deleted " + strSourcePath + ".");
//        picDropZones_Click(sender, e);  // Refresh display of Grid
//    }
//}

//private void event_Process(object sender, EventArgs e)
//{
//    string strFile = dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["Name"].Value.ToString();
//    string strDir = dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["Directory"].Value.ToString();
//    string strSourcePath = strDir + @"\" + strFile;
//    string strDestPath = strDir + @"\archive\" + strFile;
//    if (Avoli.Confirmation.Show(objSmartClient, "Are you sure you wish to process " + strSourcePath + "?", "", 2))
//    {
//        Avoli.MessageBox.Show("MOCK PROCESS of " + strSourcePath + ".");
//    }
//}
#endregion

#region Avoli .Net 2.0 specific stuff to UN-comment out at a later date
//                                            |||
//                                           / _ \
//                                          ( o o )
//+--------------------------------------oOOo-(_)-oOOo-------------------------------------+
// http://www.microsoft.com/downloads/details.aspx?familyid=0856eacb-4362-4b0d-8edd-aab15c5e04f5&displaylang=en
// Avoli.Compression.GZipStream (compression)
// Avoli.SystemInfo.Compression
// Avoli.SystemInfo.General.GetLengthCompressedReal
// Avoli.Net.FTP.FetchFiles
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region Avoli Dashboard (future)
//                                            |||
//                                           / _ \
//                                          ( o o )
//+--------------------------------------oOOo-(_)-oOOo-------------------------------------+
// FORM THUMBNAILS
// if (!Avoli.Forms.Export.ToControl(this, picThumbnail, true))
//     Avoli.MessageBox.Show("There was a problem taking a screen snapshot to the control!", "", 0);
//+------------------------------------------------Oooo------------------------------------+
//                                           oooO  (  )
//                                          (  )   ) /
//                                           \ (  (_/
//                                            \_)
#endregion

#region TOPIC: Kicking off / Invoking events
// This is how one would kick off an event to run another method
//private void DisplayText(object sender, EventArgs e)
//{
//    textBox1.AppendText(RxString);
//}

//private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
//{
//    RxString = serialPort1.ReadExisting();
//    this.Invoke(new EventHandler(DisplayText));
//}
#endregion

#region TOPIC: Structures
//                                          \\\|///
//                                        \\  - -  //
//                                         (  @ @  )
//+--------------------------------------oOOo-(_)-oOOo-------------------------------------+
// Foo f = new Foo();           // Calling a structure
// readonly Foo f = new Foo();  // Calling a structure READONLY
//
//public struct Foo
//{
//    // Fields
//    private string fooString;
//    private int fooNumber;

//    // Property
//    public string FooString
//    {
//        get { return fooString; }
//        set { fooString = value; }
//    }    
//    public string FooNumber
//    {
//        get { return fooNumber; }
//        set { fooNumber = value; }
//    }

//    // Method
//    public int GetFooNumber()
//    {
//        return fooNumber;
//    }
//}
//+------------------------------------------------Oooo------------------------------------+
//                                           oooO  (  )
//                                          (  )   ) /
//                                           \ (  (_/
//                                            \_)
#endregion

#region TOPIC: Nullable Types
// SOLVES: 
//		int? x = 123;
//		int? y = null;
//		if (x.HasValue) Console.WriteLine(x.Value); 
//		if (y.HasValue) Console.WriteLine(y.Value);

// A NEW null coalescing operator
//		int? x = GetNullableInt();
//		int? y = GetNullableInt();
//		int? z = x ?? y;
//		int i = z ?? -1;
#endregion

#region TOPIC: TList & Predicate Generic Delegate (Framework 2.0)
// A method of passing business tier result sets to a collectionbase.
// Provides for easy filtered & sorted data through the use of a Predicate
// Provides for easy control population
// (I believe the same results can be gained through generic methods of result set to control population)

// THIS DOES NOT WORK IN 2.0: message.UrlContentBase = body;
#endregion

#region TOPIC: Threading (Sytem.Threading - Synchronization)
// Main goal of creating multithreading application is performance improvement.
// EXE's have their own memory block with the WinMain (winform apps) or Main (console apps) being the primary entry point thread.
// Threading eliminates wait states

// When to use it?
// 1) For better response: let a thread do background job while another one deals with user input
// 2) To use a CPU to it's full capacity: network I/O, disks
// 3) TO benefit from multi-processor machines: Threads from the same process may run in parallel on different processors
// 4) To simplify the development of certain types of applications which naturally decompose into self-contained work items.
// 5) Sharing of resources: Need less inter-process communication as the process address space is shared.
#endregion

#region TOPIC: 3rd Party Tools
//                                            ,,,
//                                           /'^'\
//                                          ( o o )
//+-------------------------------------oooO--(_)--Oooo------------------------------------+
// telerik.com			(Utilizes Ajax for web / Windows controls / Reporting)
// componentart.com     (Web charting, gui)
// devexpress.com       (Web charting, gui, reporting)
//
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region TOPIC: XML Parsing from URL
//string url = "http://msmvps.com/coad/rss.aspx";
//System.Net.WebClient c = new System.Net.WebClient();
//string xml = ASCIIEncoding.Default.GetString(c.DownloadData(url));
//System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
//doc.LoadXml(xml);
//foreach (System.Xml.XmlNode node in doc.SelectNodes("/rss/channel/item/title"))
//   Trace.WriteLine(node.InnerText);
#endregion

#region TOPIC: N-Tiers (Splitting creates boundaries & allows encapsulation of specific functionality)
// LAYERS
//		Presentation
//		Business Logic
//		Data Access Class/Component (SQL Server, Oracle etc.)
//		Application Server

// Software utilization				(Writes classes, stored procs for you)
//		CodeSmith					(Customizable templates)
//		NetTiers.org				(Open source templates for code smith)
//									(Generates <TLIST> collections with use in PREDICATES)
//		SQL Server Export			(A Avoli.tool to export schema with data which - written in C#.Net)
//		Database C# Class Generator	(A Avoli.tool to create)
//									(Creates Insert/Update/Delete/Select table specific classes)
#endregion

#region TOPIC: Garbage Collection
//                                           _\\|//_
//                                          (` o-o ')
//+----------------------------------------ooO-(_)-Ooo-------------------------------------+
// The motivation for garbage-collected environments is to simplify memory management for the developer.
// Garbage collection at the object level is done for you and improves performance in multi-threaded apps.
// You can use Windows 2000 System monitor to track garbage collection performance.
//		1) The newer an object is, the shorter its lifetime will be.
//		2) The older an object is, the longer its lifetime will be.
//		3) Newer objects tend to have strong relationships to each other and are frequently accessed around the same time.
//		4) Compacting a portion of the heap is faster than compacting the whole heap.
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region TOPIC: Serialization
//                                          \\\|///
//                                        \\  - -  //
//                                         (  @ @  )
//+--------------------------------------oOOo-(_)-oOOo-------------------------------------+
// Serialization is the process of converting an object into a stream of bytes. De-serialization is the opposite process of creating an object from a stream of bytes. 
// Serialization is handled primarily by classes and interfaces in the System.Runtime.Serialization namespace.
// To serialize an object, you need to create two things:
//		1) A stream to contain the serialized objects 
//		2) A formatter to serialize the objects into the stream 

// The code required to perform serialization in .NET is very simple.
//		public static void WriteToFile(BaseballPlayer bp, String filename)
//		{
//			Stream str = File.OpenWrite(filename);
//			BinaryFormatter formatter = new BinaryFormatter();
//			formatter.Serialize(str, bp);
//			str.Close();
//		}

// The simplest way to make your classes eligible for serialization is to use the Serializable attribute.
// By decorating your class with this attribute as shown below, your classes are immediately made serializable:
//		[Serializable]
//		public class BaseballPlayer
//		{
//			[...]
//		}
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region TOPIC: Regular Expressions (Regex)
//                                          _\\|//_
//                                          ///|\\\
//+--------------------------------------oOO--(_)--OOo------------------------------------+
// ^\d{4}$                      4 digit number
// ^(?!0{5})(\d{5})(?!-?0{4})(-?\d{4})?$    Zip code
// \w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*  Email
// \belvis\b					Find elvis as a whole word
// \belvis\b.*\balive\b			Find text with "elvis" followed by "alive"
// \b\w{5,6}\b					Find all five and six letter words
// \b\d{5}-\d{4}\b|\b\d{5}\b	Five and nine digit Zip Codes
// \b(\w+)\b\s*\1\b				Find repeated words
// \b\w+(?=ing\b)				The beginning of words ending with "ing"
// (?<=\bre)\w+\b				The end of words starting with "re"
// \b\w*q[^u]\w*\b				Words with "q" followed by NOT "u"
// \b\w*q(?!u)\w*\b				Search for words with "q" not followed by "u"
// (?<=<(\w+)>).*(?=<\/\1>)		Text between HTML tags
//
//Character Description 
//  \  Marks the next character as either a special character or escapes a literal. For example, "n" matches the character "n". "\n" matches a newline character. The sequence "\\" matches "\" and "\(" matches "(".
//        Note: double quotes may be escaped by doubling them: "<a href=""...>" 
//  ^  Depending on whether the MultiLine option is set, matches the position before the first character in a line, or the first character in the string. 
//  $  Depending on whether the MultiLine option is set, matches the position after the last character in a line, or the last character in the string. 
//  *  Matches the preceding character zero or more times. For example, "zo*" matches either "z" or "zoo". 
//  +  Matches the preceding character one or more times. For example, "zo+" matches "zoo" but not "z". 
//  ?  Matches the preceding character zero or one time. For example, "a?ve?" matches the "ve" in "never".  
//  . Matches any single character except a newline character.  
//  (pattern)  Matches pattern and remembers the match. The matched substring can be retrieved from the resulting Matches collection, using Item [0]...[n]. To match parentheses characters ( ), use "\(" or "\)". 
//  (?<name>pattern)  Matches pattern and gives the match a name.  
//  (?:pattern)  A non-capturing group  
//  (?=...)  A positive lookahead  
//  (?!...)  A negative lookahead  
//  (?<=...)  A positive lookbehind .  
//  (?<!...)  A negative lookbehind .  
//  x|y Matches either x or y. For example, "z|wood" matches "z" or "wood". "(z|w)oo" matches "zoo" or "wood".  
//  {n} n is a non-negative integer. Matches exactly n times. For example, "o{2}" does not match the "o" in "Bob," but matches the first two o's in "foooood". 
//  {n,}  n is a non-negative integer. Matches at least n times. For example, "o{2,}" does not match the "o" in "Bob" and matches all the o's in "foooood." "o{1,}" is equivalent to "o+". "o{0,}" is equivalent to "o*". 
//  {n,m}  m and n are non-negative integers. Matches at least n and at most m times. For example, "o{1,3}" matches the first three o's in "fooooood." "o{0,1}" is equivalent to "o?". 
//  [xyz]  A character set. Matches any one of the enclosed characters. For example, "[abc]" matches the "a" in "plain".  
//  [^xyz]  A negative character set. Matches any character not enclosed. For example, "[^abc]" matches the "p" in "plain".  
//  [a-z]  A range of characters. Matches any character in the specified range. For example, "[a-z]" matches any lowercase alphabetic character in the range "a" through "z".  
//  [^m-z]  A negative range characters. Matches any character not in the specified range. For example, "[m-z]" matches any character not in the range "m" through "z".  
//  \b  Matches a word boundary, that is, the position between a word and a space. For example, "er\b" matches the "er" in "never" but not the "er" in "verb".  
//  \B  Matches a non-word boundary. "ea*r\B" matches the "ear" in "never early".  
//  \d  Matches a digit character. Equivalent to [0-9].  
//  \D  Matches a non-digit character. Equivalent to [^0-9].  
//  \f  Matches a form-feed character.  
//  \n  Matches a newline character.  
//  \r  Matches a carriage return character.  
//  \s  Matches any white space including space, tab, form-feed, etc. Equivalent to "[ \f\n\r\t\v]". 
//  \S  Matches any nonwhite space character. Equivalent to "[^ \f\n\r\t\v]".  
//  \t  Matches a tab character.  
//  \v  Matches a vertical tab character.  
//  \w  Matches any word character including underscore. Equivalent to "[A-Za-z0-9_]".  
//  \W  Matches any non-word character. Equivalent to "[^A-Za-z0-9_]".  
//  \num  Matches num, where num is a positive integer. A reference back to remembered matches. For example, "(.)\1" matches two consecutive identical characters.  
//  \n Matches n, where n is an octal escape value. Octal escape values must be 1, 2, or 3 digits long. For example, "\11" and "\011" both match a tab character. "\0011" is the equivalent of "\001" & "1". Octal escape values must not exceed 256. If they do, only the first two digits comprise the expression. Allows ASCII codes to be used in regular expressions. 
//  \xn Matches n, where n is a hexadecimal escape value. Hexadecimal escape values must be exactly two digits long. For example, "\x41" matches "A". "\x041" is equivalent to "\x04" & "1". Allows ASCII codes to be used in regular expressions. 
//  \un Matches a Unicode character expressed in hexadecimal notation with exactly four numeric digits. "\u0200" matches a space character.  
//  \A Matches the position before the first character in a string. Not affected by the MultiLine setting  
//  \Z Matches the position after the last character of a string. Not affected by the MultiLine setting.  
//  \G Specifies that the matches must be consecutive, without any intervening non-matching characters.  

//Roman Numbers
//string p1 = "^m*(d?c{0,3}|c[dm])"
//  + "(l?x{0,3}|x[lc])(v?i{0,3}|i[vx])$";
//string t1 = "vii";
//Match m1 = Regex.Match(t1, p1);

//Swapping First Two Words
//string t2 = "the quick brown fox";
//string p2 = @"(\S+)(\s+)(\S+)";
//Regex x2 = new Regex(p2);
//string r2 = x2.Replace(t2, "$3$2$1", 1);

//Keyword = Value
//string t3 = "myval = 3";
//string p3 = @"(\w+)\s*=\s*(.*)\s*$";
//Match m3 = Regex.Match(t3, p3);

//Line of at Least 80 Characters
//string t4 = "********************"
//  + "******************************"
//  + "******************************";
//string p4 = ".{80,}";
//Match m4 = Regex.Match(t4, p4);

//MM/DD/YY HH:MM:SS
//string t5 = "01/01/01 16:10:01";
//string p5 =
//  @"(\d+)/(\d+)/(\d+) (\d+):(\d+):(\d+)";
//Match m5 = Regex.Match(t5, p5);

//Changing Directories (for Windows)
//string t6 =
//  @"C:\Documents and Settings\user1\Desktop\";
//string r6 = Regex.Replace(t6,
//  @"\\user1\\",
//  @"\\user2\\");

//Expanding (%nn) Hex Escapes
//string t7 = "%41"; // capital A
//string p7 = "%([0-9A-Fa-f][0-9A-Fa-f])";
//// uses a MatchEvaluator delegate
//string r7 = Regex.Replace(t7, p7,
//  HexConvert);

//Deleting C Comments (Imperfectly)
//string t8 = @"
///*
// * this is an old cstyle comment block
// */
//";
//string p8 = @"
//  /\*  # match the opening delimiter
//  .*?	 # match a minimal numer of chracters
//  \*/	 # match the closing delimiter
//";
//string r8 = Regex.Replace(t8, p8, "", "xs");

//Removing Leading and Trailing Whitespace
//string t9a = "   leading";
//string p9a = @"^\s+";
//string r9a = Regex.Replace(t9a, p9a, "");

//string t9b = "trailing  ";
//string p9b = @"\s+$";
//string r9b = Regex.Replace(t9b, p9b, "");

//Turning '\' Followed by 'n' Into a Real Newline
//string t10 = @"\ntest\n";
//string r10 = Regex.Replace(t10, @"\\n", "\n");

//IP Address
//string t11 = "55.54.53.52";
//string p11 = "^" +
//  @"([01]?\d\d|2[0-4]\d|25[0-5])\." +
//  @"([01]?\d\d|2[0-4]\d|25[0-5])\." +
//  @"([01]?\d\d|2[0-4]\d|25[0-5])\." +
//  @"([01]?\d\d|2[0-4]\d|25[0-5])" +
//  "$";
//Match m11 = Regex.Match(t11, p11);

//Removing Leading Path from Filename
//string t12 = @"c:\file.txt";
//string p12 = @"^.*\\";
//string r12 = Regex.Replace(t12, p12, "");

//Joining Lines in Multiline Strings
//string t13 = @"this is 
//a split line";
//string p13 = @"\s*\r?\n\s*";
//string r13 = Regex.Replace(t13, p13, " ");

//Extracting All Numbers from a String
//string t14 = @"
//test 1
//test 2.3
//test 47
//";
//string p14 = @"(\d+\.?\d*|\.\d+)";
//MatchCollection mc14 = Regex.Matches(t14, p14);

//Finding All Caps Words
//string t15 = "This IS a Test OF ALL Caps";
//string p15 = @"(\b[^\Wa-z0-9_]+\b)";
//MatchCollection mc15 = Regex.Matches(t15, p15);

//Finding All Lowercase Words
//string t16 = "This is A Test of lowercase";
//string p16 = @"(\b[^\WA-Z0-9_]+\b)";
//MatchCollection mc16 = Regex.Matches(t16, p16);

//Finding All Initial Caps
//string t17 = "This is A Test of Initial Caps";
//string p17 = @"(\b[^\Wa-z0-9_][^\WA-Z0-9_]*\b)";
//MatchCollection mc17 = Regex.Matches(t17, p17);

//Finding Links in Simple HTML
//string t18 = @"
//<html>
//<a href=""first.htm"">first tag text</a>
//<a href=""next.htm"">next tag text</a>
//</html>
//";
//string p18 = @"<A[^>]*?HREF\s*=\s*[""']?"
//  + @"([^'"" >]+?)[ '""]?>";
//MatchCollection mc18 = Regex.Matches(t18, p18, "si");

//Finding Middle Initial
//string t19 = "Hanley A. Strappman";
//string p19 = @"^\S+\s+(\S)\S*\s+\S";
//Match m19 = Regex.Match(t19, p19);

//Changing Inch Marks to Quotes
//string t20 = @"2' 2"" ";
//string p20 = "\"([^\"]*)";
//string r20 = Regex.Replace(t20, p20, "``$1''");
//
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region TOPIC: Inheritance
//                                          \\\'///
//                                         / _   _ \
//                                       (| (.) (.) |)   
//+--------------------------------------oOOo-(_)-oOOo-------------------------------------+
// SINGLETON:   Is a class that only ever has one instance and provides a global way of accessing it.
// REFLECTION:  System.Reflection to return Runtime assembly information (Ex: Assembly Name, Directory, Info)
// INHERITANCE: Inheritance is a relationship between classes where one class is the parent class of another.
//              Used to extend functionality to an existing class.
//              Below the ExtendedProfile is a subclass of the Profile base class.
//
//		        public class ExtendedProfile: Profile 
//		        {
//		        }
//                                        .oooO                                  
//+---------------------------------------(   )--Oooo.-----------------------------------+
//                                         \ (   (   )
//                                          \_)   ) /
//                                                (_/
#endregion

#region TOPIC: Generic List (Type Safe)
//System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
//list.Add(2);
//list.Add(3);
//list.Add(7);
////
//foreach (int prime in list)           // Loop through List with foreach
//{
//    Console.WriteLine(prime);
//}
//
//for (int i = 0; i < list.Count; i++)  // Loop through List with for
//{
//    Console.WriteLine(list[i]);
//}
//
//list.Clear();


//List<int> primes = new List<int>(new int[] { 2, 3, 5 });
//foreach (int number in primes)
//{
//    if (number == 3) // Will match once
//    {
//      Console.WriteLine("Contains 3");
//    }
//}
#endregion

#region DEVELOPER: Example .Net C# Code
//                                            \\|//
//                                           ~~O-O~~
//+--------------------------------------oOOo--(_)--oOOo-----------------------------------+
// EXAMPLE C# CODE BASICS
//
//  DETECTING APP NAME: System.Windows.Forms.Application.ProductName.ToString());
//
//  App.Path equiv -> System.Windows.Forms.Application.StartupPath.ToString() + "\test.doc";
//  
//	Example: One Trip data fetching
//		string[] saProcNames = {"sp_select_general_companies " + intModuleNumber, "sp_select_general_users " + intModuleNumber};
//		string[] saProcValues = {"Active Companies", "Active Users"};
//		form.cboLoginIDs.Text = System.Environment.GetEnvironmentVariable("USERNAME");
//		System.Data.DataSet ds = Avoli.DataStorage.Database.General.GetDataSet(strConnection, saProcNames, saProcValues);
//		Avoli.DataStorage.Database.DataSetObject.ToControl(form.cboCompanies, "", false, ds, 0);
//		Avoli.DataStorage.Database.DataSetObject.ToControl(form.cboLoginIDs, "", false, ds, 1);
//		if (!blnXMLAuthenticateDatabase) form.cboLoginIDs.Enabled = false;
//		ds.Dispose();
//
//	Example: datagrid relations
//		string strOrders = "select * from orders";
//		string strCustomers = "select * from customers";
//		System.Data.SqlConnection cnn = new System.Data.SqlConnection(strDataSource);
//		System.Data.SqlDataAdapter da = new System.Data.SqlDataAdapter(strOrders, cnn);
//		System.Data.DataSet ds = new System.Data.DataSet();
//		da.Fill(ds, "Orders");
//		da = new SqlDataAdapter(strCustomers, cnn);
//      da.CommandTimeout = Convert.ToInt16(objSmartClient.DBTimeout); 
//		da.Fill(ds, "Customers");
//		ds.Relations.Add("CustomerOrders", ds.Tables["Customers"].Columns["CustomerID")], ds.Tables["Orders"].Columns["CustomerID"]);
//		dg.SetDataBindings(ds,"Customers");
//
//  Example: 
//
//	Example: altering a dataset result set with filtering
//		System.Data.DataViewManager dvm = new System.Data.DataViewerManager(ds);
//		dvm.DataViewSettings["Customers"].RowFilter = "Country='UK'";
//		dg.SetDataBinding(dvm, "Customers");
//
//	Example: binding datatable columns to controls
//		textBox1.DataBindings.Add("Text", ds, "Products.ProductName");
//
//	Example: array dimensioning
//		int[,] numbers = new int[3, 2] { {1, 2}, {3, 4}, {5, 6} };
//		string[,] names = new string[2, 2] { {"Rosy","Amy"}, {"Peter","Albert"} };
//
//	Example: array sorting 
//		Array.Sort(names);
//		foreach (string str in names)
//			Avoli.MessageBox.Show(str, "");
//
//	Example: array class 
//		int bucketrows = 2;
//		int bucketcount = 2;
//		Array names = Array.CreateInstance( typeof(String), bucketcount, bucketrows );
//		names.SetValue( "A1", 0, 0 );
//		names.SetValue( "Field1", 0, 1 );
//		names.SetValue( "B2", 1, 0 );
//		names.SetValue( "Field2", 1, 1 );
//
//		int items1 = names.GetLength(0); 
//		int items2 = names.GetLength(1);
//		for ( int i =0; i < items1; i++ )
//		for ( int j = 0; j < items2; j++ )
//		Avoli.MessageBox.Show (i.ToString() +	","+ j.ToString() +": " +names.GetValue( i, j ) , "");
//
//	Example: array (multi-dimensional)
//		int maxrows = 2;
//		int maxcols = 3;
//		string[,] names = new string[maxrows, maxcols];
//	
//		names[0, 0] = "FromField";
//		names[0, 1] = "FromValue";
//		names[0, 2] = "ToField";
//
//		names[1, 0] = "FromField";
//		names[1, 1] = "FromValue";
//		names[1, 2] = "ToField";
//
//		for ( int i =0; i < maxrows; i++ )
//		for ( int j = 0; j < maxcols; j++ )
//		Avoli.MessageBox.Show (i.ToString() +	","+ j.ToString() +": " +names.GetValue( i, j ) , "");
//
//	Example: hashtable (NOTE: Values can come back reversed by population order)
//		// Hashtable Example (Key & Value data elements)
//		// NOTE: Use the Add() method to add items and the Remove() method to remove items
//		System.Collections.Hashtable ht = new System.Collections.Hashtable();
//		ht.Add("1", "NC");
//		ht.Add("2", "SC");
//		foreach (System.Collections.DictionaryEntry d in ht)
//			Avoli.MessageBox.Show ( d.Key + " of " + d.Value , "Total Values Count: " + ht.Count, 0);
//		ht.Clear(); 
//		
//	Example: sortedlist
//		System.Collections.SortedList sl = new System.Collections.SortedList();
//		SortedList sortedList = new System.Collections.SortedList();
//		sortedList.Add(1, "Manashi");
//		sortedList.Add(3, "Joydip");
//		sortedList.Add(2, "Jini");
//		sortedList.Add(4, "Piku");
//		foreach (string str in sortedList.Values)
//			Console.WriteLine(str);
//
//	Example: stack (LIFO - Last in First out)
//		System.Collections.Stack stackObject = new System.Collections.Stack();
//		stackObject.Push("Joydip");
//		stackObject.Push("Steve");
//		stackObject.Push("Jini");
//		while (stackObject.Count > 0)
//			Console.WriteLine(stackObject.Pop());
//		Console.ReadLine();
//
//	Example: queue (FIFO - First in First Out)
//		// NOTE: The Enqueue() method adds items while the Dequeue() method removes items
//		System.Collections.Queue queueObject = new System.Collections.Queue();
//		queueObject.Enqueue("Joydip");
//		queueObject.Enqueue("Steve");
//		queueObject.Enqueue("Jini");
//		while (queueObject.Count > 0)
//		    Console.WriteLine(queueObject.Dequeue());
//		Console.ReadLine();
//


// CONTROL - STATUSBAR
// statusBar1.Panels[0].Text = "My Computer";
//
// BIND MANUALLY - DataSet
// COOL - Force an enumeration of values to a dropdown
// cboParity.Items.AddRange(Enum.GetNames(typeof(System.IO.Ports.Parity)));
//
// protected DataSet dsProducts = new DataSet();
// protected const string SQL_CONNECTION_STRING = "Server=localhost;" + "DataBase=Northwind;" + "Integrated Security=SSPI" ;
// protected const string MSDE_CONNECTION_STRING = @"Server=(local)\NetSDK;" + "DataBase=Northwind;" + "Integrated Security=SSPI" ;
// SqlConnection northwindConnection = new SqlConnection(strConnection);
// SqlDataAdapter ProductAdapter = new SqlDataAdapter("SELECT * " + "FROM products", northwindConnection);
// ProductAdapter.Fill(dsProducts, PRODUCT_TABLE_NAME);
// ComboBox1.DataSource=dsProducts.Tables["PRODUCTS"];
// ComboBox1.DisplayMember="ProductName";
// ComboBox1.ValueMember="ProductID";
// ComboBox1.SelectedIndex=0;
// lblAssociated.Text=ComboBox1.SelectedValue.ToString();
//
//
// BIND MANUALLY - DataView
// protected DataView dvProducts ;
// protected const string SQL_CONNECTION_STRING = "Server=localhost;" + "DataBase=Northwind;" + "Integrated Security=SSPI" ;
// protected const string MSDE_CONNECTION_STRING = @"Server=(local)\NetSDK;" + "DataBase=Northwind;" + "Integrated Security=SSPI" ;
// SqlConnection northwindConnection = new SqlConnection(strConnection);
// SqlDataAdapter ProductAdapter = new SqlDataAdapter("SELECT * " + "FROM products", northwindConnection);
// dvProducts = new DataView(dsProducts.Tables["products"],"",DEFAULT_SORT,DataViewRowState.OriginalRows);
// ComboBox1.DataSource=dvProducts;
// ComboBox1.DisplayMember="ProductName";
// ComboBox1.ValueMember="ProductID";
// ComboBox1.SelectedIndex=0;
// lblAssociated.Text=ComboBox1.SelectedValue.ToString();
//
//
// DATA TO CONTROL - With Value & ID
// strSQL = "SELECT CategoryID, CategoryName FROM Categories";
// cnSQL = new SqlConnection(Connectionstring);
// cnSQL.Open();
// cmSQL = new SqlCommand(strSQL, cnSQL);
// cmSQL.CommandTimeout = 40;
// drSQL = cmSQL.ExecuteReader();
// cbCategories.Items.Clear();
// while (drSQL.Read())
// {
//		objListItem = new ListItem(drSQL["CategoryName"].ToString(), Convert.ToInt32(drSQL["CategoryID"]));
//		cbCategories.Items.Add(objListItem);
// }
//
//
// DATETIMEPICKER usage
// MessageBox.Show("The selected value is  : " + dateTimePicker1.Text);
// MessageBox.Show("The day of the week is : " + dateTimePicker1.Value.DayOfWeek.ToString());
// MessageBox.Show("Millisecond is : " + dateTimePicker.Value.Millisecond.ToString());
//
//
// CONTROL SNAPSHOTS BY PARENT
// Control [] c = new Control[limit];
// tabControl.Controls.CopyTo(c, 0);
//
//
// CONTROL - TIMER INSTANTIATION
//		public void OnTick(object o, System.Timers.ElapsedEventArgs e)
//		{  Refresh(); }
//		private void frmClockAnalog_Load(object sender, System.EventArgs e)
//		{	// this.Load += new System.EventHandler(this.frmClockAnalog_Load);
//			Avoli.Forms.Apply.PaintKit(objSmartClient, this, new string[] {});
//			System.Timers.Timer t = new System.Timers.Timer();
//			t.Interval = 1000;
//			t.Elapsed += new System.Timers.ElapsedEventHandler(OnTick);
//			t.AutoReset = true;
//			t.Enabled = true;
//			SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
//			SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
//			SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
//		}
//
//
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region Avoli Reference Download URL's
// using Microsoft.DirectX.AudioVideoPlayback;	// Download the DirectX SDK Here... http://www.microsoft.com/downloads/details.aspx?FamilyID=7AB978B5-5F1A-4F6A-88EB-FC646138BECA&displaylang=en
#endregion

#region Avoli Speed Enhancements Techniques
// STRINGBUILDER
//		System.Text.StringBuilder sb = new System.Text.StringBuilder(200);
//		sb.Capacity = 200;
//		sb.EnsureCapacity(200);
//		sb.Length = 200;
#endregion

#region Avoli String Param Replacement
// string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", strText, strLanguagePair);
#endregion

#region Avoli AUDIT (Rules to live by)
// SECURITY, PROTECT, INSPECT
// --------------------------
// All data is fradulent.
// All communications are attempted hacks.
// All clients are thieves.
// Technology is only my first line of defense.
// Develop a crack-resistent environment (as much as possible)
// Make sure successful cracks cause little harm (if possible)

// YOUR USERS
// ----------
// Make sure your users understand good passwords v.s. bad passwords
// Make sure file permissions are set correctly

// CONTINUING ED
// -------------
// Make sure to keep abreast of security announcements
// Put yourself on CERT or CIAC mailing lists

// VULNERABILITY TESTS
// -------------------
// Attempt to perform an ETHICAL HACK yourself regularly
// Make regular backups
// Create and check your log files regularly

// AREAS TO PROTECT
// ----------------
// Web Servers are one of the most dangerous services you offer
//		(CGI Scripts must be made as safe as possible)
//		(Inevitable damages from cracked CGI scripts must be contained)
//		(Review your scripts)
//		(NEVER!!! pass unchecked remote user input to a shell command)
//		(Ex: In C/Unix/Linux a popen(), open() and system() should be stopped)
// Stop Snoopers
//		(Do not generate dynamically produced index pages)
//		(Do not be so specific on your errors which help hackers)
//		(Generate an error ID which is displayed and then go lookup the ID to see the actual error)
// Writable Directories
//		(Never store writable directories in your bin tree)
//		(Never grant write permissions to any file unless you are editing that file)
//		(Backup regularly - Ex: tar cvfp name.tar rootdirectoryname)
// User Input
//		(Treat all input as an attempted hack)
//		(Verify valid datatypes)
//		(Strip out characters which could be used in a SQL Injection attach)
//		(Disable SSI which is even more dangerous than CGI especially togeather)
#endregion

#region Avoli Populate Datagrid methods
//	objSmartClient.ConnectionString = "data source=localhost;persist security info=false;initial catalog=Northwind;user iD=admin; password=password";
//	objSmartClient.cnn = Avoli.DataStorage.Database.General.Connect(objSmartClient.ConnectionString);
//
//	// Method # 1: From a multiple table dataset
//	string[] saProcNames = {"select * from customers"};
//	string[] saProcValues = {"Customers"};
//	objSmartClient.DataSet = Avoli.DataStorage.Database.General.GetDataSet(objSmartClient, saProcNames, saProcValues);
//	Avoli.DataStorage.Database.DataSetObject.ToControl(objSmartClient, objSmartClient.DataSet, 0, dataGrid1, "Search Results", false);
//
//	// Method # 2: From a single table dataset
//	objSmartClient.SQLorStoredProc = "select * from customers";
//	Avoli.DataStorage.Database.General.ToControlBound(objSmartClient, dataGrid2, "Search Results", false, false, false, true);
#endregion

#region Avoli Returning multiple items from a method (OUT)
// BYREF passes
//		static void Method(out int i, out int j)
//		{
//			i = 44;
//			j = 55;
//		}
//		static void Main()
//		{
//			int value1, value2;
//			Method(out value1, value2);
//		}
#endregion

#region Avoli Dealing with Files (Text & Binary)
//BUFFEREDSTREAM Class (BINARY)
//The BufferedStream class is used to wrap a Stream object and supports buffering to improve performance. 
//It has a buffer size of 4,096 bytes.
//      OpenFileDialog openFileDialog = new OpenFileDialog();
//      if (openFileDialog.ShowDialog() == DialogResult.OK) 
//      {
//          string filepath = openFileDialog.FileName;
//          BufferedStream bufferedStream = new BufferedStream(openFileDialog.OpenFile());
//          Byte[] bytes = new Byte[128];   // Read a specified # of bytes from the file
//          bufferedStream.Read(bytes, 0, 128);
//      }

//STREAMREADER Class (TEXT, NOT BINARY)
//You can use the ReadLine method to read a line of text - default encoding (System.Text.UTF8Encoding) 
//The stream's current encoding can be retrieved from the read-only CurrentEncoding property. 
//NOTE: The ReadLine method returns a null reference if it encounters the end of the stream: 
//      System.IO.StreamReader sr = new System.IO.StreamReader("C:/CompanySecret.txt");
//      string line = sr.ReadLine();
//      while (line != null)
//      {
//          System.Console.WriteLine(line);
//          line = sr.ReadLine();
//      }
//      sr.Close();

//STREAMWRITER Class (TEXT, NOT BINARY)
//Write enables you to write a character, an array of characters, or a string. 
//The WriteLine method accepts a String and AUTOMATICALLY adds an end-of-line character to the end of the string. 
//      System.IO.StreamWriter sw = new System.IO.StreamWriter("C:/NewCompanySecret.txt");
//      string line = "Don't tell anyone!!!"; 
//      sw.WriteLine(line);   // Write string to the file
//      sw.Close();

//BINARYREADER Class (BINARY)
//For example, if you store a byte and then a string to a binary file, you must first read a byte and then read a String. 
//The BinaryReader class can read a character, an array of bytes, or an array of characters. 
//      System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.OpenRead("C:/MyBinary.bin"));
//      Byte b2 = br.ReadByte();      // Read a byte
//      string s2 = br.ReadString();  // Read a string
//      br.Close();

//BINARYWRITER Class (BINARY)
//You can’t pass a file path to create a BinaryWriter object. 
//The BinaryWriter class’ most important method is Write. 
//      System.IO.BinaryWriter bw = new System.IO.BinaryWriter(System.IO.File.Create("C:/MyBinary.bin"));
//      Byte b = 17;
//      string s = "August";
//      bw.Write(b);  // Write a byte
//      bw.Write(s);  // Write a string
//      bw.Close();
#endregion

#region Avoli Data Collection Normal Method
//string strConnection = "Data source=bp-us-hipt-sqld; persist security info=False; initial catalog=Avoli; user id=Avoli; password=ou812; Connection Timeout=8";
//try
//{
//    System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(strConnection);
//    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("SELECT * FROM [General History]", cn);
//    System.Data.DataTable dt = new System.Data.DataTable();
//    da.Fill(dt);
//    foreach (System.Data.DataRow row in dt.Rows)
//    {
//        foreach (System.Data.DataColumn col in dt.Columns)
//            System.Windows.Forms.MessageBox.Show(row[col].ToString());
//    }
//}
//catch (Exception ex)
//{
//    System.Windows.Forms.MessageBox.Show("Error: " + ex);
//}
#endregion

#region Avoli Data Collection Alternative Methods (Arrays, Collections, Dictionaries)
//  System.Collections.SortedList - A SortedList element can be accessed by its key, like an element in any IDictionary implementation, or by its index, like an element in any IList implementation.
//System.Collections.SortedList mySL = new System.Collections.SortedList();
//mySL.Add("First", "Hello");
//mySL.Add("Second", "World");
//mySL.Add("Third", "!");

//Console.WriteLine( "mySL" );
//Console.WriteLine( "  Count:    {0}", mySL.Count );
//Console.WriteLine( "  Capacity: {0}", mySL.Capacity );
//Console.WriteLine( "  Keys and Values:" );
//PrintKeysAndValues( mySL );

//public static void PrintKeysAndValues( SortedListSystem.Collections. myList )  {
//   Console.WriteLine( "\t-KEY-\t-VALUE-" );
//   for ( int i = 0; i < myList.Count; i++ )  {
//      Console.WriteLine( "\t{0}:\t{1}", myList.GetKey(i), myList.GetByIndex(i) );
//   }
//   Console.WriteLine();
//}

// CONTROL POPULATION - SOURCE: ENUM POPULATION & ITERATION
//  lstLangPairs.Items.AddRange(Enum.GetNames(typeof(Avoli.Globalization.Google.LangPair))); // Enum Type values to control

// STRING ARRAY - SOURCE: ENUM POPULATION & ITERATION
//  string[] colorNames = Enum.GetNames(typeof(System.Drawing.KnownColor));
//	foreach(string str in names)
//		Response.Write(str);

// STRING ARRAY - SOURCE: MANUAL POPULATION & ITERATION
//	String names[] = new String[2] {"Joydip","Jini"};
//	foreach(string str in names)
//		Response.Write(str);

// ARRAYLIST
//	public class CustomerArrayList : System.Collections.ArrayList
//	{
//		public new Customer this[int index]
//		{
//			get { return ((Customer)base[index]); }
//		}
//		public int Add(Customer customer)
//		{
//			return base.Add(customer);
//		}
//		public void Insert(int index, Customer customer)
//		{
//			base.Insert(index, customer);
//		}
//		public void Remove(Customer customer)
//		{
//			base.Remove(customer);
//		}
//		public bool Contains(Customer customer)
//		{
//			return base.Contains(customer);
//		}
//	}

// COLLECTION
//	public class Product: System.Collections.CollectionBase
//	{
//		public Product this[int index]
//		{
//			get
//			{ return ((Product)(List[index])); }
//			set
//			{ List[index] = value; }
//		}
//		public bool Contains(Product product)
//		{
//			return List.Contains(product);
//		}
//		public int Add(Product Product)
//		{
//			return List.Add(Product);
//		}
//		public void Insert(int index, Product product)
//		{
//			List.Insert(index, product);
//		}
//		public void Remove(Product product)
//		{
//			List.Remove(Product);
//		}
//	}

// DICTIONARY
//	public class Product: System.Collections.DictionaryBase
//	{
//		public Product this[int index]
//		{
//			get
//			{ return ((Product)(Dictionary[index])); }
//			set
//			{ Dictionary[index] = value; }
//		}
//		public bool Contains(Product product)
//		{
//			return Dictionary.Contains(product);
//		}
//		public int Add(Product Product)
//		{
//			return Dictionary.Add(Product);
//		}
//		public void Insert(int index, Product product)
//		{
//			Dictionary.Insert(index, product);
//		}
//		public void Remove(Product product)
//		{
//			Dictionary.Remove(Product);
//		}
//	}
#endregion

#region Avoli Database Result Set to Collection
//public class UserCollection : List<User>
//{
//    public static UserCollection GetUsers()
//    {
//        UserCollection l = new UserCollection();
//        Database d = DatabaseFactory.CreateDatabase();
//        DbCommand cmd = d.GetSqlStringCommand("SELECT * FROM Users");
//        using (IDataReader rdr = d.ExecuteReader(cmd))
//        {
//            while (rdr.Read())
//            {
//                l.Add(new User(rdr));
//            }
//        }
//        return l;
//    }
//}

//public class FingerPrintCollection : List<FingerPrint>
//{
//    public static FingerPrintCollection GetUsersPrints(int userID)
//    {
//        FingerPrintCollection fpc = new FingerPrintCollection();
//        Database d = DatabaseFactory.CreateDatabase();
//        DbCommand cmd = d.GetSqlStringCommand("SELECT * FROM FingerPrints WHERE userID = @userID");
//        d.AddInParameter(cmd, "@userID", DbType.Int32, userID);
//        using (IDataReader rdr = d.ExecuteReader(cmd))
//        {
//            while (rdr.Read())
//            {
//                fpc.Add(new FingerPrint(rdr));
//            }
//        }
//        return fpc;
//    }
//}
#endregion

#region Avoli Report Options (REPORTS)
//                                           \\|//       
//                                          ~~O-O~~
//                                         ////^\\\\
//+-------------------------------------oOOo--(_)--oOOo------------------------------------+
// REPORTING OPTIONS
// 1) Reports List - Populate a Treeview with [Designer Reports] and Report Templates (.html files in @\bin\Debug\Report Templates\)
//	Avoli.DataStorage.Database.ToControlUnbound(trvCannedReports, strConnection, "sp_select_designer_reports_tree", "", false, false, "");
//
//	// Populate (append) treeview with all Report Templates found at the folder path (These are .html files with replaceable values)
//	strReportTemplatePath = System.Windows.Forms.Application.StartupPath.Replace(@"\bin\Debug", "") + @"\Report Templates\";
//	if (!Avoli.SystemInfo.Directories.Exists(strReportTemplatePath))
//	    Avoli.SystemInfo.Directories.Create(strReportTemplatePath);
//	ArrayList al = Avoli.IO.Files.GetAllFiles(strReportTemplatePath, "*.htm*", lblMessages);
//	al = Avoli.Arrays.RemoveByValue(al, "export.html", true);
//	string[] sa = Avoli.Arrays.ArrayListToStringArray(al);
//	sa = Avoli.Arrays.StripPath(sa);
//	sa = Avoli.Arrays.StripValue(sa, ".html", true);
//	Avoli.Arrays.ToControlFromStringArray(sa, trvCannedReports, true, "");
//
//
// 2) Report HTML Generation (Put behind the click event of a button)
//  if (cboReportFontNames.Text != "") strFontName = cboReportFontNames.Text;
//  if (cboReportFontSizes.Text != "") strFontSize = cboReportFontSizes.Text;
//  strParentNodeName = trvCannedReports.SelectedNode.Parent.ToString().Replace("TreeNode: ", "");
//  Avoli.SuperString ss = trvCannedReports.SelectedNode.ToString().Replace("TreeNode: ", "");
//  string strChildNodeName = ss.GetProperCase();
// 	if (strParentNodeName == "Templates")
// 	{   //----------------------------------------------------------------------
// 		// HTML Template Report selected
// 		//----------------------------------------------------------------------
// 		objSmartClient.ParamNamesAndValues =  Avoli.Controls.Exporter.ToReport(lblMessages, trvCannedReports, strFontName, strFontSize, cboReportExportTypes.SelectedText.ToString(), strExportPath, strExportDefaultFile, strConnection, chkShowDerivative.Checked, chkShowSQL.Checked, chkShowDecimals.Checked, chkShowGridLines.Checked, chkShowAverages.Checked, chkShowTotalCount.Checked, saParamNamesAndValues, Convert.ToBoolean(chkIgnoreURLs.Checked), Convert.ToBoolean(chkIncludeReportIDs.Checked), Convert.ToBoolean(chkAutoPrint.Checked), Convert.ToBoolean(chkPrintPreview.Checked),  Convert.ToBoolean(chkPortrait.Checked), strDatabaseEnvironment);
// 		int intParams = objSmartClient.ParamNamesAndValues.GetLength(0) - 1;
// 		if (intParams == -1) intParams = 0;
// 		lblParamCnt.Text = "Params: " + intParams.ToString();
// 	}
// 	else
// 	{   //----------------------------------------------------------------------
// 		// Is this a Canned Report the user has clicked on?
// 		//----------------------------------------------------------------------
// 		if (trvCannedReports.SelectedNode.ToString() != "")
// 		{
// 			objSmartClient.ParamNamesAndValues =  Avoli.Controls.Exporter.ToReport(lblMessages, trvCannedReports, strFontName, strFontSize, cboReportExportTypes.SelectedText.ToString(), strExportPath, strExportDefaultFile, strConnection, chkShowDerivative.Checked, chkShowSQL.Checked, chkShowDecimals.Checked, chkShowGridLines.Checked, chkShowAverages.Checked, chkShowTotalCount.Checked, saParamNamesAndValues, Convert.ToBoolean(chkIgnoreURLs.Checked), Convert.ToBoolean(chkIncludeReportIDs.Checked), Convert.ToBoolean(chkAutoPrint.Checked), Convert.ToBoolean(chkPrintPreview.Checked), Convert.ToBoolean(chkPortrait.Checked), strDatabaseEnvironment);
// 			int intParams = objSmartClient.ParamNamesAndValues.GetLength(0) - 1;
// 			if (intParams == -1) intParams = 0;
// 			lblParamCnt.Text = "Params: " + intParams.ToString();
// 		}
// 		else
// 			Avoli.MessageBox.Show("You must select a Canned Report to run.", "");
// }
//
//
// 3) EXCEL data to HTML file (Grabs chunks of data in one trip)
// string strFileToSearch = @"c:\avoli\testfiles\test.xls";
// string strStartOfRange = "A3";
// string strEndOfRange = Avoli.DataStorage.File.XLS.GetCellValueBySearch(strFileToSearch, "Output", "<END OF FILE>", "A");
// string strColumnsToGrab = "A|B|C|F|H|I";
// if (strEndOfRange != "")
// {
// 		SuperString ss = strEndOfRange;
// 		strEndOfRange = ss.GetStringDecrement().ToString();  // Decrement 1 due to where we found our <END OF FILE>
// 		Array ar = Avoli.DataStorage.File.XLS.GetCellValuesBatch(strFileToSearch, "Output", strStartOfRange, strEndOfRange, strColumnsToGrab);
// 		Arrays.ToHTMLFile(ar, @"c:\export.htm", "Array Contents", true);
// }
// else
// 		Avoli.MessageBox.Show("The <END OF FILE> string could NOT be located in: " + strFileToSearch, "");
//
//
// 4) DATASET to Report (Via output form)
// string[] saProcNames = {"select * from [General Applications] order by [Application Name]"};
// string[] saProcValues = {"Log"};
// objSmartClient.DataSet = Avoli.DataStorage.Database.General.GetDataSet(objSmartClient, saProcNames, saProcValues);
// frmReporting.frmReportingResult result = frmReporting.Reference.Show(objSmartClient, "[General Applications] Query Result", null, -1, -1);
// objSmartClient.DataSet.Dispose();
//
//
// 5) Microsoft Visual Studio - Report Project (stand alone)
//  string strResults = Avoli.SystemInfo.Processes.Shell(@"C:\Avoli.ReportsApplication1\ReportsApplication1\bin\Debug\ReportsApplication1.exe", "", false, true);
//
//                                       .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region Avoli Stored proc param basics
//string sqlStmt; 
//string conString ;
//OleDbConnection cn =null ;
//OleDbCommand cmd =null ;
//try
//{
//sqlStmt = "insert into Emp (FirstName,LastName,Date) Values (?,?,?) ";
//conString = "Provider=sqloledb.1;user id=sa;pwd=;database=northwind;data source=localhost";
//cn =new OleDbConnection(conString);
//cmd = new OleDbCommand(sqlStmt, cn) ;
//cmd.Parameters.Add(new OleDbParameter("@FirstName", OleDbType.VarChar, 40));
//cmd.Parameters.Add(new OleDbParameter("@LastName", OleDbType.VarChar, 40));
//cmd.Parameters.Add(new OleDbParameter("@Date", OleDbType.Date));
//cmd.Parameters["@FirstName"].Value = txtFirstName.Text;
//cmd.Parameters["@LastName"].Value = txtLastName.Text;
//if ((txtDate.Text == "") )
//{
//cmd.Parameters["@Date"].Value = DBNull.Value;
//} 
//else
//{
//cmd.Parameters["@Date"].Value = DateTime.Parse(txtDate.Text);
//}
//cn.Open();
//cmd.ExecuteNonQuery();
//Label1.Text = "Record Inserted Succesfully";
//}
//catch (Exception ex)
//{ 
//Label1.Text = ex.Message;
//}
//finally
//{
//cn.Close();
//} 

#endregion

#region Avoli Prompting user for stored proc params automatically (objSmartClient.ParamNamesAndValues)
// How is the param extraction/determination done??? strSQL = "sp_help " + strStoredProcName;

// Return param names into an array with an empty value bucket
//		string[,] saParamsFound = DataStorage.Database.Schema.StoredProcParams(null, strConnection, cboQueries.Text);
//		for ( int i = 0; i < saParamsFound.GetLength(0); i++ )
//			for ( int j = 0; j < saParamsFound.GetLength(1); j++ )
//			Avoli.MessageBox.Show(saParamsFound.GetValue(i,j).ToString(), "");

// Determine all stored proc params & prompt the user for them
//		objSmartClient.SQLorStoredProc = strURLStoredProc;
//		objSmartClient.ParamNamesAndValues = new string[0,0];
//		objSmartClient.ParamNamesAndValues = Avoli.DataStorage.Database.Schema.StoredProcParamsGetValuesFromUser(objSmartClient);
//		strSQLorStoredProc += " " + strParamValues;
//		string[] arSPResults = Avoli.DataStorage.Database.General.ExecuteWithParams(cnn, strConnection, "sp_select_employee", new string[] {"@GetUserName(30)=" + ss.GetEnvironmentVariable("USERNAME").ToString()});
//		foreach(string result in arSPResults)
//			Avoli.MessageBox.Show (result, "", 0);

// How to replace params in a URL with user specified values for ACTUATE reporting
//		strNewURL = Web.URLParamValueEnforcer(objSmartClient, strURL, "sp=");

// Determination if we've already prompted the user for a value???
//		int intFoundAt = Avoli.rrays.LocateInArrayFuzzy(saURLQueryStringParams, "sp=", true);
//		strValueFound = saURLQueryStringParams[intFoundAt];

// Reports and Parameters
//		if (cboReportFontNames.Text != "") strFontName = cboReportFontNames.Text;
//		if (cboReportFontSizes.Text != "") strFontSize = cboReportFontSizes.Text;
//		strParentNodeName = trvCannedReports.SelectedNode.Parent.ToString().Replace("TreeNode: ", "");
//		SuperString ss = trvCannedReports.SelectedNode.ToString().Replace("TreeNode: ", "");
//		string strChildNodeName = ss.GetProperCase();
//		if (strParentNodeName == "Templates")
//		{ // Just in case we need to separate the 2 types of reports Canned vs Templates this block has been added.
//			objSmartClient.ParamNamesAndValues =  Avoli.Controls.Exporter.ToReport(lblMessages, trvCannedReports, strFontName, strFontSize, cboReportExportTypes.SelectedText.ToString(), strExportPath, strExportDefaultFile, strConnection, chkShowDerivative.Checked, chkShowSQL.Checked, chkShowDecimals.Checked, chkShowGridLines.Checked, chkShowAverages.Checked, chkShowTotalCount.Checked, saParamNamesAndValues, Convert.ToBoolean(chkIgnoreURLs.Checked), Convert.ToBoolean(chkIncludeReportIDs.Checked), Convert.ToBoolean(chkAutoPrint.Checked), Convert.ToBoolean(chkPrintPreview.Checked),  Convert.ToBoolean(chkPortrait.Checked), strDatabaseEnvironment);
//			int intParams = objSmartClient.ParamNamesAndValues.GetLength(0) - 1;
//			if (intParams == -1) intParams = 0;
//			lblParamCnt.Text = "Params: " + intParams.ToString();
//		}
//		else
//		{
//			//----------------------------------------------------------------------
//			// Is this a Canned Report the user has clicked on?
//			//----------------------------------------------------------------------
//			if (trvCannedReports.SelectedNode.ToString() != "")
//			{
//				objSmartClient.ParamNamesAndValues =  Avoli.Controls.Exporter.ToReport(lblMessages, trvCannedReports, strFontName, strFontSize, cboReportExportTypes.SelectedText.ToString(), strExportPath, strExportDefaultFile, strConnection, chkShowDerivative.Checked, chkShowSQL.Checked, chkShowDecimals.Checked, chkShowGridLines.Checked, chkShowAverages.Checked, chkShowTotalCount.Checked, saParamNamesAndValues, Convert.ToBoolean(chkIgnoreURLs.Checked), Convert.ToBoolean(chkIncludeReportIDs.Checked), Convert.ToBoolean(chkAutoPrint.Checked), Convert.ToBoolean(chkPrintPreview.Checked), Convert.ToBoolean(chkPortrait.Checked), strDatabaseEnvironment);
//				int intParams = objSmartClient.ParamNamesAndValues.GetLength(0) - 1;
//				if (intParams == -1) intParams = 0;
//				lblParamCnt.Text = "Params: " + intParams.ToString();
//			}
//			else
//				Avoli.MessageBox.Show("You must select a Canned Report to run.", "");
//		}
#endregion

#region Avoli Class Methods that apply PaintKits
// NOTE: Known framework skeletal controls (lblTitle, lblDescription, pnlHeader, pnlWindowMovement, pnlStatus)
// NOTE: Known framework forms (frmMDIMain parent)

// Avoli.Controls.Standard.Clear(dgResults);
// Avoli.DataStorage.Database.DataSetObject.ToControl(dsInfo, dgResults, "[General Log] search results", false, 0);
// Avoli.DataStorage.Database.General.ToControlBound(objSmartClient.cnn, objSmartClient.ConnectionString, strSQL, dgResults, "[General Log] search results", false, false, false);
// Avoli.DataStorage.Database.ToControlUnbound(objSmartClient.cnn, objSmartClient.ConnectionString, strSQL, dgResults, "[General Log] search results", false, false, "");
// Avoli.DataStorage.Database.DataTableObject.ToControl(dt, lstResults, "Results");
#endregion

#region Avoli Manual Control population
// Example# 1
// FROM: http://www.codeproject.com/cs/combobox/multicolumnlistbox.asp
//System.Data.DataSet ds = DataArray.ToDataSet(new object[,]
//	{ 
//	{"Row0, col0",  "Row0, col1" ,1},
//	{"Row00, col0", "Row1, col1" ,new object()},
//	{"Row1, col0",  "Row2, col1" ,"Some String"},
//	{"Row1a, col0", "Row3, col1" ,Rectangle.Empty},
//	{"row1aa,col0", "Row4, col1" ,1},
//	{"row0, col0",  "Row5, col1" ,1},
//	{"pow0, col0",  "Row6, col1" ,1},
//	{"Row7, col0",  "Row7, col1" ,new ExampleClass()},
//	{"Row8, col0",  "Row8, col1" ,Image.FromFile("StopLight.gif")}
//});
//listBox1 = new MultiColumnListBox();
//listBox1.Parent = this;
//listBox1.DataSource = arr; 

//Example# 2
//FROM: http://www.dotnet247.com/247reference/msgs/31/155430.aspx
//System.Data.DataTable dt = new System.Data.DataTable();
//DataRow dr;
//dt.Columns.Add(new DataColumn("IntegerValue", typeof(Int32)));
//dt.Columns.Add(new DataColumn("StringValue", typeof(string)));
//dt.Columns.Add(new DataColumn("CurrencyValue", typeof(double)));
//for (int i = 0; i < 10; i++)
//{
//	dr = dt.NewRow();
//	dr[0] = i;
//	dr[1] = "Item " + i.ToString();
//	dr[2] = 1.23 * (i+1);
//	dt.Rows.Add(dr);
//}
//DataView dv = new DataView(dt);
//listBox1.DataSource = dv;

// MANUAL DATA/CONTROL POPULATION - MULTIPLE COLUMNS
// string[] arColumnData = new string[] {"Jan Sales,Date,Time,Percent", "14150.25,12/14/06,12:11:04,34.19"};
// int[] arColumnWidths = new int[] {15,15,15,15};
// string[] arColumnDataTypes = new string[] {"Currency", "Date", "Time", "Percent"};
// Avoli.DataStorage.Manual.ToControlUnbound(listBox1,arColumnData,arColumnWidths,arColumnDataTypes,",",true);

// TEXT FILE DATA/CONTROL POPULATION - MULTIPLE COLUMNS (Not tested)
// string[] arColumnData = Avoli.IO.Import.ToStringArray(Environment.CurrentDirectory + "\\test.txt");
// int[] arColumnWidths = new int[] {15,15,15,15};
// string[] arColumnDataTypes = new string[] {"Currency", "Date", "Time", "Percent"};
// Avoli.DataStorage.Manual.ToControlUnbound(listBox1,arColumnData,arColumnWidths,arColumnDataTypes,",",true);

// System.Data.DataTable dt = new System.Data.DataTable("Users"); 
// dt.Columns.Add("Student Name",typeof(string)); 
// dt.Columns.Add("Misc", typeof(string)); 
// System.Data.DataRow dr = dt.NewRow(); 
// dr[0] = "Clarence"; 
// dr = "dada"; 
// dt.Rows.Add(dr); 
// System.Data.DataView dv = new System.Data.DataView(dt); 
// GridView1.DataSource = dv; 
// GridView1.DataBind(); 
#endregion

#region DEVELOPER: Class Reference Requirements
//+--------------------------------------------------------------------+
// FUNCTIONALITY TIP:
// Always look at the SuperString level to see if the functionality
// you desire exists there first.  It's VERY robust and much has
// been placed at the string level of SuperString since much of
// what we do as developers is in connection with a string.	
//+--------------------------------------------------------------------+

//+--------------------------------------------------------------------+
// REFERENCES:	System.Management reference (for WMI functionality)
//				System.Web reference (for SMTP functionality etc.)
//				Excel reference (Comment out code if not needed)
//				Word reference (Comment out code if not needed)
//				System.ServiceProcess (for Avoli.SystemInfo.Services)
//				System.DirectoryServices (for Avoli.Security.LDAP.IsAuthenticated)
//+--------------------------------------------------------------------+

// TO DO (In order of priority)
// 1) Controls in GUI (PaintGUI) - Add PictureBox & ComboBox (Requires reading SQL Server realtions)
// 2) Tighten up Formatting in Grid (DataStorage.Database.DataGridResizeandFormat)
// 3) All ToControl's have ListView/TreeView
// 4) DataTableToControl (Add ListView/TreeView) - (Allow for multiple columns on ListView/TreeView)

// How To
// SET OUTGOING values from a method... GetFilename(out string filename, DragEventArgs e)
#endregion

#region DEVELOPER: Worthy URL's
// GIS Development			http://www.freegis.org/database/?cat=1&_ZopeId=95245440A2Rv6jsXurs
// GIS Mapping with C#		http://sharpmap.iter.dk/download.aspx
// GIS DataSets				http://datafed.net/
// GPS						http://pscode.com/vb/scripts/ShowCode.asp?txtCodeId=3123&lngWId=10
// Remote Control			http://www.auditmypc.com/process/winvnc.asp		WinVNC (Remote control by IP)
// Charting					http://www.dotnetcharting.com
// Fingerprinting			http://www.devx.com/security/Article/31576	(Code example)
//							http://www.griaule.com/page/en-us/downloads	(Library)
//							http://www.microsoft.com/hardware/mouseandkeyboard/productdetails.aspx?pid=036	(Hardware)
//							(SCENARIO: Fingerprint mapping is attached directly to EMPLOYEE/USER main record & compared for entry)
#endregion

#region DEVELOPER: Jargon
// Key Container        (Storage facility for password keys)
// Data Chambers        (A Avoli.data storage method)
// Data Silos           (A Avoli.data storage method)
// Consume				(Example: Consume a web service that say uses LDAP for authentication with provided credentials)
//						http://pscode.com/vb/scripts/ShowCode.asp?txtCodeId=124&lngWId=10
// Strong Type/Casting	(Example: Code with strong types)
// Garbage Collection	(Kicks in when a memory allocation reaches a threshold which depends on amt of physical memory available, size of the processor cache etc.)
// Reflection			(Is the ability for a program at run-time to get information about loaded type and assemblies. It can be used to access and invoke members and fields by their name, for instance.) 

// Hive					(A Avoli.term signifying a hive of files - See: Avoli.Web.FileDownload)
// Trottle				(A Avoli.term signifying in Threading)
// Mine					(A Avoli.term signifying mining of information - See: Avoli.SystemInfo.Keyboard.KeyLogger.Record)
// Digest				(Refers to the Key Size utilized in the Drye Encryption scheme)
//						(Microsoft: Refers to digest for authenication via Active Directory)
// Harness				(A Avoli.term signifying a snatch and hold in memory scenario)
#endregion

#region DEVELOPER: WORK AROUNDS
// Use Environment.NewLine when \r\n isn't working
#endregion

#region Stuff for use for: Employee monitoring (Corporate fraud investigations area)
// Monitoring and Alerting System
// Architecture system expert to gather/collect employee information over time (Over a years worth of data)
// Electronic investigations tool (Analyzing packets through network monitors)
// Mainframe jobs going against IMS data/logs (Look for excessive queries with few deposit/withdrawel transactions)
// Pulls employee activity over time
// Running Artificial Intelligence models (trends and anomolies)
#endregion

#region Stuff to use for: Capture Monitor - DOS Command List
//                                           \\|//       
//                                          ~~O-O~~
//+-------------------------------------oOOo--(_)--oOOo------------------------------------+
// USEFULL DOS COMMANDS (To be used with System.Info.Processes.Shell)
// Ex 1: Avoli.MessageBox.Show(Avoli.SystemInfo.Processes.Shell("net.exe", "send " + ss.MachineGetName().ToString() + " Hi guy!", true), "Communications Result");
// Ex 2: Avoli.MessageBox.Show(Avoli.SystemInfo.Processes.Shell("ping.exe", "192.168.0.1", true), "Result of Shell");
// Ex 3: Avoli.MessageBox.Show(Avoli.SystemInfo.Processes.Shell("ipconfig.exe", "", true), "Result of Shell");
//
// DOS COMMAND LIST
// ----------------
// SYSETMINFO
// SC QUERY <-- Queries all Windows Services information
//		sc create newservice binpath= c:\nt\system32\newserv.exe type= own start= auto depend= "+tdi netbios"
//
// LPR -S HostIPaddress -P PrinterName FileName  <-- Directs a print job to a UNIX device
//
#region Ping
// PING - Use this to test Threshold speed ---> intReplyTime = Convert.ToInt(ss.StringGetValueBetween("time ", " ms "));
//		Pinging DevLaptop [192.168.0.4] with 32 bytes of data:
//
//		Reply from 192.168.0.4: bytes=32 time<1ms TTL=128
//		Reply from 192.168.0.4: bytes=32 time<1ms TTL=128
//		Reply from 192.168.0.4: bytes=32 time<1ms TTL=128
//		Reply from 192.168.0.4: bytes=32 time<1ms TTL=128
//
//		Ping statistics for 192.168.0.4:
//		Packets: Sent = 4, Received = 4, Lost = 0 (0% loss),
//		Approximate round trip times in milli-seconds:
//		Minimum = 0ms, Maximum = 0ms, Average = 0ms
#endregion

#region AT (Command Scheduler) Example
// Starting/Stopping the scheduler service on the Local machine:
// 		sc stop schedule
//		sc query schedule <-- If return contains PENDING than it's busy, try again for a better result until STOPPED/PAUSED/RUNNING
//		sc start schedule
//
// Manually Launching the Windows scheduler -> %SystemRoot%\explorer.exe ::{20D04FE0-3AEA-1069-A2D8-08002B30309D}\::{21EC2020-3AEA-1069-A2DD-08002B30309D}\::{D6277990-4C6A-11CF-8D87-00AA0060F5BF}
//
// AT Command Syntax...
//			/delete - A scheduled command is cancelled by specifying its ID number. 
//			/interactive - The batch job can interact with the user that is logged on when it runs. 
//			/every: date - Specifies a day or days of the week the command will be run on. 
//						   Days are "M, T, W, Th, F, S, Su)" or a numeric month day. 
//						   Multiple parameters may be specified separated by commas. 
//			/next: date - Schedules the command to run once on the next day indicated. 
//
//		AT 1:00 /every:M,1 BACKUP.BAT <-- Schedule it to run every MONDAY
//
//		at 6:50PM "c:\winnt\task.bat" <-- Thus schedules the task.bat to be run @ 6:50PM today 
//										  unless the task is scheduled after the time specified. 
//										  IMPORTANT: Because the \interactive switch is not in the  
//										  command this batch file will be executed in the background. 
//										  Use the /k switch to troubleshoot a scheduled job.
//
//		at 12:00 NET USE W: \\servername\sharename
//
//		at 6:50PM \interactive c:\winnt\beep.bat - 
//
//		at 3:00 /every:Su,M,Tu,Th,F,Sa dbackup.bat
//			dbackup.bat contents...
//				@echo off
//				net pause server
//				net send * Please get off this server. It will go down in 5 minutes.
//				sleep 300
//				net stop server
//				ntbackup d: ... <whatever_options_you_need>
//				net start server
//
//		at 1:00 /every:M,Tu,W,Th,F,Sa,Su net time /set /yes <-- Syncronize computers time with server
//
//		at 1 /delete - Deletes the at ID 1.
//
//		at /delete /yes - Delete all AT ID's.
//
//		at - Typing at alone will display each of the scheduled tasks, their status, id, scheduled time, and command as shown below. As you can see if an error occurs with the at process the status will display an "Error". 
//			Status ID   Day                     Time          Command Line
//			-------------------------------------------------------------------------------
//			1   Tomorrow                6:50 AM       c:\winnt\task.bat
//			2   Tomorrow                2:48 PM       c:\ch\hope\logit.bat
//			Error   3   Tomorrow                6:50 PM       start notepad
//
//		at 1 - Typing at followed by an ID number will display additional information about the task id as shown below.
//			Task ID:       5
//			Status:        OK
//			Schedule:      Today
//			Time of day:   6:55 PM
//			Interactive:   No
//			Command:       c:\winnt\task.bat
#endregion
//
// NET SEND
// IPCONFIG
// SHUTDOWN
// BATCH (batch myfile.txt) (USE: Possibly for Telnet/FTP etc.)
// COMP (comp file1.txt file2.txt /N=10 /A)
// DELTREE (deltree c:\windows\fake010)
// TREE (Result is probably tab indented) - Great for a multiline text box with BOTH scrollbars READONLY
// MEM (All memory information)
// XCOPY 
//		try
//		{
//			System.Diagnostics.Process myCmd = null;
//			System.Diagnostics.ProcessStartInfo qOptions = new System.Diagnostics.ProcessStartInfo(@"cmd.exe", @"/C Xcopy " + source + " " + destination + " /Y/H/S/Q/R");
//			qOptions.UseShellExecute = false; 
//			myCmd = System.Diagnostics.Process.Start(qOptions);		
//			return true;
//		}
//		catch 
//		{
//			return false;
//		}
//
#region ROUTE PRINT Example
// ROUTE PRINT
//		===========================================================================
//		Interface List
//		0x1 ........................... MS TCP Loopback interface
//		0x2 ...00 90 4b c6 b1 74 ...... Broadcom 802.11g Network Adapter - Packet Scheduler Miniport
//		0x3 ...00 e0 b8 5c d7 c1 ...... Intel(R) PRO/1000 CT Network Connection - Packet Scheduler Miniport
//		===========================================================================
//		===========================================================================
//		Active Routes:
//		Network Destination        Netmask          Gateway       Interface		Metric
//		0.0.0.0					   0.0.0.0			192.168.0.1	  192.168.0.5	  25
//		0.0.0.0					   0.0.0.0			192.168.0.1	  192.168.0.4	  20
//		127.0.0.0				   255.0.0.0		127.0.0.1	  127.0.0.1		  1
//		192.168.0.0			       255.255.255.0	192.168.0.4	  192.168.0.4	  20
//		192.168.0.0			       255.255.255.0	192.168.0.5	  192.168.0.5	  25
//		192.168.0.4			       255.255.255.255	127.0.0.1	  127.0.0.1		  20
//		192.168.0.5				   255.255.255.255	127.0.0.1	  127.0.0.1		  25
//		192.168.0.255		       255.255.255.255	192.168.0.4   192.168.0.4	  20
//		192.168.0.255		       255.255.255.255	192.168.0.5   192.168.0.5	  25
//		224.0.0.0				   240.0.0.0		192.168.0.4   192.168.0.4	  20
//		224.0.0.0				   240.0.0.0		192.168.0.5   192.168.0.5	  25
//		255.255.255.255			   255.255.255.255  192.168.0.4   192.168.0.4	  1
//		255.255.255.255			   255.255.255.255  192.168.0.5   192.168.0.5	  1
//		Default Gateway:           192.168.0.1
//		===========================================================================
//		Persistent Routes:
//		None
#endregion
//
#region NTBACKUP Example
// NTBACKUP
//		NTBACKUP [/nopoll] [/missingtape]
//		NTBACKUP path\filenames options
//		NTBACKUP operation path [/a] [/v] [/r] [/d description] [/b] [/hc:on:off] [/t option] [/l filename] [/e] [/tape:n] 
//
//			Path\filenames - The location and name of files to be backed up 
//			Options are: 
//			/A - Append the backup set to the current tape. 
//			/b - Back up the system Registry. 
//			/d "text" - Backup set description text. 
//			/e - Create a summary log file instead of a detailed log file. 
//			/l filename - Set the log file name to the name specified rather than the default name. 
//			/r - Restrict access to the tape to Administrators, Backup Operators, Server Operators, or users who perform the backup. 
//			/t type - The type of backup to perform. Options are Normal, Copy, Incremental, Differential, Daily. The default is Normal. 
//			/v - Verify that the files were backed up correctly. 
//			/hc: on/off - Turn hardware compression on or off. 
#endregion
//
// CACLS.EXE
//		Provides for editing of the access control list (ACL) of files and directories from the command line. User rights are specified by N-none, C-change, R-read, F-full. Options are: 
//		/T - Change the access control lists of the files specified in this directory and all subdirectories. 
//		/E - Don't replace the access control list, but modify it. 
//		/C - Change access control lists while ignoring errors. 
//		/G - Grant user rights 
//		/R - Used with the /E option is used to revoke user rights 
//		/P - Replace the specified user rights with those specified by one of N, C, R, or F. 
//		/D - Deny access to the specified user 
//
//                                        .oooO
//                                       (   )   Oooo.
//+---------------------------------------\ (----(   )-------------------------------------+
//                                         \_)    ) /
//                                               (_/
#endregion

#region Stuff to use for: Capture Monitor - Individual Monitors
//                                           \\|//
//_______________________________________ooO_(o"o)_OoO_____________________________________+
//                                            (_)
//

// DB Structure
// [Capture Monitors]
//		ID
//		Name
//		DaysOfWeekToRun
//		WindowStartDate
//		WindowEndDate
//		WeekdaysToRun
//		Attributes (Daily, Weekly, Monthly, Quarterly, Yearly)
//		NextRunDateTime
//		LastRunDateTime
//		Enabled
//		Status
//		LastError

// MONITORS
// --------
// Ethical Hack					->	???
// Disk Space					->	if (Avoli.SystemInfo.Drives.IsBelowSpaceThreshold(@"c:\", "10 MB"))
//									Avoli.MessageBox.Show(@"Drive c:\ is below what we'd like to see!!", "Disk Space Agent");
// DriveExists					->	Avoli.MessageBox.Show (System.Convert.ToString(SystemInfo.Drives.IsDriveValid ("C:\\")), "");
// FolderExists					->	Avoli.SystemInfo.Directories.Exists(@"c:\test");
// FileExists					->	if (System.IO.File.Exists(strFile) == true)
// PING							->	blnOkay = Convert.ToBoolean(Avoli.Processes.Shell("ping 192.168.0.1", true));
//									ss.ProcessShell("ping.exe", "1.50.12.224", true)
// HTTP							->	bool blnAvailable = ss.WebIsURLAvailable("http://www.cnn.com");
// STOCK QUOTE					->	ss.WebGetStockQuote("TGIC");
// RESOLVE IP					->	Avoli.MessageBox.Show(ss.WebGetResolveIP("127.0.0.1"), "");
// DNS INFO						->	Avoli.MessageBox.Show (ss.WebGetDNSInfo("www.cnn.com"),"");
// HPPrinterLCDStatus			->	Look here...http://odetocode.com/Humor/68.aspx
// Serial Communications		->	???	
// Network Drive Exists			->	Shell("NET USE U: \\master\testshare /User:Enterprise\dchoksi passwd /PERSISTENT:YES");
//									ss.ProcessShellGetResults("MSInfo")
// REGISTRY						->	ss.RegistryGetValue("Software\\" + System.Windows.Forms.Application.ProductName + @"\WindowPosition", "X")
// INI							->	ss.INIRead(@"c:\test.ini", "dailyspecial", "monday");
// XML							->	ss.XMLRead(@"c:\test.xml", "dailyspecial", "monday");
// File Contains				->	ss = ss.FileGetContents(@"c:\test.txt", false);
// Is App Running				->	bool blnResult = Avoli.SystemInfo.Processes.IsRunning("calc").ToString();
// Server Exists				->	??? (UNCName check)
// Services						->	bool bnlIsRunning = Avoli.SystemInfo.Services.IsRunning("Computer Browser");
//									bool blnExists = Avoli.SystemInfo.Services.DoesExist("Computer Browser");
// Database Connection			->	bool blnResult = Avoli.DataStorage.Database.General.CanConnect("Northwind");
// Database Value Check			->	ss.DatabaseGetValue(strConnection, strSQLorStoredProc)
// DataStorage.Database.ToString(..."select count(*) from .. ")
// REXEC (Ex: BDF)
// Atomic Clock					->	bool blnResult = Avoli.Time.GetTimeNTP("EDT", true);
// LocalSystem.Registry.Read	->	txtKickOffTime.Text = Registry.QueryValue(Registry.ERegistryPossibleRoots.HKEY_LOCALE_MACHINE, strLocation, "AutoRun", "").ToString();
// FTP File Exists (NOTHING FOR THIS YET)
// STRING COMPARISIONS			->	StringContainsOneOfTheseValues (string strValuesToLookFor, string strValueDelimiter, string strValueExample)
//									string strResult = ss.StringGetValueBetween("[", "]");
//
// LAUNCHING
// ---------
// ss.LaunchAssociationWait("");
//    or 
// ss.LaunchAssociationWait("notepad");

// NOTIFICATIONS
// -------------
// NET SEND		->	ss.ProcessShellGetNetSend(string cuid, string message)
// SOUND		->	ss.Play(Avoli.SystemInfo.General.GetAliasValue("My Documents") + @"Avoli.Test Files\test.wav");   
// SMTP EMAIL	->	if (!ss.EmailSMTPSend("imc.capmark.funb.com", "joe@here.com", "moe@here.com", "bo@here.com", "yo@here.com", "Sending	files",	"Here are...", System.Web.Mail.MailFormat.Html, new string[]	{"c:\file1.bmp", "c:\file2.bmp"}, false)) 
//						Avoli.MessageBox.Show("Unable to send email via SMTP.	Check your server name and that	the	server is up and operational.", ""); 

//
//________________________________________oooO____Oooo_____________________________________+
//                                        (  ) || (  )
//                                         \_) || (_/
//                                              \
//                                              /
//                                              o
#endregion

#region Stuff to use for: Capture Monitor - GUI Development
// To ensure the Common.cs contains also everything needed for a monitor/job scheduling system this
// section has been created.
//
// 1) Kickoff on f @ 1:00AM | Set PC Time | Succeed Script | Fail Script
//		Execute... Avoli.Time.GetTimeNTP("EDT", false)
//
// SCRIPT COMMANDS (See EZMon app I wrote in VB)
// Notify ...	string strResult = ss.ProcessShellGetNetSend("ComputerName", "This is the message.");
// Email ...	if (!ss.EmailSMTPSend("imc.capmark.funb.com", "joe@here.com", "moe@here.com", "bo@here.com", "yo@here.com", "Sending	files",	"Here are...", new string[]	{"c:\file1.bmp", "c:\file2.bmp"}, false)) 
//					Avoli.MessageBox.Show("Unable to send email via SMTP.	Check your server name and that	the	server is up and operational.", ""); 
// Log ...		ss.MachineGetInfo();
// EventLog ...	Avoli.DataStorage.LocalSystem.EventLog.Write ("", "Error occurred in FileExists for: " + strFile, True);			
// Exec ...		ss.ProcessShell("ping.exe", "1.50.12.224", true))
// Play ...		ss.Play(@"c:\test.wav");
// Print ...	ss.Print("|", "", "String Contents");
// Save ...		ss.GetWindowsTempDir();
//
//
// JOB INFORMATION (By section)
// (textbox)	Job Name
// (dropdown)	Dependent job
//
// [Resource to Monitor]
//	cboMonitorType.Items.Add("ISAPPRUNNING");		// Is an App running?
//		bool blnResult = Avoli.SystemInfo.Processes.IsRunning("calc").ToString();
//	cboMonitorType.Items.Add("APP LAUNCH");
//		ss.ExecutionLaunchAssociation("c:\\test.txt")
//		ss.ExecutionLaunchAssociationWait("c:\\test.txt", strViewerName)
//	cboMonitorType.Items.Add("WEATHER");
//		string[] saWeatherData = Avoli.Weather.Check(27265,"");
//		for(int i = 0; i < saWeatherData.Length; i++)
//				Avoli.MessageBox.Show(saWeatherData[i].ToString(), "");
//	cboMonitorType.Items.Add("UNC PATH");
//	cboMonitorType.Items.Add("DOS TERMINAL");		// Execute & evaluate results
//		ss.ProcessShell("ipconfig.exe", "", true)
//	cboMonitorType.Items.Add("TELNET TERMINAL");
//	cboMonitorType.Items.Add("TELNET SESSION");
//	cboMonitorType.Items.Add("BDF");		// Uses Telnet Session but compares individual drive space
//  cboMonitorType.Items.Add("FOLDER EXISTS")
//		Avoli.SystemInfo.Directories.Exists(@"c:\test");
//	cboMonitorType.Items.Add("SPACE ON LOCAL");
//		if (Avoli.SystemInfo.Drives.IsBelowSpaceThreshold(@"c:\", "10 MB"))
//			Avoli.MessageBox.Show(@"Drive c:\ is below what we'd like to see!!", "Disk Space Agent");
//	cboMonitorType.Items.Add("SPACE ON REMOTE MACHINE");
//	cboMonitorType.Items.Add("DIRECTORY EXISTS");
//		System.Convert.ToString(SystemInfo.Drives.IsDriveValid ("C:\\"))
//	cboMonitorType.Items.Add("FILES LEAVE");
//	cboMonitorType.Items.Add("FILE EXISTS");
//		if (System.IO.File.Exists(strFile) == true)
//	cboMonitorType.Items.Add("FILE EMPTY");
//		ss.FileGetLineCount("c:\\test.txt")
//		Avoli.IO.Import.ToString(@"C:\test.txt");
//		ss.FileGetContents("c:\\test.txt", false);
//	cboMonitorType.Items.Add("FILE CONTAINS");
//		ss.FileGetContents(@"c:\test.txt", false);
//		blnResult = ss.StringContainsOneOfTheseValues(strValuesToLookFor, strValueDelimiter, strValueExample)
//		string strResult = ss.StringGetValueBetween("[", "]");
//  cboMonitorType.Items.Add("FILE ENDS WITH");
//		ss.FileGetTailValue(@"c:\test.txt", 50);
//	cboMonitorType.Items.Add("FTP FILE EXISTS");
//  cboMonitorType.Items.Add("ENVIRONMENT VARIABLE");
//		ss.GetEnvironmentVariable("HOMEPATH");
//  cboMonitorType.Items.Add("INI");
//		ss.INIRead(string strINIFile, string strSection, string strKeyName)
//  cboMontiorType.Items.Add("XML");
//		ss.XMLRead(@"c:\test.xml", "dailyspecial", "monday");
//  cboMonitorType.Items.Add("REGISTRY");
//		ss.RegistryGetValue("Software\\" + System.Windows.Forms.Application.ProductName + @"\WindowPosition", "X")
//		Avoli.DataStorage.LocalSystem.Registry.QueryValue(Avoli.DataStorage.LocalSystem.Registry.ERegistryPossibleRoots.HKEY_LOCALE_MACHINE, "Software\\NGS\\key1\\key2\\key3", "DWordVal", (int)0).ToString())
//  cboMonitorType.Items.Add("STOCK QUOTE");
//		ss.WebGetStockQuote("TGIC");
//	cboMonitorType.Items.Add("IP");
//		blnOkay = Convert.ToBoolean(Avoli.Processes.Shell("ping 192.168.0.1", true));
//		ss.ProcessShell("ping.exe", "1.50.12.224", true)
//	cboMonitorType.Items.Add("IPRESOLVE");
//		ss.WebGetResolveIP("127.0.0.1")
//  cboMonitorType.Items.Add("DNS");
//		ss.WebGetDNSInfo("www.cnn.com")
//	cboMonitorType.Items.Add("URL");            // Availability check
//		bool blnAvailable = ss.WebIsURLAvailable("http://www.cnn.com");
//	cboMonitorType.Items.Add("DATABASE");       // Database data monitor
//	cboMonitorType.Items.Add("DATABASEFILES");  // Database data with corresponding files monitor
//	cboMonitorType.Items.Add("NTP");            // Atomic clock monitor
//  cboMonitorType.Items.Add("SNMP");			// Device status monitor (Such as a printer's status)
//  cboMonitorType.Items.Add("HP PRINTER");		// Device status monitor (Such as a printer's LCD)
//	cboMonitorType.Items.Add("PASSTHRU");		// Execute Pass Script without a monitor
//	cboMonitorType.Items.Add("DOWN RPT");		// Create a DOWN report & execute the Pass script (Typically emailed via script)
//	cboMonitorType.Items.Add("WEEKLY RPT");		// Create a WEEKLY report & execute the Pass script (Typically emailed via script)
//	cboMonitorType.Items.Add("MONTHLY RPT");	// Create a MONTHLY report & execute the Pass script (Typically emailed via script)
//
//
// PASSTHRU EXAMPLES
// -----------------
// Atomic Clock		->	Avoli.Time.GetTimeNTP("EDT", false)
// Screen Capture	->	Avoli.Imaging.PerformCapture(this, @"c:\captured.jpg");
//
//
// (listbox)	Examples of command syntax
//
// (textbox)	Initial retries
// (textbox)	Monitor for (loose description)
// (textbox)	Pass script
// (textbox)	Fail script
//
// [Database Connection Information]
// (textbox)	SQLServer
// (textbox)	Database
// (textbox)	Last Query Results
//
// (checkbox)	Capture result to file
// (textbox)	Email result to
//
// (button)		Test Monitor
//
// [Notifications]
// (textbox)	Times to notify on errors
// (textbox)	Times notified count (readonly)
// (checkbox)	Log activity flag
//
// [App to Launch]
// (textbox)	App to launch (BROWSE button)
// (textbox)	App caption/class name
// (checkbox)	Disallow multiple instances
// (button)		Test Run
//
// [Time Window]
// (textbox)	Run every HH:MM:SS
// (textbox)	Between the hours of HH:MM:SS and HH:MM:SS
//
// [Calendar Frequency Interval]
//	cboFrequency.Items.Add("Daily");
//	cboFrequency.Items.Add("Weekly");
//	cboFrequency.Items.Add("Monthly");
//	cboFrequency.Items.Add("Quarterly");
//	cboFrequency.Items.Add("Yearly");
//	cboFrequency.Items.Add("Once");
//
//	lstPickDays.Items.Add("Sunday");
//	lstPickDays.Items.Add("Monday");
//	lstPickDays.Items.Add("Tuesday");
//	lstPickDays.Items.Add("Wednesday");
//	lstPickDays.Items.Add("Thursday");
//	lstPickDays.Items.Add("Friday");
//	lstPickDays.Items.Add("Saturday");
//
// (button)		Run today
// (button)		Run every day
// (button)		Run weekdays
// (button)		Run weekends
// 
// DOWN INFORMATION
// (checkbox)	Job failing flag (readonly)
// (textbox)	Down iteration count
// (textbox)	Support group
// (textbox)	Effects what?
// (textbox)	Corrective Action
// (textbox)	Last error date
// (textbox)	Initial error date
//
// SCHEDULER AGENT TAB (Job Queue)
// Displays all jobs in listview (sorted by kickoff times)
// (button) Run all failing jobs
// (button) Create a job
// (button) Modify jobs
// (button) Print all jobs
// (button) Print failing jobs
// (button) Pause the poller (timer control)
// (button) Create copy of a job
// (button) Modify a job
// (button) Disable failing jobs
// (button) Disable jobs by type (In case a particular monitor type is causing a problem)
// (button) Disable	(single job or all jobs)
// (button) Enable	(single job or all jobs)
//
// ACTIVITY TAB
// Displays all job activity in listview (sorted by latest date/time)
// 
// TOOLS TAB
// Allows Ad-Hoc execution of 
//
// REPORTS TAB
// (textbox)	Search
// (button)		Fetch activity for
// (button)		Print activity for
// (options)	Check Severity -> Warning | Critical | Succeeded | all
// (options)	Time Frame -> Daily | Weekly | Monthly | Quarterly | Yearly
// (options)	Weekday -> Sun | Mon | Tue | Wed | Thu | Fri | Sat | All
// (listview)	Activity returned from search (Lists 500 lines most)
// (button)		Email results
#endregion

#region Stuff to use for: Capture Monitor - Ad Hod Spy's
// National Weather	(Zip Code, City Forecast, Temp, Feels Like, Wind, Reported By, Having Bad Weather?)
// Ping Spy			(Is remote site/device available?)
// UNCName Path Spy	(Can we connect to a server path? - Availability test)
// Net Send Spy		(Does Net Send work?)
// App Running Spy	(Is app running?)
// URL HTTP Spy		(Is site up test)
// FTP File Spy		(For file drops)
// Disk Space Spy	(\\intranet_svr\c$ 2000 <- connect & test on the fly option)
// SMTP Email Spy	(Is SMTP email running?)
// Services			(Is a service running?)
// SQL Window		(Is database up?  Are results returned? - Test database connection & data retrieval)    
//                  cboInformation.AddItem "sp_who"
//					cboInformation.AddItem "sp_lock"
//					cboInformation.AddItem "sp_helpuser"
//					cboInformation.AddItem "sp_helpserver"
//					cboInformation.AddItem "select @@version"
//					cboInformation.AddItem "select @@servername"
//					cboInformation.AddItem "select @@language"
//					cboInformation.AddItem "select @@cpu_busy"
//					cboInformation.AddItem "select @@io_busy"
//					cboInformation.AddItem "select @@idle"
//
// NETWORK TIME PROTOCOL (NTP) - ATOMIC CLOCK Time Servers
// Name								IP Address		Location 
// time-a.nist.gov					129.6.15.28		NIST, Gaithersburg, Maryland 
// time-b.nist.gov					129.6.15.29		NIST, Gaithersburg, Maryland 
// time-a.timefreq.bldrdoc.gov		132.163.4.101	NIST, Boulder, Colorado 
// time-b.timefreq.bldrdoc.gov		132.163.4.102	NIST, Boulder, Colorado 
// time-c.timefreq.bldrdoc.gov		132.163.4.103	NIST, Boulder, Colorado 
// utcnist.colorado.edu				128.138.140.44	University of Colorado, Boulder 
// time.nist.gov					192.43.244.18	NCAR, Boulder, Colorado 
// time-nw.nist.gov					131.107.1.10	Microsoft, Redmond, Washington 
// nist1.datum.com					209.0.72.7		Datum, San Jose, California 
// nist1.dc.certifiedtime.com		216.200.93.8	Abovnet, Virginia 
// nist1.nyc.certifiedtime.com		208.184.49.9	Abovnet, New York City 
// nist1.sjc.certifiedtime.com		208.185.146.41	Abovnet, San Jose, California 
#endregion

#region Stuff to use for: Capture Docs
// Intended to mimic Outlook Drop Folder email mechanism
// int intEmailsSent = Avoli.Notifications.Email.DropZoneWatch(@"c:\outgoing\", ".ini", "imc.capmark.funb.com", lstResults, lblPropellerControl);

// Intended to mimic Loftware's Print Server Drop Folder printing mechanism
// int intBarcodesFound = Avoli.Barcode.DropZoneWatch(@"c:\outgoing\", ".sdl", @"\\nc1wachmprn002\cpl008002", lstResults, lblPropellerControl);

// Avoli.Imaging...
// Avoli.DataStorage.Database.Blob...
// Avoli.DataStorage.Stamp...
// Avoli.DataStorage.Database.DataSetObject.WriteXML
// Avoli.DataStorage.Database.DataSetObject.ReadXML
// Avoli.Crypto...
#endregion

#region Database Connectivity - Microsoft Access
//// Query our MS Access database
//System.Data.OleDb.OleDbConnection cnn = new System.Data.OleDb.OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + lblZipCodePath.Text.ToString() + ";User Id=admin;Password=;");
//System.Data.OleDb.OleDbDataAdapter da;
//cnn.Open();
//da = new System.Data.OleDb.OleDbDataAdapter(strSQL, cnn);
//System.Data.DataSet ds = new System.Data.DataSet();
//da.Fill(ds, "Zips");
//foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
//{
//    //for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
//    //{
//    //    System.Windows.Forms.MessageBox.Show(j.ToString() + " - " + ds.Tables[0].Columns[j].ColumnName.ToUpper());
//    //}

//    // 1) Get the next ID, City, StateAbbr, StateName, Zip...
//    strCity = dr[1].ToString();
//    strStateAbbr = dr[2].ToString();
//    strState = dr[3].ToString();
//    strZip = dr[4].ToString();

//    lblStatus.Text = strCity + ", " + strState;
//    lblStatus.Refresh();
//}
#endregion

#region Database Connectivity - H2database.com
//Using the JDBC API on .NET
//•Install the .NET Framework from Microsoft. Mono has not yet been tested. 
//•Install IKVM.NET. (http://www.ikvm.net)
//•Copy the h2*.jar file to ikvm/bin 
//•Run the H2 Console using: ikvm -jar h2*.jar 
//•Convert the H2 Console to an .exe file using: ikvmc -target:winexe h2*.jar. You may ignore the warnings. 
//•Create a .dll file using (change the version accordingly): ikvmc.exe -target:library -version:1.0.69.0 h2*.jar 
//If you want your C# application use H2, you need to add the h2.dll and the IKVM.OpenJDK.ClassLibrary.dll to your C# solution. Here some sample code: 

//using System;
//using java.sql;

//class Test
//{
//    static public void Main()
//    {
//        org.h2.Driver.load();
//        Connection conn = DriverManager.getConnection("jdbc:h2:~/test", "sa", "sa");
//        Statement stat = conn.createStatement();
//        ResultSet rs = stat.executeQuery("SELECT 'Hello World'");
//        while (rs.next())
//        {
//            Console.WriteLine(rs.getString(1));
//        }
//    }
//}
#endregion

#region CODES: Hayes Modem AT Command Set
//Command Description Comments
//A0 or A Answer incoming call
//A/ Repeat last command Don't preface with AT, don't follow with carriage return. Enter usually aborts. 
//B0 or B Call negotiation V32 Mode/CCITT Answer Seq. Today, most modems ignore this command, though they answer "OK" for compatibility. 
//B1 Call negotiation Bell 212A Answer Seq. Ignored by most modern modems. 
//B2 Call negotiation Verbose/Quiet On Answer. Ignored by most modern modems. 
//D Dial Dial the following number and then handshake in originate mode.

//P - Pulse Dial
//T - Touch Tone Dial
//W - Wait for the second dial tone
//R - Reverse to answer-mode after dialing
//@ - Wait for up to 30 seconds for one or more ringbacks
//, - Pause for the time specified in register S8 (usually 2 seconds)
//; - Remain in command mode after dialing.
//! - Flash switch-hook (Hang up for a half second, as in transferring a call.)
//L - Dial last number

//E0 or E No Echo Will not echo commands to the computer 
//E1 Echo Will echo commands to the computer (so one can see what one types) 
//H0 Hook Status On hook - Hang up 
//H1 Hook status Off hook - phone picked up 
//I0 or I Inquiry, Information, or Interrogation This command is very model specific. I0 usually returns a number or code, 
//while higher numbers often provide much more useful information. Today, Windows uses this for Plug and Play detection of specific modem types.

//I3 or I Information (3) This command, on newer modems, usually returns the make and model number of the modem. For example: ATI3 returns LT V.90 Data+Fax Modem Version 6.00. 
//L0 or L Speaker Loudness. Modems with volume control knobs will not always have these options. Off or low volume 
//L1  Low Volume 
//L2  Medium Volume 
//L3  Loud or High Volume 
//M0 or M Speaker off M3 is also common, but different on many brands 
//M1  Speaker on until remote carrier detected (i.e. until the other modem is heard) 
//M2  Speaker always on (data sounds are heard after CONNECT) 
//N0 or N Handshake Speed Handshake only at speed in S37 
//N1  Handshake at highest speed larger than S37 
//O0 or O Return Online Returns the modem back to the normal connected state after being interrupted by the "+++" escape code. 
//O1  Return Online after an equalizer retrain sequence 
//Q0 or Q1 Quiet Mode Off - Displays result codes, user sees command responses (e.g. OK) 
//Q1 Quiet Mode On - Result codes are suppressed, user does not see responses. 
//Sn Select current register 
//Note that Sn, ? and =r are actually three separate commands, and can be given in separate AT commands.
// Select register n as the current register 
//Sn? Select register n as the current register, and query its value. Using ? on its own will query whichever register was most recently selected. 
//Sn=r Select register n as the current register, and store r in it. Using =r on its own will store into whichever register was most recently selected. 
//V0 or V Verbose Numeric result codes 
//V1  English result codes (e.g. CONNECT, BUSY, NO CARRIER etc.) 
//X0 or X Smartmodem Hayes Smartmodem 300 compatible result codes 
//X1  Usually adds connection speed to basic result codes (e.g. CONNECT 1200) 
//X2  Usually adds dial tone detection (preventing blind dial, and sometimes preventing ATO) 
//X3  Usually adds busy signal detection. 
//X4  Usually adds both busy signal and dial tone detection 
//Z0 or Z Reset Reset modem to stored configuration. Use Z0, Z1etc. for multiple profiles. This is the same as &F for factory default on modems without NVRAM (non volatile memory) 
#endregion

#region File Results: File System Info from MULTIPLE sources (Coolest thing since sliced bread!!!)
//System.Collections.ArrayList alFolders = Avoli.SystemInfo.Directories.Enum(@"c:\windows\system", false);
//System.Collections.Generic.List<System.IO.FileInfo> fi = Avoli.IO.Files.EnumToFileInfo(Avoli.Arrays.ArrayListToStringArray(alFolders), "*.*", false, false);
//dataGrid.DataSource = fi;
//dataGrid.ReadOnly = true;
//dataGrid.Columns["Extension"].Width = 200;
//dataGrid.Columns["Name"].Width = 300;
//dataGrid.Columns["Directory"].Width = 300;
//dataGrid.Columns["CreationTime"].Width = 200;
//dataGrid.Columns["Exists"].Visible = false;
//dataGrid.Columns["Attributes"].Visible = false;
//dataGrid.Columns["FullName"].Visible = false;
//dataGrid.Columns["DirectoryName"].Visible = false;
//dataGrid.Columns["IsReadOnly"].Visible = false;
//dataGrid.Columns["CreationTimeUtc"].Visible = false;
//dataGrid.Columns["LastAccessTime"].Visible = false;
//dataGrid.Columns["LastAccessTimeUtc"].Visible = false;
//dataGrid.Columns["LastWriteTime"].Visible = false;
//dataGrid.Columns["LastWriteTimeUtc"].Visible = false;
//try { dataGrid.Rows[0].Cells[0].Selected = false; }
//catch { }

//System.Collections.Generic.List<System.IO.FileInfo> tests = new System.Collections.Generic.List<System.IO.FileInfo>();
#endregion

#region Usings / Includes
// Eliminate using's where possible Strong Type/Cast everything to make it easier on those who wish to grab code snippets.
using System;
using System.ComponentModel;			// Used in TypeConverter
using System.Windows.Forms;				// WinForm constructor
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;	// Used for DLL's & Marshal, Charset, Out, COMException, references
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;                  // Add reference under .Net tab
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
//using TwainLib;                       // See: TwainLib.cs / TwainDefs.cs
using WIALib;                           // Microsft Image Acquisition reference
using WinSCP;
//using Edidev.FrameworkEDI;
#endregion

namespace Avoli
{
    /// <summary>
    /// Avoli.Framework / Avoli.Foundation Classes
    /// </summary>

    #region APIs - Printing
    public struct DOCINFO     // Used in the PRINTING class
    {
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
        public string pDocName;
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
        public string pOutputFile;
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
        public string pDataType;
    }
    #endregion

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
    public class BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }

    public class Init
    {
        [STAThread]
        static void Main(string[] args)
        {
            Avoli.SmartClient objSmartClient = new Avoli.SmartClient();
            Avoli.MessageBox.Show(objSmartClient.FrameworkVersion, Avoli.DevTools.GetCurrentMethodName(), 0);
            Avoli.DevTools.End();
        }
    }

    // COMMAND LINES STUFF
    public static class CommandLineFlag
    {
        public static string Flag { get; set; }
        public static string Argument { get; set; }
    }

    public class CommandLine
    {
        #region Example1
        // Ex: arg1 "c:\program files\filename.txt" /a -b:arg2 /c d:arg3/3:"arg 4"
        //Avoli.CommandLine cmd = new Avoli.CommandLine(txtCommandLine.Text);
        //lvwResults.Items.Clear();

        //Avoli.CommandLine cmd = new Avoli.CommandLine("");
        //string strResult = "";
        //foreach (string arg in cmd.Arguments)
        //{
        //    if (strResult == "")
        //        strResult = "Argument: " + arg;
        //    else
        //        strResult = strResult + @"\r\nArgument: " + arg;
        //}
        //Avoli.MessageBox.Show(strResult, "Command Line Arguments", 8);


        //// Add arguments to list
        //foreach (string arg in cmd.Arguments)
        //{
        //    ListViewItem item = new ListViewItem();
        //    item.Text = "Argument";
        //    item.SubItems.Add(arg);
        //    lvwResults.Items.Add(item);
        //}

        // Fetching flags is 99% complete.  For some reason it was getting an error we'll fix later
        ////// Add flags to list
        ////foreach (Avoli.CommandLineFlag flag in cmd.Flags)
        ////{
        ////    ListViewItem item = new ListViewItem();
        ////    item.Text = "Flag";
        ////    item.SubItems.Add(flag.Flag);
        ////    if (flag.Argument != null)
        ////        item.SubItems.Add(flag.Argument);
        ////    lvwResults.Items.Add(item);
        ////}        
        #endregion

        // List of command-line arguments
        public List<string> Arguments { get; set; }

        // List of command-line flags
        //public List<Avoli.CommandLineFlag> Flags { get; set; }

        // Parsing state variables
        string _cmd;
        int _pos;

        public CommandLine(string commandLine)
        {
            if (commandLine == "")
                commandLine = System.Environment.CommandLine;

            // Initialize parser for given command line
            BeginParsing(commandLine);

            // Loop until all characters processed
            while (!EndOfText)
            {
                // Skip whitespace
                while (Char.IsWhiteSpace(Peek()))
                    MoveAhead();

                // Determine if this is a flag
                if (Peek() == '/' || Peek() == '-')
                {
                    // Parse flag
                    MoveAhead();
                    int start = _pos;
                    while (Char.IsLetterOrDigit(Peek()))
                        MoveAhead();
                    if (_pos > start)
                    {
                        //CommandLineFlag flag = new CommandLineFlag();
                        //flag.Flag = _cmd.Substring(start, _pos - start);
                        if (Peek() == ':')
                        {
                            // Parse flag argument
                            MoveAhead();
                            //flag.Argument = ParseArgument();
                        }
                        //Flags.Add(flag);
                    }
                }
                else
                {
                    // Parse command-line argument
                    string arg = ParseArgument();
                    if (arg.Length > 0)
                        Arguments.Add(arg);
                }
            }
        }

        protected void BeginParsing(string commandLine)
        {
            Arguments = new List<string>();
            //Flags = new List<CommandLineFlag>();
            _cmd = commandLine;
            _pos = 0;
        }

        protected bool EndOfText
        {
            get { return (_pos >= _cmd.Length); }
        }

        protected char Peek()
        {
            return (_pos < _cmd.Length) ? _cmd[_pos] : (char)0;
        }

        protected char MoveAhead()
        {
            char c = Peek();
            _pos = Math.Min(_pos + 1, _cmd.Length);
            return c;
        }

        protected string ParseArgument()
        {
            string result;

            if (Peek() == '"' || Peek() == '\'')
            {
                // Parse quoted argument
                char quote = MoveAhead();
                int start = _pos;
                while (!EndOfText && Peek() != quote)
                    MoveAhead();
                result = _cmd.Substring(start, _pos - start);
                MoveAhead();    // Eat closing quote
            }
            else
            {
                // Parse argument
                int start = _pos;
                while (!EndOfText && !Char.IsWhiteSpace(Peek()) &&
                    Peek() != '/' && Peek() != '-')
                    MoveAhead();
                result = _cmd.Substring(start, _pos - start);
            }
            // Return the parsed argument
            return result;
        }
    }

    public class Solution
    {
        public static string GetAssemblyAttribute<T>(Func<T, string> value)
        where T : Attribute
        {
            T attribute = (T)Attribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }
    }

    /// <summary>
    /// Class for Application specific methods
    /// </summary>
    public class Application
    {
        // Generic Delegates
        public static void SetLabelText(string text, System.Windows.Forms.Label lbl)
        {
            if (lbl.InvokeRequired)
                lbl.BeginInvoke(new MethodInvoker(delegate () { SetLabelText(text, lbl); }));
            else
                lbl.Text = text;
        }

        public static void SetProgressBar(int val, int max, System.Windows.Forms.ProgressBar pb)
        {
            if (pb.InvokeRequired)
                pb.BeginInvoke(new MethodInvoker(delegate () { SetProgressBar(val, max, pb); }));
            else
            {
                pb.Maximum = max;
                pb.Value = val;
                if (val > 0)
                {   // NOTE: Depending on the type of file and it's size these Numbers should be fine.  (EDI files are large)
                    if (max > 0 && max <= 20)
                        pb.ForeColor = System.Drawing.Color.MediumBlue;
                    else if (max > 21 && max <= 50)
                        pb.ForeColor = System.Drawing.Color.LimeGreen;
                    else if (max > 51 && max <= 100)
                        pb.ForeColor = System.Drawing.Color.YellowGreen;
                    else if (max > 101 && max <= 150)
                        pb.ForeColor = System.Drawing.Color.Orange;
                    else if (max > 151)
                        pb.ForeColor = System.Drawing.Color.Red;
                    pb.Visible = true;
                }
                if (val == 0 || val >= max)
                {
                    pb.Minimum = 0;
                    pb.Visible = false;
                }
            }
        }

        public static string GetTitle()
        {
            return Avoli.Application.GetName().Replace(".exe", "") + " ( " + Avoli.Security.User.Info.GetID() + " @ " + System.Net.Dns.GetHostName() + " ) Version " + Avoli.Application.GetVersion();
        }

        public static string GetName()
        {	// Tested: 8/30/2006 (Example Return: WindowsApplication1)

            return System.AppDomain.CurrentDomain.FriendlyName.Replace(".vshost.exe", ""); // EXE Name
            // return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();  // Could pull DLL name
        }

        public static string GetAppPath()
        {	// Tested: 8/30/2006
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "");
            // return System.Windows.Forms.Application.StartupPath.ToString();
        }

        public static string GetAssemblyInfo()
        {	// Tested: 8/30/2006
            return System.Reflection.Assembly.GetExecutingAssembly().FullName;
        }

        public static void ToSystemTray(System.Windows.Forms.Form frm, System.Windows.Forms.NotifyIcon ni)
        {
            // private void Form1_Load(object sender, System.EventArgs e)
            // {   
            //     // this.Load += new System.EventHandler(this.frmEmail_Load);
            //
            //     Avoli.Application.ToSystemTray(this);
            // }
            if (!frm.IsMdiChild)
            {
                frm.Hide();
                frm.ShowInTaskbar = false;
                ni.Visible = true;
            }
        }

        public static void FromSystemTray(System.Windows.Forms.Form frm, System.Windows.Forms.NotifyIcon ni)
        {
            // private void notifyIcon1_Click(object sender, System.EventArgs e)
            // {
            //     // REQUIRED: Add a NotifyIcon to the form (notifyIcon1)
            //  
            // this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            //     // this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDown);
            //
            //     Avoli.Application.FromSystemTray(this);
            // }

            frm.Show();
            frm.ShowInTaskbar = true;
            ni.Visible = false;
        }

        public static Avoli.SmartClient InitializeSmartClient(Avoli.SmartClient objSmartClient, bool blnConnectToDatabase)
        {
            string strStopWatch = "";

            objSmartClient.ConfigFilePath = Avoli.SystemInfo.General.GetAliasValue("CONFIGFILE");	// string strConfigFile = System.Windows.Forms.Application.StartupPath.ToLower() + @"\config.xml";
            string strAppName = "";

            #region Overriding Command Line Settings
            objSmartClient.Debug = false;
            Avoli.SuperString ss = new Avoli.SuperString();
            try
            {
                ss = System.Environment.CommandLine;
                if (ss.ToString() != "")
                {   // Set smart client command line hash and forget about it
                    objSmartClient.CommandLineHash = ss.CommandLineGetToHash();

                    // Enum the list to see what was returned
                    //System.Collections.Hashtable ht = ss.CommandLineGetToHash();
                    //foreach (System.Collections.DictionaryEntry d in ht)
                    //    Avoli.MessageBox.Show(d.Key + " = " + d.Value, "Total Values Count: " + ht.Count, 0);

                    // Init certain command line values
                    string strDebug = "";
                    try
                    {
                        strDebug = ss.CommandLineLocateInHash("debug", objSmartClient);
                        if (strDebug.Length > 0)
                            objSmartClient.Debug = Convert.ToBoolean(strDebug);
                        else
                            objSmartClient.Debug = false;
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nProblem with Overriding command line settings.\r\n\r\n" + ex.Message + "\r\n\r\n" + objSmartClient.ConnectionString, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }

            if (objSmartClient.Debug == true)
                Avoli.MessageBox.Show(ss.ToString(), "DEBUG: Command Line Value", 8);
            #endregion

            #region Application Settings
            try
            {
                if (Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "changemanagement", "appenabled", "false").ToUpper() != "TRUE")
                {
                    Avoli.Multimedia.ToneGenerator.Caution(false, 8);
                    Avoli.MessageBox.Show("This application has been temporarily disabled by your Technology Department to apply a maintenance update.", "ATTENTION!!!", 10);
                    Avoli.DevTools.End();
                }

                objSmartClient.ComputerName = ss.EnvironmentGetVariable("COMPUTERNAME").ToString();
                objSmartClient.ApplicationTitle = Avoli.Solution.GetAssemblyAttribute<System.Reflection.AssemblyTitleAttribute>(a => a.Title);
                objSmartClient.ApplicationCopyright = Avoli.Solution.GetAssemblyAttribute<System.Reflection.AssemblyCopyrightAttribute>(a => a.Copyright);
                objSmartClient.ApplicationVersion = Avoli.Application.GetVersion(); //Avoli.Solution.GetAssemblyAttribute<System.Reflection.AssemblyVersionAttribute>(a => a.Version);
                objSmartClient.ApplicationDescription = Avoli.Solution.GetAssemblyAttribute<System.Reflection.AssemblyDescriptionAttribute>(a => a.Description);
                objSmartClient.ApplicationAuthor = Avoli.Solution.GetAssemblyAttribute<System.Reflection.AssemblyCompanyAttribute>(a => a.Company);
                objSmartClient.ApplicationAuthorWebSite = "www.Avoli.com/corporate";
                if (objSmartClient.ApplicationName == "" || objSmartClient.ApplicationName == String.Empty || objSmartClient.ApplicationName == null)
                {
                    if (strAppName == "")
                        objSmartClient.ApplicationName = System.Windows.Forms.Application.ProductName;
                    // strAppName = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "changemanagement", "appname", "");
                    objSmartClient.ApplicationName = strAppName;
                }

                objSmartClient.PaintKitApplicatorsEnabled = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "applicator", "Enabled", "false"));
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for APP AUTHOR settings. \r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region GUI Settings
            try
            {
                objSmartClient.Debug = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "gui", "Debug", "false"));
                objSmartClient.ShowInTaskBar = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "gui", "ShowInTaskBar", "false"));
                objSmartClient.ShowInTray = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "gui", "ShowInTray", "false"));
                objSmartClient.MixedCase = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "gui", "MixedCase", "true"));
                objSmartClient.ComboMaxDropDownItems = Convert.ToInt16(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "gui", "ComboMaxDropDownItems", "8"));
                objSmartClient.TimesToConfirmDeletions = Convert.ToInt16(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "gui", "TimesToConfirmDeletions", "3"));
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for GUI settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region Registry Settings
            try
            {
                if (objSmartClient.RegistryRootPath == "" || objSmartClient.RegistryRootPath == String.Empty || objSmartClient.RegistryRootPath == null)
                    objSmartClient.RegistryRootPath = "Software\\" + objSmartClient.ApplicationAuthor + "\\" + objSmartClient.ApplicationName + "\\Settings".ToString().ToLower();
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for REGISTRY settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region Live Update Settings
            try
            {
                objSmartClient.LiveUpdateEnabled = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "changemanagement", "liveupdateenabled", "false"));
                objSmartClient.LiveUpdateLocation = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "changemanagement", "liveupdatelocation", "");
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for LIVE UPDATE settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region Audit Settings (Database Specific)
            try
            {
                objSmartClient.DeletesIncludeAuditTables = false;

                // Machine Settings
                objSmartClient.MachineName = System.Net.Dns.GetHostName();
                objSmartClient.MachineIPAddress = Avoli.Net.MachineInfo.GetInternalIPAddress();
                objSmartClient.MachineOwner = Avoli.DataStorage.LocalSystem.Registry.QueryValue(Avoli.DataStorage.LocalSystem.Registry.ERegistryPossibleRoots.HKEY_LOCALE_MACHINE, @"Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOwner", "").ToString();
                objSmartClient.MachineOrganization = Avoli.DataStorage.LocalSystem.Registry.QueryValue(Avoli.DataStorage.LocalSystem.Registry.ERegistryPossibleRoots.HKEY_LOCALE_MACHINE, @"Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOrganization", "").ToString();
                if (objSmartClient.MachineOrganization == "")
                    objSmartClient.MachineOrganization = "Not specified";
                objSmartClient.MachineOrganization = objSmartClient.MachineOrganization.Replace(",", "");
                objSmartClient.MachineOrganization = objSmartClient.MachineOrganization.Replace(".", "");
                objSmartClient.MachinePlatform = System.Environment.OSVersion.Platform.ToString();
                objSmartClient.MachineOSVersion = System.Environment.OSVersion.Version.ToString();
                objSmartClient.MachineOpSys = Avoli.SystemInfo.OpSys.Version();
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for AUDIT settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region PAINT KIT Settings
            objSmartClient.PaintKitApplicatorForm = "default";
            objSmartClient.PaintKitApplicatorFoundation = "default";
            if (objSmartClient.PaintKitApplicatorsEnabled)
            {
                try
                {
                    objSmartClient.PaintKitGradientFill = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "applicator", "GradientFill", "false"));
                    objSmartClient.PaintKitColorGridHeaders = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "applicator", "ColorGridHeaders", "true"));
                    objSmartClient.PaintKitColorTabPages = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "applicator", "ColorTabPages", "true"));
                    objSmartClient.PaintKitStyle = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "applicator", "Style", "Flat");
                }
                catch (Exception ex)
                {
                    Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for PAINT KIT settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                    Avoli.DevTools.End();
                }
            }
            #endregion

            #region THEME Settings
            if (objSmartClient.PaintKitApplicatorsEnabled)
            {
                try
                {
                    objSmartClient.ThemesEnabled = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "themes", "Enabled", "false"));
                    objSmartClient.ThemesPath = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "themes", "Path", "null");

                    // PAINT KIT Applicators
                    objSmartClient.PaintKitApplicatorForm = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "applicator", "form", "");
                    objSmartClient.PaintKitApplicatorFormMDIChild = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "applicator", "formmdichild", "");
                    objSmartClient.PaintKitApplicatorFoundation = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "applicator", "foundation", "");
                    objSmartClient.PaintKitDataEntry = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "applicator", "dataentry", "");
                    if (objSmartClient.PaintKitApplicatorFormMDIChild == "" || objSmartClient.PaintKitApplicatorFormMDIChild == String.Empty || objSmartClient.PaintKitApplicatorFormMDIChild == null)
                        objSmartClient.PaintKitApplicatorFormMDIChild = objSmartClient.PaintKitApplicatorForm;

                    // PAINT KIT Base Applicator COLOR CANVAS's
                    objSmartClient.PaintKitForm = Avoli.Controls.Apply.PaintKit.GetSpecificCanvas(objSmartClient, "Form", false);
                    objSmartClient.PaintKitFoundation = Avoli.Controls.Apply.PaintKit.GetSpecificCanvas(objSmartClient, "Foundation", false);
                    if (objSmartClient.PaintKitApplicatorFormMDIChild == objSmartClient.PaintKitApplicatorForm)
                        objSmartClient.PaintKitFormMDIChild = objSmartClient.PaintKitForm;
                    else
                        objSmartClient.PaintKitFormMDIChild = Avoli.Controls.Apply.PaintKit.GetSpecificCanvas(objSmartClient, "FormChild", false);
                }
                catch (Exception ex)
                {
                    Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for PAINT KIT THEME settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                    Avoli.DevTools.End();
                }
            }
            #endregion

            #region EMAIL Settings
            try
            {
                objSmartClient.EmailSMTPDropZoneEnabled = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "email", "smtpdropzoneenabled", "false"));
                objSmartClient.EmailSMTPDropZonePath = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "email", "smtpdropzonepath", "");
                objSmartClient.EmailSMTPServers = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "email", "smtpservers", "");
                objSmartClient.EmailSMTPAnonFromAddress = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "email", "smtpanonfromaddress", "");
                objSmartClient.EmailSMTPUserID = Avoli.Security.Crypto.Base64Modified.Decode(out strStopWatch, Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "email", "smtpuserid", ""));
                if (objSmartClient.EmailSMTPUserID != "")
                    objSmartClient.EmailSMTPPassword = Avoli.Security.Crypto.Base64Modified.Decode(out strStopWatch, Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "email", "smtppassword", ""));
                else
                    objSmartClient.EmailSMTPPassword = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "email", "smtppassword", "");
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for EMAIL settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region Authentication
            objSmartClient.AuthenticationByLDAP = System.Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "authentication", "byldap", "false"));
            objSmartClient.AuthenticationByWebService = System.Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "authentication", "byconsumingwebservice", "false"));
            #endregion

            #region LDAP Settings
            try
            {
                objSmartClient.LDAPServerPath = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "authentication", "ldapserverpath", "");    // corpldapdev.csm.fub.com
                objSmartClient.LDAPDomain = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "authentication", "ldapdomain", "ou=People,ou=Internal,dc=??,dc=com"); //"ou=People,ou=Internal,dc=walkovaya,dc=com"));
                objSmartClient.LDAPPort = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "authentication", "ldapport", "false");
                objSmartClient.LDAPWebService = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "authentication", "webservice", "");
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for LDAP settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region EXCEPTIONS Settings
            try
            {
                objSmartClient.LogRollupMultipleErrors = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "exceptions", "logrollupmultipleerrors", "true"));
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for EXCEPTION settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region MDI Settings
            try
            {
                objSmartClient.MDISaveWindowLocations = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mdi", "MDISaveWindowLocations", "false"));
                objSmartClient.MDIForceMaximizedChildren = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mdi", "MDIForceMaximizedChildren", "false"));
                objSmartClient.MDIAllowMultipleChildren = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mdi", "MDIAllowMultipleChildren", "true"));
                objSmartClient.MDIAllowChildMovement = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mdi", "MDIAllowChildMovement", "true"));
                objSmartClient.MDIAllowChildMinimize = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mdi", "MDIAllowChildMinimize", "true"));
                if (objSmartClient.MDIForceMaximizedChildren)
                    objSmartClient.MDIAllowMultipleChildren = false;
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for MDI settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region Required Apps
            try
            {
                objSmartClient.ApplicationRequiredApps = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "requiredapps", "list", "");
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for REQUIRED APPS settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region DATABASE Information
            try
            {
                // If the config.xml says we're to run disconnected the do not load db info
                if (Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "rundisconnected", "false").ToLower() == "true"))
                    objSmartClient.Debug = false;
                if (objSmartClient.Debug == true)
                    Avoli.MessageBox.Show("Config.xml - <Database> RunDisconnected: " + blnConnectToDatabase.ToString(), "DEBUG: " + Avoli.DevTools.GetCurrentMethodName(), 8);
                if (blnConnectToDatabase)
                {
                    objSmartClient.DatabaseEnvironment = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "mode", "prod");
                    objSmartClient.DatabaseName = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "databasename", "");
                    objSmartClient.DBProvider = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "provider", "SQLOLEDB");
                    objSmartClient.DBDevServer = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "devserver", "");
                    objSmartClient.DBTestServer = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "testserver", "");
                    objSmartClient.DBProdServer = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "prodserver", "");
                    objSmartClient.DBDRServer = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "drserver", "");
                    objSmartClient.DBTimeout = Convert.ToInt16(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "timeout", "30"));
                    objSmartClient.DBPooling = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "pooling", ""));
                    objSmartClient.DBIntegratedSecurity = Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "integrated", ""));
                }
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for DATABASE settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            if (blnConnectToDatabase)
            {

                #region Auto encrypt base64 on LoginID & Password (The next run the values will come in base64 encoded)
                try
                {
                    objSmartClient.DBLoginID = Avoli.Security.Crypto.Base64Modified.Decode(out strStopWatch, Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "loginid", objSmartClient.DatabaseName));
                    if (objSmartClient.DBLoginID != "")
                        objSmartClient.DBPassword = Avoli.Security.Crypto.Base64Modified.Decode(out strStopWatch, Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "password", "password"));
                    else
                        objSmartClient.DBPassword = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "database", "password", "password");
                    switch (objSmartClient.DatabaseEnvironment.ToLower())
                    {
                        case "dev":
                            objSmartClient.DBActiveServer = objSmartClient.DBDevServer;
                            break;
                        case "test":
                            objSmartClient.DBActiveServer = objSmartClient.DBTestServer;
                            break;
                        case "prod":
                            objSmartClient.DBActiveServer = objSmartClient.DBProdServer;
                            break;
                        case "dr":
                            objSmartClient.DBActiveServer = objSmartClient.DBDRServer;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Avoli.MessageBox.Show(ex.Message + "\r\n\r\nThere are possible inconsistencies in config.xml for DATABASE LOGIN/ENV settings.\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                    Avoli.DevTools.End();
                }
                #endregion

                #region Build our OLE provider connection string
                try
                {
                    objSmartClient.ConnectionStringOLE = "Provider=" + objSmartClient.DBProvider + ";Data Source=" + objSmartClient.DBActiveServer;
                    if (objSmartClient.DatabaseName != "") objSmartClient.ConnectionStringOLE = objSmartClient.ConnectionStringOLE + ";Initial Catalog=" + objSmartClient.DatabaseName;
                    if (objSmartClient.DBLoginID != "")
                        objSmartClient.ConnectionStringOLE = objSmartClient.ConnectionStringOLE + ";User ID=" + objSmartClient.DBLoginID;
                    if (objSmartClient.DBPassword != "" && objSmartClient.DBLoginID != "")
                        objSmartClient.ConnectionStringOLE = objSmartClient.ConnectionStringOLE + ";Password=" + objSmartClient.DBPassword;
                    if (objSmartClient.DBTimeout > 0) objSmartClient.ConnectionStringOLE = objSmartClient.ConnectionStringOLE + ";Connection Timeout=" + objSmartClient.DBTimeout;
                }
                catch (Exception ex)
                {
                    Avoli.MessageBox.Show(ex.Message + "\r\n\r\nProblem building OLE provider connection string.\r\n\r\n" + ex.Message + "\r\n\r\n" + objSmartClient.ConnectionString, Avoli.DevTools.GetCurrentMethodName(), 0);
                    Avoli.DevTools.End();
                }
                #endregion

                #region Build our SQL provider connection string
                try
                {
                    objSmartClient.ConnectionString = "Data source=" + objSmartClient.DBActiveServer + "; persist security info=False";
                    if (objSmartClient.DatabaseName != "") objSmartClient.ConnectionString = objSmartClient.ConnectionString + "; initial catalog=" + objSmartClient.DatabaseName;
                    if (objSmartClient.DBLoginID != "")
                        objSmartClient.ConnectionString = objSmartClient.ConnectionString + "; user id=" + objSmartClient.DBLoginID;
                    if (objSmartClient.DBPassword != "" & objSmartClient.DBLoginID != "")
                        objSmartClient.ConnectionString = objSmartClient.ConnectionString + "; password=" + objSmartClient.DBPassword;
                    if (objSmartClient.DBTimeout > 0) objSmartClient.ConnectionString = objSmartClient.ConnectionString + "; Connection Timeout=" + objSmartClient.DBTimeout;
                }
                catch (Exception ex)
                {
                    Avoli.MessageBox.Show(ex.Message + "\r\n\r\nProblem building SQL provider connection string.\r\n\r\n" + ex.Message + "\r\n\r\n" + objSmartClient.ConnectionString, Avoli.DevTools.GetCurrentMethodName(), 0);
                    Avoli.DevTools.End();
                }
                #endregion

                #region Attach connection pooling settings here (If told to... See config.xml)
                try
                {
                    if (objSmartClient.DBPooling)
                    {	// Tack on pooling information onto our existing connectionstring
                        objSmartClient.ConnectionString = objSmartClient.ConnectionString +
                            "; connection reset=false" +
                            "; connection lifetime=5" +
                            "; min pool size=1" +
                            "; max pool size=50";
                    }

                    if (objSmartClient.DBIntegratedSecurity)
                        objSmartClient.ConnectionString = "Server=" + objSmartClient.DBActiveServer + ";Database=" + objSmartClient.DatabaseName + ";Trusted_Connection=Yes;";

                    if (blnConnectToDatabase)
                    {
                        objSmartClient = Avoli.DataStorage.Database.General.Connect(objSmartClient);
                        if (objSmartClient.cnn == null)
                        {
                            try
                            {
                                Avoli.DevTools.End();
                            }
                            catch (Exception ex)
                            {
                                Avoli.Exceptions.Publish(objSmartClient, ex, Avoli.Exceptions.Severity.Error, Avoli.DevTools.GetCurrentMethodName(), true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Avoli.MessageBox.Show(ex.Message + "\r\n\r\nCould NOT connect to the database with config.xml settings.\r\n\r\n" + ex.Message + "\r\n\r\n" + objSmartClient.ConnectionString, Avoli.DevTools.GetCurrentMethodName(), 0);
                    Avoli.DevTools.End();
                }
                #endregion

            }

            #region Scan Settings
            try
            {
                objSmartClient.ScanProtocol = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "scan", "protocol", "TWAIN");
                if (objSmartClient.ScanProtocol != "TWAIN" && objSmartClient.ScanProtocol != "ISIS" && objSmartClient.ScanProtocol != "WIA")
                    objSmartClient.ScanProtocol = "TWAIN";
                objSmartClient.ScanDropZoneFolder = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "scan", "dropzone", @"c:\scans");
                objSmartClient.ScanDefaultScanner = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "scan", "defaultscanner", "");
                objSmartClient.ScanDefaultTemplate = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "scan", "defaulttemplate", "");
                objSmartClient.ScanPrinterName = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "scan", "printername", "true");
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nProblem fetching Scan settings from config.xml.\r\n\r\n" + ex.Message + "\r\n\r\n" + objSmartClient.ConnectionString, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            #region Mine Settings
            try
            {
                objSmartClient.RemoteLocationName = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "remotelocationname", "Corporate");
                objSmartClient.MineInterval = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "interval", "60000");
                objSmartClient.MineDropZoneFolder = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "dropzone", @"c:\dropzone");
                objSmartClient.MineDestinationFolder = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "destinationfolder", "");
                objSmartClient.MineFTPServer = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "ftpserver", "");
                objSmartClient.MineFTPCert = Avoli.Security.Crypto.Base64Modified.Decode(out strStopWatch, Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "ftpcert", ""));
                objSmartClient.MineFTPUserName = Avoli.Security.Crypto.Base64Modified.Decode(out strStopWatch, Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "ftpuser", ""));
                if (objSmartClient.MineFTPUserName != "")
                    objSmartClient.MineFTPPassword = Avoli.Security.Crypto.Base64Modified.Decode(out strStopWatch, Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "ftppassword", "password"));
                else
                    objSmartClient.MineFTPPassword = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "ftppassword", "");
                objSmartClient.MineWildcard = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "wildcard", "*.*");
                objSmartClient.MineChangeToFormat = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "changetoformat", "jpeg");
                objSmartClient.MineRecursive = true; //  Convert.ToBoolean(Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "recursive", "false"));
                objSmartClient.MineEmailTo = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "emailto", "true");
                objSmartClient.MineEmailCC = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "emailcc", "true");
                objSmartClient.MineEmailBCC = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "mine", "emailbcc", "true");
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message + "\r\n\r\nProblem fetching Mine settings from config.xml.\r\n\r\n" + ex.Message + "\r\n\r\n" + objSmartClient.ConnectionString, Avoli.DevTools.GetCurrentMethodName(), 0);
                Avoli.DevTools.End();
            }
            #endregion

            return objSmartClient;
        }

        public static void InitialSetup(Avoli.SmartClient objSmartClient, string strDevelopedBy, string strAppName, string strAppPath)
        {	// CALLED BY: frmSplash.Reference_Load

            #region Examples
            // Avoli.Application.InitialSetup("Avoli., System.Windows.Forms.Application.ProductName, System.Windows.Forms.Application.StartupPath.ToString());
            // Avoli.MessageBox.Show(Avoli.IO.Files.GetExecutablePath("Avoli., System.Windows.Forms.Application.ProductName.ToString()));
            #endregion

            #region Get Correct version (Accounts for LiveUpdates)
            string strOurVersion = Avoli.Application.GetVersion();
            string[] saVersionBreakdown = strOurVersion.Split(new char[] { '.' });
            objSmartClient.ApplicationVersionMajor = Convert.ToInt16(saVersionBreakdown[0]);
            objSmartClient.ApplicationVersionMinor = Convert.ToInt16(saVersionBreakdown[1]);
            #endregion

            #region Store App Path to the registry
            //		Why???	This lets the registry know where our app's location is just in case other apps want to see if it's installed.
            //				This is needed if you intend on using the Avoli.IO.Files.GetExecutablePath method on this app.
            if (objSmartClient.ApplicationAuthor == "" || objSmartClient.ApplicationAuthor == String.Empty || objSmartClient.ApplicationAuthor == null)
                objSmartClient.ApplicationAuthor = strDevelopedBy;
            if (objSmartClient.ApplicationName == "" || objSmartClient.ApplicationName == String.Empty || objSmartClient.ApplicationName == null)
                objSmartClient.ApplicationName = strAppName;
            if (objSmartClient.RegistryRootPath == "" || objSmartClient.RegistryRootPath == String.Empty || objSmartClient.RegistryRootPath == null)
                objSmartClient.RegistryRootPath = "Software\\" + objSmartClient.ApplicationAuthor + "\\" + objSmartClient.ApplicationName + "\\Settings";
            // COMMENTED OUT DUE TO PERMISSION CONTRAINTS CRAP
            //Avoli.DataStorage.LocalSystem.Registry.SetValue(Avoli.DataStorage.LocalSystem.Registry.ERegistryPossibleRoots.HKEY_LOCALE_MACHINE, objSmartClient.RegistryRootPath, "Path", strAppPath);
            #endregion

            #region Live Update Check (If NEW version THEN launch LiveUpdate.exe)
            string strLastRanOn = Avoli.DataStorage.File.INI.Read(System.Windows.Forms.Application.StartupPath + @"\liveupdateinfo.ini", "DEFAULTS", "LastCheckedOn");
            // REPLACED WITH INI METHOD: string strLastRanOn = Avoli.DataStorage.LocalSystem.Registry.QueryValue(Avoli.DataStorage.LocalSystem.Registry.ERegistryPossibleRoots.HKEY_LOCALE_MACHINE, objSmartClient.RegistryRootPath, "LiveUpdateLastCheck", "");
            if (strLastRanOn != System.DateTime.Now.ToShortDateString()) // Also could do a WEEKDAY check here
            {	// Check for a newer version before we call LiveUpdate
                Avoli.DataStorage.File.INI.Write(System.Windows.Forms.Application.StartupPath + @"\liveupdateinfo.ini", "DEFAULTS", "LastCheckedOn", System.DateTime.Now.ToShortDateString());
                // REPLACED WITH INI METHOD: Avoli.DataStorage.LocalSystem.Registry.SetValue(Avoli.DataStorage.LocalSystem.Registry.ERegistryPossibleRoots.HKEY_LOCALE_MACHINE, objSmartClient.RegistryRootPath, "LiveUpdateLastCheck", System.DateTime.Now.ToShortDateString());
                if (Avoli.Application.LiveUpdateCheck(objSmartClient, strDevelopedBy))
                {	// Perform the Live Update (Launch LiveUpdate.exe)
                    if (Avoli.Confirmation.Show(objSmartClient, "A newer version was found.  Do you wish to continue with the Live Update?", "Live Update Check for: " + objSmartClient.ApplicationName.ToString(), 0))
                    {
                        // Avoli.MessageBox.Show(System.Windows.Forms.Application.StartupPath + @"\liveupdate.exe", "", 0);
                        System.Windows.Forms.Application.DoEvents();
                        string strShellResult = Avoli.SystemInfo.Processes.Shell(Avoli.IO.Files.GetShortPath(System.Windows.Forms.Application.StartupPath + @"\liveupdate.exe"), "", false, false);
                        Avoli.DevTools.End();
                    }
                }
            }
            #endregion
        }

        public static string GetVersion()
        {	// All we care about is MAJOR & MINOR which is all that's used on the LiveUpdate method
            string strAppStoredVersion = "";
            string strLiveUpdateVersionFile = System.Windows.Forms.Application.StartupPath + @"\sdsivf.dll";
            if (!System.IO.File.Exists(strLiveUpdateVersionFile))
            {	// Utilize DEFAULT versioning
                strAppStoredVersion = System.Windows.Forms.Application.ProductVersion.ToString();
                string[] saVersionBreakdown = strAppStoredVersion.Split(new char[] { '.' });
                strAppStoredVersion = saVersionBreakdown[0] + "." + saVersionBreakdown[1];	// Grab only major & minor info
                bool blnOkay = Avoli.IO.Export.FromString(strLiveUpdateVersionFile, strAppStoredVersion, false, true);
            }
            else
            {	// LiveUpdate has been run before.  Use the version passed down from it.
                strAppStoredVersion = Avoli.IO.Import.ToString(strLiveUpdateVersionFile);
            }
            return strAppStoredVersion;
        }

        public static bool LiveUpdateCheck(Avoli.SmartClient objSmartClient, string strDevelopedBy)
        {
            #region Examples (Tested: August 8, 2006)
            //	Called by: Avoli.Application.InitialSetup
            #endregion

            #region HELP DESK: Why isn't LiveUpdate running when there is a NEWER version on the web?
            // 1) Make sure you have an INTERNET connection
            // 2) Look in the LiveUpdate.INI file to see when the last update was ran & what version it downloaded
            //    HINT: If the date is TODAYS date just remove the date and try LiveUpdate again
            #endregion

            string strAppName = Avoli.DataStorage.File.XML.Read(objSmartClient.ConfigFilePath, "changemanagement", "appname", "");
            objSmartClient.ConfigFilePath = Avoli.SystemInfo.General.GetAliasValue("CONFIGFILE");
            bool blnNewVersion = false;
            if (objSmartClient.LiveUpdateEnabled && objSmartClient.LiveUpdateLocation != "")
            {
                bool blnViaWeb = false;
                bool blnViaNetwork = false;
                bool blnWebConnected = Avoli.Web.IsConnectedToInternet();
                if (!blnWebConnected && Avoli.DevTools.InDev())
                {
                    Avoli.MessageBox.Show("Live Update will not run until your Internet connection is restored.", Avoli.DevTools.GetCurrentMethodName(), 0);
                    return false;
                }

                if (objSmartClient.LiveUpdateLocation.ToLower().IndexOf("www.") > 0 && blnWebConnected)
                {	// Test WEB Live Update Path & Internet connected
                    blnViaWeb = true;
                    blnViaNetwork = false;
                }
                if (objSmartClient.LiveUpdateLocation.ToLower().IndexOf("www.") <= 0)
                {	// Test NETWORK Live Update Path & Folder exists
                    if (Avoli.SystemInfo.Directories.Exists(objSmartClient.LiveUpdateLocation))
                    {
                        blnViaWeb = false;
                        blnViaNetwork = true;
                    }
                    else
                    {
                        Avoli.MessageBox.Show("ERROR - CONTACT SUPPORT: The LIVE UPDATE network path for this application does NOT exist:\r\n\r\n" + objSmartClient.LiveUpdateLocation, Avoli.DevTools.GetCurrentMethodName(), 0);
                        Avoli.DevTools.End();
                    }
                }

                if (!blnViaWeb && !blnViaNetwork && Avoli.DevTools.InDev())
                {
                    Avoli.MessageBox.Show("Live Update has NOT been ran becase the flags ViaWeb & ViaNetwork are both FALSE!", Avoli.DevTools.GetCurrentMethodName(), 0);
                    return false;
                }

                if (blnViaWeb || blnViaNetwork)
                {	// Is it time for a Live Update
                    string strTmp = "";
                    int intVersion = 0;

                    #region Start version search
                    string strFileToLaunch = strAppName + ".exe";
                    string strAppPath = System.Windows.Forms.Application.StartupPath;
                    string strLiveUpdateTraverseURL = "";
                    string strLiveUpdateLocation = "";

                    // Fetch version of the app
                    string strCurrentVersion = "";
                    string strLiveUpdateVersionFile = System.Windows.Forms.Application.StartupPath + @"\sdsivf.dll";
                    if (System.IO.File.Exists(strLiveUpdateVersionFile))
                    {
                        strCurrentVersion = Avoli.IO.Import.ToString(strLiveUpdateVersionFile);
                        string[] saVersionBreakdown = strCurrentVersion.Split(new char[] { '.' });
                        objSmartClient.ApplicationVersionMajor = Convert.ToInt16(saVersionBreakdown[0]);
                        objSmartClient.ApplicationVersionMinor = Convert.ToInt16(saVersionBreakdown[1]);
                    }

                    if (strFileToLaunch == "" || strAppName == "" || objSmartClient.LiveUpdateLocation == "" || objSmartClient.ApplicationVersionMajor == 0)
                    {
                        Avoli.MessageBox.Show("Not all information was specified...\r\n\r\nAPPNAME(config.xml)\r\nLIVEUPDATELOCATION(config.xml)\r\nOr not able to detect previous version", Avoli.DevTools.GetCurrentMethodName(), 0);
                        Avoli.MessageBox.Show("File to launch: " + strFileToLaunch, Avoli.DevTools.GetCurrentMethodName(), 0);
                        Avoli.MessageBox.Show("App to launch: " + strAppName, Avoli.DevTools.GetCurrentMethodName(), 0);
                        Avoli.MessageBox.Show("Live update location: " + objSmartClient.LiveUpdateLocation, Avoli.DevTools.GetCurrentMethodName(), 0);
                        Avoli.MessageBox.Show("Version: " + objSmartClient.ApplicationVersionMajor + "." + objSmartClient.ApplicationVersionMinor, Avoli.DevTools.GetCurrentMethodName(), 0);
                        Avoli.DevTools.End();
                    }
                    #endregion

                    #region Determine Live Update source & proceed with the update check
                    int intFilesToDownload = 0;
                    int intCurrentVersion = Convert.ToInt16(objSmartClient.ApplicationVersionMajor.ToString() + objSmartClient.ApplicationVersionMinor.ToString());
                    if (blnViaWeb)
                    {
                        #region Live Update via Web
                        string strHTTPSource = "";
                        string strHighestWebVersion = "0";
                        string[] saVersions = { };
                        string[] saFileHive = { };
                        string[] saFileHiveInfo = { };

                        // A) See if our product is on the web for downloadable updates - "http://www.avoli.com/tp.asp?path=products/updates/" + strAppName;
                        strLiveUpdateTraverseURL = objSmartClient.LiveUpdateLocation.Replace(".com/", ".com/tp.asp?path=") + "/" + strAppName;
                        strLiveUpdateLocation = objSmartClient.LiveUpdateLocation + "/" + strAppName;
                        strHTTPSource = Avoli.Web.GetHTTPPageSource(strLiveUpdateTraverseURL);
                        if (strHTTPSource == "" || strHTTPSource.ToLower().IndexOf("page not found") >= 0)
                        {	// B) Determine the latest version to pull down & start download if a latest version was detected to download.
                            strLiveUpdateTraverseURL = strLiveUpdateTraverseURL + "/versions";
                            strLiveUpdateLocation = strLiveUpdateLocation + "/versions";
                            // Avoli.MessageBox.Show("PATH SEARCHED: " + strLiveUpdateTraverseURL);
                            strHTTPSource = Avoli.Web.GetHTTPPageSource(strLiveUpdateTraverseURL).ToLower().Replace("\n", "").Replace("<br />", "\r");
                            saVersions = strHTTPSource.Split(new char[] { '\r' });
                            if (saVersions.GetUpperBound(0) == 0 && Avoli.DevTools.InDev())
                                Avoli.MessageBox.Show("ERROR: # OF VERSIONS FOUND: " + saVersions.GetUpperBound(0).ToString() + "\r\n\r\nSPLIT of " + strLiveUpdateTraverseURL + " results failed.\r\nHere is what was returned: " + strHTTPSource, "LiveUpdate Problem:", 0);
                            saVersions = Avoli.Arrays.BubbleSortStringArray(saVersions, true);
                            for (int i = 0; i < saVersions.Length; i++)
                            {	// The filenames on the web should appear as /products/updates/versions/1.2
                                if (saVersions[i] != "")
                                {
                                    try
                                    {
                                        intVersion = Convert.ToInt16(saVersions[i].Replace(".", ""));
                                        // Avoli.MessageBox.Show("VERSION FOUND: " + intVersion.ToString() + "  Compared with: " + intCurrentVersion.ToString());
                                        if (intVersion > intCurrentVersion)
                                        {
                                            strHighestWebVersion = saVersions[i];
                                            break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Avoli.MessageBox.Show("VERSION CHECK ERROR on: " + saVersions[i] + "\r\n\r\n" + ex.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
                                    }
                                }
                            }

                            // C) Did we find a higher version?
                            if (strHighestWebVersion != "0")
                            {	// YES! - Fetch all version filenames now (IF ANY) (... products/updates/boiler/versions/1.2)
                                strLiveUpdateTraverseURL = strLiveUpdateTraverseURL + "/" + strHighestWebVersion;
                                strLiveUpdateLocation = strLiveUpdateLocation + "/" + strHighestWebVersion;
                                strHTTPSource = Avoli.Web.GetHTTPPageSource(strLiveUpdateTraverseURL);
                                strHTTPSource = strHTTPSource.Replace("<br />", "\r\n");
                                saFileHive = strHTTPSource.Replace("\n", "").Split(new char[] { '\r' });
                                intFilesToDownload = saFileHive.Length - 1;
                            }
                        }
                        else
                        {
                            if (Avoli.DevTools.InDev())
                                Avoli.MessageBox.Show("Unable to locate LiveUpdate information for this application from config.xml location:\r\n\r\n" + strLiveUpdateTraverseURL, Avoli.DevTools.GetCurrentMethodName(), 0);
                        }
                        #endregion
                    }
                    else if (blnViaNetwork)
                    {
                        #region Live Update via Network
                        string strHighestWebVersion = "0";
                        bool blnIsNetworkDrive = false;
                        string[] saVersions = { };
                        string[] saFileHive = { };
                        string[] saFileHiveInfo = { };

                        // If EXE is running from the Network drive we are NOT to perform a Live Update
                        blnIsNetworkDrive = Avoli.SystemInfo.Drives.IsNetworkDrive(System.Windows.Forms.Application.StartupPath.ToString().Substring(0, 1));
                        if (!blnIsNetworkDrive)
                        {	// A) See if our product is on the web for downloadable updates
                            objSmartClient.LiveUpdateLocation = objSmartClient.LiveUpdateLocation.Replace("/", @"\");	// Just in case our config file has invalid/malformed slash's
                            strLiveUpdateLocation = objSmartClient.LiveUpdateLocation + @"\" + strAppName + @"\versions";
                            if (Avoli.SystemInfo.Directories.Exists(strLiveUpdateLocation))
                            {
                                System.Collections.ArrayList alVersions = Avoli.SystemInfo.Directories.Enum(strLiveUpdateLocation, true);
                                foreach (string item in alVersions)
                                {	// The filenames on the network should appear as \products\updates\versions\1.2\...dll etc.
                                    if (item != "")
                                    {
                                        strTmp = item.Replace(strLiveUpdateLocation, "").Replace(@"\", "");
                                        try
                                        {
                                            intVersion = Convert.ToInt16(strTmp.Replace(".", ""));
                                            if (intVersion > intCurrentVersion)
                                            {
                                                strHighestWebVersion = strTmp;
                                                break;
                                            }
                                        }
                                        catch
                                        { }
                                    }
                                }

                                // B) Did we find a higher version?
                                if (strHighestWebVersion != "0")
                                {	// YES! - Fetch all version filenames now (IF ANY) (... products\updates\boiler\versions\1.2)
                                    strLiveUpdateLocation = strLiveUpdateLocation + @"\" + strHighestWebVersion;
                                    saFileHive = Avoli.IO.Files.ToStringArray(strLiveUpdateLocation, "*.*", false, false);
                                    intFilesToDownload = saFileHive.GetLength(0);
                                }
                            }
                        }
                        else
                        {
                            if (Avoli.DevTools.InDev())
                                Avoli.MessageBox.Show("LiveUpdate is either NOT enabled, does NOT have a location or is NOT running from a local drive.", "DEVELOPMENT ENVIRONMENT MESSAGE", 0);
                        }
                        #endregion
                    }

                    if (intFilesToDownload > 0)
                        blnNewVersion = true;
                    #endregion
                }
            }
            return blnNewVersion;
        }
    }

    public static class Confirmation
    {
        public static bool Show(Avoli.SmartClient objSmartClient, string strMessage, string strTitle = "Attention", int intTimesToPrompt = 0)
        {
            #region Examples
            // if (!Avoli.Confirmation.Show(objSmartClient, "Are you sure you wish to delete this entry?", "Confirmation", 2))
            //  	return;
            // else
            //		// Exec delete code here
            #endregion

            #region Display Dialog
            int intYesCnt = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            frmMessageBox.frmMessageBoxResult result = null;
            string[] saClarifications = {"Initial prompt...\r\n\r\n",
                                            "For clarification...\r\n\r\n",
                                            "Ok, I know I already asked but...\r\n\r\n",
                                            "Ok, I have to ask again...\r\n\r\n",
                                            "Ok, I know what you said but...\r\n\r\n",
                                            "Ok, I know it's anal but...\r\n\r\n"};
            if (strMessage != "")
            {
                if (strTitle == null)
                    strTitle = "Attention";
                else if (strTitle == "")
                    strTitle = "Attention";
                if (intTimesToPrompt == null)
                    intTimesToPrompt = 0;
                else if (intTimesToPrompt == 0)
                    intTimesToPrompt = 0;
                if (!strMessage.EndsWith("?")) strMessage += "?";
                if (intTimesToPrompt <= 0) intTimesToPrompt = 1;
                if (intTimesToPrompt > saClarifications.GetUpperBound(0)) intTimesToPrompt = saClarifications.GetUpperBound(0);
                for (int i = 1; i <= intTimesToPrompt; i++)
                {
                    if (i == 1)
                    {
                        if (intTimesToPrompt == 1)
                            result = frmMessageBox.Reference.Show(strMessage, strTitle, null, -1, -1, -1, -1, false, true, 0);
                        else
                            result = frmMessageBox.Reference.Show(strMessage, strTitle + " - Confirmation " + i.ToString() + " of " + intTimesToPrompt.ToString(), null, -1, -1, -1, -1, false, true, 0);
                    }
                    else
                        if (i != intTimesToPrompt)
                        result = frmMessageBox.Reference.Show(saClarifications[i] + strMessage, strTitle + " - Confirmation " + i.ToString() + " of " + intTimesToPrompt.ToString(), null, -1, -1, -1, -1, false, true, 0);
                    else
                        result = frmMessageBox.Reference.Show("I promise this is the last time...\r\n\r\n" + strMessage, strTitle + " - Confirmation " + i.ToString() + " of " + intTimesToPrompt.ToString(), null, -1, -1, -1, -1, false, true, 0);
                    if (result.OK == true)
                    {
                        if (intYesCnt >= intTimesToPrompt)
                            break;
                    }
                    else
                        break; // User clicked NO/Cancel so get out
                    intYesCnt++;
                }
            }
            #endregion

            return result.OK;
        }
    }

    public static class MessageBox
    {
        public static void Show(string strMessage, [Optional] string strTitle, [Optional] int intTimeToLiveInSeconds)
        {
            //if (strTitle != "" && strMethodName != "")
            //    strTitle += " ( " + strMethodName + ")";
            //if (strTitle == "" && strMethodName != "")
            //    strTitle = strMethodName;
            if (strTitle == null)
                strTitle = "Message";
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            frmMessageBox.frmMessageBoxResult result = frmMessageBox.Reference.Show(strMessage, strTitle, null, -1, -1, -1, -1, false, false, intTimeToLiveInSeconds);
        }
    }

    public static class InputBox
    {
        public static string Show(Avoli.SmartClient objSmartClient, string strMessage, [Optional] string strTitle, [Optional] string strDefaultValue, [Optional] int intTimeToLive, [Optional] bool blnRequired)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            frmInputBox.frmInputBoxResult retval = frmInputBox.Reference.Show(objSmartClient, strMessage, strTitle, strDefaultValue, null, -1, -1, -1, -1, intTimeToLive);
            if (retval.OK == true)
            {
                if (retval.Text == "" && blnRequired)
                    Avoli.InputBox.Show(objSmartClient, strMessage, strTitle, strDefaultValue, intTimeToLive, blnRequired);
                return retval.Text.ToString();
            }
            else
                return "";
        }
    }

    /// <summary>
    /// Class for Array specific methods
    /// </summary>
    public class Arrays
    {	// TO DO:	Try to combine routines below for Single and Multi dim arrays by figuring out
        //			how to determine TYPE of string array [] or [,] etc.

        //		public static System.Collections.ArrayList ConcatArrayLists(System.Collections.ArrayList al1, System.Collections.ArrayList al2)
        //		{
        //			#region Examples
        //			//	System.Collections.ArrayList listA = new System.Collections.ArrayList();
        //			//	System.Collections.ArrayList listB = new System.Collections.ArrayList();
        //			//	listA.Add("1");
        //			//	listA.Add("2");
        //			//	listB.Add("3");
        //			//	listB.Add("4");
        //			//	System.Collections.ArrayList al = Avoli.Arrays.ConcatArrayLists(listA, listB);
        //			#endregion
        //
        //			return al1.AddRange(al2);
        //		}

        public static System.Collections.Generic.List<int> ToList(int[] ar, int intStartRange, int intEndRange)
        {   // Generic List
            System.Collections.Generic.List<int> gl = new System.Collections.Generic.List<int>(ar); // Copy to List
            gl = gl.GetRange(intStartRange, intEndRange - intStartRange + 1);
            return gl;
        }

        public static System.Collections.Generic.List<string> StringArrayToList(string[] sa, int intStartRange, int intEndRange)
        {   // Generic List
            System.Collections.Generic.List<string> gl = new System.Collections.Generic.List<string>(sa); // Copy to List
            gl = gl.GetRange(intStartRange, intEndRange - intStartRange + 1);
            // string strDetails = string.Join("\r\n", gl.ToArray()) + "\r\n";  // Make sure we don't loose our line terminators
            return gl;
        }

        public static void ViewByteArray(byte[] array)
        {
            #region Examples
            // Avoli.Arrays.ViewByteArray(bArray);
            #endregion

            int i;
            string strTmp = "";
            for (i = 0; i < array.Length; i++)
            {
                // Console.Write(String.Format("{0:X2}", array[i]));
                if (i == 0)
                    strTmp = array[i].ToString();
                else
                    strTmp = strTmp + "\r\n" + array[i].ToString();
                if ((i % 4) == 3)
                {
                    strTmp = strTmp + "\r\n------";
                    // Console.Write(" ");
                }
            }
            Avoli.MessageBox.Show(strTmp, "Jagged Array Contents", 0);
            // Console.WriteLine();
        }

        public static string ByteArrayToHexString(byte[] bytes)
        {   // Fastest way
            int length = bytes.Length;
            char[] chars = new char[length << 1];
            for (int i = 0, j = 0; i < length; i++, j++)
            {
                byte b = (byte)(bytes[i] >> 4);
                chars[j] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                j++;
                b = (byte)(bytes[i] & 0x0F);
                chars[j] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }
            return new String(chars);
        }

        public static byte[] StringtoByteArray(string str)
        {
            #region Examples
            // byte[] dBytes = Avoli.Arrays.StringtoByteArray("Hello World!");
            #endregion

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        // C# to convert a byte array to a string.
        public static string ByteArrayToString(byte[] dBytes)
        {
            #region Examples
            // byte[] dBytes = Avoli.Arrays.StringtoByteArray("Hello World!");
            // string str = Avoli.Arrays.ByteArrayToString(dBytes);
            #endregion

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(dBytes);
        }

        public static int[][] JaggedInitInt(int intNbrOfArrays, int[] intArrayBounds)
        {	// A jagged array is an array of arrays.

            #region Examples
            // int[][] ja = Avoli.Arrays.JaggedInitInt(3, int[] = {2, 5, 7});
            #endregion

            int[][] ja = new int[intNbrOfArrays][];
            for (int i = 0; i < ja.Length; i++)
                ja[i] = new int[intArrayBounds[i]];
            return ja;
        }

        public static void ViewArrayJaggedInt(int[][] ja, int intNbrOfArrays)
        {
            string strTmp = "";
            for (int i = 0; i < ja.GetLength(0); i++)
            {	// Loop through the elements of the current array
                for (int x = 0; x < ja[i].GetLength(0); x++)
                {
                    if (x == 0)
                        strTmp = ja[i][x].ToString();
                    else
                        strTmp = strTmp + "\r\n" + ja[i][x].ToString();
                }
                strTmp = strTmp + "\r\n------";
            }
            Avoli.MessageBox.Show(strTmp, "Jagged Array Contents", 0);
        }

        public static string GetRandomValue(string[] sa)
        { return sa[Avoli.Randomizer.rng.Next(0, sa.Length - 1)]; }

        public static void ToHTMLFile(Array ar, string strFile, string strReportHeader, bool blnLaunchFileWhenDone)
        {
            #region Examples (Tested: 9/5/04)
            // Ex 1: // REPORTING: Fetch Excel cell values to an array then export it out to an HTML file
            //		 Array ar = Avoli.DataStorage.File.XLS.GetCellValues(@"c:\document settings\owner\Avoli.test files\test.xls", "Output", "B2", "B5", "A|B|C|E|I);
            //       Avoli.Arrays.Export.ToHTMLFile(ar, @"c:\export.htm", "Array Contents", true);

            // Ex 2: // REPORTING: Take a datatable and export it out to an HTML file
            //		 Avoli.Arrays.Export.ToHTMLFile( DataStorage.Database.DataTableObjectToArray(datatable, ",", false), @"C:\export.htm", "Output", true);
            #endregion

            // Report Header (Optional)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<html>\n");
            if (strReportHeader == "" || strReportHeader == String.Empty || strReportHeader == null)
            {
                sb.Append(strReportHeader + " ... as of " + DateTime.Now + "\n");
                sb.Append("<hr color='#1F60A9' size='1'>\n");
            }
            sb.Append("<table>\n");

            // Output Detail
            string strFieldValue = "";
            int items1 = ar.GetLength(0);
            int items2 = ar.GetLength(1);
            for (int i = 0; i < items1; i++)
            {
                sb.Append("<tr>\n");
                for (int j = 0; j < items2; j++)
                {
                    strFieldValue = ar.GetValue(i, j).ToString();
                    if (strFieldValue == null) strFieldValue = "";
                    if (j > 0) sb.Append("<td width='20'></td>\n");
                    sb.Append("<td>" + strFieldValue + "</td>\n");
                }
                sb.Append("</tr>\n");
            }
            sb.Append("</table>\n</html>\n");

            // Footer Total
            if (strReportHeader != "") sb.Append("<br />Total Returned: " + items1.ToString());

            // Output the result set to our file
            string strOutput = sb.ToString();
            System.IO.FileStream fs = new System.IO.FileStream(strFile, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            sw.Write(strOutput);
            sw.Close();
            fs.Close();
            if (blnLaunchFileWhenDone == true) Avoli.IO.Files.LaunchAssociation(strFile);
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        public static void ToTXTFile(Array ar, string strFile, bool blnLaunchFileWhenDone)
        {
            #region Examples
            // Tested: 9/5/04
            // Ex 1: Array ar = Avoli.DataStorage.File.XLS.GetCellValues(strFileToSearch, "A3", "A341", "A|B|C|E|I);
            //       Arrays.Export.ToTXTFile(ar, "c:\export.csv", "Array Contents", true);

            // Ex 2: Avoli.Arrays.Export.ToTXTFile( DataStorage.Database.DataTableObjectToArray(datatable, ",", false), @"C:\temp\xyz.csv", true);
            #endregion

            // Output Detail
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string strFieldValue = "";
            string strOutput = "";
            int items1 = ar.GetLength(0);
            int items2 = ar.GetLength(1);
            for (int i = 0; i < items1; i++)
            {
                for (int j = 0; j < items2; j++)
                {
                    strFieldValue = ar.GetValue(i, j).ToString();
                    if (strOutput == null) strOutput = "";
                    if (j == 0)
                        strOutput = strFieldValue;
                    else
                        strOutput = strOutput + "," + strFieldValue;
                }
                sb.Append(strOutput + "\n");
            }

            // Output the result set to our file
            strOutput = sb.ToString();
            System.IO.FileStream fs = new System.IO.FileStream(strFile, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            sw.Write(strOutput);
            sw.Close();
            fs.Close();
            if (blnLaunchFileWhenDone == true) Avoli.IO.Files.LaunchAssociation(strFile);
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        public static void ToFile(string[] ar, string strFile, bool blnLaunchFileWhenDone)
        {
            #region Examples
            // Ex1: string[] ar = {"String 0", "String 1", "String 2", "String 3"};
            //	    Avoli.Arrays.Export.ToFile(ar, "@c:\test.txt");
            // Ex2: Avoli.Arrays.Export.ToFile( Avoli.Arrays.DataTableToArray(datatable, ",", false), @"C:\misc\xyz.txt" );
            #endregion

            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                System.IO.StreamWriter sw = System.IO.File.CreateText(strFile);
                int items1 = ar.GetLength(0);
                for (int i = 0; i < ar.Length; i++)
                    sw.WriteLine(ar[i]);
                sw.Close();
                if (blnLaunchFileWhenDone == true) Avoli.IO.Files.LaunchAssociation(strFile);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
            catch (Exception ex)
            {
                Avoli.MessageBox.Show(ex.Message, "", 0);
            }
        }

        public static void ToFile(Array ar, string strFile, bool blnLaunchFileWhenDone)
        {
            #region Examples
            // Ex 1: string[,] ar = new string[2, 2] { {"Rosy","Amy"}, {"Peter","Albert"} };
            //		 Avoli.Arrays.Export.ToFile(ar, @"c:\test.txt");

            // Ex 2: Avoli.Arrays.Export.ToFile( Avoli.Arrays.DataTableToArray(datatable, ",", false), @"C:\misc\xyz.txt" );
            #endregion

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.IO.StreamWriter sw = System.IO.File.CreateText(strFile);
            int items1 = ar.GetLength(0);
            int items2 = ar.GetLength(1);
            for (int i = 0; i < items1; i++)
                for (int j = 0; j < items2; j++)
                    sw.WriteLine(ar.GetValue(i, j));
            sw.Close();
            if (blnLaunchFileWhenDone == true) Avoli.IO.Files.LaunchAssociation(strFile);
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        public static void ToFile(System.Collections.ArrayList ar, string strFile, bool blnLaunchFileWhenDone)
        {
            #region Examples
            // Ex 1:	System.Collections.ArrayList al = new System.Collections.ArrayList();
            //			al.Add("1");
            //			al.Add("2");
            //			Avoli.Arrays.Export.ToFile(al, @"c:\test.txt");
            // Ex 2:	Avoli.Arrays.Export.ToFile( Avoli.Arrays.DataTableToArray(datatable, ",", false), @"C:\misc\xyz.txt" );
            #endregion

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.IO.StreamWriter sw = System.IO.File.CreateText(strFile);
            foreach (string str in ar)
                sw.WriteLine(str);
            sw.Close();
            if (blnLaunchFileWhenDone == true) Avoli.IO.Files.LaunchAssociation(strFile);
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        public static void Print(Array ar, string strPrinterName, string strReportHeader)
        {
            // Ex: string[,] ar = new string[2, 2] { {"Rosy","Amy"}, {"Peter","Albert"} };
            //	   Arrays.Print (ar, "", "Array Contents");

            // Get the printer ready
            if (strPrinterName == "" || strPrinterName == String.Empty || strPrinterName == null)
            {
                System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                strPrinterName = pd.PrinterSettings.PrinterName;
            }
            System.IntPtr lhPrinter = new System.IntPtr();
            DOCINFO di = new DOCINFO();
            int pcWritten = 0;
            di.pDocName = "PrintDoc";
            di.pDataType = "RAW";
            Printing.Printer.OpenPrinter(strPrinterName, ref lhPrinter, 0);
            Printing.Printer.StartDocPrinter(lhPrinter, 1, ref di);
            Printing.Printer.StartPagePrinter(lhPrinter);
            int intLeftMargin = 50;
            int intLineHeightInDots = 50;
            int intCharacterWidthInDots = 30;
            int intPrintAt = 0;
            string strStartOfLine = "";
            string strFieldValue = "";

            // Draw Header (if any)
            int lcnt = 0;
            try
            {
                lcnt++;
                intPrintAt = (lcnt * intLineHeightInDots);
                strStartOfLine = "\x1b*p" + intLeftMargin.ToString() + "x" + intPrintAt.ToString() + "Y";
                strFieldValue = strStartOfLine + strReportHeader + " :   " + DateTime.Now;
                Printing.Printer.WritePrinter(lhPrinter, strFieldValue, strFieldValue.Length, ref pcWritten);

                // Draw a Blank Line
                intPrintAt = (lcnt * intLineHeightInDots);
                strStartOfLine = "\x1b*p" + intLeftMargin.ToString() + "x" + intPrintAt.ToString() + "Y";
                strFieldValue = strStartOfLine + " ";
                Printing.Printer.WritePrinter(lhPrinter, strFieldValue, strFieldValue.Length, ref pcWritten);
                lcnt++;

                // Draw a Line
                intPrintAt = (lcnt * intLineHeightInDots);
                strStartOfLine = "\x1b*p" + intLeftMargin.ToString() + "x" + intPrintAt.ToString() + "Y";
                strFieldValue = strStartOfLine.PadRight(50 * intCharacterWidthInDots, Convert.ToChar("-"));
                Printing.Printer.WritePrinter(lhPrinter, strFieldValue, strFieldValue.Length, ref pcWritten);
                lcnt++;
            }
            catch (Exception e)
            {
                Avoli.MessageBox.Show(e.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
            }

            // Draw Detail
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int items1 = ar.GetLength(0);
            int items2 = ar.GetLength(1);
            for (int i = 0; i < items1; i++)
            {
                for (int j = 0; j < items2; j++)
                {
                    try
                    { sb.Append(ar.GetValue(i, j).ToString() + "\t"); }
                    catch
                    { sb.Append("" + "\t"); }
                }

                // Print this line
                lcnt++;
                strFieldValue = sb.ToString();
                intPrintAt = (lcnt * intLineHeightInDots);
                strStartOfLine = "\x1b*p" + intLeftMargin.ToString() + "x" + intPrintAt.ToString() + "Y";
                strFieldValue = strStartOfLine + sb.ToString();
                Printing.Printer.WritePrinter(lhPrinter, strFieldValue, strFieldValue.Length, ref pcWritten);
            }
            Printing.Printer.EndPagePrinter(lhPrinter);
            Printing.Printer.EndDocPrinter(lhPrinter);
            Printing.Printer.ClosePrinter(lhPrinter);
        }

        public static void Print(string[] sa, string strPrinterName, string strReportHeader)
        {
            // Ex: string[] sa = new string[1] { {"Rosy"}, {"Peter"} };
            //	   Arrays.Print (sa, "", "Array Contents");

            // Get the printer ready
            if (strPrinterName == "" || strPrinterName == String.Empty || strPrinterName == null)
            {
                System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                strPrinterName = pd.PrinterSettings.PrinterName;
            }
            System.IntPtr lhPrinter = new System.IntPtr();
            DOCINFO di = new DOCINFO();
            int pcWritten = 0;
            di.pDocName = "PrintDoc";
            di.pDataType = "RAW";
            Printing.Printer.OpenPrinter(strPrinterName, ref lhPrinter, 0);
            Printing.Printer.StartDocPrinter(lhPrinter, 1, ref di);
            Printing.Printer.StartPagePrinter(lhPrinter);
            int intLeftMargin = 50;
            int intLineHeightInDots = 50;
            int intCharacterWidthInDots = 30;
            int intPrintAt = 0;
            string strStartOfLine = "";
            string strFieldValue = "";

            // Draw Header (if any)
            int lcnt = 0;
            try
            {
                lcnt++;
                intPrintAt = (lcnt * intLineHeightInDots);
                strStartOfLine = "\x1b*p" + intLeftMargin.ToString() + "x" + intPrintAt.ToString() + "Y";
                strFieldValue = strStartOfLine + strReportHeader + " :   " + DateTime.Now;
                Printing.Printer.WritePrinter(lhPrinter, strFieldValue, strFieldValue.Length, ref pcWritten);

                // Draw a Blank Line
                intPrintAt = (lcnt * intLineHeightInDots);
                strStartOfLine = "\x1b*p" + intLeftMargin.ToString() + "x" + intPrintAt.ToString() + "Y";
                strFieldValue = strStartOfLine + " ";
                Printing.Printer.WritePrinter(lhPrinter, strFieldValue, strFieldValue.Length, ref pcWritten);
                lcnt++;

                // Draw a Line
                intPrintAt = (lcnt * intLineHeightInDots);
                strStartOfLine = "\x1b*p" + intLeftMargin.ToString() + "x" + intPrintAt.ToString() + "Y";
                strFieldValue = strStartOfLine.PadRight(50 * intCharacterWidthInDots, Convert.ToChar("-"));
                Printing.Printer.WritePrinter(lhPrinter, strFieldValue, strFieldValue.Length, ref pcWritten);
                lcnt++;
            }
            catch (Exception e)
            {
                Avoli.MessageBox.Show(e.Message, Avoli.DevTools.GetCurrentMethodName(), 0);
            }

            // Draw Detail
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string item in sa)
            {
                lcnt++;
                intPrintAt = (lcnt * intLineHeightInDots);
                strStartOfLine = "\x1b*p" + intLeftMargin.ToString() + "x" + intPrintAt.ToString() + "Y";
                sb.Append(strStartOfLine + item + "\r\n");
            }

            // Print this line
            lcnt++;
            strFieldValue = sb.ToString();
            intPrintAt = (lcnt * intLineHeightInDots);
            strStartOfLine = "\x1b*p" + intLeftMargin.ToString() + "x" + intPrintAt.ToString() + "Y";
            strFieldValue = strStartOfLine + sb.ToString();
            Printing.Printer.WritePrinter(lhPrinter, strFieldValue, strFieldValue.Length, ref pcWritten);

            Printing.Printer.EndPagePrinter(lhPrinter);
            Printing.Printer.EndDocPrinter(lhPrinter);
            Printing.Printer.ClosePrinter(lhPrinter);
        }

        public static string[] BubbleSortStringArray(string[] nlist, bool blnDescending)
        {   // Ok an old way of sorting but requested by a client

            #region Examples (Tested: Nov, 22, 2005)
            // string[] mylist = {"Zack", "Harry", "Bobby", "Stan"};
            // string[] sortedList = Avoli.Arrays.BubbleSortStringArray(mylist, false);
            #endregion

            #region See Also
            // System.Array.Sort(saFound);
            // System.Array.Reverse(saFound);
            #endregion

            #region Called by
            // Avoli.Controls.Children.General.LocationsSortedToArray
            #endregion

            int i, j;
            string temp;
            for (i = nlist.Length - 1; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (nlist[j - 1].CompareTo(nlist[j]) > 0)
                    {
                        temp = nlist[j - 1];
                        nlist[j - 1] = nlist[j];
                        nlist[j] = temp;
                    }
                }
            }

            if (blnDescending)
                System.Array.Reverse(nlist);

            return nlist;
        }
        public static int[] BubbleSortIntArray(int[] nlist)
        {   // Ok an old way of sorting but requested by a client

            #region Examples (Tested: Nov, 22, 2005)
            // int[] mylist = {16, 2, 9, 8, 32, 1, 10, 14, 3, 7};
            // int[] sortedList = Avoli.Arrays.BubbleSortIntArray(mylist);
            #endregion

            int i, j, temp;
            for (i = nlist.Length - 1; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (nlist[j - 1] > nlist[j])
                    {
                        temp = nlist[j - 1];
                        nlist[j - 1] = nlist[j];
                        nlist[j] = temp;
                    }
                }
            }
            return nlist;
        }
        public static bool IsEmptyBucket(Array ac, int intBucketToSearch)
        {
            bool blnIsEmpty = true;
            for (int j = 0; j < ac.GetUpperBound(0); j++)
            {
                int items1 = ac.GetLength(intBucketToSearch);
                for (int i = 0; i < items1; i++)
                {
                    try
                    {
                        if (ac.GetValue(i, 0).ToString() != "")
                        {
                            blnIsEmpty = false;
                            break;
                        }
                    }
                    catch { }
                }
            }
            return blnIsEmpty;
        }
        public static bool IsEmpty(string[] sa)
        {
            #region Evaluation
            bool blnIsEmpty = true;
            for (int j = 0; j < sa.GetUpperBound(0); j++)
            {
                if (sa[j].ToString() != "")
                {
                    blnIsEmpty = false; break;
                }
            }
            #endregion

            return blnIsEmpty;
        }

        public static string[] Init()
        { return new string[] { }; }

        public static string[] Init(string[] ar)
        { return new string[] { }; }

        //public static string[,] InitMulti (string[,] ar)
        //{ return new string[,] {}; }

        public static string[] RedimStringArray(string[] pOldArray, Int32 pNewSize)
        {	// Manual Array Resizing - The contents of an array are laid out contiguously in the managed heap.
            // NOTE: ArrayList's are Self-redimensioning arrays

            #region Examples (Tested: December 12, 2005)
            // string[] sa1 = new string[0];
            // sa1 = Avoli.Arrays.RedimStringArray(sa1, sa1.GetUpperBound(0) + 10);
            // sa1 = Avoli.Arrays.RedimStringArray(sa, 100);
            #endregion

            Int32 counter = 0;
            string[] newArray = null;
            if (pOldArray != null)
            {
                if (pNewSize <= pOldArray.Length)
                    newArray = pOldArray;
                else
                {
                    newArray = new string[pNewSize];
                    foreach (String item in pOldArray)
                    {
                        newArray[counter] = item;
                        counter++;
                    }
                }
            }
            return newArray;	// All the items of the old array have been copied to the new array and the new items are NULL
        }

        public static byte[] ReDimByteArray(byte[] pOldArray, Int32 pNewSize)
        {	// Manual Array Resizing - The contents of an array are laid out contiguously in the managed heap.
            // NOTE: ArrayList's are Self-redimensioning arrays

            #region Examples (Tested: December 12, 2005)
            // byte[] sa1 = new byte[0];
            // sa1 = Avoli.Arrays.ReDimByteArray(sa1, sa1.GetUpperBound(0) + 10);
            // sa1 = Avoli.Arrays.ReDimByteArray(sa, 100);
            #endregion

            Int32 counter = 0;
            byte[] newArray = null;
            if (pOldArray != null)
            {
                if (pNewSize <= pOldArray.Length)
                    newArray = pOldArray;
                else
                {
                    newArray = new byte[pNewSize];
                    foreach (byte item in pOldArray)
                    {
                        newArray[counter] = item;
                        counter++;
                    }
                }
            }
            return newArray;	// All the items of the old array have been copied to the new array and the new items are NULL
        }

        public static int[] ReDimIntArray(int[] pOldArray, Int32 pNewSize)
        {	// Manual Array Resizing - The contents of an array are laid out contiguously in the managed heap.
            // NOTE: ArrayList's are Self-redimensioning arrays

            #region Examples (Tested: December 12, 2005)
            // int[] sa1 = new int[0];
            // sa1 = Avoli.Arrays.ReDimIntArray(sa1, sa1.GetUpperBound(0) + 10);
            // sa1 = Avoli.Arrays.ReDimIntArray(sa, 100);
            #endregion

            Int32 counter = 0;
            int[] newArray = null;
            if (pOldArray != null)
            {
                if (pNewSize <= pOldArray.Length)
                    newArray = pOldArray;
                else
                {
                    newArray = new int[pNewSize];
                    foreach (int item in pOldArray)
                    {
                        newArray[counter] = item;
                        counter++;
                    }
                }
            }
            return newArray;	// All the items of the old array have been copied to the new array and the new items are NULL
        }

        public static string[,] ReDimMulti(string[,] pOldArray, Int32 pNewSize)
        {
            #region Examples
            // string[,] sa1 = new string[2, 2] { {"Rosy","Amy"}, {"Peter","Albert"} };
            // sa = Avoli.Arrays.ReDimMulti(sa, 52);
            #endregion

            Int32 counter = 0;
            string[,] newArray = null;

            if (pOldArray != null)
            {
                if (pNewSize <= pOldArray.Length)
                    newArray = pOldArray;
                else
                {
                    newArray = new String[pNewSize, pOldArray.GetUpperBound(1)];
                    foreach (String item in pOldArray)
                    {	// Needs Testing
                        newArray[counter, pOldArray.GetUpperBound(1)] = item;
                        counter++;
                    }
                }
            }
            return newArray;	// All the items of the old array have been copied to the new array and the new items are NULL
        }

        public static string[] Append(string[] ar, string strNewItem)
        {
            #region Examples (Dynamic array resize - Redim Preserve)
            // Tested: 9/9/04
            // Ex: Speedy string array adds w/o slowness of conventional redim preserve style
            // string[] s = new string[0];
            // s = Avoli.Arrays.Append(s, "Hello");
            // s = Avoli.Arrays.Append(s, "World");
            // Avoli.DevTools.ViewArrayClass(s);
            #endregion

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Collections.ArrayList alArgs = new System.Collections.ArrayList(ar);
            alArgs.Add(strNewItem);
            string[] newArray = (string[])alArgs.ToArray(typeof(string));
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            return newArray;
        }

        public static int[] AppendInt(int[] ar, int intNewItem)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Collections.ArrayList alArgs = new System.Collections.ArrayList(ar);
            alArgs.Add(intNewItem);
            int[] newArray = (int[])alArgs.ToArray(typeof(int));
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            return newArray;
        }

        public static string[] Combine(string[] ar1, string[] ar2)
        {
            string[] ar3 = new string[ar1.Length + ar2.Length];
            ar1.CopyTo(ar3, 0);
            ar2.CopyTo(ar3, ar1.Length);
            return ar3;
        }

        public static int[] CombineInt(int[] ar1, int[] ar2)
        {
            int[] ar3 = new int[ar1.Length + ar2.Length];
            Array.Copy(ar1, ar3, ar1.Length);
            Array.Copy(ar2, 0, ar3, ar1.Length, ar2.Length);
            return ar3;
        }

        public static System.Collections.ArrayList CombineArrayList(System.Collections.ArrayList al1, System.Collections.ArrayList al2)
        {
            al1.AddRange(al2);
            return al1;
        }

        public static string[] RemoveByValue(string[] sa, string strSearchValues, bool blnFuzzySearch)
        {
            #region Examples (NOT case sensitive)
            // Tested: 9/9/04
            // string[] sa = { "Zack", "Harry", "Bobby", "Dick" };
            // sa = Avoli.Arrays.RemoveByValue(sa, "Harry|Dick", false);
            // Avoli.Arrays.ViewStringArray(sa, "Results minus Harry|Dick");
            #endregion

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            int items1 = sa.GetLength(0);
            for (int i = 0; i < sa.Length; i++)
            {
                // See if we have multile WhatToRemove delimited values
                if (strSearchValues.IndexOf("|") == 0)
                {   // Single value search removal
                    if (blnFuzzySearch)
                    {	// Fuzzy match test
                        if (sa[i].ToUpper().IndexOf(strSearchValues.ToUpper()) >= 0)
                            al.Add(sa[i]);
                    }
                    else
                    {	// Exact match test
                        if (sa[i].ToUpper() != strSearchValues.ToUpper())
                            al.Add(sa[i]);
                    }
                }
                else
                {   // Multiple value search removal
                    string[] saValues = strSearchValues.Split(new char[] { '|' });
                    bool blnOkayAdd = true;
                    foreach (string strValue in saValues)
                    {
                        if (blnFuzzySearch)
                        {	// Fuzzy match check - NOT case sensitive
                            if (sa[i].ToUpper().IndexOf(strValue.ToUpper()) > -1)
                                blnOkayAdd = false;
                        }
                        else
                        {	// Exact match check - NOT case sensitive
                            if (sa[i].ToUpper() == strValue.ToUpper())
                                blnOkayAdd = false;
                        }
                    }
                    if (blnOkayAdd)
                        al.Add(sa[i]);
                }
            }
            string[] newArray = (string[])al.ToArray(typeof(string));
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            return newArray;
        }

        public static System.Collections.ArrayList RemoveByValue(System.Collections.ArrayList ar, string strSearchValues, bool blnFuzzySearch)
        {
            #region Examples (NOT Case sensitive)
            // System.Collections.ArrayList al = new System.Collections.ArrayList;
            // al.AddRange = { "Zack", "Harry", "Bobby", "Dick" };
            // al = Avoli.Arrays.RemoveByValue(al, "Harry|Dick", false);
            // Avoli.Arrays.ViewArrayClass(al, "Results minus Harry|Dick");
            #endregion

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            foreach (string item in ar)
            {
                // See if we have multile WhatToRemove delimited values
                if (strSearchValues.IndexOf("|") == 0)
                {   // Single value search removal
                    if (blnFuzzySearch)
                    {	// Fuzzy match check - NOT case sensitive
                        if (item.ToUpper().IndexOf(strSearchValues.ToUpper()) == -1) al.Add(item);
                    }
                    else
                    {	// Exact match check - NOT case sensitive
                        if (item.ToUpper() != strSearchValues.ToUpper()) al.Add(item);
                    }
                }
                else
                {   // Multiple value search removal
                    string[] saValues = strSearchValues.Split(new char[] { '|' });
                    bool blnOkayAdd = true;
                    foreach (string strValue in saValues)
                    {
                        if (blnFuzzySearch)
                        {	// Fuzzy match check - NOT case sensitive
                            if (item.ToUpper().IndexOf(strValue.ToUpper()) > -1)
                                blnOkayAdd = false;
                        }
                        else
                        {	// Exact match check - NOT case sensitive
                            if (item.ToUpper() != strValue.ToUpper())
                                blnOkayAdd = false;
                        }
                    }
                    if (blnOkayAdd)
                        al.Add(item);
                }
            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            return al;
        }

        public static string[] RemoveEmptyElements(string[] arSources)
        {
            int intItemNbr = 0;
            foreach (string strSourceData in arSources)
            {
                if (strSourceData == "")
                    arSources = RemoveByLocation(arSources, intItemNbr);
                intItemNbr++;
            }
            return arSources;
        }

        public static string[] RemoveByLocation(string[] sa, int intElementNbr)
        {
            #region Examples
            // Tested: 9/9/04
            // string[] sa = new string[0];
            // sa = Arrays.Add(s, "Hello");
            // sa = Arrays.Add(s, "There");
            // sa = Arrays.Add(s, "World");
            // DevTools.ViewArrayClass(s, "Before");
            // int intFoundAtRow = (int)Arrays.Locate(sa, "There");
            // if (intFoundAtRow != -1)
            //		s = Arrays.RemoveByLocation(sa, intFoundAtRow);
            // DevTools.ViewArrayClass(s, "After");
            #endregion

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            int items1 = sa.GetLength(0);
            for (int i = 0; i < sa.Length; i++)
                if (i != intElementNbr)
                    al.Add(sa[i]);
            string[] newArray = (string[])al.ToArray(typeof(string));
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            return newArray;
        }

        public static System.Collections.ArrayList RemoveByLocation(System.Collections.ArrayList al, int intElementNbr)
        {
            #region Examples
            // Ex: Locate and item and remove it
            // int intFoundAtRow = (int)Arrays.Locate(al, "South Carolina");
            // if (intFoundAtRow != -1)
            //		s = Arrays.Remove(ar, intFoundAtRow);
            #endregion

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Collections.ArrayList alNew = new System.Collections.ArrayList();
            int i = 0;
            foreach (string item in al)
                if (i != intElementNbr) alNew.Add(item);
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            return alNew;
        }

        public static void ViewArrayInt(int[] ar)
        {
            #region Process Standard Out
            string strArrayContents = "";
            for (int cnt = 0; cnt < ar.Length; cnt++)
                strArrayContents += ar[cnt] + " ";
            Avoli.MessageBox.Show(strArrayContents.TrimEnd().ToString(), "", 0);
            #endregion
        }
        public static void ViewStringArray(string[] ar, string strMessage)
        {
            #region Examples
            // Ex: Avoli.Arrays.ViewStringArray(al);	
            #endregion

            #region Process Standard Out
            for (int i = 0; i < ar.Length; i++)
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Continue traversing values? \n\n" + ar[i], strMessage + " - String Array Values", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if (result != System.Windows.Forms.DialogResult.Yes) return;
            }
            #endregion
        }
        public static void ViewStringArrayMulti(string[,] ar, string strMessage)
        {	// To Do: Figure out a way to dynamically determine bucket count etc.
            #region Examples
            // Ex: Arrays.ViewStringArrayMulti(sa);	
            #endregion

            #region Process Standard Out
            // Fetch # of dimensions
            int items1 = ar.GetLength(0);
            int items2 = ar.GetLength(1);
            for (int i = 0; i < items1; i++)
            {
                for (int j = 0; j < items2; j++)
                {
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Continue traversing values? \n\n" + "Element1=" + ar[i, 0] + "\nElement2=" + ar[j, 1] + "\nElement3=" + ar[j, 2], strMessage + " - String Array Values", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                    if (result != System.Windows.Forms.DialogResult.Yes) return;
                }
            }
            #endregion
        }
        public static void ViewArrayClass(Array ac, string strMessage)
        {
            #region Examples (Tested: Oct 14, 2005)
            // Avoli.Arrays.ViewArrayClass(ac);		
            #endregion

            #region Process Standard Out
            int items2 = -1;
            int items1 = ac.GetLength(0);
            try
            {
                items2 = ac.GetLength(1);
            }
            catch { }
            string strTmp = "";
            string strBucketValue = "";
            int intEmptyCnt = 0;
            for (int i = 0; i < items1; i++)
            {
                strTmp = "";
                intEmptyCnt =