********### Introduction

### Environment
.net5

GtkSharp

Rider 2021 or Terminal or VisualSutdio


### Rider Setting
ExploerPanel - right click - edit execution configuration - external tools

![alt text](./readMe/1.png)

Set up external tools. Set the arguments

![alt text](./readMe/3.png)

Uncheck Run after file sync.

![alt text](./readMe/5.png)

execution
TopMenu - Tool - ExternalTool

Right-click on the Exploer bar
You can run it from an external tool

### Arguments Macro Required

Set the path of the program
You must specify a macro
copy perst****

``` Rider arguments macro require
-projectPath $FilePath$ -projectName $SolutionName$ -fileDir $FilePath$ -saveDir $SolutionDir$
```
****

The working directory can be empty.

#### Description

projectName Used for the namespace of the exported program. Rider doesn't have a macro for namespaces, so we need to fill in ProjetctPath
projectPath　Required to get accurate namespace
fileDir The file or folder selected in Rider's explorer.
saveDir The directory to be saved. The default setting is the project folder.

### ConfigSetting.xml
```
<Setting AddSaveFolder="" isCodeHint ="true" codeHitFolder="codeHint" />
```

isCodeHint    We will also export a sample of the code when we export it.
codeHitFolder Name of the folder with the code samples

CodeHint can be customized

### GladFileClassMap.xml
Overrides the name of the class to be written out

```
<gladeFileMap>
  <gladeFile targetFileName="" reNameClassName="" />
</gladeFileMap>
```
targetFileName　Write the glade file name. Include extension
reNameClassName　Write the class name to be rewritten

#### NoImportGladeFileSetting.xml
You can prevent the specified grade file from being loaded.

```
<NoImportGladeFile>
  <gladFile targetFileName="" />
</NoImportGladeFile>
```
#### template.txt
The contents of the exported class can be changed

#### Automatic generation of declarations
Every time you add a control to the Glade file, a declaration statement is added

````
using System;
using Gtk;
Using UI = Gtk.Builder.ObjectAttribute;
namespace testGtkApplication
{
    partial class MainWindow
    {    
		//[UI] private readonly Gtk.Window MainWindow = null;
		[UI] private readonly Gtk.Box sdfsdfsd111 = null;
		[UI] private readonly Gtk.Button _button1 = null;		
    }
}
````

### Auto-generated content

#### Automatic generation of event handlers
Every time you add a signal to the control, an event handler statement is added.

```
using System;
using Gtk;
Using UI = Gtk.Builder.ObjectAttribute;
namespace testGtkApplication
{
    partial class MainWindow
    {
	    private void on__button1_Clicked(object sender , EventArgs e){
			
		}	    
    }
}
````

````
partial class MainWindow
````

Add "partial" before "class" in the class file you want to use

````
Use the IntelliSense feature in the name field of the handler of the grade
````
Enter 'On' in the Name field of the grade handler. IntelliSense will work.