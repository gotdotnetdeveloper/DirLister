# DirLister v2 beta

Easy-to use utility for quickly listing folder or drive contents. DirLister can be run from the graphical interface or Windows Explorer right-click menu 

### Features
* Multi-format output: HTML, plain text (.txt), CSV, JSON, XML, Markdown (.md). See output examples [here](https://github.com/SanderSade/DirLister/tree/master/docs), also [rendered HTML page](https://sandersade.github.io/DirLister/DirLister.2019-02-22_14-32-27.C_DirListerTest.html)
* Filtering by filename (multiple wildcards or regular expression)
* Shell integration - can be run from right-click menu for folders and drives, with or without opening the UI. Use Sent To --> DirLister to create a list of multiple folders directly from Explorer.
* Can include file sizes
* Option to include media info
* Option to include file creation and modification dates
* Drag'n'drop support
* Can include system and hidden files
* Fast, free, open source

#### Reviews
* https://www.ghacks.net/2019/07/22/dirlister-open-source-program-to-print-a-list-of-files-and-folders/
* https://www.softpedia.com/get/System/File-Management/DirLister.shtml

### Installation

* Download from [Releases](https://github.com/SanderSade/DirLister/releases)
  * During the initial beta testing, only the portable version is available.
* Extract all files to easy-to-get location (e.g. c:\tools\DirLister)
* Double-click on DirLister.exe. This will both start the program and create the shell shortcuts
* To uninstall, uncheck in UI "Enable shell integration.." and delete the files

#### Screenshots
Click on image to enlarge

![Input tab](https://user-images.githubusercontent.com/18664267/53328900-a4868b00-38f3-11e9-973b-4bc4d91cf187.png)
![Output tab](https://user-images.githubusercontent.com/18664267/53328947-b9fbb500-38f3-11e9-8b96-7a6a0419d027.png)
![Shell integration](https://user-images.githubusercontent.com/18664267/53329865-cd0f8480-38f5-11e9-812d-44831277ea68.png)
![Optional progress window](https://user-images.githubusercontent.com/18664267/53329283-6d64a980-38f4-11e9-9a38-3c9aed74829f.png)



### TODO
* Win32 Installer
* UWP installer
* Release DirLister.Core as NuGet package
#### Ideas and plans for v2.1
* Improve JSON format ([#1](https://github.com/SanderSade/DirLister/issues/1))
* Advanced HTML output ([#9](https://github.com/SanderSade/DirLister/issues/9))
* Folders-only mode ([#5](https://github.com/SanderSade/DirLister/issues/5), [#7](https://github.com/SanderSade/DirLister/issues/7))
* Investigate speeding up gathering files in NTFS drives, e.g. https://github.com/search?l=C%23&q=ntfs&type=Repositories
#### Additional plans for the future
* Move over to .NET 5 (this will be DirLister v3)


#### Advanced
While most of the preferences can be set from UI, there are some advanced options that are available only by manually editing the configuration file (user.config).
* History:
	* DirectoryHistoryLength - maximum number of directory history entries. Defaults to 16
	* FilterHistoryLength - maximum number of filter entries (separate for wildcards and regex). Defaults to 8
* Settings:
	* EnableMultithreading - True/False, defaults to False. Set to true to enable multi-threading for file info gathering and media processing. Should never be enabled for regular hard drives, as it will cause HDD trashing and slow down list creation. Can speed up processing on SSDs.
	* DateFormat - date/time output format for output. Defaults to yyyy-MM-dd HH:mm:ss (ISO 8601). See https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings  for options.
	* CssFile - specify custom CSS file to include to HTML output instead of the [default CSS](https://github.com/SanderSade/DirLister/blob/master/DirLister.Core/Application/Writers/Default.css). This is fullpath filename, not CSS content - and ignored if the file doesn't exist.

### Used components

* Code from [taglib#](https://github.com/mono/taglib-sharp), licensed under [GNU Lesser General Public License v2.1](https://github.com/mono/taglib-sharp/blob/master/COPYING). Heavily modified.
<h3>Version history</h3>


<h4>v2.beta 5</h4>
<ul>
	<li>Further code cleanup</li>
</ul>

<h4>v2.beta 4</h4>
<ul>
	<li>Improve CSV compatibility with RFC 4180 standard</li>
	<li>Add tooltips to many form elements</li>
	<li>Some general code cleanup</li>
</ul>

<h4>v2.beta 3</h4>
<ul>
	<li>Fix issue with incorrect size for large (2GB+) files</li>
</ul>


<h4>v2.beta 2</h4>
<ul>
	<li>Add "Open" button to output folder</li>
	<li>Fix some form element resizing issues</li>
	<li>Initial work on the About tab</li>
	<li>On-demand update check functionality</li>
</ul>

<h4>v2.beta 1</h4>
<ul>
	<li>Everything is new
</ul>