using CommunityToolkit.Mvvm.Input;
using SourceBindingTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SourceBindingTest.ViewModels
{
    public partial class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<ItemModel> _items;
        public List<ItemModel> Items {
            get => _items;
            set
            {
                _items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }

        private ItemModel _selectedItem;
        public ItemModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        //public ICommand SelectCommand { get; set; }

        public MainPageViewModel() {
            Items = new List<ItemModel>()
            {
                new ItemModel {Id = 1, Name = "One"},
                new ItemModel {Id = 2, Name = "Two"},
                new ItemModel {Id = 3, Name = "Three"},
                new ItemModel {Id = 4, Name = "Four"}
            };

            //SelectCommand = new Command<object>(SelectCurrentItem);
        }

        [RelayCommand]
        private void SelectCurrentItem(object item)
        {
            if (item is ItemModel itemModel) {
                itemModel.SelectedAt = DateTime.UtcNow;
                SelectedItem = itemModel;
            }
            else
            {
                SelectedItem = new ItemModel { Id = -1, Name = "Unknown!", SelectedAt = DateTime.UtcNow };
            }
        }
    }
}
