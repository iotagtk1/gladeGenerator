###Introduction

###Environment
.net5

Rider 2021 or Terminal


###Rider Setting
ExploerPanel - right click - edit execution configuration - external tools

![alt text](./readMe/1.png)

Set up external tools. Set the arguments
![alt text](./readMe/3.png)

execution
TopMenu - Tool - ExternalTool

Right-click on the Exploer bar
You can run it from an external tool

###Arguments Macro Required

Set the path of the program
You must specify a macro
copy perst

```Rider arguments macro require
-projectName $SolutionName$ -projectDir $SolutionDir$
```

###ConfigSetting.xml
```
<Setting SaveFolder="" />
```
SaveFolder Add a folder to save.

###GladFileClassMap.xml
Overrides the name of the class to be written out

```
<gladeFileMap>
  <gladeFile targetFileName="" reNameClassName="" />
</gladeFileMap>
```
targetFileName　Write the glade file name. Include extension
reNameClassName　Write the class name to be rewritten

####NoImportGladeFileSetting.xml
You can prevent the specified grade file from being loaded.

```
<NoImportGladeFile>
  <gladFile targetFileName="" />
</NoImportGladeFile>
```
####template.txt
The contents of the exported class can be changed





