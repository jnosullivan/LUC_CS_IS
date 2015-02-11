using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace BiDirectionalApp {
	public class HomeScreen : UIViewController {
		UITableView table;

		public HomeScreen ()
		{	
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			table = new UITableView(View.Bounds); // defaults to Plain style
			table.AutoresizingMask = UIViewAutoresizing.All;
			string[] tableItems = new string[] {"Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers"};
			table.Source = new TableSource(tableItems);
			Add (table);
		}
	}
}