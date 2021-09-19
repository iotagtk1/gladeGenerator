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


For detailed instructions, please read the readMe text in the program file.
