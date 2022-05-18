using System;
using System.Globalization;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GtkDateTimePicker
{
    
    /*
     　  set EmbedResorce
     
     *   DateDialog DateDialog1 = new DateDialog();

         DateDialog1.DateTimeObj = DateTime.Now;

         var r = DateDialog1.Run();

         if (r == 1) {
             Console.WriteLine(DateDialog1.DateTimeObj);
         }
     *
     */
    
    
     partial class DateDialog : Dialog
    {
        
        [UI] private Gtk.Button cancelBtn = null;
        [UI] private Gtk.Button okBtn = null;
        [UI] private Gtk.Calendar dateCalendar = null;
        [UI] private Gtk.SpinButton hoursBtn = null;
        [UI] private Gtk.SpinButton minitusBtn = null;
        
        
        private DateTime _dateTimeObj;
        public DateTime DateTimeObj
        {
            get
            {

                string dateStr = dateCalendar.Year.ToString() + "-" + (dateCalendar.Month+1).ToString() + "-" + dateCalendar.Day + " " +
                    hoursBtn.ValueAsInt.ToString() + ":" + minitusBtn.ValueAsInt.ToString();

                DateTime _dateTimeObj = DateTime.ParseExact(dateStr, "yyyy-M-d H:m",CultureInfo.CurrentCulture);
                    
                return _dateTimeObj;
            }
            set
            {
                _dateTimeObj = value;
                dateCalendar.Date = _dateTimeObj;
                hoursBtn.Value = value.Hour;
                minitusBtn.Value = value.Minute;
            }
        }
        
        private void on_cancelBtn_clicked(object sender , EventArgs e){
            this.Destroy();
        }
		
        private void on_okBtn_clicked(object sender , EventArgs e){
            this.Destroy();
        }

        public DateDialog() : this(new Builder("DateDialog.glade"))
        {
            
        }
        private DateDialog(Builder builder) : base(builder.GetRawOwnedObject("DateDialog"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
        }
        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            this.Destroy();
        }

        
    }
}