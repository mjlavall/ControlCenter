using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ControlCenter.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ControlCenter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShoppingList : Page
    {
        public ShoppingList()
        {
            this.InitializeComponent();
            DetailsPanel.Visibility = Visibility.Collapsed;
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                Lists.ItemsSource = db.ShoppingLists.ToList();
                ComboBoxUnit.ItemsSource = db.ShoppingUnits.ToList();
                ComboBoxUnit.SelectedIndex = 0;
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
                var newList = new Models.ShoppingList
                {
                    Title = NewTitle.Text,
                    Created = DateTime.Now
                };
                db.ShoppingLists.Add(newList);
                db.SaveChanges();

                Lists.ItemsSource = db.ShoppingLists.ToList();
            }

            NewTitle.Text = "";
        }
        
        private void Add_Item_Click(object sender, RoutedEventArgs e)
        {
            if (Lists.SelectedItem == null) return;
            int count;
            if(!int.TryParse(NewItemCount.Text, out count)) return;
            
            using (var db = new ApplicationDbContext())
            {
                var unit = ComboBoxUnit.SelectedIndex >= 0 
                            ? db.ShoppingUnits.SingleOrDefault(u => u.Id == ((ShoppingUnit) ComboBoxUnit.SelectedItem).Id)
                            : null;
                var listId = ((Models.ShoppingList)Lists.SelectedItem)?.Id;
                var newItem = new ShoppingItem { Name = NewItem.Text, Count = count, Unit = unit?.Name, ShoppingListId = listId.Value};
                db.ShoppingItems.Add(newItem);
                db.SaveChanges();

                var list = db.ShoppingLists.SingleOrDefault(g => g.Id == listId);
                db.SaveChanges();

                Items.ItemsSource = db.ShoppingItems.Where(i => i.ShoppingListId == list.Id).ToList();
            }

            NewItem.Text = "";
        }

        private void List_Selected(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var listId = ((Models.ShoppingList)Lists.SelectedItem)?.Id;
                var list = db.ShoppingLists.SingleOrDefault(g => g.Id == listId);
                if (list == null) return;

                ListDetailsTitle.Text = list.Title;
                DetailsPanel.Visibility = Visibility.Visible;
                Items.ItemsSource = db.ShoppingItems.Where(i => i.ShoppingListId == list.Id).ToList();
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
                var item = db.ShoppingItems.SingleOrDefault(i => i.Id == id);
                if (item == null) return;
                db.ShoppingItems.Remove(item);
                db.SaveChanges();

                var listId = ((Models.ShoppingList) Lists.SelectedItem)?.Id;
                Items.ItemsSource = db.ShoppingItems.Where(i => i.ShoppingListId == listId).ToList();
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox) sender;
            using (var db = new ApplicationDbContext())
            {
                var item = db.ShoppingItems.SingleOrDefault(i => i.Id == (Guid)cb.Tag);
                if (item == null) return;
                item.Complete = cb.IsChecked ?? false;
                db.SaveChanges();
            }
        }
    }
}
