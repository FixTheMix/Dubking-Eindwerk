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
using System.Windows.Shapes;

namespace DubKing.View
{
    /// <summary>
    /// Interaction logic for ConfirmDelete.xaml
    /// </summary>
    public partial class ConfirmDelete : Window
    {
        private bool _isConfirmed;

        public ConfirmDelete()
        {
            InitializeComponent();
        }

        public bool GetConfirmation(string objectName, string type)
        {
            Object.Text = objectName;
            Message.Text = Message.Text.Replace("--::PlaceHolder::--", type);
            ShowDialog();
            return _isConfirmed;
        }

        private void OnDelete_Click(object sender, RoutedEventArgs e)
        {
            _isConfirmed = true;
            Close();
        }

        private void OnCancel_Click(object sender, RoutedEventArgs e)
        {
            _isConfirmed = false;
            Close();
        }
    }
}
