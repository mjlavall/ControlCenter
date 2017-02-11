using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ControlCenter.Models;
using System.Windows.Input;
using Windows.System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ControlCenter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoList : Page
    {
        public TodoList()
        {
            this.InitializeComponent();
            DetailsPanel.Visibility = Visibility.Collapsed;
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                Lists.ItemsSource = db.TodoLists.ToList();
            }
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        
        private void Add_List_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var newList = new Models.TodoList
                {
                    Title = NewTitle.Text
                };
                db.TodoLists.Add(newList);
                db.SaveChanges();

                Lists.ItemsSource = db.TodoLists.ToList();
            }

            NewTitle.Text = "";
        }
        
        private void Add_Item_Click(object sender, RoutedEventArgs e)
        {
            if (Lists.SelectedItem == null) return;
            
            using (var db = new ApplicationDbContext())
            {
                var listId = ((Models.TodoList)Lists.SelectedItem)?.Id;
                var newItem = new TodoItem { Title = NewItem.Text, TodoListId = listId.Value};
                db.TodoItems.Add(newItem);
                db.SaveChanges();

                var list = db.TodoLists.SingleOrDefault(g => g.Id == listId);
                db.SaveChanges();

                Items.ItemsSource = db.TodoItems.Where(i => i.TodoListId == list.Id).ToList();
            }

            NewItem.Text = "";
        }

        private void List_Selected(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var listId = ((Models.TodoList)Lists.SelectedItem)?.Id;
                var list = db.TodoLists.SingleOrDefault(g => g.Id == listId);
                if (list == null) return;

                ListDetailsTitle.Text = list.Title;
                DetailsPanel.Visibility = Visibility.Visible;
                Items.ItemsSource = db.TodoItems.Where(i => i.TodoListId == list.Id).ToList();
            }
        }

        private void ItemList_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var id = (Guid) button.Tag;
            using (var db = new ApplicationDbContext())
            {
                var item = db.TodoItems.SingleOrDefault(i => i.Id == id);
                if (item == null) return;
                db.TodoItems.Remove(item);
                db.SaveChanges();

                var listId = ((Models.TodoList) Lists.SelectedItem)?.Id;
                Items.ItemsSource = db.TodoItems.Where(i => i.TodoListId == listId).ToList();
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox) sender;
            using (var db = new ApplicationDbContext())
            {
                var item = db.TodoItems.SingleOrDefault(i => i.Id == (Guid)cb.Tag);
                if (item == null) return;
                item.Complete = cb.IsChecked ?? false;
                db.SaveChanges();
            }
        }
    }
}
