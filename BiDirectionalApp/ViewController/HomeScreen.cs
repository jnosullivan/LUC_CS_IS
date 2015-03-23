using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace BiDirectionalApp {
	public class HomeScreen : UIViewController {
		UITableView table;
		TableSource tableSource;




		List<String> stringItems = new List<String>();



	
		public HomeScreen ()
		{	
		}

		UIBarButtonItem edit,dismiss;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.Title = "My Data";

			table = new UITableView(View.Bounds); // defaults to Plain style
			table.AutoresizingMask = UIViewAutoresizing.All;
			List<TableItem> tableItems = new List<TableItem>();



			tableSource = new TableSource(tableItems);
			table.Source = tableSource;




			dismiss = new UIBarButtonItem(UIBarButtonSystemItem.Done, (s,e)=>{
			

				var del = (AppDelegate)UIApplication.SharedApplication.Delegate;

				del.dataItems = stringItems;

				this.DismissModalViewControllerAnimated(true);

			});


			edit = new UIBarButtonItem(UIBarButtonSystemItem.Add, (s,e)=>{



				UIAlertController alert = UIAlertController.Create ("Create Data", "Format: \n 'a b 130' ", UIAlertControllerStyle.Alert);

				alert.AddAction (UIAlertAction.Create ("Add", UIAlertActionStyle.Default, action => {
					// This code is invoked when the user taps on login, and this shows how to access the field values
					Console.WriteLine ("User: {0}/", alert.TextFields[0].Text);
					tableItems.Add (new TableItem(alert.TextFields[0].Text) { SubHeading="", ImageName=""});
					stringItems.Add(alert.TextFields[0].Text);
					tableSource = new TableSource(tableItems);
					table.Source = tableSource;
					table.ReloadData();
				}));

				alert.AddTextField ((field) => {
					field.Placeholder = "data";
				});
			
				PresentViewController (alert, animated: true, completionHandler: null);



			});

			NavigationItem.RightBarButtonItem = edit;

			NavigationItem.LeftBarButtonItem = dismiss;


			Add (table);
		}


	}
}