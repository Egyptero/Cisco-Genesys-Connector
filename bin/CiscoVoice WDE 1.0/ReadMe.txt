*************************************************************************************************
	InstGenesys WDE 1.0
	Created by ISTNetworks
	Copy Rights IST 2018.
*************************************************************************************************

Installation Steps
------------------
1- Copy all dll files from this folder to the folder of the WDE installation as the following
C:\Program Files (x86)\GCTI\Workspace Desktop Edition\InteractionWorkspace
2- Open the file Genesyslab.Desktop.Modules.StandardResponse.module-config from the WDE installation path in case Social Media plugin is installed.
3- Add the following line under Tasks Tag

    <task name="InteractionWorkspace.WorkItem.canUse" clickOnceGroupsToDownload="WorkItem" modulesToLoad="InstGenesysModule" />
4- Add the following module under Modules Tag

   <module assemblyFile="Genesyslab.Desktop.Modules.InstGenesys.dll"
				moduleType="Genesyslab.Desktop.Modules.InstGenesys.InstGenesysModule"
				moduleName="InstGenesysModule"
				startupLoaded="false"/>

5- In case of social media plugin is installed , please modify the file : Genesyslab.Desktop.Modules.SocialMedia.module-config

Testing Steps
-------------
1- Login to InstGenesys Administrator at the link
http://emp.istnetworks.com:8080/InstGenesys
2- Login with Facebook account
3- Add the configuration -IST System Engineer may help you with that-
4- Start the InstGenesys Engine
5- Login to WDE with Valid User ID
6- Start receiving Instagram comments / feeds.