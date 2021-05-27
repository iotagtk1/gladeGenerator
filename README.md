## gladeGenerator
 This is a tool to support development with GTK+ Glade Interface Designer, which analyzes the contents of Glade files and automatically generates event handler syntax in C#.

<a href="https://github.com/iotagtk/archive">Binary Program Only</a>

#### How to use
Place Window, Dialog and Gadget controls at the top of the grade file and name them.
Add a signal and a name to the control, and an event handler will be generated automatically.

#### Run from a terminal

```
$ gladeGenerator -projectName projectPath -projectDir namespace name
```

Browse all Glade files under the project path
Generate ID declaration file and Signal statement under the project path


##### Run from Rider macro can be used
```
$ gladeGenerator -projectName $SolutionName$ -projectDir $SolutionDir$
```

For detailed instructions, please read the readMe text in the program file.
