﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MaintenancePlatform.Views.Systems;
using Telerik.Windows.Data;
using ZNC.DataAnalysis.BIZ.Systems;
using ZNC.DataEntiry;
using ZNC.Utility.Command;

namespace MaintenancePlatform.ViewModels.Systems
{

    /// <summary>
    /// AndrewChien 2017/10/22 10:34:20
    /// AutomaticCoder代码生成器生成
    /// </summary>
    public class SystemServiceVM : ChildPageViewModel
    {
        SystemServiceView View;
        internal void PageLoad(object sender, RoutedEventArgs e)
        {
            View = (SystemServiceView)sender;
        }

        #region Command
        private ICommand _BtnSearch;
        public ICommand BtnSearch
        {
            get
            {
                if (_BtnSearch == null)
                {
                    _BtnSearch = new DelegateCommand<object>(BtnSearch_Click);
                }
                return _BtnSearch;
            }
        }
        /// <summary>
        /// 查询记录
        /// </summary>
        private void BtnSearch_Click(object sender)
        {
            ObservableCollection<SystemService> source = new SystemServiceBIZ().SelectAll();
            var pagedSource = new QueryableCollectionView(source);
            View.DGSelect.ItemsSource = pagedSource;
            View.searchDataPager.Source = pagedSource;
            View.DGSelect.SelectedItems.Remove(View.DGSelect.SelectedItem);//取消首行选中
        }
        private ICommand _BtnInsert;
        public ICommand BtnInsert
        {
            get
            {
                if (_BtnInsert == null)
                {
                    _BtnInsert = new DelegateCommand<object>(BtnInsert_Click);
                }
                return _BtnInsert;
            }
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        private void BtnInsert_Click(object sender)
        {
            // NavigationService.Navigate(new Uri("/Views/Maintenance/RFIDAddView.xaml", UriKind.RelativeOrAbsolute));
            SystemServiceEditView view = new SystemServiceEditView(null);
            view.ShowDialog();
            BtnSearch_Click(null);
        }

        private ICommand _BtnDelete;
        public ICommand BtnDelete
        {
            get
            {
                if (_BtnDelete == null)
                {
                    _BtnDelete = new DelegateCommand<object>(BtnDelete_Click);
                }
                return _BtnDelete;
            }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        private void BtnDelete_Click(object sender)
        {
            if (View.DGSelect.SelectedItem == null)
            {
                MessageBox.Show("请选择要删除的功能模块！");
            }
            foreach (var item in View.DGSelect.Items)
            {

                if (View.DGSelect.SelectedItem == item)
                {
                    SystemService model = item as SystemService;
                    new SystemServiceBIZ().delete(model);
                    MessageBox.Show("删除成功！");
                    BtnSearch_Click(null);
                }
            }
        }

        private ICommand _BtnUpdate;
        public ICommand BtnUpdate
        {
            get
            {
                if (_BtnUpdate == null)
                {
                    _BtnUpdate = new DelegateCommand<object>(BtnUpdate_Click);
                }
                return _BtnUpdate;
            }
        }
        /// <summary>
        /// 修改记录
        /// </summary>
        private void BtnUpdate_Click(object sender)
        {
            if (View.DGSelect.SelectedItem == null)
            {
                MessageBox.Show("请选择要修改的记录！");
            }
            foreach (var item in View.DGSelect.Items)
            {

                if (View.DGSelect.SelectedItem == item)
                {
                    SystemService model = item as SystemService;
                    //FuncModule gnmk = (FuncModule)sender;
                    SystemServiceEditView view = new SystemServiceEditView(model);
                    view.ShowDialog();
                    BtnSearch_Click(null);

                }
            }
        }
        #endregion
        #region
        private ObservableCollection<SystemService> _SystemServiceCollection;
        public ObservableCollection<SystemService> SystemServiceCollection
        {
            get
            {
                if (_SystemServiceCollection == null)
                {
                    _SystemServiceCollection = new ObservableCollection<SystemService>();
                }
                return _SystemServiceCollection;
            }
            set
            {
                base.SetValue(ref _SystemServiceCollection, value, () => this.SystemServiceCollection);

            }
        }
        #endregion
    }
}
