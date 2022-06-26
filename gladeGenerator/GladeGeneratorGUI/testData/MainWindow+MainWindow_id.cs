using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GladeGeneratorGUI
{
    partial class MainWindow
    {
    
    
		//[UI] private readonly Gtk.Window MainWindow = null;
		[UI] private Gtk.Label _label1 = null;
		[UI] private Gtk.Button _button1 = null;
	
		//[UI] private Gtk.Window MainWindow = null;
		[UI] private Gtk.Button stopTrackBtn = null;
		[UI] private Gtk.Button startTrackBtn = null;
		[UI] private Gtk.Label textLable = null;
		[UI] private Gtk.SpinButton rsiAboveNumBtn = null;
		[UI] private Gtk.Button appEndBtn = null;
		[UI] private Gtk.Label RsiText = null;
		[UI] private Gtk.Label CoinLabel = null;
		[UI] private Gtk.TreeView treeView1 = null;
		[UI] private Gtk.TreeSelection treeView_section = null;
		[UI] private Gtk.Label MacdText = null;
		[UI] private Gtk.Label CommentText = null;
		[UI] private Gtk.Button TestPlayBtn = null;
		[UI] private Gtk.Label updateTimeLabel = null;
		[UI] private Gtk.Label yesterdayText = null;
		[UI] private Gtk.Label todayText = null;
		[UI] private Gtk.Label tomorrowText = null;
		[UI] private Gtk.Label actionText = null;
		[UI] private Gtk.Label actionCommentText = null;
		[UI] private Gtk.CheckButton LPlayerBtn = null;
		[UI] private Gtk.CheckButton SPlayerBtn = null;
		[UI] private Gtk.ComboBox lineIntervalModelComboBox = null;
		[UI] private Gtk.Button CrossBtn_75 = null;
		[UI] private Gtk.CheckButton CrossPlayerBtn_on_75 = null;
		[UI] private Gtk.Label CrossText = null;
		[UI] private Gtk.CheckButton autoCrossBtn_on_70 = null;
		[UI] private Gtk.Label ZeroCrossText = null;
		[UI] private Gtk.Label RirikuCrossText = null;
		[UI] private Gtk.CheckButton ZeroCrossPlayerBtn = null;
		[UI] private Gtk.CheckButton RirikuCrossPlayerBtn = null;
		[UI] private Gtk.Button ZeroCrossBtn = null;
		[UI] private Gtk.Button RirikuBelowCrossBtn = null;
		[UI] private Gtk.Button RirikuAboveCrossBtn = null;
		[UI] private Gtk.CheckButton autoZeroCrossBtn_at_70 = null;
		[UI] private Gtk.Button CalcViewBtn = null;
		[UI] private Gtk.CheckButton autoCrossBtn_on_25 = null;
		[UI] private Gtk.CheckButton autoZeroCrossBtn_at_25 = null;
		[UI] private Gtk.CheckButton autoRirikuCrossBtn_on_25 = null;
		[UI] private Gtk.CheckButton autoRirikuCrossBtn_on_70 = null;
		[UI] private Gtk.SpinButton rsiBelowNumBtn = null;
		[UI] private Gtk.CheckButton debugModeBtn = null;
		[UI] private Gtk.Button scheduleAlertBtn = null;
		[UI] private Gtk.CheckButton CrossPlayerBtn_on_25 = null;
		[UI] private Gtk.CheckButton autoStartBtn = null;
		[UI] private Gtk.CheckButton autoCrossBtn_on_70_add_25 = null;
		[UI] private Gtk.CheckButton CrossPlayerBtn_on_75_add_25 = null;
		[UI] private Gtk.CheckButton jyougenKagenMacdMegaCheckModeBtn = null;
		[UI] private Gtk.Label jyougenKagenMacdMegaCheckModeText = null;
		[UI] private Gtk.Button simulatorPlayBtn = null;
		[UI] private Gtk.Button simulatorGetBtn = null;
		[UI] private Gtk.Button simulatorDateStartBtn = null;
		[UI] private Gtk.Button simulatorDateEndBtn = null;
		[UI] private Gtk.Button simulatorStopBtn = null;
		[UI] private Gtk.Label simulatorText = null;
		[UI] private Gtk.Label CrossText_75_25 = null;
		[UI] private Gtk.Button CrossBtn_25 = null;
		[UI] private Gtk.CheckButton allPlayerMuteBtn = null;
		[UI] private Gtk.CheckButton TreeViewOffBtn = null;
		[UI] private Gtk.Button trackStartBtn = null;
		[UI] private Gtk.Button trackEndBtn = null;
		[UI] private Gtk.CheckButton autoHistgram_on_75 = null;
		[UI] private Gtk.CheckButton autoHistgram_on_25 = null;
		[UI] private Gtk.CheckButton HistgramPlayer_on_75 = null;
		[UI] private Gtk.CheckButton HistgramPlayer_on_25 = null;
	
		[UI] private Gtk.TreeView treeView = null;
		[UI] private Gtk.TreeSelection treeVie_sellection = null;
	}
}
