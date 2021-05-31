### Introduction

### Environment
.net5

Rider 2021 or Terminal


### Rider Setting
ExploerPanel - right click - edit execution configuration - external tools

![alt text](./readMe/1.png)

Set up external tools. Set the arguments
![alt text](./readMe/3.png)

execution
TopMenu - Tool - ExternalTool

Right-click on the Exploer bar
You can run it from an external tool

### Arguments Macro Required

Set the path of the program
You must specify a macro
copy perst

```Rider arguments macro require
-projectName $SolutionName$ -projectDir $SolutionDir$
```

### ConfigSetting.xml
```
<Setting SaveFolder="" />
```
SaveFolder Add a folder to save.

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

Translated with www.DeepL.com/Translator (free version)




