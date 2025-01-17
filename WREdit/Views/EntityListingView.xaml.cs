﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WREdit.Views
{
    public partial class EntityListingView : UserControl
    {
        public EntityListingView()
        {
            InitializeComponent();
        }

        private void RevealAddOptions(object sender, RoutedEventArgs e)
        {
            var menuResource = FindResource("AddOptionsMenu");

            if (menuResource is ContextMenu contextMenu)
            {
                contextMenu.PlacementTarget = PrimaryAdd;
                contextMenu.Placement = PlacementMode.Bottom;
                contextMenu.IsOpen = true;
            }
        }
    }
}
