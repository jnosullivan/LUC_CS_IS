using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;


namespace BiDirectionalApp
{
	public partial class RootViewController : UIViewController
	{

		private readonly IList<string> colors = new List<string>
		{
			"BiDirectional",
			"Dijkstra's",
			"...",
			"...",
			"..."
		};
		private string typeapply;
		protected Graph graph;
		Problem task;
		Foundation algo;
		//UIRotationGestureRecognizer rotateGesture;
		//UIPanGestureRecognizer panGesture;
		//UILongPressGestureRecognizer longGesture;
		UIScrollView scrollView;
		//WorkView newWorkView = new WorkView();
		public Foundation foundation = new Foundation();
		//public Dictionary<String,int> ar = new Dictionary<String,int>(); 
		//int ViewTempTag;
		public RootViewController (IntPtr handle) : base (handle)
		{
			ModelController = new ModelController ();
		}
			

		public ModelController ModelController {
			get;
			private set;
		}

		public UIPageViewController PageViewController {
			get;
			private set;
		}
		/*
		partial void actnButtonClickadd (UIButton sender) {

			Console.WriteLine("Add node to view");


			string[] data = { "a", "b", "c","d","e","f","g","h","i","j","k"};

			foreach (string value in data)
			{
				algo.addNode(value);
			}




		}
		*/

		/*
		partial void addNode (UIBarButtonItem sender) {
			Console.WriteLine("Add node to view");


			string[] data = { "a", "b", "c","d","e","f","g","h","i","j","k"};

			foreach (string value in data)
			{
				algo.addNode(value);
			}
		}
		partial void NextStep (UIBarButtonItem sender) {
			Run();
		}
		*/

		public void getAngle() {
			/*
			PointF first = new PointF(100, 100);
			PointF second = new PointF(200, 200);

			Console.WriteLine("firstX:" + first.X + "secondX:" + second.X);
			Console.WriteLine("firstY:" + first.Y + "secondY:" + second.Y);

			float sideone = second.Y - first.Y;
			float sidetwo = second.X - first.X;

			double insidevalue = ((sideone * sideone) + (sidetwo * sidetwo));
			double thirdside = Math.Sqrt (insidevalue);

			Console.WriteLine("1: " + sideone + " 2: " + sidetwo + " 3: " + thirdside);
*/
		}
		/*
		partial void actnButtonClick (UIButton sender){
			Run();


		}
	*/

		partial void diss (UITextField sender) {

		}
		partial void thenext (UIBarButtonItem sender) {
			Run();
		}
		partial void add (UIBarButtonItem sender) {

			string[] data = { "a", "b", "c","d","e","f","g","h","i","j","k"};

			foreach (string value in data)
			{
				algo.addNode(value);
			}

		}

