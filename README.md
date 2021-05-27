## gladeGenerator
 This is a tool to support development with GTK+ Glade Interface Designer, which analyzes the contents of Glade files and automatically generates event handler syntax in C#.
 
Place Window, Dialog and Gadget controls at the top of the grade file and name them.
Add a signal and a name to the control, and an event handler will be generated automatically.

<a href="https://github.com/iotagtk/archive">Binary Program Only</a>

#### How to use
1. Design a form with Grade Design.
1. Fill in the ID and name. Only the name of the top Window and the controls needed for exporting.
1. If you want to add an event to the control, fill in the signal. The name can be anything you want.
1. Run the gladeGenerator. You can run it from the command line or Rider.
1. The event handler file will be automatically generated in the same project.

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
