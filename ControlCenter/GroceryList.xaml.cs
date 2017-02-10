using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class GroceryList : Page
    {
        public GroceryList()
        {
            this.InitializeComponent();
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                Lists.ItemsSource = db.GroceryLists.ToList();
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
                var newList = new Models.GroceryList
                {
                    Title = NewTitle.Text,
                    Created = DateTime.Now
                };
                db.GroceryLists.Add(newList);
                db.SaveChanges();

                Lists.ItemsSource = db.GroceryLists.ToList();
            }
        }
        
        private void Add_Item_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var newItem = new GroceryItem { Name = NewItem.Text };
                var listId = ((Models.GroceryList) Lists.SelectedItem)?.Id;
                var list = db.GroceryLists.SingleOrDefault(g => g.Id == listId);
                if(list.Items == null) list.Items = new List<GroceryItem>();
                list.Items.Add(newItem);
                db.SaveChanges();

                Items.ItemsSource = list.Items.ToList();
            }
        }

        private void List_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationDbContext())
            {
                var listId = ((Models.GroceryList)Lists.SelectedItem)?.Id;
                var list = db.GroceryLists.SingleOrDefault(g => g.Id == listId);
                if (list == null) return;

                Items.ItemsSource = list.Items?.ToList();
            }
        }
    }
}
