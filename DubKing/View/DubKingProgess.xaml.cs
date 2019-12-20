using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class DubKingProgress : Window
    {
        static DubKingProgress _progresBar;
        static public DubKingProgress ProgressBarWindow
        {
            get
            {
                if (_progresBar == null)
                {
                    _progresBar = new DubKingProgress();
                }
                return _progresBar;
            }
        }
        private DubKingProgress()
        {
            InitializeComponent();
        }

        static public void ShowProgress()
        {
            ProgressBarWindow.Show();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
            
        }

        static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(100);
            }
        }

        static void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBarWindow.pbStatus.Value = e.ProgressPercentage;
        }
    }
}
