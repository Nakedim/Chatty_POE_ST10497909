using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberChat
{
   
    public partial class DateTimePicker : UserControl
    {
       
        private bool _isInitializing = true;

        // Dependency Property so you can bind to this control in XAML
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(DateTime?), typeof(DateTimePicker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedValuePropertyChanged));

        public DateTime? SelectedValue
        {
            get => (DateTime?)GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }

        public DateTimePicker()
        {
            InitializeComponent();
            PopulateTimePickers();

            // Default to today's date
            InnerCalendar.SelectedDate = DateTime.Today;
            _isInitializing = false;

            UpdateCombinedDateTime();
        }

        private void PopulateTimePickers()
        {
            for (int i = 0; i < 24; i++) HoursCombo.Items.Add(i.ToString("D2"));
            for (int i = 0; i < 60; i += 5) MinutesCombo.Items.Add(i.ToString("D2")); // 5-minute intervals

            // Default values
            HoursCombo.SelectedValue = DateTime.Now.Hour.ToString("D2");
            MinutesCombo.SelectedValue = "00";
        }

        private void OnSelectedDateChanged(object sender, SelectionChangedEventArgs e) => UpdateCombinedDateTime();
        private void OnTimeChanged(object sender, SelectionChangedEventArgs e) => UpdateCombinedDateTime();

        private void UpdateCombinedDateTime()
        {
            if (_isInitializing) return;

            if (InnerCalendar.SelectedDate is DateTime baseDate &&
                HoursCombo.SelectedValue is string hrStr && int.TryParse(hrStr, out int hours) &&
                MinutesCombo.SelectedValue is string minStr && int.TryParse(minStr, out int minutes))
            {
                SelectedValue = baseDate.Date.AddHours(hours).AddMinutes(minutes);
                TxtSummary.Text = SelectedValue.Value.ToString("g"); // Displays: dd/MM/yyyy HH:mm
            }
        }

        private static void OnSelectedValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Syncs the UI if the property is changed externally via code
            if (d is DateTimePicker control && e.NewValue is DateTime newDateTime && control._isInitializing == false)
            {
                control._isInitializing = true;
                control.InnerCalendar.SelectedDate = newDateTime.Date;
                control.HoursCombo.SelectedValue = newDateTime.Hour.ToString("D2");
                control.MinutesCombo.SelectedValue = ((newDateTime.Minute / 5) * 5).ToString("D2"); // Snap to nearest 5
                control.TxtSummary.Text = newDateTime.ToString("g");
                control._isInitializing = false;
            }
        }
    }
}