		public void setProblem(string a, string b)
		{
			task.start = a;
			task.end = b;
			task.forwards = new Queue<List<Edge>>();
			task.backwards = new Queue<List<Edge>>();
			Edge edge1 = new Edge(task.start,task.start);
			List<Edge> list1 = new List<Edge>();
			list1.Add(edge1);
			task.forwards.Enqueue(list1);
			Edge edge2 = new Edge(task.end, task.end);
			List<Edge> list2 = new List<Edge>();
			list2.Add(edge2);
			task.backwards.Enqueue(list2);
			task.checkedforwards = new HashSet<string>();
			task.checkedbackwards = new HashSet<string>();
			task.checkedforwards.Add(task.start);
			task.checkedbackwards.Add(task.end);
			algo.setProblem(task);
		}
		public void setGraph(string path)
		{
			graph = new Graph();
			graph.ReadEdges(path);
		}
		public void Run()
		{

			task = algo.Run(graph);
			if (task.result != " ")
			{
				List<Edge> way = task.getWays();


				string s = "";
				for (int i = 1; i < way.Count-1; i++)
				{
					s = s + way[i].lA.ToString() + "-" + way[i].lB.ToString() + ";";
				}
				Console.WriteLine (s);


				algo.drawDone (way);

			}
		}
		public void DrawLine()
		{

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			algo = new Foundation();
			task = new Problem();




			//ViewTempTag = 0;
			var documents =
				Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine (documents, "test.txt");
			File.WriteAllText(filename, "a c 100\na b 50\na f 200\na e 250\nb c 150\ne i 110\ne f 155\nf c 180\nf g 190\ng c 120\ng h 100\nc h 120\nc d 200\nd h 150\nh l 140\ng l 50\nj k 200\ni l 500\nf k 100\ne j 300");

			//"e","f","g","h","i","j","k"
			//var text = System.IO.File.ReadAllText("TestData/test.txt");
			setGraph (filename);
			setProblem ("a","j");
			// Configure the page view controller and add it as a child view controller.
			PageViewController = new UIPageViewController (UIPageViewControllerTransitionStyle.PageCurl, UIPageViewControllerNavigationOrientation.Horizontal, UIPageViewControllerSpineLocation.Min);
			PageViewController.WeakDelegate = this;

			var startingViewController = ModelController.GetViewController (0, Storyboard);
			var viewControllers = new UIViewController[] { startingViewController };
			PageViewController.SetViewControllers (viewControllers, UIPageViewControllerNavigationDirection.Forward, false, null);

			PageViewController.WeakDataSource = ModelController;

			AddChildViewController (PageViewController);
			//View.AddSubview (PageViewController.View);

			// Set the page view controller's bounds using an inset rect so that self's view is visible around the edges of the pages.
			var pageViewRect = View.Bounds;
			pageViewRect = new RectangleF (pageViewRect.X + 20, pageViewRect.Y + 20, pageViewRect.Width - 40, pageViewRect.Height - 40);
			PageViewController.View.Frame = pageViewRect;

			PageViewController.DidMoveToParentViewController (this);

			// Add the page view controller's gesture recognizers to the book view controller's view so that the gestures are started more easily.
			View.GestureRecognizers = PageViewController.GestureRecognizers;


			scrollView = new UIScrollView (
				new RectangleF (0, 65, 320
					, 410));
			View.AddSubview (scrollView);
			scrollView.ContentSize = foundation.Frame.Size;
			scrollView.AddSubview (algo);

			// Setup the picker and model
			PickerModel model = new PickerModel(this.colors);
			model.PickerChanged += (sender, e) => {
				this.typeapply = e.SelectedValue;
			};

			UIPickerView picker = new UIPickerView();
			picker.ShowSelectionIndicator = true;
			picker.Model = model;

			// Setup the toolbar
			UIToolbar toolbar = new UIToolbar();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit();

			// Create a 'done' button for the toolbar and add it to the toolbar
			UIBarButtonItem doneButton = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done,
				(s, e) => {
					this.TextField.Text = typeapply;
					this.TextField.ResignFirstResponder();
				});
			toolbar.SetItems(new UIBarButtonItem[]{doneButton}, true);

			// Tell the textbox to use the picker for input
			this.TextField.InputView = picker;
			//this.next.InputView = picker;
			// Display the toolbar over the pickers
			this.TextField.InputAccessoryView = toolbar;


		}

		[Export ("pageViewController:spineLocationForInterfaceOrientation:")]
		public UIPageViewControllerSpineLocation GetSpineLocation (UIPageViewController pageViewController, UIInterfaceOrientation orientation)
		{
			UIViewController currentViewController;
			UIViewController[] viewControllers;

			if (orientation == UIInterfaceOrientation.Portrait || orientation == UIInterfaceOrientation.PortraitUpsideDown) {
				// In portrait orientation: Set the spine position to "min" and the page view controller's view controllers array to contain just one view controller.
				// Setting the spine position to 'UIPageViewControllerSpineLocation.Mid' in landscape orientation sets the doubleSided property to true, so set it to false here.
				currentViewController = pageViewController.ViewControllers [0];
				viewControllers = new UIViewController[] { currentViewController };
				pageViewController.SetViewControllers (viewControllers, UIPageViewControllerNavigationDirection.Forward, true, null);

				pageViewController.DoubleSided = false;

				return UIPageViewControllerSpineLocation.Min;
			}

			// In landscape orientation: Set set the spine location to "mid" and the page view controller's view controllers array to contain two view controllers.
			// If the current page is even, set it to contain the current and next view controllers; if it is odd, set the array to contain the previous and current view controllers.
			currentViewController = pageViewController.ViewControllers [0];

			int index = ModelController.IndexOf ((DataViewController)currentViewController);
			if (index == 0 || index % 2 == 0) {
				var nextViewController = ModelController.GetNextViewController (pageViewController, currentViewController);
				viewControllers = new UIViewController[] {
					currentViewController,
					nextViewController
				};
			} else {
				var previousViewController = ModelController.GetPreviousViewController (pageViewController, currentViewController);
				viewControllers = new UIViewController[] {
					previousViewController,
					currentViewController
				};
			}

			pageViewController.SetViewControllers (viewControllers, UIPageViewControllerNavigationDirection.Forward, true, null);

			return UIPageViewControllerSpineLocation.Mid;
		}

		partial void UIButton204_TouchUpInside (UIButton sender)
		{
			//throw new NotImplementedException ();



		}
	}
}

