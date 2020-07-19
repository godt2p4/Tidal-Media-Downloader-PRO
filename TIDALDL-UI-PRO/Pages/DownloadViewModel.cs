﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using AIGS.Common;
using AIGS.Helper;
using Stylet;
using Tidal;
using TIDALDL_UI.Else;

namespace TIDALDL_UI.Pages
{
    public class DownloadViewModel : Screen
    {
        public BindableCollection<TaskViewModel> DownloadList { get; set; }
        public BindableCollection<TaskViewModel> CompleteList { get; set; }
        public BindableCollection<TaskViewModel> ErrorList { get; set; }

        public bool VisibilityDownload { get; set; } = true;
        public bool VisibilityComplete { get; set; }
        public bool VisibilityError{ get; set; }

        public string PageHeader { set { } get {
                if (VisibilityDownload) return "Download";
                if (VisibilityComplete) return "Complete";
                return "Error";
            }  }

        public DownloadViewModel()
        {
            DownloadList = new BindableCollection<TaskViewModel>();
            CompleteList = new BindableCollection<TaskViewModel>();
            ErrorList = new BindableCollection<TaskViewModel>();
        }

        public void AddTask(object data)
        {
            DownloadList.Insert(0, new TaskViewModel(data, this));
            SelectPage("download");
        }

        public void DeleteTask(TaskViewModel from)
        {
            if (DownloadList.Contains(from))
                DownloadList.Remove(from);
            if (CompleteList.Contains(from))
                CompleteList.Remove(from);
            if (ErrorList.Contains(from))
                ErrorList.Remove(from);
        }

        public void TaskComplete(TaskViewModel from)
        {
            DownloadList.Remove(from);
            CompleteList.Insert(0, from);
        }

        public void TaskError(TaskViewModel from)
        {
            DownloadList.Remove(from);
            ErrorList.Insert(0, from);
        }

        #region select page
        private void SelectPage(string name)
        {
            VisibilityDownload = false;
            VisibilityComplete = false;
            VisibilityError = false;
            if (name == "download")
                VisibilityDownload = true;
            else if (name == "complete")
                VisibilityComplete = true;
            else
                VisibilityError = true;
        }
        public void SelectDownloadPage() => SelectPage("download");
        public void SelectCompletePage() => SelectPage("complete");
        public void SelectErrorPage() => SelectPage("error");
        #endregion
    }



}