# AutoPatcher
 
This is an autopatcher which is created by using C# in the Visual Studio Environment.  

Autopatcher has four parts;                	

1- AutopatcherClient =   The one that client has in his/her own computer.

2- AutopatcherDiffer = The one which will be used in the server side to produce diff files between two versions.

3- AutopatcherZipper =The one which will be used by company to produce compressed files.

4- MVCApplication = The one to share files.

  And there is also an unit test for autopatch process.
 
Zipper and Content File Producer (“AutopatcherZipper”)=>
 
  ● 	This windows form application compresses all the files of a version into zip files into a selected directory by the name of their MD5 hash values. Because they are named by their hash values, it checks if this file compressed for another version before. If this .zip file already exists in the selected directory, this file is not gonna be compresed.
 
  ● 	This application also creates a content file of version.
  
  Content file has files and folders of version;
 
  	● 	If it is a folder, content file line has its fullpath from selected folder.
  	
  	● 	If it is a file, content file line has its name and its MD5 hash value.
 
  Example for context file lines: 

 /
  
  file1    	MD5_HASH_VALUE
  file2   		MD5_HASH_VALUE
  
  /Directory1
  
  file1    	MD5_HASH_VALUE
  
  file2     	MD5_HASH_VALUE
  
  /Directory1/Directory1.1
  
  file     		MD5_HASH_VALUE

 
Diff Creator (”AutopatcherDiffer”) =>
 
  To use this application there should be 3 paths selected one is for old version path, the other is for new version path and the last one is to save the result files.
 


  ● 	By comparing these two versions, application creates 5 files;
 
  ● 	A file which contains all the files which will be inserted in the new version .

  ● 	A text file to read insert content. Insert content file contains inserted files and folders names; 
  
    ○   If it is a folder, content file line has its fullpath from selected folder. 
    
    ○   If it is a file, content file line has its fullpath from selected folder, its starting offset in the insert file and its size. 
    
  ● 	A file that contains all the differences between the file in the old version and the file that is updated in the new version. Updated files will be understood by comparing their MD5 values. If a file with the same name in the same directory exists in both versions but they are different as it is understanded from their hash values, application uses Google Diff-Match-Patch algorithm and creates a diff file to achieve new version of the file from the old version of it. To sum up, if a file is updated in the new version, application doesnt send all of the new file to the client. Instead of it it sends only the difference which is created by Google Diff-Match-Patch alghoritm. 
  
  ● 	A text file to read update content. Update content file has diff file names; line has its full path from selected folder, its starting offset in the update file and its size. 
  
  ● 	A text file which has the paths of files and folders that will be deleted in the new version. 
   
  ● 	At the end we have 5 files named
  
    ○   oldversionname_newversionname-insert
    ○   oldversionname_newversionname-insertcontent.txt
    ○   oldversionname_newversionname-update
    ○   oldversionname_newversionname-updatecontent.txt
    ○   oldversionname_newversionname-deletecontent.txt 

MVC Application
 
  It has two aims;

  1.   It serves the version name that client has and returns the next version that client should have. To decide which version will be the next there are some ideas in my mind; 
  
    ● The version which is produced right after the version that client has. 

    ● The first version which is produced after the version that client has and it is divisible by a decided number. 
    
    ● The last version. 

  2.   It serves the file that client requests, if it exists. 

Client Side of AutoPatcher (”AutopatcherClient”) =>
 
It has two big steps repair and upgrade.
 
REPAIR
 
  ● 	The application learns current version by reading from a text file. 
  
  ● 	Downloads context file of this current version. 
  
  ● 	By reading this context file it learns the repository that this current version should have. Application finds changed or missing files and missing folders that client doesn’t have but this version should have. 
  
  ● 	By using the hash values that are written in text file it downloads the necessary files as compressed. It also creates directories if there are missing folders. 
  
  ● 	By decompressing these zip files, application creates necessary files in this version. After decompressing each file application checks if the downloaded file is the correct one by comparing the hash value of decompressed file with the hash value which is written in context file.
   
  When repair phase is done, client has all the requirements of this version. Now it is time to upgrade our version.
   
UPGRADE
 
  Here client side application learns which version that client should have by giving the current version that client has. After getting answer application downloads these following 5 files if they exists.
   
    ○   currentversionname_versiontogo-insert
    ○   currentversionname_versiontogo-insertcontent.txt
    ○   currentversionname_versiontogo-update
    ○   currentversionname_versiontogo-updatecontent.txt
    ○   currentversionname_versiontogo-deletecontent.txt
     
  Then application
   
  ● 	Partitions update file as it is written in update content and creates diff data. By applying this diff data with Google diff-match-patch algorithm to the file which will be updated, it creates the updated file which is in new version. 
  
  ● 	Creates necessary directories as they are written in insert content. 
  
  ● 	Partitions insert file as it is written in insert content and creates inserted files. 
  
  ● 	Deletes necessary files and folders as they are written in delete content.
   
  This upgrade process continues until next version will be equal to the version that client has.
   


	

	
