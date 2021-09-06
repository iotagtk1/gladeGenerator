## gladeGenerator
 This is a tool to support development with GTK+ Glade Interface Designer, which analyzes the contents of Glade files and automatically generates event handler syntax in C#.
 
Place Window, Dialog and Gadget controls at the top of the grade file and name them.
Add a signal and a name to the control, and an event handler will be generated automatically.

<a href="https://github.com/iotagtk/gladeGenerator/releases/tag/gladeGenerator">Release</a>

#### How to use
1. Design a form in the Grade Designer.
1. Fill in the ID and name for the control. Fill in the name on the top Window control. Fill in the controls you need to export as well.
1. Fill in the signal to the control if you want to add an event. The name can be appropriate.
1. Run the gladeGenerator.
1. The event handler file will be automatically generated in the same project

#### Run from a terminal

```
$ gladeGenerator -projectName namespace name -projectDir projectPath
```

Browse all Glade files under the project path
Generate ID declaration file and Signal statement under the project path


##### Run from Rider macro can be used
```
$ gladeGenerator -projectName $SolutionName$ -fileDir $FilePath$ -saveDir $SolutionDir$
```

### Arguments Macro Required

Set the path of the program
You must specify a macro
copy perst

```Rider arguments macro require
-projectName $SolutionName$ -projectDir $SolutionDir$ -fileDir $FilePath$
```

### ConfigSetting.xml
```
<Setting SaveFolder="" isWinNameHandlerInclude="true" />
```
SaveFolder Add a folder to save.

isWinNameHandlerInclude　Whether or not to enter the parent ID name in the signal name
true or false

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


For detailed instructions, please read the readMe text in the program file.
